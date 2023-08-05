using HeBianGu.App.Touch.View.Share;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;

namespace HeBianGu.App.Touch
{
    public class AssemblyDomain : IAssemblyDomain
    {

        public static IAssemblyDomain Instance
        {
            get
            {
                return ServiceRegistry.Instance.GetInstance<IAssemblyDomain>();
            }
        }

        public void GoToLinkAction(string controller, string action, object[] parameter)
        {
            DataSourceLocator.Instance.ShellViewModel.GoLinkAction(controller, action, parameter);
        }


        public void GoToLinkAction(string controller, string action)
        {
            GoToLinkAction(controller, action, null);
        }

        /// <summary> 判断是否是身份证号 </summary>
        public bool IsCardID(string id)
        {
            if (CheckIDCard18(id)) return true;
            if (CheckIDCard15(id)) return true;

            return false;
        }

        private bool CheckIDCard18(string Id)
        {
            if (Id.Length < 18) return false;

            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;
            }
            return true;//正确
        }

        private bool CheckIDCard15(string Id)
        {
            if (Id.Length < 15) return false;
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }
            return true;//正确
        }

        #region - 定时任务 -

        private MonitorActiveService _monitor = new MonitorActiveService();

        /// <summary> 开始超时任务 </summary>
        public void StartMonitor()
        {
            _monitor.StartMonitor();

            _monitor.OnCheckCount = l =>
            {
                if (AwaitControl.IsShowed) return;

                MessageProxy.Container.Show(AwaitControl.Create());
            };
        }

        #endregion
    }
}
