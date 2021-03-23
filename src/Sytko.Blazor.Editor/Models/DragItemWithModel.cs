using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.Editor.Models
{
    public class DragItemWithModel<T> : DragItem
        where T : class
    {
        public T DataModel { get; set; }
    }
}
