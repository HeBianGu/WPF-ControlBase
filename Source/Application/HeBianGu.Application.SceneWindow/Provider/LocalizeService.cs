using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using HeBianGu.Control.ThemeSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.SceneWindow
{
    class LocalizeService : IThemeSerializeService
    {
        LocalConfigService _localConfigService = new LocalConfigService();

        public ThemeConfig LoadTheme()
        {
            return _localConfigService.LoadConfig<ThemeConfig>();
        }

        public bool SaveTheme(ThemeConfig theme)
        {
            return _localConfigService.SaveConfig(theme);
        }

    }
}
