using ExpForge.Application.Commands.Component;
using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using MediatR;
using System.Xml.Linq;

namespace ExpForge.Application.Handlers.Component
{
    public class NewComponentHandler : IRequestHandler<NewComponentCommand, bool>
    {

        public NewComponentHandler(ITemplateGeneratorService templateGeneratorService, ITerminalMessageService terminalMessageService)
        {
            _templateGeneratorService = templateGeneratorService;
            _terminalMessageService = terminalMessageService;
        }

        public readonly ITemplateGeneratorService _templateGeneratorService;
        public readonly ITerminalMessageService _terminalMessageService;

        public Task<bool> Handle(NewComponentCommand request, CancellationToken cancellationToken)
        {
            var tags = new Dictionary<TemplateTag, string>
            {
                { TemplateTag.NAME      , request.ComponentName },
                { TemplateTag.AUTHOR, "Your Organization/Name" },
                { TemplateTag.DATE  , DateTime.UtcNow.ToString("yyyy-MM-dd") }
            };

            if (_templateGeneratorService.Generate(request.ComponentName, request.TemplatePath, "empty", TemplateType.Component, tags))
            {
                _terminalMessageService.WriteLine($"Component '{request.ComponentName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
