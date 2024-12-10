using FileProcessor;
using FileProcessor.Renaming;
using System.Text;

namespace FileManager.ItemRenamer
{
    public partial class FileRenamer : Form
    {
        private List<FileInfo> files { get; set; }

        public FileRenamer(List<FileInfo> files)
        {
            InitializeComponent();
            this.files = files;

        }

        private void Rename()
        {


            string stringPattern = PatternBuild();

            Renamer renamer = new Renamer(new FSObjectContainer() { Files = files });
            renamer.RenameFiles(stringPattern);
            this.Close();
        }

        private string PatternBuild()
        {
            StringBuilder pattern = new StringBuilder();

            if (textBox1.Text.Length > 0)
            {
                pattern.Append(textBox1.Text);
            }

            if (checkBox1.Checked)
            {
                pattern.Append("<old_name>");
            }
            if (checkBox2.Checked)
            {
                pattern.Append($"<increment({numericUpDown1.Value.ToString()},{numericUpDown1.Value.ToString()})>");
            }
            if (checkBox3.Checked)
            {
                pattern.Append("<uuid>");
            }

            return pattern.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rename();
        }
    }
}
