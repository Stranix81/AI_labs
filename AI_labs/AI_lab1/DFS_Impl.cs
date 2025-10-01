using AI_labs.Core.Enums;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_labs.Core
{
    public partial class Solver
    {
        /// <summary>
        /// Finds the path using depth-first search 
        /// </summary>
        /// <param name="start">Initial coordinates</param>
        /// <param name="target">Target coordinates</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        public List<Node>? FindPathDFS((int x, int y) start, (int x, int y) target)
        {
            listsLengthMax = 1;
            listsLengthCurrent = 1;
            iterCount = 0;
            var O = new Stack<Node>();
            var C = new HashSet<(int, int, CubeOrientation)>();

            O.Push(new Node(start.x, start.y, CubeOrientation.RedDown));

            while (O.Count > 0)
            {
                var current = O.Pop();
                listsLengthCurrent--;

                if ((current.X, current.Y) == target && current.Orientation == CubeOrientation.RedDown) //if this one is the target
                    return ReconstructPath(current);

                if (C.Contains((current.X, current.Y, current.Orientation))) continue;  //if this one has been visited

                C.Add((current.X, current.Y, current.Orientation));  //x moves from O to C
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
                        var nextOri = Roll(current.Orientation, move);
                        O.Push(new Node(nrow, ncol, nextOri, current));
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
