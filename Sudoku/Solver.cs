using System;
using System.Drawing;

namespace Sudoku
{
    public class Solver
    {
        private Cell[,] grid;

        public Solver(Cell[,] grid) 
        {
            this.grid = grid;
        }

        public void Solve()
        {
            SetGridToDefault();
            SetUsersInput();
            SudokuCore.FillRestOfTheGrid(grid, 0, 0);
            MakeAllCellsVisible();
        }

        /// <summary>
        /// Method to clear cells values and restore whole list of possible values
        /// </summary>

        private void SetGridToDefault()
        {
            foreach (var cell in grid)
            {
                cell.Reset();
            }
        }

        /// <summary>
        /// Method to process users input of keys and store them as cells values
        /// </summary>
        private void SetUsersInput()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i, j].Text != string.Empty)
                    {
                        grid[i, j].Value = Convert.ToInt32(grid[i, j].Text);
                        // Reduce given keys from lists of possible values of other cells
                        SudokuCore.ReduceCellsValues(grid, i, j, grid[i, j].Value);
                    }
                }
            }
        }

        /// <summary>
        /// When Sudoku puzzle is solved, make all numbers visible to the user.
        /// </summary>
        private void MakeAllCellsVisible()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (grid[i, j].Text == string.Empty)
                    {
                        grid[i, j].Text = Convert.ToString(grid[i, j].Value);
                        // Make solved numbers white so user can see what keys were given from them and what keys were solved by computer.
                        grid[i, j].ForeColor = Color.White;
                    }
                }
            }
        }
    }
}
