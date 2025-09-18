using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace experience_widget.Interfaces
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
