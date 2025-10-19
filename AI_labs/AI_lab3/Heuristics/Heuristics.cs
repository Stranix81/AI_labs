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
        public static int Manhattan(int x1, int y1, int x2, int y2, CubeOrientation orientation)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

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
