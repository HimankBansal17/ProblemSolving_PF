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

namespace Assignment____Forms_
{
    public partial class Activity1 : Form
    {
        Form1 MatrixBoard = new Form1();
        int col;
        int row;
        public Activity1()
        {
            InitializeComponent();
            button3.Text = "Reset";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            MatrixBoard.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            MatrixBoard.StartPosition = FormStartPosition.Manual;
            if(row!=0 && col!=0)
            {
                MatrixBoard.row = row;
                MatrixBoard.col = col;
                MatrixBoard.CreateGrid(row,col);
                MatrixBoard.Show();
                button2.Visible = true;
            }
            //MatrixBoard.ChangeBGColor();

        }
        private void button2_Click(object sender,EventArgs e)
        {

            //MatrixBoard.MFindPath();
            MatrixBoard.FindPath();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
            button2.Click += new EventHandler(button2_Click);
            MatrixBoard.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {   
            if(Start.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.S;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }


        private void EndPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (EndPoint.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.E;
                Start.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void WaterSquare_CheckedChanged(object sender, EventArgs e)
        {
            if(WaterSquare.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.Ww;
                Start.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                EndPoint.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;
                checkBox3.Checked = false;
            }

        }

        private void GrassSquare_CheckedChanged(object sender, EventArgs e)
        {

            if (GrassSquare.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.Wg;
                EndPoint.Checked = false;
                Start.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;
                checkBox3.Checked = false;
            }
        }
                                                     
        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void Obstacle_CheckedChanged(object sender, EventArgs e)
        {
            if (Obstacle.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.O;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Start.Checked = false;
                Walkable.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void RockySquare_CheckedChanged_1(object sender, EventArgs e)
        {
            if (RockySqaure.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.Wr;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;
                checkBox3.Checked = false;
            }
        }


        private void WalkableSquare(object sender, EventArgs e)
        {
            if (Walkable.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.W;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;
                checkBox3.Checked = false;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MatrixBoard.pathFound == false)
            {
                MatrixBoard.PathType = Form1.pathtype.easypath;
                MatrixBoard.pathFound = true;
                MatrixBoard.FindPath();
                
            }
            else
                MessageBox.Show("Reset The App");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(MatrixBoard.pathFound==false)
            {
                MatrixBoard.PathType = Form1.pathtype.hardestpath;
                MatrixBoard.pathFound = true;
                MatrixBoard.FindPath();
                
            }
            else
                MessageBox.Show("Reset The App");

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(MatrixBoard.pathFound==false)
            {
                MatrixBoard.PathType = Form1.pathtype.shortestpath;
                MatrixBoard.pathFound = true;
                MatrixBoard.FindPath();
                
            }
            else
                MessageBox.Show("Reset The App");

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.W120;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                checkBox90.Checked = false;
                Walkable.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.W0;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                checkBox90.Checked = false;
                Walkable.Checked = false;
                checkBox2.Checked = false;
                checkBox90.Checked = false;

            }
        }

        private void checkBox90_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox90.Checked)
            {
                MatrixBoard.CurrentOption = Activity_1.Box.W90;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                checkBox90.Checked = false;
                Walkable.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }
    }
}
