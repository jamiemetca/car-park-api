using car_park_api.Persistance;
using car_park_api.Persistance.Repositories;
using car_park_api.Service;
using car_park_api.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Repos
builder.Services.AddSingleton<CarParkApiContext, CarParkApiContext>();

builder.Services.AddScoped<IReservationsRepository,ReservationsRepository>();
builder.Services.AddScoped<ICarParksRepository, CarParksRepository>();
builder.Services.AddScoped<IPricingSchedulesRepository, PricingSchedulesRepository>();

// Services
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<ICarParkService, CarParkService>();

// Mapping
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Ensure db is seeded
    CarParkApiContext context = app.Services.GetRequiredService<CarParkApiContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
