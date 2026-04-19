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
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
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
                name: "Dishes",
                columns: table => new
                {
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfTheDish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionOfTheDish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PriceOfTheDish = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryOfTheDish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.DishId);
                    table.ForeignKey(
                        name: "FK_Dishes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Обработва се"),
                    FulfillmentType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Вземане на място"),
                    CustomerFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    CustomerPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: ""),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_CustomerProfile_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerProfile",
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderItems", x => x.Id);
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
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CustomerOrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
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
                    { new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"), "Серен диоксид и сулфити с концентрация над 10 mg/kg или 10 mg/l" },
                    { new Guid("6ab84643-d4db-4789-bf18-f43afe7e4a38"), "Лупина и продукти от нея" },
                    { new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e"), "Мекотели и продукти от тях" },
                    { new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"), "Синап и продукти от него" },
                    { new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"), "Сусамово семе и продукти от него" },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), "Мляко и млечни продукти (включително лактоза)" },
                    { new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"), "Риба и рибни продукти" },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), "Яйца и продукти от тях" },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), "Зърнени култури, съдържащи глутен: пшеница, ръж, ечемик, овес, спелта, камут, както и продукти от тях" },
                    { new Guid("fe83dc00-a553-4041-8e2e-aa7c9eb5a0ed"), "Фъстъци и продукти от тях" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("0a55dc5d-23b6-4c3a-8428-3f0f7f370aa6"), "Основни ястия" },
                    { new Guid("a19f1c7a-0a27-4c91-a220-2f4c55fb0b21"), "Салати" },
                    { new Guid("aeae6939-7449-467f-b1d6-b0cbd340fc7d"), "Напитки" },
                    { new Guid("b3cb4f8b-8f1c-44f7-a332-3f2d2bb24b0b"), "Десерти" }
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
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFavorites_DishId",
                table: "CustomerFavorites",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_DishId",
                table: "CustomerOrderItems",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_EmployeeId",
                table: "CustomerOrderItems",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderItems_OrderId",
                table: "CustomerOrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DishAllergens_AllergenId",
                table: "DishAllergens",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_CategoryId",
                table: "Dishes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

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
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Allergens");

            migrationBuilder.DropTable(
                name: "EmployeeProfile");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "CustomerProfile");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
