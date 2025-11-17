using AI_labs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            oLengthMax = 1;
            cLengthMax = 0;

            bool useSMA = nodesLimit > 0;

            var O = new List<Node>();
            var C = new HashSet<(int, int, CubeOrientation)>();

            var startNode = new Node(start.x, start.y, CubeOrientation.RedDown)
            {
                G = 0,
                H = Heuristic(start.x, start.y, target.x, target.y, CubeOrientation.RedDown)
            };
            O.Add(startNode);

            while (O.Count > 0)
            {
                O.Sort((a, b) =>
                {
                    int cmp = a.F.CompareTo(b.F);
                    if (cmp == 0) cmp = a.G.CompareTo(b.G);
                    if (cmp == 0) cmp = (Math.Abs(target.x - a.X), Math.Abs(target.y - a.Y)).CompareTo((Math.Abs(target.x - b.X), Math.Abs(target.y - b.Y)));
                    return cmp;
                });
                var current = O[0];
                O.RemoveAt(0);

                if ((current.X, current.Y) == target && current.Orientation == CubeOrientation.RedDown) //if this one is the target
                    return ReconstructPath(current);

                if (C.Contains((current.X, current.Y, current.Orientation)))    //if this one has been visited
                    continue;

                C.Add((current.X, current.Y, current.Orientation)); //X moves from O to C
                cLengthMax = Math.Max(cLengthMax, C.Count); 

                foreach (var move in Moves) //P: disclosure of X
                {
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

                        O.Add(nextNode);

                        if (useSMA && O.Count > nodesLimit)
                        {
                            O.Sort((a, b) =>
                            {
                                int cmp = b.F.CompareTo(a.F);
                                if (cmp == 0) cmp = b.G.CompareTo(a.G);
                                if (cmp == 0) cmp = (Math.Abs(target.x - b.X), Math.Abs(target.y - b.Y)).CompareTo((Math.Abs(target.x - a.X), Math.Abs(target.y - a.Y)));
                                return cmp;
                            });
                            var worst = O[0];
                            O.RemoveAt(0);

                            PropagateBackup(worst, O);
                        }

                        listsLengthCurrent = O.Count + C.Count;
                        oLengthMax = Math.Max(oLengthMax, O.Count);
                        listsLengthMax = Math.Max(listsLengthMax, listsLengthCurrent);
                    }
                }
                pCount++;
            }
            return null;
        }

        /// <summary>
        /// Propagates the updated BackupF value of a removed node up the search tree.
        /// </summary>
        /// <param name="removedNode">The node that has been removed from the open list and whose F value should be propagated.</param>
        /// <param name="O">The current open list containing nodes that are still eligible for expansion.</param>
        private void PropagateBackup(Node removedNode, List<Node> O)
        {
            Node node = removedNode;    //the removed one
            while (node.Parent != null) 
            {
                var parent = node.Parent;   //the removed one's parent

                int oldF = parent.F;    //the removed one's parent's F
                parent.BackupF = Math.Max(parent.BackupF, node.F);  //the parent must know the max F of its inheritants; it's stored in BackupF

                if (!O.Contains(parent)) break; //if the parent is not in the open collection, there's no need to deal with it

                if (parent.F == oldF)   //if the parent's F hasn't changed after the propagation, the F of all its ancestors will not change either
                    break;

                node = parent;  //now all of the above must be done with the parent and so on..
            }
        }
    }
}
