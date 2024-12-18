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
using FileManager.FileHandling;

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

        public void UpdateUIAfterDeletion(RemoverMode mode, FileDisplayer fileDisplayer, System.Windows.Forms.TreeView treeView1)
        {
            if (mode == RemoverMode.Files)
            {
                fileDisplayer.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());
            }
            else
            {
                treeView1.SelectedNode.Remove();
            }
        }


    }
}



