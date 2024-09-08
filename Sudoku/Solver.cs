using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    public class Solver
    {
        private Sudoku sudoku;

        public Solver(Sudoku sudoku) 
        {
            this.sudoku = new Sudoku(sudoku);
        }

        public Sudoku Solve()
        {
            SetGridToDefault();
            //If users input is incorrect (one of the proccesed values is already placed in row, column or square), throw error message.
            if (!SetUsersInput())
            {
                MessageBox.Show("This Sudoku is insolvable.");
                return null;
            }

            //If algorithm was not able to find any solution, throws message error.
            if (!SudokuCore.FillRestOfTheGrid(sudoku, 0, 0))
            {
                MessageBox.Show("This Sudoku is insolvable.");
                return null;
            }

            MakeAllCellsVisible();
            return sudoku;
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
        private bool SetUsersInput()
        {
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                for (int j = 0; j < sudoku.MaxValue; j++)
                {
                    if (sudoku.Grid[i, j].Text != string.Empty)
                    {
                        int value = sudoku.Grid[i, j].GetFromText();
                        //If the input is incorrect, return false and throw error message
                        if (SudokuCore.IsValidNumber(sudoku, i, j, value))
                        {
                            sudoku.Grid[i, j].Value = value;
                            // Reduce given keys from lists of possible values of other cells
                            SudokuCore.ReduceCellsValues(sudoku, i, j, sudoku.Grid[i, j].Value);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
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
