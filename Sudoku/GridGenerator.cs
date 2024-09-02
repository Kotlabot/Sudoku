using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    internal class GridGenerator
    {
        public static Cell[,] CreateGrid9x9()
        {
            Cell[,] grid = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new Cell(i, j);

                    //Make the squares 3x3 different colours
                    grid[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.LightSkyBlue : Color.CornflowerBlue;

                    grid[i, j].KeyPress += ChangeValue;
                }
            }
            return grid;
        }

        //public static Cell[,] CreateGrid4x4()
        //{
        //    Cell[,] grid = new Cell[4, 4];
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            grid[i, j] = new Cell(i, j);

        //            //Make the squares 3x3 different colours
        //            grid[i, j].BackColor = ((i / 2) + (j / 2)) % 2 == 0 ? Color.LightSkyBlue : Color.CornflowerBlue;

        //            grid[i, j].KeyPress += ChangeValue;
        //        }
        //    }
        //    return grid;
        //}

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
