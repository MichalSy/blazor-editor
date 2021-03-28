using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.Editor.Common
{
    public enum EditorActionTypes
    {
        None,
        DragWorld,
        MoveWorld,
        DragItem,
        MoveItem,
        ResizeItem,
        Zoom,
    }
}
