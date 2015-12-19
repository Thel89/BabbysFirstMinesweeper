using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            SuspendLayout();

            if (gamePanel != null)
            {
                Controls.Remove(gamePanel);
                gamePanel.Dispose();
            }
            gamePanel = new TableLayoutPanel();

            gameState = new MinesweeperState(gamePanel, rowCount, columnCount);


            Controls.Add(gamePanel);
            
            ClientSize = new Size(Math.Max(340, 20 * columnCount + 100), 20 * rowCount + 100);

            ResumeLayout(false);
            PerformLayout();


         /*   for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    this.tableLayoutPanel1.Controls.Add(new Button(), j, i);
                }
            }
           */ 
        }
    }
}
