using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    internal class GridGenerator
    {
        public static Cell[,] CreateGrid(List<int> possibleValues, int size, int squareX, int squareY)
        {
            Cell[,] grid = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new Cell(possibleValues, i, j);

                    //Make the squares 3x3 different colours
                    grid[i, j].BackColor = ((i / squareY) + (j / squareX)) % 2 == 0 ? Color.LightSkyBlue : Color.CornflowerBlue;

                    grid[i, j].KeyPress += ChangeValue;
                }
            }
            return grid;
        }
        private static void ChangeValue(object sender, KeyPressEventArgs e)
        {
            var cell = sender as Cell;
            int value;

            //Insert to cell only if pressed key is number
            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                cell.Text = value.ToString();
                cell.ForeColor = Color.DarkSlateGray;
            }

            //If pressed key is backspace, clear this cell
            else if ((int)e.KeyChar == 8)
            {
                cell.ClearText();
            }
        }
    }
}
