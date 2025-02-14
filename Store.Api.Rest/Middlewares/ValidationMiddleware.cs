

using FluentValidation;

namespace Store.Api.Rest.Middlewares
{
    public class ValidationMiddleware<TRequest> : IMiddleware
    {
        private readonly IEnumerable<IValidator<TRequest>> validator;
        private readonly TRequest request;

        public ValidationMiddleware(IEnumerable<IValidator<TRequest>> validator, TRequest request)
        {
            this.validator = validator;
            this.request = request;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if(validator.Any() && request is WeatherForecast)
            {
                var _context = new ValidationContext<TRequest>(request);

                var validationResult = await Task.WhenAll(
                validator.Select(v => 
                v.ValidateAsync(_context)));

                var failures = validationResult.Where(v => v.Errors.Any()).
                    SelectMany(r => r.Errors).ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }
            }

            await next(context); 
        }
    }
}
