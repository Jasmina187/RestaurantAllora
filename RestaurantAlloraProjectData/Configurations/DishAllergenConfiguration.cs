using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Configurations
{
    public class DishAllergenConfiguration: IEntityTypeConfiguration<DishAllergen>
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
            builder.HasData(CreateDishAllergens());
        }
        public List<DishAllergen> CreateDishAllergens()
        {
            var list = new List<DishAllergen>();

            void Add(Guid dishId, Guid allergenId)
            {
                if (!list.Any(x => x.DishId == dishId && x.AllergenId == allergenId))
                {
                    list.Add(new DishAllergen
                    {
                        DishId = dishId,
                        AllergenId = allergenId
                    });
                }
            }
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("dc08b4ec-5095-4811-a672-192301357e16"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("503510df-d3a4-4266-a182-3b3db962de57"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"));
            Add(new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"));
          


            return list;
        }
    }
}
