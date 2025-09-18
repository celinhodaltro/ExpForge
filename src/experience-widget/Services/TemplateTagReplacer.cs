using experience_widget.Interfaces;


namespace ExperienceWidgetCli.Services
{
    public class TemplateTagReplacer : ITemplateTagReplacer
    {
        private readonly Dictionary<string, string> _tags;

        public TemplateTagReplacer(Dictionary<string, string> tags)
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
        /// Substitui tags dentro de um arquivo e salva no destino.
        /// </summary>
        public void ReplaceTagsInFile(string sourceFile, string destinationFile)
        {
            var content = File.ReadAllText(sourceFile);
            content = ReplaceTags(content);

            var destDir = Path.GetDirectoryName(destinationFile);
            if (!string.IsNullOrEmpty(destDir) && !Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            File.WriteAllText(destinationFile, content);
        }
    }
}
