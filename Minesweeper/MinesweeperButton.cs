using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class MinesweeperButton : Button
    {
        private int row;
        private int column;
        private MinesweeperState gameState;

        public enum states {Empty, One, Two, Three, Four, Five, Six, Seven, Eight, Bomb};

        public MinesweeperButton(int c, int r, MinesweeperState minesweeperState)
        {
            column = c;
            row = r;
            gameState = minesweeperState;
            Padding = new Padding(0);
            Margin = new Padding(0);
            Click += new EventHandler(mineSweeperButton_Click);
        }

        private void mineSweeperButton_Click(object sender, EventArgs e)
        {
            ((MinesweeperButton)sender).gameState.GameButtonClick(row, column);
        }
    }
}
