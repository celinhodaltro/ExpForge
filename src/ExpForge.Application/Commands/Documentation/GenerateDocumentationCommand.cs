using MediatR;
using System.Reflection.Metadata;

public record GenerateDocumentationCommand(List<GenerateDocumentationCommand.CommandInfo> Commands) : IRequest
{
    public record CommandInfo(string Title, string Description, List<ParameterInfo> Parameters);
    public record ParameterInfo(string Name, string Type, string Description);


}


public static class DocumentationExtensions
{
    public static string ToHtmlTable(IEnumerable<GenerateDocumentationCommand.ParameterInfo> parameters)
    {
        if (parameters == null || !parameters.Any())
            return string.Empty;

        return $@"
<MudText Typo=""Typo.h6"" Class=""mt-3"">Parameters:</MudText>

<table class=""custom-table"" style=""width:100%; border-collapse: collapse;"">
    <thead>
        <tr style=""background-color:#1976d2; color:white;"">
            <th style=""padding:8px; text-align:left;"">Name</th>
            <th style=""padding:8px; text-align:left;"">Type</th>
            <th style=""padding:8px; text-align:left;"">Description</th>
        </tr>
    </thead>
    <tbody>
        {string.Join("\n", parameters.Select(p => $@"
        <tr style=""border-bottom:1px solid #e0e0e0;"">
            <td style=""padding:8px;"">{p.Name}</td>
            <td style=""padding:8px;"">{p.Type}</td>
            <td style=""padding:8px;"">{p.Description}</td>
        </tr>"))}
    </tbody>
</table>";
    }
}
