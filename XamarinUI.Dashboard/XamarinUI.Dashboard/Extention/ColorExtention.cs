using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using XamarinUI.Dashboard.Models;

namespace XamarinUI.Dashboard.Extention
{
    public static class ColorExtention
    {
        /// <summary>
        /// From https://flatuicolors.com/palette/defo
        /// </summary>
        public static List<string> HexFlatColors
        {
            get
            {
                return new List<string>
                {
                    "#16a085",
                    "#27ae60",
                    "#2980b9",
                    "#8e44ad",
                    "#2c3e50",
                    "#f39c12",
                    "#d35400",
                    "#c0392b",
                };
            }
        }

        public static string GetARandomHexFlatColor()
        {
            return HexFlatColors.PickRandom();
        }

        public static List<MenuBody> SetColorInMenuList(this List<MenuBody> source)
        {
            var count = 0;
            var lastColor = HexFlatColors.LastOrDefault();

            foreach (var item in source)
            {
                item.BackgroundColor = Color.FromHex(HexFlatColors[count]);

                if (HexFlatColors[count].Equals(lastColor))
                {
                    item.BackgroundColor = Color.FromHex(lastColor);
                    count = 0;
                }

                count++;
            }

            return source;
        }
    }
}
