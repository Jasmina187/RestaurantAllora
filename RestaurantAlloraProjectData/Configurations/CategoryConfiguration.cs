using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectData.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.HasData(new List<Category>
            {
                new Category
                {
                    CategoryId = new Guid("a19f1c7a-0a27-4c91-a220-2f4c55fb0b21"),
                    Name = "Салати"
                },
                new Category
                {
                    CategoryId = new Guid("0a55dc5d-23b6-4c3a-8428-3f0f7f370aa6"),
                    Name = "Основни ястия"
                },
                new Category
                {
                    CategoryId = new Guid("b3cb4f8b-8f1c-44f7-a332-3f2d2bb24b0b"),
                    Name = "Десерти"
                },
                new Category
                {
                    CategoryId = new Guid("aeae6939-7449-467f-b1d6-b0cbd340fc7d"),
                    Name = "Напитки"
                }
            });
        }
    }
}
