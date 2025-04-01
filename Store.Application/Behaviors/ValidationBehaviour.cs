
namespace Store.Application.Behaviors
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResult = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

                var failures = validationResult.Where(x => x.Errors.Any()).SelectMany(y => y.Errors).ToList();

                if (failures.Any())
                    throw new ValidationException(failures);
            }

            return await next();

        }

    }
}
