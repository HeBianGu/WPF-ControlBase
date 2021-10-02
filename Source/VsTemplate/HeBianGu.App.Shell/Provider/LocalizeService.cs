using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.App.Shell
{
    class LocalizeService : IThemeLocalizeService
    {
        LocalConfigService _localConfigService = new LocalConfigService();

        public ThemeLocalizeConfig LoadTheme()
        {
            return _localConfigService.LoadConfig<ThemeLocalizeConfig>();
        }

        public bool SaveTheme(ThemeLocalizeConfig theme)
        {
            return _localConfigService.SaveConfig(theme);
        }

    }
}
