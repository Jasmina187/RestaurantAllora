using RestaurantAlloraProjectViewModels.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface ITableService
    {
        Task<IEnumerable<TableViewModel>> GetAllAsync();
        Task<TableViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(TableViewModel vm);
        Task UpdateAsync(TableViewModel vm);
        Task DeleteAsync(Guid id);
    }
}

