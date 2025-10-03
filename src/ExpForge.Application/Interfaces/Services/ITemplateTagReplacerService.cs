namespace ExpForge.Application.Interfaces.Services
{
    public interface ITemplateTagReplacerService
    {
       void ReplaceTagsInFile(string sourceFile, string destinationFile);
       string ReplaceTags(string content);
    }
}
