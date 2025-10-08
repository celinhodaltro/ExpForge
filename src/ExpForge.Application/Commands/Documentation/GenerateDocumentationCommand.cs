using MediatR;

public record GenerateDocumentationCommand(List<GenerateDocumentationCommand.CommandInfo> Commands) : IRequest
{
    public record CommandInfo(string Title, string Description, List<ParameterInfo> Parameters);
    public record ParameterInfo(string Name, string Type, string Description);
}
