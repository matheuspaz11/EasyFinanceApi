using EasyFinanceApi.Context;
using EasyFinanceApi.Helpers.Util;
using EasyFinanceApi.Repository;
using EasyFinanceApi.Repository.Interfaces;
using EasyFinanceApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IObjectiveRepository, ObjectiveRepository>();
builder.Services.AddScoped<ExpenseService>();
builder.Services.AddScoped<ObjectiveService>();

builder.Services.AddDbContext<EasyFinanceContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("EasyFinanceWeb", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.WebHost.ConfigureServices(services =>
{
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    services.AddSingleton(new ConfigHelper(configuration));

    services.AddControllers();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("EasyFinanceWeb");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();