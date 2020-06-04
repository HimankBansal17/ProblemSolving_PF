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
    public partial class Board : Form
    {
        public int row;
        public int col;
        public int BoxSizOffset=10;

        public enum pathtype
        {
            easypath,
            shortestpath,
            hardestpath,
        }

        public bool pathfound = false;

        public pathtype PathType;
        PathFinder pathFinder = new PathFinder();
        Cell[,] Cells; //2D matrix for the board of location cells on the board

        Index StartPos = new Index();//start position for the robot

       Index EndPos = new Index();//end position or target position for the robot

        Cell CurrentCell;//Current Node Path Finding Agent is visiting

        List<Cell> Path;
        List<Cell> OpenNodes;//List of Open Nodes (To be Removed in the final code) ******just for testing purposes***********
        HashSet<Cell> ClosedNodes;
        public Box CurrentOption = new Box();
        public Board()
        {
            InitializeComponent();
        }
        private void Board_Load(object sender, EventArgs e)
        {
            CreateGrid(60, 60);
            row = 60;
            col = 60;
        }
        public void CreateGrid(int row, int col)
        {
            this.col = col;
            this.row = row;
            OpenNodes = new List<Cell>();
            ClosedNodes = new HashSet<Cell>();
            Path = new List<Cell>();
            Cells = new Cell[row, col];//initialise the the cell in 2D matrix with the input row and col size
            for (int x = 0; x < row; x++)
            {
                for (int y = 0; y < col; y++)
                {
                    Cells[x, y] = new Cell(x, y, Box.W0);//initiates a matrix to store the values with all blocks as walkable as default until user make changes with input
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel1.BackColor = Color.Yellow;
            Graphics g = e.Graphics;

            //Panel1
            SolidBrush b = new SolidBrush(Color.Red);
            panel1.BackColor = Color.Black;
            BoxSizOffset = panel1.Width / row;

            ////draw the verticle lines for the board
            //for (int i = 0; i < row; i++)
            //{
            //    g.DrawLine(Pens.DarkGray, i * BoxSizOffset, 0, i * BoxSizOffset, col * row);
            //}

            //float BoxhieghtOffset = panel1.Height / col;

            ////draw the vertical 
            //for (int i = 0; i < col; i++)
            //{
            //    g.DrawLine(Pens.DarkGray, 0, i * BoxhieghtOffset, col * row, i * BoxhieghtOffset);
            //}
        }


        //https://www.youtube.com/watch?v=mZfyt03LDH4&t=810s
        //Find path Using Activity 1
        public void FindPath()
        {
            //this does a single cycle and visites the 
            FindPossibleNodes(Cells, CurrentCell,col, row);
        }

        /// <summary>
        /// 
        /// 
        /// Function for Getting Possible nodes and visiting the next node once every time it is called 
        /// this functions also updates the values for the GUI panel 
        /// function is recursive till the path to end point is not found
        /// 
        /// </summary>
        /// <param name="Cells"></param>
        /// <param name="CurrentNode"></param>
        /// <param name="colsize"></param>
        /// <param name="rowsize"></param>
        public void FindPossibleNodes(Cell[,] Cells,Cell CurrentNode, int colsize, int rowsize)
        {
            List<Cell> PossibleNodes = pathFinder.PossibleNodes(CurrentNode, colsize, rowsize, Cells);

            PossibleNodes = pathFinder.CostOfOpenNodes(PossibleNodes, StartPos, EndPos,CurrentNode,OpenNodes,PathType);
            foreach (Cell Node in PossibleNodes)
            {
                //loop to check if the new possible nodes already exists in the

                if (Node.visited == false && !ClosedNodes.Contains(Node) && !OpenNodes.Contains(Node))
                {
                    OpenNodes.Add(Node);
                    ChangeColor(Node.CellPos.x, Node.CellPos.y, Color.Blue);
                }
            }

            //The least Expensive Node will be Selected
            CurrentNode.visited = true;
            ClosedNodes.Add(CurrentNode);
            OpenNodes.Remove(CurrentNode);
            ChangeColor(CurrentNode.CellPos.x, CurrentNode.CellPos.y, Color.Red);
            Cells[CurrentNode.CellPos.x, CurrentNode.CellPos.y] = CurrentNode;
            CurrentNode = pathFinder.SelectNextNode(OpenNodes);
            CurrentCell = CurrentNode;


            if (CurrentCell.CellPos.x == EndPos.x && CurrentCell.CellPos.y == EndPos.y)
            {
                pathfound = true;
                ShowPath(CurrentNode, ClosedNodes);
                MessageBox.Show("Path Found");
                return;
            }

            else
            {
               FindPath();
            }


        }

        
        private const int SqaureSize = 10;
        public void ChangeColor(float x, float y, Color color)
        {
            Graphics g = panel1.CreateGraphics();
            SolidBrush OpenNodeColor = new SolidBrush(color);
            g.FillRectangle(OpenNodeColor, ((x * SqaureSize) + 1 + ((x *SqaureSize) * 0.1f)), ((y *SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)),SqaureSize, SqaureSize);
        }

        Cell PrevNode;
        /// <summary>
        /// 
        /// Function finds the shortest path that reaches the 
        /// 
        /// Function is responsible to Display the shortest path been created using the A* algorithm in the GUI Format using the Visited Node list
        ///
        /// </summary>
        /// <param name="CurrentNode"></param>
        /// <param name="ClosedNodes"></param>
        public void ShowPath(Cell CurrentNode, HashSet<Cell> ClosedNodes)
        {
            PrevNode = CurrentNode.Parent;
            Path.Add(PrevNode);
            foreach (Cell Node in ClosedNodes)
            {
                if (PrevNode==Node)
                {
                    CurrentNode = Node;
                    break;
                }
            }
            if (PrevNode.CellPos.x == StartPos.x && PrevNode.CellPos.y == StartPos.y)
            {
                foreach (Cell Node in Path)
                {
                    ChangeColor(Node.CellPos.x, Node.CellPos.y, Color.Green);
                }
            }
            else
            {
                ShowPath(CurrentNode, ClosedNodes);
            }
        
        }

        /// <summary>
        /// The below region is for mouse interface or mouse input response for the panel

        /// if the mouse is clicked and dragged 
        ///         Assign the cell value and color according to the selected current box value type
        /// if the mouse button is not pressed then 
        ///         do nothing 
        /// if the mouse button is clicked but mouse is not moved or dragged 
        ///         do Nothing
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        #region Mouse Interface for GUI 

        bool MouseIsDown = false;
        public void RefreshPanel()
        {
            Refresh();
        }
        #endregion

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
            Graphics g = panel1.CreateGraphics();
            SolidBrush B = new SolidBrush(Color.AliceBlue);
        }

        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
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
                     else if (CurrentOption == Box.W)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Salmon);
                        Cells[mousePoseX, mousePoseY].box = Box.W;
                    }
                    else if (CurrentOption == Box.W0)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.BurlyWood);
                        Cells[mousePoseX, mousePoseY].box = Box.W0;
                    }
                    else if (CurrentOption == Box.W90)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.DarkSlateGray);
                        Cells[mousePoseX, mousePoseY].box = Box.W90;
                    }
                    else if (CurrentOption == Box.W120)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.GreenYellow);
                        Cells[mousePoseX, mousePoseY].box =Box.W120;
                    }

                    else if (CurrentOption == Box.Wg)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.LawnGreen);
                        Cells[mousePoseX, mousePoseY].box = Box.Wg;
                    }
                    else if (CurrentOption == Box.Wr)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Gray);
                        Cells[mousePoseX, mousePoseY].box = Box.Wr;
                    }
                    else if (CurrentOption == Box.Ww)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Aquamarine);
                        Cells[mousePoseX, mousePoseY].box = Box.Ww;
                    }
                }
            }
        }

        private void panel1_MouseUp_1(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }

        
    }



}
