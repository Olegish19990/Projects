using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileOpen.FileTypes
{
    public class PictureFile : IFileViewer
    {
        public void View(Window window, FileInfo file)
        {
            byte[] imageBytes = File.ReadAllBytes(file.FullName);
            Bitmap bitmap = new Bitmap(file.FullName);
            Size pictureSize = new Size(bitmap.Width, bitmap.Height);
            window.PictureBox.Visible = true;
            window.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage; 
            window.PictureBox.Image = bitmap;
            window.fileWindow.ClientSize = new Size(pictureSize.Width + 20, pictureSize.Height + 40); 
            window.fileWindow.Resize += (sender, e) => ResizePictureBox(window, bitmap);
            ResizePictureBox(window, bitmap);
            window.fileWindow.Show();
        }

        private void ResizePictureBox(Window window, Bitmap bitmap)
        {
     
            int newWidth = window.fileWindow.ClientSize.Width - 20; 
            int newHeight = window.fileWindow.ClientSize.Height - 40; 

          
            float aspectRatio = (float)bitmap.Width / bitmap.Height;

            
            if (newWidth / aspectRatio <= newHeight)
            {
                window.PictureBox.Width = newWidth;
                window.PictureBox.Height = (int)(newWidth / aspectRatio);
            }
            else
            {
                window.PictureBox.Height = newHeight;
                window.PictureBox.Width = (int)(newHeight * aspectRatio);
            }


            window.PictureBox.Location = new Point((window.fileWindow.ClientSize.Width - window.PictureBox.Width) / 2,
                                                   (window.fileWindow.ClientSize.Height - window.PictureBox.Height) / 2);
        }
    }
}
