using HeBianGu.General.WpfControlLib;
using System;

namespace HeBianGu.App.Map
{
    public class AssemblyDomain : IAssemblyDomain
    {
        public LoginViewModel GetAccountConfig()
        {
            LoginViewModel loginViewModel = new LoginViewModel
            {

                //loginViewModel.LoginUseName = "HeBianGu";
                LoginUseName = "admin",

                LoginPassWord = "89757",

                Remenber = true
            };

            return loginViewModel;
        }

        private Random r = new Random();
        public bool Login(string userName, string psd, bool IsSavePSD, out string err)
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

    }
}
