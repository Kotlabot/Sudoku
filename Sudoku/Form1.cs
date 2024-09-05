using System.Drawing;
using System.Windows.Forms;
using System;
using System.Linq;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        Sudoku sudoku;
        Random randomValue = new Random();
        int numberOfClues;
        bool isSelectedInternally;
        
        public Form1()
        {
            InitializeComponent();
            CreateGrid(SudokuType.x9);
        }

        private void CreateGrid(SudokuType type)
        {
            Grid.Controls.Clear();

            sudoku = new Sudoku(type);
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
                    MessageBox.Show(string.Format("Solution is incorrect. There are {0} mistakes, which are highlated in red, or empty spaces.", numberOfMistakes));
                }
                else
                {
                    MessageBox.Show(string.Format("Solution is incorrect. There is {0} mistake, which is highlated in red, or empty space.", numberOfMistakes));
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
            var solved = solver.Solve();

            if(solved != null)
            {
                Grid.Controls.Clear();
                sudoku = solved;

                foreach (var cell in sudoku.Grid)
                {
                    Grid.Controls.Add(cell);
                }
            }
        }

        /// <summary>
        /// Method to clear text in all cells, so user can input new puzzle to solve.
        /// </summary>
        private void buttonClearGrid_Click(object sender, EventArgs e)
        {
            sudoku.ClearGridText();
        }

        /// <summary>
        /// Method to change sudoku type when user choose different size in ComboBox.
        /// </summary>
        private void sudokuSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If ComboBox value was changed because of loading sudoku from a text file, dont generate new grid.
            if (isSelectedInternally)
            {
                isSelectedInternally = false;
                return;
            }

            Grid.Controls.Clear();
            switch(sudokuSize.Text) 
            {
                case "Sudoku 4 x 4":
                    CreateGrid(SudokuType.x4);
                    break;
                case "Sudoku 6 x 6":
                    CreateGrid(SudokuType.x6);
                    break;
                case "Sudoku 8 x 8":
                    CreateGrid(SudokuType.x8);
                    break;
                case "Sudoku 9 x 9":
                    CreateGrid(SudokuType.x9);
                    break;
                case "Sudoku 10 x 10":
                    CreateGrid(SudokuType.x10);
                    break;
                case "Sudoku 12 x 12":
                    CreateGrid(SudokuType.x12);
                    break;
                case "Sudoku 16 x 16":
                    CreateGrid(SudokuType.x16);
                    break;
            }
        }

        /// <summary>
        /// Method to set right value in ComboBox "sudokuSize" when loading sudoku grid from text file.
        /// </summary>
        private void SetSelector(Sudoku sudoku)
        {
            isSelectedInternally = true;
            switch (sudoku.Type)
            {
                case SudokuType.x4:
                    sudokuSize.SelectedIndex = 0;
                    break;
                case SudokuType.x6:
                    sudokuSize.SelectedIndex = 1;
                    break;
                case SudokuType.x8:
                    sudokuSize.SelectedIndex = 2;
                    break;
                case SudokuType.x9:
                    sudokuSize.SelectedIndex = 3;
                    break;
                case SudokuType.x10:
                    sudokuSize.SelectedIndex = 4;
                    break;
                case SudokuType.x12:
                    sudokuSize.SelectedIndex = 5;
                    break;
                case SudokuType.x16:
                    sudokuSize.SelectedIndex = 6;
                    break;
            }
        }

        /// <summary>
        /// Method to save Sudoku grid as text file.
        /// </summary>
        private void saveButton_Click(object sender, EventArgs e)
        {
            sudoku.SaveSudoku();
        }

        /// <summary>
        /// Method to load Sudoku grid from text file.
        /// </summary>
        private void loadButton_Click(object sender, EventArgs e)
        {
            Grid.Controls.Clear();

            var loaded = Sudoku.LoadSudoku();
            if (loaded != null)
            {
                sudoku = loaded;
                foreach (var cell in sudoku.Grid)
                {
                    Grid.Controls.Add(cell);
                }
                SetSelector(sudoku);
            }
        }
    }
}
