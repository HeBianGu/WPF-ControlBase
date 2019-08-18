using HeBianGu.General.WpfControlLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
   public class AssemblyDomain
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
            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");

            string txt = System.IO.File.ReadAllText(url, Encoding.Default);

            return JsonConvert.DeserializeObject<List<TreeNodeEntity>>(txt);
        }
    }
}
