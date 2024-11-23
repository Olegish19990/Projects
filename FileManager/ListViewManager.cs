using FileProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class ListViewManager
    {
        //public Scaner scaner { get; set; }
        private ListView listView { get; set; }
        public ListViewManager(ListView newListView)
        {
            
            listView = newListView;
            listView.View = View.Details;
            listView.FullRowSelect = true; 
            listView.GridLines = true;
            listView.Columns.Add("File Name", 200);      
            listView.Columns.Add("Size (KB)", 100);      
            listView.Columns.Add("Extension", 100);     
            listView.Columns.Add("Last Modified", 150);
            listView.Columns.Add("Full path",500);

        }

        public void DisplayFiles(IEnumerable<FileInfo> files)
        {
            listView.Items.Clear();
            foreach (var file in files)
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add((file.Length / 1024).ToString());
                item.SubItems.Add(file.Extension);
                item.SubItems.Add(file.LastWriteTime.ToString());
                item.SubItems.Add(file.FullName);
                item.Text = file.Name;
                item.Tag = file;
                listView.Items.Add(item);
                
            }
        }

        public List<FileInfo> GetSelectedItems()
        {
            var selectedItems = listView.SelectedItems;
        
            List<FileInfo> result = new List<FileInfo>();
            for(int i = 0; i < selectedItems.Count;i++)
            {
                result.Add((FileInfo)selectedItems[i].Tag);
            }
            return result;
        }
    }
}
