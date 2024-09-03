using System.Drawing;
using System.Windows.Forms;
using System;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        Sudoku sudoku;
        Random randomValue = new Random();
        int numberOfClues;
        
        public Form1()
        {
            InitializeComponent();
            CreateGrid();
        }

        private void CreateGrid()
        {
            sudoku = new Sudoku(SudokuType.x9);
            foreach (var cell in sudoku.Grid)
            {
                Grid.Controls.Add(cell);
            }
        }

        private void GenerateNewGame()
        {
            var generator = new Generator(sudoku, numberOfClues);
            generator.Generate();  
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
                numberOfClues = randomValue.Next(sudoku.MinKeysCount, sudoku.MaxKeysCount);
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
            if (newNumberOfClues < sudoku.MinKeysCount)
            {
                MessageBox.Show($"Minimum number of clues is {sudoku.MinKeysCount}. Less than {sudoku.MinKeysCount} would be extremely hard.");
            }

            // If difficulty is more than maximum boundary, throws error.
            else if (newNumberOfClues > sudoku.MaxKeysCount)
            {
                MessageBox.Show($"Maximum number of clues is {sudoku.MaxKeysCount}. More than {sudoku.MaxKeysCount} would be extremely easy.");
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

            for(int i = 0; i < sudoku.MaxValue; i++)
            {
                for(int j = 0; j < sudoku.MaxValue; j++)
                {
                    // If there is a misktake, highlight it in red color and increase counter of mistakes by 1.
                    if (sudoku.Grid[i, j].GetText(sudoku.Grid[i, j].Value) != sudoku.Grid[i, j].Text)
                    {
                        sudoku.Grid[i, j].ForeColor = Color.Red;
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

        /// <summary>
        /// Method to trigger Sudoku Solver when user press "Solve!" button
        /// </summary>

        private void solveButton_Click(object sender, EventArgs e)
        {
            var solver = new Solver(sudoku);
            solver.Solve();
        }

        /// <summary>
        /// Method to clear text in all cells, so user can input new puzzle to solve.
        /// </summary>
        private void buttonClearGrid_Click(object sender, EventArgs e)
        {
            sudoku.ClearGridText();
        }
    }
}
