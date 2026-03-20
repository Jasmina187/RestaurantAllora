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
        private readonly RestaurantAlloraProjectContext _context;
        public TableService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TableViewModel>> GetAllAsync()
        {
            return await _context.Tables
                .OrderBy(t => t.TableNumber)
                .Select(t => new TableViewModel
                {
                    TableId = t.TableId,
                    TableNumber = t.TableNumber,
                    CapacityOfTheTable = t.CapacityOfTheTable,
                    StatusOfTheTable = t.StatusOfTheTable
                })
                .ToListAsync();
        }
        public async Task<TableViewModel?> GetByIdAsync(Guid id)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.TableId == id);
            if (table == null) return null;
            return new TableViewModel
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                CapacityOfTheTable = table.CapacityOfTheTable,
                StatusOfTheTable = table.StatusOfTheTable
            };
        }
        public async Task CreateAsync(TableViewModel vm)
        {
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
            table.StatusOfTheTable = vm.StatusOfTheTable;
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
    }
}
