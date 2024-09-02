using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Data.Common;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        Cell[,] grid;
        Random randomValue = new Random();
        int numberOfClues;
        
        public Form1()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            grid = GridGenerator.CreateGrid9x9();
            foreach (var cell in grid)
            {
                Grid.Controls.Add(cell);
            }
        }

        private void GenerateNewGame()
        {
            var generator = new Generator(grid, numberOfClues);
            grid = generator.Generate();  
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
            else if (newNumberOfClues > 45)
            {
                MessageBox.Show("Maximum number of clues is 45. More than 45 would be extremely easy.");
            }

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

        private void solveButton_Click(object sender, EventArgs e)
        {
            var solver = new Solver(grid);
            solver.Solve();
        }

        /// <summary>
        /// Method to clear text in all cells, so user can input new puzzle to solve.
        /// </summary>
        private void buttonClearGrid_Click(object sender, EventArgs e)
        {
            foreach(var cell in grid)
            {
                cell.ClearText();
            }
        }
    }
}
