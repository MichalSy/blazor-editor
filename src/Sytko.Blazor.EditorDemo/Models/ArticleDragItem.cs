using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.Editor;

namespace Sytko.Blazor.EditorDemo.Models
{
    public class ArticleDragItem : DragItem
    {
        public IEnumerable<ArticleInformation> AvailableArticles { get; set; } = Array.Empty<ArticleInformation>();
    }
}
