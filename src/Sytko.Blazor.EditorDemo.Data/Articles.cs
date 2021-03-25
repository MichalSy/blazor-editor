using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sytko.Blazor.EditorDemo.Data.Models;

namespace Sytko.Blazor.EditorDemo.Data
{
    public static class Articles
    {
        public static IEnumerable<ArticleInformation> AllArticles => new ArticleInformation[]
{
            new ArticleInformation
            {
                AvailableVariants = new ArticleInformation[]
                {
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_shirt_1/1027290_25833_7.png",
                        ColorHexCode = "#fbf07f",
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorYellow,
                            FilterItems.CategoryTshirt
                        }
                    },
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_shirt_1/1027290_10620_7.png",
                        ColorHexCode = "#e2bbb4",
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorPink,
                            FilterItems.CategoryTshirt
                        }
                    },
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_shirt_1/1027290_10343_7.png",
                        ColorHexCode = "#000",
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorBlack,
                            FilterItems.CategoryTshirt
                        }
                    }
                }
            },
            new ArticleInformation
            {
                AvailableVariants = new ArticleInformation[]
                {
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_shirt_2/1026223_27229_7.png",
                        ColorHexCode = "#000",
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorBlack,
                            FilterItems.CategoryTshirt
                        }
                    },
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_shirt_2/1026223_27231_7.png",
                        ColorHexCode = "#e2bbb4",
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorPink,
                            FilterItems.CategoryTshirt
                        }
                    }
                }
            }
        };
    }
}
