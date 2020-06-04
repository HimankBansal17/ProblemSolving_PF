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
    public partial class Board : Form
    {
        Cell[,] Cells;
        Cell[,] Collider;
        public Box CurrentOption;
        int boxoffset;
        Cell CurrentNode;
        Cell StartNode;
        
        List<Cell> PossibleNode = new List<Cell>();
        Queue<Cell> OpenNodes = new Queue<Cell>();
        Queue<Cell> CloseNodes = new Queue<Cell>();
        Queue<Cell> Path = new Queue<Cell>();

        Activity_5 pathFinder = new Activity_5();
        public Board()
        {
            InitializeComponent();
        }

        public int row;
        public int col;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public void BorderColor(int x, int y, Color color,int r)
        {
            Graphics g = panel1.CreateGraphics();
            SolidBrush OpenNodeColor = new SolidBrush(color);
            g.FillRectangle(OpenNodeColor, x, y, r, r);
        }


        public void ChangeColor(int x, int y, Color color,int SqaureSize,Box shapetype)
        {
            Graphics g = panel1.CreateGraphics();
            SolidBrush OpenNodeColor = new SolidBrush(color);
            if(shapetype==Box.square)
            {
                Rectangle rect=new Rectangle(
                   (int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)) ,
                   (int)(((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f))) ,
                   (int)((3 * SqaureSize) + 1 + ((3 * SqaureSize) * 0.1f)),
                    (int)(((3 * SqaureSize) + 1 + ((3 * SqaureSize) * 0.1f))));
                g.FillRectangle(OpenNodeColor, rect);
                int h = (int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)+((3 * SqaureSize) + 1 + ((3 * SqaureSize) * 0.1f) / 2));
                int k =(int) (((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)) + ((3 * SqaureSize) + 1 + ((3 * SqaureSize) * 0.1f) / 2));
                
                
                BorderColor(h, k, Color.Red, 3);
                BorderColor(h, (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), Color.Red, 3);
                BorderColor((int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), k, Color.Red, 3);
                BorderColor((int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), Color.Red, 3);
                
                for(int i= (int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)); i<=h;i++)
                {
                    bool contains = rect.Contains(i, k);
                    if (contains == true)
                    {
                        BorderColor(i, k, Color.Red, 3);
                        int X = (int)Math.Ceiling(((i / SqaureSize) - ((i / SqaureSize) * 0.1f)));
                        int Y = (int)Math.Ceiling(((k / SqaureSize) - ((k / SqaureSize) * 0.1f)));
                        Collider[X, Y].box = Box.O;

                    }

                }
                for (int j = (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)); j <= k; j++)
                {
                    bool contains = rect.Contains(h, j);
                    if (contains == true)
                    {
                        BorderColor(h, j, Color.Red, 3);
                        int X= (int)Math.Ceiling(((h / SqaureSize) - ((h / SqaureSize) * 0.1f)));
                        int Y= (int)Math.Ceiling(((j / SqaureSize) - ((j / SqaureSize) * 0.1f)));
                        Collider[X, Y].box = Box.O;
                    }
                }

                for(int p = (int)((x * SqaureSize) + 1 + ((x * SqaureSize)*0.1f)); p<=h; p++)
                {
                    bool contains = rect.Contains(p, (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)));
                    if (contains == true)
                    {
                        BorderColor(p, (int)(((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f))), Color.Red, 3);
                        int X = (int)Math.Ceiling(((p / SqaureSize) - ((p / SqaureSize) * 0.1f)));
                        int Y = (int)Math.Floor((((int)(((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f))) / SqaureSize) - (((int)(((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f))) / SqaureSize) * 0.1f)));
                        Collider[X, Y].box = Box.O;
                    }


                }
                for (int j = (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)); j<=k; j++)
                {
                    bool contains = rect.Contains((int)((x * SqaureSize) + 1 + ((x * SqaureSize)*0.1f)), j);
                    if (contains == true)
                    {
                        BorderColor((int)(((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f))), j, Color.Red, 3);
                        int X = (int)Math.Ceiling(((int)((x * SqaureSize) + 1 + ((x * SqaureSize)*0.1f)) / SqaureSize) - (((int)((x * SqaureSize) + 1 + ((x * SqaureSize)*0.1f)) / SqaureSize) * 0.1f));
                        int Y = (int)Math.Floor(((j / SqaureSize) - ((j / SqaureSize) * 0.1f)));
                        Collider[X, Y].box = Box.O;
                    }
                }
                int H= (int)Math.Ceiling(((h / 13) - ((h / 13) * 0.1f)));
                int K = (int)Math.Floor(((k / 13) - ((k / 13) * 0.1f)));
                int Ah = Math.Abs(H - x / 2);
                int Ak = Math.Abs(K - y / 2);
                int Ar = (H-x);
            }
            else if(shapetype==Box.O)
            {
                g.FillRectangle(OpenNodeColor, ((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), ((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)),13, 13);
            }
            else if(shapetype==Box.rectangle)
            {
                g.FillRectangle(OpenNodeColor, ((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), ((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), 30, 50);
            }

            else if(shapetype==Box.circle)
            {
                Rectangle react=new Rectangle((int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)),
                    (int)((y * SqaureSize) + 1 + ((y * SqaureSize)) * 0.1f), 50, 50);
                int h = (int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f) + (50/2));
                int k = (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f) + (50/2));

                g.FillEllipse(OpenNodeColor,
                    react);
               // BorderColor(h, k, Color.Red,3);
                //BorderColor(h, (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), Color.Red, 3);
               // BorderColor((int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)),k, Color.Red, 3);
               // BorderColor((int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), Color.Red, 3);
                /*
                for(int i= (int)((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f));i<=h;i++)
                {
                    for (int j = (int)((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)); j <= k; j++)
                    {
                        bool contains = react.Contains(i, k);
                        if (contains == true)
                        {
                            
                        }
                    }

                }*/
                for(int i=0;i<360;i++)
                {
                    int X = (int)((50 / 2) * Math.Cos(i) + h);
                    int Y = (int)((50 / 2) * Math.Sin(i) + k);
                    BorderColor(X, Y, Color.Red, 3);
                    int MatrixX = (int)Math.Ceiling(((X / SqaureSize) - ((X / SqaureSize) * 0.1f)));
                    int MatrixY = (int)Math.Ceiling(((Y / SqaureSize) - ((Y / SqaureSize) * 0.1f)));
                    Collider[MatrixX, MatrixY].box = Box.O;
                }
            }

            else if(shapetype==Box.W0)
            {
                g.FillRectangle(OpenNodeColor, ((x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f)), ((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)),SqaureSize, SqaureSize);
            }

            else if(shapetype==Box.S)
            {
                OpenNodeColor = new SolidBrush(Color.Green);
                g.FillRectangle(OpenNodeColor, (x * SqaureSize) + 1 + ((x * SqaureSize) * 0.1f), ((y * SqaureSize) + 1 + ((y * SqaureSize) * 0.1f)), SqaureSize, SqaureSize);
                
            }

        }

        public void CreateGrid(int row, int col)
        {
             boxoffset = (panel1.Height/5);
            int mapoffset = (panel1.Height / 50);
            this.row = 51;
            this.col = 51;
            Cells = new Cell[6,6];//initialise the the cell in 2D matrix with the input row and col size
            Collider = new Cell[51,51];
            for (int x = 0; x <6; x++)
            {
                for (int y = 0; y <6 ; y++)
                {
                    Cells[x, y] = new Cell(x, y, Box.W0);//initiates a matrix to store the values with all blocks as walkable as default until user make changes with input
                    
                    ChangeColor(x, y, Color.White, boxoffset,Box.W0);
                }
            }
            for (int p = 0; p< 51; p++)
            {
                for (int q = 0; q <51; q++)
                {
                    Collider[p, q] = new Cell(p, q, Box.W0);
                }
            }
            
        }
        public void FindPath()
        {
            if (StartNode != null)
            {
                boxoffset = panel1.Width / 50;
                PossibleNode = pathFinder.PossibleNodes(CurrentNode, row, col, Collider);
                List<Cell> Obstacles = new List<Cell>();
                int obstaclefound = 0;
                foreach (Cell Node in PossibleNode)
                {

                    if (Node.box == Box.O)
                    {
                        Obstacles.Add(Node);
                        obstaclefound++;
                        ChangeColor(Node.CellPos.x, Node.CellPos.y, Color.Purple, boxoffset, Box.O);
                    }
                }
                foreach (Cell Node in Obstacles)
                {
                    ChangeColor(Node.CellPos.x, Node.CellPos.y, Color.Purple, boxoffset, Box.O);
                    PossibleNode.Remove(Node);
                }

                foreach (Cell Node in PossibleNode)
                {
                    if (!OpenNodes.Contains(Node))
                    {
                        OpenNodes.Enqueue(Node);
                    }
                }

                if(OpenNodes.Count>0)
                {
                    CurrentNode = OpenNodes.Dequeue();
                    CurrentNode.visited = true;
                    Collider[CurrentNode.CellPos.x, CurrentNode.CellPos.y] = CurrentNode;
                    ChangeColor(CurrentNode.CellPos.x, CurrentNode.CellPos.y, Color.Yellow, boxoffset, Box.W0);

                    if (OpenNodes.Count > 0)
                    {
                        FindPath();
                    }
                    else
                    {
                        return;
                    }
                }
                
            }
            else
                MessageBox.Show("NO Start Position Found");
            
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            boxoffset = panel1.Width / 50;
            double mousePoseX = Math.Ceiling(((e.X /boxoffset)-((e.X / boxoffset) * 0.1f)));
            double mousePoseY = Math.Ceiling(((e.Y / boxoffset) - ((e.Y /boxoffset) * 0.1f)));
            ChangeColor((int)mousePoseX, (int)mousePoseY, Color.Aqua, boxoffset,CurrentOption);
            if(CurrentOption==Box.S)
            {
                Collider[(int)mousePoseX, (int)mousePoseY].box = Box.S;
                Collider[(int)mousePoseX, (int)mousePoseY].visited = true;
                StartNode = Collider[(int)mousePoseX, (int)mousePoseY];
                CurrentNode = StartNode;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Board_Load(object sender, EventArgs e)
        {

        }
    }
}
