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
            Add(new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58"), new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"));
            Add(new Guid("44ce4f8a-6e15-4947-bd6b-e20bdb14d842"), new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"));
            Add(new Guid("05267740-181b-43e4-96dd-e5b9c250ac75"), new Guid("c8f84b5a-4cd4-405b-adb4-2eeb69942cfc"));

            return list;
        }
    }
}
