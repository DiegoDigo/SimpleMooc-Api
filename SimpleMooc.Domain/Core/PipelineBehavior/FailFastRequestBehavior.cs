using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleMooc.Shared.Entities;

namespace SimpleMooc.Domain.Core.PipelineBehavior
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : BaseResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<FailFastRequestBehavior<TRequest, TResponse>> _logger;

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators,
            ILogger<FailFastRequestBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogDebug($"Validando o {typeof(TRequest).Name}");
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            return failures.Any() ? await Errors(failures) : await next();
        }

        private static Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            var fail = failures.GroupBy(x => x.PropertyName)
                .ToDictionary(p => p.Key,
                    p => p.Select(x => x.ErrorMessage).ToList());
            var response = new BaseResponse(false, "errors", fail);
            return Task.FromResult(response as TResponse);
        }
    }
}