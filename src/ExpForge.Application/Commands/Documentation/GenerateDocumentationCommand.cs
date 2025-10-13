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
<MudText Typo=""Typo.h6"" Class=""mt-3 mb-2"" Align=""Align.Center"">Parameters:</MudText>

<table class=""custom-table"" style=""width:100%; border-collapse: collapse; font-family: Arial, sans-serif; font-size:14px; text-align:center;"">
    <thead>
        <tr style=""background-color:#1976d2; color:white;"">
            <th style=""padding:10px; border-bottom:2px solid #115293; text-align:center;"">Name</th>
            <th style=""padding:10px; border-bottom:2px solid #115293; text-align:center;"">Type</th>
            <th style=""padding:10px; border-bottom:2px solid #115293; text-align:center;"">Description</th>
        </tr>
    </thead>
    <tbody>
        {string.Join("\n", parameters.Select((p, index) => $@"
        <tr style=""background-color:{(index % 2 == 0 ? "#f5f5f5" : "white")}; border-bottom:1px solid #e0e0e0;"">
            <td style=""padding:8px; text-align:center;"">{p.Name}</td>
            <td style=""padding:8px; text-align:center;"">{p.Type}</td>
            <td style=""padding:8px; text-align:center;"">{p.Description}</td>
        </tr>"))}
    </tbody>
</table>";
    }
}


