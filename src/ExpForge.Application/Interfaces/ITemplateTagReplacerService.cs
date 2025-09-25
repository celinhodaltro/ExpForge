using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace experience_widget.Interfaces
{
    public interface ITemplateTagReplacerService
    {
       void ReplaceTagsInFile(string sourceFile, string destinationFile);
       string ReplaceTags(string content);
    }
}
