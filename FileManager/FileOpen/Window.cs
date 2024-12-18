using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileOpen
{
    public class Window
    {
        public RichTextBox RichText { get; set; }
        public PictureBox PictureBox { get; set; }
        public FileWindow fileWindow { get; set; }
        public object Controls { get; internal set; }
    }
}
