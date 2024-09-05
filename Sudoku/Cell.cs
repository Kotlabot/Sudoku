using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;

namespace Sudoku
{
    public class Cell : Button
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public List<int> PossibleValues { get; set; }

        private List<int> InitialValues { get; set; }
        
        /// <summary>
        /// Every cell is declared with Size, Location, Font and its style and size, FlatStyle and Flatappearance.
        /// </summary>
        /// <param name="x"></ first cells coordinate>
        /// <param name="y"></ second cells coordinate>
        public Cell(List<int> possibleValues, int row, int column, int size, int font, int value = 0)
        {
            X = column;
            Y = row;
            Value = value;
            
            Size = new Size(size, size);
            Location = new Point(X * size, Y * size);
            Font = new Font("Arial", font, FontStyle.Bold);
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;

            PossibleValues = new List<int>(possibleValues);
            InitialValues = new List<int>(PossibleValues);
        }

        //Copy constructor for solver 
        public Cell(Cell cell) 
        {
            X = cell.X;
            Y = cell.Y;
            Value = cell.Value;

            Size = cell.Size;
            Location = cell.Location;

            Font = cell.Font;
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;

            BackColor = cell.BackColor;
            KeyPress += GridGenerator.ChangeValue;
            Text = cell.Text;

            PossibleValues = new List<int>(cell.PossibleValues);
            InitialValues = new List<int>(cell.InitialValues);
        }

        /// <summary>
        /// Method to display values assigned to the cells as text that user can see.
        /// </summary>
        public void SetText(int value)
        {
            this.Text = GetText(value);
        }

        /// <summary>
        /// In Sudoku grids 10x10 or larger we need to display numbers as letters of alphabet, but its plainer to work with numbers in the backtracking method.
        /// Method converts numbers to letters.
        /// </summary>

        public string GetText(int value)
        {
            if (value > 9)
            {
                switch (value)
                {
                    case 10:
                        return "A";
                    case 11:
                        return "B";
                    case 12:
                        return "C";
                    case 13:
                        return "D";
                    case 14:
                        return "E";
                    case 15:
                        return "F";
                    case 16:
                        return "G";
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// Method does the opposite of GetText(), it converts letters to numbers.
        /// </summary>
        public int GetFromText()
        {
            switch (this.Text)
            {
                case "A":
                    return 10;
                case "B":
                    return 11;
                case "C":
                    return 12;
                case "D":
                    return 13;
                case "E":
                    return 14;
                case "F":
                    return 15;
                case "G":
                    return 16;
                default:
                    return Convert.ToInt32(this.Text);
            }
        }

        public void ClearText()
        {
            this.Text = string.Empty;
        }

        public void Reset()
        {
            Value = 0;
            PossibleValues = new List<int>(InitialValues);
        }
    }
}
