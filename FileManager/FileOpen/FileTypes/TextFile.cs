using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileOpen.FileTypes
{
    public class TextFile : IFileViewer
    {
        //FileInfo file { get; set; }
        public void View(Window window, FileInfo file)
        {
          
            window.RichText.Visible = true;
            var text = file.OpenText().ReadToEnd();
            window.RichText.Text = text;
            window.fileWindow.Show();
        }
    }
}
