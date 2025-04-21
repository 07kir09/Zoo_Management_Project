using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Zoo_Management_Project.Zoo.Application.Interfaces;
using Zoo_Management_Project.Zoo.Application.Services;
using Zoo_Management_Project.Zoo.Infrastructure.Events;

namespace Zoo_Management_Project;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
        builder.Services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
        builder.Services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();
        builder.Services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        builder.Services.AddScoped<AnimalTransferService>();
        builder.Services.AddScoped<FeedingOrganizationService>();
        builder.Services.AddScoped<ZooStatisticsService>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Zoo API", Version = "v1" });
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoo API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}