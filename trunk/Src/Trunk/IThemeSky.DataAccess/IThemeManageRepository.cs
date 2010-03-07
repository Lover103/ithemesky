using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IThemeSky.Model;

namespace IThemeSky.DataAccess
{
    public interface IThemeManageRepository
    {
        bool AddTheme(Theme theme);

        bool AddThemeDownloadUrl(int themeId, string url, bool isDefault, int source);

        bool MappingThemeTag(int themeId, string tagName);
    }
}
