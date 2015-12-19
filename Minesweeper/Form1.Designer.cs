namespace Minesweeper
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
            this.startButton = new System.Windows.Forms.Button();
            this.rowsEntry = new System.Windows.Forms.TextBox();
            this.rowsLabel = new System.Windows.Forms.Label();
            this.columnsLabel = new System.Windows.Forms.Label();
            this.columnsEntry = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(251, 10);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start!";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // rowsEntry
            // 
            this.rowsEntry.Location = new System.Drawing.Point(46, 12);
            this.rowsEntry.Name = "rowsEntry";
            this.rowsEntry.Size = new System.Drawing.Size(40, 20);
            this.rowsEntry.TabIndex = 2;
            this.rowsEntry.Text = "20";
            // 
            // rowsLabel
            // 
            this.rowsLabel.AutoSize = true;
            this.rowsLabel.Location = new System.Drawing.Point(6, 15);
            this.rowsLabel.Name = "rowsLabel";
            this.rowsLabel.Size = new System.Drawing.Size(34, 13);
            this.rowsLabel.TabIndex = 3;
            this.rowsLabel.Text = "Rows";
            // 
            // columnsLabel
            // 
            this.columnsLabel.AutoSize = true;
            this.columnsLabel.Location = new System.Drawing.Point(115, 14);
            this.columnsLabel.Name = "columnsLabel";
            this.columnsLabel.Size = new System.Drawing.Size(47, 13);
            this.columnsLabel.TabIndex = 4;
            this.columnsLabel.Text = "Columns";
            // 
            // columnsEntry
            // 
            this.columnsEntry.Location = new System.Drawing.Point(169, 12);
            this.columnsEntry.Name = "columnsEntry";
            this.columnsEntry.Size = new System.Drawing.Size(40, 20);
            this.columnsEntry.TabIndex = 5;
            this.columnsEntry.Text = "20";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 233);
            this.Controls.Add(this.columnsEntry);
            this.Controls.Add(this.columnsLabel);
            this.Controls.Add(this.rowsLabel);
            this.Controls.Add(this.rowsEntry);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Boop";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox rowsEntry;
        private System.Windows.Forms.Label rowsLabel;
        private System.Windows.Forms.Label columnsLabel;
        private System.Windows.Forms.TextBox columnsEntry;
    }
}

