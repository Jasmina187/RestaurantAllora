using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;

namespace RestaurantAlloraProjectData.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder
                .HasIndex(t => t.TableNumber)
                .IsUnique();

            builder
                .Property(t => t.StatusOfTheTable)
                .HasDefaultValue("Свободна");

            var tables = new List<Table>();
            var currentTableNumber = 1;

            AddTables(tables, ref currentTableNumber, 20, 2);
            AddTables(tables, ref currentTableNumber, 40, 4);
            AddTables(tables, ref currentTableNumber, 20, 6);
            AddTables(tables, ref currentTableNumber, 10, 8);
            AddTables(tables, ref currentTableNumber, 10, 10);

            builder.HasData(tables);
        }

        private static void AddTables(List<Table> tables, ref int currentTableNumber, int count, int capacity)
        {
            for (var i = 0; i < count; i++)
            {
                tables.Add(new Table
                {
                    TableId = new Guid($"10000000-0000-0000-0000-{currentTableNumber:D12}"),
                    TableNumber = currentTableNumber,
                    CapacityOfTheTable = capacity,
                    StatusOfTheTable = "Свободна"
                });

                currentTableNumber++;
            }
        }
    }
}
