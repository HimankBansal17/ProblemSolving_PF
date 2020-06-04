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

namespace Assignment_5
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
        }

        Board board = new Board();


        private void Sqaure_CheckedChanged(object sender, EventArgs e)
        {
            if (Sqaure.Checked == true)
            {
                board.CurrentOption = Box.square;
                Start.Checked = false;
                Circle.Checked = false;
            }
        }

        private void Circle_CheckedChanged(object sender, EventArgs e)
        {
            if (Circle.Checked == true)
            {
                board.CurrentOption = Box.circle;
                Sqaure.Checked = false;
                Start.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void Console_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            board.Show();
            board.CreateGrid(5, 5);
        }

        private void Start_CheckedChanged(object sender, EventArgs e)
        {
            if(Start.Checked==true)
            {
                board.CurrentOption = Box.S;
                Sqaure.Checked = false;
                Circle.Checked = false;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            board.FindPath();
        }
    }
}
