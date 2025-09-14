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
            var O = new Queue<Node>();            
            var C = new HashSet<(int, int, CubeOrientation)>();

            O.Enqueue(new Node(start.x, start.y, CubeOrientation.RedDown));

            while (O.Count > 0)
            {
                var current = O.Dequeue();  //x := first(O)

                if ((current.X, current.Y) == target && current.Orientation == CubeOrientation.RedDown) //if this one is the target
                    return ReconstructPath(current);

                if (C.Contains((current.X, current.Y, current.Orientation)))    //if this one has been visited
                    continue;

                C.Add((current.X, current.Y, current.Orientation)); //x moves from O to C

                foreach (var move in Moves) //P: disclosure of X
                {
                    int nrow = current.X + move.drow;
                    int ncol = current.Y + move.dcol;

                    if (nrow >= 0 && ncol >= 0 && nrow < rows && ncol < cols &&
                        grid[nrow, ncol] != CellStates.Abyss &&
                        !C.Contains((nrow, ncol, Roll(current.Orientation, move))))
                    {
                        var nextOrientation = Roll(current.Orientation, move);
                        O.Enqueue(new Node(nrow, ncol, nextOrientation, current));
                    }
                }
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
            var O = new Stack<Node>();
            var C = new HashSet<(int, int, CubeOrientation)>();

            O.Push(new Node(start.x, start.y, CubeOrientation.RedDown));

            while (O.Count > 0)
            {
                var current = O.Pop();

                if ((current.X, current.Y) == target && current.Orientation == CubeOrientation.RedDown) //if this one is the target
                    return ReconstructPath(current);

                if (C.Contains((current.X, current.Y, current.Orientation))) continue;  //if this one has been visited

                C.Add((current.X, current.Y, current.Orientation));  //x moves from O to C

                foreach (var move in Moves) //P: disclosure of X
                {
                    int nrow = current.X + move.drow;
                    int ncol = current.Y + move.dcol;

                    if (nrow >= 0 && ncol >= 0 && nrow < rows && ncol < cols &&
                        grid[nrow, ncol] != CellStates.Abyss &&
                        !C.Contains((nrow, ncol, Roll(current.Orientation, move))))
                    {
                        var nextOri = Roll(current.Orientation, move);
                        O.Push(new Node(nrow, ncol, nextOri, current));
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
    }
}
