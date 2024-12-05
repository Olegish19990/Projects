using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileOpen.FileTypes
{
    public class UnknowableFileType : IFileViewer
    {
        public void View(Window window, FileInfo file)
        {
            MessageBox.Show($"{file.Name} has an unknown type");
           
        }
    }
}
