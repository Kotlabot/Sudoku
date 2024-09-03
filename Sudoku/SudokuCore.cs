using System.Collections.Generic;

namespace Sudoku
{
    public class SudokuCore
    {
        /// <summary>
        /// Filling rest of the grid, using DFS and backtracking algorithm, starting in a cell [0, 0].
        /// </summary>
        public static bool FillRestOfTheGrid(Sudoku sudoku, int row, int column)
        {
            // If row index is equal to or more than 9, whole grid is filled
            if (row >= sudoku.MaxValue)
            {
                return true;
            }

            // If column index is equal to or more than 9, move to the next row and set column to zero
            if (column >= sudoku.MaxValue)
            {
                return FillRestOfTheGrid(sudoku, row + 1, 0);
            }

            // If a cell is already filled (earlier filled diagonal 3x3 squares), move to the next column
            if (sudoku.Grid[row, column].Value != 0)
            {
                return FillRestOfTheGrid(sudoku, row, column + 1);
            }

            var possibleValuesCopy = new List<int>(sudoku.Grid[row, column].PossibleValues);

            foreach (var value in possibleValuesCopy)
            {
                // For each value in list of possible values in a cell, check if its correct and assing it to the cell as cell.Value, then reduce this value
                if (IsValidNumber(sudoku, row, column, value))
                {
                    sudoku.Grid[row, column].Value = value;
                    //grid[row, column].Text = value.ToString();
                    ReduceCellsValues(sudoku, row, column, value);

                    if (FillRestOfTheGrid(sudoku, row, column + 1))
                    {
                        return true;
                    }

                    // If in the next recursion step there is no possible value to assign, backtrack to the last safe cell and set its value to zero
                    sudoku.Grid[row, column].Value = 0;
                    // When backtracking, restore earlier removed values from lists of possible values
                    RestoreCellsValues(sudoku, row, column, value);
                }
            }

            return false;
        }

        /// <summary>
        /// Method to reduce value that was placed in the grid from lists of possible values of cells in the same row, column and square 3x3
        /// </summary>
        public static void ReduceCellsValues(Sudoku sudoku, int row, int column, int value)
        {
            // Reduce this value in the list of possible values of every cell in the same row and column.
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                if (i != column)
                {
                    sudoku.Grid[row, i].PossibleValues.Remove(value);
                }

                if (i != row)
                {
                    sudoku.Grid[i, column].PossibleValues.Remove(value);
                }
            }

            // Reduce this value in the list of possible values of every cell in the same 3x3 square
            int startRow = row - row % sudoku.SquareSizeY;
            int startCol = column - column % sudoku.SquareSizeX;

            for (int i = startRow; i < startRow + sudoku.SquareSizeY; i++)
            {
                for (int j = startCol; j < startCol + sudoku.SquareSizeX; j++)
                {
                    if (i != row || j != column)
                    {
                        sudoku.Grid[i, j].PossibleValues.Remove(value);
                    }
                }
            }
        }

        /// <summary>
        /// Method to check if number to be placed is not in the same row, column or square 3x3
        /// </summary>
        private static bool IsValidNumber(Sudoku sudoku, int row, int column, int value)
        {
            // Check if the value can be placed in the cell (row, column)
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                if (sudoku.Grid[row, i].Value == value || sudoku.Grid[i, column].Value == value)
                {
                    return false;
                }
            }


            // Check if the value can be placed in the cell (square 3x3)
            int startRow = row - row % sudoku.SquareSizeY;
            int startColumn = column - column % sudoku.SquareSizeX;

            for (int i = startRow; i < startRow + sudoku.SquareSizeY; i++)
            {
                for (int j = startColumn; j < startColumn + sudoku.SquareSizeX; j++)
                {
                    if (sudoku.Grid[i, j].Value == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Method to restore value that was missplaced in the grid to lists of possible values of cells in the same row, column and square 3x3
        /// </summary>
        /// 
        private static void RestoreCellsValues(Sudoku sudoku, int row, int column, int value)
        {
            // Restore this value in the list of possible values of every cell in the same row and column.
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                if (i != column)
                {
                    if (!sudoku.Grid[row, i].PossibleValues.Contains(value))
                    {
                        sudoku.Grid[row, i].PossibleValues.Add(value);
                    }
                }

                if (i != row)
                {
                    if (!sudoku.Grid[i, column].PossibleValues.Contains(value))
                    {
                        sudoku.Grid[i, column].PossibleValues.Add(value);
                    }
                }
            }

            // Restore this value in the list of possible values of every cell in the same 3x3 square
            int startRow = row - row % sudoku.SquareSizeY;
            int startColumn = column - column % sudoku.SquareSizeX;

            for (int i = startRow; i < startRow + sudoku.SquareSizeY; i++)
            {
                for (int j = startColumn; j < startColumn + sudoku.SquareSizeX; j++)
                {
                    if (i != row || j != column)
                    {
                        if (!sudoku.Grid[i, j].PossibleValues.Contains(value))
                        {
                            sudoku.Grid[i, j].PossibleValues.Add(value);
                        }
                    }
                }
            }
        }
    }
}
