namespace experience_widget.Interfaces
{
    public interface ITemplateTagReplacerService
    {
       void ReplaceTagsInFile(string sourceFile, string destinationFile);
       string ReplaceTags(string content);
    }
}
