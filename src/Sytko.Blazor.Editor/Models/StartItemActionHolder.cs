using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sytko.Blazor.Editor.Common;

namespace Sytko.Blazor.Editor.Models
{
    public class StartItemActionHolder
    {
        public Rectangle ItemRectangle { get; set; }
        public Vector2Int MatrixMousePosition { get; set; }
        public Vector2Int WorldMousePosition { get; set; }
    }
}
