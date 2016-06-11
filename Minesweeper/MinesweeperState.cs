using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace Minesweeper
{
    class MinesweeperState
    {
        private int columnCount, rowCount, bombCount, gameState;
        private TableLayoutPanel gamePanel;
        private MinesweeperButton[,] gameField;
        private bool left_down, right_down, both_click;
        private bool isDebug;
        private CheckBox debugCheckBox;

        public enum GameStates : int { NotStarted, InProgress, Won, Lost };

        public MinesweeperState(TableLayoutPanel gamePanel, int rowCount, int columnCount, int bombCount, CheckBox debug)
        {
            gameState = (int)GameStates.NotStarted;
            this.gamePanel = gamePanel;
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.bombCount = bombCount;
            this.isDebug = debug.Checked;
            this.debugCheckBox = debug;

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

            gameField = new MinesweeperButton[rowCount, columnCount];

            for (int i = 0; i < gamePanel.ColumnCount; i++)
            {
                for (int j = 0; j < gamePanel.RowCount; j++)
                {
                    MinesweeperButton b = new MinesweeperButton(i, j, this);
                    gameField[i, j] = b;
                    gamePanel.Controls.Add(b, i, j);
                }
            }
            int width = Math.Max(340, 20 * columnCount + 100);
            int xEdge = width / 2 - 10 * columnCount;
            gamePanel.Location = new System.Drawing.Point(xEdge, 50);
            gamePanel.Name = "gamePanel";
        }

        #region Awkward mouse handling shite
        internal void GameButton_MouseDown(int row, int column, MouseEventArgs e)
        {
            if (isDebug) {
                Debug.WriteLine("Down at ({0},{1}) - {2}", row, column, e.Button);
            }
            if (e.Button == MouseButtons.Left)
            {
                left_down = true;
                if (right_down)
                {
                    both_click = true;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                right_down = true;
                if (left_down)
                {
                    both_click = true;
                }
            }
        }

        internal void GameButton_MouseUp(int row, int column, MouseEventArgs e)
        {
            if (isDebug)
            {
                Debug.WriteLine("Up at ({0},{1}) - {2}", row, column, e.Button);
            }
            if((both_click == false) && (left_down == false) && (right_down == false))
            {
                // we've moved, ignore this event
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (!right_down)
                {
                    if (both_click)
                    {
                        TwoButtonClick(row, column);
                    }
                    else
                    {
                        LeftButtonClick(row, column);
                    }
                    both_click = false;
                }
                left_down = false;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (!left_down)
                {
                    if (both_click)
                    {
                        TwoButtonClick(row, column);
                    }
                    else
                    {
                        RightButtonClick(row, column);
                    }
                    both_click = false;
                }
                right_down = false;
            }
        }

        internal void GameButton_MouseLeave(int row, int column, EventArgs e)
        {
            both_click = false;
            left_down = false;
            right_down = false;
        }
        #endregion

        internal void LeftButtonClick(int row, int column)
        {
            switch (gameState)
            {
                case (int)GameStates.NotStarted:
                    gameState = (int)GameStates.InProgress;
                    GenerateBombs(row, column); // we pass through where we clicked so people don't click a bomb and instantly lose
                    UpdateProximityCounts();
                    if (isDebug) {
                        UpdateAllLabels();
                    }
                    goto case (int)GameStates.InProgress; // I feel horrible for doing this
                case (int)GameStates.InProgress:
                    ExposeCell(gameField[row, column]);
                    CheckIfWon();
                    break;
            }

        }

        private void CheckIfWon()
        {
            int count = 0;
            for(int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    if (gameField[i, j].Enabled)
                    {
                        count++;
                    }
                }
            }
            if(count == bombCount)
            {
                MessageBox.Show("You win!");
                gameState = (int)GameStates.Won;
                //todo: stats and shit
            }
        }

        private void ExposeCell(MinesweeperButton button)
        {
            if (button.BombState == MinesweeperButton.BombStates.Bomb && !isDebug)
            {
                MessageBox.Show("You lose");
                gameState = (int)GameStates.Lost;
                //todo: stats and shit
                //todo: expose all mines
            }
            else
            {
                button.Enabled = false;
                updateLabel(button);
                if (button.BombState == MinesweeperButton.BombStates.Empty)
                {
                    ExposeAdjacentCells(button, button.Row, button.Column);
                }
            }
            debugCheckBox.Focus();
        }

        private void ExposeAdjacentCells(MinesweeperButton button, int row, int column)
        {
            List<MinesweeperButton> neighbours = new List<MinesweeperButton>();
            if (row > 0 && column > 0)
            {
                neighbours.Add(gameField[row - 1, column - 1]);
            }
            if (row > 0)
            {
                neighbours.Add(gameField[row - 1, column]);
            }
            if (row > 0 && column < (columnCount - 1))
            {
                neighbours.Add(gameField[row - 1, column + 1]);
            }
            if (column < (columnCount - 1))
            {
                neighbours.Add(gameField[row, column + 1]);
            }
            if (row < (rowCount - 1) && column < (columnCount - 1))
            {
                neighbours.Add(gameField[row + 1, column + 1]);
            }
            if (row < (rowCount - 1))
            {
                neighbours.Add(gameField[row + 1, column]);
            }
            if (row < (rowCount - 1) && column > 0)
            {
                neighbours.Add(gameField[row + 1, column-1]);
            }
            if (column > 0)
            {
                neighbours.Add(gameField[row, column - 1]);
            }
            foreach(MinesweeperButton b in neighbours)
            {
                if (b.Enabled)
                {
                    ExposeCell(b);
                }
            }
        }

        internal void RightButtonClick(int row, int column)
        {
            MinesweeperButton button = gameField[row, column];
            if (button.Enabled)
            {
                switch (button.FlagState)
                {
                    case MinesweeperButton.FlagStates.Unmarked:
                        button.FlagState = MinesweeperButton.FlagStates.Flagged;
                        button.Text = "#";
                        break;
                    case MinesweeperButton.FlagStates.Flagged:
                        button.FlagState = MinesweeperButton.FlagStates.Maybe;
                        button.Text = "?";
                        break;
                    case MinesweeperButton.FlagStates.Maybe:
                        button.FlagState = MinesweeperButton.FlagStates.Unmarked;
                        button.Text = "";
                        break;

                }
            }
        }
        internal void TwoButtonClick(int row, int column)
        {

        }

        private void updateLabel(MinesweeperButton button)
        {
            switch (button.BombState)
            {
                case MinesweeperButton.BombStates.Empty:
                    button.Text = "";
                    break;
                case MinesweeperButton.BombStates.One:
                    button.Text = "1";
                    break;
                case MinesweeperButton.BombStates.Two:
                    button.Text = "2";
                    break;
                case MinesweeperButton.BombStates.Three:
                    button.Text = "3";
                    break;
                case MinesweeperButton.BombStates.Four:
                    button.Text = "4";
                    break;
                case MinesweeperButton.BombStates.Five:
                    button.Text = "5";
                    break;
                case MinesweeperButton.BombStates.Six:
                    button.Text = "6";
                    break;
                case MinesweeperButton.BombStates.Seven:
                    button.Text = "7";
                    break;
                case MinesweeperButton.BombStates.Eight:
                    button.Text = "8";
                    break;
                case MinesweeperButton.BombStates.Bomb:
                    button.Text = "*";
                    break;
            }
            button.Refresh();
        }

        private void UpdateAllLabels()
        {
            foreach(MinesweeperButton mb in gamePanel.Controls)
            {
                updateLabel(mb);
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
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Empty;
                                break;
                            case 1:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.One;
                                break;
                            case 2:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Two;
                                break;
                            case 3:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Three;
                                break;
                            case 4:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Four;
                                break;
                            case 5:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Five;
                                break;
                            case 6:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Six;
                                break;
                            case 7:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Seven;
                                break;
                            case 8:
                                gameField[i, j].BombState = MinesweeperButton.BombStates.Eight;
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
            if (gameField[row, column].BombState == MinesweeperButton.BombStates.Bomb) {
                return 1;
            }
            return 0;
        }

        private void GenerateBombs(int rowPositionToExclude, int columnPositionToExclude)
        {
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
                else if (gameField[possibleRow,possibleColumn].BombState == MinesweeperButton.BombStates.Bomb)
                {
                    continue; // bomb here already
                }
                else
                {
                    gameField[possibleRow, possibleColumn].BombState = MinesweeperButton.BombStates.Bomb;
                    bombsToGenerate--;
                }

            }
        }
    }
}
