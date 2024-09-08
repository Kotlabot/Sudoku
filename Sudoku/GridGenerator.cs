using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    internal class GridGenerator
    {
        /// <summary>
        /// Creates grid (grid of cells represented by multidimensional array) according to given size.
        /// </summary>
        /// <returns></returns>
        public static Cell[,] CreateGrid(List<int> possibleValues, int size, int squareX, int squareY, int cellsize, int font)
        {
            Cell[,] grid = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = new Cell(possibleValues, i, j, cellsize, font);

                    //Make the sudoku squares different colours
                    grid[i, j].BackColor = ((i / squareY) + (j / squareX)) % 2 == 0 ? Color.LightSkyBlue : Color.CornflowerBlue;
                    //Event handler for cell when keypressed
                    grid[i, j].KeyPress += ChangeValue;
                }
            }
            return grid;
        }
        /// <summary>
        /// Event handler for cells. When user press a key, verifies if its a number or acceptable letter of alphabet or backspace for deleting the cell text.
        /// </summary>

        public static void ChangeValue(object sender, KeyPressEventArgs e)
        {
            var cell = sender as Cell;
            int value;

            //Insert to cell only if pressed key is number
            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                cell.Text = value.ToString();
                cell.ForeColor = Color.DarkSlateGray;
            }

            //Insert to cell a letter onlt if its A-G
            else if((int)e.KeyChar >= 65 && (int)e.KeyChar <= 71)
            {
                cell.Text = e.KeyChar.ToString();
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
