using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinUI.Dashboard.Extention;

namespace XamarinUI.Dashboard.Models
{
    public class MenuBody : MenuItem
    {
        public MenuBody()
        {
            Child = new List<MenuBody>();
        }

        public Color BackgroundColor { get; set; }

        //public string Title { get; set; }

        public string Icon { get; set; }

        public List<MenuBody> Child { get; private set; }

        public MenuBody AddChild(List<MenuBody> childrens)
        {
            childrens.SetColorInMenuList();

            var back = new MenuBody
            {
                Text = "Back",
                Icon = IconFont.AngleLeft,
                BackgroundColor = Color.FromHex("#bdc3c7")
            };

            childrens.Insert(0, back);

            this.Child = childrens;

            return this;
        }
    }
}
