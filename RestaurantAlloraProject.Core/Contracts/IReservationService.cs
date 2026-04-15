using RestaurantAlloraProjectViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface IReservationService
    {
        Task<List<ReservationIndexViewModel>> GetUserReservationsAsync(Guid userId);
        Task<List<ReservationIndexViewModel>> GetPendingReservationsAsync();
        Task<ReservationManagementViewModel> GetReservationsForManagementAsync(ReservationFilterViewModel filter);
        Task CreateReservationAsync(ReservationCreateViewModel vm, Guid userId);
        Task ApproveReservationAsync(Guid id, Guid employeeId);
        Task RejectReservationAsync(Guid id, Guid employeeId);
        Task FillTablesAsync(ReservationCreateViewModel vm);
    }
}
