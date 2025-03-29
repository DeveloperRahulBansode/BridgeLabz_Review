using BusinessLayer.Interface;
using BusinessLayer.Service;
using DataAcessLayer.Context;
using DataAcessLayer.Interface;
using DataAcessLayer.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//DB Connection
builder.Services.AddDbContext<UserDbContext>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));


//dependancy injection

builder.Services.AddScoped<IUserDataService, UserDataService>();
builder.Services.AddScoped<IUserService,UserService>();




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
}

app.UseAuthorization();

app.MapControllers();

app.Run();
