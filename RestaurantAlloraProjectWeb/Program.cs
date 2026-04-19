using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectWeb.Contracts;
using RestaurantAlloraProjectWeb.Helpers;
using RestaurantAlloraProjectWeb.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RestaurantAlloraProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/LogIn";
    options.AccessDeniedPath = "/User/AccessDenied";
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<RestaurantAlloraProjectContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection(nameof(CloudinarySettings)));
builder.Services.AddSingleton(serviceProvider =>
{
    var settings = serviceProvider
        .GetRequiredService<Microsoft.Extensions.Options.IOptions<CloudinarySettings>>()
        .Value;

    return new Cloudinary(new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret));
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ICustomerFavoriteService, CustomerFavoriteService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IAllergenService, AllergenService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReviewService, ReviewService>();



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RestaurantAlloraProjectContext>();
    await dbContext.Database.MigrateAsync();
    await DataSeeder.SeedRolesAsync(scope.ServiceProvider);
    await DataSeeder.SeedAdminAsync(scope.ServiceProvider);
    await MenuDataSeeder.SeedHappyMenuAsync(scope.ServiceProvider);
}
;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusCodePage", "?code={0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
