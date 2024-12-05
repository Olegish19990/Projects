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
using FileManager.FileOpen;
using FileManager.ItemCreate;
using FileManager.nodeController;
using FileProcessor;
using FileProcessor.Removing;
using FileProcessor.Reporting;
using FileManager.FileHandling;
namespace FileManager
{
    public partial class Manager : Form
    {
        private NodeController nodeController { get; set; }
        private FileDisplayer fileDisplayer { get; set; }
        private ContextMenuHandler contextMenuHandler { get; set; }


        public Manager()
        {
            InitializeComponent();
            ManagerInitialize();
            ContextMenuInitilize();
        }

        private void ManagerInitialize()
        {
            nodeController = new NodeController(treeView1);
            fileDisplayer = new FileDisplayer(listView1);
            listView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.ContextMenuStrip = contextMenuStrip2;
            listView1.DoubleClick += listView_DoubleClick;


        }

        private void ContextMenuInitilize()
        {
            
            contextMenuHandler = new ContextMenuHandler(contextMenuStrip1,contextMenuStrip2, fileDisplayer, treeView1);
        }

       
        private void listView_DoubleClick(object sender, EventArgs e)
        {
            FileInfo? selectedFile = listView1.SelectedItems[0].Tag as FileInfo;
            FileWindow fileWindow = new FileWindow(selectedFile);
           
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
            Scaner scaner = new Scaner();
            scaner.FileMasks = textBox2.Lines;
            List<FileInfo> scanedFiles = new List<FileInfo>();
            scaner.FindFiles(CurrentDirectory.CurrentDir, true, scanedFiles);
            fileDisplayer.DisplayFiles(scanedFiles);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Text != "Root")
            {

                if (DirectoryIsAcces.CheckAccess((DirectoryInfo)e.Node.Tag))
                {
                    CurrentDirectory.CurrentDir = (DirectoryInfo)e.Node.Tag;
                    var dir = (DirectoryInfo)e.Node.Tag;
                    fileDisplayer.DisplayFiles(dir.GetFiles());
                    textBox1.Text = CurrentDirectory.CurrentDir.FullName;
            
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