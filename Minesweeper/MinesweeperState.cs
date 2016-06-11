using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Minesweeper
{
    class MinesweeperState
    {
        private int columnCount, rowCount, bombCount, gameState;
        private TableLayoutPanel gamePanel;
        private int[,] gameField;
        private bool left_down, right_down, both_click;
        private bool isDebug;

        public enum GameStates : int { NotStarted, InProgress, Won, Lost };

        public MinesweeperState(TableLayoutPanel gamePanel, int rowCount, int columnCount, int bombCount, bool isDebug)
        {
            gameState = (int)GameStates.NotStarted;
            this.gamePanel = gamePanel;
            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.bombCount = bombCount;
            this.isDebug = isDebug;

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
                        UpdateLabels();
                    }
                    goto case (int)GameStates.InProgress; // I feel horrible for doing this
                case (int)GameStates.InProgress:
                    
                    if (gameField[row, column] == (int)MinesweeperButton.BombState.Bomb)
                    {
                        MessageBox.Show("You lose");
                        gameState = (int)GameStates.Lost;
                        //todo: stats and shit
                        //todo: expose all mines
                    }
                    else
                    {
                        foreach (MinesweeperButton mb in gamePanel.Controls)
                        {

                        }
                        if (gameField[row, column] == (int)MinesweeperButton.BombState.Empty)
                        {

                        }
                    }
                    break;
            }

        }
        internal void RightButtonClick(int row, int column)
        {
            //todo: handle 'flagged' plus 'maybe'. Will have to rethink all the things
        }
        internal void TwoButtonClick(int row, int column)
        {

        }

        private void UpdateLabels()
        {
            foreach(MinesweeperButton mb in gamePanel.Controls)
            {
                int state = gameField[mb.row, mb.column];
                switch (state)
                {
                    case (int)MinesweeperButton.BombState.Empty:
                        mb.Text = "";
                        break;
                    case (int)MinesweeperButton.BombState.One:
                        mb.Text = "1";
                        break;
                    case (int)MinesweeperButton.BombState.Two:
                        mb.Text = "2";
                        break;
                    case (int)MinesweeperButton.BombState.Three:
                        mb.Text = "3";
                        break;
                    case (int)MinesweeperButton.BombState.Four:
                        mb.Text = "4";
                        break;
                    case (int)MinesweeperButton.BombState.Five:
                        mb.Text = "5";
                        break;
                    case (int)MinesweeperButton.BombState.Six:
                        mb.Text = "6";
                        break;
                    case (int)MinesweeperButton.BombState.Seven:
                        mb.Text = "7";
                        break;
                    case (int)MinesweeperButton.BombState.Eight:
                        mb.Text = "8";
                        break;
                    case (int)MinesweeperButton.BombState.Bomb:
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
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Empty);
                                break;
                            case 1:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.One);
                                break;
                            case 2:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Two);
                                break;
                            case 3:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Three);
                                break;
                            case 4:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Four);
                                break;
                            case 5:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Five);
                                break;
                            case 6:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Six);
                                break;
                            case 7:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Seven);
                                break;
                            case 8:
                                gameField[i, j] = ((int)MinesweeperButton.BombState.Eight);
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
            if (gameField[row, column] == ((int)MinesweeperButton.BombState.Bomb)) {
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
                else if (gameField[possibleRow,possibleColumn] == ((int)MinesweeperButton.BombState.Bomb))
                {
                    continue; // bomb here already
                }
                else
                {
                    gameField[possibleRow, possibleColumn] = ((int)MinesweeperButton.BombState.Bomb);
                    bombsToGenerate--;
                }

            }
        }
    }
}
