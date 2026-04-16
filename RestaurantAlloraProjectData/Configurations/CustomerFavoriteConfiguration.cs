using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData.Configurations
{
    public class CustomerFavoriteConfiguration : IEntityTypeConfiguration<CustomerFavorite>
    {
        public void Configure(EntityTypeBuilder<CustomerFavorite> builder)
        {
            builder.HasKey(x => new { x.CustomerId, x.DishId });

            builder
                .HasOne(x => x.Customer)
                .WithMany(c => c.Favorites)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Dish)
                .WithMany(d => d.FavoritedBy)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
