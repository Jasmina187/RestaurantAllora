using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAlloraProjectData.Migrations
{
    public partial class SeedDishAllergens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"),
                column: "AllergenName",
                value: "Мляко и млечни продукти (включително лактоза)");

            migrationBuilder.UpdateData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"),
                column: "AllergenName",
                value: "Серен диоксид и сулфити с концентрация над 10 mg/kg или 10 mg/l");

            migrationBuilder.InsertData(
                table: "DishAllergens",
                columns: new[] { "AllergenId", "DishId" },
                values: new object[,]
                {
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58") },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58") },
                    { new Guid("247f192a-3e44-480a-bde9-98089f8b398b"), new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("44ce4f8a-6e15-4947-bd6b-e20bdb14d842") },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("05267740-181b-43e4-96dd-e5b9c250ac75") },
                    { new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"), new Guid("05267740-181b-43e4-96dd-e5b9c250ac75") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89") },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89") },
                    { new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f"), new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89") },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57") },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57") },
                    { new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99"), new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef") },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef") },
                    { new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528"), new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef") },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b") },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b") },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b") },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b") },
                    { new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f"), new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b") },
                    { new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e"), new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("503510df-d3a4-4266-a182-3b3db962de57"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e"), new Guid("f4201dbf-1adb-4949-b235-d137db7698f7") },
                    { new Guid("dc08b4ec-5095-4811-a672-192301357e16"), new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c") },
                    { new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977"), new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c") },
                    { new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"), new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c") },
                    { new Guid("247f192a-3e44-480a-bde9-98089f8b398b"), new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DishAllergens",
                keyColumns: new[] { "DishId", "AllergenId" },
                keyValues: new object[,]
                {
                    { new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58"), new Guid("503510df-d3a4-4266-a182-3b3db962de57") },
                    { new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58"), new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2") },
                    { new Guid("5b48ad92-c4f7-4b93-a015-789f26f40d58"), new Guid("247f192a-3e44-480a-bde9-98089f8b398b") },
                    { new Guid("44ce4f8a-6e15-4947-bd6b-e20bdb14d842"), new Guid("dc08b4ec-5095-4811-a672-192301357e16") },
                    { new Guid("05267740-181b-43e4-96dd-e5b9c250ac75"), new Guid("503510df-d3a4-4266-a182-3b3db962de57") },
                    { new Guid("05267740-181b-43e4-96dd-e5b9c250ac75"), new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a") },
                    { new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89"), new Guid("dc08b4ec-5095-4811-a672-192301357e16") },
                    { new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89"), new Guid("503510df-d3a4-4266-a182-3b3db962de57") },
                    { new Guid("6482fc36-4bfc-4cda-874b-2300a5f3cc89"), new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f") },
                    { new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57"), new Guid("503510df-d3a4-4266-a182-3b3db962de57") },
                    { new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57"), new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2") },
                    { new Guid("12489f7b-9d30-4a28-aba0-c9bd5cc7dd57"), new Guid("7d1e6d36-e29a-40ca-970a-3c016cfb7a99") },
                    { new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef"), new Guid("dc08b4ec-5095-4811-a672-192301357e16") },
                    { new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef"), new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977") },
                    { new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef"), new Guid("bcccc627-fc03-40cc-bfb4-9047d4626528") },
                    { new Guid("a450b794-5144-4f72-a436-bb29fbbb00ef"), new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2") },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), new Guid("dc08b4ec-5095-4811-a672-192301357e16") },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977") },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), new Guid("503510df-d3a4-4266-a182-3b3db962de57") },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2") },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f") },
                    { new Guid("3cfc8ca4-f70c-47d8-9cc1-dee9c6de5c1b"), new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("dc08b4ec-5095-4811-a672-192301357e16") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("503510df-d3a4-4266-a182-3b3db962de57") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("3a8c6114-15c6-4f6c-9de1-21126a38706f") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("7f4554e9-9835-479e-bb37-b97ed9c58d6a") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("64d4fbb0-ffe7-4526-9d18-300608276013") },
                    { new Guid("f4201dbf-1adb-4949-b235-d137db7698f7"), new Guid("6b990dac-6b4f-49f4-a715-ccaa67246c3e") },
                    { new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c"), new Guid("dc08b4ec-5095-4811-a672-192301357e16") },
                    { new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c"), new Guid("c4399ef7-4776-4b45-92fb-35ae3dd3f977") },
                    { new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c"), new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2") },
                    { new Guid("a02bbc5e-4df1-4e8e-a923-33b65336614c"), new Guid("247f192a-3e44-480a-bde9-98089f8b398b") }
                });

            migrationBuilder.UpdateData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("a846b85f-53b1-4b2a-b096-825824c3b7e2"),
                column: "AllergenName",
                value: "Мляко и млечни продукти");

            migrationBuilder.UpdateData(
                table: "Allergens",
                keyColumn: "AllergenId",
                keyValue: new Guid("64d4fbb0-ffe7-4526-9d18-300608276013"),
                column: "AllergenName",
                value: "Серен диоксид и сулфиди");
        }
    }
}
