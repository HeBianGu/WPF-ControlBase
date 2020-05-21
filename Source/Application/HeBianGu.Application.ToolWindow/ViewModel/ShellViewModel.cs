using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.ToolWindow
{
    [ViewModel("Shell")]
    class ShellViewModel : WindowLinkViewModel
    {


        private ObservableCollection<ClickAction> _clickActions = new ObservableCollection<ClickAction>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ClickAction> ClickActions
        {
            get { return _clickActions; }
            set
            {
                _clickActions = value;
                RaisePropertyChanged("ClickActions");
            }
        }

        protected override void Init()
        {
            base.Init();

            this.ClickActions.Clear();

            this.ClickActions.Add(new ClickAction() { DisplayName = "我的电脑", Logo = "\xe799", Action = () => Process.Start("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "控制面板", Logo = "\xe799", Action = () => Process.Start("control") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "回收站", Logo = "\xe799", Action = () => Process.Start("::{645FF040-5081-101B-9F08-00AA002F954E}") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "远程连接", Logo = "\xe799", Action = () => Process.Start("mstsc") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "网络测速", Logo = "\xe799", Action = () => Process.Start("ping", "www.baidu.com -t") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "记事本", Logo = "\xe799", Action = () => Process.Start("notepad") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "注册表", Logo = "\xe799", Action = () => Process.Start("regedit") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "计算器", Logo = "\xe799", Action = () => Process.Start("calc") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "系统服务", Logo = "\xe799", Action = () => Process.Start("services.msc") });

            this.ClickActions.Add(new ClickAction() { DisplayName = "设备管理器", Logo = "\xe799", Action = () => Process.Start("devmgmt.msc") });

            this.ClickActions.Add(new ClickAction()
            {
                DisplayName = "倒计时60s关机",
                Logo = "\xe799",
                Action = () =>
                {
                    var result = MessageWindow.ShowDialog("确定开启60s倒计时关机?");
                    if (result)
                    {
                        Process.Start("shutdown");
                    };
                }
            });

            this.ClickActions.Add(new ClickAction() { DisplayName = "算机管理", Logo = "\xe799", Action = () => Process.Start("compmgmt.msc") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "本机用户和组", Logo = "\xe799", Action = () => Process.Start("lusrmgr.msc") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "磁盘管理实用程序", Logo = "\xe799", Action = () => Process.Start("diskmgmt.msc") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "Windows版本", Logo = "\xe799", Action = () => Process.Start("winver") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "写字板", Logo = "\xe799", Action = () => Process.Start("write") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "画图板", Logo = "\xe799", Action = () => Process.Start("mspaint") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "放大镜", Logo = "\xe799", Action = () => Process.Start("magnify") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "事件查看器", Logo = "\xe799", Action = () => Process.Start("eventvwr") });
            this.ClickActions.Add(new ClickAction() { DisplayName = "证书管理", Logo = "\xe799", Action = () => Process.Start("certmgr.msc") });

        }


        Random random = new Random();

        protected async override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {

            }

            //  Do：取消
            else if (command == "init")
            {

            }
        }
    }



}
