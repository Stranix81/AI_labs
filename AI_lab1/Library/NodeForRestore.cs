using AI_lab1.Library.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_lab1.Library
{
    public class NodeForRestore
    {
        public int X { get; }
        public int Y { get; }
        public Color Color { get; }
        public bool Abyss {  get; }
        public string ButtonText { get; }

        public NodeForRestore(int x, int y, string buttonText, Color color, bool isAbyss)
        {  
            X = x;
            Y = y;
            Color = color;
            Abyss = isAbyss;
            ButtonText = buttonText;
        }
    }
}
