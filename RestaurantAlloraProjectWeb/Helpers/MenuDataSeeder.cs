using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectWeb.Helpers
{
    public static class MenuDataSeeder
    {
        private const int SeedDishesPerCategory = 3;
        private const string Gluten = "gluten";
        private const string Crustaceans = "crustaceans";
        private const string Eggs = "eggs";
        private const string Fish = "fish";
        private const string Soy = "soy";
        private const string Milk = "milk";
        private const string Nuts = "nuts";
        private const string Celery = "celery";
        private const string Mustard = "mustard";
        private const string Sesame = "sesame";
        private const string Sulfites = "sulfites";

        private static readonly Dictionary<string, Guid> AllergenIds = new()
        {
            [Gluten] = new Guid("dc08b4ec-5095-4811-a672-192301357e16"),
            [Crustaceans] = new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"),
            [Eggs] = new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"),
            [Fish] = new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"),
            [Soy] = new Guid("503510df-d3a4-4266-a182-3b3db962de57"),
            [Milk] = new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"),
            [Nuts] = new Guid("247f192a-3e44-480a-bde9-98089f8b398b"),
            [Celery] = new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f"),
            [Mustard] = new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"),
            [Sesame] = new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"),
            [Sulfites] = new Guid("64d4fbb0-ffe7-4526-9d18-300608276013")
        };

        private static readonly string[] LegacySeedDishNames =
        {
            "САЛАТА ГЕЙША",
            "ИСТАНБУЛ",
            "ФАТУШ",
            "САЛАТА ЕНЕРДЖИ",
            "ПУЕШКИ СТЕК НА BBQ",
            "ТЕЛЕШКИ КЮФТЕТА БЛЕК АНГЪС",
            "ФИЛЕ БЯЛА РИБА ИТАМЕШИ",
            "ЯЙЧНИ НУДЛИ С ПИЛЕ И ЗЕЛЕНЧУЦИ",
            "ПИЛЕ ПАД КАПРАО",
            "ПИСТАЧИО ЧИЙЗКЕЙК"
        };

        private static readonly string[] LegacySeedCategoryNames =
        {
            "Салати",
            "Основни ястия",
            "Десерти",
            "Напитки"
        };

        private static readonly string[] HappyCategoryNames =
        {
            "Френско меню",
            "Златна есен",
            "Всички артикули",
            "Най-поръчвани",
            "Салати",
            "Предястия",
            "Основни",
            "Морски",
            "Вегетариански",
            "Детско",
            "Протеиново меню",
            "Gaming combo",
            "Бургери",
            "Бургери и сандвичи",
            "Суши",
            "Нови предложения",
            "Суши комбо",
            "Чикън",
            "Калифорния",
            "Вулкани и хосомаки",
            "Филаделфия",
            "Футомаки",
            "Нигири, сашими и татаки",
            "Поке бол",
            "Сосове за суши",
            "Пица",
            "Пици",
            "Паста",
            "Паста и ризото",
            "Нудли и ориз",
            "Десерти",
            "Десерти и напитки",
            "Комбо оферти",
            "Азиатско меню",
            "Азиатски предложения",
            "Оферти",
            "Средиземноморска кухня",
            "Bbq",
            "Веган ястия",
            "Гарнитури",
            "Домашен хляб",
            "Морски дарове",
            "Предястия, хайвери, разядки",
            "Рибни ястия",
            "Салатите на капитана",
            "Сашими, татаки, тартари и мариновани риби",
            "Сосове",
            "Спагети и ризото"
        };

        private static readonly SeedDish[] HappyDishes =
        {
            new("ПРОЛЕТНА ЗЕЛЕНА САЛАТА", "Салати", 5.69m, "https://dostavka.happy.bg/remote/files/images/718811/fit_600_376.png?rev=1714317364",
                "Свежи зелени салати с хрупкави зеленчуци, лек дресинг и пролетен аромат.", Array.Empty<string>()),
            new("ФРЕНСКА ПРОЛЕТ", "Салати", 8.68m, "https://dostavka.happy.bg/remote/files/images/5037689/fit_600_376.png?rev=1774873924",
                "Френски салатен микс с нежно сирене, свежи плодови акценти и балансиран дресинг.", new[] { Milk, Nuts, Sulfites }),
            new("НИЦА САЛАД БОЛ", "Салати", 7.66m, "https://dostavka.happy.bg/remote/files/images/4453303/fit_600_376.png?rev=1768203232",
                "Средиземноморска салата с яйце, риба тон, зеленчуци и маслини.", new[] { Eggs, Fish, Mustard, Sulfites }),
            new("ЗАПЕЧЕНО КОЗЕ СИРЕНЕ И ЯГОДИ", "Салати", 9.71m, "https://dostavka.happy.bg/remote/files/images/407610/fit_600_376.png?rev=1767967435",
                "Запечено козе сирене върху свежи салати с ягоди, ядки и сладко-кисел дресинг.", new[] { Milk, Nuts, Sulfites }),
            new("МАК & ЧИЙЗ ПУКАНКИ", "Предястия", 6.13m, "https://dostavka.happy.bg/remote/files/images/5037929/fit_600_376.png?rev=1774874883",
                "Хрупкави хапки с кремообразен чийз пълнеж и златиста панировка.", new[] { Gluten, Eggs, Milk }),
            new("ДУО ФРЕНСКИ РАЗЯДКИ", "Предястия", 6.13m, "https://dostavka.happy.bg/remote/files/images/4959433/fit_600_376.png?rev=1773909518",
                "Две фини разядки с млечна мекота, подправки и ароматен хляб за споделяне.", new[] { Gluten, Milk, Sesame }),
            new("ФРЕНСКИ ПАЛАЧИНКИ ОТ СИРЕНА", "Предястия", 7.15m, "https://dostavka.happy.bg/remote/files/images/4468716/fit_600_376.png?rev=1770280059",
                "Топли солени палачинки със сирена, лека коричка и нежен млечен вкус.", new[] { Gluten, Eggs, Milk }),
            new("ХРУПКАВИ СИРЕНЦА БРИ", "Предястия", 7.15m, "https://dostavka.happy.bg/remote/files/images/4468884/fit_600_376.png?rev=1768203398",
                "Панирано бри с деликатна коричка и мек разтапящ се център.", new[] { Gluten, Eggs, Milk }),
            new("ПИЛЕ ФРЕНЧИ ЧИЙЗИ ХОЛАНДЕЗ", "Основни", 9.45m, "https://dostavka.happy.bg/remote/files/images/4474085/fit_600_376.png?rev=1773909599",
                "Крехко пилешко със сирена, холандез сос и богата кремообразна текстура.", new[] { Eggs, Milk, Mustard }),
            new("ПИЛЕ ФРЕНЧИ ШАМПИНЬОН", "Основни", 9.45m, "https://dostavka.happy.bg/remote/files/images/4794162/fit_600_376.png?rev=1772010368",
                "Сочно пилешко с шампиньони, фин сос и ароматни билки.", new[] { Milk, Mustard, Sulfites }),
            new("ПИЛЕ ФРЕНЧИ ДИЖОН", "Основни", 9.45m, "https://dostavka.happy.bg/remote/files/images/4472405/fit_600_376.png?rev=1768204183",
                "Пилешко филе с дижонски сос, лека пикантност и маслен завършек.", new[] { Milk, Mustard }),
            new("ПИЛЕ ФРЕНЧИ ЕЛЕГАНС", "Основни", 9.45m, "https://dostavka.happy.bg/remote/files/images/4473245/fit_600_376.png?rev=1768204351",
                "Пилешко с крем сос, зеленчуци и изчистен френски характер.", new[] { Milk, Mustard, Sulfites }),
            new("СКАРИДИ ПРОВАНС", "Морски", 10.22m, "https://dostavka.happy.bg/remote/files/images/4469052/fit_600_376.png?rev=1768203426",
                "Скариди с провансалски билки, чеснов аромат и свеж морски вкус.", new[] { Crustaceans, Milk, Sulfites }),
            new("СКАРИДИ НА ПЛОЧА", "Морски", 10.22m, "https://dostavka.happy.bg/remote/files/images/238/fit_600_376.png?rev=1648497933",
                "Скариди на плоча с леко опушен вкус, лимон и маслена глазура.", new[] { Crustaceans, Milk }),
            new("ХРУПКАВИ ПИКАНТНИ СКАРИДИ", "Морски", 10.22m, "https://dostavka.happy.bg/remote/files/images/11093/fit_600_376.png?rev=1733843173",
                "Пикантни скариди в хрупкава панировка с апетитен сос.", new[] { Gluten, Crustaceans, Eggs, Soy }),
            new("ДЕТСКО МЕНЮ С КАРТОФЕНИ ПУКАНКИ", "Детско", 4.99m, "https://dostavka.happy.bg/remote/files/images/168100/fit_600_376.png?rev=1686231830",
                "Детско меню с картофени хапки, мек вкус и весела порция.", new[] { Gluten, Milk }),
            new("ДЕТСКО МЕНЮ С ХРУПКАВИ ПИЛЕШКИ СТРИПСОВЕ", "Детско", 5.99m, "https://dostavka.happy.bg/remote/files/images/417706/fit_600_376.png?rev=1709050316",
                "Хрупкави пилешки стрипсове с гарнитура, подходящи за малки гости.", new[] { Gluten, Eggs, Milk }),
            new("ДЕТСКО МЕНЮ С ПИЛЕШКИ ПРЪЧИЦИ", "Детско", 5.99m, "https://dostavka.happy.bg/remote/files/images/7565/fit_600_376.png?rev=0",
                "Пилешки пръчици със златиста коричка и балансирана детска порция.", new[] { Gluten, Eggs, Milk }),
            new("ПОСТНО КОМБО 24 БР", "Суши комбо", 8.68m, "https://dostavka.happy.bg/remote/files/images/4058587/fit_600_376.png?rev=1763128108",
                "Постно суши комбо с ориз, зеленчуци, нори и свежи азиатски вкусове.", new[] { Soy, Sesame }),
            new("АЙФЕЛОВАТА КУЛА 14 БР", "Суши комбо", 12.26m, "https://dostavka.happy.bg/remote/files/images/4600367/fit_600_376.png?rev=1770029450",
                "Селекция суши с фини ролки, крем сирене и морски акценти.", new[] { Fish, Soy, Milk, Sesame }),
            new("КОМБО ЛУВЪР 20 БР", "Суши комбо", 15.33m, "https://dostavka.happy.bg/remote/files/images/4600535/fit_600_376.png?rev=1770029468",
                "Богато суши комбо с разнообразни ролки, сосове и свеж завършек.", new[] { Fish, Soy, Milk, Sesame }),
            new("КАЛИФОРНИЯ ГРИЙН 4 БР", "Калифорния", 3.57m, "https://dostavka.happy.bg/remote/files/images/4040531/fit_600_376.png?rev=1762957568",
                "Зеленчукова калифорния с ориз, нори, авокадо и лек сусамов вкус.", new[] { Soy, Sesame }),
            new("КАЛИФОРНИЯ РЕД СКАРИДА ТЕМПУРА 4 БР", "Калифорния", 4.03m, "https://dostavka.happy.bg/remote/files/images/32632/fit_600_376.png?rev=1660664935",
                "Калифорния рол със скарида темпура, свежи зеленчуци и пикантен завършек.", new[] { Gluten, Crustaceans, Eggs, Soy, Sesame }),
            new("МАНГО КАЛИФОРНИЯ 4 БР", "Калифорния", 4.54m, "https://dostavka.happy.bg/remote/files/images/470098/fit_600_376.png?rev=1710100152",
                "Калифорния рол с манго, авокадо и леко сладък тропически баланс.", new[] { Fish, Soy, Sesame }),
            new("ЕЛЕГАНС 4 БР", "Филаделфия", 3.57m, "https://dostavka.happy.bg/remote/files/images/4601219/fit_600_376.png?rev=1770029584",
                "Филаделфия рол с крем сирене, ориз и деликатен морски вкус.", new[] { Fish, Soy, Milk, Sesame }),
            new("САЛМОН БИЖУ 4 БР", "Филаделфия", 4.59m, "https://dostavka.happy.bg/remote/files/images/4793034/fit_600_376.png?rev=1772004565",
                "Рол със сьомга, крем сирене и свежа мека текстура.", new[] { Fish, Soy, Milk, Sesame }),
            new("САЛМОН ЧИЙЗ 4 БР", "Филаделфия", 5.11m, "https://dostavka.happy.bg/remote/files/images/4601591/fit_600_376.png?rev=1770029620",
                "Сьомга, крем сирене и ориз в класическа филаделфия комбинация.", new[] { Fish, Soy, Milk, Sesame }),
            new("ПРОТЕИНОВА ПИЦА МАРГАРИТА СПЕЦИАЛЕ", "Пици", 8.18m, "https://dostavka.happy.bg/remote/files/images/2319509/fit_600_376.png?rev=1739782125",
                "Протеинова пица с доматен сос, моцарела и свеж босилек.", new[] { Gluten, Milk }),
            new("ПРОТЕИНОВА ПИЦА ЧИКЕНИТА", "Пици", 8.94m, "https://dostavka.happy.bg/remote/files/images/2318981/fit_600_376.png?rev=1739782083",
                "Протеинова пица с пилешко, моцарела и леко пикантен сос.", new[] { Gluten, Milk, Mustard }),
            new("ПРОТЕИНОВА ПИЦА СЮПРИЙМ", "Пици", 8.94m, "https://dostavka.happy.bg/remote/files/images/2690763/fit_600_376.png?rev=1744098946",
                "Пица с богата плънка, разтопено сирене и ароматна доматена основа.", new[] { Gluten, Milk, Sulfites }),
            new("ПРОТЕИНОВА ПИЦА БЛЕК АНГЪС", "Пици", 9.20m, "https://dostavka.happy.bg/remote/files/images/2319149/fit_600_376.png?rev=1739782100",
                "Протеинова пица с Блек Ангъс телешко, сирене и плътен BBQ характер.", new[] { Gluten, Milk, Mustard, Soy }),
            new("ПРОТЕИНОВА ПИЦА ПРО ЕНЕРДЖИ", "Пици", 9.20m, "https://dostavka.happy.bg/remote/files/images/2690595/fit_600_376.png?rev=1744098937",
                "Пица с протеинова основа, зеленчуци, сирена и енергичен вкус.", new[] { Gluten, Milk, Soy }),
            new("ПРОТЕИНОВА ПИЦА ФИТ-ПИЛЕ", "Пици", 9.45m, "https://dostavka.happy.bg/remote/files/images/2690427/fit_600_376.png?rev=1765378776",
                "Фит пица с пилешко, зеленчуци и разтопена моцарела.", new[] { Gluten, Milk }),
            new("Вентричина", "Пици", 8.49m, "https://dostavka.happy.bg/remote/files/images/17496/fit_600_376.png?rev=1773929166",
                "Италианска пица с пикантна вентричина, моцарела и доматен сос.", new[] { Gluten, Milk, Sulfites }),
            new("Маргарита", "Пици", 8.18m, "https://dostavka.happy.bg/remote/files/images/428556/fit_600_376.png?rev=1771233941",
                "Класическа пица с доматен сос, моцарела и босилек.", new[] { Gluten, Milk }),
            new("БУРАТА С ПРОШУТО КРУДО", "Пица", 11.24m, "https://dostavka.happy.bg/remote/files/images/3062787/fit_600_376.png?rev=1749392510",
                "Пица с бурата, прошуто крудо, доматен сос и ароматна италианска коричка.", new[] { Gluten, Milk, Sulfites }),
            new("ЯЙЧНИ НУДЛИ СЪС ЗЕЛЕНЧУЦИ", "Нудли и ориз", 6.64m, "https://dostavka.happy.bg/remote/files/images/3695692/fit_600_376.png?rev=1758658170",
                "Яйчни нудли със зеленчуци, соев сос и лек азиатски аромат.", new[] { Gluten, Eggs, Soy, Celery }),
            new("ЯЙЧНИ НУДЛИ С ПИЛЕ И ЗЕЛЕНЧУЦИ", "Нудли и ориз", 8.69m, "https://dostavka.happy.bg/remote/files/images/3695860/fit_600_376.png?rev=1758658194",
                "Яйчни нудли с пилешко, зеленчуци, соев сос и пресен зелен лук.", new[] { Gluten, Eggs, Soy, Celery }),
            new("ЯЙЧНИ НУДЛИ С ТЕЛЕШКО БЛЕК АНГЪС И СКАРИДИ", "Нудли и ориз", 10.22m, "https://dostavka.happy.bg/remote/files/images/3696028/fit_600_376.png?rev=1758658216",
                "Нудли с Блек Ангъс телешко, скариди, зеленчуци и плътен азиатски сос.", new[] { Gluten, Crustaceans, Eggs, Soy, Celery }),
            new("ОРИЗ СЪС ЗЕЛЕНЧУЦИ", "Нудли и ориз", 6.64m, "https://dostavka.happy.bg/remote/files/images/3695188/fit_600_376.png?rev=1758658010",
                "Пържен ориз със зеленчуци, соев сос и свежи подправки.", new[] { Soy, Celery, Sesame }),
            new("ФРЕНЧИ ПОКЕ БОЛ", "Френско меню", 11.75m, "https://dostavka.happy.bg/remote/files/images/4629076/fit_600_376.png?rev=1770026599",
                "Богат поке бол с френски акцент, свежи зеленчуци, ароматен сос и балансирана плътност.", new[] { Fish, Soy, Sesame, Mustard }),
            new("ПРОТЕИНОВ БОЛ С БЛЕК АНГЪС КЮФТЕНЦА", "Протеиново меню", 9.71m, "https://dostavka.happy.bg/remote/files/images/5038601/fit_600_376.png?rev=1774875447",
                "Засищащ протеинов бол с телешки кюфтенца Блек Ангъс, зеленчуци и лек сос.", new[] { Gluten, Eggs, Soy, Mustard }),
            new("ПРОТЕИНОВ ЧИКЪН ПОКЕ БОЛ", "Протеиново меню", 9.45m, "https://dostavka.happy.bg/remote/files/images/2576840/fit_600_376.png?rev=1743409024",
                "Пилешки протеинов бол с ориз, свежи зеленчуци и чиста пикантна нотка.", new[] { Soy, Sesame, Mustard }),
            new("ТРЮФЕЛ СЬОМГА ПОКЕ БОЛ", "Поке бол", 11.75m, "https://dostavka.happy.bg/remote/files/images/4033883/fit_600_376.png?rev=1762957201",
                "Поке бол със сьомга, трюфелов крем, ориз и свежи зеленчуци за мек морски вкус.", new[] { Fish, Soy, Milk, Sesame }),
            new("ПОКЕ БОЛ СЪС СКАРИДИ", "Поке бол", 11.24m, "https://dostavka.happy.bg/remote/files/images/1740960/fit_600_376.png?rev=1736323464",
                "Поке бол със скариди, зеленчуци, ориз и лек азиатски сос.", new[] { Crustaceans, Soy, Sesame }),
            new("GAMING COMBO КРИСПИ С COCA-COLA", "Gaming combo", 11.24m, "https://dostavka.happy.bg/remote/files/images/1054831/fit_600_376.png?rev=1758266887",
                "Комбо с хрупкави пилешки хапки, гарнитура и Coca-Cola за споделяне пред екрана.", new[] { Gluten, Eggs, Milk }),
            new("СПАНАК С КИНОА", "Най-поръчвани", 7.66m, "https://dostavka.happy.bg/remote/files/images/190/fit_600_376.png?rev=0",
                "Любима салата със спанак, киноа, свежи зеленчуци и лек дресинг.", new[] { Mustard, Sulfites }),
            new("СЕЙНТ БАРТ", "Бургери и сандвичи", 9.20m, "https://dostavka.happy.bg/remote/files/images/5038097/fit_600_376.png?rev=1774874991",
                "Сочен бургер с богата плънка, разтопено сирене, сос и хрупкава гарнитура.", new[] { Gluten, Eggs, Milk, Mustard, Sesame }),
            new("ФРЕНЧИ БУРГЕР", "Бургери", 7.66m, "https://dostavka.happy.bg/remote/files/images/4607332/fit_600_376.png?rev=1769774672",
                "Бургер с френски характер, меко хлебче, сочно месо и кремообразен сос.", new[] { Gluten, Eggs, Milk, Mustard, Sesame }),
            new("ФИЛАДЕЛФИЯ КОМБО 20 БР", "Суши", 16.35m, "https://dostavka.happy.bg/remote/files/images/4631121/fit_600_376.png?rev=1770103070",
                "Комбо селекция с филаделфия ролки, сьомга, крем сирене и балансирани сосове.", new[] { Fish, Soy, Milk, Sesame }),
            new("ЧИКЪН РАКЛЕТ 4 БР", "Нови предложения", 4.08m, "https://dostavka.happy.bg/remote/files/images/4793202/fit_600_376.png?rev=1772004596",
                "Нов рол с пилешко, раклет, ориз и мек млечен завършек.", new[] { Gluten, Soy, Milk, Sesame }),
            new("ПИЛЕ РОКФОР 4 БР", "Чикън", 4.59m, "https://dostavka.happy.bg/remote/files/images/4601051/fit_600_376.png?rev=1770029544",
                "Пилешки суши рол с рокфор нотка, ориз и свежи зеленчуци.", new[] { Gluten, Soy, Milk, Sesame }),
            new("ТЕРИЯКИ ВЕЗУВИЙ 8 БР", "Вулкани и хосомаки", 5.11m, "https://dostavka.happy.bg/remote/files/images/3704694/fit_600_376.png?rev=1758718936",
                "Топъл рол Везувий с терияки сос, ориз и изразен азиатски аромат.", new[] { Gluten, Soy, Sesame }),
            new("ФУТОМАКИ САКУРА 6 БР", "Футомаки", 6.64m, "https://dostavka.happy.bg/remote/files/images/4631289/fit_600_376.png?rev=1770103368",
                "Футомаки с плътна плънка, ориз, нори и свеж балансиран вкус.", new[] { Fish, Soy, Sesame }),
            new("ТАКОС ТАРТАР СЪС СЬОМГА", "Нигири, сашими и татаки", 7.15m, "https://dostavka.happy.bg/remote/files/images/4039523/fit_600_376.png?rev=1762957302",
                "Хрупкав такос с тартар от сьомга, свежи подправки и деликатен сос.", new[] { Fish, Soy, Sesame }),
            new("СОЕВО-ЧЕСНОВА МАЙОНЕЗА", "Сосове за суши", 1.02m, "https://dostavka.happy.bg/remote/files/images/31277/fit_600_376.png?rev=1653548426",
                "Кремообразен сос със соев и чеснов аромат за суши и хрупкави хапки.", new[] { Eggs, Soy }),
            new("ПИЦА МАРГАРИТА КОМБО", "Комбо оферти", 8.69m, "https://dostavka.happy.bg/remote/files/images/1597029/fit_600_376.png?rev=1730188690",
                "Комбо с класическа Маргарита, напитка и десертен акцент.", new[] { Gluten, Milk, Sulfites }),
            new("ЕНЕРДЖИ", "Азиатско меню", 7.49m, "https://dostavka.happy.bg/remote/files/images/4914105/fit_600_376.png?rev=1773305723",
                "Свежа азиатска салата с хрупкави зеленчуци, лек сос и енергичен вкус.", new[] { Soy, Sesame, Mustard }),
            new("ГЕЙША", "Азиатски предложения", 8.69m, "https://dostavka.happy.bg/remote/files/images/3931688/fit_600_376.png?rev=1761568664",
                "Азиатско предложение с нежна текстура, зеленчуци и ароматен сос.", new[] { Soy, Sesame, Sulfites }),
            new("ФЕШЪН БУРАТА", "Вегетариански", 9.99m, "https://dostavka.happy.bg/remote/files/images/5094689/fit_600_376.png?rev=1775560473",
                "Свежо вегетарианско ястие с бурата, домати, зелени акценти и фин дресинг.", new[] { Milk, Nuts, Sulfites }),
            new("ПИСТАЧИО ЧИЙЗКЕЙК", "Десерти", 5.69m, "https://dostavka.happy.bg/remote/files/images/4897788/fit_600_376.png?rev=1773153746",
                "Кремообразен чийзкейк с шамфъстък, нежна основа и свеж завършек.", new[] { Gluten, Eggs, Milk, Nuts }),
            new("ФРЕШ ЯГОДА", "Десерти и напитки", 3.42m, "https://dostavka.happy.bg/remote/files/images/11618/fit_600_376.png?rev=1774439269",
                "Свежа ягодова напитка с плодова плътност и лек летен аромат.", Array.Empty<string>()),
            new("ОРИЗ С ПИЛЕ И ЗЕЛЕНЧУЦИ", "Паста и ризото", 8.18m, "https://dostavka.happy.bg/remote/files/images/3695356/fit_600_376.png?rev=1758658068",
                "Пържен ориз с пилешко, зеленчуци, соев сос и ароматни подправки.", new[] { Soy, Celery, Sesame }),
            new("КЕЙЛ С АВОКАДО И НАР", "Средиземноморска кухня", 13.49m, "https://dostavka.happy.bg/remote/files/images/64476/fit_600_376.png?rev=1666546927",
                "Средиземноморска салата с кейл, авокадо, нар и лек цитрусов дресинг.", new[] { Mustard, Sulfites }),
            new("ОРИЗ СЪС СПАНАК НА ФУРНА", "Френско меню", 5.11m, "https://dostavka.happy.bg/remote/files/images/150221/fit_600_376.png?rev=1771836836",
                "Запечен ориз със спанак, нежна текстура и лек френски домашен характер.", new[] { Milk, Celery }),
            new("ПАЛАЧИНКИ ОТ СИРЕНА ФРЕНСКО МЕНЮ", "Френско меню", 7.15m, "https://dostavka.happy.bg/remote/files/images/4468716/fit_600_376.png?rev=1770280059",
                "Солени палачинки със сирена, мека плънка и златиста коричка.", new[] { Gluten, Eggs, Milk }),
            new("GAMING COMBO С ПИЛЕШКИ ПРЪЧИЦИ И COCA-COLA", "Gaming combo", 9.20m, "https://dostavka.happy.bg/remote/files/images/248673/fit_600_376.png?rev=1758205845",
                "Комбо с пилешки пръчици, гарнитура и Coca-Cola за дълга вечер.", new[] { Gluten, Eggs, Milk }),
            new("GAMING COMBO ЧИКЪН С COCA-COLA", "Gaming combo", 11.75m, "https://dostavka.happy.bg/remote/files/images/2165320/fit_600_376.png?rev=1758266780",
                "Гейминг комбо с пилешко, хрупкава гарнитура и студена Coca-Cola.", new[] { Gluten, Eggs, Milk }),
            new("АЗИАТСКИ ЦЕЗАР С ХРУПКАВО ПИЛЕ И АВОКАДО", "Азиатски предложения", 8.69m, "https://dostavka.happy.bg/remote/files/images/3796093/fit_600_376.png?rev=1759904167",
                "Цезар салата с хрупкаво пиле, авокадо и азиатски дресинг.", new[] { Gluten, Eggs, Fish, Milk, Mustard }),
            new("МИСО-ЛАЙМ ХУМУС", "Азиатски предложения", 5.29m, "https://dostavka.happy.bg/remote/files/images/4915269/fit_600_376.png?rev=1773306053",
                "Кремообразен хумус с мисо, лайм и свеж азиатски аромат.", new[] { Soy, Sesame }),
            new("ЗЕЛЕНЧУЦИ С БАМБУК И ГЪБИ", "Азиатско меню", 6.99m, "https://dostavka.happy.bg/remote/files/images/4917900/fit_600_376.png?rev=1773317182",
                "Зеленчуци с бамбук, гъби и лек соев сос за чист азиатски вкус.", new[] { Soy, Celery, Sesame }),
            new("ЕДАМАМЕ", "Азиатско меню", 3.06m, "https://dostavka.happy.bg/remote/files/images/3694288/fit_600_376.png?rev=1758657656",
                "Класическо едамаме с морска сол и свеж зелен вкус.", new[] { Soy }),
            new("БЛЕК АНГЪС БУРГЕР", "Бургери", 9.20m, "https://dostavka.happy.bg/remote/files/images/3157642/fit_600_376.png?rev=1750850715",
                "Сочен бургер с Блек Ангъс, сирене, свежи зеленчуци и плътен сос.", new[] { Gluten, Eggs, Milk, Mustard, Sesame }),
            new("ЕКСТРА ЧИЙЗБУРГЕР", "Бургери", 9.20m, "https://dostavka.happy.bg/remote/files/images/3156034/fit_600_376.png?rev=1750671395",
                "Чийзбургер с щедро сирене, сочно месо и класически сос.", new[] { Gluten, Eggs, Milk, Mustard, Sesame }),
            new("КОМБО СЕЙНТ БАРТ", "Бургери и сандвичи", 10.73m, "https://dostavka.happy.bg/remote/files/images/5038265/fit_600_376.png?rev=1774875128",
                "Комбо със Сейнт Барт бургер, гарнитура и напитка.", new[] { Gluten, Eggs, Milk, Mustard, Sesame }),
            new("КЕСАДИЯ С ПЕЧЕНО ПИЛЕ И ФИЛАДЕЛФИЯ", "Бургери и сандвичи", 7.92m, "https://dostavka.happy.bg/remote/files/images/3154750/fit_600_376.png?rev=1750666302",
                "Топла кесадия с печено пиле, крем сирене и ароматна коричка.", new[] { Gluten, Milk }),
            new("ИТАЛИАНА С БЕЙБИ МОЦАРЕЛА", "Вегетариански", 8.69m, "https://dostavka.happy.bg/remote/files/images/5094917/fit_600_376.png?rev=1775551975",
                "Свежа салата с бейби моцарела, домати, зелени листа и фин дресинг.", new[] { Milk, Sulfites }),
            new("НИКОЛЕТА", "Вегетариански", 8.18m, "https://dostavka.happy.bg/remote/files/images/233017/fit_600_376.png?rev=1698911765",
                "Вегетарианска салата с богати зеленчуци, сирене и балансиран дресинг.", new[] { Milk, Nuts, Sulfites }),
            new("ХОСОМАКИ АВОКАДО ФРЕШ 8 БР", "Вулкани и хосомаки", 3.57m, "https://dostavka.happy.bg/remote/files/images/1722162/fit_600_376.png?rev=1731591293",
                "Хосомаки с авокадо, ориз и нори в свежа лека порция.", new[] { Soy, Sesame }),
            new("ВЕЗУВИЙ 8 БР", "Вулкани и хосомаки", 5.97m, "https://dostavka.happy.bg/remote/files/images/20404/fit_600_376.png?rev=1697196233",
                "Топъл Везувий рол с богат сос, ориз и пикантен завършек.", new[] { Gluten, Fish, Soy, Sesame }),
            new("ЛОТУС ЧИЙЗКЕЙК", "Десерти", 4.59m, "https://dostavka.happy.bg/remote/files/images/4897404/fit_600_376.png?rev=1774853720",
                "Нежен чийзкейк с лотус крем, бисквитена основа и карамелен вкус.", new[] { Gluten, Eggs, Milk }),
            new("ПАВЛОВА С МАНГО И ЯГОДИ", "Десерти", 4.39m, "https://dostavka.happy.bg/remote/files/images/4898472/fit_600_376.png?rev=1773153315",
                "Лек десерт с целувчена основа, манго, ягоди и крем.", new[] { Eggs, Milk }),
            new("МИНИ ЕКЛЕРИ С ШОКОЛАД", "Десерти и напитки", 3.89m, "https://dostavka.happy.bg/remote/files/images/4999933/fit_600_376.png?rev=1774427006",
                "Мини еклери с шоколадов крем и мека сладка текстура.", new[] { Gluten, Eggs, Milk }),
            new("ЯГОДОВ ВУЛКАН", "Десерти и напитки", 4.85m, "https://dostavka.happy.bg/remote/files/images/4998421/fit_600_376.png?rev=1774426425",
                "Топъл ягодов десерт с кремообразен център и плодов аромат.", new[] { Gluten, Eggs, Milk }),
            new("ПИЦА ПЕПЕРОНИ КОМБО", "Комбо оферти", 9.20m, "https://dostavka.happy.bg/remote/files/images/1597365/fit_600_376.png?rev=1730188598",
                "Комбо с пеперони пица, напитка и подходящ завършек.", new[] { Gluten, Milk, Sulfites }),
            new("ПИЦА БЛЕК АНГЪС БУРГЕР КОМБО", "Комбо оферти", 9.71m, "https://dostavka.happy.bg/remote/files/images/1596861/fit_600_376.png?rev=1730188619",
                "Комбо с пица Блек Ангъс бургер, гарнитура и плътен вкус.", new[] { Gluten, Eggs, Milk, Mustard }),
            new("ТАБИЕТЛИЙСКА САЛАТА", "Най-поръчвани", 8.43m, "https://dostavka.happy.bg/remote/files/images/2176427/fit_600_376.png?rev=1737579424",
                "Популярна салата с богати зеленчуци, сирене и класически ресторантски вкус.", new[] { Milk, Sulfites }),
            new("ЦЕЗАР С ХРУПКАВО ПИЛЕ", "Най-поръчвани", 9.20m, "https://dostavka.happy.bg/remote/files/images/2178813/fit_600_376.png?rev=1737620767",
                "Цезар салата с хрупкаво пиле, пармезан, крутони и крем дресинг.", new[] { Gluten, Eggs, Fish, Milk, Mustard }),
            new("ТАКОС ТАРТАР СЪС СКАРИДИ", "Нигири, сашими и татаки", 6.64m, "https://dostavka.happy.bg/remote/files/images/4039691/fit_600_376.png?rev=1762957307",
                "Хрупкав такос с тартар от скариди, свежи подправки и азиатски сос.", new[] { Crustaceans, Soy, Sesame }),
            new("СЬОМГА ТАТАКИ С ТРЮФЕЛ КРЕМ", "Нигири, сашими и татаки", 8.68m, "https://dostavka.happy.bg/remote/files/images/4041203/fit_600_376.png?rev=1762957752",
                "Сьомга татаки с трюфелов крем, свежа текстура и фин морски вкус.", new[] { Fish, Soy, Milk, Sesame }),
            new("КРИСПИ РАКЛЕТ 4 БР", "Нови предложения", 4.08m, "https://dostavka.happy.bg/remote/files/images/4609360/fit_600_376.png?rev=1770029644",
                "Нов рол с хрупкава текстура, раклет и мек млечен завършек.", new[] { Gluten, Soy, Milk, Sesame }),
            new("СКАРИДА АМАРАНТ 4 БР", "Нови предложения", 4.08m, "https://dostavka.happy.bg/remote/files/images/4600883/fit_600_376.png?rev=1770029527",
                "Свеж рол със скарида, амарант и лек азиатски сос.", new[] { Crustaceans, Soy, Sesame }),
            new("ФИЛАДЕЛФИЯ ПОКЕ БОЛ", "Поке бол", 10.22m, "https://dostavka.happy.bg/remote/files/images/2452110/fit_600_376.png?rev=1741095754",
                "Поке бол с филаделфия стил, ориз, сьомга и крем сирене.", new[] { Fish, Soy, Milk, Sesame }),
            new("ПРОТЕИНОВ БЛЕК АНГЪС БОЛ", "Протеиново меню", 9.96m, "https://dostavka.happy.bg/remote/files/images/2676713/fit_600_376.png?rev=1743869127",
                "Протеинов бол с Блек Ангъс, зеленчуци и засищащ сос.", new[] { Eggs, Soy, Mustard }),
            new("ШРИРАЧА ЧИЛИ МАЙОНЕЗА", "Сосове за суши", 0.86m, "https://dostavka.happy.bg/remote/files/images/11629/fit_600_376.png?rev=1653904990",
                "Пикантна майонеза със шрирача за суши, бургери и хрупкави хапки.", new[] { Eggs, Soy }),
            new("ДРАКОН СОС", "Сосове за суши", 0.86m, "https://dostavka.happy.bg/remote/files/images/31310/fit_600_376.png?rev=1654008677",
                "Плътен сос с пикантен характер и леко сладък завършек.", new[] { Soy, Sesame }),
            new("САЛАТА МАНГО И СКАРИДИ", "Средиземноморска кухня", 15.49m, "https://dostavka.happy.bg/remote/files/images/3749862/fit_600_376.png?rev=1761567532",
                "Средиземноморска салата със скариди, манго и цитрусов дресинг.", new[] { Crustaceans, Mustard, Sulfites }),
            new("ГРЪЦКАТА САЛАТА НА КАПИТАНА", "Средиземноморска кухня", 12.99m, "https://dostavka.happy.bg/remote/files/images/3064547/fit_600_376.png?rev=1761567588",
                "Гръцка салата с домати, краставици, сирене и маслини.", new[] { Milk, Sulfites }),
            new("ФРЕШ КОМБО 14 БР", "Суши", 13.29m, "https://dostavka.happy.bg/remote/files/images/4041707/fit_600_376.png?rev=1763450241",
                "Свежо суши комбо с ролки, морски вкус и балансирани сосове.", new[] { Fish, Soy, Milk, Sesame }),
            new("ДРАКОН КОМБО 18 БР", "Суши", 12.52m, "https://dostavka.happy.bg/remote/files/images/4040699/fit_600_376.png?rev=1762957634",
                "Комбо от 18 суши хапки с интензивен вкус и дракон сос.", new[] { Fish, Soy, Sesame }),
            new("СЪН ДЕНС 6 БР", "Футомаки", 5.62m, "https://dostavka.happy.bg/remote/files/images/4042379/fit_600_376.png?rev=1762958158",
                "Футомаки с ориз, нори, свежа плънка и лек сладко-солен баланс.", new[] { Fish, Soy, Sesame }),
            new("ФУТОМАКИ СЬОМГА И АВОКАДО 6 БР", "Футомаки", 5.62m, "https://dostavka.happy.bg/remote/files/images/797/fit_600_376.png?rev=1637858350",
                "Футомаки със сьомга, авокадо, ориз и нори.", new[] { Fish, Soy, Sesame }),
            new("КИМЧИ ЧИКЪН 4 БР", "Чикън", 3.57m, "https://dostavka.happy.bg/remote/files/images/4040027/fit_600_376.png?rev=1762957450",
                "Пилешки рол с кимчи, ориз и пикантен азиатски акцент.", new[] { Gluten, Soy, Sesame }),
            new("ТЕРИЯКИ ЧИКЪН 8 БР", "Чикън", 6.13m, "https://dostavka.happy.bg/remote/files/images/3704862/fit_600_376.png?rev=1758788476",
                "Рол с пилешко терияки, ориз и сладко-солен сос.", new[] { Gluten, Soy, Sesame }),
            new("Куатро Формаджи", "Пица", 9.20m, "https://dostavka.happy.bg/remote/files/images/428796/fit_600_376.png?rev=1773929602",
                "Италианска пица с четири сирена, доматена основа и ароматни билки.", new[] { Gluten, Milk }),
            new("МОРТАДЕЛА И ПИСТАЧИО", "Пица", 9.20m, "https://dostavka.happy.bg/remote/files/images/219862/fit_600_376.png?rev=1697101982",
                "Пица с мортадела, шамфъстък, сирена и италиански аромат.", new[] { Gluten, Milk, Nuts }),
            new("ОРИЗ С ТЕЛЕШКО БЛЕК АНГЪС И СКАРИДИ", "Паста и ризото", 9.97m, "https://dostavka.happy.bg/remote/files/images/3695524/fit_600_376.png?rev=1758658084",
                "Ориз с Блек Ангъс, скариди, зеленчуци и соев сос.", new[] { Crustaceans, Soy, Celery, Sesame }),
            new("ПАСТА ПРОТЕИНА С ПИЛЕ", "Паста и ризото", 8.43m, "https://dostavka.happy.bg/remote/files/images/4140968/fit_600_376.png?rev=1764069082",
                "Паста с пилешко, протеинова основа, сос и свежи подправки.", new[] { Gluten, Eggs, Milk })
        };

        private static readonly SeedDish[] BalancedHappyDishes = HappyDishes
            .GroupBy(dish => dish.Category)
            .SelectMany(group => group.Take(SeedDishesPerCategory))
            .ToArray();

        private static readonly string[] ActiveCategoryNames = BalancedHappyDishes
            .Select(dish => dish.Category)
            .Distinct()
            .ToArray();

        private static readonly HashSet<string> KnownSeedDishNames = HappyDishes
            .Select(dish => dish.Name)
            .Concat(LegacySeedDishNames)
            .ToHashSet(StringComparer.Ordinal);

        private static readonly HashSet<string> ActiveSeedDishNames = BalancedHappyDishes
            .Select(dish => dish.Name)
            .ToHashSet(StringComparer.Ordinal);

        private static readonly HashSet<string> ActiveSeedCategoryNames = ActiveCategoryNames
            .ToHashSet(StringComparer.Ordinal);

        private static readonly HashSet<string> KnownSeedCategoryNames = HappyCategoryNames
            .Concat(LegacySeedCategoryNames)
            .ToHashSet(StringComparer.Ordinal);

        public static async Task SeedHappyMenuAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<RestaurantAlloraProjectContext>();
            var cloudinarySettings = serviceProvider.GetRequiredService<IOptions<CloudinarySettings>>().Value;

            if (string.IsNullOrWhiteSpace(cloudinarySettings.CloudName))
            {
                throw new InvalidOperationException("Попълни CloudinarySettings:CloudName преди seed-ване на менюто.");
            }

            await SeedCategoriesAsync(context);
            await SeedDishesAsync(context, cloudinarySettings.CloudName);
            await RemoveUnusedSeedCategoriesAsync(context);
            await ReplaceLocalSeedImagesAsync(context, cloudinarySettings.CloudName);
        }

        public static IReadOnlyList<DishViewModel> BalanceMenuDishes(IEnumerable<DishViewModel> dishes)
            => dishes
                .Where(dish => ActiveSeedCategoryNames.Contains(dish.CategoryOfTheDish))
                .GroupBy(dish => dish.CategoryOfTheDish)
                .OrderBy(group => group.Key)
                .SelectMany(group => group
                    .OrderBy(dish => ActiveSeedDishNames.Contains(dish.NameOfTheDish) ? 0 : 1)
                    .ThenBy(dish => dish.NameOfTheDish)
                    .Take(SeedDishesPerCategory))
                .ToList();

        private static async Task SeedCategoriesAsync(RestaurantAlloraProjectContext context)
        {
            var existingNames = await context.Categories
                .Select(category => category.Name)
                .ToListAsync();

            var newCategories = ActiveCategoryNames
                .Where(categoryName => !existingNames.Contains(categoryName))
                .Select(categoryName => new Category
                {
                    CategoryId = Guid.NewGuid(),
                    Name = categoryName
                })
                .ToList();

            if (newCategories.Count == 0)
            {
                return;
            }

            context.Categories.AddRange(newCategories);
            await context.SaveChangesAsync();
        }

        private static async Task SeedDishesAsync(RestaurantAlloraProjectContext context, string cloudName)
        {
            await RemoveRetiredSeedDishesAsync(context);

            var categories = await context.Categories.ToDictionaryAsync(category => category.Name);
            var existingDishes = (await context.Dishes
                .Include(dish => dish.DishAllergens)
                .ToListAsync())
                .GroupBy(dish => dish.NameOfTheDish)
                .ToDictionary(group => group.Key, group => group.First());

            foreach (var seedDish in BalancedHappyDishes)
            {
                if (!categories.TryGetValue(seedDish.Category, out var category))
                {
                    continue;
                }

                var cloudinaryUrl = BuildCloudinaryFetchUrl(cloudName, seedDish.SourceImageUrl);

                if (existingDishes.TryGetValue(seedDish.Name, out var existingDish))
                {
                    existingDish.PriceOfTheDish = seedDish.Price;
                    existingDish.DescriptionOfTheDish = seedDish.Description;
                    existingDish.CategoryId = category.CategoryId;
                    existingDish.CategoryOfTheDish = category.Name;
                    existingDish.ImageUrl = cloudinaryUrl;
                    SyncDishAllergens(existingDish, seedDish);
                    continue;
                }

                var dish = new Dish
                {
                    DishId = Guid.NewGuid(),
                    NameOfTheDish = seedDish.Name,
                    DescriptionOfTheDish = seedDish.Description,
                    PriceOfTheDish = seedDish.Price,
                    CategoryId = category.CategoryId,
                    CategoryOfTheDish = category.Name,
                    ImageUrl = cloudinaryUrl
                };

                SyncDishAllergens(dish, seedDish);
                context.Dishes.Add(dish);
            }

            await context.SaveChangesAsync();
        }

        private static async Task RemoveRetiredSeedDishesAsync(RestaurantAlloraProjectContext context)
        {
            var retiredSeedDishes = await context.Dishes
                .Where(dish => KnownSeedDishNames.Contains(dish.NameOfTheDish)
                               && !ActiveSeedDishNames.Contains(dish.NameOfTheDish)
                               && !dish.OrderItems.Any()
                               && !dish.Reviews.Any()
                               && !dish.FavoritedBy.Any())
                .ToListAsync();

            if (retiredSeedDishes.Count == 0)
            {
                return;
            }

            context.Dishes.RemoveRange(retiredSeedDishes);
            await context.SaveChangesAsync();
        }

        private static async Task RemoveUnusedSeedCategoriesAsync(RestaurantAlloraProjectContext context)
        {
            var unusedSeedCategories = await context.Categories
                .Where(category => KnownSeedCategoryNames.Contains(category.Name)
                                   && !ActiveSeedCategoryNames.Contains(category.Name)
                                   && !category.Dishes.Any())
                .ToListAsync();

            if (unusedSeedCategories.Count == 0)
            {
                return;
            }

            context.Categories.RemoveRange(unusedSeedCategories);
            await context.SaveChangesAsync();
        }

        private static async Task ReplaceLocalSeedImagesAsync(RestaurantAlloraProjectContext context, string cloudName)
        {
            var localImageDishes = await context.Dishes
                .Where(dish => dish.ImageUrl.StartsWith("/img/"))
                .ToListAsync();

            if (localImageDishes.Count == 0)
            {
                return;
            }

            var fallbackByCategory = BalancedHappyDishes
                .GroupBy(dish => dish.Category)
                .ToDictionary(group => group.Key, group => group.First().SourceImageUrl);
            var fallbackImage = BalancedHappyDishes.First().SourceImageUrl;

            foreach (var dish in localImageDishes)
            {
                var seedDish = BalancedHappyDishes.FirstOrDefault(seed => seed.Name == dish.NameOfTheDish);
                var sourceImageUrl = seedDish?.SourceImageUrl
                    ?? (fallbackByCategory.TryGetValue(dish.CategoryOfTheDish, out var categoryImageUrl)
                        ? categoryImageUrl
                        : fallbackImage);

                dish.ImageUrl = BuildCloudinaryFetchUrl(cloudName, sourceImageUrl);
            }

            await context.SaveChangesAsync();
        }

        private static void SyncDishAllergens(Dish dish, SeedDish seedDish)
        {
            dish.DishAllergens.Clear();

            foreach (var allergenId in seedDish.Allergens
                         .Where(AllergenIds.ContainsKey)
                         .Select(allergen => AllergenIds[allergen])
                         .Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen
                {
                    DishId = dish.DishId,
                    AllergenId = allergenId
                });
            }
        }

        private static string BuildCloudinaryFetchUrl(string cloudName, string sourceImageUrl)
            => $"https://res.cloudinary.com/{cloudName}/image/fetch/f_auto,q_auto/{Uri.EscapeDataString(sourceImageUrl)}";

        private sealed record SeedDish(
            string Name,
            string Category,
            decimal Price,
            string SourceImageUrl,
            string Description,
            string[] Allergens);
    }
}
