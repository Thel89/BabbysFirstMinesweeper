using System;
using System.Windows.Forms;

namespace Minesweeper
{
    class MinesweeperState
    {
        private int columnCount, rowCount, bombCount, gameState;
        private TableLayoutPanel gamePanel;
        private int[,] gameField;

        private bool isDebug = true;

        public enum GameStates : int { NotStarted, InProgress, Won, Lost };

        public MinesweeperState(TableLayoutPanel gamePanel, int rowCount, int columnCount, int bombCount)
        {
            gameState = (int)GameStates.NotStarted;
            this.gamePanel = gamePanel;
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.bombCount = bombCount;

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
                    UpdateProximityCounts();
                    if (isDebug) { UpdateLabels(); }
                    gameState = (int)GameStates.InProgress;
                    goto case (int)GameStates.InProgress; // I feel horrible for doing this
                case (int)GameStates.InProgress:

                    break;
            }
        }

        private void UpdateLabels()
        {
            foreach(MinesweeperButton mb in gamePanel.Controls)
            {
                int state = gameField[mb.row, mb.column];
                switch (state)
                {
                    case (int)MinesweeperButton.States.Empty:
                        mb.Text = "";
                        break;
                    case (int)MinesweeperButton.States.One:
                        mb.Text = "1";
                        break;
                    case (int)MinesweeperButton.States.Two:
                        mb.Text = "2";
                        break;
                    case (int)MinesweeperButton.States.Three:
                        mb.Text = "3";
                        break;
                    case (int)MinesweeperButton.States.Four:
                        mb.Text = "4";
                        break;
                    case (int)MinesweeperButton.States.Five:
                        mb.Text = "5";
                        break;
                    case (int)MinesweeperButton.States.Six:
                        mb.Text = "6";
                        break;
                    case (int)MinesweeperButton.States.Seven:
                        mb.Text = "7";
                        break;
                    case (int)MinesweeperButton.States.Eight:
                        mb.Text = "8";
                        break;
                    case (int)MinesweeperButton.States.Bomb:
                        mb.Text = "*";
                        break;
                }
                mb.Refresh();
            }
        }

        private void UpdateProximityCounts()
        {
            for(int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (GetIfExistsAndIsBomb(i, j) == 0) {
                        int count = GetIfExistsAndIsBomb(i - 1, j - 1) + GetIfExistsAndIsBomb(i - 1, j) + GetIfExistsAndIsBomb(i - 1, j + 1) +
                                    GetIfExistsAndIsBomb(i + 1, j - 1) + GetIfExistsAndIsBomb(i + 1, j) + GetIfExistsAndIsBomb(i + 1, j + 1) +
                                    GetIfExistsAndIsBomb(i, j - 1) + GetIfExistsAndIsBomb(i, j + 1);
                    switch (count)
                        {
                            case 0:
                                gameField[i, j] = ((int)MinesweeperButton.States.Empty);
                                break;
                            case 1:
                                gameField[i, j] = ((int)MinesweeperButton.States.One);
                                break;
                            case 2:
                                gameField[i, j] = ((int)MinesweeperButton.States.Two);
                                break;
                            case 3:
                                gameField[i, j] = ((int)MinesweeperButton.States.Three);
                                break;
                            case 4:
                                gameField[i, j] = ((int)MinesweeperButton.States.Four);
                                break;
                            case 5:
                                gameField[i, j] = ((int)MinesweeperButton.States.Five);
                                break;
                            case 6:
                                gameField[i, j] = ((int)MinesweeperButton.States.Six);
                                break;
                            case 7:
                                gameField[i, j] = ((int)MinesweeperButton.States.Seven);
                                break;
                            case 8:
                                gameField[i, j] = ((int)MinesweeperButton.States.Eight);
                                break;


                        }
                    }
                }
            }
        }

        // returns 1 if it's a bomb, 0 if not (or invalid tile)
        private int GetIfExistsAndIsBomb(int row, int column)
        {
            if (row < 0 || row >= rowCount || column < 0 || column >= columnCount) {
                return 0;
            }
            if (gameField[row, column] == ((int)MinesweeperButton.States.Bomb)) {
                return 1;
            }
            return 0;
        }

        private void GenerateBombs(int rowPositionToExclude, int columnPositionToExclude)
        {
            gameField = new int[rowCount, columnCount];
            int bombsToGenerate = bombCount;
            Random rand = new Random();
            while (bombsToGenerate > 0)
            {
                int possibleRow = rand.Next(rowCount);
                int possibleColumn = rand.Next(columnCount);
                if((Math.Abs(possibleRow-rowPositionToExclude) <= 1) && (Math.Abs(possibleColumn - columnPositionToExclude) <= 1))
                {
                    continue; // we don't want to place a bomb where they've just clicked! :( 
                    // (ps. we also exclude the 8 tiles immediately adjacent so as to give them a completely empty tile to start with)
                }
                else if (gameField[possibleRow,possibleColumn] == ((int)MinesweeperButton.States.Bomb))
                {
                    continue; // bomb here already
                }
                else
                {
                    gameField[possibleRow, possibleColumn] = ((int)MinesweeperButton.States.Bomb);
                    bombsToGenerate--;
                }

            }
        }
    }
}
