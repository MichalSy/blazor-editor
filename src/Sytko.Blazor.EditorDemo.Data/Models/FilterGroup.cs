using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.EditorDemo.Data.Models
{
    public class FilterGroup
    {
        public string GroupName { get; set; }
        public IEnumerable<FilterItem> FilterItems { get; set; } = Array.Empty<FilterItem>();
    }
}
