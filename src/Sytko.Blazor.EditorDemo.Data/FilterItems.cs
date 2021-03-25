using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sytko.Blazor.EditorDemo.Data.Models;

namespace Sytko.Blazor.EditorDemo.Data
{
    public static class FilterItems
    {
        #region Gender
        public static FilterItem GenderFemale => new()
        {
            Key = "female",
            Title = "Female",
            IconInfo = "female"
        };

        public static FilterItem GenderMale => new()
        {
            Key = "male",
            Title = "Male",
            IconInfo = "male"
        };
        #endregion

        #region Color
        public static FilterItem ColorBlack => new()
        {
            Key = "black",
            Title = "Black",
            IconInfo = "#000"
        };

        public static FilterItem ColorWhite => new()
        {
            Key = "white",
            Title = "White",
            IconInfo = "#FFF"
        };

        public static FilterItem ColorBlue => new()
        {
            Key = "blue",
            Title = "Blue",
            IconInfo = "#637cf7"
        };
        public static FilterItem ColorGreen => new()
        {
            Key = "green",
            Title = "Green",
            IconInfo = "#63f76f"
        };
        public static FilterItem ColorYellow => new()
        {
            Key = "yellow",
            Title = "Yellow",
            IconInfo = "#f7f563"
        };
        public static FilterItem ColorOrange => new()
        {
            Key = "orange",
            Title = "Orange",
            IconInfo = "#f7c163"
        };
        public static FilterItem ColorRed => new()
        {
            Key = "red",
            Title = "Red",
            IconInfo = "#f76363"
        };

        public static FilterItem ColorPink => new()
        {
            Key = "pink",
            Title = "Pink",
            IconInfo = "#e6a5e8"
        };
        #endregion

        #region Category
        public static FilterItem CategoryTshirt => new()
        {
            Key = "tshirt",
            Title = "T-Shirt"
        };

        public static FilterItem CategorySweater => new()
        {
            Key = "sweater",
            Title = "Sweater"
        };

        public static FilterItem CategoryPants => new()
        {
            Key = "pants",
            Title = "Pants"
        };

        public static FilterItem CategoryDress => new()
        {
            Key = "dress",
            Title = "Dress"
        };

        public static FilterItem CategorySkirts => new()
        {
            Key = "skirts",
            Title = "Skirts"
        };

        public static FilterItem CategoryShoes => new()
        {
            Key = "shoes",
            Title = "Shoes"
        }; 
        #endregion
    }
}
