using RentalApp.Application;
using RentalApp.Infrastructure;
using RentalApp.Infrastructure.Context;
using RentalApp.WebAPI.Extensions;

namespace RentalApp.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureInfrastructure(builder.Configuration);

            builder.Services.ConfigureApplication();

            // Добавляем кастомную настройку поведения API
            builder.Services.ConfigureApiBehavior();

            // Добавляем политику CORS
            builder.Services.ConfigureCorsPolicy();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            var serviceScope = app.Services.CreateScope();
            var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
            dataContext?.Database.EnsureCreated();

            // Configure the HTTP request pipeline.
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
}
