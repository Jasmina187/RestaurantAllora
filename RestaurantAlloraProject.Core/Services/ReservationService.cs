using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels;
using RestaurantAlloraProjectViewModels.Reservation;
using System;

namespace RestaurantAlloraProject.Core.Services
{
    public class ReservationService : IReservationService
    {
        private const string AllStatuses = "Всички";
        private const string PendingStatus = "Очаква одобрение";
        private const string ApprovedStatus = "Одобрена";
        private const string RejectedStatus = "Отказана";
        private const string AvailableTableStatus = "Свободна";
        private const string ReservedTableStatus = "Резервирана";

        private readonly RestaurantAlloraProjectContext _context;

        public ReservationService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationIndexViewModel>> GetUserReservationsAsync(Guid userId)
        {
            return await BuildReservationQuery()
                .Where(r => r.CustomerId == userId)
                .OrderByDescending(r => r.ReservationDate)
                .Select(r => ToReservationIndexViewModel(r))
                .ToListAsync();
        }

        public async Task<ReservationListViewModel> GetUserReservationsPageAsync(Guid userId, int page, int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            var query = BuildReservationQuery()
                .Where(r => r.CustomerId == userId);

            var totalReservations = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalReservations / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            var reservations = await query
                .OrderByDescending(r => r.ReservationDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => ToReservationIndexViewModel(r))
                .ToListAsync();

            return new ReservationListViewModel
            {
                Reservations = reservations,
                CurrentPage = page,
                PageSize = pageSize,
                TotalReservations = totalReservations
            };
        }

        public async Task<List<ReservationIndexViewModel>> GetPendingReservationsAsync()
        {
            var managementModel = await GetReservationsForManagementAsync(new ReservationFilterViewModel
            {
                Status = PendingStatus,
                PageSize = int.MaxValue
            });

            return managementModel.Reservations;
        }

        public async Task<ReservationManagementViewModel> GetReservationsForManagementAsync(ReservationFilterViewModel filter)
        {
            filter ??= new ReservationFilterViewModel();
            filter.Page = Math.Max(1, filter.Page);
            filter.PageSize = Math.Max(1, filter.PageSize);

            var query = BuildReservationQuery();

            if (!string.IsNullOrWhiteSpace(filter.Status) && filter.Status != AllStatuses)
            {
                query = query.Where(r => r.Status == filter.Status);
            }

            if (filter.Date.HasValue)
            {
                var date = filter.Date.Value.Date;
                query = query.Where(r => r.ReservationDate.Date == date);
            }

            if (filter.TableNumber.HasValue)
            {
                query = query.Where(r => r.Table.TableNumber == filter.TableNumber.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var searchTerm = filter.SearchTerm.Trim();

                query = query.Where(r =>
                    (r.Customer.User.UserName != null && r.Customer.User.UserName.Contains(searchTerm)) ||
                    (r.Customer.User.Email != null && r.Customer.User.Email.Contains(searchTerm)) ||
                    (r.Employee != null && r.Employee.User.UserName != null && r.Employee.User.UserName.Contains(searchTerm)) ||
                    (r.Employee != null && r.Employee.User.Email != null && r.Employee.User.Email.Contains(searchTerm)));
            }

            var totalReservations = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalReservations / (double)filter.PageSize);

            if (totalPages > 0 && filter.Page > totalPages)
            {
                filter.Page = totalPages;
            }

            var reservations = await query
                .OrderBy(r => r.Status == PendingStatus ? 0 : 1)
                .ThenBy(r => r.ReservationDate)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(r => ToReservationIndexViewModel(r))
                .ToListAsync();

            return new ReservationManagementViewModel
            {
                Filter = filter,
                Reservations = reservations,
                Statuses = new List<string> { AllStatuses, PendingStatus, ApprovedStatus, RejectedStatus },
                CurrentPage = filter.Page,
                PageSize = filter.PageSize,
                TotalReservations = totalReservations
            };
        }

        public async Task CreateReservationAsync(ReservationCreateViewModel vm, Guid userId)
        {
            ValidateReservationInput(vm);

            var availableTable = await FindAvailableTableAsync(vm);

            if (availableTable == null)
            {
                throw new ArgumentException(await BuildUnavailableReservationMessageAsync(vm));
            }

            await EnsureCustomerProfileAsync(userId);

            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                TableId = availableTable.TableId,
                CustomerId = userId,
                ReservationDate = vm.ReservationDate,
                NumberOfGuests = vm.NumberOfGuests,
                Status = PendingStatus
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveReservationAsync(Guid id, Guid employeeId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return;
            }

            if (reservation.Status != PendingStatus)
            {
                throw new InvalidOperationException("Само чакащи резервации могат да бъдат одобрявани.");
            }

            var isOccupied = await HasOverlappingApprovedReservationAsync(
                reservation.TableId,
                reservation.ReservationDate,
                reservation.ReservationDate.AddHours(3),
                reservation.ReservationId);

            if (isOccupied)
            {
                throw new InvalidOperationException("Масата вече има одобрена резервация за този часови диапазон.");
            }

            await EnsureEmployeeProfileAsync(employeeId);

            reservation.Status = ApprovedStatus;
            reservation.EmployeeId = employeeId;
            reservation.Table.StatusOfTheTable = ReservedTableStatus;

            await _context.SaveChangesAsync();
        }

        public async Task RejectReservationAsync(Guid id, Guid employeeId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
            {
                return;
            }

            if (reservation.Status == RejectedStatus)
            {
                throw new InvalidOperationException("Резервацията вече е отказана.");
            }

            await EnsureEmployeeProfileAsync(employeeId);

            reservation.Status = RejectedStatus;
            reservation.EmployeeId = employeeId;

            if (!await HasFutureApprovedReservationForTableAsync(reservation.TableId, reservation.ReservationId))
            {
                reservation.Table.StatusOfTheTable = AvailableTableStatus;
            }

            await _context.SaveChangesAsync();
        }

        public async Task FillTablesAsync(ReservationCreateViewModel vm)
        {
            var tables = await _context.Tables
                .AsNoTracking()
                .OrderBy(t => t.TableNumber)
                .ToListAsync();

            vm.Tables = tables.Select(t => new SelectOptionViewModel
            {
                Value = t.TableId.ToString(),
                Text = $"Маса {t.TableNumber} ({t.CapacityOfTheTable} места, {t.StatusOfTheTable})",
                Capacity = t.CapacityOfTheTable
            }).ToList();
        }

        private IQueryable<Reservation> BuildReservationQuery()
        {
            return _context.Reservations
                .AsNoTracking()
                .Include(r => r.Table)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Employee).ThenInclude(e => e!.User);
        }

        private static ReservationIndexViewModel ToReservationIndexViewModel(Reservation r)
        {
            return new ReservationIndexViewModel
            {
                ReservationId = r.ReservationId,
                TableNumber = r.Table.TableNumber,
                CapacityOfTheTable = r.Table.CapacityOfTheTable,
                Status = r.Status,
                ReservationDate = r.ReservationDate,
                NumberOfGuests = r.NumberOfGuests,
                StatusOfTheTable = r.Table.StatusOfTheTable,
                CustomerName = r.Customer.User.UserName ?? r.Customer.User.Email,
                EmployeeName = r.Employee != null ? r.Employee.User.UserName ?? r.Employee.User.Email : null
            };
        }

        private static void ValidateReservationInput(ReservationCreateViewModel vm)
        {
            if (vm.NumberOfGuests <= 0)
            {
                throw new ArgumentException("Моля, въведи валиден брой гости.");
            }

            if (vm.ReservationDate < DateTime.Now)
            {
                throw new ArgumentException("Не може да резервирате за минало време.");
            }

            if (vm.ReservationDate.Hour < 8)
            {
                throw new ArgumentException("Ресторантът отваря в 08:00 ч.");
            }

            if (vm.ReservationDate.Hour > 21 || (vm.ReservationDate.Hour == 21 && vm.ReservationDate.Minute > 0))
            {
                throw new ArgumentException("Резервацията е за 3 часа. Най-късният час за резервация е 21:00 ч.");
            }
        }

        private async Task<Table?> FindAvailableTableAsync(ReservationCreateViewModel vm)
        {
            var reservationStart = vm.ReservationDate;
            var reservationEnd = vm.ReservationDate.AddHours(3);

            var tableQuery = _context.Tables
                .Where(t => t.CapacityOfTheTable >= vm.NumberOfGuests);

            if (vm.TableId != Guid.Empty)
            {
                tableQuery = tableQuery.Where(t => t.TableId == vm.TableId);
            }

            var potentialTables = await tableQuery
                .OrderBy(t => t.CapacityOfTheTable)
                .ThenBy(t => t.TableNumber)
                .ToListAsync();

            foreach (var table in potentialTables)
            {
                var isOccupied = await HasOverlappingActiveReservationAsync(
                    table.TableId,
                    reservationStart,
                    reservationEnd);

                if (!isOccupied)
                {
                    return table;
                }
            }

            return null;
        }

        private async Task<bool> HasOverlappingActiveReservationAsync(Guid tableId, DateTime reservationStart, DateTime reservationEnd)
        {
            return await _context.Reservations.AnyAsync(r =>
                r.TableId == tableId &&
                r.Status != RejectedStatus &&
                reservationStart < r.ReservationDate.AddHours(3) &&
                reservationEnd > r.ReservationDate);
        }

        private async Task<string> BuildUnavailableReservationMessageAsync(ReservationCreateViewModel vm)
        {
            var reservationStart = vm.ReservationDate;
            var reservationEnd = vm.ReservationDate.AddHours(3);

            if (vm.TableId != Guid.Empty)
            {
                var selectedTable = await _context.Tables
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.TableId == vm.TableId);

                if (selectedTable == null)
                {
                    return "Избраната маса не съществува.";
                }

                if (selectedTable.CapacityOfTheTable < vm.NumberOfGuests)
                {
                    return $"Маса {selectedTable.TableNumber} е за {selectedTable.CapacityOfTheTable} гости. Избери по-голяма маса за {vm.NumberOfGuests} гости.";
                }

                var conflict = await FindOverlappingActiveReservationAsync(selectedTable.TableId, reservationStart, reservationEnd);

                if (conflict != null)
                {
                    return $"Маса {selectedTable.TableNumber} вече е резервирана от {conflict.ReservationDate:HH:mm} до {conflict.ReservationDate.AddHours(3):HH:mm}. Избери час след {conflict.ReservationDate.AddHours(3):HH:mm} или друга маса.";
                }
            }

            var hasTableWithEnoughCapacity = await _context.Tables
                .AnyAsync(t => t.CapacityOfTheTable >= vm.NumberOfGuests);

            if (!hasTableWithEnoughCapacity)
            {
                return $"Няма маса с достатъчен капацитет за {vm.NumberOfGuests} гости.";
            }

            var nearestConflict = await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Table)
                .Where(r =>
                    r.Status != RejectedStatus &&
                    r.Table.CapacityOfTheTable >= vm.NumberOfGuests &&
                    reservationStart < r.ReservationDate.AddHours(3) &&
                    reservationEnd > r.ReservationDate)
                .OrderBy(r => r.ReservationDate)
                .FirstOrDefaultAsync();

            if (nearestConflict != null)
            {
                return $"Няма свободна подходяща маса в диапазона {reservationStart:HH:mm}-{reservationEnd:HH:mm}. Например маса {nearestConflict.Table.TableNumber} е заета от {nearestConflict.ReservationDate:HH:mm} до {nearestConflict.ReservationDate.AddHours(3):HH:mm}.";
            }

            return "Няма свободна маса за избраното време и брой гости.";
        }

        private async Task<Reservation?> FindOverlappingActiveReservationAsync(Guid tableId, DateTime reservationStart, DateTime reservationEnd)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Table)
                .Where(r =>
                    r.TableId == tableId &&
                    r.Status != RejectedStatus &&
                    reservationStart < r.ReservationDate.AddHours(3) &&
                    reservationEnd > r.ReservationDate)
                .OrderBy(r => r.ReservationDate)
                .FirstOrDefaultAsync();
        }

        private async Task<bool> HasOverlappingApprovedReservationAsync(Guid tableId, DateTime reservationStart, DateTime reservationEnd, Guid ignoredReservationId)
        {
            return await _context.Reservations.AnyAsync(r =>
                r.ReservationId != ignoredReservationId &&
                r.TableId == tableId &&
                r.Status == ApprovedStatus &&
                reservationStart < r.ReservationDate.AddHours(3) &&
                reservationEnd > r.ReservationDate);
        }

        private async Task<bool> HasFutureApprovedReservationForTableAsync(Guid tableId, Guid ignoredReservationId)
        {
            return await _context.Reservations.AnyAsync(r =>
                r.ReservationId != ignoredReservationId &&
                r.TableId == tableId &&
                r.Status == ApprovedStatus &&
                r.ReservationDate >= DateTime.Now);
        }

        private async Task EnsureCustomerProfileAsync(Guid userId)
        {
            var customerProfileExists = await _context.Set<CustomerProfile>()
                .AnyAsync(cp => cp.UserId == userId);

            if (!customerProfileExists)
            {
                _context.Add(new CustomerProfile { UserId = userId });
                await _context.SaveChangesAsync();
            }
        }

        private async Task EnsureEmployeeProfileAsync(Guid employeeId)
        {
            var employeeProfileExists = await _context.Set<EmployeeProfile>()
                .AnyAsync(ep => ep.UserId == employeeId);

            if (!employeeProfileExists)
            {
                _context.Add(new EmployeeProfile { UserId = employeeId });
                await _context.SaveChangesAsync();
            }
        }
    }
}
