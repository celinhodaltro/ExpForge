using ExpForge.Application.Commands.Widget;
using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using MediatR;

namespace ExpForge.Application.Handlers.Widget
{
    public class RenameWidgetHandler : IRequestHandler<RenameWidgetCommand, bool>
    {

        public readonly ITerminalMessageService _terminalMessageService;
        public readonly IRenameWidgetService _renameWidgetService;

        public RenameWidgetHandler(ITerminalMessageService terminalMessageService, IRenameWidgetService renameWidgetService)
        {
            _terminalMessageService = terminalMessageService;
            _renameWidgetService = renameWidgetService;
        }

        public Task<bool> Handle(RenameWidgetCommand request, CancellationToken cancellationToken)
        {

            if (_renameWidgetService.Rename(request.WidgetPath, request.NewWidgetName))
            {
                _terminalMessageService.WriteLine($"Widget '{request.NewWidgetName}' renamed successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
