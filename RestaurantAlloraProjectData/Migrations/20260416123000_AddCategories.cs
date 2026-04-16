using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RestaurantAlloraProjectData;

#nullable disable

namespace RestaurantAlloraProjectData.Migrations
{
    [DbContext(typeof(RestaurantAlloraProjectContext))]
    [Migration("20260416123000_AddCategories")]
    public partial class AddCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.Sql("""
                INSERT INTO [Categories] ([CategoryId], [Name])
                VALUES
                    ('a19f1c7a-0a27-4c91-a220-2f4c55fb0b21', N'Салати'),
                    ('0a55dc5d-23b6-4c3a-8428-3f0f7f370aa6', N'Основни ястия'),
                    ('b3cb4f8b-8f1c-44f7-a332-3f2d2bb24b0b', N'Десерти'),
                    ('aeae6939-7449-467f-b1d6-b0cbd340fc7d', N'Напитки')
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
