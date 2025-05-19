namespace RentalApp.WebAPI.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin() // Разрешить запросы с любых доменов
                    .AllowAnyMethod() // Разрешить все HTTP-методы (GET, POST, etc.)
                    .AllowAnyHeader()); // Разрешить все заголовки
            });
        }
    }
}
