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
        public class AllergenConfiguration : IEntityTypeConfiguration<Allergen>
        {
            public void Configure(EntityTypeBuilder<Allergen> builder)
            {

                builder.HasData(Allergens());
            }
            public List<Allergen> Allergens()
            {
                List<Allergen> allergens = new List<Allergen>()
                {

                    new Allergen
                    {
                        AllergenId = new Guid("dc08b4ec-5095-4811-a672-192301357e16"),
                        AllergenName = "Зърнени култури, съдържащи глутен"
                    },
                    new Allergen
                    {
                        AllergenId = new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"),
                        AllergenName = "Ракообразни и продукти от тях"
                    },
                    new Allergen
                    {
                        AllergenId = Guid.Parse("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), 
                        AllergenName = "Яйца и продукти от тях"
                    },
                    new Allergen
                    {
                        AllergenId = new Guid("c8f84b5a-4cd4-405b-adb4-2eeb69942cfc"),
                        AllergenName = "Риба и рибни продукти"
                    }                    
                };
                return allergens;
            }
        }
}
