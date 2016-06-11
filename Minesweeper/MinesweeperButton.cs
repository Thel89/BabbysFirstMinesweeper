using System;
using System.Windows.Forms;

namespace Minesweeper
{
    class MinesweeperButton : Button
    {
        internal int row;
        internal int column;
        private MinesweeperState gameState;

        public enum BombState { Empty, One, Two, Three, Four, Five, Six, Seven, Eight, Bomb };
        public enum FlagState { Unmarked, Maybe, Flagged };
        public enum Visibility { Hidden, Exposed };

        public MinesweeperButton(int c, int r, MinesweeperState minesweeperState)
        {
            column = c;
            row = r;
            gameState = minesweeperState;
            Padding = new Padding(0);
            Margin = new Padding(0);
            MouseDown += new MouseEventHandler(mineSweeperButton_MouseDown);
            MouseUp += new MouseEventHandler(mineSweeperButton_MouseUp);
            MouseLeave += new EventHandler(mineSweeperButton_MouseLeave);

        }

        private void mineSweeperButton_MouseDown(object sender, MouseEventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButton_MouseDown(row, column, e);
        }

        private void mineSweeperButton_MouseUp(object sender, MouseEventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButton_MouseUp(row, column, e);
        }

        private void mineSweeperButton_MouseLeave(object sender, EventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButton_MouseLeave(row, column, e);
        }
    }
}
