using ExpForge.Application.Commands;
using ExpForge.CLI.Services;
using ExpForgeCli.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpForge.Application.Handlers
{
    public class RenameWidgetHandler : IRequestHandler<RenameWidgetCommand, bool>
    {
        public Task<bool> Handle(RenameWidgetCommand request, CancellationToken cancellationToken)
        {
            var renameService = new RenameWidgetService();

            if (renameService.Rename(request.WidgetPath, request.NewWidgetName))
            {
                TerminalMessageService.WriteLine($"Widget '{request.NewWidgetName}' renamed successfully!", MessageStatus.Success);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
