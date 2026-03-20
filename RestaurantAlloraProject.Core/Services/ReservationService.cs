using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly RestaurantAlloraProjectContext _context;
        public ReservationService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task<List<ReservationIndexViewModel>> GetUserReservationsAsync(Guid userId)
        {
            return await _context.Reservations
                .Include(r => r.Table)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Employee).ThenInclude(e => e.User)
                .Where(r => r.CustomerId == userId)
                .OrderByDescending(r => r.ReservationDate)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.ReservationId,
                    TableNumber = r.Table.TableNumber,
                    CapacityOfTheTable = r.Table.CapacityOfTheTable,
                    Status = r.Status,
                    ReservationDate = r.ReservationDate,
                    NumberOfGuests = r.NumberOfGuests,
                    StatusOfTheTable = r.Table.StatusOfTheTable,
                    CustomerName = r.Customer.User.UserName,
                    EmployeeName = r.Employee != null ? r.Employee.User.UserName : null
                })
                .ToListAsync();
        }
        public async Task<List<ReservationIndexViewModel>> GetPendingReservationsAsync()
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Table)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Employee).ThenInclude(e => e.User)
                .Where(r => r.Status == "Очаква одобрение")
                .OrderBy(r => r.ReservationDate)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.ReservationId,
                    TableNumber = r.Table.TableNumber,
                    CapacityOfTheTable = r.Table.CapacityOfTheTable,
                    Status = r.Status,
                    ReservationDate = r.ReservationDate,
                    NumberOfGuests = r.NumberOfGuests,
                    StatusOfTheTable = r.Table.StatusOfTheTable,
                    CustomerName = r.Customer.User.UserName,
                    EmployeeName = r.Employee != null ? r.Employee.User.UserName : null
                })
                .ToListAsync();
        }
        public async Task CreateReservationAsync(ReservationCreateViewModel vm, Guid userId)
        {
         
            if (vm.NumberOfGuests <= 0)
            {
                throw new ArgumentException("Моля, въведи валиден брой гости.");
            }
            if (vm.ReservationDate < DateTime.Now)
            {
                throw new ArgumentException("Не може да резервирате за минало време.");
            }
            var reservationStart = vm.ReservationDate;
            var reservationEnd = vm.ReservationDate.AddHours(3);
            if (reservationStart.Hour < 8)
            {
                throw new ArgumentException("Ресторантът отваря в 08:00 ч.");
            }

            else if (reservationStart.Hour > 21 || (reservationStart.Hour == 21 && reservationStart.Minute > 0))
            {
                throw new ArgumentException("Резервацията е за 3 часа и надхвърля часа на затваряне (00:00 ч.). Най-късният час за резервация е 21:00 ч.");
            }
                var potentialTables = await _context.Tables
                .Where(t => t.CapacityOfTheTable >= vm.NumberOfGuests)
                .ToListAsync();
            Table? availableTable = null;
            foreach (var table in potentialTables)
            {
                var isOccupied = await _context.Reservations
                    .AnyAsync(r => r.TableId == table.TableId &&
                                   r.Status != "Отказана" &&
                                   reservationStart < r.ReservationDate.AddHours(3) &&
                                   reservationEnd > r.ReservationDate);
                if (!isOccupied)
                {
                    availableTable = table;
                    break;
                }
            }

            if (availableTable == null)
            {
                throw new ArgumentException("За съжаление няма свободна маса за избраното време и брой гости.");
            }
            var customerProfileExists = await _context.Set<CustomerProfile>()
                .AnyAsync(cp => cp.UserId == userId);
            if (!customerProfileExists)
            {
                var newCustomerProfile = new CustomerProfile { UserId = userId };
                _context.Add(newCustomerProfile);
                await _context.SaveChangesAsync();
            }   
            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                TableId = availableTable.TableId,
                CustomerId = userId,
                ReservationDate = vm.ReservationDate,
                NumberOfGuests = vm.NumberOfGuests,
                Status = "Очаква одобрение"
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task ApproveReservationAsync(Guid id, Guid employeeId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if (reservation != null)
            {
                reservation.Status = "Одобрена";
                reservation.EmployeeId = employeeId;
                await _context.SaveChangesAsync();
            }
        }
        public async Task RejectReservationAsync(Guid id, Guid employeeId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if (reservation != null)
            {
                reservation.Status = "Отказана";
                reservation.EmployeeId = employeeId;
                await _context.SaveChangesAsync();
            }
        }
        public async Task FillTablesAsync(ReservationCreateViewModel vm)
        {
            var tables = await _context.Tables
                .AsNoTracking()
                .OrderBy(t => t.TableNumber)
                .ToListAsync();

            vm.Tables = tables.Select(t => new SelectListItem
            {
                Value = t.TableId.ToString(),
                Text = $"Маса {t.TableNumber} ({t.CapacityOfTheTable} места)"
            }).ToList();
        }
    }
}
