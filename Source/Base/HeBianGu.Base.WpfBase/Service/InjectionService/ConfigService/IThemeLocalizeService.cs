using System;

namespace HeBianGu.Base.WpfBase
{
    public interface IThemeLocalizeService
    {
        ThemeLocalizeConfig LoadTheme();

        bool SaveTheme(ThemeLocalizeConfig theme);
    }
}