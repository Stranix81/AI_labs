using AI_labs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_labs.AI_lab3.Heuristics
{
    public class Heuristics
    {
        /// <summary>
        /// Calculates the Manhattan distance between two grid positions.
        /// This heuristic estimates the minimal number of moves required 
        /// to reach the target cell from the current position, assuming 
        /// movement is only allowed horizontally or vertically.
        /// 
        /// Note: The cube's red face orientation is ignored in this heuristic.
        /// </summary>
        /// <param name="x1">The X-coordinate of the current position.</param>
        /// <param name="y1">The Y-coordinate of the current position.</param>
        /// <param name="x2">The X-coordinate of the target position.</param>
        /// <param name="y2">The Y-coordinate of the target position.</param>
        /// <param name="orientation">The current cube orientation (unused in this heuristic).</param>
        /// <returns>The Manhattan distance between the two positions.</returns>
        public static int Manhattan(int x1, int y1, int x2, int y2, CubeOrientation orientation)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        /// <summary>
        /// Calculates the Manhattan distance between two grid positions, 
        /// adding an orientation-based penalty depending on the cube’s red face direction.
        /// 
        /// This heuristic estimates the minimal number of moves required 
        /// to reach the target cell and align the cube’s red face correctly.
        /// The penalty increases the estimated cost when the red face 
        /// is not already facing downward toward the target cell.
        /// </summary>
        /// <param name="x1">The X-coordinate of the current position.</param>
        /// <param name="y1">The Y-coordinate of the current position.</param>
        /// <param name="x2">The X-coordinate of the target position.</param>
        /// <param name="y2">The Y-coordinate of the target position.</param>
        /// <param name="orientation">The current cube orientation (used to compute the penalty).</param>
        /// <returns>
        /// The Manhattan distance between the two positions plus a penalty 
        /// based on the cube’s red face orientation.
        /// </returns>
        public static int ManhattanWithPenalty(int x1, int y1, int x2, int y2, CubeOrientation orientation)
        {
            int manhattanDistance = Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
            int penalty = 0;
            switch(orientation)
            {
                case CubeOrientation.RedUp:
                    {
                        penalty = 2;
                        break;
                    }
                case CubeOrientation.RedRight:
                    {
                        penalty = 1;
                        break;
                    }
                case CubeOrientation.RedLeft:
                    {
                        penalty = 1;
                        break;
                    }
                case CubeOrientation.RedFront:
                    {
                        penalty = 1;
                        break;
                    }
                case CubeOrientation.RedBack:
                    {
                        penalty = 1;
                        break;
                    }
                default:
                    {
                        penalty = 0;
                        break;
                    }
            }
            return manhattanDistance + penalty;
        }
    }
}
