namespace FileManager.FileOpen
{
    partial class FileWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            richTextBox1 = new RichTextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(83, 25);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(1414, 793);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            richTextBox1.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(83, 25);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1414, 793);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // FileWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1534, 887);
            Controls.Add(pictureBox1);
            Controls.Add(richTextBox1);
            Name = "FileWindow";
            Text = "FileWindow";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private PictureBox pictureBox1;
    }
}