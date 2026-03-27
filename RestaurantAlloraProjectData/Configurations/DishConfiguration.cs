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
        //new Dish
        //{
        //    DishId = new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89"),
        //    NameOfTheDish = "ПУЕШКИ СТЕК НА BBQ",
        //    DescriptionOfTheDish = "Пуешки стек на BBQ със специална марината, запечени тиквички, гъби, моркови и карфиол с билкова марината.",
        //    PriceOfTheDish = 9.97m,
        //    CategoryOfTheDish = "Основни ястия",
        //    ImageUrl = "/img/stek.png"
        //},
        //new Dish
        //{
        //    DishId = new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57"),
        //    NameOfTheDish = "ТЕЛЕШКИ КЮФТЕТА БЛЕК АНГЪС",
        //    DescriptionOfTheDish = "Кюфтета от кълцано телешко Блек Ангъс, пържени картофи и микс салати с чери домати и дресинг винегрет.",
        //    PriceOfTheDish = 9.20m,
        //    CategoryOfTheDish = "Основни ястия",
        //    ImageUrl = "/img/meatballs.png"
        //},
        //new Dish
        //{
        //    DishId = new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef"),
        //    NameOfTheDish = "ФИЛЕ БЯЛА РИБА ИТАМЕШИ",
        //    DescriptionOfTheDish = "Филе бяла риба и спагети в Алфредо сос със спанак и азиатски подправки, пармезан, пресен пипер и див лук.",
        //    PriceOfTheDish = 9.71m,
        //    CategoryOfTheDish = "Основни ястия",
        //    ImageUrl = "/img/fish.png"
        //},
        
        //new Dish
        //{
        //    DishId = new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"),
        //    NameOfTheDish = "ЯЙЧНИ НУДЛИ С ПИЛЕ И ЗЕЛЕНЧУЦИ",
        //    DescriptionOfTheDish = "Традиционни китайски нудли с мариновано пилешко месо от бут, яйце, зеле, моркови, пресен зелен и червен пипер, специален сос и зелен лук.",
        //    PriceOfTheDish = 8.69m,
        //    CategoryOfTheDish = "Основни ястия",
        //    ImageUrl = "/img/noodles.png"
        //},
        //new Dish
        //{
        //    DishId = new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"),
        //    NameOfTheDish = "ПИЛЕ ПАД КАПРАО",
        //    DescriptionOfTheDish = "Пилешки късчета с леко пикантен азиатски сос и подправки, яйце, пресен босилек и магданоз върху жасминов ориз.",
        //    PriceOfTheDish = 9.20m,
        //    CategoryOfTheDish = "Основни ястия",
        //    ImageUrl = "/img/chicken.png"
        //},
        //new Dish
        //{
        //    DishId = new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"),
        //    NameOfTheDish = "ПИЛЕ ПАД КАПРАО",
        //    DescriptionOfTheDish = "Пилешки късчета с леко пикантен азиатски сос и подправки, яйце, пресен босилек и магданоз върху жасминов ориз.",
        //    PriceOfTheDish = 9.20m,
        //    CategoryOfTheDish = "Основни ястия",
        //    ImageUrl = "/img/chicken.png"
        //},
        //new Dish
        //{
        //    DishId = new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c"),
        //    NameOfTheDish = "ПИСТАЧИО ЧИЙЗКЕЙК",
        //    DescriptionOfTheDish = "Чийзкейк с хрупкав блат, маскарпоне крем и пистачио глазура.",
        //    PriceOfTheDish = 5.62m,
        //    CategoryOfTheDish = "Десерти",
        //    ImageUrl = "/img/pistachio.png"
        //},

            };

            return dishes;

        }
    }
}
