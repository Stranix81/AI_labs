using AI_lab1.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_lab1.Library
{
    public class Solver
    {
        private int rows = default;
        private int cols = default;
        private CellStates[,] grid;
        public int iterCount = 0;
        private bool P = false;
        public int listsLengthMax = 1;
        public int listsLengthCurrent = 1;

        public Solver(CellStates[,] gridStates)
        {
            grid = gridStates;
            rows = gridStates.GetLength(0);
            cols = gridStates.GetLength(1);

        }

        /// <summary>
        /// all the possible moves
        /// </summary>
        private static readonly (int drow, int dcol)[] Moves =
        {
            (-1, 0),    //up
            (1, 0), //down
            (0, -1),    //left
            (0, 1)  //right
        };

        /// <summary>
        /// Finds the path using breadth-first search
        /// </summary>
        /// <param name="start">Initial coordinates</param>
        /// <param name="target">Target coordinates</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        public List<Node>? FindPathBFS((int x, int y) start, (int x, int y) target)
        {
            listsLengthMax = 1;
            listsLengthCurrent = 1;
            var O = new Queue<Node>();            
            var C = new HashSet<(int, int, CubeOrientation)>();

            O.Enqueue(new Node(start.x, start.y, CubeOrientation.RedDown));

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
                        O.Enqueue(new Node(nrow, ncol, nextOrientation, current));
                        listsLengthCurrent++;
                    }
                }
                if (P == true) iterCount++;
                P = false;
                if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
            }
            return null;
        }

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
                if(P == true) iterCount++;
                P = false;
                if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
            }

            return null;
        }

        /// <summary>
        /// Finds the path using depth-first search with L+1 limit
        /// </summary>
        /// <param name="start">Initial coordinates</param>
        /// <param name="target">Target coordinates</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        public List<Node>? FindPathIDS((int x, int y) start, (int x, int y) target, int maxL)
        {
            listsLengthMax = 1;
            listsLengthCurrent = 1;
            iterCount = 0;

            for (int L = 0; L <= maxL; L++)
            {
                var O = new Stack<Node>();
                var C = new HashSet<(int, int, CubeOrientation)>();

                O.Push(new Node(start.x, start.y, CubeOrientation.RedDown, null, 0));
                listsLengthCurrent = 1;
                if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;

                while (O.Count > 0)
                {
                    var current = O.Pop();
                    listsLengthCurrent--;

                    if ((current.X, current.Y) == target && current.Orientation == CubeOrientation.RedDown)
                        return ReconstructPath(current);

                    if (current.Depth == L)
                    {
                        continue;
                    }

                    if (C.Contains((current.X, current.Y, current.Orientation))) continue;

                    C.Add((current.X, current.Y, current.Orientation));
                    listsLengthCurrent++;

                    foreach (var move in Moves)
                    {
                        P = true;
                        int nrow = current.X + move.drow;
                        int ncol = current.Y + move.dcol;

                        if (nrow >= 0 && ncol >= 0 && nrow < rows && ncol < cols &&
                            grid[nrow, ncol] != CellStates.Abyss &&
                            !C.Contains((nrow, ncol, Roll(current.Orientation, move))))
                        {
                            var nextOri = Roll(current.Orientation, move);
                            O.Push(new Node(nrow, ncol, nextOri, current, current.Depth + 1));
                            listsLengthCurrent++;
                        }
                    }

                    if (P == true) iterCount++;
                    P = false;
                    if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                }
            }

            return null;
        }

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

                        iterCount++;
                        if (C_target.ContainsKey(state))
                        {
                            if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                            C_target[state].IsMeetingPoint = true;
                            return SplitAndReconstructPath(nextNode, C_target[state]);
                        }
                    }
                }

                levelCount = O_target.Count;
                for (int i = 0; i < levelCount; i++)
                {
                    var current = O_target.Dequeue();
                    listsLengthCurrent--;

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
                        listsLengthCurrent += 2;
                        
                        iterCount++;
                        if (C_start.ContainsKey(state))
                        {
                            if (listsLengthCurrent > listsLengthMax) listsLengthMax = listsLengthCurrent;
                            nextNode.IsMeetingPoint = true;
                            return SplitAndReconstructPath(C_start[state], nextNode);
                        }
                    }                   
                }
            }

            return null;
        }

        /// <summary>
        /// Calculates the cube's CubeOrientation after movement
        /// </summary>
        /// <param name="CubeOrientation">Cube's original CubeOrientation</param>
        /// <param name="move">Cube's movement direction</param>
        /// <returns><see cref="CubeOrientation"/> cube's CubeOrientation after movement</returns>
        private CubeOrientation Roll(CubeOrientation CubeOrientation, (int drow, int dcol) move)
        {
            return move switch
            {
                (-1, 0) => //up
                    CubeOrientation switch
                    {
                        CubeOrientation.RedDown => CubeOrientation.RedBack,
                        CubeOrientation.RedUp => CubeOrientation.RedFront,
                        CubeOrientation.RedFront => CubeOrientation.RedDown,
                        CubeOrientation.RedBack => CubeOrientation.RedUp,
                        _ => CubeOrientation
                    },
                (1, 0) => //down
                    CubeOrientation switch
                    {
                        CubeOrientation.RedDown => CubeOrientation.RedFront,
                        CubeOrientation.RedUp => CubeOrientation.RedBack,
                        CubeOrientation.RedFront => CubeOrientation.RedUp,
                        CubeOrientation.RedBack => CubeOrientation.RedDown,
                        _ => CubeOrientation
                    },
                (0, -1) => //left
                    CubeOrientation switch
                    {
                        CubeOrientation.RedDown => CubeOrientation.RedRight,
                        CubeOrientation.RedUp => CubeOrientation.RedLeft,
                        CubeOrientation.RedLeft => CubeOrientation.RedDown,
                        CubeOrientation.RedRight => CubeOrientation.RedUp,
                        _ => CubeOrientation
                    },
                (0, 1) => //right
                    CubeOrientation switch
                    {
                        CubeOrientation.RedDown => CubeOrientation.RedLeft,
                        CubeOrientation.RedUp => CubeOrientation.RedRight,
                        CubeOrientation.RedLeft => CubeOrientation.RedUp,
                        CubeOrientation.RedRight => CubeOrientation.RedDown,
                        _ => CubeOrientation
                    },
                _ => CubeOrientation
            };
        }

        /// <summary>
        /// Reconstructs the whole path from the parent refs
        /// </summary>
        /// <param name="node">The last node</param>
        /// <returns> The path in the type of
        /// <see cref="List{Node}"/>, 
        /// where <typeparamref name="T"/> - <see cref="Node"/>.</returns>
        private List<Node> ReconstructPath(Node node)
        {
            var path = new List<Node>();
            while (node != null)
            {
                path.Add(node);
                node = node.Parent!;
            }
            path.Reverse();
            return path;
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
