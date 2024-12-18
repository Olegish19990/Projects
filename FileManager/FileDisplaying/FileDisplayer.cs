using FileManager;
using FileProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileHandling
{
    public class FileDisplayer
    {
        public ListView listView { get; set; }

        public FileDisplayer(ListView NewlistView)
        {
            ImageList icons = new ImageList();

            listView = NewlistView;
            icons.Images.Add("file", Image.FromFile("file.png"));
            listView.View = View.Details;
            listView.SmallImageList = icons;
            listView.FullRowSelect = true;
            listView.GridLines = true;
          
            listView.Columns.Add("File Name", 200);
            listView.Columns.Add("Size (KB)", 100);
            listView.Columns.Add("Extension", 100);
            listView.Columns.Add("Last Modified", 150);
            listView.Columns.Add("Full path", 500);
        }

     
        public void DisplayFiles(IEnumerable<FileInfo> files)
        {
            listView.Items.Clear();
            foreach (var file in files)
            {
                ListViewItem item = new ListViewItem(file.Name)
                {
                    Tag = file
                };
                item.ImageKey = "file"; // Указываем ключ для иконки
                
                item.SubItems.Add((file.Length / 1024).ToString());
                item.SubItems.Add(file.Extension);
                item.SubItems.Add(file.LastWriteTime.ToString());
                item.SubItems.Add(file.FullName);
                listView.Items.Add(item);
            }
        }


     
        public List<FileInfo> GetSelectedItems()
        {
            var selectedItems = listView.SelectedItems;
            List<FileInfo> result = new List<FileInfo>();
            for (int i = 0; i < selectedItems.Count; i++)
            {
                result.Add((FileInfo)selectedItems[i].Tag);
            }
            return result;
        }

    }
}