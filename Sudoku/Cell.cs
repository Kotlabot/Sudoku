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
        /// Every cell is declared with Size, Location, Font and its style and size, FlatStyle and Flatappearance
        /// </summary>
        /// <param name="x"></ first cells coordinate>
        /// <param name="y"></ second cells coordinate>
        public Cell(List<int> possibleValues, int row, int column, int value = 0)
        {
            X = column;
            Y = row;
            Value = value;
            
            Size = new Size(45, 45);
            Location = new Point(X * 45, Y * 45);
            Font = new Font("Arial", 23, FontStyle.Bold);
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;

            PossibleValues = new List<int>(possibleValues);
            InitialValues = new List<int>(PossibleValues);
        }

        public void SetText(int value)
        {
            this.Text = GetText(value);
        }

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
