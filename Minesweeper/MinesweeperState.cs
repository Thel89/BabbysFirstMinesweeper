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
        private int gameState;

        public enum GameStates : int { NotStarted, InProgress, Won, Lost };

        public MinesweeperState(TableLayoutPanel gamePanel, int rowCount, int columnCount)
        {
            gameState = (int)GameStates.NotStarted;
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
            switch (gameState)
            {
                case (int)GameStates.NotStarted:
                    GenerateBombs(row, column); // we pass through where we clicked so people don't click a bomb and instantly lose
                    gameState = (int)GameStates.InProgress;
                    goto case (int)GameStates.InProgress; // I feel horrible for doing this
                case (int)GameStates.InProgress:

                    break;
            }
        }

        internal void GenerateBombs(int row, int column)
        {

        }
    }
}
