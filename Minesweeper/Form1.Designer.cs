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
            this.bombsEntry = new System.Windows.Forms.TextBox();
            this.bombsLabel = new System.Windows.Forms.Label();
            this.debugCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(282, 10);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(69, 23);
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
            this.columnsLabel.Location = new System.Drawing.Point(92, 15);
            this.columnsLabel.Name = "columnsLabel";
            this.columnsLabel.Size = new System.Drawing.Size(47, 13);
            this.columnsLabel.TabIndex = 4;
            this.columnsLabel.Text = "Columns";
            // 
            // columnsEntry
            // 
            this.columnsEntry.Location = new System.Drawing.Point(145, 12);
            this.columnsEntry.Name = "columnsEntry";
            this.columnsEntry.Size = new System.Drawing.Size(40, 20);
            this.columnsEntry.TabIndex = 5;
            this.columnsEntry.Text = "20";
            // 
            // bombsEntry
            // 
            this.bombsEntry.Location = new System.Drawing.Point(236, 12);
            this.bombsEntry.Name = "bombsEntry";
            this.bombsEntry.Size = new System.Drawing.Size(40, 20);
            this.bombsEntry.TabIndex = 7;
            this.bombsEntry.Text = "20";
            // 
            // bombsLabel
            // 
            this.bombsLabel.AutoSize = true;
            this.bombsLabel.Location = new System.Drawing.Point(191, 15);
            this.bombsLabel.Name = "bombsLabel";
            this.bombsLabel.Size = new System.Drawing.Size(39, 13);
            this.bombsLabel.TabIndex = 6;
            this.bombsLabel.Text = "Bombs";
            // 
            // debugCheckBox
            // 
            this.debugCheckBox.AutoSize = true;
            this.debugCheckBox.Location = new System.Drawing.Point(358, 13);
            this.debugCheckBox.Name = "debugCheckBox";
            this.debugCheckBox.Size = new System.Drawing.Size(64, 17);
            this.debugCheckBox.TabIndex = 8;
            this.debugCheckBox.Text = "Debug?";
            this.debugCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 233);
            this.Controls.Add(this.debugCheckBox);
            this.Controls.Add(this.bombsEntry);
            this.Controls.Add(this.bombsLabel);
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
        private System.Windows.Forms.TextBox bombsEntry;
        private System.Windows.Forms.Label bombsLabel;
        private System.Windows.Forms.CheckBox debugCheckBox;
    }
}

