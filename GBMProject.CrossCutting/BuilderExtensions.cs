using FluentValidation;
using GBMProject.Application.Commands.Truck;
using GBMProject.Application.Commands.Validations;
using GBMProject.Core.Interfaces;
using GBMProject.Infrastructure.Persistence;
using GBMProject.Infrastructure.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace GBMProject.CrossCutting;

public static class BuilderExtensions
{
    public static void AddDbConfigurations(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<GbmProjectDbContext>(options
            => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("GBMProject.Infrastructure")));

    public static void AddSwaggerConfigurations(this IServiceCollection services, string baseDirectory, string assemblyName)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(sg =>
        {
            sg.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ACME Logística Rodoviária LTDA",
                Description = "Aplicação de transporte de cargas que permite realizar CRUD de " +
                              "motoristas e caminhões e cadastrar entregas vinculando motoristas e caminhões.",
                Contact = new OpenApiContact
                {
                    Name = "Rodrigo Ferreira",
                    Email = "Rodrigo.ferreiradaslv@gmail.com"
                }
            });

            sg.EnableAnnotations();
            var xmlFile = $"{assemblyName}.xml";
            var xmlPath = Path.Combine(baseDirectory, xmlFile);
            sg.IncludeXmlComments(xmlPath);
        });
    }

    public static void AddFluentValidationConfiguration(this IServiceCollection services)
        => services.AddValidatorsFromAssemblyContaining<UpdateDriverCommandValidator>();

    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
        });
        services.AddScoped<ITruckRepository, TruckRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddMediatR(this IServiceCollection services)
        => services.AddMediatR(m => m.RegisterServicesFromAssembly(typeof(UpdateTruckCommandHandler).Assembly));
}