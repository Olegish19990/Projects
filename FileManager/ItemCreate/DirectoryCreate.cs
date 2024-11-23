using FileManager.nodeController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager.ItemCreate
{
    public partial class DirectoryCreate : Form
    {
        private TreeNode _node { get; set; }
        public DirectoryCreate(TreeNode node)
        {
            InitializeComponent();
            _node = node;
            button1.Enabled = false;
            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        private void DirectoryCreateLogic(TreeNode node)
        {
           
            string dirName = textBox1.Text;
            DirectoryInfo directory = new DirectoryInfo(Path.Combine(CurrentDirectory.CurrentDir.FullName, dirName));
            directory.Create();
            node.Nodes.Add(NodeCreator.CreateNode(directory));
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryCreateLogic(_node);
        }
    }
}
