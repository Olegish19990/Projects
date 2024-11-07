namespace FileManager
{
    partial class Manager
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
            treeView1 = new TreeView();
            listView1 = new ListView();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // treeView1
            // 
            treeView1.Location = new Point(12, 3);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(321, 1008);
            treeView1.TabIndex = 0;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            treeView1.AfterSelect += treeView1_AfterSelect;
            // 
            // listView1
            // 
            listView1.Location = new Point(368, 32);
            listView1.Name = "listView1";
            listView1.Size = new Size(1189, 599);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(368, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(882, 23);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(1256, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(198, 23);
            textBox2.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(1460, 3);
            button1.Name = "button1";
            button1.Size = new Size(97, 23);
            button1.TabIndex = 4;
            button1.Text = "Find";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Manager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1867, 1073);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(listView1);
            Controls.Add(treeView1);
            Name = "Manager";
            Text = "Manager";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeView1;
        private ListView listView1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
    }
}