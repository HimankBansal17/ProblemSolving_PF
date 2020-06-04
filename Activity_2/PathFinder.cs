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

namespace Activity_2
{

    //Enum To set the properties of each box in the matrix
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
    class PathFinder
    {
        public int rowsize;
        public int columnsize;
        public List<Cell> PossibleNodes(Cell CurrentNode, int ColSize, int RowSize, Cell[,] Matrix)
        {
            List<Cell> OpenNodes = new List<Cell>();
            

            //to get the left node
            if (CurrentNode.CellPos.x - 1 >= 0)
            {
                Cell LeftNode = Matrix[CurrentNode.CellPos.x - 1, CurrentNode.CellPos.y];

                if (LeftNode.box != Box.O)
                {
                   
                    LeftNode.ID = CellID.Left;
                    OpenNodes.Add(LeftNode);
                }
            }

            //to get the right node
            if (CurrentNode.CellPos.x + 1 < RowSize)
            {
                Cell RightNode = Matrix[CurrentNode.CellPos.x + 1, CurrentNode.CellPos.y];
                if (RightNode.box != Box.O)
                {
                    
                    RightNode.ID = CellID.Right;
                    OpenNodes.Add(RightNode);
                }
            }

            // to get the upnode
            if (CurrentNode.CellPos.y - 1 >= 0)
            {
                Cell UpNode = Matrix[CurrentNode.CellPos.x, CurrentNode.CellPos.y - 1];
                if (UpNode.box != Box.O)
                {
                    
                    UpNode.ID = CellID.Up;
                    OpenNodes.Add(UpNode);
                }
            }

            //for getting the down node
            if (CurrentNode.CellPos.y + 1 < ColSize)
            {
                Cell DownNode = Matrix[CurrentNode.CellPos.x, CurrentNode.CellPos.y + 1];
                if (DownNode.box != Box.O)
                {
                    
                    DownNode.ID = CellID.Down;
                    OpenNodes.Add(DownNode);
                }
            }
            //TL
            if (CurrentNode.CellPos.x - 1 >= 0 && CurrentNode.CellPos.y - 1 >= 0)
            {
                Cell TLNode = Matrix[CurrentNode.CellPos.x - 1, CurrentNode.CellPos.y - 1];
                if (TLNode.box != Box.O)
                {
                    TLNode.ID = CellID.TL;
                    OpenNodes.Add(TLNode);
                }
            }
            //TR
            if (CurrentNode.CellPos.x + 1 < RowSize && CurrentNode.CellPos.y - 1 >= 0)
            {
                Cell TRNode = Matrix[CurrentNode.CellPos.x + 1, CurrentNode.CellPos.y - 1];
                if (TRNode.box != Box.O)
                {
                    TRNode.ID = CellID.TR;
                    OpenNodes.Add(TRNode);
                }
            }
            //BL
            if (CurrentNode.CellPos.x - 1 >= 0 && CurrentNode.CellPos.y + 1 < ColSize)
            {
                Cell BLNode = Matrix[CurrentNode.CellPos.x - 1, CurrentNode.CellPos.y + 1];
                if (BLNode.box != Box.O)
                {

                    BLNode.ID = CellID.BL;
                    OpenNodes.Add(BLNode);
                }
            }
            //BR
            if (CurrentNode.CellPos.x + 1 < RowSize && CurrentNode.CellPos.y + 1 < ColSize)
            {
                Cell BRNode = Matrix[CurrentNode.CellPos.x + 1, CurrentNode.CellPos.y + 1];
                if (BRNode.box != Box.O)
                {
                   
                    BRNode.ID = CellID.BR;
                    OpenNodes.Add(BRNode);
                }
            }

            return OpenNodes;
        }
        public List<Cell> CostOfOpenNodes(List<Cell> PossibleNodes, Index StartPos, Index EndPos,Cell CurrentNode,List<Cell> OpenNodes,Board.pathtype pathtype)
        {
             int RocksPenalty = 300;
            int WaterPenalty = 600;
            int GrassPenalty = 400;
            int Slope40Penalty = 0;
            int Slope90Penalty = 0;
            int Slope120Penalty = 0;
            if (pathtype == Board.pathtype.easypath)
            {
                GrassPenalty = 6;
                RocksPenalty = 1;
                WaterPenalty = 8;
                Slope40Penalty = 6;
                Slope90Penalty = 9;
                Slope120Penalty = 3;
            }
            else if (pathtype == Board.pathtype.shortestpath)
            {
                GrassPenalty = 0;
                RocksPenalty = 0;
                WaterPenalty = 0;
                Slope40Penalty = 0;
                Slope90Penalty = 0;
                Slope120Penalty = 0;
            }
            else if (pathtype == Board.pathtype.hardestpath)
            {
                GrassPenalty = -1;
                RocksPenalty = -5;
                WaterPenalty = -3;
                Slope40Penalty = -4;
                Slope90Penalty = -5;
                Slope120Penalty = -1;

            }

            Cell[] ArrayPossibleNode = PossibleNodes.ToArray();
            //PossibleNodes.Clear();

            foreach (Cell PossibleNode in PossibleNodes)
            {
                double TravelCostToNewNode;
                if (PossibleNode.Parent!=null)
                {
                    TravelCostToNewNode = PossibleNode.Parent.CellCost.GCost + GetDistance(StartPos, PossibleNode.CellPos);
                }
                else
                {
                    TravelCostToNewNode =GetDistance(StartPos, PossibleNode.CellPos);
                }

                if(TravelCostToNewNode<PossibleNode.CellCost.GCost||PossibleNode.Parent==null)
                {
                    //if(PossibleNode.ID==CellID.BL|| PossibleNode.ID == CellID.TL|| PossibleNode.ID == CellID.TR|| PossibleNode.ID == CellID.BR)
                    //{
                        PossibleNode.CellCost.GCost = (int)TravelCostToNewNode;
                    if (PossibleNode.box == Box.Wg)
                    {
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos) + GrassPenalty;
                    }
                    else if (PossibleNode.box == Box.Wr)
                    {
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos) + RocksPenalty;
                    }
                    else if (PossibleNode.box == Box.Ww)
                    {
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos) + WaterPenalty;
                    }
                    else if (PossibleNode.box == Box.W90)
                    {
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos) + Slope90Penalty;
                    }
                    else if (PossibleNode.box == Box.W120)
                    {
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos) + Slope120Penalty;
                    }
                    else if (PossibleNode.box == Box.W0)
                    {
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos) + Slope40Penalty;
                    }
                    else
                        PossibleNode.CellCost.HCost = (int)GetDistance(PossibleNode.CellPos, EndPos);
                        //}


                        PossibleNode.CellCost = new CostOfTravel(PossibleNode.CellCost.GCost, PossibleNode.CellCost.HCost);
                    PossibleNode.Parent = CurrentNode;

                    if(!OpenNodes.Contains(PossibleNode))
                    {
                        continue;
                    }
                    else
                    {
                        OpenNodes[OpenNodes.FindIndex(ind => ind.Equals(PossibleNode))].CellCost = new CostOfTravel(PossibleNode.CellCost.GCost, PossibleNode.CellCost.HCost);
                    }
                }
                
                
                
            }

            return PossibleNodes;
        }
        int GetDistance(Index nodeA, Index nodeB)
        {
            
            int distX = Math.Abs(nodeA.x - nodeB.x);
            int distY = Math.Abs(nodeA.y - nodeB.y);
                
           if (distX > distY)
            {
                return 5*(distX-distY)+10*distY;
            }
            else
                return 5* (distY- distX)+10*distX;
        }
        public Cell SelectNextNode(List<Cell> OpenNodes)
        {
            Cell NextNode = OpenNodes[0];
            foreach (Cell Node in OpenNodes)
            {
                if (NextNode.CellCost.FCost > Node.CellCost.FCost)
                {
                    NextNode = Node;
                }
                else if (NextNode.CellCost.FCost == Node.CellCost.FCost)
                {
                    if (NextNode.CellCost.HCost > Node.CellCost.HCost)
                    {
                        NextNode = Node;
                    }
                }
            }
            return NextNode;
        }
    }
}

