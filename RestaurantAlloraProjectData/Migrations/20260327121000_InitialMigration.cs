using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantAlloraProjectData.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergens",
                columns: table => new
                {
                    AllergenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllergenName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergens", x => x.AllergenId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfTheDish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionOfTheDish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PriceOfTheDish = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryOfTheDish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    CapacityOfTheTable = table.Column<int>(type: "int", nullable: false),
                    StatusOfTheTable = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "Свободна")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerProfile",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_CustomerProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProfile",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfile", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_EmployeeProfile_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishAllergens",
                columns: table => new
                {
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllergenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishAllergens", x => new { x.DishId, x.AllergenId });
                    table.ForeignKey(
                        name: "FK_DishAllergens_Allergens_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Allergens",
                        principalColumn: "AllergenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishAllergens_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFavorites",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFavorites", x => new { x.CustomerId, x.DishId });
                    table.ForeignKey(
                        name: "FK_CustomerFavorites_CustomerProfile_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerFavorites_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_CustomerProfile_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderItems",
                columns: table => new
                {
                    CustomerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Обработва се"),
                    IsPickup = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderItems", x => x.CustomerOrderId);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItems_CustomerProfile_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItems_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItems_EmployeeProfile_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Очаква одобрение")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_CustomerProfile_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_EmployeeProfile_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "AllergenId", "AllergenName" },
                values: new object[,]
                {
                    { new Guid("247f192a-3e44-480a-bde9-98089f8b398b"), "Ядки – бадеми, орехи, лешници, кашу и др." },
                    { new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f"), "Целина и продукти от нея" },
                    { new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"), "Ракообразни и продукти от тях" },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), "Соя и соеви продукти" },
                    { new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"), "Серен диоксид и сулфиди" },
                    { new Guid("6ab84643-d4db-4789-bf18-f43afe7e4a38"), "Лупина и продукти от нея" },
                    { new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e"), "Мекотели и продукти от тях" },
                    { new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"), "Синап и продукти от него" },
                    { new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"), "Сусамово семе и продукти от него" },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), "Мляко и млечни продукти" },
                    { new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"), "Риба и рибни продукти" },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), "Яйца и продукти от тях" },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), "Зърнени култури, съдържащи глутен: пшеница, ръж, ечемик, овес, спелта, камут, както и продукти от тях" },
                    { new Guid("fe83dc00-a553-4041-8e2e-aa7c9eb5a0ed"), "Фъстъци и продукти от тях" }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "DishId", "CategoryOfTheDish", "DescriptionOfTheDish", "ImageUrl", "NameOfTheDish", "PriceOfTheDish" },
                values: new object[,]
                {
                    { new Guid("05267740-181b-43e4-96dd-e5b9c250ac75"), "Салати", "Черна леща Белуга, шарена киноа, нахут, краставици, чери домати, печен маринован пипер, маслини, магданоз, червен лук, дресинг Винегрет, мисо-лайм хумус и сумак.", "/img/fatush.png", "САЛАТА ЕНЕРДЖИ", 7.49m },
                    { new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57"), "Основни ястия", "Кюфтета от кълцано телешко Блек Ангъс, пържени картофи и микс салати с чери домати и дресинг винегрет.", "/img/meatballs.png", "ТЕЛЕШКИ КЮФТЕТА БЛЕК АНГЪС", 9.20m },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), "Основни ястия", "Традиционни китайски нудли с мариновано пилешко месо от бут, яйце, зеле, моркови, пресен зелен и червен пипер, специален сос и зелен лук.", "/img/noodles.png", "ЯЙЧНИ НУДЛИ С ПИЛЕ И ЗЕЛЕНЧУЦИ", 8.69m },
                    { new Guid("44ce4f8a-6e15-4947-bd6b-e20bdb14d842"), "Салати", "Салата Романа, микс салати, чери домати, краставици, репички, пресен червен пипер, лук, магданоз, босилек, свеж зеленчуков дресинг, хрупкав хляб, нар и дресинг нар.", "/img/fatush.png", "ФАТУШ", 7.49m },
                    { new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58"), "Салати", "Козе сирене, микс салати, свежи соеви кълнове, чери домати, ябълка, карамелизирани орехи, стафиди, сушени боровинки и нар.", "/img/instanbul.png", "ИСТАНБУЛ", 8.69m },
                    { new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89"), "Основни ястия", "Пуешки стек на BBQ със специална марината, запечени тиквички, гъби, моркови и карфиол с билкова марината.", "/img/stek.png", "ПУЕШКИ СТЕК НА BBQ", 9.97m },
                    { new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec"), "Салати", "Мариновани скариди, авокадо, шарена киноа, микс салати със спанак, червен лук, чери домати, японски дресинг с мисо паста и див лук.", "/img/geisha.png", "САЛАТА ГЕЙША", 17.30m },
                    { new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c"), "Десерти", "Чийзкейк с хрупкав блат, маскарпоне крем и пистачио глазура.", "/img/pistachio.png", "ПИСТАЧИО ЧИЙЗКЕЙК", 5.62m },
                    { new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef"), "Основни ястия", "Филе бяла риба и спагети в Алфредо сос със спанак и азиатски подправки, пармезан, пресен пипер и див лук.", "/img/fish.png", "ФИЛЕ БЯЛА РИБА ИТАМЕШИ", 9.71m },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), "Основни ястия", "Пилешки късчета с леко пикантен азиатски сос и подправки, яйце, пресен босилек и магданоз върху жасминов ориз.", "/img/chicken.png", "ПИЛЕ ПАД КАПРАО", 9.20m }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "CapacityOfTheTable", "StatusOfTheTable", "TableNumber" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 2, "Свободна", 1 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 2, "Свободна", 2 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), 2, "Свободна", 3 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), 2, "Свободна", 4 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), 2, "Свободна", 5 },
                    { new Guid("10000000-0000-0000-0000-000000000006"), 2, "Свободна", 6 },
                    { new Guid("10000000-0000-0000-0000-000000000007"), 2, "Свободна", 7 },
                    { new Guid("10000000-0000-0000-0000-000000000008"), 2, "Свободна", 8 },
                    { new Guid("10000000-0000-0000-0000-000000000009"), 2, "Свободна", 9 },
                    { new Guid("10000000-0000-0000-0000-000000000010"), 2, "Свободна", 10 },
                    { new Guid("10000000-0000-0000-0000-000000000011"), 2, "Свободна", 11 },
                    { new Guid("10000000-0000-0000-0000-000000000012"), 2, "Свободна", 12 },
                    { new Guid("10000000-0000-0000-0000-000000000013"), 2, "Свободна", 13 },
                    { new Guid("10000000-0000-0000-0000-000000000014"), 2, "Свободна", 14 },
                    { new Guid("10000000-0000-0000-0000-000000000015"), 2, "Свободна", 15 },
                    { new Guid("10000000-0000-0000-0000-000000000016"), 2, "Свободна", 16 },
                    { new Guid("10000000-0000-0000-0000-000000000017"), 2, "Свободна", 17 },
                    { new Guid("10000000-0000-0000-0000-000000000018"), 2, "Свободна", 18 },
                    { new Guid("10000000-0000-0000-0000-000000000019"), 2, "Свободна", 19 },
                    { new Guid("10000000-0000-0000-0000-000000000020"), 2, "Свободна", 20 },
                    { new Guid("10000000-0000-0000-0000-000000000021"), 4, "Свободна", 21 },
                    { new Guid("10000000-0000-0000-0000-000000000022"), 4, "Свободна", 22 },
                    { new Guid("10000000-0000-0000-0000-000000000023"), 4, "Свободна", 23 },
                    { new Guid("10000000-0000-0000-0000-000000000024"), 4, "Свободна", 24 },
                    { new Guid("10000000-0000-0000-0000-000000000025"), 4, "Свободна", 25 },
                    { new Guid("10000000-0000-0000-0000-000000000026"), 4, "Свободна", 26 },
                    { new Guid("10000000-0000-0000-0000-000000000027"), 4, "Свободна", 27 },
                    { new Guid("10000000-0000-0000-0000-000000000028"), 4, "Свободна", 28 },
                    { new Guid("10000000-0000-0000-0000-000000000029"), 4, "Свободна", 29 },
                    { new Guid("10000000-0000-0000-0000-000000000030"), 4, "Свободна", 30 },
                    { new Guid("10000000-0000-0000-0000-000000000031"), 4, "Свободна", 31 },
                    { new Guid("10000000-0000-0000-0000-000000000032"), 4, "Свободна", 32 },
                    { new Guid("10000000-0000-0000-0000-000000000033"), 4, "Свободна", 33 },
                    { new Guid("10000000-0000-0000-0000-000000000034"), 4, "Свободна", 34 },
                    { new Guid("10000000-0000-0000-0000-000000000035"), 4, "Свободна", 35 },
                    { new Guid("10000000-0000-0000-0000-000000000036"), 4, "Свободна", 36 },
                    { new Guid("10000000-0000-0000-0000-000000000037"), 4, "Свободна", 37 },
                    { new Guid("10000000-0000-0000-0000-000000000038"), 4, "Свободна", 38 },
                    { new Guid("10000000-0000-0000-0000-000000000039"), 4, "Свободна", 39 },
                    { new Guid("10000000-0000-0000-0000-000000000040"), 4, "Свободна", 40 },
                    { new Guid("10000000-0000-0000-0000-000000000041"), 4, "Свободна", 41 },
                    { new Guid("10000000-0000-0000-0000-000000000042"), 4, "Свободна", 42 },
                    { new Guid("10000000-0000-0000-0000-000000000043"), 4, "Свободна", 43 },
                    { new Guid("10000000-0000-0000-0000-000000000044"), 4, "Свободна", 44 },
                    { new Guid("10000000-0000-0000-0000-000000000045"), 4, "Свободна", 45 },
                    { new Guid("10000000-0000-0000-0000-000000000046"), 4, "Свободна", 46 },
                    { new Guid("10000000-0000-0000-0000-000000000047"), 4, "Свободна", 47 },
                    { new Guid("10000000-0000-0000-0000-000000000048"), 4, "Свободна", 48 },
                    { new Guid("10000000-0000-0000-0000-000000000049"), 4, "Свободна", 49 },
                    { new Guid("10000000-0000-0000-0000-000000000050"), 4, "Свободна", 50 },
                    { new Guid("10000000-0000-0000-0000-000000000051"), 4, "Свободна", 51 },
                    { new Guid("10000000-0000-0000-0000-000000000052"), 4, "Свободна", 52 },
                    { new Guid("10000000-0000-0000-0000-000000000053"), 4, "Свободна", 53 },
                    { new Guid("10000000-0000-0000-0000-000000000054"), 4, "Свободна", 54 },
                    { new Guid("10000000-0000-0000-0000-000000000055"), 4, "Свободна", 55 },
                    { new Guid("10000000-0000-0000-0000-000000000056"), 4, "Свободна", 56 },
                    { new Guid("10000000-0000-0000-0000-000000000057"), 4, "Свободна", 57 },
                    { new Guid("10000000-0000-0000-0000-000000000058"), 4, "Свободна", 58 },
                    { new Guid("10000000-0000-0000-0000-000000000059"), 4, "Свободна", 59 },
                    { new Guid("10000000-0000-0000-0000-000000000060"), 4, "Свободна", 60 },
                    { new Guid("10000000-0000-0000-0000-000000000061"), 6, "Свободна", 61 },
                    { new Guid("10000000-0000-0000-0000-000000000062"), 6, "Свободна", 62 },
                    { new Guid("10000000-0000-0000-0000-000000000063"), 6, "Свободна", 63 },
                    { new Guid("10000000-0000-0000-0000-000000000064"), 6, "Свободна", 64 },
                    { new Guid("10000000-0000-0000-0000-000000000065"), 6, "Свободна", 65 },
                    { new Guid("10000000-0000-0000-0000-000000000066"), 6, "Свободна", 66 },
                    { new Guid("10000000-0000-0000-0000-000000000067"), 6, "Свободна", 67 },
                    { new Guid("10000000-0000-0000-0000-000000000068"), 6, "Свободна", 68 },
                    { new Guid("10000000-0000-0000-0000-000000000069"), 6, "Свободна", 69 },
                    { new Guid("10000000-0000-0000-0000-000000000070"), 6, "Свободна", 70 },
                    { new Guid("10000000-0000-0000-0000-000000000071"), 6, "Свободна", 71 },
                    { new Guid("10000000-0000-0000-0000-000000000072"), 6, "Свободна", 72 },
                    { new Guid("10000000-0000-0000-0000-000000000073"), 6, "Свободна", 73 },
                    { new Guid("10000000-0000-0000-0000-000000000074"), 6, "Свободна", 74 },
                    { new Guid("10000000-0000-0000-0000-000000000075"), 6, "Свободна", 75 },
                    { new Guid("10000000-0000-0000-0000-000000000076"), 6, "Свободна", 76 },
                    { new Guid("10000000-0000-0000-0000-000000000077"), 6, "Свободна", 77 },
                    { new Guid("10000000-0000-0000-0000-000000000078"), 6, "Свободна", 78 },
                    { new Guid("10000000-0000-0000-0000-000000000079"), 6, "Свободна", 79 },
                    { new Guid("10000000-0000-0000-0000-000000000080"), 6, "Свободна", 80 },
                    { new Guid("10000000-0000-0000-0000-000000000081"), 8, "Свободна", 81 },
                    { new Guid("10000000-0000-0000-0000-000000000082"), 8, "Свободна", 82 },
                    { new Guid("10000000-0000-0000-0000-000000000083"), 8, "Свободна", 83 },
                    { new Guid("10000000-0000-0000-0000-000000000084"), 8, "Свободна", 84 },
                    { new Guid("10000000-0000-0000-0000-000000000085"), 8, "Свободна", 85 },
                    { new Guid("10000000-0000-0000-0000-000000000086"), 8, "Свободна", 86 },
                    { new Guid("10000000-0000-0000-0000-000000000087"), 8, "Свободна", 87 },
                    { new Guid("10000000-0000-0000-0000-000000000088"), 8, "Свободна", 88 },
                    { new Guid("10000000-0000-0000-0000-000000000089"), 8, "Свободна", 89 },
                    { new Guid("10000000-0000-0000-0000-000000000090"), 8, "Свободна", 90 },
                    { new Guid("10000000-0000-0000-0000-000000000091"), 10, "Свободна", 91 },
                    { new Guid("10000000-0000-0000-0000-000000000092"), 10, "Свободна", 92 },
                    { new Guid("10000000-0000-0000-0000-000000000093"), 10, "Свободна", 93 },
                    { new Guid("10000000-0000-0000-0000-000000000094"), 10, "Свободна", 94 },
                    { new Guid("10000000-0000-0000-0000-000000000095"), 10, "Свободна", 95 },
                    { new Guid("10000000-0000-0000-0000-000000000096"), 10, "Свободна", 96 },
                    { new Guid("10000000-0000-0000-0000-000000000097"), 10, "Свободна", 97 },
                    { new Guid("10000000-0000-0000-0000-000000000098"), 10, "Свободна", 98 },
                    { new Guid("10000000-0000-0000-0000-000000000099"), 10, "Свободна", 99 },
                    { new Guid("10000000-0000-0000-0000-000000000100"), 10, "Свободна", 100 }
                });

            migrationBuilder.InsertData(
                table: "DishAllergens",
                columns: new[] { "AllergenId", "DishId" },
                values: new object[,]
                {
                    { new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("71979776-b2cb-4b9f-84b4-6165b80871ec") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFavorites_DishId",
                table: "CustomerFavorites",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_CustomerId",
                table: "CustomerOrderItems",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_DishId",
                table: "CustomerOrderItems",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_EmployeeId",
                table: "CustomerOrderItems",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DishAllergens_AllergenId",
                table: "DishAllergens",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CustomerId",
                table: "Reservations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EmployeeId",
                table: "Reservations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CustomerId_DishId",
                table: "Reviews",
                columns: new[] { "CustomerId", "DishId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DishId",
                table: "Reviews",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_TableNumber",
                table: "Tables",
                column: "TableNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CustomerFavorites");

            migrationBuilder.DropTable(
                name: "CustomerOrderItems");

            migrationBuilder.DropTable(
                name: "DishAllergens");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "EmployeeProfile");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "CustomerProfile");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
