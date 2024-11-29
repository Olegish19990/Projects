using FileManager.FileOpen.FileTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager.FileOpen
{
    public partial class FileWindow : Form
    {
        
        Window window { get; set; }
        FileTypeAnalyzer analyzer = new FileTypeAnalyzer();
        public FileWindow(FileInfo file)
        {
            InitializeComponent();
            WindowInitilize();
            FileOpen(file);
           
        }

        private void WindowInitilize()
        {
            window = new Window();
            window.PictureBox = pictureBox1;
            window.RichText = richTextBox1;
            window.fileWindow = this;
        }

        private void FileOpen(FileInfo file) 
        {
            var typeFile = analyzer.Analyze(file);
            typeFile.View(window, file);
           
        }

       
    }
}
