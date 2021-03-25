using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sytko.Blazor.EditorDemo.Data;
using Sytko.Blazor.EditorDemo.Data.Models;

namespace Sytko.Blazor.EditorDemo.Components
{
    public partial class ArticleCatalog
    {
        [Parameter]
        public IEnumerable<ArticleInformation> CatalogItems { get; set; } = Array.Empty<ArticleInformation>();

        [Parameter]
        public EventCallback<ArticleInformation> OnDragStart { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        public string ImageBasePath => Configuration["DefaultImageBasePath"];

        protected override void OnInitialized()
        {
            var catalogItems = new List<ArticleInformation>();

            foreach (var currentArticle in Articles.AllArticles)
            {
                foreach (var currentVariant in currentArticle.AvailableVariants)
                {
                    currentVariant.AvailableVariants = currentArticle.AvailableVariants;
                    catalogItems.Add(currentVariant);
                }
            }

            CatalogItems = catalogItems;

        }
    }
}
