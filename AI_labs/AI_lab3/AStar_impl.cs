using AI_labs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_labs.Core
{
    public partial class Solver
    {
        /// <summary>
        /// Finds the path using A* heuristic search
        /// </summary>
        /// <param name="start">Initial coordinates</param>
        /// <param name="target">Target coordinates</param>
        /// <param name="Heuristic">Heuristic</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        public List<Node>? FindPathAStar((int x, int y) start, (int x, int y) target, Func<int, int, int, int, CubeOrientation, int> Heuristic, int nodesLimit = 0)
        {
            listsLengthMax = 1;
            listsLengthCurrent = 1;

            bool useSMA = nodesLimit > 0;

            PriorityQueue<Node, int>? oDefault = null;
            SortedSet<Node>? oLimited = null;
            var C = new HashSet<(int, int, CubeOrientation)>();

            if (useSMA)
                oLimited = new SortedSet<Node>(Comparer<Node>.Create((first, second) =>
                {
                    int compare = first.F.CompareTo(second.F);
                    if (compare == 0) compare = first.G.CompareTo(second.G);
                    if (compare == 0) compare = first.Orientation.CompareTo(second.Orientation);
                    if (compare == 0) compare = (first.X, first.Y).CompareTo((second.X, second.Y));
                    return compare;
                }));
            else 
            oDefault = new PriorityQueue<Node, int>();
            

            var startNode = new Node(start.x, start.y, CubeOrientation.RedDown)
            {
                G = 0,
                H = Heuristic(start.x, start.y, target.x, target.y, CubeOrientation.RedDown)
            };

            if (useSMA)
                oLimited.Add(startNode);
            else
                oDefault.Enqueue(startNode, startNode.F);

            while ((useSMA ? oLimited.Count : oDefault.Count) > 0)
            {
                var current = useSMA ? oLimited.Min : oDefault.Dequeue(); //x := first(O)
                if (useSMA) oLimited.Remove(current);
                listsLengthCurrent--;

                if ((current.X, current.Y) == target && current.Orientation == CubeOrientation.RedDown) //if this one is the target
                    return ReconstructPath(current);

                if (C.Contains((current.X, current.Y, current.Orientation)))    //if this one has been visited
                    continue;

                C.Add((current.X, current.Y, current.Orientation)); //x moves from O to C
                listsLengthCurrent++;

                foreach (var move in Moves) //P: disclosure of X
                {
                    P = true;
                    int nrow = current.X + move.drow;
                    int ncol = current.Y + move.dcol;

                    if (nrow >= 0 && ncol >= 0 && nrow < rows && ncol < cols &&
                        grid[nrow, ncol] != CellStates.Abyss &&
                        !C.Contains((nrow, ncol, Roll(current.Orientation, move))))
                    {
                        var nextOrientation = Roll(current.Orientation, move);
                        var nextNode = new Node(nrow, ncol, nextOrientation, current)
                        {
                            G = current.G + 1,
                            H = Heuristic(nrow, ncol, target.x, target.y, nextOrientation)
                        };

                        if (useSMA)
                            oLimited.Add(nextNode);
                        else
                            oDefault.Enqueue(nextNode, nextNode.F);

                        listsLengthCurrent++;
                    }
                }

                if (P == true) iterCount++;
                P = false;
                if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;

                while (useSMA && oLimited.Count > nodesLimit)
                {
                    var max = oLimited.Max;
                    oLimited.Remove(max);

                    if (max.Parent != null)
                        max.Parent.H = Math.Max(max.Parent.H, max.F);

                    //if (oLimited.Count >= nodesLimit)
                    //    return null;
                }
            }

            return null;
        }
    }
}
