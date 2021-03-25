using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sytko.Blazor.EditorDemo.Data.Models;

namespace Sytko.Blazor.EditorDemo.Data
{
    public static class FilterGroups
    {
        public static IEnumerable<FilterGroup> AllGroups => new FilterGroup[]
        {
            GenderGroup,
            ColorGroup,
            CategoryGroup
        };

        public static FilterGroup GenderGroup => new()
        {
            GroupName = "gender",
            FilterItems = new FilterItem[]
            {
                FilterItems.GenderFemale,
                FilterItems.GenderMale
            }
        };

        public static FilterGroup ColorGroup => new()
        {
            GroupName = "color",
            FilterItems = new FilterItem[]
            {
                FilterItems.ColorBlack,
                FilterItems.ColorWhite,
                FilterItems.ColorBlue,
                FilterItems.ColorGreen,
                FilterItems.ColorYellow,
                FilterItems.ColorOrange,
                FilterItems.ColorRed,
                FilterItems.ColorPink
            }
        };

        public static FilterGroup CategoryGroup => new()
        {
            GroupName = "category",
            FilterItems = new FilterItem[]
            {
                FilterItems.CategoryTshirt,
                FilterItems.CategorySweater,
                FilterItems.CategoryPants,
                FilterItems.CategoryDress,
                FilterItems.CategorySkirts,
                FilterItems.CategoryShoes
            }
        };
    }
}
