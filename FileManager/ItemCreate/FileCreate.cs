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
    public partial class FileCreate : Form
    {
        public FileCreate()
        {
            InitializeComponent();
            button1.Enabled = false; 
            textBox1.TextChanged += TextBox1_TextChanged; 
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = !string.IsNullOrWhiteSpace(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileCreateLogic();
        }

        private void FileCreateLogic()
        {
       
            string input = textBox1.Text.Trim();
            string fileName = input.Contains(".") ? input.Substring(0, input.LastIndexOf('.')) : input;
            string extension = input.Contains(".") ? input.Substring(input.LastIndexOf('.') + 1) : null; 

            string directoryPath = CurrentDirectory.CurrentDir.FullName;

          
            string finalName = fileName;
            int count = 0;
            while (File.Exists(Path.Combine(directoryPath, finalName + (extension != null ? $".{extension}" : ""))))
            {
                count++;
                finalName = $"{fileName}({count})";
            }

         
            string fullPath = Path.Combine(directoryPath, finalName + (extension != null ? $".{extension}" : ""));
            try
            {
                using (FileStream fs = File.Create(fullPath)) { }
                MessageBox.Show($"File created: {fullPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("file creation error");
            }
           
            this.Close();
        }
    }
}
