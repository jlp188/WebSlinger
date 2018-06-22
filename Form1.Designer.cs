namespace WebSlinger
{
    partial class Form1
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
            this.treeViewWebList = new System.Windows.Forms.TreeView();
            this.btnSling = new System.Windows.Forms.Button();
            this.txtBxInput = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // treeViewWebList
            // 
            this.treeViewWebList.Location = new System.Drawing.Point(31, 108);
            this.treeViewWebList.Name = "treeViewWebList";
            this.treeViewWebList.Size = new System.Drawing.Size(684, 116);
            this.treeViewWebList.TabIndex = 0;
            // 
            // btnSling
            // 
            this.btnSling.Location = new System.Drawing.Point(736, 500);
            this.btnSling.Name = "btnSling";
            this.btnSling.Size = new System.Drawing.Size(75, 23);
            this.btnSling.TabIndex = 1;
            this.btnSling.Text = "Sling";
            this.btnSling.UseVisualStyleBackColor = true;
            this.btnSling.Click += new System.EventHandler(this.btnSling_Click);
            // 
            // txtBxInput
            // 
            this.txtBxInput.Location = new System.Drawing.Point(31, 26);
            this.txtBxInput.Multiline = true;
            this.txtBxInput.Name = "txtBxInput";
            this.txtBxInput.Size = new System.Drawing.Size(512, 56);
            this.txtBxInput.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(31, 239);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(684, 355);
            this.listBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 881);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtBxInput);
            this.Controls.Add(this.btnSling);
            this.Controls.Add(this.treeViewWebList);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewWebList;
        private System.Windows.Forms.Button btnSling;
        private System.Windows.Forms.TextBox txtBxInput;
        private System.Windows.Forms.ListBox listBox1;
    }
}

