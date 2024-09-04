using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
            SudokuCore.FillRestOfTheGrid(sudoku, 0, 0);
            MakeRandomCluesVisible(numberOfClues);
            return sudoku;
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
        /// Filling diagonal squares, starting with cell [0, 0]
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
            //Make list of possible values for a cell, from which we step by step remove used values.
            List<int> possibleValues = sudoku.PossibleValues;
            int index;
            int startColumn = column;

            for (int i = 0; i < sudoku.SquareSizeY; i++)
            {
                for (int j = 0; j < sudoku.SquareSizeX; j++)
                {
                    //randomValue pics one index from defined range, number on that index of a list possibleNumbers is than assigned to cell as a cell.Value.
                    index = randomValue.Next(0, possibleValues.Count - 1);
                    var backupIndex = 0;
                    while (!IsPossibleToChoose(sudoku, row, column, possibleValues[index]))
                    {
                        //If there are some values left, try other value.
                        if (backupIndex != index)
                            index = backupIndex;
                        
                        backupIndex++;

                        //If there are no values left to try, throw error message and return.
                        if (backupIndex == possibleValues.Count)
                        {
                            MessageBox.Show("Generating failed. Try again.");
                            return;
                        }
                    }

                    sudoku.Grid[row, column].Value = possibleValues[index];

                    SudokuCore.ReduceCellsValues(sudoku, row, column, possibleValues[index]);
                    possibleValues.Remove(possibleValues[index]);
                    column++;
                }
                column = startColumn;
                row++;
            }
        }

        /// <summary>
        /// Method to check if filling diagonal squares does not lead to reducing all possible values from some cells. 
        /// </summary>
        public static bool IsPossibleToChoose(Sudoku sudoku, int row, int column, int value)
        {
            // If setting a value leads to situation that any cell in the same row or column has no possible numbers left, return false and try different value.
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

            // If setting a value leads to situation that any cell in the same square has no possible numbers left, return false and try different value.
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
