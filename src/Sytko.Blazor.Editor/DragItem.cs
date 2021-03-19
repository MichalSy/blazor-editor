using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.Editor
{
    public class DragItem
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public string ImageUrl { get; set; }

        public string BackgroundColor { get; set; }
    }
}
