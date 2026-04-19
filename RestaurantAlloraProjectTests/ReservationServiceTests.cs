using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Reservation;

namespace RestaurantAlloraProjectTests;

public class ReservationServiceTests
{
    [Fact]
    public async Task GetUserReservationsAsync_ReturnsReservationsForUser()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(number: 4);
        var customerId = Guid.NewGuid();
        context.Tables.Add(table);
        context.Reservations.AddRange(
            CreateReservation(table, customerId, DateTime.UtcNow.AddDays(1), "Одобрена"),
            CreateReservation(table, Guid.NewGuid(), DateTime.UtcNow.AddDays(2), "Одобрена"));
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        var reservation = Assert.Single(await service.GetUserReservationsAsync(customerId));

        Assert.Equal(4, reservation.TableNumber);
        Assert.Equal("Одобрена", reservation.Status);
    }

    [Fact]
    public async Task GetUserReservationsPageAsync_ClampsPageAndReturnsMetadata()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        var customerId = Guid.NewGuid();
        context.Tables.Add(table);
        context.Reservations.AddRange(
            CreateReservation(table, customerId, DateTime.UtcNow.AddDays(1), "Одобрена"),
            CreateReservation(table, customerId, DateTime.UtcNow.AddDays(2), "Очаква одобрение"));
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        var page = await service.GetUserReservationsPageAsync(customerId, page: 10, pageSize: 1);

        Assert.Equal(2, page.TotalReservations);
        Assert.Equal(2, page.CurrentPage);
        Assert.Single(page.Reservations);
    }

    [Fact]
    public async Task GetPendingReservationsAsync_ReturnsOnlyPendingReservations()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        var customerId = Guid.NewGuid();
        var user = TestDataFactory.CreateUser(customerId);
        var profile = new CustomerProfile { UserId = customerId, User = user };
        context.Users.Add(user);
        context.Set<CustomerProfile>().Add(profile);
        context.Tables.Add(table);
        var pending = CreateReservation(table, customerId, DateTime.UtcNow.AddDays(1), "Очаква одобрение");
        pending.Customer = profile;
        var approved = CreateReservation(table, customerId, DateTime.UtcNow.AddDays(2), "Одобрена");
        approved.Customer = profile;
        context.Reservations.AddRange(pending, approved);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        var result = Assert.Single(await service.GetPendingReservationsAsync());

        Assert.Equal("Очаква одобрение", result.Status);
    }

    [Fact]
    public async Task GetReservationsForManagementAsync_AppliesFiltersAndPaging()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(number: 8);
        var customerId = Guid.NewGuid();
        var employeeId = Guid.NewGuid();
        var customerUser = TestDataFactory.CreateUser(customerId, "ivan", "ivan@example.com");
        var employeeUser = TestDataFactory.CreateUser(employeeId, "employee", "employee@example.com");
        var customer = new CustomerProfile { UserId = customerId, User = customerUser };
        var employee = new EmployeeProfile { UserId = employeeId, User = employeeUser };
        var date = DateTime.UtcNow.AddDays(1).Date.AddHours(19);
        var reservation = CreateReservation(table, customerId, date, "Одобрена");
        reservation.Customer = customer;
        reservation.EmployeeId = employeeId;
        reservation.Employee = employee;
        context.Users.AddRange(customerUser, employeeUser);
        context.Set<CustomerProfile>().Add(customer);
        context.Set<EmployeeProfile>().Add(employee);
        context.Tables.Add(table);
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        var result = await service.GetReservationsForManagementAsync(new ReservationFilterViewModel
        {
            Status = "Одобрена",
            Date = date.Date,
            TableNumber = 8,
            SearchTerm = "ivan",
            Page = 5,
            PageSize = 1
        });

        Assert.Equal(1, result.TotalReservations);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal("ivan", Assert.Single(result.Reservations).CustomerName);
    }

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
    public async Task CreateReservationAsync_ValidatesInputAndAvailabilityMessages()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(capacity: 2);
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            NumberOfGuests = 0,
            ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(18)
        }, Guid.NewGuid()));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            NumberOfGuests = 1,
            ReservationDate = DateTime.Now.AddDays(-1)
        }, Guid.NewGuid()));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            NumberOfGuests = 1,
            ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(7)
        }, Guid.NewGuid()));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            NumberOfGuests = 1,
            ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(21).AddMinutes(1)
        }, Guid.NewGuid()));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            TableId = Guid.NewGuid(),
            NumberOfGuests = 1,
            ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(18)
        }, Guid.NewGuid()));

        var tooSmall = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            TableId = table.TableId,
            NumberOfGuests = 5,
            ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(18)
        }, Guid.NewGuid()));
        Assert.Contains("по-голяма маса", tooSmall.Message);
    }

    [Fact]
    public async Task CreateReservationAsync_ReturnsUnavailableMessagesForNoCapacityAndNearestConflict()
    {
        await using var context = TestDataFactory.CreateContext();
        var smallTable = TestDataFactory.CreateTable(number: 1, capacity: 2);
        var occupiedTable = TestDataFactory.CreateTable(number: 2, capacity: 4);
        var date = DateTime.Now.AddDays(1).Date.AddHours(18);
        context.Tables.AddRange(smallTable, occupiedTable);
        context.Reservations.Add(CreateReservation(occupiedTable, Guid.NewGuid(), date, "Одобрена"));
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        var noCapacity = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            NumberOfGuests = 9,
            ReservationDate = date
        }, Guid.NewGuid()));

        var conflict = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateReservationAsync(new ReservationCreateViewModel
        {
            NumberOfGuests = 4,
            ReservationDate = date.AddHours(1)
        }, Guid.NewGuid()));

        Assert.Contains("достатъчен капацитет", noCapacity.Message);
        Assert.Contains("Няма свободна подходяща маса", conflict.Message);
    }

    [Fact]
    public async Task BuildUnavailableReservationMessageAsync_ReturnsFallbackMessage()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Tables.Add(TestDataFactory.CreateTable(capacity: 4));
        await context.SaveChangesAsync();
        var service = new ReservationService(context);
        var method = typeof(ReservationService).GetMethod("BuildUnavailableReservationMessageAsync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;

        var task = (Task<string>)method.Invoke(service, new object[]
        {
            new ReservationCreateViewModel
            {
                NumberOfGuests = 2,
                ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(18)
            }
        })!;
        var message = await task;

        Assert.Equal("Няма свободна маса за избраното време и брой гости.", message);
    }

    [Fact]
    public async Task BuildUnavailableReservationMessageAsync_ReturnsFallbackForSelectedTableWithoutSpecificConflict()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(capacity: 4);
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);
        var method = typeof(ReservationService).GetMethod("BuildUnavailableReservationMessageAsync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!;

        var task = (Task<string>)method.Invoke(service, new object[]
        {
            new ReservationCreateViewModel
            {
                TableId = table.TableId,
                NumberOfGuests = 2,
                ReservationDate = DateTime.Now.AddDays(1).Date.AddHours(18)
            }
        })!;
        var message = await task;

        Assert.Equal("Няма свободна маса за избраното време и брой гости.", message);
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
    public async Task ApproveReservationAsync_IgnoresMissingAndRejectsNonPendingOrConflictingReservation()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        var date = DateTime.Now.AddDays(1).Date.AddHours(18);
        var approved = CreateReservation(table, Guid.NewGuid(), date, "Одобрена");
        var pending = CreateReservation(table, Guid.NewGuid(), date.AddHours(1), "Очаква одобрение");
        context.Tables.Add(table);
        context.Reservations.AddRange(approved, pending);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await service.ApproveReservationAsync(Guid.NewGuid(), Guid.NewGuid());

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ApproveReservationAsync(approved.ReservationId, Guid.NewGuid()));
        await Assert.ThrowsAsync<InvalidOperationException>(() => service.ApproveReservationAsync(pending.ReservationId, Guid.NewGuid()));
    }

    [Fact]
    public async Task RejectReservationAsync_RejectsReservationAndUpdatesTableWhenNoFutureApprovedReservations()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable(status: "Резервирана");
        var reservation = CreateReservation(table, Guid.NewGuid(), DateTime.Now.AddDays(1).Date.AddHours(18), "Одобрена");
        var employeeId = Guid.NewGuid();
        context.Tables.Add(table);
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await service.RejectReservationAsync(reservation.ReservationId, employeeId);

        Assert.Equal("Отказана", reservation.Status);
        Assert.Equal("Свободна", table.StatusOfTheTable);
        Assert.True(await context.Set<EmployeeProfile>().AnyAsync(ep => ep.UserId == employeeId));
    }

    [Fact]
    public async Task RejectReservationAsync_IgnoresMissingAndThrowsWhenAlreadyRejected()
    {
        await using var context = TestDataFactory.CreateContext();
        var table = TestDataFactory.CreateTable();
        var reservation = CreateReservation(table, Guid.NewGuid(), DateTime.Now.AddDays(1), "Отказана");
        context.Tables.Add(table);
        context.Reservations.Add(reservation);
        await context.SaveChangesAsync();
        var service = new ReservationService(context);

        await service.RejectReservationAsync(Guid.NewGuid(), Guid.NewGuid());

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.RejectReservationAsync(reservation.ReservationId, Guid.NewGuid()));
    }

    [Fact]
    public async Task FillTablesAsync_PopulatesTableOptions()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Tables.Add(TestDataFactory.CreateTable(number: 2, capacity: 4));
        await context.SaveChangesAsync();
        var service = new ReservationService(context);
        var model = new ReservationCreateViewModel();

        await service.FillTablesAsync(model);

        var option = Assert.Single(model.Tables);
        Assert.Contains("Маса 2", option.Text);
        Assert.Equal(4, option.Capacity);
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

    private static Reservation CreateReservation(Table table, Guid customerId, DateTime date, string status)
    {
        return new Reservation
        {
            ReservationId = Guid.NewGuid(),
            TableId = table.TableId,
            Table = table,
            CustomerId = customerId,
            ReservationDate = date,
            NumberOfGuests = 2,
            Status = status
        };
    }
}
