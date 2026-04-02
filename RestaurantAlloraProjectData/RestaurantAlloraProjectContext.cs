using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData.Configurations;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData
{
    public class RestaurantAlloraProjectContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
      
        public RestaurantAlloraProjectContext(DbContextOptions<RestaurantAlloraProjectContext> options) : base(options)
        {
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<DishAllergen> DishAllergens { get; set; }
        public DbSet<CustomerFavorite> CustomerFavorites { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<CustomerOrderItem> CustomerOrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }


        public async Task FindAsync(Guid tableId)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AllergenConfiguration());
            builder.ApplyConfiguration(new DishAllergenConfiguration());
            builder.ApplyConfiguration(new DishConfiguration());
            builder.ApplyConfiguration(new TableConfiguration());

            builder.Entity<CustomerProfile>()
         .HasOne(cp => cp.User)
         .WithOne(u => u.CustomerProfile)
         .HasForeignKey<CustomerProfile>(cp => cp.UserId)
         .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EmployeeProfile>()
                .HasOne(ep => ep.User)
                .WithOne(u => u.EmployeeProfile)
                .HasForeignKey<EmployeeProfile>(ep => ep.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomerFavorite>()
                .HasKey(x => new { x.CustomerId, x.DishId });

            builder.Entity<CustomerFavorite>()
                .HasOne(x => x.Customer)
                .WithMany(c => c.Favorites)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomerFavorite>()
                .HasOne(x => x.Dish)
                .WithMany(d => d.FavoritedBy)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasOne(r => r.Dish)
                .WithMany(d => d.Reviews)
                .HasForeignKey(r => r.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Review>()
                .HasIndex(r => new { r.CustomerId, r.DishId })
                .IsUnique();

        
            builder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Reservation>()
                .HasOne(r => r.Employee)
                .WithMany(e => e.HandledReservations)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>()
                .Property(o => o.Status)
                .HasDefaultValue("Обработва се");   
            builder.Entity<CustomerOrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.CustomerOrderItems)
                .HasForeignKey(oi => oi.OrderId) 
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CustomerOrderItem>()
                .HasOne(oi => oi.Dish)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.DishId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CustomerOrderItem>()
                .HasOne(oi => oi.Employee)
                .WithMany(e => e.HandledOrders)
                .HasForeignKey(oi => oi.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Table>()
                .HasIndex(t => t.TableNumber)
                .IsUnique();

            builder.Entity<Table>()
                .Property(t => t.StatusOfTheTable)
                .HasDefaultValue("Свободна");

            builder.Entity<Reservation>()
                .Property(r => r.Status)
                .HasDefaultValue("Очаква одобрение");
        }
    }   
}
