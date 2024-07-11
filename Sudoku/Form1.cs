using System.Drawing;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateGrid();  
        }

        Cell[,] grid = new Cell[9, 9];
        private void CreateGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = new Cell(i, j);

                    //Make the squares 3x3 different colours
                    grid[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? Color.LightSkyBlue : Color.CornflowerBlue;

                    grid[i, j].KeyPress += ChangeValue;
                    Grid.Controls.Add(grid[i, j]);
                }
            }
        }

        private void ChangeValue(object sender, KeyPressEventArgs e)
        {
            var cell = sender as Cell;
            int value;

            //Insert to cell only if pressed key is number
            if (int.TryParse(e.KeyChar.ToString(), out value))
            { 
                cell.Text = value.ToString();
                cell.ForeColor = Color.Black;
            }   

            //If pressed key is backspace, clear this cell
            else if ((int)e.KeyChar == 8)
            {
                cell.ClearCell();
            }
        }
    }
}
