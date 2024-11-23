using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager.ItemCreate;
using FileManager.nodeController;
using FileProcessor;
using FileProcessor.Removing;
using FileProcessor.Reporting;

namespace FileManager
{
    public partial class Manager : Form
    {
        private NodeController nodeController { get; set; }
        private DeleteItem deleteItem { get; set; }
        private ListViewManager listViewManager { get; set; }
        private Scaner scaner { get; set; }

       // private CurrentDirectory currentDir { get;set; }

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
            deleteItem = new DeleteItem();
            //nodeIsAccess = new NodeIsAccess();
            scaner = new Scaner();
            listView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.ContextMenuStrip = contextMenuStrip2;


        }

        private void ContextMenuInitilize()
        {
            ToolStripMenuItem deleteFiles = new ToolStripMenuItem("Delete Files", null, (sender, e) => Delete_click(RemoverMode.Files));
            ToolStripMenuItem deleteFolders = new ToolStripMenuItem("Delete Folders", null, (sender, e) => Delete_click(RemoverMode.Directories));
            ToolStripMenuItem createFile = new ToolStripMenuItem("Create File", null, (sender, e) => Create(CreateMode.File));
            ToolStripMenuItem createDirectory = new ToolStripMenuItem("Create Directory", null, (sender, e) => Create(CreateMode.Directory));
            contextMenuStrip1.Items.Add(deleteFiles);
            contextMenuStrip1.Items.Add(createFile);

            contextMenuStrip2.Items.Add(deleteFolders);
            contextMenuStrip2.Items.Add(createDirectory);
        }


        private void Create(CreateMode mode)
        {
            if (DirectoryIsAcces.CheckAccess(CurrentDirectory.CurrentDir))
            {

                if (mode == CreateMode.Directory)
                {
                    DirectoryCreate directoryCreate = new DirectoryCreate(treeView1.SelectedNode);
                    directoryCreate.ShowDialog();
                }
                else if (mode == CreateMode.File)
                {
                    FileCreate fileCreate = new FileCreate();
                    fileCreate.ShowDialog();
                    listViewManager.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());
                }
            }
            else
            {
                MessageBox.Show("Access denied");
            }
        }
        private void Delete_click(RemoverMode mode)
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
                    e.Node.ForeColor = Color.Red;
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
                    e.Node.ForeColor = Color.Red;
                    e.Node.Nodes.Clear();
                }
            }
        }
    }
}