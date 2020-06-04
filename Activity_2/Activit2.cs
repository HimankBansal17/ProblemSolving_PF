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

namespace Activity_2
{
    public partial class Activity2 : Form
    {
        int row=0;
        int col = 0;
        Board board = new Board();
        public Activity2()
        {
            InitializeComponent();
        }




        private void button2_Click(object sender, EventArgs e)
        {
            if (board.pathfound == false)
            {
                board.PathType = Board.pathtype.shortestpath;
                board.pathfound = true;
                board.FindPath();

            }
            else
                MessageBox.Show("Reset The App");
        }

        private void Start_CheckedChanged(object sender, EventArgs e)
        {
            if (Start.Checked)
            {
                board.CurrentOption = Box.S;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }



        private void button3_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void EndPoint_CheckedChanged(object sender, EventArgs e)
        {
            if (EndPoint.Checked)
            {
                board.CurrentOption = Box.E;
                Start.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void WaterSquare_CheckedChanged(object sender, EventArgs e)
        {
            if (WaterSquare.Checked)
            {
                board.CurrentOption = Box.Ww;
                Start.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                EndPoint.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void GrassSquare_CheckedChanged(object sender, EventArgs e)
        {
            if (GrassSquare.Checked)
            {
                board.CurrentOption = Box.Wg;
                EndPoint.Checked = false;
                Start.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void RockySqaure_CheckedChanged(object sender, EventArgs e)
        {
            if (RockySqaure.Checked)
            {
                board.CurrentOption = Box.Wr;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                Walkable.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void Obstacle_CheckedChanged(object sender, EventArgs e)
        {
            if (Obstacle.Checked)
            {
                board.CurrentOption = Box.O;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                RockySqaure.Checked = false;
                WaterSquare.Checked = false;
                Start.Checked = false;
                Walkable.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void Walkable_CheckedChanged(object sender, EventArgs e)
        {
            if (Walkable.Checked)
            {
                board.CurrentOption = Box.W;
                EndPoint.Checked = false;
                GrassSquare.Checked = false;
                Start.Checked = false;
                WaterSquare.Checked = false;
                Obstacle.Checked = false;
                checkBox3.Checked = false;
                checkBox90.Checked = false;
                checkBox2.Checked = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Activity2_Load(object sender, EventArgs e)
        {
            board.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            board.StartPosition = FormStartPosition.Manual;
            board.Show();
            button2.Visible = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                board.CurrentOption =Box.W120;
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
                board.CurrentOption = Box.W0;
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
                board.CurrentOption = Box.W90;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (board.pathfound == false)
            {
                board.PathType = Board.pathtype.easypath;
                board.pathfound = true;
                board.FindPath();

            }
            else
                MessageBox.Show("Reset The App");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (board.pathfound == false)
            {
                board.PathType = Board.pathtype.hardestpath;
                board.pathfound = true;
                board.FindPath();

            }
            else
                MessageBox.Show("Reset The App");
        }
    }
}
