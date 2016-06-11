using System;
using System.Windows.Forms;

namespace Minesweeper
{
    class MinesweeperButton : Button
    {
        public enum BombStates { Empty = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Bomb };
        public enum FlagStates { Unmarked, Flagged, Maybe };
        public enum Visibility { Hidden, Exposed };

        private int row;
        private int column;
        private bool isExposed;
        private BombStates bombState;
        private FlagStates flagState;
        private MinesweeperState gameState;

        public BombStates BombState
        {
            get
            {
                return bombState;
            }

            set
            {
                if (Enum.IsDefined(typeof(BombStates), value)) {
                    bombState = value;
                } else
                {
                    throw new InvalidOperationException(String.Format("\"{0}\" is not a valid value for BombStates Enum", value));
                }
            }
        }

        internal int Row
        {
            get
            {
                return row;
            }

            set
            {
                row = value;
            }
        }

        internal int Column
        {
            get
            {
                return column;
            }

            set
            {
                column = value;
            }
        }

        internal FlagStates FlagState
        {
            get
            {
                return flagState;
            }

            set
            {
                flagState = value;
            }
        }

        public bool IsExposed
        {
            get
            {
                return isExposed;
            }

            set
            {
                isExposed = value;
                if (value)
                {
                    this.FlatStyle = FlatStyle.Popup;
                }
            }
        }

        public MinesweeperButton(int r, int c, MinesweeperState minesweeperState)
        {
            Column = c;
            Row = r;
            gameState = minesweeperState;
            Padding = new Padding(0);
            Margin = new Padding(0);
            MouseDown += new MouseEventHandler(mineSweeperButton_MouseDown);
            MouseUp += new MouseEventHandler(mineSweeperButton_MouseUp);
            MouseLeave += new EventHandler(mineSweeperButton_MouseLeave);
            this.FlatStyle = FlatStyle.System;
            isExposed = false;
        }

        private void mineSweeperButton_MouseDown(object sender, MouseEventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButton_MouseDown(Row, Column, e);
        }

        private void mineSweeperButton_MouseUp(object sender, MouseEventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButton_MouseUp(Row, Column, e);
        }

        private void mineSweeperButton_MouseLeave(object sender, EventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButton_MouseLeave(Row, Column, e);
        }
    }
}
