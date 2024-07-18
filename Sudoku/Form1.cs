using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

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
            int value;
            int startColumn = column;

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    //randomValue pics one index from defined range, number on that index of a list possibleNumbers is than assigned to cell as a cell.Value
                    value = randomValue.Next(0, possibleValues.Count - 1);
                    grid[row, column].Value = possibleValues[value];
                    //Storing value as a text in every cell just to make sure hte grid loads right. Erase after final compilation.
                    grid[row, column].Text = possibleValues[value].ToString();
                    possibleValues.Remove(possibleValues[value]);

                    ReduceCellsValues(row, column, value);

                    column++;
                }
                column = startColumn;
                row++;
            }
        }

        private void ReduceCellsValues(int row, int column, int value)
        {
            //After assigning a value to one cell, reduce this value in list of possible values of every cell in the same row and column.
            for(int i = 0; i < 9; i++)
            {
                if(i != column)
                {
                    grid[row, i].PossibleValues.Remove(value);
                }

                if (i != row)
                {
                    grid[i, column].PossibleValues.Remove(value);
                }
            }

            //Reduce this value in list of possible values of every cell in the same square 3x3
            for (int i = row - (row % 3); i < row - (row % 3) + 3; i++)
            {
                for (int j = column - (column % 3); j < column - (column % 3) + 3; j++)
                {
                    if (i != row && j != column)
                    {
                        grid[i, j].PossibleValues.Remove(value);
                    }

                }
            }
        }

        // // // // Work in progress // // // //
        private bool FindValueForOneCell(int x, int y, List<int> possibleNumbers)
        {
            Random randomNumber = new Random();
            var value = randomNumber.Next(possibleNumbers.Count);

            while (!IsValidNumber(x, y, value))
            {
                if (possibleNumbers.Count < 1)
                {
                    return false;
                }
                possibleNumbers.Remove(value);
                FindValueForOneCell(x, y, possibleNumbers);
            }

            grid[x, y].Value = value;
            grid[x, y].Text = value.ToString();

            return true;
        }

        private bool IsValidNumber(int x, int y, int value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i != y && grid[i, y].Value == value)
                {
                    return false;
                }
            }

            for (int j = 0; j < 9; ++j)
            {
                if (j != x && grid[x, j].Value == value)
                {
                    return false;
                }
            }

            for (int i = x - (x % 3); i < x - (x % 3) + 3; i++)
            {
                for (int j = y - (y % 3); j < y - (y % 3) + 3; j++)
                {
                    if (i != x && j != y && grid[i, j].Value == value)
                    {
                        return false;
                    }

                }
            }

            return true;
        }
    }
}
