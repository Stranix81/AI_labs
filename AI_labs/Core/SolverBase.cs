using AI_labs.Core;
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
        private int rows = default;
        private int cols = default;
        private CellStates[,] grid;
        public int pCount = 0;
        public int listsLengthMax = 1;
        public int listsLengthCurrent = 1;
        public int oLengthMax = 1;
        public int cLengthMax = 0;

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
