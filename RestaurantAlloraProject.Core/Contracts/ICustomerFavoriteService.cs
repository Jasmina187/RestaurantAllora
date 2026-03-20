using RestaurantAlloraProject.ViewModels.CustomerFavorite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface ICustomerFavoriteService
    {
        Task<IEnumerable<CustomerFavoriteViewModel>> GetFavoritesAsync(Guid userId);
        Task ToggleFavoriteAsync(Guid dishId, Guid userId);
    }
}
