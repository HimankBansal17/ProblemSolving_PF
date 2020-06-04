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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity_3
{

    public enum Box
    {
        S = 'S',
        E = 'E',
        W = 'W',
        W0 = 'w',
        W90 = '9',
        W120 = '0',
        O = 'O',
        Ww = 'R',
        Wg = 'G',
        Wr = 'M',
    }

    public enum CellID
    {
        Up,
        Down,
        Right,
        Left,
        TR,
        TL,
        BR,
        BL,
    }

    //To store the Current Position of each cell
    public class Index
    {
        public int x;
        public int y;
    }

    //To Store:
    //G-Cost,F-Cost,H-Cost for each node
    public struct CostOfTravel
    {
        //total cost of travel
        public float GCost;

        //Cost of travel from the start point
        public float FCost;

        //cost of travel from the end point
        public float HCost;
        public CostOfTravel(float _Gcost, float _Hcost)
        {
            GCost = _Gcost;
            HCost = _Hcost;
            FCost = GCost + HCost;
        }
    }
    public class Cell
    {
        public CellID ID;
        public Cell Parent;
        public bool Collected;
        public bool visited;
        public Index CellPos = new Index();
        public CostOfTravel CellCost;
        public Box box;

        public Cell(int _CellPosX, int _CellPosY, Box boxType)
        {
            box = boxType;
            CellPos.x = _CellPosX;
            CellPos.y = _CellPosY;
        }

    }
    /*Use Breadth First Search for the Finding the Items or the end points for multiple agents at the same time
    ******* Maximum number of Agents Can be Used are 8 ********/
    #region temp comment
    class PathFinder
    {
        Cell[,] CommonBoard;
        Index EndPoint;

        Cell Agent2CurrentCell;
        Cell Agent1CurrentCell;


        int Agent1count = 0;
        int Agent2count = 0;
        //Thread FoundingAgent = Thread.CurrentThread;                                //Founding Agent stores the name of the current Thread

        //Common Open Nodes and Closed Nodes
        //ConcurrentQueue<Cell> OpenNodes = new ConcurrentQueue<Cell>();            //Array for storing next possible nodes
        ConcurrentQueue<Cell> ClosedNodes = new ConcurrentQueue<Cell>();          //Array for storing the visited nodes as visited


        //ConcurrentQueue for Nodes of Agent 1
        ConcurrentQueue<Cell> ClosedNodesAgent1 = new ConcurrentQueue<Cell>();     //Array for storing visited Nodes for Agent 1
        ConcurrentQueue<Cell> Agent1OpenNodes = new ConcurrentQueue<Cell>();      //Array for storing   Open  Nodes for Agent 1

        //Queues for Nodes of Agent 2
        ConcurrentQueue<Cell> Agent2OpenNodes = new ConcurrentQueue<Cell>();      //Array for storing visited Nodes for agent1
        ConcurrentQueue<Cell> ClosedNodesAgent2 = new ConcurrentQueue<Cell>();     //Array for Storing visited Nodes for agent2

        //ConcurrentQueue for collecting the path found ********IF They Have Different Start Points****************
        ConcurrentQueue<Cell> Agent1Path = new ConcurrentQueue<Cell>();         //Array for storing path to end point for agent 1
        ConcurrentQueue<Cell> Agent2Path = new ConcurrentQueue<Cell>();         //Array for storing path to end point for agent 2    

        //bool Agent1Stop = false;
        //bool Agen2Stop = true;

        //Get Possible Nodes
        //Get Cost For Each Node
        //Add them to open node list
        //Modify the cells on  the board
        //Select the Next Node
        //Remove it from the Open Node List
        //Add it to the closed nodes list
        //Change the Cell on the board Board

        #region Find path Function for Agent 1
        public void FindPathAgent1(Cell[,] Matrix, PathFinder Instance,
        Index StartPoint, Index EndPoint, int RowSize, int ColSize, Board Board, Color ClosedNodeColor, Color PathColor)
        {
            if (Agent1count == 0)
            {
                Agent1CurrentCell= Matrix[StartPoint.x,StartPoint.y];
            }

            List<Cell> PossibleNodes = Instance.PossibleNodes(Agent1CurrentCell, RowSize, ColSize, Matrix);


            //Change the Color of each Open Node in the list
            foreach (Cell Node in PossibleNodes)
            {
                //Thread.Sleep(100);

                if (Instance.CheckVisited(Node, ClosedNodes, Agent1OpenNodes, ClosedNodesAgent1, Agent2OpenNodes) == false)
                {
                    Agent1OpenNodes.Enqueue(Node); //Add the Possible node position 
                    Board.ChangeColor(Node.CellPos.x, Node.CellPos.y,Color.Brown);
                }

            }

            Agent1OpenNodes.TryDequeue(out Cell VisitedNode);
            Agent1CurrentCell = VisitedNode;
            ClosedNodesAgent1.Enqueue(Agent1CurrentCell);
            ClosedNodes.Enqueue(Agent1CurrentCell);

            Board.ChangeColor(Agent1CurrentCell.CellPos.x, Agent1CurrentCell.CellPos.y, ClosedNodeColor);

            if (Matrix[Agent1CurrentCell.CellPos.x, Agent1CurrentCell.CellPos.y].box == Box.E)
            {
                CommonBoard[Agent1CurrentCell.CellPos.x, Agent1CurrentCell.CellPos.y].box = Box.W;
                Instance.SelectTheFoundPath(Agent1CurrentCell, Board, StartPoint, ClosedNodesAgent1, PathColor);
                Board.ChangeColor(Agent1CurrentCell.CellPos.x, Agent1CurrentCell.CellPos.y, Color.Aqua);
                //MessageBox.Show("Path Found By :" + Thread.CurrentThread.Name);
                Instance.FindPathAgent1(Matrix, Instance, StartPoint, EndPoint, RowSize, ColSize, Board, ClosedNodeColor, PathColor);
            }
           else if(Agent1OpenNodes.Count<=0)
            {
                MessageBox.Show("Search Finished");
                return;
            }
            else
            {
                Agent1count++;
                Instance.FindPathAgent1(Matrix, Instance, StartPoint, EndPoint, RowSize, ColSize, Board, ClosedNodeColor, PathColor);
            }

        }
        #endregion


        #region Find Path Function for Agent2
        public void FindPath(Cell[,] Matrix, PathFinder Instance,
        Index StartPoint, Index EndPoint, int RowSize, int ColSize, Board Board, Color ClosedNodeColor, Color PathColor)
        {
            if (Agent2count == 0)
            {
                Agent2CurrentCell = Matrix[StartPoint.x,StartPoint.y];
            }

            List<Cell> PossibleNodes = Instance.PossibleNodes(Agent2CurrentCell, RowSize, ColSize, Matrix);


            //Change the Color of each Open Node in the list
            foreach (Cell Node in PossibleNodes)
            {
                //Thread.Sleep(100);
                if (Instance.CheckVisited(Node, ClosedNodes, Agent2OpenNodes, ClosedNodesAgent2, Agent1OpenNodes) == false)
                {
                    Agent2OpenNodes.Enqueue(Node); //Add the Possible node position 
                    Board.ChangeColor(Node.CellPos.x, Node.CellPos.y,Color.Red);
                    //Board[Node.CellPos.x, Node.CellPos.y].Style.BackColor = Color.Green;   //Change the color of the current open nodes
                }

            }
            if (Agent2OpenNodes.Count > 0)
            {
                Agent2OpenNodes.TryDequeue(out Cell VisitedNode);
                Agent2CurrentCell = VisitedNode;
                ClosedNodesAgent2.Enqueue(Agent2CurrentCell);
                ClosedNodes.Enqueue(Agent2CurrentCell);
                Board.ChangeColor(Agent2CurrentCell.CellPos.x, Agent2CurrentCell.CellPos.y, ClosedNodeColor);
            }
            if (Matrix[Agent2CurrentCell.CellPos.x, Agent2CurrentCell.CellPos.y].box ==Box.E)
            {
                Instance.SelectTheFoundPath(Agent2CurrentCell, Board, StartPoint, ClosedNodesAgent2, PathColor);
                CommonBoard[Agent2CurrentCell.CellPos.x, Agent2CurrentCell.CellPos.y].box = Box.W;
                Board.ChangeColor(Agent2CurrentCell.CellPos.x, Agent2CurrentCell.CellPos.y, Color.Plum);
                //MessageBox.Show("Path Found By :" + Thread.CurrentThread.Name,"Result");
                Instance.FindPath(Matrix, Instance, StartPoint, EndPoint, RowSize, ColSize, Board, ClosedNodeColor, PathColor);
            }
            else if(Agent2OpenNodes.Count <= 0)
                {
                    MessageBox.Show("Search Finished");
                    return;
                }

                else
            {
                Agent2count++;
                Instance.FindPath(Matrix, Instance, StartPoint, EndPoint, RowSize, ColSize, Board, ClosedNodeColor, PathColor);
            }
            
        }
        #endregion

        #region Common functions for agent one and two to generate paths to the end point using BreadthFirst Search
        public void SelectTheFoundPath(Cell CurrentNode, Board Board, Index StartPoint, ConcurrentQueue<Cell> ClosedNodes, Color PathColor)
        {
            Cell PrevNode= CurrentNode.Parent; 
            while (PrevNode.CellPos.x != StartPoint.x || PrevNode.CellPos.y != StartPoint.y)
            {
                PrevNode = CurrentNode.Parent;
                foreach (Cell Node in ClosedNodes)
                {
                    if (PrevNode.CellPos.x == Node.CellPos.x && PrevNode.CellPos.y == Node.CellPos.y)
                    {
                        CurrentNode = Node;
                        if (Thread.CurrentThread.Name == "Agent1")
                        {
                            Agent1Path.Enqueue(PrevNode);
                        }
                        else if (Thread.CurrentThread.Name == "Agent2")
                        {
                            Agent2Path.Enqueue(PrevNode);
                        }
                        Board.ChangeColor(PrevNode.CellPos.x,PrevNode.CellPos.y,PathColor);
                        break;
                    }
                }


            }
        }
        public void SetAgents(Cell[,] Matrix, PathFinder Instance, Index StartPoint, Index EndPoint, int RowSize, int ColSize, Board Board)
        {
            Index Agent1StartPoint = new Index();
            Index Agent2StartPoint = new Index();
            CommonBoard = Matrix;
            Cell StartPoints = new Cell(StartPoint.x,StartPoint.y,Box.S);
            StartPoints.CellPos = StartPoint;
            List<Cell> PossibleStartPoints = new List<Cell>();
            PossibleStartPoints = Instance.PossibleNodes(StartPoints, RowSize, ColSize, CommonBoard);
            if (PossibleStartPoints.Count > 1)
            {
                Agent1StartPoint = PossibleStartPoints[0].CellPos;
                Agent2StartPoint = PossibleStartPoints[1].CellPos;
            }

            Thread Agent1 = new Thread(() => FindPathAgent1(CommonBoard, Instance, Agent1StartPoint, EndPoint, RowSize, ColSize, Board, Color.Purple, Color.White));
            Thread Agent2 = new Thread(() => FindPath(CommonBoard, Instance, Agent2StartPoint, EndPoint, RowSize, ColSize, Board, Color.Green, Color.Blue));
            Agent1.Name = "Agent1";
            Agent2.Name = "Agent2";
            Agent1.Start();
            Agent2.Start();

           
        }

        public bool CheckVisited(Cell Node, ConcurrentQueue<Cell> ClosedNodes, ConcurrentQueue<Cell> OpenNodes,
            ConcurrentQueue<Cell> AgentClosedNodes, ConcurrentQueue<Cell> AgentOpenNodes)
        {
            bool Visited = false;
            foreach (Cell ClosedNode in ClosedNodes)
            {
                if (ClosedNode.CellPos.x == Node.CellPos.x && ClosedNode.CellPos.y == Node.CellPos.y)
                {
                    Visited = true;
                    break;
                }
            }

            foreach (Cell OpenNode in AgentOpenNodes)
            {
                if (Node.CellPos.x == OpenNode.CellPos.x && Node.CellPos.y == OpenNode.CellPos.y)
                {
                    Visited = true;
                    break;
                }
            }

            foreach (Cell AgentNode in AgentClosedNodes)
            {
                if (AgentNode.CellPos.x == Node.CellPos.x && AgentNode.CellPos.y == Node.CellPos.y)
                {
                    Visited = true;
                    break;
                }
            }

            foreach (Cell OpenNode in OpenNodes)
            {
                if (Node.CellPos.x == OpenNode.CellPos.x && Node.CellPos.y == OpenNode.CellPos.y)
                {
                    Visited = true;
                    break;
                }
            }
            return Visited;
        }

        public List<Cell> PossibleNodes(Cell CurrentCell, int RowSize, int ColSize, Cell[,] Matrix)
        {
            List<Cell> PossibleNodes = new List<Cell>();
            if(CurrentCell.CellPos.x-1>=0&&CurrentCell.CellPos.y-1>=0)
            {
                Cell TLNode = new Cell(CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y-1, Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y-1].box);
                if (
                       TLNode.box != Box.O
                        && TLNode.visited != true
                        && TLNode.box != Box.S)
                {
                    TLNode.Parent = CurrentCell;
                    TLNode.ID = CellID.TL;
                    PossibleNodes.Add(TLNode);
                }
            }
            if (CurrentCell.CellPos.x + 1 < RowSize && CurrentCell.CellPos.y - 1 >= 0)
            {
                Cell TRNode = new Cell(CurrentCell.CellPos.x+1, CurrentCell.CellPos.y - 1, Matrix[CurrentCell.CellPos.x + 1, CurrentCell.CellPos.y - 1].box);
                if (
                       TRNode.box != Box.O
                        && TRNode.visited != true
                        && TRNode.box != Box.S)
                {
                    TRNode.Parent = CurrentCell;
                    TRNode.ID = CellID.TR;
                    PossibleNodes.Add(TRNode);
                }
            }
            if (CurrentCell.CellPos.x -1 >=0 && CurrentCell.CellPos.y +1<ColSize)
            {
                Cell BLNode = new Cell(CurrentCell.CellPos.x -1, CurrentCell.CellPos.y + 1, Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y + 1].box);
                if (
                       BLNode.box != Box.O
                        && BLNode.visited != true
                        && BLNode.box != Box.S)
                {
                    BLNode.Parent = CurrentCell;
                    BLNode.ID = CellID.TR;
                    PossibleNodes.Add(BLNode);
                }
            }
            if (CurrentCell.CellPos.x + 1 <RowSize && CurrentCell.CellPos.y + 1 <ColSize)
            {
                Cell BRNode = new Cell(CurrentCell.CellPos.x+1, CurrentCell.CellPos.y + 1, Matrix[CurrentCell.CellPos.x+1, CurrentCell.CellPos.y + 1].box);
                if (
                       BRNode.box != Box.O
                        && BRNode.visited != true
                        && BRNode.box != Box.S)
                {
                    BRNode.Parent = CurrentCell;
                    BRNode.ID = CellID.TR;
                    PossibleNodes.Add(BRNode);
                }
            }
            //to get the left node
            if (CurrentCell.CellPos.x - 1 >= 0)
            {
                Cell LeftNode = new Cell(CurrentCell.CellPos.x - 1,CurrentCell.CellPos.y,Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y].box);
                if (
                       LeftNode.box !=Box.O
                        && LeftNode.visited != true
                        && LeftNode.box != Box.S)
                {
                    LeftNode.Parent = CurrentCell;
                    LeftNode.ID = CellID.Left;
                    PossibleNodes.Add(LeftNode);

                }

            }

            //to get the right node
            if (CurrentCell.CellPos.x + 1 < RowSize)
            {
                Cell RightNode = new Cell(CurrentCell.CellPos.x + 1, CurrentCell.CellPos.y, Matrix[CurrentCell.CellPos.x+1, CurrentCell.CellPos.y].box);

                if (RightNode.box != Box.O && RightNode.visited != true
                    && RightNode.box != Box.S)
                {
                    RightNode.Parent = CurrentCell;
                    RightNode.ID = CellID.Right;
                    PossibleNodes.Add(RightNode);
                }

            }

            //to get the upnode
            if (CurrentCell.CellPos.y - 1 >= 0)
            {
                Cell UpNode = new Cell(CurrentCell.CellPos.x, CurrentCell.CellPos.y - 1, Matrix[CurrentCell.CellPos.x, CurrentCell.CellPos.y-1].box);
                if (UpNode.box != Box.O && UpNode.visited != true
                    && UpNode.box != Box.S)
                {
                    UpNode.Parent = CurrentCell;
                    UpNode.ID = CellID.Up;
                    PossibleNodes.Add(UpNode);
                }

            }

            //for getting the down node
            if (CurrentCell.CellPos.y + 1 < ColSize)
                {
                Cell DownNode = new Cell(CurrentCell.CellPos.x, CurrentCell.CellPos.y + 1,Matrix[CurrentCell.CellPos.x,CurrentCell.CellPos.y].box);
                    if (Matrix[DownNode.CellPos.x, DownNode.CellPos.y].box != Box.O && Matrix[DownNode.CellPos.x, DownNode.CellPos.y].visited != true
                        && Matrix[DownNode.CellPos.x, DownNode.CellPos.y].box != Box.S)
                    {
                        DownNode.Parent = CurrentCell;
                        DownNode.ID = CellID.Down;
                        PossibleNodes.Add(DownNode);
                    }

                }
            return PossibleNodes;

        }

        
        /*
        public List<Cell> CostCalculator(List<Cell> PossibleNodes)
        {
            return PossibleNodes;
        }

        public Cell SelectNextNode(List<Cell> OpenNodeList)
        {
            Cell NextNode = new Cell();
            return NextNode;
        }*/
        #endregion
    }

    #endregion
}
