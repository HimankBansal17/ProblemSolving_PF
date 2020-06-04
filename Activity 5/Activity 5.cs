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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    public enum Box
    {
        S ,
        E ,
        W0 ,
        O ,
        square,
        circle,
        triangle,
        rectangle,
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
    class Activity_5
    {
        /*
        public void FindPathAgent1(Cell[,] Matrix,Index StartPoint, Index EndPoint, int RowSize, int ColSize)
        {

            List<Cell> PossibleNodes = PossibleNodes(Agent1CurrentCell, RowSize, ColSize, Matrix);


            //Change the Color of each Open Node in the list
            foreach (Cell Node in PossibleNodes)
            {
                //Thread.Sleep(100);

                if (Instance.CheckVisited(Node, ClosedNodes, Agent1OpenNodes, ClosedNodesAgent1, Agent2OpenNodes) == false)
                {
                    Agent1OpenNodes.Enqueue(Node); //Add the Possible node position 
                    Board.ChangeColor(Node.CellPos.x, Node.CellPos.y, Color.Brown);
                }

            }

            Agent1OpenNodes.TryDequeue(out Cell VisitedNode);
            Agent1CurrentCell = VisitedNode;
            ClosedNodesAgent1.Enqueue(Agent1CurrentCell);
            ClosedNodes.Enqueue(Agent1CurrentCell);

            Board.ChangeColor(Agent1CurrentCell.CellPos.x, Agent1CurrentCell.CellPos.y, ClosedNodeColor);

            if (Matrix[Agent1CurrentCell.CellPos.x, Agent1CurrentCell.CellPos.y].box == Box.E)
            {
                Instance.SelectTheFoundPath(Agent1CurrentCell, Board, StartPoint, ClosedNodesAgent1, PathColor);
                //  MessageBox.Show("Path Found By :" + Thread.CurrentThread.Name);
                Instance.FindPathAgent1(Matrix, Instance, StartPoint, EndPoint, RowSize, ColSize, Board, ClosedNodeColor, PathColor);
            }
            else if (Agent1OpenNodes.Count <= 0)
            {
                MessageBox.Show("Search Finished");
                return;
            }
            else
            {
                Agent1count++;
                Instance.FindPathAgent1(Matrix, Instance, StartPoint, EndPoint, RowSize, ColSize, Board, ClosedNodeColor, PathColor);
            }

        }*/
        public void SelectTheFoundPath(Cell CurrentNode, Board Board, Index StartPoint, Queue<Cell> ClosedNodes)
        {
            Cell PrevNode = CurrentNode.Parent;
            while (PrevNode.CellPos.x != StartPoint.x || PrevNode.CellPos.y != StartPoint.y)
            {
                PrevNode = CurrentNode.Parent;
                foreach (Cell Node in ClosedNodes)
                {
                    if (PrevNode.CellPos.x == Node.CellPos.x && PrevNode.CellPos.y == Node.CellPos.y)
                    {
                        CurrentNode = Node;
                        //Board.ChangeColor(PrevNode.CellPos.x, PrevNode.CellPos.y);
                        break;
                    }
                }


            }
        }
        public List<Cell> PossibleNodes(Cell CurrentCell, int RowSize, int ColSize, Cell[,] Matrix)
        {
            List<Cell> PossibleNodes = new List<Cell>();
            if (CurrentCell.CellPos.x - 1 >= 0 && CurrentCell.CellPos.y - 1 >= 0)
            {
                Cell TLNode =Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y - 1];
                if (
                       TLNode.visited != true
                        && TLNode.box != Box.S)
                {
                    TLNode.Parent = CurrentCell;
                    TLNode.ID = CellID.TL;
                    PossibleNodes.Add(TLNode);
                }
            }
            if (CurrentCell.CellPos.x + 1 < RowSize && CurrentCell.CellPos.y - 1 >= 0)
            {
                Cell TRNode = Matrix[CurrentCell.CellPos.x + 1, CurrentCell.CellPos.y - 1];
                if (
                        TRNode.visited != true
                        && TRNode.box != Box.S)
                {
                    TRNode.Parent = CurrentCell;
                    TRNode.ID = CellID.TR;
                    PossibleNodes.Add(TRNode);
                }
            }
            if (CurrentCell.CellPos.x - 1>= 0 && CurrentCell.CellPos.y + 1 < ColSize)
            {
                Cell BLNode =Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y + 1];
                if (
                        BLNode.visited != true
                        && BLNode.box != Box.S)
                {
                    BLNode.Parent = CurrentCell;
                    BLNode.ID = CellID.TR;
                    PossibleNodes.Add(BLNode);
                }
            }
            if (CurrentCell.CellPos.x + 1 < RowSize && CurrentCell.CellPos.y + 1< ColSize)
            {
                Cell BRNode =Matrix[CurrentCell.CellPos.x + 1, CurrentCell.CellPos.y + 1];
                if (
                         BRNode.visited != true
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
                Cell LeftNode =Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y];
                if (
                        LeftNode.visited != true
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
                Cell RightNode =Matrix[CurrentCell.CellPos.x + 1, CurrentCell.CellPos.y];

                if (RightNode.box != Box.O && RightNode.visited != true
                    && RightNode.box != Box.S)
                {
                    RightNode.Parent = CurrentCell;
                    RightNode.ID = CellID.Right;
                    PossibleNodes.Add(RightNode);
                }
                else if (RightNode.box == Box.O)
                {

                }

            }

            //to get the upnode
            if (CurrentCell.CellPos.y - 1 >= 0)
            {
                Cell UpNode =Matrix[CurrentCell.CellPos.x, CurrentCell.CellPos.y - 1];
                if (UpNode.box != Box.O && UpNode.visited != true
                    && UpNode.box != Box.S)
                {
                    UpNode.Parent = CurrentCell;
                    UpNode.ID = CellID.Up;
                    PossibleNodes.Add(UpNode);
                }
                else if (UpNode.box == Box.O)
                {

                }

            }

            //for getting the down node
            if (CurrentCell.CellPos.y + 1 < ColSize)
            {
                Cell DownNode =Matrix[CurrentCell.CellPos.x, CurrentCell.CellPos.y+1];
                if (DownNode.box != Box.O && DownNode.visited != true
                    &&DownNode.box != Box.S)
                {
                    DownNode.Parent = CurrentCell;
                    DownNode.ID = CellID.Down;
                    PossibleNodes.Add(DownNode);
                }

            }
            return PossibleNodes;

        }
    }
}
