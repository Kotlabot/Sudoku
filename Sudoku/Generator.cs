using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sudoku
{
    class Generator
    {
        private Cell[,] grid;
        private int numberOfClues;

        private static Random randomValue = new Random();
        public Generator(Cell[,] grid, int numberOfClues) 
        {
            this.grid = grid;
            this.numberOfClues = numberOfClues;
        }

        public Cell[,] Generate()
        {
            ClearGrid();

            SetGridToDefault();

            FillDiagonalSquares();

            SudokuCore.FillRestOfTheGrid(grid, 0, 0);

            MakeRandomCluesVisible(numberOfClues);

            return grid;
        }

        private void ClearGrid()
        {
            //Clears Text in every cell in the grid
            foreach (var cell in grid)
            {
                cell.ClearText();
            }
        }

        private void SetGridToDefault()
        {
            //Reset every cell: set cell.Value to default and restore the whole list of possible values.
            foreach (var cell in grid)
            {
                cell.Reset();
            }
        }

        /// <summary>
        /// Filling diagonal squares 3x3, starting with cell [0, 0], [3, 3] and [6, 6]
        /// </summary>
        private void FillDiagonalSquares()
        {
            for (int i = 0; i < 9; i = i + 3)
            {
                FillOneSquare(i, i);
            }
        }

        private void FillOneSquare(int row, int column)
        {
            //Make list of possible values for a cell, from which we step by step remove used values
            List<int> possibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int index;
            int startColumn = column;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //randomValue pics one index from defined range, number on that index of a list possibleNumbers is than assigned to cell as a cell.Value
                    index = randomValue.Next(0, possibleValues.Count - 1);
                    grid[row, column].Value = possibleValues[index];
                    //Storing value as a text in every cell just to make sure the grid loads right. Erase after final compilation.
                    //grid[row, column].Text = possibleValues[index].ToString();

                    SudokuCore.ReduceCellsValues(grid, row, column, possibleValues[index]);

                    possibleValues.Remove(possibleValues[index]);

                    column++;
                }
                column = startColumn;
                row++;
            }
        }
        
        /// <summary>
        /// Method that makes random clues visible for the user, according to the given difficulty.
        /// </summary>

        private void MakeRandomCluesVisible(int difficulty)
        {
            for (int i = 0; i < difficulty; i++)
            {
                int rowIndex = randomValue.Next(0, 8);
                int columnIndex = randomValue.Next(0, 8);

                if (grid[rowIndex, columnIndex].Text == string.Empty)
                {
                    grid[rowIndex, columnIndex].Text = grid[rowIndex, columnIndex].Value.ToString();
                    grid[rowIndex, columnIndex].ForeColor = Color.Black;
                    continue;
                }

                i--;
            }
        }

    }
}
