using ExpForge.Application.Commands.Widget;
using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using MediatR;
using System.Xml.Linq;

namespace ExpForge.Application.Handlers.Widget
{
    public class NewWidgetHandler : IRequestHandler<NewWidgetCommand, bool>
    {

        public NewWidgetHandler(ITemplateGeneratorService templateGeneratorService, ITerminalMessageService terminalMessageService)
        {
            _templateGeneratorService = templateGeneratorService;
            _terminalMessageService = terminalMessageService;
        }

        public readonly ITemplateGeneratorService _templateGeneratorService;
        public readonly ITerminalMessageService _terminalMessageService;

        public Task<bool> Handle(NewWidgetCommand request, CancellationToken cancellationToken)
        {

            var tags = new Dictionary<TemplateTag, string>
            {
                { TemplateTag.WIDGETNAME, request.WidgetName},
                { TemplateTag.AUTHOR, "Your Organization/Name" },
                { TemplateTag.DATE  , DateTime.UtcNow.ToString("yyyy-MM-dd") }
            };

            if (_templateGeneratorService.Generate(request.WidgetName, request.TemplatePath, request.TemplateName, TemplateType.Widget, tags))
            {
                _terminalMessageService.WriteLine($"Widget '{request.WidgetName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
