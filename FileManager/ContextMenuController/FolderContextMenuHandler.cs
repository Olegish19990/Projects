using FileManager.ItemCreate;
using FileManager.ItemRenamer;
using FileManager.nodeController;
using FileProcessor.Removing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ContextMenuController
{
    public class FolderContextMenuHandler : IContextMenuHandler
    {
        private TreeView treeView;

        public FolderContextMenuHandler(TreeView treeView)
        {
            this.treeView = treeView;
        }

        public void Create()
        {
            DirectoryCreate directoryCreate = new DirectoryCreate(treeView.SelectedNode);
            directoryCreate.ShowDialog();
        }

        public void Delete()
        {
            DeleteItem deleteItem = new DeleteItem();

            if (deleteItem.ConfirmDelete())
            {
                try
                {
                    deleteItem.Delete(RemoverMode.Directories, null);
                    if (treeView.SelectedNode != null)
                    {
                        TreeNode parentNode = treeView.SelectedNode.Parent;
                        treeView.SelectedNode.Remove();


                        if (parentNode != null && parentNode.Nodes.Count == 0)
                        {
                            DirectoryInfo dirInfo = (DirectoryInfo)parentNode.Tag;
                            ExpandDirectoryNodes(dirInfo, parentNode);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Deletion error");
                }

               
               
            }
        }

        private void ExpandDirectoryNodes(DirectoryInfo dir, TreeNode node)
        {
            node.Nodes.Clear(); 

            try
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (var d in dirs)
                {
                    node.Nodes.Add(NodeCreator.CreateNode(d));
                }
            }
            catch (UnauthorizedAccessException)
            {
               
            }
        }




        public void Rename()
        {
           FolderRenamer folderRenamer = new FolderRenamer(treeView.SelectedNode);
           folderRenamer.ShowDialog();
           TreeNode parentNode = treeView.SelectedNode.Parent;
           ExpandDirectoryNodes((DirectoryInfo)treeView.SelectedNode.Parent.Tag, parentNode);

            
        }
    }
}
