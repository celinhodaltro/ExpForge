using ExpForge.Application.Interfaces.Services;
using ExpForge.Domain.Enums;

namespace ExpForge.Infrastructure.Services
{
    public class TemplateTagReplacerService : ITemplateTagReplacerService
    {
        private readonly Dictionary<TemplateTag, string> _tags;

        public TemplateTagReplacerService(Dictionary<TemplateTag, string> tags)
        {
            _tags = tags ?? throw new ArgumentNullException(nameof(tags));
        }

        /// <summary>
        /// Substitui todas as tags no conteúdo informado.
        /// </summary>
        public string ReplaceTags(string content)
        {
            if (string.IsNullOrEmpty(content)) return content;

            foreach (var kv in _tags)
            {
                content = content.Replace($"{{{{{kv.Key}}}}}", kv.Value);
            }

            return content;
        }

        /// <summary>
        /// Substitui tags dentro de um arquivo e no nome do arquivo, salvando no destino.
        /// </summary>
        public void ReplaceTagsInFile(string sourceFile, string destinationFile)
        {
            var content = File.ReadAllText(sourceFile);
            content = ReplaceTags(content);

            // Aplica tags também no nome do arquivo
            var directory = Path.GetDirectoryName(destinationFile) ?? "";
            var fileName = Path.GetFileName(destinationFile);
            fileName = ReplaceTags(fileName); // substitui tags no nome

            var finalPath = Path.Combine(directory, fileName);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(finalPath, content);
        }

    }
}
