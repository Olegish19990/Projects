using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager.ItemRenamer
{
    public partial class FolderRenamer : Form
    {
        TreeNode node { get; set; }
        public FolderRenamer(TreeNode nodeForWork)
        {
            node = nodeForWork;
            InitializeComponent();
            button1.Enabled = false;
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        private void RenameFolder()
        {

            if (new DirectoryInfo(textBox1.Text).Exists)
            {
                MessageBox.Show("Directory already exists");
            }
            else
            {
                try
                {
                    DirectoryInfo dir = (DirectoryInfo)node.Tag;
                    string newPath = Path.Combine(dir.Parent.FullName, textBox1.Text);
                    dir.MoveTo(newPath);
                }
                catch
                {
                    MessageBox.Show("Rename operation error");
                }

            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RenameFolder();
        }
    }
}
