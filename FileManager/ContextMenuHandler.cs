using FileManager.FileHandling;
using FileManager.ItemCreate;
using FileManager.ItemRenamer;
using FileProcessor.Removing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public class ContextMenuHandler
    {
        
        private FileDisplayer _fileDisplayer { get; set; }
        private TreeView _treeView { get; set; }
        private ContextMenuStrip _contextMenuStripFiles { get; set; }
        private ContextMenuStrip _contextMenuStripDirs { get; set; }
        public ContextMenuHandler(ContextMenuStrip contextMenuStripFiles, ContextMenuStrip contextMenuStripDirs, FileDisplayer fileDisplayer, TreeView treeView) 
        {
            _fileDisplayer = fileDisplayer;
            _treeView = treeView;
            
            _contextMenuStripFiles = contextMenuStripFiles;
            _contextMenuStripDirs = contextMenuStripDirs;

            ToolStripMenuItem deleteFiles = new ToolStripMenuItem("Delete Files", null, (sender, e) => Delete_click(RemoverMode.Files));
            ToolStripMenuItem deleteFolders = new ToolStripMenuItem("Delete Folders", null, (sender, e) => Delete_click(RemoverMode.Directories));

            ToolStripMenuItem createFile = new ToolStripMenuItem("Create File", null, (sender, e) => Create(CreateMode.File));
            ToolStripMenuItem createDirectory = new ToolStripMenuItem("Create Directory", null, (sender, e) => Create(CreateMode.Directory));

            ToolStripMenuItem renameFiles = new ToolStripMenuItem("Rename Files", null, (sender, e) => Rename(CreateMode.File));
            _contextMenuStripFiles.Items.Add(deleteFiles);
            _contextMenuStripFiles.Items.Add(createFile);
            _contextMenuStripFiles.Items.Add(renameFiles);

            _contextMenuStripDirs.Items.Add(deleteFolders);
            _contextMenuStripDirs.Items.Add(createDirectory);

        }


        private void Rename(CreateMode mode)
        {
            FileRenamer fileRenamer = new FileRenamer(_fileDisplayer.GetSelectedItems());
            fileRenamer.ShowDialog();
           
            _fileDisplayer.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());
            _fileDisplayer.listView.Refresh();
        }

        private void Create(CreateMode mode)
        {
            if (DirectoryIsAcces.CheckAccess(CurrentDirectory.CurrentDir))
            {

                if (mode == CreateMode.Directory)
                {
                    DirectoryCreate directoryCreate = new DirectoryCreate(_treeView.SelectedNode);
                    directoryCreate.ShowDialog();
                }
                else if (mode == CreateMode.File)
                {
                    FileCreate fileCreate = new FileCreate();
                    fileCreate.ShowDialog();
                    _fileDisplayer.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());
                }
            }
            else
            {
                MessageBox.Show("Access denied");
            }
        }
        private void Delete_click(RemoverMode mode)
        {
            DeleteItem deleteItem = new DeleteItem();
            var files = _fileDisplayer.GetSelectedItems();
            string message = "Do you want to delete the selected items?";
            if (DirectoryIsAcces.CheckAccess(CurrentDirectory.CurrentDir))
            {
                if (deleteItem.ConfirmDelete())
                {
                    var report = deleteItem.Delete(mode, files);
                    deleteItem.UpdateUIAfterDeletion(mode, _fileDisplayer, _treeView);

                }
            }
            else
            {
                MessageBox.Show("Access denied");
            }
        }






    }
}
