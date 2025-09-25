namespace ExpForgeCli.Services
{
    public class TemplateCopierService
    {
        private readonly TemplateTagReplacerService _replacer;

        public TemplateCopierService(Dictionary<string, string> tags)
        {
            _replacer = new TemplateTagReplacerService(tags);
        }

        /// <summary>
        /// Copia todos os arquivos e pastas de sourcePath para destinationPath,
        /// mantendo a estrutura de pastas, substituindo tags e removendo ".template" do nome dos arquivos.
        /// </summary>
        /// <param name="sourcePath">Caminho da pasta de templates</param>
        /// <param name="destinationPath">Caminho da pasta de destino</param>
        public void Copy(string sourcePath, string destinationPath)
        {
            if (!Directory.Exists(sourcePath))
                throw new DirectoryNotFoundException($"A pasta de origem '{sourcePath}' não existe.");

            // Cria a pasta de destino se não existir
            Directory.CreateDirectory(destinationPath);

            // Copia arquivos na raiz da pasta
            foreach (var file in Directory.GetFiles(sourcePath))
            {
                var fileName = Path.GetFileName(file).Replace(".template", "");
                var destFile = Path.Combine(destinationPath, fileName);

                _replacer.ReplaceTagsInFile(file, destFile);
            }

            foreach (var dir in Directory.GetDirectories(sourcePath))
            {
                var subDirName = Path.GetFileName(dir);
                var destSubDir = Path.Combine(destinationPath, subDirName);
                Copy(dir, destSubDir);
            }
        }
    }
}
