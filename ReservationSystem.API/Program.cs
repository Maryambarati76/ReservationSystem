using Microsoft.EntityFrameworkCore;
using ReservationSystem.Infrastructure.Persistence;
using ReservationSystem.Core.Interfaces;
using ReservationSystem.Infrastructure.Repositories;
using ReservationSystem.Core.Services;
using ReservationSystem.Infrastructure.Services;
using FluentValidation;
using ReservationSystem.Core.Validators;
using ReservationSystem.API.Middleware;
using ReservationSystem.Infrastructure.Mappings;
using AutoMapper;
using ReservationSystem.Infrastructure.Mappings;

namespace ReservationSystem.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IReservationService, ReservationService>();
        //var mapperConfig = new MapperConfiguration(cfg =>
        //{
        //    cfg.AddProfile<MappingProfile>();
        //},null);

        //builder.Services.AddSingleton(mapperConfig.CreateMapper());

        builder.Services.AddValidatorsFromAssemblyContaining<ReservationCreateDtoValidator>();

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}