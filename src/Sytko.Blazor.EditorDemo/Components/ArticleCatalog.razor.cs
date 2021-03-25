using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.EditorDemo.Data.Models;

namespace Sytko.Blazor.EditorDemo.Components
{
    public partial class ArticleCatalog
    {
        [Parameter]
        public IEnumerable<ArticleInformation> CatalogItems { get; set; } = Array.Empty<ArticleInformation>();

        [Parameter]
        public EventCallback<ArticleInformation> OnDragStart { get; set; }
    }
}
