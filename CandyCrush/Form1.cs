using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CandyCrush
{
    public partial class Form1 : Form
    {

        private MyButton[,] btnGrid;
        private Color currColor, originalColor;
        private int rows, cols;


        public Form1()
        {
            InitializeComponent();
            populateGrid();
        }

        public void populateGrid()
        {

            int x, y = 0;

            //calculate the number of rows and cols based on the panel and button size
            cols = panel1.Height / MyButton.Btn_size;
            rows = panel1.Width / MyButton.Btn_size;

            // new 2D array of buttons
            btnGrid = new MyButton[rows, cols];

            // create a new button at each row and col location
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    btnGrid[r, c] = new MyButton();
                    btnGrid[r, c].row = r;
                    btnGrid[r, c].col = c;

                    // assign the same event handler to every button in the panel
                    btnGrid[r, c].Click += gridbutton_Click;

                    panel1.Controls.Add(btnGrid[r, c]);

                    // change the location of the button to its proper x and y coordinates
                    btnGrid[r, c].Location = new Point(r * MyButton.Btn_size, c * MyButton.Btn_size);
                }
            }

        }

        private void colorbutton_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            currColor = btn.BackColor;
            pictureBox1.BackColor = btn.BackColor;

        }

        private void gridbutton_Click(object sender, EventArgs e)
        {


            MyButton btn = (MyButton)sender;
            originalColor = btn.BackColor;

            floodFill(btn.row, btn.col);


        }

        private void floodFill(int r, int c)
        {

            btnGrid[r, c].BackColor = currColor;
            

        }
    }
}
