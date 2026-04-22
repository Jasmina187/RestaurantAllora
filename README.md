# Restaurant Allora

Restaurant Allora е ASP.NET Core MVC приложение за ресторант с публично меню, онлайн поръчки, резервации, любими ястия, ревюта и администраторско табло.

## Основни възможности

- Регистрация, вход и роли: `Admin`, `Employee`, `Customer`.
- Публично меню с категории, търсене и филтър по алергени.
- Клиентско меню с количка и финализиране на поръчка.
- Резервации на маси с проверка за заетост и статуси.
- Любими ястия и ревюта от клиенти.
- Админ табло за поръчки, резервации, приходи и популярни ястия.
- Управление на ястия, категории, маси, поръчки, потребители и ревюта.

## Технологии

- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Core Identity
- SQL Server
- Bootstrap
- Cloudinary за качване и fetch на изображения
- xUnit тестове

## Стартиране

1. Отвори `RestaurantAlloraProjectWeb/appsettings.json`.
2. Смени `ConnectionStrings:DefaultConnection` според SQL Server-а на твоя компютър.
3. Провери `CloudinarySettings`, ако ще качваш снимки през админ формите.
4. Стартирай приложението:

```powershell
dotnet run --project .\RestaurantAlloraProjectWeb\RestaurantAlloraProjectWeb.csproj
```

При старт приложението изпълнява миграциите и seed-ва роли, администраторски профил и начално меню.

## Важни файлове за изображения

Статичните снимки и иконите за алергени са в:

```text
RestaurantAlloraProjectWeb/wwwroot/img/
RestaurantAlloraProjectWeb/wwwroot/img/allergens/
```

Тези файлове трябва да бъдат commit-нати в Git. Ако липсват на друг компютър, няма да се показват иконите за алергени и снимките на началната страница.

Снимките на ястията се пазят в базата като URL в колоната `Dishes.ImageUrl`. Cloudinary пази самите изображения, но списъкът с ястия и техните URL-и идва от SQL базата.

## Seed по подразбиране

Проектът създава роли `Admin`, `Employee` и `Customer`, както и администраторски профил:

- Потребителско име: `admin`
- Имейл: `admin@gmail.com`
- Парола: `Admin1*`

Seed-ът за менюто добавя категории, ястия, описания, алергени и Cloudinary fetch URL-и. Ако друг лаптоп използва нова празна база, ще вижда само данните от seed-а, а не ръчно добавените ястия от твоята локална база.

## Тестове

```powershell
dotnet test .\RestaurantAllora.sln
```
