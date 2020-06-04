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

namespace Assignment_4
{
    public enum Box
    {
        S,
        E,
        W0,
        O,
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
    class Activity_4
    {
         public Cell SelectTheFoundPath(Cell CurrentNode, Index StartPoint, Stack<Cell> ClosedNodes)
        {
            Cell PrevNode = CurrentNode.Parent;
                foreach (Cell Node in ClosedNodes)
                {
                    if (PrevNode.CellPos.x== Node.CellPos.x&&PrevNode.CellPos.y==Node.CellPos.y)
                    {
                        CurrentNode = Node;
                        break;
                    }
                }
            return CurrentNode;
        }
        public Cell PossibleNodes(Cell CurrentCell, int RowSize, int ColSize, Cell[,] Matrix,Stack<Cell> OpenNodes,Queue<Cell> ClosedNodes)
        {
            
            //to get the left node
            if (CurrentCell.CellPos.x - 1 >= 0)
            {
                Cell LeftNode =Matrix[CurrentCell.CellPos.x - 1, CurrentCell.CellPos.y];
                if (!OpenNodes.Contains(LeftNode) && !ClosedNodes.Contains(LeftNode) &&
                        LeftNode.visited != true
                        && LeftNode.box != Box.S)
                {
                    LeftNode.Parent = CurrentCell;
                    LeftNode.ID = CellID.Left;
                    return LeftNode;
                    //PossibleNodes.Add(LeftNode);

                }

            }

            //to get the right node
            if (CurrentCell.CellPos.x + 1 < RowSize)
            {
                Cell RightNode =Matrix[CurrentCell.CellPos.x + 1, CurrentCell.CellPos.y];

                if (!OpenNodes.Contains(RightNode) && !ClosedNodes.Contains(RightNode) &&
                    RightNode.visited != true
                    && RightNode.box != Box.S)
                {
                    RightNode.Parent = CurrentCell;
                    RightNode.ID = CellID.Right;
                    return RightNode;
                    //PossibleNodes.Add(RightNode);
                }

            }

            //to get the upnode
            if (CurrentCell.CellPos.y - 1 >= 0)
            {
                Cell UpNode =Matrix[CurrentCell.CellPos.x, CurrentCell.CellPos.y - 1];
                if (!OpenNodes.Contains(UpNode) && !ClosedNodes.Contains(UpNode) && UpNode.visited != true
                    && UpNode.box != Box.S)
                {
                    UpNode.Parent = CurrentCell;
                    UpNode.ID = CellID.Up;
                    return UpNode;
                    //PossibleNodes.Add(UpNode);
                }

            }

            //for getting the down node
            if (CurrentCell.CellPos.y + 1 < ColSize)
            {
                Cell DownNode =Matrix[CurrentCell.CellPos.x, CurrentCell.CellPos.y+1];
                if (!OpenNodes.Contains(DownNode) && !ClosedNodes.Contains(DownNode) && DownNode.visited != true
                    &&DownNode.box != Box.S)
                {
                    DownNode.Parent = CurrentCell;
                    DownNode.ID = CellID.Down;
                    return DownNode;
                    //PossibleNodes.Add(DownNode);
                }

            }

            return null;

        }
    }
}
