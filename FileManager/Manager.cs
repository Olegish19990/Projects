using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager.nodeController;
using FileProcessor;
using FileProcessor.Removing;

namespace FileManager
{
    public partial class Manager : Form
    {
        private NodeController nodeController { get; set; }

        private ListViewManager listViewManager { get; set; }
        private Scaner scaner { get; set; }


        public Manager()
        {
            InitializeComponent();
            ManagerInitialize();
            ContextMenuInitilize();
        }

        private void ManagerInitialize()
        {
            nodeController = new NodeController(treeView1);
            listViewManager = new ListViewManager(listView1);
            //nodeIsAccess = new NodeIsAccess();
            scaner = new Scaner();
            listView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.ContextMenuStrip = contextMenuStrip2;


        }

        private void ContextMenuInitilize()
        {
            ToolStripMenuItem deleteFiles = new ToolStripMenuItem("Delete", null, Delete_Files);
            ToolStripMenuItem deleteFolder = new ToolStripMenuItem("DeleteFolder", null, Delete_Folders);
            contextMenuStrip1.Items.Add(deleteFiles);
            contextMenuStrip2.Items.Add(deleteFolder);
           
          
        }


        //REFACTORING

         private void Delete_Files(object sender, EventArgs e)
         {
            Remover remover = new Remover(new FSObjectContainer());
            remover.Container.Files = listViewManager.GetSelectedItems(); 
            var result = MessageBox.Show("Do you want to delete the selected files?", "сonfirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                remover.Execute(RemoverMode.Files);
                MessageBox.Show("Files deleted successfully.");
                listView1.SelectedItems.Clear();
                listViewManager.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());
            }
          }

        private void Delete_Folders(object sender, EventArgs e)
        {
          
            Remover remover = new Remover(new FSObjectContainer());
            remover.Container.Dirs.Add(CurrentDirectory.CurrentDir);
            var result = MessageBox.Show("Do you want to delete the selected folders?", "сonfirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                remover.Execute(RemoverMode.Directories);
                MessageBox.Show("Folders deleted successfully.");
                treeView1.SelectedNode.Remove();
            }
        }




        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text != "Root")
            {

                if (DirectoryIsAcces.CheckAccess((DirectoryInfo)e.Node.Tag))
                {
                    nodeController.NodeOpen(e.Node);
                }
                else
                {
                    MessageBox.Show("Access denied");
                    e.Node.Nodes.Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scaner.FileMasks = textBox2.Lines;
            List<FileInfo> scanedFiles = new List<FileInfo>();
            scaner.FindFiles(CurrentDirectory.CurrentDir, true, scanedFiles);
            listViewManager.DisplayFiles(scanedFiles);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {



            if (e.Node.Text != "Root")
            {

                if (DirectoryIsAcces.CheckAccess((DirectoryInfo)e.Node.Tag))
                {
                    CurrentDirectory.CurrentDir = (DirectoryInfo)e.Node.Tag;
                    var dir = (DirectoryInfo)e.Node.Tag;
                    listViewManager.DisplayFiles(dir.GetFiles());
                    textBox1.Text = CurrentDirectory.CurrentDir.FullName;
                    //textBox1.Refresh();
                }
                else
                {
                    MessageBox.Show("Access denied");
                    e.Node.Nodes.Clear();
                }
            }
        }
    }
}