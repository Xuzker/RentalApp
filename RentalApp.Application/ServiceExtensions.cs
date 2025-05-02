using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RentalApp.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            // Автоматически находит и регистрирует все профили маппинга
            // (классы, унаследованные от Profile) в текущей сборке
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Регистрирует все обработчики команд/запросов
            // (классы, реализующие IRequestHandler<TRequest, TResponse>) из текущей сборки
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Находит и регистрирует все валидаторы
            // (классы, унаследованные от AbstractValidator<T>) в текущей сборке
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Добавляет поведение для валидации в конвейер MediatR.
            // Это означает, что перед выполнением любого обработчика(IRequestHandler)
            // сначала будет вызван ValidationBehavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
