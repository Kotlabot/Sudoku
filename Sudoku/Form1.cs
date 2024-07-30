using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Data.Common;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        Cell[,] grid = new Cell[9, 9];
        Random randomValue = new Random();
        
        public Form1()
        {
            InitializeComponent();
            CreateGrid();
            GenerateNewGame();
        }

        private void CreateGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new Cell(i, j);

                    //Make the squares 3x3 different colours
                    grid[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.LightSkyBlue : Color.CornflowerBlue;

                    grid[i, j].KeyPress += ChangeValue;
                    Grid.Controls.Add(grid[i, j]);
                }
            }
        }

        private void ChangeValue(object sender, KeyPressEventArgs e)
        {
            var cell = sender as Cell;
            int value;

            //Insert to cell only if pressed key is number
            if (int.TryParse(e.KeyChar.ToString(), out value))
            {
                cell.Text = value.ToString();
                cell.ForeColor = Color.Black;
            }

            //If pressed key is backspace, clear this cell
            else if ((int)e.KeyChar == 8)
            {
                cell.ClearCell();
            }
        }

        private void ClearGrid()
        {
            //Clears Text in every cell in the grid
            foreach (var cell in grid)
            {
                cell.ClearCell();
            }
        }

        private void GenerateNewGame()
        {
            ClearGrid();

            FillDiagonalSquares();

            FillRestOfTheGrid(0, 0);
              
        }

        /// <summary>
        /// Filling diagonal squares 3x3, starting with cell [0, 0], [3, 3] and [6, 6]
        /// </summary>
        private void FillDiagonalSquares()
        {
            for(int i = 0; i < 9; i = i + 3)
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

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //randomValue pics one index from defined range, number on that index of a list possibleNumbers is than assigned to cell as a cell.Value
                    index = randomValue.Next(0, possibleValues.Count - 1);
                    grid[row, column].Value = possibleValues[index];
                    //Storing value as a text in every cell just to make sure the grid loads right. Erase after final compilation.
                    grid[row, column].Text = possibleValues[index].ToString();

                    ReduceCellsValues(row, column, possibleValues[index]);

                    possibleValues.Remove(possibleValues[index]);

                    column++;
                }
                column = startColumn;
                row++;
            }
        }

        private void ReduceCellsValues(int row, int column, int value)
        {
            // Reduce this value in the list of possible values of every cell in the same row and column.
            for (int i = 0; i < 9; i++)
            {
                if (i != column)
                {
                    grid[row, i].PossibleValues.Remove(value);
                }

                if (i != row)
                {
                    grid[i, column].PossibleValues.Remove(value);
                }
            }

            // Reduce this value in the list of possible values of every cell in the same 3x3 square
            int startRow = row - row % 3;
            int startCol = column - column % 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startCol; j < startCol + 3; j++)
                {
                    if (i != row || j != column)
                    {
                        grid[i, j].PossibleValues.Remove(value);
                    }
                }
            }
        }

        /// <summary>
        /// Filling rest of the grid, using DFS and backtracking algorithm, starting in a cell [0, 0].
        /// </summary>
        private bool FillRestOfTheGrid(int row, int column)
        {
            // If row index is equal to or more than 9, whole grid is filled
            if (row >= 9)
            {
                return true; 
            }

            // If column index is equal to or more than 9, move to the next row and set column to zero
            if (column >= 9)
            {
                return FillRestOfTheGrid(row + 1, 0);
            }

            // If a cell is already filled (earlier filled diagonal 3x3 squares), move to the next column
            if (grid[row, column].Value != 0)
            {
                return FillRestOfTheGrid(row, column + 1);
            }

            var possibleValuesCopy = new List<int>(grid[row, column].PossibleValues);

            foreach (var value in possibleValuesCopy)
            {
                // For each value in list of possible values in a cell, check if its correct and assing it to the cell as cell.Value, then reduce this value
                if (IsValidNumber(row, column, value))
                {
                    grid[row, column].Value = value;
                    grid[row, column].Text = value.ToString();
                    ReduceCellsValues(row, column, value);

                    if (FillRestOfTheGrid(row, column + 1))
                    {
                        return true;
                    }

                    // If in the next recursion step there is no possible value to assign, backtrack to the last safe cell and set its value to zero
                    grid[row, column].Value = 0;
                    // When backtracking, restore earlier removed values from lists of possible values
                    RestoreCellsValues(row, column, value);
                }
            }

            return false; 
        }

        private bool IsValidNumber(int row, int column, int value)
        {
            // Check if the value can be placed in the cell (row, column)
            for (int i = 0; i < 9; i++)
            {
                if (grid[row, i].Value == value || grid[i, column].Value == value)
                {
                    return false;
                }
            }


            // Check if the value can be placed in the cell (square 3x3)
            int startRow = row - row % 3;
            int startColumn = column - column % 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startColumn; j < startColumn + 3; j++)
                {
                    if (grid[i, j].Value == value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void RestoreCellsValues(int row, int column, int value)
        {
            // Restore this value in the list of possible values of every cell in the same row and column.
            for (int i = 0; i < 9; i++)
            {
                if (i != column)
                {
                    if (!grid[row, i].PossibleValues.Contains(value))
                    {
                        grid[row, i].PossibleValues.Add(value);
                    }
                }

                if (i != row)
                {
                    if (!grid[i, column].PossibleValues.Contains(value))
                    {
                        grid[i, column].PossibleValues.Add(value);
                    }
                }
            }

            // Restore this value in the list of possible values of every cell in the same 3x3 square
            int startRow = row - row % 3;
            int startColumn = column - column % 3;

            for (int i = startRow; i < startRow + 3; i++)
            {
                for (int j = startColumn; j < startColumn + 3; j++)
                {
                    if (i != row || j != column)
                    {
                        if (!grid[i, j].PossibleValues.Contains(value))
                        {
                            grid[i, j].PossibleValues.Add(value);
                        }
                    }
                }
            }
        }
    }
}
