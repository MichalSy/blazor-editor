using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.Editor.Common
{
    public struct Vector2Int
    {
        public int X;
        public int Y;

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2Int Zero => new Vector2Int(0, 0);
    }
}
