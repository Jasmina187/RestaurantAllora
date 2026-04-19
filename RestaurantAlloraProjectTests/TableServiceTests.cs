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
    public async Task GetAllAsync_MarksTablePendingWhenItHasOnlyFuturePendingReservation()
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
            Status = "Очаква одобрение"
        });
        await context.SaveChangesAsync();
        var service = new TableService(context);

        var result = Assert.Single(await service.GetAllAsync());

        Assert.Equal("Очаква одобрение", result.StatusOfTheTable);
        Assert.Single(result.PendingReservationStarts);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsTableStatusOrNull()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(number: 9);
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        var service = new TableService(context);

        var found = await service.GetByIdAsync(table.TableId);
        var missing = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Equal(9, found?.TableNumber);
        Assert.Equal("Свободна", found?.StatusOfTheTable);
        Assert.Null(missing);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsReservedAndPendingReservationStarts()
    {
        await using var context = TestDataFactory.CreateContext();
        var reservedTable = TestDataFactory.CreateTable(number: 1);
        var pendingTable = TestDataFactory.CreateTable(number: 2);
        context.Tables.AddRange(reservedTable, pendingTable);
        context.Reservations.AddRange(
            new Reservation
            {
                ReservationId = Guid.NewGuid(),
                TableId = reservedTable.TableId,
                Table = reservedTable,
                CustomerId = Guid.NewGuid(),
                ReservationDate = DateTime.Now.AddHours(1),
                NumberOfGuests = 2,
                Status = "Одобрена"
            },
            new Reservation
            {
                ReservationId = Guid.NewGuid(),
                TableId = pendingTable.TableId,
                Table = pendingTable,
                CustomerId = Guid.NewGuid(),
                ReservationDate = DateTime.Now.AddHours(2),
                NumberOfGuests = 2,
                Status = "Очаква одобрение"
            });
        await context.SaveChangesAsync();
        var service = new TableService(context);

        var reserved = await service.GetByIdAsync(reservedTable.TableId);
        var pending = await service.GetByIdAsync(pendingTable.TableId);

        Assert.Equal("Резервирана", reserved?.StatusOfTheTable);
        Assert.Single(reserved!.ActiveReservationStarts);
        Assert.Equal("Очаква одобрение", pending?.StatusOfTheTable);
        Assert.Single(pending!.PendingReservationStarts);
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
    public async Task CreateAsync_SavesTableWithDefaultStatus()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new TableService(context);

        await service.CreateAsync(new TableViewModel
        {
            TableNumber = 10,
            CapacityOfTheTable = 4,
            StatusOfTheTable = ""
        });

        var table = await context.Tables.SingleAsync();
        Assert.Equal("Свободна", table.StatusOfTheTable);
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

    [Fact]
    public async Task UpdateAsync_ThrowsWhenNumberExistsOrTableMissing()
    {
        await using var context = TestDataFactory.CreateContext();
        var first = TestDataFactory.CreateTable(number: 1);
        var second = TestDataFactory.CreateTable(number: 2);
        context.Tables.AddRange(first, second);
        await context.SaveChangesAsync();
        var service = new TableService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(new TableViewModel
        {
            TableId = second.TableId,
            TableNumber = 1,
            CapacityOfTheTable = 4
        }));

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.UpdateAsync(new TableViewModel
        {
            TableId = Guid.NewGuid(),
            TableNumber = 3,
            CapacityOfTheTable = 4
        }));
    }

    [Fact]
    public async Task DeleteAsync_RemovesExistingTableAndIgnoresMissingTable()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        var service = new TableService(context);

        await service.DeleteAsync(Guid.NewGuid());
        await service.DeleteAsync(table.TableId);

        Assert.Empty(context.Tables);
    }
}
