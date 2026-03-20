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
            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("a1f1c2d3-1111-4b11-8111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("a4a4b5c6-dddd-47dd-8ddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("a7a7c8d9-7777-4177-8777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("b2f2c3d4-2222-4c22-8222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("b5b5c6d7-eeee-48ee-8eee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("b8b8c9e1-8888-4288-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("c3f3c4d5-3333-4d33-8333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("c9c9d1e2-9999-4399-8999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("d1d1e2f3-aaaa-44aa-8aaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("d4f4c5d6-4444-4e44-8444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("e2e2f3a4-bbbb-45bb-8bbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("e5f5c6d7-5555-4f55-8555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("f3f3a4b5-cccc-46cc-8ccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("f6f6c7d8-6666-4066-8666-666666666666"));

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "AllergenId", "AllergenName" },
                values: new object[,]
                {
                    { new Guid("063e346a-e671-4710-835b-8f6132ee7744"), "Серен диоксид и сулфиди" },
                    { new Guid("2138d67d-b112-4d75-8340-4130c6d49f6f"), "Синап и продукти от него" },
                    { new Guid("32aabc2d-ef09-4d9f-b85f-973062edf531"), "Ядки – бадеми, орехи, лешници, кашу и др." },
                    { new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"), "Ракообразни и продукти от тях" },
                    { new Guid("4cab969c-d612-4a00-9d29-3aedc8d90f3b"), "Соя и соеви продукти" },
                    { new Guid("4eed56a2-0d6e-4c82-811f-d84066553249"), "Сусамово семе и продукти от него" },
                    { new Guid("8459d499-ec62-49a3-b93c-fdd2a75c940b"), "Лупина и продукти от нея" },
                    { new Guid("859bc440-5fd3-4a13-b827-48a2540ffc6c"), "Целина и продукти от нея" },
                    { new Guid("945ea094-2772-4113-80f9-4b9c8fc5188d"), "Мекотели и продукти от тях" },
                    { new Guid("be7c8688-70de-4742-a524-d3d8e99616d8"), "Мляко и млечни продукти" },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), "Яйца и продукти от тях" },
                    { new Guid("c8f84b5a-4cd4-405b-adb4-2eeb69942cfc"), "Риба и рибни продукти" },
                    { new Guid("d72de7c5-8e83-4d4c-a8e2-c5cdcf8da39e"), "Фъстъци и продукти от тях" },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), "Зърнени култури, съдържащи глутен" }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("063e346a-e671-4710-835b-8f6132ee7744"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("2138d67d-b112-4d75-8340-4130c6d49f6f"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("32aabc2d-ef09-4d9f-b85f-973062edf531"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("42ccb79f-216b-4aab-9655-944d4f7b9823"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("4cab969c-d612-4a00-9d29-3aedc8d90f3b"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("4eed56a2-0d6e-4c82-811f-d84066553249"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("8459d499-ec62-49a3-b93c-fdd2a75c940b"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("859bc440-5fd3-4a13-b827-48a2540ffc6c"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("945ea094-2772-4113-80f9-4b9c8fc5188d"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("be7c8688-70de-4742-a524-d3d8e99616d8"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("c8f84b5a-4cd4-405b-adb4-2eeb69942cfc"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("d72de7c5-8e83-4d4c-a8e2-c5cdcf8da39e"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("dc08b4ec-5095-4811-a672-192301357e16"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000076"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000077"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000078"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000079"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000080"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000081"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000082"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000083"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000084"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000085"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000086"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000088"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000091"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000092"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000093"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000094"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000095"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000096"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000097"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000098"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000099"));

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000100"));

            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "AllergenId", "AllergenName" },
                values: new object[,]
                {
                    { new Guid("a1f1c2d3-1111-4b11-8111-111111111111"), "Зърнени култури, съдържащи глутен" },
                    { new Guid("a4a4b5c6-dddd-47dd-8ddd-dddddddddddd"), "Серен диоксид и сулфиди" },
                    { new Guid("a7a7c8d9-7777-4177-8777-777777777777"), "Мляко и млечни продукти" },
                    { new Guid("b2f2c3d4-2222-4c22-8222-222222222222"), "Ракообразни и продукти от тях" },
                    { new Guid("b5b5c6d7-eeee-48ee-8eee-eeeeeeeeeeee"), "Мекотели и продукти от тях" },
                    { new Guid("b8b8c9e1-8888-4288-8888-888888888888"), "Ядки – бадеми, орехи, лешници, кашу и др." },
                    { new Guid("c3f3c4d5-3333-4d33-8333-333333333333"), "Яйца и продукти от тях" },
                    { new Guid("c9c9d1e2-9999-4399-8999-999999999999"), "Целина и продукти от нея" },
                    { new Guid("d1d1e2f3-aaaa-44aa-8aaa-aaaaaaaaaaaa"), "Синап и продукти от него" },
                    { new Guid("d4f4c5d6-4444-4e44-8444-444444444444"), "Риба и рибни продукти" },
                    { new Guid("e2e2f3a4-bbbb-45bb-8bbb-bbbbbbbbbbbb"), "Сусамово семе и продукти от него" },
                    { new Guid("e5f5c6d7-5555-4f55-8555-555555555555"), "Фъстъци и продукти от тях" },
                    { new Guid("f3f3a4b5-cccc-46cc-8ccc-cccccccccccc"), "Лупина и продукти от нея" },
                    { new Guid("f6f6c7d8-6666-4066-8666-666666666666"), "Соя и соеви продукти" }
                });
        }
    }
}
