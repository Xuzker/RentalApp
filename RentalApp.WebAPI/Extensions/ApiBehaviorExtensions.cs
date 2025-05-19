using Microsoft.AspNetCore.Mvc;

namespace RentalApp.WebAPI.Extensions
{
    /// <summary>
    /// Это класс в ASP.NET Core, который управляет поведением API-контроллеров, 
    /// включая обработку ошибок валидации модели.
    /// </summary>
    public static class ApiBehaviorExtensions
    {
        public static void ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                /// По умолчанию (false), если модель (DTO) не проходит валидацию 
                /// (например, из-за [Required] или [MaxLength]), 
                /// ASP.NET Core автоматически возвращает HTTP 400 Bad Request 
                /// с ошибками валидации.
                /// Если установить в true, автоматическая валидация отключается, 
                /// и вы должны вручную проверять ModelState.IsValid в контроллерах
                options.SuppressModelStateInvalidFilter = true;
            });
        }
    }
}
