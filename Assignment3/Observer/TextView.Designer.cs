using System.Drawing;
using System.Windows.Forms;

namespace Assignment3
{
    partial class textualView
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
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(91, 51);
            this.textBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(600, 22);
            this.textBox.TabIndex = 0;
            // 
            // textualView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 144);
            this.Controls.Add(this.textBox);
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "textualView";
            this.Text = "Maze Narrator";
            this.Load += new System.EventHandler(this.textualView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox;
    }
}

