using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    class Cell : Button
    {
        public int X { get; set; }
        public int Y { get; set; }
        /// <summary>
        /// Every cell is declared with Size, Location, Font and its style and size, FlatStyle and Flatappearance
        /// </summary>
        /// <param name="x"></ first cells coordinate>
        /// <param name="y"></ second cells coordinate>
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
    
            Size = new Size(45, 45);
            Location = new Point(x * 45, y * 45);
            Font = new Font("Arial", 23, FontStyle.Bold);
            ForeColor = Color.Black;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = Color.Black;
        }

        public void ClearCell()
        {
            this.Text = string.Empty;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
