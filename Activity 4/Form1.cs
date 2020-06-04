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

namespace Assignment_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Board board = new Board();
        bool boardBuild = false;
        
        private void Start_CheckedChanged(object sender, EventArgs e)
        {
            if (Start.Checked == true)
            {
                board.CurrentOption = Box.S;
                Sqaure.Checked = false;
                Circle.Checked = false;
                EndPoint.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(boardBuild==false)
            {
                board.Show();
                boardBuild = true;
                board.CreateGrid(5, 5);
            }
            
        }

        private void Circle_CheckedChanged(object sender, EventArgs e)
        {
            if (Circle.Checked == true)
            {
                board.CurrentOption = Box.circle;
                Sqaure.Checked = false;
                Start.Checked = false;
                EndPoint.Checked = false;
            }
        }

        private void Sqaure_CheckedChanged(object sender, EventArgs e)
        {
            if (Sqaure.Checked == true)
            {
                board.CurrentOption = Box.square;
                Start.Checked = false;
                Circle.Checked = false;
                EndPoint.Checked = false;
            }
        }

        //Reset Button
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //Start path Finding Button
        private void button2_Click(object sender, EventArgs e)
        {
            board.FindPath();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void EndPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (EndPoint.Checked == true)
            {
                board.CurrentOption = Box.E;
                Sqaure.Checked = false;
                Circle.Checked = false;
                Start.Checked = false;
            }
        }
    }
}
