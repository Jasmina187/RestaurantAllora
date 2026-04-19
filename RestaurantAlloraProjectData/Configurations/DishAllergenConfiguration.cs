using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData.Configurations
{
    public class DishAllergenConfiguration : IEntityTypeConfiguration<DishAllergen>
    {
        public void Configure(EntityTypeBuilder<DishAllergen> builder)
        {
            builder
                .HasKey(x => new { x.DishId, x.AllergenId });

            builder
                .HasOne(x => x.Dish)
                .WithMany(d => d.DishAllergens)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Allergen)
                .WithMany(a => a.DishAllergens)
                .HasForeignKey(x => x.AllergenId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
