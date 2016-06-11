using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {

        private TableLayoutPanel gamePanel;

        private MinesweeperState gameState;

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int rowCount = int.Parse(rowsEntry.Text);
            int columnCount = int.Parse(columnsEntry.Text);
            int bombCount = int.Parse(bombsEntry.Text);

            SuspendLayout();
            if (gamePanel != null)
            {
                Controls.Remove(gamePanel);
                gamePanel.Dispose();
            }
            gamePanel = new TableLayoutPanel();
            gameState = new MinesweeperState(gamePanel, rowCount, columnCount, bombCount, debugCheckBox);


            Controls.Add(gamePanel);
            ClientSize = new Size(Math.Max(420, 20 * columnCount + 100), 20 * rowCount + 100);
            
            ResumeLayout(false);
            PerformLayout();

        }
    }
}
