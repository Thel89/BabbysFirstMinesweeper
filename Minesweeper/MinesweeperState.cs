using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class MinesweeperState
    {
        private int columnCount;
        private TableLayoutPanel gamePanel;
        private int rowCount;

        public MinesweeperState(TableLayoutPanel gamePanel, int rowCount, int columnCount)
        {
            this.gamePanel = gamePanel;
            this.rowCount = rowCount;
            this.columnCount = columnCount;

            gamePanel.ColumnCount = columnCount;
            for (int i = 0; i < columnCount; i++)
            {
                gamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            }

            gamePanel.RowCount = rowCount;
            for (int i = 0; i < rowCount; i++)
            {
                this.gamePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            }

            gamePanel.Size = new System.Drawing.Size(20 * columnCount, 20 * rowCount);
            gamePanel.TabIndex = 1;

            for (int i = 0; i < gamePanel.ColumnCount; i++)
            {
                for (int j = 0; j < gamePanel.RowCount; j++)
                {
                    MinesweeperButton b = new MinesweeperButton(i, j, this);
                    gamePanel.Controls.Add(b, i, j);
                }
            }
            int width = Math.Max(340, 20 * columnCount + 100);
            int xEdge = width / 2 - 10 * columnCount;
            gamePanel.Location = new System.Drawing.Point(xEdge, 50);
            gamePanel.Name = "gamePanel";
        }

        internal void GameButtonClick(int row, int column)
        {
            throw new NotImplementedException();
        }
    }
}
