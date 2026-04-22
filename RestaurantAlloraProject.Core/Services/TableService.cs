using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class TableService : ITableService
    {
        private const int MinTableNumber = 1;
        private const int MaxTableNumber = 500;
        private const int MinTableCapacity = 1;
        private const int MaxTableCapacity = 10;

        private static readonly string[] ApprovedReservationStatuses = { "Одобрена", "РћРґРѕР±СЂРµРЅР°" };
        private static readonly string[] PendingReservationStatuses = { "Очаква одобрение", "РћС‡Р°РєРІР° РѕРґРѕР±СЂРµРЅРёРµ" };

        private readonly RestaurantAlloraProjectContext _context;
        public TableService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TableViewModel>> GetAllAsync()
        {
            var now = DateTime.Now;

            var tables = await _context.Tables
                .Include(t => t.Reservations)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();

            return tables.Select(t =>
            {
                var activeReservationStarts = t.Reservations
                    .Where(r => ApprovedReservationStatuses.Contains(r.Status) && r.ReservationDate.AddHours(3) > now)
                    .OrderBy(r => r.ReservationDate)
                    .Select(r => r.ReservationDate)
                    .ToList();

                var pendingReservationStarts = t.Reservations
                    .Where(r => PendingReservationStatuses.Contains(r.Status) && r.ReservationDate.AddHours(3) > now)
                    .OrderBy(r => r.ReservationDate)
                    .Select(r => r.ReservationDate)
                    .ToList();

                return new TableViewModel
                {
                    TableId = t.TableId,
                    TableNumber = t.TableNumber,
                    CapacityOfTheTable = t.CapacityOfTheTable,
                    StatusOfTheTable = activeReservationStarts.Any()
                        ? "Резервирана"
                        : pendingReservationStarts.Any() ? "Очаква одобрение" : "Свободна",
                    NextReservationStart = activeReservationStarts.Select(r => (DateTime?)r).FirstOrDefault(),
                    ActiveReservationStarts = activeReservationStarts,
                    PendingReservationStarts = pendingReservationStarts
                };
            });
        }
        public async Task<TableViewModel?> GetByIdAsync(Guid id)
        {
            var table = await _context.Tables
                .Include(t => t.Reservations)
                .FirstOrDefaultAsync(t => t.TableId == id);
            if (table == null) return null;

            var activeReservationStarts = table.Reservations
                .Where(r => ApprovedReservationStatuses.Contains(r.Status) && r.ReservationDate.AddHours(3) > DateTime.Now)
                .OrderBy(r => r.ReservationDate)
                .Select(r => r.ReservationDate)
                .ToList();

            var pendingReservationStarts = table.Reservations
                .Where(r => PendingReservationStatuses.Contains(r.Status) && r.ReservationDate.AddHours(3) > DateTime.Now)
                .OrderBy(r => r.ReservationDate)
                .Select(r => r.ReservationDate)
                .ToList();

            return new TableViewModel
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                CapacityOfTheTable = table.CapacityOfTheTable,
                StatusOfTheTable = activeReservationStarts.Any()
                    ? "Резервирана"
                    : pendingReservationStarts.Any() ? "Очаква одобрение" : "Свободна",
                NextReservationStart = activeReservationStarts.Select(r => (DateTime?)r).FirstOrDefault(),
                ActiveReservationStarts = activeReservationStarts,
                PendingReservationStarts = pendingReservationStarts
            };
        }
        public async Task CreateAsync(TableViewModel vm)
        {
            ValidateTable(vm);

            bool tableExists = await _context.Tables.AnyAsync(t => t.TableNumber == vm.TableNumber);
            if (tableExists)
            {
                throw new ArgumentException($"Маса с номер {vm.TableNumber} вече съществува.");
            }
            var table = new Table
            {
                TableId = Guid.NewGuid(),
                TableNumber = vm.TableNumber,
                CapacityOfTheTable = vm.CapacityOfTheTable,
                StatusOfTheTable = string.IsNullOrWhiteSpace(vm.StatusOfTheTable) ? "Свободна" : vm.StatusOfTheTable
            };
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TableViewModel vm)
        {
            ValidateTable(vm);
            
            bool tableExists = await _context.Tables.AnyAsync(t => t.TableNumber == vm.TableNumber && t.TableId != vm.TableId);
            if (tableExists)
            {
                throw new ArgumentException($"Маса с номер {vm.TableNumber} вече съществува.");
            }
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.TableId == vm.TableId);
            if (table == null)
            {
                throw new InvalidOperationException("Масата не е намерена.");
            }
            table.TableNumber = vm.TableNumber;
            table.CapacityOfTheTable = vm.CapacityOfTheTable;
            table.StatusOfTheTable = string.IsNullOrWhiteSpace(vm.StatusOfTheTable) ? "Свободна" : vm.StatusOfTheTable;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }

        private static void ValidateTable(TableViewModel vm)
        {
            if (vm.TableNumber < MinTableNumber || vm.TableNumber > MaxTableNumber)
            {
                throw new ArgumentException($"Номерът на масата трябва да бъде между {MinTableNumber} и {MaxTableNumber}.");
            }

            if (vm.CapacityOfTheTable < MinTableCapacity || vm.CapacityOfTheTable > MaxTableCapacity)
            {
                throw new ArgumentException($"Капацитетът на масата трябва да бъде между {MinTableCapacity} и {MaxTableCapacity} човека.");
            }
        }
    }
}
