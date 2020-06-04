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

namespace Assignment____Forms_.Activity_1
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
         public CostOfTravel(float _Gcost,float _Hcost)
         {
            GCost = _Gcost;
            HCost = _Hcost;
            FCost = GCost + HCost;
         }
    }
    public class Cell
    {
        public Cell Parent;
        public bool visited;
        public Index CellPos=new Index();
        public CostOfTravel CellCost;
        public Box box;
     
        public Cell(int _CellPosX,int _CellPosY,Box boxType)
        {
            box = boxType;
            CellPos.x = _CellPosX;
            CellPos.y = _CellPosY;
        }

    }

    class Activity_1
    {
        //All the variables used for The calls program
        public int rowsize;
        public int columnsize;


        //Function responsible Collection Possible Future Nodes for Current Node
    }


    public class FindPath
    {
        //This function collects next possible nodes for the possible future movement
        public List<Cell> PossibleNodes(Cell CurrentNode,int ColSize,int RowSize,Cell[,] Matrix)
        {
            List<Cell> OpenNodes = new List<Cell>();
            //to get the left node
            if (CurrentNode.CellPos.x - 1 >= 0)
            {
                Cell LeftNode = Matrix[CurrentNode.CellPos.x - 1, CurrentNode.CellPos.y];

                if (LeftNode.box!=Box.O)
                {
                    if(LeftNode.Parent!=null)
                    {
                        if (CurrentNode.CellCost.GCost + 5 < LeftNode.Parent.CellCost.GCost + 5)
                        {
                            LeftNode.Parent = CurrentNode;
                        }
                    }
                    else
                    {
                        LeftNode.Parent = CurrentNode;
                    }
                    OpenNodes.Add(LeftNode);
                }
            }

            //to get the right node
            if (CurrentNode.CellPos.x + 1 < RowSize)
            {
                Cell RightNode = Matrix[CurrentNode.CellPos.x + 1, CurrentNode.CellPos.y];
                if (RightNode.box != Box.O)
                {
                        if (RightNode.Parent != null)
                        {
                            if (CurrentNode.CellCost.GCost + 5< RightNode.Parent.CellCost.GCost + 5)
                            {
                                RightNode.Parent = CurrentNode;
                            }
                        }
                        else
                        {
                            RightNode.Parent = CurrentNode;
                        }

                    OpenNodes.Add(RightNode);
                }
            }

            // to get the upnode
            if (CurrentNode.CellPos.y - 1 >= 0)
            {
                Cell UpNode = Matrix[CurrentNode.CellPos.x, CurrentNode.CellPos.y - 1];
                if (UpNode.box != Box.O)
                {

                            if (UpNode.Parent != null)
                            {
                                if (CurrentNode.CellCost.GCost + 5 < UpNode.Parent.CellCost.GCost + 5)
                                {
                                    UpNode.Parent = CurrentNode;
                                }
                            }
                            else
                            {
                                UpNode.Parent = CurrentNode;

                            }
                        OpenNodes.Add(UpNode);
                }
            }

            //for getting the down node
            if (CurrentNode.CellPos.y + 1 < ColSize)
            {
                Cell DownNode = Matrix[CurrentNode.CellPos.x, CurrentNode.CellPos.y + 1];
                if (DownNode.box != Box.O)
                {
                                if (DownNode.Parent != null)
                                {
                                    if (CurrentNode.CellCost.GCost + 5 < DownNode.Parent.CellCost.GCost + 5)
                                    {
                                        DownNode.Parent = CurrentNode;
                                    }
                                }
                                else
                                {
                                    DownNode.Parent = CurrentNode;
                                }
                        OpenNodes.Add(DownNode);
                }
            }
            return OpenNodes;
        }

        //This Assignes all the possible nodesSuitable cost according to the property of hte node
        public List<Cell> CostOfOpenNodes(List<Cell> PossibleNodes,Index StartPos,Index EndPos,Form1.pathtype pathtype)
        {
            int columncount = 0;
            int rowcount = 0;
             int GrassPenalty=0;
            int Rockspenlaty=0;
            int WaterPenalty=0;
            int Slope40Penalty=0;
            int Slope90penalty=0;
            int Slope120penalty=0;
            if (pathtype==Form1.pathtype.easypath)
            {
                 GrassPenalty = 6;
                 Rockspenlaty = 1;
                 WaterPenalty = 8;
                 Slope40Penalty =6;
                 Slope90penalty = 0;
                 Slope120penalty = 3;
            }
            else if (pathtype == Form1.pathtype.shortestpath)
            {
                GrassPenalty = 0;
                Rockspenlaty = 0;
                WaterPenalty = 0;
                Slope40Penalty = 0;
                Slope90penalty = 0;
                Slope120penalty = 0;
            }
            else if (pathtype == Form1.pathtype.hardestpath)
            {
                GrassPenalty = -10;
                Rockspenlaty = -5;
                WaterPenalty = -30;
                Slope40Penalty =-40;
                Slope90penalty = -50;
                Slope120penalty = -10;

            }

                Cell[] ArrayPossibleNode = PossibleNodes.ToArray();
            PossibleNodes.Clear();
            //To Calculate the HCost
            for (int i = 0; i < ArrayPossibleNode.Length; i++)
            {
                columncount = 0;
                rowcount = 0;
                columncount = Math.Abs(ArrayPossibleNode[i].CellPos.y - EndPos.y);
                rowcount = Math.Abs(ArrayPossibleNode[i].CellPos.x - EndPos.x);
                int dx2 = Math.Abs(StartPos.x - EndPos.x);
                int dy2 = Math.Abs(StartPos.y - EndPos.y);
                //float cross = Math.Abs(rowcount * dy2 - dx2 * columncount);
                //ArrayPossibleNode[i].CellCost.HCost = 20+ cross*0.001f;
                if(ArrayPossibleNode[i].box==Box.Wg)
                {
                    ArrayPossibleNode[i].CellCost.HCost = ((columncount + rowcount) *10.0f)+GrassPenalty;
                }
                else if(ArrayPossibleNode[i].box == Box.Ww)
                {
                    ArrayPossibleNode[i].CellCost.HCost = ((columncount + rowcount) *10.0f) + WaterPenalty;
                }
                else if (ArrayPossibleNode[i].box == Box.Wr)
                {
                    ArrayPossibleNode[i].CellCost.HCost = ((columncount + rowcount) * 10.0f) + Rockspenlaty;
                }
                else if(ArrayPossibleNode[i].box == Box.W90)
                {
                    ArrayPossibleNode[i].CellCost.HCost = ((columncount + rowcount) * 10.0f) +Slope90penalty;
                }
                else if (ArrayPossibleNode[i].box == Box.W120)
                {
                    ArrayPossibleNode[i].CellCost.HCost = ((columncount + rowcount) * 10.0f) + Slope120penalty;
                }
                else if (ArrayPossibleNode[i].box == Box.W0)
                {
                    ArrayPossibleNode[i].CellCost.HCost = ((columncount + rowcount) * 10.0f) + Slope40Penalty;
                }
                else
                    ArrayPossibleNode[i].CellCost.HCost = (columncount + rowcount) * (10.0f);


            }

            //To Calculate The GCost
            for (int i = 0; i < ArrayPossibleNode.Length; i++)
            {
                columncount = 0;
                rowcount = 0;
                
                columncount = Math.Abs(ArrayPossibleNode[i].CellPos.y - StartPos.y);
                rowcount = Math.Abs(ArrayPossibleNode[i].CellPos.x - EndPos.x);

                if(ArrayPossibleNode[i].Parent!=null)
                {
                    ArrayPossibleNode[i].CellCost.GCost= ArrayPossibleNode[i].Parent.CellCost.GCost+10;
                }
                else
                {
                    ArrayPossibleNode[i].CellCost.GCost = 10;
                }

                ArrayPossibleNode[i].CellCost = new CostOfTravel(ArrayPossibleNode[i].CellCost.GCost, ArrayPossibleNode[i].CellCost.HCost);
                PossibleNodes.Add(ArrayPossibleNode[i]);
            }

            return PossibleNodes;
        }
        
        //this functions selectes the next node from the open node list which has the lowest cost of travel
        //for Shortest path
        public Cell SelectNextNode(List<Cell> OpenNodes)
        {
            Cell NextNode=OpenNodes[0];
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

