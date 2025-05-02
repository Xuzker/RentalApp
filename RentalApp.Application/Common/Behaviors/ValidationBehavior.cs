using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalApp.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                // Асинхронно запускает все валидаторы для запроса
                // Task.WhenAll оптимизирует производительность
                var context = new ValidationContext<TRequest>(request);

                // Преобразует каждый валидатор в Task<ValidationResult>
                // ValidateAsync - асинхронный метод проверки из FluentValidation
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    // "Разворачивает" все ошибки из всех ValidationResult в одну плоскую коллекцию
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }

            // Если валидация успешна — передаёт запрос дальше по конвейеру
            return await next();
        }
    }
}
