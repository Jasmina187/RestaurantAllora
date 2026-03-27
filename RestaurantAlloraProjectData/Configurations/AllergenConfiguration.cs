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
                        AllergenName = "Зърнени култури, съдържащи глутен: пшеница, ръж, ечемик, овес, спелта, камут, както и продукти от тях"
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
                        AllergenId = new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"),
                        AllergenName = "Риба и рибни продукти"
                    },
                    new Allergen
                    {
                        AllergenId = Guid.Parse("fe83dc00-a553-4041-8e2e-aa7c9eb5a0ed"),
                        AllergenName = "Фъстъци и продукти от тях"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("503510df-d3a4-4266-a182-3b3db962de57"),
                       AllergenName = "Соя и соеви продукти"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("a846b85f-53b1-4b2a-b096-825824c3b7e2"),
                       AllergenName = "Мляко и млечни продукти"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("247f192a-3e44-480a-bde9-98089f8b398b"),
                       AllergenName = "Ядки – бадеми, орехи, лешници, кашу и др."
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("3a8c6114-15c6-4f6c-9de1-21126a38706f"),
                       AllergenName = "Целина и продукти от нея"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"),
                       AllergenName = "Синап и продукти от него"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("7f4554e9-9835-479e-bb37-b97ed9c58d6a"),
                       AllergenName = "Сусамово семе и продукти от него"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("6ab84643-d4db-4789-bf18-f43afe7e4a38"),
                       AllergenName = "Лупина и продукти от нея"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("64d4fbb0-ffe7-4526-9d18-300608276013"),
                       AllergenName = "Серен диоксид и сулфиди"
                    },
                    new Allergen
                    {
                       AllergenId = Guid.Parse("6b990dac-6b4f-49f4-a715-ccaa67246c3e"),
                       AllergenName = "Мекотели и продукти от тях"
                    }
};
            return allergens;
        }
    }
}
