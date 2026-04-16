using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(o => o.Status)
                .HasDefaultValue("Обработва се");

            builder
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            builder
                .Property(o => o.FulfillmentType)
                .HasMaxLength(30)
                .HasDefaultValue("Вземане на място");

            builder
                .Property(o => o.CustomerFullName)
                .HasMaxLength(100)
                .HasDefaultValue("");

            builder
                .Property(o => o.CustomerPhone)
                .HasMaxLength(30)
                .HasDefaultValue("");

            builder
                .Property(o => o.DeliveryAddress)
                .HasMaxLength(300);

            builder
                .Property(o => o.Notes)
                .HasMaxLength(500);
        }
    }
}
