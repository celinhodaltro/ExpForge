using MediatR;


namespace ExpForge.Application.Commands.Component
{
    public class NewComponentCommand : IRequest<bool>
    {
        public NewComponentCommand(string componentName, string? destinationPath = null, string templatePath = null)
        {
            ComponentName = componentName;
            DestinationPath = destinationPath;
            TemplatePath = templatePath;
        }

        public string ComponentName { get; set; }
        public string? DestinationPath { get; set; }
        public string TemplatePath { get; set; }
    }
}
