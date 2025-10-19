using AI_labs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_labs.Core
{
    public partial class Solver
    {
        public List<Node>? FindPathAStar((int x, int y) start, (int x, int y) target, Func<int, int, int, int, CubeOrientation, int> Heuristic)
        {
            listsLengthMax = 1;
            listsLengthCurrent = 1;

            PriorityQueue<Node, int>? O = null;
            var C = new HashSet<(int, int, CubeOrientation)>();

            O = new PriorityQueue<Node, int>();

            var startNode = new Node(start.x, start.y, CubeOrientation.RedDown)
            {
                G = 0,
                H = Heuristic(start.x, start.y, target.x, target.y, CubeOrientation.RedDown)
            };

            O.Enqueue(startNode, startNode.F);

            while (O.Count > 0)
            {
                var current = O.Dequeue();  //x := first(O)
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
                        O.Enqueue(nextNode, nextNode.F);
                        listsLengthCurrent++;
                    }
                }
                if (P == true) iterCount++;
                P = false;
                if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
            }
            return null;
        }
    }
}
