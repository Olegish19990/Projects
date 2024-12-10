using FileManager.FileHandling;
using FileManager.ItemCreate;
using FileManager.ItemRenamer;
using FileManager.nodeController;
using FileProcessor.Removing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FileManager.ContextMenuController
{
    public class ContextMenuHandler
    {
        private IContextMenuHandler fileHandler;
        private IContextMenuHandler folderHandler;

        public ContextMenuHandler(ContextMenuStrip contextMenuStripFiles, ContextMenuStrip contextMenuStripDirs, FileDisplayer fileDisplayer, TreeView treeView)
        {
            fileHandler = new FileContextMenuHandler(fileDisplayer);
            folderHandler = new FolderContextMenuHandler(treeView);

            contextMenuStripFiles.Items.Add(new ToolStripMenuItem("Create File", null, (sender, e) => fileHandler.Create()));
            contextMenuStripFiles.Items.Add(new ToolStripMenuItem("Delete File", null, (sender, e) => fileHandler.Delete()));
            contextMenuStripFiles.Items.Add(new ToolStripMenuItem("Rename File", null, (sender, e) => fileHandler.Rename()));

            contextMenuStripDirs.Items.Add(new ToolStripMenuItem("Create Folder", null, (sender, e) => folderHandler.Create()));
            contextMenuStripDirs.Items.Add(new ToolStripMenuItem("Delete Folder", null, (sender, e) => folderHandler.Delete()));
            contextMenuStripDirs.Items.Add(new ToolStripMenuItem("Rename Folder", null, (sender, e) => folderHandler.Rename()));
        }
    }
}
