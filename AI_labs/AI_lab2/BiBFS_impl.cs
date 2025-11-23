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
        /// Finds the path using bidirectional breadth-first search
        /// </summary>
        /// <param name="start">Initial coordinates</param>
        /// <param name="target">Target coordinates</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        public List<Node>? FindPathBiBFS((int x, int y) start, (int x, int y) target)
        {
            listsLengthMax = 2;
            listsLengthCurrent = 2;
            pCount = 0;
            genNodesCount = 0;
            oLengthMax = 2;
            cLengthMax = 0;

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

                    foreach (var move in Moves)
                    {
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
                        genNodesCount++;
                        listsLengthCurrent = O_start.Count + C_start.Count + O_target.Count + C_target.Count;
                        oLengthMax = Math.Max(oLengthMax, O_start.Count + O_target.Count);
                        cLengthMax = Math.Max(cLengthMax, C_start.Count + C_target.Count);
                        listsLengthMax = Math.Max(listsLengthMax, listsLengthCurrent);

                        if (C_target.ContainsKey(state))
                        {
                            C_target[state].IsMeetingPoint = true;
                            return SplitAndReconstructPath(nextNode, C_target[state]);
                        }
                    }
                    pCount++;
                }

                levelCount = O_target.Count;
                for (int i = 0; i < levelCount; i++)
                {
                    var current = O_target.Dequeue();

                    foreach (var move in Moves)
                    {
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
                        genNodesCount++;
                        listsLengthCurrent = O_start.Count + C_start.Count + O_target.Count + C_target.Count;
                        oLengthMax = Math.Max(oLengthMax, O_start.Count + O_target.Count);
                        cLengthMax = Math.Max(cLengthMax, C_start.Count + C_target.Count);
                        listsLengthMax = Math.Max(listsLengthMax, listsLengthCurrent);

                        if (C_start.ContainsKey(state))
                        {
                            nextNode.IsMeetingPoint = true;
                            return SplitAndReconstructPath(C_start[state], nextNode);
                        }
                    }
                    pCount++;
                }
            }
            return null;
        }

        /// <summary>
        /// Splits and reconstructs the whole path from the parent refs (for Bi-BFS)
        /// </summary>
        /// <param name="node_start"></param>
        /// <param name="node_target"></param>
        /// <returns></returns>
        private List<Node> SplitAndReconstructPath(Node node_start, Node node_target)
        {
            var path = ReconstructPath(node_start);
            path.RemoveAt(path.Count - 1);
            while (node_target != null)
            {
                path.Add(node_target);
                node_target = node_target.Parent!;
            }
            return path;
        }
    }
}
