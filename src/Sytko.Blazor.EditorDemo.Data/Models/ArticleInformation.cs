using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytko.Blazor.EditorDemo.Data.Models
{
    public class ArticleInformation
    {
        public string ColorHexCode { get; set; }
        public string ImageUrl { get; set; }

        public Size Size { get; set; }

        private ArticleInformation _currentVariant;
        public ArticleInformation CurrentVariant
        {
            get
            {
                if (_currentVariant == null)
                {
                    _currentVariant = AvailableVariants.FirstOrDefault();
                }
                return _currentVariant;
            }
            set => _currentVariant = value;
        }

        public IEnumerable<FilterItem> FilterOptions { get; set; } = Array.Empty<FilterItem>();

        public IEnumerable<ArticleInformation> AvailableVariants { get; set; } = Array.Empty<ArticleInformation>();
    }
}
