using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Factory
{
    public class PositionEventArgs: EventArgs
    {
        public PositionEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
    }
}
