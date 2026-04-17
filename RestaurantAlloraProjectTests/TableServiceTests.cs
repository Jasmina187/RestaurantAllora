using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Table;

namespace RestaurantAlloraProjectTests;

public class TableServiceTests
{
    [Fact]
    public async Task GetAllAsync_MarksTableReservedWhenItHasFutureApprovedReservation()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        context.Tables.Add(table);
        context.Reservations.Add(new Reservation
        {
            ReservationId = Guid.NewGuid(),
            TableId = table.TableId,
            Table = table,
            CustomerId = Guid.NewGuid(),
            ReservationDate = DateTime.Now.AddHours(1),
            NumberOfGuests = 2,
            Status = "Одобрена"
        });
        await context.SaveChangesAsync();
        var service = new TableService(context);

        var result = Assert.Single(await service.GetAllAsync());

        Assert.Equal("Резервирана", result.StatusOfTheTable);
        Assert.Single(result.ActiveReservationStarts);
    }

    [Fact]
    public async Task CreateAsync_ThrowsWhenTableNumberAlreadyExists()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Tables.Add(TestDataFactory.CreateTable(number: 7));
        await context.SaveChangesAsync();
        var service = new TableService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(new TableViewModel
        {
            TableNumber = 7,
            CapacityOfTheTable = 4
        }));
    }

    [Fact]
    public async Task UpdateAsync_ChangesTableDetails()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(number: 3, capacity: 2);
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        var service = new TableService(context);

        await service.UpdateAsync(new TableViewModel
        {
            TableId = table.TableId,
            TableNumber = 5,
            CapacityOfTheTable = 6,
            StatusOfTheTable = ""
        });

        Assert.Equal(5, table.TableNumber);
        Assert.Equal(6, table.CapacityOfTheTable);
        Assert.Equal("Свободна", table.StatusOfTheTable);
    }
}
