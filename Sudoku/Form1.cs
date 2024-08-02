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
        int numberOfClues;
        
        public Form1()
        {
            InitializeComponent();
            CreateGrid();
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
                cell.ForeColor = Color.DarkSlateGray;
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

            MakeRandomCluesVisible(numberOfClues);
              
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
                    //grid[row, column].Text = possibleValues[index].ToString();

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
                    //grid[row, column].Text = value.ToString();
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

        private void MakeRandomCluesVisible(int difficulty)
        {
            for(int i = 0; i < difficulty; i++)
            {
                int rowIndex = randomValue.Next(0, 8);
                int columnIndex = randomValue.Next(0, 8);

                if(grid[rowIndex, columnIndex].Text == string.Empty)
                {
                    grid[rowIndex, columnIndex].Text = grid[rowIndex, columnIndex].Value.ToString();
                    continue;
                }

                i--;
            }
        }


        /// <summary>
        /// Gereates new Sudoku based on given difficulty (number of shown clues). If no difficulty is given, 
        /// method generates random number of clues.
        /// </summary>
        
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string input = difficultyInput.Text;

            if (input == "")
            {
                numberOfClues = randomValue.Next(20, 45);
                GenerateNewGame();
                return;
            }

            // If input contains non numeric character, throws error.
            else if (!CheckForNonNumericString(input))
            {
                MessageBox.Show("Input difficulty contains non numeric character.");
                return;
            }

            var newNumberOfClues = Convert.ToInt32(input);
            // If difficulty is less than minimum boundary, throws error.
            if (newNumberOfClues < 20)
            {
                MessageBox.Show("Minimum number of clues is 20. Less than 20 would be extremely hard.");
            }

            // If difficulty is more than maximum boundary, throws error.
            //else if (newNumberOfClues > 45)
            //{
            //    MessageBox.Show("Maximum number of clues is 45. More than 45 would be extremely easy.");
            //}

            // If the difficulty input is correct, generate new game with given number of clues.
            else
            {
                numberOfClues = newNumberOfClues;
                GenerateNewGame();
            }
        }
        
        /// <summary>
        /// Method to check if input contains non numeric characters.
        /// </summary>
        private bool CheckForNonNumericString(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Method verifies if user filled Sudoku correctly
        /// </summary>
        private void buttonVerify_Click(object sender, EventArgs e)
        {
            int numberOfMistakes;
            // When Sudoku is not filled properly, throws negative notification including number if mistakes made.
            if (!VerifySudokuResult(out numberOfMistakes))
            {
                if(numberOfMistakes > 1)
                {
                    MessageBox.Show(string.Format("Solution is incorrect. There are {0} mistakes, which are highlated in red.", numberOfMistakes));
                }
                else
                {
                    MessageBox.Show(string.Format("Solution is incorrect. There is {0} mistake, which is highlated in red.", numberOfMistakes));
                }
            }

            //If no mistakes were found and Sudoku is filled properly, throws positive notification.
            else
            {
                MessageBox.Show("Congrats, you win!");
            }
        }

        /// <summary>
        /// Method verifies filled Sudoku grid by checking cell by cell cell's value as cell.Value and compare it to users input as cell.Text
        /// </summary>
        private bool VerifySudokuResult(out int numberOfMistakes)
        {
            int counter = 0;

            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    // If there is a misktake, highlight it in red color and increase counter of mistakes by 1.
                    if (grid[i, j].Value.ToString() != grid[i, j].Text)
                    {
                        grid[i, j].ForeColor = Color.Red;
                        counter++;
                    }
                }
            }

            numberOfMistakes = counter;

            if(counter > 0)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
