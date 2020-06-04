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
using System.Drawing;
using System.Windows.Forms;

namespace Activity_3
{
    public partial class Board : Form
    {
        public int row;
        public int col;
        public int BoxSizOffset=10;
        PathFinder pathFinder = new PathFinder();
        Cell[,] Cells; //2D matrix for the board of location cells on the board
        Index StartPos = new Index();//start position for the robot
        Index EndPos = new Index();//end position or target position for the robot
        Cell CurrentCell;//Current Node Path Finding Agent is visiting
        public Box CurrentOption = new Box();

        public Board()
        {
            InitializeComponent();
        }

        private void Board_Load(object sender, EventArgs e)
        {
            //CreateGrid(60, 60);
            row = 60;
            col = 60;
        }

        
        public void CreateGrid(int row, int col)
        {
            this.col = col;
            this.row = row;
            Cells = new Cell[row, col];//initialise the the cell in 2D matrix with the input row and col size
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    Cells[x, y] = new Cell(x, y, Box.W0);//initiates a matrix to store the values with all blocks as walkable as default until user make changes with input
                }
            }
        }
      
        //Find path for Activity 3
        public void MFindPath()
        {
            Board panel = this;
            pathFinder.SetAgents(Cells,pathFinder, StartPos, EndPos, row, col, panel);
        }
      
        private const int SqaureSize = 10;
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //panel2.BackColor = Color.Yellow;
            Graphics g = e.Graphics;

            //panel2
            SolidBrush b = new SolidBrush(Color.Red);
            panel2.BackColor = Color.Black;
            //BoxSizOffset = panel2.Width / row;
            /*
            //draw the verticle lines for the board
            for (int i = 0; i < row; i++)
            {
                g.DrawLine(Pens.DarkGray, i * BoxSizOffset, 0, i * BoxSizOffset, col * row);
            }

            float BoxhieghtOffset = panel2.Height / col;

            //draw the vertical 
            for (int i = 0; i < col; i++)
            {
                g.DrawLine(Pens.DarkGray, 0, i * BoxhieghtOffset, col * row, i * BoxhieghtOffset);
            }*/
        }

        public void ChangeColor(float x, float y, Color color)
        {
            Graphics g = panel2.CreateGraphics();
            SolidBrush OpenNodeColor = new SolidBrush(color);
            g.FillRectangle(OpenNodeColor, ((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), ((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), SqaureSize, SqaureSize);
        }


        #region Mouse Input Functions
        bool MouseIsDown =false;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
            Graphics g = panel2.CreateGraphics();
            SolidBrush B = new SolidBrush(Color.AliceBlue);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            int mousePoseX = (int)((e.X / 10) - ((e.X / 10) * 0.1f));
            int mousePoseY = (int)((e.Y / 10) - ((e.Y / 10) * 0.1f));
            if (MouseIsDown)
            {
                if (mousePoseX >= 0 && mousePoseX < row && mousePoseY >= 0 && mousePoseY < col)
                {
                    if (CurrentOption == Box.S)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.AntiqueWhite);
                        Cells[mousePoseX, mousePoseY].box = new Box();
                        Cells[mousePoseX, mousePoseY].box = Box.S;
                        CurrentCell = new Cell(mousePoseX, mousePoseY, Box.S);
                        StartPos.x = mousePoseX;
                        StartPos.y = mousePoseY;
                    }

                    else if (CurrentOption == Box.E)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Red);
                        Cells[mousePoseX, mousePoseY].box = Box.E;
                        EndPos.x = mousePoseX;
                        EndPos.y = mousePoseY;
                    }

                    else if (CurrentOption == Box.O)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.SandyBrown);
                        Cells[mousePoseX, mousePoseY].box = Box.O;
                    }
                    else if (CurrentOption == Box.Wg)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.LawnGreen);
                        Cells[mousePoseX, mousePoseY].box = Box.Wg;
                    }
                    else if (CurrentOption == Box.Wr)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Aquamarine);
                        Cells[mousePoseX, mousePoseY].box = Box.Wg;
                    }
                    else if (CurrentOption == Box.Ww)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Azure);
                        Cells[mousePoseX, mousePoseY].box = Box.Ww;
                    }
                }
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }
        #endregion
    }
}
