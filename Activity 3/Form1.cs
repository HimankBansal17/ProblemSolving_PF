//Programmed by:- Himank Bansal
//u3183058
//Certain part of the code were referenced:
//Windows Forms:
//https://docs.microsoft.com/en-us/dotnet/framework/winforms/

//Alorithm logic Videos
//Class Lecutres
//A*:
//https://www.youtube.com/watch?v=-L-WgKMFuhE&t=3s

//BFS:
//https://www.youtube.com/watch?v=KiCBXu4P-2Y&t=2s

//DFS:
//https://www.youtube.com/watch?v=iaBEKo5sM7w

//MultiThreading
//https://www.tutorialspoint.com/csharp/csharp_multithreading.htm
//https://docs.microsoft.com/en-us/dotnet/standard/threading/using-threads-and-threading

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity_3
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
        }


        Board MatrixBoard = new Board();
        int col;
        int row;


        private void button2_Click(object sender, EventArgs e)
        {
            MatrixBoard.MFindPath();
        }

        private void Start_CheckedChanged(object sender, EventArgs e)
        {
            if (Start.Checked)
            {
                MatrixBoard.CurrentOption = Box.S;
                EndPoint.Checked = false;
                Obstacle.Checked = false;
            }
        }

        private void EndPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (EndPoint.Checked)
            {
                MatrixBoard.CurrentOption = Box.E;
                Start.Checked = false;
                Obstacle.Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }





        private void Obstacle_CheckedChanged(object sender, EventArgs e)
        {
            if (Obstacle.Checked)
            {
                MatrixBoard.CurrentOption = Box.O;
                EndPoint.Checked = false;
                Start.Checked = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Console_Load(object sender, EventArgs e)
        {
            MatrixBoard.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            MatrixBoard.StartPosition = FormStartPosition.Manual;
            MatrixBoard.row = 60;
            MatrixBoard.col = 60;
            MatrixBoard.CreateGrid(60, 60);
                MatrixBoard.Show();
                button2.Visible = true;
        }
    }
}
