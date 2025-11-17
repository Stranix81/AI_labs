using AI_labs.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_labs.Core
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        public Node? Parent { get; }
        public CubeOrientation Orientation { get; }
        public int Depth { get; }
        public bool IsMeetingPoint { get; set; }

        // Cost from start to current node
        public int G { get; set; }

        // Heuristic cost estimate to goal
        public int H { get; set; }

        // Inheritants' worst (greatest) F
        public int BackupF { get; set; } = int.MinValue;

        // Total cost (priority)
        public int F => Math.Max(G + H, BackupF);

        public Node(int x, int y, CubeOrientation orientation, Node? parent = null, int depth = 0, bool isMeetingPoint = false)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Parent = parent;
            Depth = depth;
            IsMeetingPoint = isMeetingPoint;
        }
    }
}
