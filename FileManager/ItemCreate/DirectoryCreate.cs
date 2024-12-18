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
           
            string newDirPath = Path.Combine(CurrentDirectory.CurrentDir.FullName, textBox1.Text);
            if (new DirectoryInfo(newDirPath).Exists)
            {
                MessageBox.Show("A folder with that name already exists.");
            }
            else
            {
                try
                {
                    DirectoryInfo directory = new DirectoryInfo(newDirPath);
                    directory.Create();
                    node.Nodes.Add(NodeCreator.CreateNode(directory));
                }
                catch
                {
                    MessageBox.Show("Folder creation error");
                }

            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryCreateLogic(_node);
        }
    }
}
