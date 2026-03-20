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
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {

        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasData(Dishes());
        }
        public List<Dish> Dishes()
        {
            List<Dish> dishes = new List<Dish>()
            {
                new Dish
        {
            DishId = new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"),
            NameOfTheDish = "САЛАТА ГЕЙША",
            DescriptionOfTheDish = "Мариновани скариди, авокадо, шарена киноа, микс салати със спанак, червен лук, чери домати, японски дресинг с мисо паста и див лук.",
            PriceOfTheDish = 17.30m,
            CategoryOfTheDish = "Салати",
            ImageUrl = "/img/geisha.png"
        },
        new Dish
        {
            DishId = new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58"),
            NameOfTheDish = "ИСТАНБУЛ",
            DescriptionOfTheDish = "Козе сирене, микс салати, свежи соеви кълнове, чери домати, ябълка, карамелизирани орехи, стафиди, сушени боровинки и нар.",
            PriceOfTheDish = 8.69m,
            CategoryOfTheDish = "Салати",
            ImageUrl = "/img/instanbul.png"
        },
        new Dish
        {
            DishId = new Guid("44ce4f8a-6e15-4947-bd6b-e20bdb14d842"),
            NameOfTheDish = "ФАТУШ",
            DescriptionOfTheDish = "Салата Романа, микс салати, чери домати, краставици, репички, пресен червен пипер, лук, магданоз, босилек, свеж зеленчуков дресинг, хрупкав хляб, нар и дресинг нар.",
            PriceOfTheDish = 7.49m,
            CategoryOfTheDish = "Салати",
            ImageUrl = "/img/fatush.png"
        },
        new Dish
        {
            DishId = new Guid("05267740-181b-43e4-96dd-e5b9c250ac75"),
            NameOfTheDish = "САЛАТА ЕНЕРДЖИ",
            DescriptionOfTheDish = "Черна леща Белуга, шарена киноа, нахут, краставици, чери домати, печен маринован пипер, маслини, магданоз, червен лук, дресинг Винегрет, мисо-лайм хумус и сумак.",
            PriceOfTheDish = 7.49m,
            CategoryOfTheDish = "Салати",
            ImageUrl = "/img/fatush.png"
        }
            };
            return dishes;
        }
      
    }
}
