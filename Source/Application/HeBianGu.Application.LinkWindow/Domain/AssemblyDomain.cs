using HeBianGu.Base.WpfBase;
using HeBianGu.Common.LocalConfig;
using HeBianGu.Control.ThemeSet;
using HeBianGu.General.WpfControlLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.LinkWindow
{
    public class AssemblyDomain : IThemeSerializeService,ILogService
    {
        public static AssemblyDomain Instance = new AssemblyDomain();

        internal LoginViewModel GetAccountConfig()
        {
            LoginViewModel loginViewModel = new LoginViewModel();

            loginViewModel.LoginUseName = "HeBianGu";

            loginViewModel.LoginPassWord = "89757";

            loginViewModel.Remenber = true;

            return loginViewModel;
        }

        Random r = new Random();
        internal bool Login(string userName, string psd, bool IsSavePSD, out string err)
        {
            err = string.Empty;

            if (r.Next(5) == 1)
            {
                err = "运气不佳，请再来一次";
                return false;
            }
            else
            {
                return true;
            }


        }


        SettingViewModel _setting;
        public bool SaveSetting(SettingViewModel setting, out string err)
        {
            err = string.Empty;

            if (r.Next(2) == 1)
            {
                _setting = setting;

                return true;
            }
            else
            {

                err = "保存配置错误，请检查！";

                return false;
            }
        }

        public SettingViewModel GetSetting(out string err)
        {
            err = string.Empty;

            if (_setting == null)
            {
                err = "不存在保存的配置，请先设置配置文件";
            }

            return _setting;
        }

        public List<TreeNodeEntity> GetTreeListData()
        {
            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "data.json");

            string txt = System.IO.File.ReadAllText(url, Encoding.UTF8);

            return JsonConvert.DeserializeObject<List<TreeNodeEntity>>(txt);
        }


        LocalConfigService _localConfigService = new LocalConfigService();

        public ThemeConfig LoadTheme()
        {
            return _localConfigService.LoadConfig<ThemeConfig>(); 
        }

        public bool SaveTheme(ThemeConfig theme)
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
