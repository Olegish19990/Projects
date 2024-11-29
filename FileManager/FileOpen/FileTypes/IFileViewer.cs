using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileOpen.FileTypes
{
    public interface IFileViewer
    {
        public void View(Window window, FileInfo file);
    }
}
