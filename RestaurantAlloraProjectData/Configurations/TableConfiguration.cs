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
            var tables = new List<Table>();
            int currentTableNumber = 1;
            for (int i = 0; i < 20; i++)
            {
                tables.Add(new Table
                {
                    TableId = new Guid($"10000000-0000-0000-0000-{currentTableNumber:D12}"),
                    TableNumber = currentTableNumber,
                    CapacityOfTheTable = 2,
                    StatusOfTheTable = "Свободна"
                });
                currentTableNumber++;
            }

      
            for (int i = 0; i < 40; i++)
            {
                tables.Add(new Table
                {
                    TableId = new Guid($"10000000-0000-0000-0000-{currentTableNumber:D12}"),
                    TableNumber = currentTableNumber,
                    CapacityOfTheTable = 4,
                    StatusOfTheTable = "Свободна"
                });
                currentTableNumber++;
            }

           
            for (int i = 0; i < 20; i++)
            {
                tables.Add(new Table
                {
                    TableId = new Guid($"10000000-0000-0000-0000-{currentTableNumber:D12}"),
                    TableNumber = currentTableNumber,
                    CapacityOfTheTable = 6,
                    StatusOfTheTable = "Свободна"
                });
                currentTableNumber++;
            }

          
            for (int i = 0; i < 10; i++)
            {
                tables.Add(new Table
                {
                    TableId = new Guid($"10000000-0000-0000-0000-{currentTableNumber:D12}"),
                    TableNumber = currentTableNumber,
                    CapacityOfTheTable = 8,
                    StatusOfTheTable = "Свободна"
                });
                currentTableNumber++;
            }

        
            for (int i = 0; i < 10; i++)
            {
                tables.Add(new Table
                {
                    TableId = new Guid($"10000000-0000-0000-0000-{currentTableNumber:D12}"),
                    TableNumber = currentTableNumber,
                    CapacityOfTheTable = 10,
                    StatusOfTheTable = "Свободна"
                });
                currentTableNumber++;
            }

            builder.HasData(tables);
        }
    }
}
