namespace ExpForge.Application.Services.IServices
{
    public interface ITemplateTagReplacerService
    {
       void ReplaceTagsInFile(string sourceFile, string destinationFile);
       string ReplaceTags(string content);
    }
}
