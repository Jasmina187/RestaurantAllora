using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Reservation;

namespace RestaurantAlloraProjectTests;

public class ReservationServiceTests
{
    [Fact]
    public async Task CreateReservationAsync_UsesAvailableTableAndCreatesCustomerProfile()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(capacity: 4);
        var customerId = Guid.NewGuid();
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await service.CreateReservationAsync(new ReservationCreateViewModel
        {
            TableId = table.TableId,
            NumberOfGuests = 2,
            ReservationDate = DateTime.Now.AddDays(2).Date.AddHours(19)
        }, customerId);

        var reservation = await context.Reservations.SingleAsync();
        Assert.Equal(table.TableId, reservation.TableId);
        Assert.Equal("Очаква одобрение", reservation.Status);
        Assert.True(await context.Set<CustomerProfile>().AnyAsync(cp => cp.UserId == customerId));
    }

    [Fact]
    public async Task ApproveReservationAsync_SetsReservationAndTableStatus()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        var reservation = new Reservation
        {
            ReservationId = Guid.NewGuid(),
            TableId = table.TableId,
            Table = table,
            CustomerId = Guid.NewGuid(),
            ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(18),
            NumberOfGuests = 2,
            Status = "Очаква одобрение"
        };
        var employeeId = Guid.NewGuid();
        context.Tables.Add(table);
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await service.ApproveReservationAsync(reservation.ReservationId, employeeId);

        Assert.Equal("Одобрена", reservation.Status);
        Assert.Equal(employeeId, reservation.EmployeeId);
        Assert.Equal("Резервирана", table.StatusOfTheTable);
        Assert.True(await context.Set<EmployeeProfile>().AnyAsync(ep => ep.UserId == employeeId));
    }

    [Fact]
    public async Task CreateReservationAsync_ThrowsWhenSelectedTableIsOccupied()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        var date = DateTime.Now.AddDays(1).Date.AddHours(18);
        context.Tables.Add(table);
        context.Reservations.Add(new Reservation
        {
            ReservationId = Guid.NewGuid(),
            TableId = table.TableId,
            Table = table,
            CustomerId = Guid.NewGuid(),
            ReservationDate = date,
            NumberOfGuests = 2,
            Status = "Одобрена"
        });
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            TableId = table.TableId,
            NumberOfGuests = 2,
            ReservationDate = date.AddHours(1)
        }, Guid.NewGuid()));
    }
}
