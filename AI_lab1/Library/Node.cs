using AI_lab1.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_lab1.Library
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        public Node? Parent { get; }
        public CubeOrientation Orientation { get; }

        public Node(int x, int y, CubeOrientation orientation, Node? parent = null)
        {
            X = x;
            Y = y;
            Orientation = orientation;
            Parent = parent;
        }
    }
}
