using ExpForge.Application.Commands.Widget;
using ExpForge.Application.Services.IServices;
using ExpForge.CLI.Services;
using MediatR;
using static ExpForge.Application.Services.Enums.Template;

namespace ExpForge.Application.Handlers.Widget
{
    public class NewWidgetHandler : IRequestHandler<NewWidgetCommand, bool>
    {

        public NewWidgetHandler(ITemplateGeneratorService service)
        {
            _service = service;
        }

        public readonly ITemplateGeneratorService _service;

        public Task<bool> Handle(NewWidgetCommand request, CancellationToken cancellationToken)
        {

            if (_service.Generate(request.WidgetName, request.TemplatePath, request.TemplateName, TemplateType.Widget))
            {
                TerminalMessageService.WriteLine($"Widget '{request.WidgetName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
