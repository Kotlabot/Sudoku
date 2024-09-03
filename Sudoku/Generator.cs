using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sudoku
{
    class Generator
    {
        private Sudoku sudoku;
        private int numberOfClues;

        private static Random randomValue = new Random();
        public Generator(Sudoku sudoku, int numberOfClues) 
        {
            this.sudoku = sudoku;
            this.numberOfClues = numberOfClues;
        }

        public Sudoku Generate()
        {
            ClearGrid();

            SetGridToDefault();

            FillDiagonalSquares();

            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                for (int j = 0; j < sudoku.MaxValue; j++)
                {
                    var value = sudoku.Grid[i, j].Value;
                    var possiblevals = sudoku.Grid[i, j].PossibleValues;
                }
            }

            SudokuCore.FillRestOfTheGrid(sudoku, 0, 0);

            MakeRandomCluesVisible(numberOfClues);

            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                for (int j = 0; j < sudoku.MaxValue; j++)
                {
                    var value = sudoku.Grid[i, j].Value;
                    var possiblevals = sudoku.Grid[i, j].PossibleValues;
                }
            }
            return sudoku;
        }

        private void print()
        {
            int i = 0;
            foreach(var cell in sudoku.Grid)
            {
                i++;
                if (i > sudoku.MaxValue)
                {
                    i = 0;
                    Console.WriteLine();
                }

                Console.Write(cell.Value);
            }
        }

        private void ClearGrid()
        {
            sudoku.ClearGridText();
        }

        private void SetGridToDefault()
        {
            sudoku.ClearGrid();
        }

        /// <summary>
        /// Filling diagonal squares 3x3, starting with cell [0, 0], [3, 3] and [6, 6]
        /// </summary>
        private void FillDiagonalSquares()
        {
            int j = 0;
            for (int i = 0; i < sudoku.MaxValue; i = i + sudoku.SquareSizeX)
            {
                FillOneSquare(j, i);
                j += sudoku.SquareSizeY;
            }
        }

        private void FillOneSquare(int row, int column)
        {
            //Make list of possible values for a cell, from which we step by step remove used values
            List<int> possibleValues = sudoku.PossibleValues;
            int index;
            int startColumn = column;

            for (int i = 0; i < sudoku.SquareSizeY; i++)
            {
                for (int j = 0; j < sudoku.SquareSizeX; j++)
                {
                    //randomValue pics one index from defined range, number on that index of a list possibleNumbers is than assigned to cell as a cell.Value
                    index = randomValue.Next(0, possibleValues.Count - 1);
                    var backupIndex = 0;
                    while (!IsPossibleToChoose(sudoku, row, column, possibleValues[index]))
                    {
                        if (backupIndex != index)
                            index = backupIndex;
                        
                        backupIndex++;

                        if (backupIndex == possibleValues.Count)
                            return;
                    }

                    sudoku.Grid[row, column].Value = possibleValues[index];
                    //Storing value as a text in every cell just to make sure the grid loads right. Erase after final compilation.
                    //grid[row, column].Text = possibleValues[index].ToString();

                    SudokuCore.ReduceCellsValues(sudoku, row, column, possibleValues[index]);
                    sudoku.Print(possibleValues[index]);

                    possibleValues.Remove(possibleValues[index]);

                    column++;
                }
                column = startColumn;
                row++;
            }

            print();
        }

        /// <summary>
        /// Method to reduce value that was placed in the grid from lists of possible values of cells in the same row, column and square 3x3
        /// </summary>
        public static bool IsPossibleToChoose(Sudoku sudoku, int row, int column, int value)
        {
            // Reduce this value in the list of possible values of every cell in the same row and column.
            for (int i = 0; i < sudoku.MaxValue; i++)
            {
                if (i != column)
                {
                    if (sudoku.Grid[row, i].PossibleValues.Contains(value) &&
                        sudoku.Grid[row, i].PossibleValues.Count == 1)
                        return false;
                }

                if (i != row)
                {
                    if (sudoku.Grid[i, column].PossibleValues.Contains(value) &&
                        sudoku.Grid[i, column].PossibleValues.Count == 1)
                        return false;
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
                        if (sudoku.Grid[i, j].PossibleValues.Contains(value) &&
                            sudoku.Grid[i, j].PossibleValues.Count == 1)
                            return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Method that makes random clues visible for the user, according to the given difficulty.
        /// </summary>

        private void MakeRandomCluesVisible(int difficulty)
        {
            for (int i = 0; i < difficulty; i++)
            {
                int rowIndex = randomValue.Next(0, sudoku.MaxValue);
                int columnIndex = randomValue.Next(0, sudoku.MaxValue);

                if (sudoku.Grid[rowIndex, columnIndex].Text == string.Empty)
                {
                    sudoku.Grid[rowIndex, columnIndex].SetText(sudoku.Grid[rowIndex, columnIndex].Value);
                    sudoku.Grid[rowIndex, columnIndex].ForeColor = Color.Black;
                    continue;
                }

                i--;
            }
        }

    }
}
