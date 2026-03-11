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
                AllergenId = Guid.Parse("dc08b4ec-5095-4811-a672-192301357e16"),
                AllergenName = "Зърнени култури, съдържащи глутен"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("42ccb79f-216b-4aab-9655-944d4f7b9823"),
                AllergenName = "Ракообразни и продукти от тях"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("c4399ef7-4776-4b45-92fb-35ae3dd3f977"),
                AllergenName = "Яйца и продукти от тях"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("c8f84b5a-4cd4-405b-adb4-2eeb69942cfc"),
                AllergenName = "Риба и рибни продукти"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("d72de7c5-8e83-4d4c-a8e2-c5cdcf8da39e"),
                AllergenName = "Фъстъци и продукти от тях"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("4cab969c-d612-4a00-9d29-3aedc8d90f3b"),
                AllergenName = "Соя и соеви продукти"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("be7c8688-70de-4742-a524-d3d8e99616d8"),
                AllergenName = "Мляко и млечни продукти"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("32aabc2d-ef09-4d9f-b85f-973062edf531"),
                AllergenName = "Ядки – бадеми, орехи, лешници, кашу и др."
            },
            new Allergen
            {
                AllergenId = Guid.Parse("859bc440-5fd3-4a13-b827-48a2540ffc6c"),
                AllergenName = "Целина и продукти от нея"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("2138d67d-b112-4d75-8340-4130c6d49f6f"),
                AllergenName = "Синап и продукти от него"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("4eed56a2-0d6e-4c82-811f-d84066553249"),
                AllergenName = "Сусамово семе и продукти от него"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("8459d499-ec62-49a3-b93c-fdd2a75c940b"),
                AllergenName = "Лупина и продукти от нея"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("063e346a-e671-4710-835b-8f6132ee7744"),
                AllergenName = "Серен диоксид и сулфиди"
            },
            new Allergen
            {
                AllergenId = Guid.Parse("945ea094-2772-4113-80f9-4b9c8fc5188d"),
                AllergenName = "Мекотели и продукти от тях"
            });
        }
    }
}
