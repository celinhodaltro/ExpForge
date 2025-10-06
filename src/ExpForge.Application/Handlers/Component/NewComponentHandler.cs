using ExpForge.Application.Commands.Component;
using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using MediatR;

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

            if (_templateGeneratorService.Generate(request.ComponentName, request.TemplatePath, "empty", TemplateType.Component))
            {
                _terminalMessageService.WriteLine($"Component '{request.ComponentName}' created successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
