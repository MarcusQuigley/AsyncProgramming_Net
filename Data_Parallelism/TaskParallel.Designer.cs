namespace Data_Parallelism
{
    partial class TaskParallel
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
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonGetStats = new System.Windows.Forms.Button();
            this.textBoxBook = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(21, 168);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(75, 36);
            this.buttonDownload.TabIndex = 0;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonGetStats
            // 
            this.buttonGetStats.Location = new System.Drawing.Point(142, 168);
            this.buttonGetStats.Name = "buttonGetStats";
            this.buttonGetStats.Size = new System.Drawing.Size(75, 36);
            this.buttonGetStats.TabIndex = 1;
            this.buttonGetStats.Text = "Get Stats";
            this.buttonGetStats.UseVisualStyleBackColor = true;
            this.buttonGetStats.Click += new System.EventHandler(this.buttonGetStats_Click);
            // 
            // textBoxBook
            // 
            this.textBoxBook.Location = new System.Drawing.Point(21, 37);
            this.textBoxBook.Multiline = true;
            this.textBoxBook.Name = "textBoxBook";
            this.textBoxBook.Size = new System.Drawing.Size(102, 79);
            this.textBoxBook.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(160, 37);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 95);
            this.listBox1.TabIndex = 3;
            // 
            // TaskParallel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBoxBook);
            this.Controls.Add(this.buttonGetStats);
            this.Controls.Add(this.buttonDownload);
            this.Name = "TaskParallel";
            this.Text = "TaskParallel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonGetStats;
        private System.Windows.Forms.TextBox textBoxBook;
        private System.Windows.Forms.ListBox listBox1;
    }
}