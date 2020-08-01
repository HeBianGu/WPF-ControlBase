using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.MainWindow
{
    public class AssemblyDomain : IThemeLocalizeService,ILogService
    {
        public static AssemblyDomain Instance = new AssemblyDomain();

        LocalConfigService _localConfigService = new LocalConfigService();

        public ThemeLocalizeConfig LoadTheme()
        {
            return _localConfigService.LoadConfig<ThemeLocalizeConfig>(); 
        }

        public bool SaveTheme(ThemeLocalizeConfig theme)
        {
            return _localConfigService.SaveConfig(theme); 
        }

        public void Debug(params string[] messages)
        {
            foreach (var item in messages)
            {
               System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Error(params Exception[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Error(params string[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Fatal(params string[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Fatal(params Exception[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Info(params string[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Trace(params string[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Warn(params string[] messages)
        {
            foreach (var item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }
    }
}
