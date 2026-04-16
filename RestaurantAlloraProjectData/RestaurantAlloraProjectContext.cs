using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData
{
    public class RestaurantAlloraProjectContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public RestaurantAlloraProjectContext(DbContextOptions<RestaurantAlloraProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<DishAllergen> DishAllergens { get; set; }
        public DbSet<CustomerFavorite> CustomerFavorites { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(RestaurantAlloraProjectContext).Assembly);
        }
    }
}
