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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment____Forms_
{
    public partial class Form1 : Form
    {

        public enum pathtype
        {
            easypath,
            shortestpath,
            hardestpath,
        }

        public bool pathFound = false;

        public pathtype PathType=new pathtype();
        //For activity 1 and 2 test code
         Activity_1.Activity_1 activity_1=new Activity_1.Activity_1();
        //For Final Code
        Activity_1.FindPath PathFinder = new Activity_1.FindPath();

        List<Activity_1.Cell> Path;

        float BoxSizOffset=10; //size of boxoffset
        public int row=60; //row size
        public int col=60; //column size

        Activity_1.Cell[,] Cells; //2D matrix for the board of location cells on the board
        
        Activity_1.Index StartPos=new Activity_1.Index();//start position for the robot
        
        Activity_1.Index EndPos=new Activity_1.Index();//end position or target position for the robot
        
        Activity_1.Cell CurrentCell;//Current Node Path Finding Agent is visiting

        List<Activity_1.Cell> OpenNodes;//List of Open Nodes (To be Removed in the final code) ******just for testing purposes***********
        HashSet<Activity_1.Cell> ClosedNodes;//List of Closed Nodes (May be Removeed in the final code )************just for testing purposes**************

        public Activity_1.Box CurrentOption=new Activity_1.Box();
        public Form1()
        {
            InitializeComponent();
        }
        int SqaureSize = 10;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            Board.Visible = true;//to remove the dataview grid from display to test people
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            CreateGrid(60,60);
        }


        //fucntion Responsible for Initialising a grid for the algorithm and greate a 2D matrix to store the value
        public void CreateGrid(int row,int col)
        {
            OpenNodes = new List<Activity_1.Cell>();
            ClosedNodes = new HashSet<Activity_1.Cell>();
            Path = new List<Activity_1.Cell>();
            activity_1.columnsize = col;
            activity_1.rowsize = row;
            Cells = new Activity_1.Cell[row, col];//initialise the the cell in 2D matrix with the input row and col size
            for(int x=0;x<row;x++)
            {
                for(int y=0;y<col;y++)
                {
                    Cells[x, y] = new Activity_1.Cell(x, y, Activity_1.Box.W0);//initiates a matrix to store the values with all blocks as walkable as default until user make changes with input
                }
            }
        }

        //function used to reset the board to empty again and 
        public void RefreshPanel()
        {
            Refresh();
        }
        
        
        #region A* Algorithm


        //https://www.youtube.com/watch?v=mZfyt03LDH4&t=810s
        //Find path Using Activity 1
        public void FindPath()
        {
            //Finds the path using A* Algorithm
            A_FindPath(Cells, CurrentCell,activity_1.columnsize,activity_1.rowsize);
        }
        #endregion

        #region Panel For Board draws lines and graphs according to the user input
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
            //panel1.BackColor = Color.Yellow;
            Graphics g = e.Graphics;
        
            //Panel1
            SolidBrush b = new SolidBrush(Color.Red);
            panel1.BackColor = Color.Black;
            BoxSizOffset = panel1.Width / row;
            //SqaureSize = (int)BoxSizOffset;
           /* 
            //draw the verticle lines for the board
            for(int i=0;i<row;i++)
            {
                g.DrawLine(Pens.DarkGray,i*BoxSizOffset,0,i*BoxSizOffset,panel1.Height);
            }

            float BoxhieghtOffset = panel1.Height / col;
            
            //draw the vertical 
            for(int i=0;i<col;i++)
            {
                g.DrawLine(Pens.DarkGray,0,i*BoxhieghtOffset,panel1.Width,i*BoxhieghtOffset);
            }*/
        }
        #endregion


        //function used to change the Color on the panel        
        public void ChangeColor(int x,int y,Color color)
        {
            Graphics g = panel1.CreateGraphics();
            SolidBrush OpenNodeColor = new SolidBrush(color);
            g.FillRectangle(OpenNodeColor,((x * SqaureSize)+1+((x*SqaureSize)*0.1f)),((y * SqaureSize)+1+((y*SqaureSize)*0.1f)), SqaureSize, SqaureSize);
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
       public void A_FindPath(Activity_1.Cell[,] Cells,Activity_1.Cell CurrentNode,int colsize,int rowsize)
        {
          
            //collect possible nodes
            List<Activity_1.Cell>  PossibleNodes =PathFinder.PossibleNodes(CurrentNode,colsize, rowsize,Cells);

            //assign cost to each possible node for current node
            PossibleNodes = PathFinder.CostOfOpenNodes(PossibleNodes, StartPos, EndPos,PathType);
            
            foreach (Activity_1.Cell Node in PossibleNodes)
            {
                bool existing = false;
                
                //loop to check if the new possible nodes already exists in the open node list or not
                if(Node.visited==false&&!ClosedNodes.Contains(Node)&&!OpenNodes.Contains(Node))
                {
                    OpenNodes.Add(Node);
                    ChangeColor(Node.CellPos.x, Node.CellPos.y, Color.Blue);
                }
            }

            //mark current node as visited
            //add the node to closed node list
            //remove the node from open node list
            //The least Expensive Node will be Selected
            CurrentNode.visited = true;
            ClosedNodes.Add(CurrentNode);
            OpenNodes.Remove(CurrentNode);
            ChangeColor(CurrentNode.CellPos.x, CurrentNode.CellPos.y, Color.Red);
            Cells[CurrentNode.CellPos.x, CurrentNode.CellPos.y] = CurrentNode;
            
            //Select next node wiht lowest cost from the open node list
            CurrentNode =PathFinder.SelectNextNode(OpenNodes);
            CurrentCell = CurrentNode;

            //show the path when end point is found
            if (CurrentCell.CellPos.x == EndPos.x && CurrentCell.CellPos.y == EndPos.y)
            {

                ShowPath(CurrentNode, ClosedNodes);
                pathFound = true;
                MessageBox.Show("Path Found");
                return;
            }

            else
            {
               FindPath();
            }


        }
        

        Activity_1.Cell PrevNode;
        /// <summary>
        /// 
        /// Function finds the shortest path that reaches the 
        /// 
        /// Function is responsible to Display the shortest path been created using the A* algorithm in the GUI Format using the Visited Node list
        ///
        /// </summary>
        /// <param name="CurrentNode"></param>
        /// <param name="ClosedNodes"></param>
        public void ShowPath(Activity_1.Cell CurrentNode,HashSet<Activity_1.Cell> ClosedNodes)
        {
                PrevNode = CurrentNode.Parent;
                Path.Add(PrevNode);
                foreach (Activity_1.Cell Node in ClosedNodes)
                {
                    if (PrevNode.CellPos.x==Node.CellPos.x&&PrevNode.CellPos.y==Node.CellPos.y)
                    {
                        CurrentNode = Node;
                        break;
                    }
                }
                if(PrevNode.CellPos.x==StartPos.x&&PrevNode.CellPos.y==StartPos.y)
                {
                    foreach(Activity_1.Cell Node in Path)
                    {
                        ChangeColor(Node.CellPos.x, Node.CellPos.y,Color.Green);
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
        //The Code beneatehe is for user input with mouse
        bool MouseIsDown = false;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            
            MouseIsDown = true;
            Graphics g = panel1.CreateGraphics();
            SolidBrush B = new SolidBrush(Color.AliceBlue);
            
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            int mousePoseX= (int)((e.X/10) - ((e.X/10) * 0.1f));
            int mousePoseY = (int)((e.Y/10) - ((e.Y/10) * 0.1f));
            if (MouseIsDown)
            {
                if(mousePoseX>=0&&mousePoseX<row&&mousePoseY>=0&&mousePoseY<col)
                {
                        if (CurrentOption == Activity_1.Box.S)
                        {
                            ChangeColor(mousePoseX, mousePoseY, Color.AntiqueWhite);
                            Cells[mousePoseX, mousePoseY].box= new Activity_1.Box();
                            Cells[mousePoseX, mousePoseY].box = Activity_1.Box.S;
                            CurrentCell =new Activity_1.Cell(mousePoseX, mousePoseY,Activity_1.Box.S);
                            StartPos.x = mousePoseX;
                            StartPos.y = mousePoseY;
                        }

                        else if (CurrentOption == Activity_1.Box.E)
                        {
                            ChangeColor(mousePoseX, mousePoseY, Color.Red);
                            Cells[mousePoseX, mousePoseY].box = Activity_1.Box.E;
                            EndPos.x = mousePoseX;
                            EndPos.y = mousePoseY;
                        }

                        else if (CurrentOption == Activity_1.Box.O)
                        {
                            ChangeColor(mousePoseX, mousePoseY, Color.SandyBrown);
                            Cells[mousePoseX, mousePoseY].box = Activity_1.Box.O;
                        }
                    else if (CurrentOption == Activity_1.Box.W)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.Salmon);
                        Cells[mousePoseX, mousePoseY].box = Activity_1.Box.W;
                    }
                    else if (CurrentOption == Activity_1.Box.W0)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.BurlyWood);
                        Cells[mousePoseX, mousePoseY].box = Activity_1.Box.W0;
                    }
                    else if (CurrentOption == Activity_1.Box.W90)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.DarkSlateGray);
                        Cells[mousePoseX, mousePoseY].box = Activity_1.Box.W90;
                    }
                    else if (CurrentOption == Activity_1.Box.W120)
                    {
                        ChangeColor(mousePoseX, mousePoseY, Color.GreenYellow);
                        Cells[mousePoseX, mousePoseY].box = Activity_1.Box.W120;
                    }

                    else if (CurrentOption == Activity_1.Box.Wg)
                        {
                            ChangeColor(mousePoseX, mousePoseY, Color.LawnGreen);
                            Cells[mousePoseX, mousePoseY].box = Activity_1.Box.Wg;
                        }
                        else if (CurrentOption == Activity_1.Box.Wr)
                        {
                            ChangeColor(mousePoseX, mousePoseY, Color.Gray);
                            Cells[mousePoseX, mousePoseY].box = Activity_1.Box.Wr;
                        }
                        else if (CurrentOption == Activity_1.Box.Ww)
                        {
                            ChangeColor(mousePoseX, mousePoseY, Color.Azure);
                            Cells[mousePoseX, mousePoseY].box = Activity_1.Box.Ww;
                        }
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }
    }
    #endregion
}
