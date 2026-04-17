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
            var gluten = new Guid("dc08b4ec-5095-4811-a672-192301357e16");
            var crustaceans = new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823");
            var eggs = new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977");
            var fish = new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528");
            var soy = new Guid("503510df-d3a4-4266-a182-3b3db962de57");
            var milk = new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2");
            var nuts = new Guid("247f192a-3e44-480a-bde9-98089f8b398b");
            var celery = new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f");
            var mustard = new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99");
            var sesame = new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a");
            var sulfites = new Guid("64d4fbb0-ffe7-4526-9d18-300608276013");
            var molluscs = new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e");

            var geisha = new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec");
            var istanbul = new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58");
            var fatush = new Guid("44ce4f8a-6e15-4947-bd6b-e20bdb14d842");
            var energy = new Guid("05267740-181b-43e4-96dd-e5b9c250ac75");
            var turkeySteak = new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89");
            var blackAngusMeatballs = new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57");
            var whiteFishItameshi = new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef");
            var eggNoodles = new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b");
            var padKaprao = new Guid("f4201dbf-1adb-4949-b235-d137db7698f7");
            var pistachioCheesecake = new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c");

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

            Add(geisha, gluten);
            Add(geisha, crustaceans);
            Add(geisha, eggs);
            Add(geisha, fish);
            Add(geisha, soy);
            Add(geisha, mustard);
            Add(geisha, sesame);
            Add(geisha, sulfites);

            Add(istanbul, soy);
            Add(istanbul, milk);
            Add(istanbul, nuts);

            Add(fatush, gluten);

            Add(energy, soy);
            Add(energy, sesame);

            Add(turkeySteak, gluten);
            Add(turkeySteak, soy);
            Add(turkeySteak, celery);

            Add(blackAngusMeatballs, soy);
            Add(blackAngusMeatballs, milk);
            Add(blackAngusMeatballs, mustard);

            Add(whiteFishItameshi, gluten);
            Add(whiteFishItameshi, eggs);
            Add(whiteFishItameshi, fish);
            Add(whiteFishItameshi, milk);

            Add(eggNoodles, gluten);
            Add(eggNoodles, eggs);
            Add(eggNoodles, soy);
            Add(eggNoodles, milk);
            Add(eggNoodles, celery);
            Add(eggNoodles, molluscs);

            Add(padKaprao, gluten);
            Add(padKaprao, eggs);
            Add(padKaprao, soy);
            Add(padKaprao, milk);
            Add(padKaprao, celery);
            Add(padKaprao, sesame);
            Add(padKaprao, sulfites);
            Add(padKaprao, molluscs);

            Add(pistachioCheesecake, gluten);
            Add(pistachioCheesecake, eggs);
            Add(pistachioCheesecake, milk);
            Add(pistachioCheesecake, nuts);

            return list;
        }
    }
}
