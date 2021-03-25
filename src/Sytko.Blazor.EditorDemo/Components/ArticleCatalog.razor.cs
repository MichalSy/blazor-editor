using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.EditorDemo.Models;

namespace Sytko.Blazor.EditorDemo.Components
{
    public partial class ArticleCatalog
    {
        [Parameter]
        public IEnumerable<CatalogItem> CatalogItems { get; set; } = Array.Empty<CatalogItem>();

        [Parameter]
        public EventCallback<CatalogItem> OnDragStart { get; set; }

        public CatalogItem DragImage { get; set; }
    }
}
