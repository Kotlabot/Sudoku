using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sudoku
{
    //Making enum to resolve different sudoku types of grids - 4x4, 6x6...
    public enum SudokuType
    {
        empty,
        x4,
        x6,
        x8,
        x9,
        x10,
        x12,
        x16
    }

    public class Sudoku
    {
        public SudokuType Type { get; private set; }
        public Cell[,] Grid { get; set; }

        private List<int> possibleValues = new List<int>();
        public List<int> PossibleValues
        {
            get { return new List<int>(possibleValues); }
            private set { possibleValues = value; }
        }

        //Properties which differs according to the sudoku type
        public int MaxValue { get; private set; }
        public int SquareSizeX { get; private set; }
        public int SquareSizeY { get; private set; }

        public int MinKeysCount { get; private set; }
        public int MaxKeysCount { get; private set; }
        public int CellSize { get; private set; }
        public int CellFont { get; private set; }

        //Copy constructor for solver
        public Sudoku(Sudoku sudoku)
        {
            SetSudoku(sudoku.Type);
            Cell[,] gridCopy = new Cell[MaxValue, MaxValue];
            for (int i = 0; i < MaxValue; i++)
            {
                for (int j = 0; j < MaxValue; j++)
                {
                    gridCopy[i, j] = new Cell(sudoku.Grid[i, j]);
                }
            }
            Grid = gridCopy;
        }

        public Sudoku(SudokuType type)
        {
            SetSudoku(type);
            Grid = GridGenerator.CreateGrid(PossibleValues, MaxValue, SquareSizeX, SquareSizeY, CellSize, CellFont);
        }

        //Setting to every sudoku type correct values
        public void SetSudoku(SudokuType type)
        {
            switch (type)
            {
                case SudokuType.x4:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4 };
                    MaxValue = 4;
                    SquareSizeX = 2;
                    SquareSizeY = 2;
                    MinKeysCount = 4;
                    MaxKeysCount = 6;
                    CellSize = 100;
                    CellFont = 40;
                    break;

                case SudokuType.x6:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6 };
                    MaxValue = 6;
                    SquareSizeX = 3;
                    SquareSizeY = 2;
                    MinKeysCount = 10;
                    MaxKeysCount = 16;
                    CellSize = 67;
                    CellFont = 30;
                    break;

                case SudokuType.x8:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
                    MaxValue = 8;
                    SquareSizeX = 4;
                    SquareSizeY = 2;
                    MinKeysCount = 16;
                    MaxKeysCount = 32;
                    CellSize = 50;
                    CellFont = 23;
                    break;

                case SudokuType.x9:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                    MaxValue = 9;
                    SquareSizeX = 3;
                    SquareSizeY = 3;
                    MinKeysCount = 20;
                    MaxKeysCount = 45;
                    CellSize = 45;
                    CellFont = 23;
                    break;

                case SudokuType.x10:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    MaxValue = 10;
                    SquareSizeX = 5;
                    SquareSizeY = 2;
                    MinKeysCount = 45;
                    MaxKeysCount = 70;
                    CellSize = 40;
                    CellFont = 20;
                    break;

                case SudokuType.x12:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    MaxValue = 12;
                    SquareSizeX = 4;
                    SquareSizeY = 3;
                    MinKeysCount = 72;
                    MaxKeysCount = 95;
                    CellSize = 33;
                    CellFont = 16;
                    break;

                case SudokuType.x16:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                    MaxValue = 16;
                    SquareSizeX = 4;
                    SquareSizeY = 4;
                    MinKeysCount = 130;
                    MaxKeysCount = 180;
                    CellSize = 25;
                    CellFont = 12;
                    break;
            }
        }

        public void ClearGridText()
        {
            //Clear cell.Text from every cell
            foreach (var cell in Grid)
            {
                cell.ClearText();
            }
        }

        public void ClearGrid()
        {
            //Reset every cell: set cell.Value to default and restore the whole list of possible values.
            foreach (var cell in Grid)
            {
                cell.Reset();
            }
        }

        /// <summary>
        /// Method to save Sudoku grid by using jagged array to store one line of sudoku grid as one array, 
        /// then join one array as one line, with comma as separator, of text file using StreamWriter.
        /// </summary>
        public void SaveSudoku()
        {
            string[][] sudokuText = new string[MaxValue][];
            for (int i = 0; i < MaxValue; i++)
            {
                string[] oneLine = new string[MaxValue];
                for (int j = 0; j < MaxValue; j++)
                {
                    //If a cell is empty (not solved yet), store it as underscore.
                    if (Grid[i, j].Text == string.Empty)
                        oneLine[j] = "_";
                    else
                        oneLine[j] = Grid[i, j].Text;
                }
                sudokuText[i] = oneLine;
            }

            using (StreamWriter writer = new StreamWriter(GetName()))
            {
                foreach (string[] line in sudokuText)
                {
                    string oneLine = string.Join(",", line);
                    writer.WriteLine(oneLine);
                }
            }

        }

        /// <summary>
        /// Method to pick correct name for a new text file by iterating through existing files and increasing number of file by 1.
        /// </summary>
        public string GetName()
        {
            int counter = 1;
            string name = $"sudoku{counter}.txt";
            while (File.Exists(name))
            {
                name = $"sudoku{++counter}.txt";
            }
            return name;
        }

        /// <summary>
        /// Method to load existing text file as Sudoku grid.
        /// </summary>
        public static Sudoku LoadSudoku()
        {
            string path = GetPath();
            if (path == null)
            {
                return null;
            }

            //Store text file as string array - one line is one string. 
            string[] allLines = File.ReadAllLines(path);
            int sudokuSize = allLines.Length;

            //Get Sudoku type according to number of lines in string array "allLines". If its not acceptale number (size of sudoku), return null.
            if (GetType(sudokuSize) == SudokuType.empty)
            {
                MessageBox.Show("Input is incorrect!");
                return null;
            }

            Sudoku sudoku = new Sudoku(GetType(sudokuSize));
            for (int i = 0; i < sudokuSize; i++)
            {
                string[] oneLine = allLines[i].Split(',');
                if (oneLine.Length != sudokuSize)
                {
                    MessageBox.Show("Input is incorrect!");
                    return null;
                }

                for (int j = 0; j < sudokuSize; j++)
                {
                    oneLine[j] = oneLine[j].Trim(' ');
                    //If size of one line is not same as size of all grid it means that sudoku grid is not valid. In this case show error message.
                    //If a character in file is not number, but underscore, is means that corresponding cell is empty (not solved)
                    if (oneLine[j].Count() != 1)
                    {
                        MessageBox.Show("Input is incorrect!");
                        return null;
                    }

                    if (oneLine[j] == "_")
                    {
                        sudoku.Grid[i, j].Text = string.Empty;
                    }
                    else
                    {
                        if ((oneLine[j][0] >= '1' && oneLine[j][0] <= '9') ||
                            (oneLine[j][0] >= 'A' && oneLine[j][0] <= 'G'))
                        {
                            sudoku.Grid[i, j].Text = oneLine[j];
                        }
                        else 
                        {
                            MessageBox.Show("Input is incorrect!");
                            return null;
                        }
                    }
                }

            }

            return sudoku;

        }

        /// <summary>
        /// Method to get SudokuType according to size - number of lines in string array "allLines".
        /// </summary>
        public static SudokuType GetType(int size)
        {
            switch (size)
            {
                case 4:
                    return SudokuType.x4;
                case 6:
                    return SudokuType.x6;
                case 8:
                    return SudokuType.x8;
                case 9:
                    return SudokuType.x9;
                case 10:
                    return SudokuType.x10;
                case 12:
                    return SudokuType.x12;
                case 16:
                    return SudokuType.x16;
            }
            return SudokuType.empty;
        }

        /// <summary>
        /// Method to get path to a text file. Created based upon MSDOC.
        /// </summary>
        public static string GetPath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }

            return null;
        }
    }
}
