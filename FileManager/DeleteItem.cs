using FileProcessor.Removing;
using FileProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileProcessor.Reporting;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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


        public bool ConfirmDelete()
        {
            string message = "Do you want to delete the selected items?";
            var result = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo);
            return result == DialogResult.Yes;
        }

        public void UpdateUIAfterDeletion(RemoverMode mode,ListViewManager listViewManager, System.Windows.Forms.TreeView treeView1)
        {
            if (mode == RemoverMode.Files)
            {
                listViewManager.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());
            }
            else
            {
                treeView1.SelectedNode.Remove();
            }
        }


    }
}



/*private void Delete_click(RemoverMode mode)
{

    var files = listViewManager.GetSelectedItems();
    string message = "Do you want to delete the selected items?";
    if (DirectoryIsAcces.CheckAccess(CurrentDirectory.CurrentDir))
    {
        var result = MessageBox.Show(message, "Confirm Delete", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes)
        {
            var report = deleteItem.Delete(mode, files);
            if (mode == RemoverMode.Files)
            {
                listViewManager.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());


            }
            else
            {
                treeView1.SelectedNode.Remove();
            }
        }
    }
    else
    {
        MessageBox.Show("Access denied");
    }
}*/