namespace ExpForge.Application.Services.IServices
{
    interface ITemplateCopierService
    {
        /// <summary>
        /// Copia todos os arquivos e pastas de sourcePath para destinationPath,
        /// mantendo a estrutura de pastas e removendo ".template" do nome dos arquivos.
        /// </summary>
        /// <param name="sourcePath">Caminho da pasta de templates</param>
        /// <param name="destinationPath">Caminho da pasta de destino</param>
        public void Copy(string sourcePath, string destinationPath);
    }
}
