using ExpForge.Application.Commands;
using ExpForge.CLI.Services;
using ExpForgeCli.Services;
using MediatR;

namespace ExpForge.Application.Handlers
{
    public class CreateWidgetHandler : IRequestHandler<CreateWidgetCommand, bool>
    {

        public CreateWidgetHandler(ICreateWidgetService service)
        {
            _service = service;
        }

        public readonly ICreateWidgetService _service;

        public Task<bool> Handle(CreateWidgetCommand request, CancellationToken cancellationToken)
        {
            string templatesPath;

            if (_service.Generate(request.WidgetName, request.TemplatePath, request.TemplateName))
            {
                TerminalMessageService.WriteLine($"Widget '{request.WidgetName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
