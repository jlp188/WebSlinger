namespace WebSlinger
{
    partial class Form2
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnProxIP = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnDropList = new System.Windows.Forms.Button();
            this.btnProcess = new System.Windows.Forms.Button();
            this.checkedListBoxResources = new System.Windows.Forms.CheckedListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnImportBL4 = new System.Windows.Forms.Button();
            this.btnSpider = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxExecTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(35, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(645, 20);
            this.textBox1.TabIndex = 0;
            // 
            // btnProxIP
            // 
            this.btnProxIP.Location = new System.Drawing.Point(734, 24);
            this.btnProxIP.Name = "btnProxIP";
            this.btnProxIP.Size = new System.Drawing.Size(136, 20);
            this.btnProxIP.TabIndex = 1;
            this.btnProxIP.Text = "Browse for IP file";
            this.btnProxIP.UseVisualStyleBackColor = true;
            this.btnProxIP.Click += new System.EventHandler(this.btnProxIP_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(35, 96);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(645, 407);
            this.listBox1.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnDropList
            // 
            this.btnDropList.Location = new System.Drawing.Point(734, 58);
            this.btnDropList.Name = "btnDropList";
            this.btnDropList.Size = new System.Drawing.Size(136, 23);
            this.btnDropList.TabIndex = 3;
            this.btnDropList.Text = "Browse for Drop List";
            this.btnDropList.UseVisualStyleBackColor = true;
            this.btnDropList.Click += new System.EventHandler(this.btnDropList_Click);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(734, 96);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(136, 23);
            this.btnProcess.TabIndex = 4;
            this.btnProcess.Text = "Begin Processing";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // checkedListBoxResources
            // 
            this.checkedListBoxResources.FormattingEnabled = true;
            this.checkedListBoxResources.Items.AddRange(new object[] {
            "Alexa",
            "PageRank",
            "Archive",
            "Compete",
            "Quantcast",
            "DMOZ",
            "WhoIs",
            "Estibot",
            "IndexPages",
            "BackLinks"});
            this.checkedListBoxResources.Location = new System.Drawing.Point(734, 136);
            this.checkedListBoxResources.Name = "checkedListBoxResources";
            this.checkedListBoxResources.Size = new System.Drawing.Size(136, 199);
            this.checkedListBoxResources.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(734, 378);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Use Proxy?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // btnImportBL4
            // 
            this.btnImportBL4.Location = new System.Drawing.Point(734, 439);
            this.btnImportBL4.Name = "btnImportBL4";
            this.btnImportBL4.Size = new System.Drawing.Size(75, 23);
            this.btnImportBL4.TabIndex = 7;
            this.btnImportBL4.Text = "Import BL4";
            this.btnImportBL4.UseVisualStyleBackColor = true;
            this.btnImportBL4.Click += new System.EventHandler(this.btnImportBL4_Click);
            // 
            // btnSpider
            // 
            this.btnSpider.Location = new System.Drawing.Point(734, 502);
            this.btnSpider.Name = "btnSpider";
            this.btnSpider.Size = new System.Drawing.Size(75, 23);
            this.btnSpider.TabIndex = 8;
            this.btnSpider.Text = "Spider";
            this.btnSpider.UseVisualStyleBackColor = true;
            this.btnSpider.Click += new System.EventHandler(this.btnSpider_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 535);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Execution time:";
            // 
            // txtBxExecTime
            // 
            this.txtBxExecTime.Location = new System.Drawing.Point(120, 532);
            this.txtBxExecTime.Name = "txtBxExecTime";
            this.txtBxExecTime.Size = new System.Drawing.Size(100, 20);
            this.txtBxExecTime.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 535);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "counter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(553, 535);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "timeAvg";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 535);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Avg Time in Seconds:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 577);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBxExecTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSpider);
            this.Controls.Add(this.btnImportBL4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkedListBoxResources);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnDropList);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btnProxIP);
            this.Controls.Add(this.textBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnProxIP;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnDropList;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.CheckedListBox checkedListBoxResources;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnImportBL4;
        private System.Windows.Forms.Button btnSpider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxExecTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}