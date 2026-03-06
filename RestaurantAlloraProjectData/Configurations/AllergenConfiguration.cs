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
            builder.HasData(
                new Allergen
                {
                    AllergenId = Guid.Parse("a1f1c2d3-1111-4b11-8111-111111111111"),
                    AllergenName = "Зърнени култури, съдържащи глутен"
                },
            new Allergen
            {
                AllergenId = Guid.Parse("b2f2c3d4-2222-4c22-8222-222222222222"),
                AllergenName = "Ракообразни и продукти от тях"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("c3f3c4d5-3333-4d33-8333-333333333333"),
                AllergenName = "Яйца и продукти от тях"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("d4f4c5d6-4444-4e44-8444-444444444444"),
                AllergenName = "Риба и рибни продукти"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("e5f5c6d7-5555-4f55-8555-555555555555"),
                AllergenName = "Фъстъци и продукти от тях"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("f6f6c7d8-6666-4066-8666-666666666666"),
                AllergenName = "Соя и соеви продукти"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("a7a7c8d9-7777-4177-8777-777777777777"),
                AllergenName = "Мляко и млечни продукти"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("b8b8c9e1-8888-4288-8888-888888888888"),
                AllergenName = "Ядки – бадеми, орехи, лешници, кашу и др."
            },
            new Allergen
            {
                AllergenId = Guid.Parse("c9c9d1e2-9999-4399-8999-999999999999"),
                AllergenName = "Целина и продукти от нея"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("d1d1e2f3-aaaa-44aa-8aaa-aaaaaaaaaaaa"),
                AllergenName = "Синап и продукти от него"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("e2e2f3a4-bbbb-45bb-8bbb-bbbbbbbbbbbb"),
                AllergenName = "Сусамово семе и продукти от него"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("f3f3a4b5-cccc-46cc-8ccc-cccccccccccc"),
                AllergenName = "Лупина и продукти от нея"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("a4a4b5c6-dddd-47dd-8ddd-dddddddddddd"),
                AllergenName = "Серен диоксид и сулфиди"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("b5b5c6d7-eeee-48ee-8eee-eeeeeeeeeeee"),
                AllergenName = "Мекотели и продукти от тях"
            }
        );

        }
    }
}
