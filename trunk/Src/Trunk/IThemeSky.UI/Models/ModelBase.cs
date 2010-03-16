using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IThemeSky.Model;

namespace IThemeSky.UI.Models
{
    public abstract class ModelBase
    {
        public List<ThemeCategory> ThemeCategories
        {
            get
            {
                return new List<ThemeCategory>();
            }
        }
    }
}
