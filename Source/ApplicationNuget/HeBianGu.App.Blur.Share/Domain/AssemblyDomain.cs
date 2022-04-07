using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeBianGu.App.Blur
{
    public class AssemblyDomain : ILogService
    {
        public static AssemblyDomain Instance = new AssemblyDomain();

        internal LoginViewModel GetAccountConfig()
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {
                LoginUseName = "HeBianGu",

                LoginPassWord = "89757",

                Remenber = true
            };

            return loginViewModel;
        }

        private Random r = new Random();
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

        public List<TreeNodeEntity> GetTreeListData()
        {
            string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "data.json");

            string txt = System.IO.File.ReadAllText(url, Encoding.UTF8);

            return JsonConvert.DeserializeObject<List<TreeNodeEntity>>(txt);
        }

        public void Debug(params string[] messages)
        {
            foreach (string item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Error(params Exception[] messages)
        {
            foreach (Exception item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Error(params string[] messages)
        {
            foreach (string item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Fatal(params string[] messages)
        {
            foreach (string item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Fatal(params Exception[] messages)
        {
            foreach (Exception item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Info(params string[] messages)
        {
            foreach (string item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Trace(params string[] messages)
        {
            foreach (string item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }

        public void Warn(params string[] messages)
        {
            foreach (string item in messages)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
        }
    }
}
