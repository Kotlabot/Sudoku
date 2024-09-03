using System;
using System.Drawing;

namespace Sudoku
{
    public class Solver
    {
        private Sudoku sudoku;

        public Solver(Sudoku sudoku) 
        {
            this.sudoku = sudoku;
        }

        public void Solve()
        {
            SetGridToDefault();
            SetUsersInput();
            SudokuCore.FillRestOfTheGrid(sudoku, 0, 0);
            MakeAllCellsVisible();
        }

        /// <summary>
        /// Method to clear cells values and restore whole list of possible values
        /// </summary>

        private void SetGridToDefault()
        {
            sudoku.ClearGrid();
        }

        /// <summary>
        /// Method to process users input of keys and store them as cells values
        /// </summary>
        private void SetUsersInput()
        {
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                for (int j = 0; j < sudoku.MaxValue; j++)
                {
                    if (sudoku.Grid[i, j].Text != string.Empty)
                    {
                        sudoku.Grid[i, j].Value = sudoku.Grid[i, j].GetFromText();
                        // Reduce given keys from lists of possible values of other cells
                        SudokuCore.ReduceCellsValues(sudoku, i, j, sudoku.Grid[i, j].Value);
                    }
                }
            }
        }

        /// <summary>
        /// When Sudoku puzzle is solved, make all numbers visible to the user.
        /// </summary>
        private void MakeAllCellsVisible()
        {
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                for (int j = 0; j < sudoku.MaxValue; j++)
                {
                    if (sudoku.Grid[i, j].Text == string.Empty)
                    {
                        sudoku.Grid[i, j].SetText(sudoku.Grid[i, j].Value);
                        // Make solved numbers white so user can see what keys were given from them and what keys were solved by computer.
                        sudoku.Grid[i, j].ForeColor = Color.White;
                    }
                }
            }
        }
    }
}
