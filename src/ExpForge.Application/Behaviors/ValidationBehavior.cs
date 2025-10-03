
using ExpForge.Domain.Enums;
using ExpForge.Infrastructure.Services;
using FluentValidation;
using MediatR;

namespace ExpForge.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ITerminalMessageService _terminalMessageService;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ITerminalMessageService terminalMessageService)
        {
            _validators = validators;
            _terminalMessageService = terminalMessageService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)   
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .Select(f => f.ErrorMessage)
                    .ToList();

                if (failures.Count != 0)
                {
                    _terminalMessageService.WriteLine("Validation Errors:");
                    _terminalMessageService.WriteLines(failures, MessageStatus.Error);
                }
            }

            return await next(); 
        }
    }
}
