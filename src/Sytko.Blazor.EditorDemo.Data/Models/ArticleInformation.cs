using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.EditorDemo.Data.Models
{
    public class ArticleInformation
    {
        public string ColorHexCode { get; set; }
        public string ImageUrl { get; set; }

        public IEnumerable<ArticleInformation> AvailableVariants { get; set; } = Array.Empty<ArticleInformation>();
    }
}
