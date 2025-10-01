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
        /// <summary>
        /// Finds the path using bidirectional search
        /// </summary>
        /// <param name="start">Initial coordinates</param>
        /// <param name="target">Target coordinates</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        public List<Node>? FindPathBiBFS((int x, int y) start, (int x, int y) target)
        {
            listsLengthMax = 4;
            listsLengthCurrent = 4;
            iterCount = 0;
            var O_start = new Queue<Node>();
            var C_start = new Dictionary<(int, int, CubeOrientation), Node>();

            var O_target = new Queue<Node>();
            var C_target = new Dictionary<(int, int, CubeOrientation), Node>();

            var startNode = new Node(start.x, start.y, CubeOrientation.RedDown, null, 0);
            var targetNode = new Node(target.x, target.y, CubeOrientation.RedDown, null, 0);

            O_start.Enqueue(startNode);
            C_start[(start.x, start.y, startNode.Orientation)] = startNode;

            O_target.Enqueue(targetNode);
            C_target[(target.x, target.y, targetNode.Orientation)] = targetNode;

            while (O_start.Count > 0 && O_target.Count > 0)
            {
                int levelCount = O_start.Count;
                for (int i = 0; i < levelCount; i++)
                {
                    var current = O_start.Dequeue();
                    listsLengthCurrent--;

                    foreach (var move in Moves)
                    {
                        P = true;

                        int nx = current.X + move.drow;
                        int ny = current.Y + move.dcol;
                        var nextOrientation = Roll(current.Orientation, move);

                        if (nx < 0 || ny < 0 || nx >= rows || ny >= cols) continue;
                        if (grid[nx, ny] == CellStates.Abyss) continue;

                        var state = (nx, ny, nextOrientation);
                        if (C_start.ContainsKey(state)) continue;

                        var nextNode = new Node(nx, ny, nextOrientation, current, current.Depth + 1);
                        C_start[state] = nextNode;
                        O_start.Enqueue(nextNode);
                        listsLengthCurrent += 2;

                        if (C_target.ContainsKey(state))
                        {
                            if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                            C_target[state].IsMeetingPoint = true;
                            return SplitAndReconstructPath(nextNode, C_target[state]);
                        }
                    }
                    if (P == true) iterCount++;
                    P = false;
                    if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                }

                levelCount = O_target.Count;
                for (int i = 0; i < levelCount; i++)
                {
                    var current = O_target.Dequeue();
                    listsLengthCurrent--;

                    foreach (var move in Moves)
                    {
                        P = true;

                        int nx = current.X + move.drow;
                        int ny = current.Y + move.dcol;
                        var nextOrientation = Roll(current.Orientation, move);

                        if (nx < 0 || ny < 0 || nx >= rows || ny >= cols) continue;
                        if (grid[nx, ny] == CellStates.Abyss) continue;

                        var state = (nx, ny, nextOrientation);
                        if (C_target.ContainsKey(state)) continue;

                        var nextNode = new Node(nx, ny, nextOrientation, current, current.Depth + 1);
                        C_target[state] = nextNode;
                        O_target.Enqueue(nextNode);
                        listsLengthCurrent += 2;

                        if (C_start.ContainsKey(state))
                        {
                            if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                            nextNode.IsMeetingPoint = true;
                            return SplitAndReconstructPath(C_start[state], nextNode);
                        }
                    }
                    if (P == true) iterCount++;
                    P = false;
                    if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                }
            }

            return null;
        }
    }
}
