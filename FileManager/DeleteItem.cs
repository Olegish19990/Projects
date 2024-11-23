using FileProcessor.Removing;
using FileProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileProcessor.Reporting;

namespace FileManager
{
    public class DeleteItem
    {
        public Report<FileSystemInfo> Delete(RemoverMode mode, List<FileInfo> files)
        {
         
            Remover remover = new Remover(new FSObjectContainer());
            if (mode == RemoverMode.Files)
            {
                remover.Container.Files = files;
            }
            else if(mode == RemoverMode.Directories)
            {
                remover.Container.Dirs.Add(CurrentDirectory.CurrentDir);
            }
            return remover.Execute(mode);
        }

    }
}
