using System;
using System.Collections.Generic;
using System.Drawing;
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
                        Size = new Size
                        {
                            Width = 265,
                            Height = 316
                        },
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
                        Size = new Size
                        {
                            Width = 265,
                            Height = 316
                        },
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
                        Size = new Size
                        {
                            Width = 265,
                            Height = 316
                        },
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
                        Size = new Size
                        {
                            Width = 265,
                            Height = 316
                        },
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
                        Size = new Size
                        {
                            Width = 265,
                            Height = 316
                        },
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorPink,
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
                        ImageUrl = "tt/w_pants_1/1024970_10668_7.png",
                        ColorHexCode = "#212836",
                        Size = new Size
                        {
                            Width = 199,
                            Height = 470
                        },
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorBlue,
                            FilterItems.CategoryPants
                        }
                    },
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_pants_1/1024970_13178_7.png",
                        ColorHexCode = "#669297",
                        Size = new Size
                        {
                            Width = 199,
                            Height = 470
                        },
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorBlue,
                            FilterItems.CategoryPants
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
                        ImageUrl = "tt/w_pants_2/1020631_10668_7.png",
                        ColorHexCode = "#30343e",
                        Size = new Size
                        {
                            Width = 169,
                            Height = 440
                        },
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorBlue,
                            FilterItems.CategoryPants
                        }
                    },
                    new ArticleInformation
                    {
                        ImageUrl = "tt/w_pants_2/1020631_23808_7.png",
                        ColorHexCode = "#b77363",
                        Size = new Size
                        {
                            Width = 169,
                            Height = 440
                        },
                        FilterOptions = new FilterItem[]
                        {
                            FilterItems.GenderFemale,
                            FilterItems.ColorRed,
                            FilterItems.CategoryPants
                        }
                    }
                }
            }
        };
    }
}
