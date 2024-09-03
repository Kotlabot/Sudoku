using System.Collections.Generic;
using System.IO;
using System;

namespace Sudoku
{
    public enum SudokuType
    {
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
            get { return new List<int> (possibleValues); }
            private set { possibleValues = value; } 
        } 

        public int MaxValue { get; private set; }
        public int SquareSizeX {  get; private set; }
        public int SquareSizeY { get; private set; }

        public int MinKeysCount { get; private set; }
        public int MaxKeysCount { get; private set; }

        public Sudoku(SudokuType type)
        {
            switch (type) 
            {
                case SudokuType.x4:
                    Type = type;
                    PossibleValues = new List<int>() {1, 2, 3, 4};
                    MaxValue = 4;
                    SquareSizeX = 2;
                    SquareSizeY = 2;
                    MinKeysCount = 4;
                    MaxKeysCount = 6;
                    break;

                case SudokuType.x6:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6};
                    MaxValue = 6;
                    SquareSizeX = 3;
                    SquareSizeY = 2;
                    MinKeysCount = 10;
                    MaxKeysCount = 16;
                    break;

                case SudokuType.x8:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8};
                    MaxValue = 8;
                    SquareSizeX = 4;
                    SquareSizeY = 2;
                    MinKeysCount = 16;
                    MaxKeysCount = 32;
                    break;

                case SudokuType.x9:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
                    MaxValue = 9;
                    SquareSizeX = 3;
                    SquareSizeY = 3;
                    MinKeysCount = 20;
                    MaxKeysCount = 45;
                    break;

                case SudokuType.x10:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
                    MaxValue = 10;
                    SquareSizeX = 5;
                    SquareSizeY = 2;
                    MinKeysCount = 45;
                    MaxKeysCount = 70;
                    break;

                case SudokuType.x12:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
                    MaxValue = 12;
                    SquareSizeX = 4;
                    SquareSizeY = 3;
                    MinKeysCount = 72;
                    MaxKeysCount = 95;
                    break;

                case SudokuType.x16:
                    Type = type;
                    PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
                    MaxValue = 16;
                    SquareSizeX = 4;
                    SquareSizeY = 4;
                    MinKeysCount = 82;
                    MaxKeysCount = 155;
                    break;
            }

            Grid = GridGenerator.CreateGrid(PossibleValues, MaxValue, SquareSizeX, SquareSizeY);
        }

        public void ClearGridText()
        {
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

        public void Print(int value)
        {
            using (var sw = File.AppendText("output.txt"))
            {
                sw.WriteLine("Test:");
                sw.WriteLine(value);
                var i = 0;
                foreach (var cell in Grid)
                {
                    if (i == MaxValue)
                    {
                        i = 0;
                        sw.WriteLine();
                    }
                    sw.Write($"[ {String.Join(", ", cell.PossibleValues.ToArray())} ]");
                    i++;
                }
                sw.WriteLine("End");
                sw.WriteLine("End");
                sw.WriteLine("End");
            }
        }
    }
}
