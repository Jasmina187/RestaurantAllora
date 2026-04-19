namespace RestaurantAlloraProjectViewModels.Dashboard
{
    public class AdminDashboardViewModel
    {
        public List<DashboardMetricViewModel> Metrics { get; set; } = new();

        public List<DashboardStatusCountViewModel> OrderStatuses { get; set; } = new();

        public List<DashboardRecentOrderViewModel> RecentOrders { get; set; } = new();

        public List<DashboardUpcomingReservationViewModel> UpcomingReservations { get; set; } = new();

        public List<DashboardPopularDishViewModel> PopularDishes { get; set; } = new();
    }

    public class DashboardMetricViewModel
    {
        public string Label { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string Hint { get; set; } = string.Empty;

        public string Accent { get; set; } = "gold";
    }

    public class DashboardStatusCountViewModel
    {
        public string Status { get; set; } = string.Empty;

        public int Count { get; set; }
    }

    public class DashboardRecentOrderViewModel
    {
        public string ShortId { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
    }

    public class DashboardUpcomingReservationViewModel
    {
        public string CustomerName { get; set; } = string.Empty;

        public DateTime ReservationDate { get; set; }

        public int TableNumber { get; set; }

        public int NumberOfGuests { get; set; }

        public string Status { get; set; } = string.Empty;
    }

    public class DashboardPopularDishViewModel
    {
        public string DishName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Revenue { get; set; }
    }
}
