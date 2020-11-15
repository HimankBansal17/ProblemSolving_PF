using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder_Interface
{
    //make agent interface
    //make agent class
    //path request interface
    //path finder action
    //path  class
    //path instance in agents
    public class Node
    {
        public Vector2 Position;
        public TravelCost travelCost;
        public Node ParentNode;
        public NeighbourNodes neighbourNodes;

        public Node(int x, int y)
        {
            Position.x = x;
            Position.y = y;
        }


    }
    public class TravelCost
    {
        private int _GCost;
        private int _HCost;
        private int _FCost;
        public int GCost
        {
            get
            {
                return _GCost;
            }
            set
            {
                _GCost = value;
            }
        }

        //To Generate the HCost
        public int HCost
        {
            get;
            set;
        }


        //To generate the Fcost
        public int FCost
        {
            get
            {
                return _GCost + _HCost;
            }
        }

    }


    public partial class Board
    {
        public int GridX;
        public int GridY;
        public Node[,] Grid;
    }
    public class Path : Board
    {
        Node startPoint;
        Node EndPoint;
        Node CurrentNode;
        Node[] path;//Stores the final path each time a path is being searched

        //collection of possible nodes for future paths
        Dictionary<int, Node> OpenNodes;

        //collection of visted nodes from the grid that can not be re visited for finding path
        Dictionary<int, Node> VisitedNodes;
        public Path(Node _Start, Node _Goal)
        {
            startPoint = _Start;
            EndPoint = _Goal;
            path = FindPath();
        }

        public Node[] FindPath()
        {
            return path;
        }
    }

    public class Agent
    {
        private Node StartPositon;
        private Node Target;
        private Path path;

        //to make the agent request a new path every time called
        public void PathRequest()
        {
            path = new Path(StartPositon, Target);
        }

    }
    public class NeighbourNodes : Board
    {
        public List<Node> GetNeighbourNodes(Node CurrentNode)
        {
            List<Node> NeighbourNodes = new List<Node>();
            #region horizontal Nodes
            //check if the current node is in the grid
            if (CurrentNode.Position.x + 1 < GridX)
            {
                Node RightNode = Grid[CurrentNode.Position.x + 1, CurrentNode.Position.y];
                //check if the node has a parent
                //if no parent
                if (RightNode.ParentNode == null)
                {
                    NeighbourNodes.Add(RightNode);
                }
                //if has a parent check the gcost diff in new parent and old parent
                else
                {
                    CompareGcost(CurrentNode, RightNode, 10);
                }
            }
            //check if the current node is in the grid
            if (CurrentNode.Position.x - 1 > 0)
            {
                Node LeftNode = Grid[CurrentNode.Position.x - 1, CurrentNode.Position.y];
                if (LeftNode.ParentNode == null)
                {
                    NeighbourNodes.Add(LeftNode);
                }
                else
                {
                    CompareGcost(CurrentNode, LeftNode, 10);
                }
            }
            #endregion

            #region Vertical Nodes
            //check if the current node is in the grid
            if (CurrentNode.Position.y - 1 > 0)
            {
                Node BottomNode = Grid[CurrentNode.Position.x, CurrentNode.Position.y - 1];
                if (BottomNode.ParentNode == null)
                {
                    NeighbourNodes.Add(BottomNode);
                }
                else
                {
                    CompareGcost(CurrentNode, BottomNode, 10);
                }
            }

            //check if the current node is in the grid
            if (CurrentNode.Position.y + 1 > 0)
            {
                Node TopNode = Grid[CurrentNode.Position.x, CurrentNode.Position.y + 1];
                if (TopNode.ParentNode == null)
                {
                    NeighbourNodes.Add(TopNode);
                }
                else
                {
                    CompareGcost(CurrentNode, TopNode, 10);
                }
            }
            #endregion

            #region Diagonal Nodes
            //check if the current node is in the grid
            if (CurrentNode.Position.y - 1 >= 0 && CurrentNode.Position.x - 1 >= 0)
            {
                Node BLNode = Grid[CurrentNode.Position.x - 1, CurrentNode.Position.y - 1];
                if (BLNode.ParentNode == null)
                {
                    NeighbourNodes.Add(BLNode);
                }
                else
                {
                    CompareGcost(CurrentNode, BLNode, 14);
                }
            }

            //check if the current node is in the grid
            if (CurrentNode.Position.y - 1 >= 0 && CurrentNode.Position.x + 1 < GridX)
            {
                Node BRNode = Grid[CurrentNode.Position.x + 1, CurrentNode.Position.y - 1];
                if (BRNode.ParentNode == null)
                {
                    NeighbourNodes.Add(BRNode);
                }
                else
                {
                    CompareGcost(CurrentNode, BRNode, 14);
                }
            }


            //check if the current node is in the grid
            if (CurrentNode.Position.y + 1 < GridY && CurrentNode.Position.x - 1 >= 0)
            {
                Node TRNode = Grid[CurrentNode.Position.x - 1, CurrentNode.Position.y + 1];
                if (TRNode.ParentNode == null)
                {
                    NeighbourNodes.Add(TRNode);
                }
                else
                {
                    CompareGcost(CurrentNode, TRNode, 14);
                }
            }

            //check if the current node is in the grid
            if (CurrentNode.Position.y + 1 < GridY && CurrentNode.Position.x - 1 < GridX)
            {
                Node TLNode = Grid[CurrentNode.Position.x + 1, CurrentNode.Position.y + 1];
                if (TLNode.ParentNode == null)
                {
                    NeighbourNodes.Add(TLNode);
                }
                else
                {
                    CompareGcost(CurrentNode, TLNode, 14);
                }
            }
            #endregion
            //return the list of NeighbourNodes
            return NeighbourNodes;
        }

        //Function to check if the old parent travel cost is more than the new parent travel cost 
        public bool CompareGcost(Node start, Node node2, int CostOfTravel)
        {
            //if yes then set the new Gcost and the parent node for the node
            if (node2.travelCost.GCost >= start.travelCost.GCost + CostOfTravel)
            {
                node2.travelCost.GCost = start.travelCost.GCost + CostOfTravel;
                node2.ParentNode = start;
            }

            //else return false being no change to be made to this node and not to be added as neighbour in the collection
            else
            {
                return false;
            }
            return true;
        }
    }


    public class Vector2
    {
        private int X;
        private int Y;

        public int x { get; set; }
        public int y { get; set; }

        public Vector2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }




}
