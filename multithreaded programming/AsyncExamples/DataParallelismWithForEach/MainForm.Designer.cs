namespace DataParallelismWithForEach
{
    partial class MainForm
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
            this.ProcessImagesBtn = new System.Windows.Forms.Button();
            this.MainTextBox = new System.Windows.Forms.RichTextBox();
            this.MainLabel = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProcessImagesBtn
            // 
            this.ProcessImagesBtn.Location = new System.Drawing.Point(12, 114);
            this.ProcessImagesBtn.Name = "ProcessImagesBtn";
            this.ProcessImagesBtn.Size = new System.Drawing.Size(122, 23);
            this.ProcessImagesBtn.TabIndex = 0;
            this.ProcessImagesBtn.Text = "Process Images";
            this.ProcessImagesBtn.UseVisualStyleBackColor = true;
            this.ProcessImagesBtn.Click += new System.EventHandler(this.ProcessImagesBtn_Click);
            // 
            // MainTextBox
            // 
            this.MainTextBox.Location = new System.Drawing.Point(12, 12);
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.Size = new System.Drawing.Size(776, 96);
            this.MainTextBox.TabIndex = 2;
            this.MainTextBox.Text = "";
            // 
            // MainLabel
            // 
            this.MainLabel.AutoSize = true;
            this.MainLabel.Location = new System.Drawing.Point(165, 124);
            this.MainLabel.Name = "MainLabel";
            this.MainLabel.Size = new System.Drawing.Size(38, 13);
            this.MainLabel.TabIndex = 3;
            this.MainLabel.Text = "Ready";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(13, 144);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 4;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.MainLabel);
            this.Controls.Add(this.MainTextBox);
            this.Controls.Add(this.ProcessImagesBtn);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ProcessImagesBtn;
        private System.Windows.Forms.RichTextBox MainTextBox;
        private System.Windows.Forms.Label MainLabel;
        private System.Windows.Forms.Button CancelBtn;
    }
}

