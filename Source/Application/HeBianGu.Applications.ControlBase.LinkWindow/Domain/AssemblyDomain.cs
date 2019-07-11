using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    class AssemblyDomain
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

        public List<UpLoadItem> GetGridList(out string errorInfor, out uint total, string startTime = "", string endTime = "", string name = "", int type = 0, uint page = 0, uint size = 10)
        {
            List<UpLoadItem> result = new List<UpLoadItem>();

            uint index = page - 1;

            for (uint i = index * size; i < index * size + size; i++)
            {

                UpLoadItem upLoadItem = new UpLoadItem();
                upLoadItem.Index = (i + 1).ToString();
                upLoadItem.Name = $"《权利的游戏 第{(int)(i / 10) + 1 }季》 第{i + 1}集";
                upLoadItem.Path = "D:/Movie/2019-06-25 10:11:23/" + upLoadItem.Name + ".mp4";
                upLoadItem.Type = ".mp4";
                upLoadItem.Tag = "欧美,中文字幕,高清";
                upLoadItem.Time = DateTime.Now.ToString("yyyy-MM-dd");
                upLoadItem.Span = "01:25;12";
                upLoadItem.Size = "1.2G";
                result.Add(upLoadItem);
            }

            total = 100;

            errorInfor = string.Empty;

            if (r.Next(5) == 1)
            {
                errorInfor = "运气不佳，请再试一次";
                return null;
            
            }
            else
            {
                return result;
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

    }
}
