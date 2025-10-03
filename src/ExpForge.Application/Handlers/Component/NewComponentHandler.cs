using ExpForge.Application.Commands.Component;
using ExpForge.Application.Commands.Widget;
using ExpForge.Application.Interfaces.Services;
using ExpForge.CLI.Services;
using MediatR;
using static ExpForge.Application.Services.Enums.Template;

namespace ExpForge.Application.Handlers.Component
{
    public class NewComponentHandler : IRequestHandler<NewComponentCommand, bool>
    {

        public NewComponentHandler(ITemplateGeneratorService service)
        {
            _service = service;
        }

        public readonly ITemplateGeneratorService _service;

        public Task<bool> Handle(NewComponentCommand request, CancellationToken cancellationToken)
        {

            if (_service.Generate(request.ComponentName, request.TemplatePath, "component", TemplateType.Component))
            {
                TerminalMessageService.WriteLine($"Component '{request.ComponentName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
