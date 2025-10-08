using ExpForge.Application.Commands.Component;
using ExpForge.Application.Interfaces.Providers;
using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;
using ExpForge.Domain.Extensions;
using MediatR;

namespace ExpForge.Application.Handlers.Component
{
    public class GenerateDocumentationHandler : IRequestHandler<GenerateDocumentationCommand>
    {
        private readonly ITemplateGeneratorService _templateGeneratorService;
        private readonly ITerminalMessageService _terminalMessageService;
        private readonly ITemplatePathProvider _templatePathProvider;

        public GenerateDocumentationHandler(
            ITemplatePathProvider templatePathProvider,
            ITemplateGeneratorService templateGeneratorService,
            ITerminalMessageService terminalMessageService)
        {
            _templateGeneratorService = templateGeneratorService;
            _terminalMessageService = terminalMessageService;
            _templatePathProvider = templatePathProvider;
        }



        public Task Handle(GenerateDocumentationCommand request, CancellationToken cancellationToken)
        {
            var templateRoot = _templatePathProvider.GetTemplatesPath();
            var templateName = "empty";
            var templateType = TemplateType.ComandDocumentation_InBlazor;

            foreach (var cmd in request.Commands)
            {
                var outputName = cmd.Title.Replace(" ", "_");

                // Monta uma tabela HTML simples com os parâmetros
                var parametersTable = string.Join("",
                    cmd.Parameters?.Any() == true
                        ? new[]
                        {
                            @"
          <MudText Typo=""Typo.h6"" Class=""mt-3"">Parameters:</MudText>
          
          <MudTable Dense=""true"" Hover=""true"">
              <HeaderContent>
                  <MudTh>Name</MudTh>
                  <MudTh>Type</MudTh>
                  <MudTh>Description</MudTh>
              </HeaderContent>
              <RowTemplate> 
                  " + string.Join("\n", cmd.Parameters.Select(p =>
                      $"\n" +
                      $"                  <MudTr>\n" +
                      $"                    <MudTd>{p.Name}</MudTd>\n" +
                      $"                    <MudTd>{p.Type}</MudTd>\n" +
                      $"                    <MudTd>{p.Description}</MudTd>\n" +
                      $"                  </MudTr>"
                  )) + @"
              </RowTemplate>
          </MudTable>"
                        }
                        : Array.Empty<string>()
                );

                var tags = new Dictionary<TemplateTag, string>
                {
                    { TemplateTag.NAME, outputName },
                    { TemplateTag.AUTHOR, "Your Organization/Name" },
                    { TemplateTag.DATE, DateTime.UtcNow.ToString("yyyy-MM-dd") },
                    { TemplateTag.DESCRIPTION, cmd.Description },
                    { TemplateTag.WIDGETNAME, cmd.Title },
                    { TemplateTag.PARAMETERS, parametersTable }
                };

                if(_templateGeneratorService.Generate(
                    outputName,
                    templateRoot,
                    templateName,
                    templateType,
                    tags,
                    useUnifiedFolder: true
                ))
                {
                    _terminalMessageService.WriteLine(
                        $"Documentation for '{cmd.Title}' generated!",
                        MessageStatus.Success
                    );
                }
                else
                {
                    return Task.CompletedTask;
                }

            }
            string commandsPath = Path.Combine(Directory.GetCurrentDirectory(), "Commands");
            FileExtension.FlattenFolder(commandsPath);
            return Task.CompletedTask;
        }

    }
}
