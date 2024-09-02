using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Sudoku
{
    public class Cell : Button
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public List<int> PossibleValues { get; set; }

        
        /// <summary>
        /// Every cell is declared with Size, Location, Font and its style and size, FlatStyle and Flatappearance
        /// </summary>
        /// <param name="x"></ first cells coordinate>
        /// <param name="y"></ second cells coordinate>
        public Cell(int row, int column, int value = 0, int count = 9)
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

            PossibleValues = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
        }

        public void ClearText()
        {
            this.Text = string.Empty;
        }

        public void Reset()
        {
            Value = 0;
            PossibleValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
