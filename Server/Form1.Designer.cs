
namespace Server
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
            this.Journal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Journal
            // 
            this.Journal.Location = new System.Drawing.Point(13, 75);
            this.Journal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Journal.Multiline = true;
            this.Journal.Name = "Journal";
            this.Journal.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Journal.Size = new System.Drawing.Size(690, 341);
            this.Journal.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "IpAddress:";
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(93, 12);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(100, 22);
            this.ipAddressTextBox.TabIndex = 2;
            this.ipAddressTextBox.Text = "127.0.0.1";
            this.ipAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(93, 38);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(100, 22);
            this.portTextBox.TabIndex = 4;
            this.portTextBox.Text = "9000";
            this.portTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 429);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Journal);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Task-maneger-server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Journal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label2;
    }
}

