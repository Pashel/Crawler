namespace Crawler2
{
    partial class CrawlerForm
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
            this.Word = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Deep = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OutputBox = new System.Windows.Forms.RichTextBox();
            this.Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Word
            // 
            this.Word.Location = new System.Drawing.Point(365, 6);
            this.Word.Name = "Word";
            this.Word.Size = new System.Drawing.Size(100, 20);
            this.Word.TabIndex = 15;
            this.Word.Text = "Лукашенко";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Word";
            // 
            // Deep
            // 
            this.Deep.Location = new System.Drawing.Point(509, 6);
            this.Deep.Name = "Deep";
            this.Deep.Size = new System.Drawing.Size(30, 20);
            this.Deep.TabIndex = 13;
            this.Deep.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Deep";
            // 
            // Url
            // 
            this.Url.Location = new System.Drawing.Point(36, 6);
            this.Url.Name = "Url";
            this.Url.Size = new System.Drawing.Size(286, 20);
            this.Url.TabIndex = 11;
            this.Url.Text = "https://tut.by";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Url";
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(8, 99);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.Size = new System.Drawing.Size(534, 195);
            this.OutputBox.TabIndex = 9;
            this.OutputBox.Text = "";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(11, 35);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(531, 52);
            this.Start.TabIndex = 8;
            this.Start.Text = "Начать";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.StartClick);
            // 
            // CrawlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 299);
            this.Controls.Add(this.Word);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Deep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Url);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.Start);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(566, 338);
            this.MinimumSize = new System.Drawing.Size(566, 338);
            this.Name = "CrawlerForm";
            this.Text = "Crawler";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Word;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Deep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox OutputBox;
        private System.Windows.Forms.Button Start;
    }
}

