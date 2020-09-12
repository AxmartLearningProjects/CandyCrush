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
        private Color currColor = Color.Red;
        private Color originalColor;
        private int rows, cols;
        private int clickCounter = 0;


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
            if (gameWonCheck())
            {
                return;
            }
            

            MyButton btn = (MyButton)sender;
            originalColor = btn.BackColor;

            if (!originalColor.Equals(currColor))
            {
                floodFill(btn.row, btn.col);
            }

            lbl_clickCounter.Text = (++clickCounter).ToString();
            if (clickCounter % 5 == 0)
            {
                chaneRandomButton();
            }


        }

        private void chaneRandomButton()
        {
            btnGrid[MyButton.rnd.Next(rows), MyButton.rnd.Next(cols)].BackColor = MyButton.myColors[MyButton.rnd.Next(MyButton.myColors.Length)];
        }

        private bool gameWonCheck()
        {

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (!btnGrid[r,c].BackColor.Equals(btnGrid[0,0].BackColor))
                    {
                        return false;
                    }
                }
            }

            MessageBox.Show($"Congratulation you won with {clickCounter} clicks! If you can do better press the Reset button.");

            return true;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {

            panel1.Controls.Clear();
            populateGrid();
            lbl_clickCounter.Text = "0";
            clickCounter = 0;

        }

        private void floodFill(int r, int c)
        {
            if (isValid(r , c) && btnGrid[r,c].BackColor.Equals(originalColor))
            {
                // change the current cell clicked
                btnGrid[r, c].BackColor = currColor;

                // apply to the cell to the east (r +1)

                floodFill(r +1 , c);

                // apply to the cell to the west (r -1)
                floodFill(r - 1, c);

                // apply to the cell to the south (c +1)
                floodFill(r , c+1);

                // apply to the cell to the west (c -1)
                floodFill(r, c - 1);
            }



        }


        private bool isValid(int r, int c)
        {
            return r < rows && r >= 0 && c < cols && c >= 0;

        }

    }
}
