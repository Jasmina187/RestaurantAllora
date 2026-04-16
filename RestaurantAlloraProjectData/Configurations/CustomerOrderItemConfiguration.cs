using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData.Configurations
{
    public class CustomerOrderItemConfiguration : IEntityTypeConfiguration<CustomerOrderItem>
    {
        public void Configure(EntityTypeBuilder<CustomerOrderItem> builder)
        {
            builder
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            builder
                .HasOne(oi => oi.Order)
                .WithMany(o => o.CustomerOrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(oi => oi.Dish)
                .WithMany(d => d.OrderItems)
                .HasForeignKey(oi => oi.DishId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(oi => oi.Employee)
                .WithMany(e => e.HandledOrders)
                .HasForeignKey(oi => oi.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
