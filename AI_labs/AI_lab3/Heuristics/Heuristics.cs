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
        public static Dictionary<(int x, int y, CubeOrientation orientation), int>? patternDB = null;

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
        /// Calculates a Manhattan-based admissible heuristic for the rolling-cube puzzle.
        /// <para>
        /// Rules:
        /// <list type="bullet">
        /// <item>
        /// If the cube is 2+ steps away, the path to the target provides enough room
        /// to correct the orientation “for free”, so no penalty is added.
        /// </item>
        /// <item>
        /// If the cube is exactly 1 step away, a penalty is added only if rolling
        /// toward the target does NOT place the red face on the bottom.
        /// </item>
        /// <item>
        /// If the cube is already on the target, penalty is added only when the orientation is not the target.
        /// </item>
        /// </list>
        /// </para>
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

            //--- case 1: already on the target ---
            if (manhattanDistance == 0)
            {
                if (orientation == CubeOrientation.RedDown)
                    return 0;

                return 4; // need at least four moves to get red face down and get back to the target cell
            }

            //--- case 2: target is 2+ steps away ---
            // There is enough room to correct orientation naturally during movement.
            if (manhattanDistance >= 2)
                return manhattanDistance;

            //--- case 3: target is exactly 1 step away ---
            //only add penalty if moving toward the target does NOT bring red face down.
  
            // Determine the direction of the target relative to the cube.
            int moveX = Math.Sign(x2 - x1); // +1 right, -1 left, 0 none
            int moveY = Math.Sign(y2 - y1); // +1 down, -1 up, 0 none

            // Check if rolling in that direction puts red face down.
            bool redWillBeDown =
                (moveX == 1 && orientation == CubeOrientation.RedRight) ||
                (moveX == -1 && orientation == CubeOrientation.RedLeft) ||
                (moveY == 1 && orientation == CubeOrientation.RedFront) ||
                (moveY == -1 && orientation == CubeOrientation.RedBack);

            if (redWillBeDown)
                return 1;    // manhattan = 1, no penalty needed

            //otherwise, at least one additional move is needed before reaching the target
            return 1 + 1;
        }

        /// <summary>
        /// Calculates the maximum absolute difference between the corresponding coordinates of two points in space.
        /// </summary>
        /// <param name="x1">The X-coordinate of the current position.</param>
        /// <param name="y1">The Y-coordinate of the current position.</param>
        /// <param name="x2">The X-coordinate of the target position.</param>
        /// <param name="y2">The Y-coordinate of the target position.</param>
        /// <param name="orientation">The current cube orientation (unused in this heuristic).</param>
        /// <returns>The Chebyshev distance between the two positions</returns>
        public static int Chebyshev(int x1, int y1, int x2, int y2, CubeOrientation orientation)
        {
            return Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
        }

        /// <summary>
        /// Returns a value from the pre-generated pattern database
        /// </summary>
        /// <param name="x1">The X-coordinate of the current position.</param>
        /// <param name="y1">The Y-coordinate of the current position.</param>
        /// <param name="x2">The X-coordinate of the target position (unused in this heuristic).</param>
        /// <param name="y2">The Y-coordinate of the target position (unused in this heuristic).</param>
        /// <param name="orientation">The current cube orientation (unused in this heuristic).</param>
        /// <returns>A value from the previously generated pattern database</returns>
        public static int PatternDatabases(int x1, int y1, int x2, int y2, CubeOrientation orientation)
        {
            if (patternDB is null)
                throw new Exception("Не инициализирована база данных с шаблонами");
            return patternDB[(x1, y1, orientation)];
        }
    }
}
