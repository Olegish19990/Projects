using FileProcessor;
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


namespace FileManager
{
    public partial class Manager : Form
    {
        private TreeNodeManager treeNodeManager { get; set; }

        private ListViewManager listViewManager { get; set; }

        //private NodeIsAccess nodeIsAccess { get; set; }

        private DirectoryInfo currentDir { get; set; }

        private Scaner scaner { get; set; }

       
        public Manager()
        {
            InitializeComponent();
            ManagerInitialize();
        }

        private void ManagerInitialize()
        {
            treeNodeManager = new TreeNodeManager(treeView1);
            listViewManager = new ListViewManager(listView1);
            //nodeIsAccess = new NodeIsAccess();
            scaner = new Scaner();
            
        }





        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Text != "Root")
            {
                if (NodeIsAccess.CheckAccess((DirectoryInfo)e.Node.Tag))
                {
                    treeNodeManager.NodeOpen(e.Node);
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
            scaner.FindFiles(currentDir, true, scanedFiles);
            listViewManager.DisplayFiles(scanedFiles);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text != "Root")
            {

                if (NodeIsAccess.CheckAccess((DirectoryInfo)e.Node.Tag))
                {
                    currentDir = (DirectoryInfo)e.Node.Tag;
                    var dir = (DirectoryInfo)e.Node.Tag;
                    listViewManager.DisplayFiles(dir.GetFiles());
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
