using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Window.Link;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HeBianGu.App.Tool
{
    [ViewModel("Shell")]
    internal class ShellViewModel : WindowLinkViewModel
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

            ClickActions.Clear();

            ClickActions.Add(new ClickAction() { DisplayName = "我的电脑", Logo = "\xe799", Action = () => Process.Start("::{20D04FE0-3AEA-1069-A2D8-08002B30309D}") });

            ClickActions.Add(new ClickAction() { DisplayName = "控制面板", Logo = "\xe799", Action = () => Process.Start("control") });

            ClickActions.Add(new ClickAction() { DisplayName = "回收站", Logo = "\xe799", Action = () => Process.Start("::{645FF040-5081-101B-9F08-00AA002F954E}") });

            ClickActions.Add(new ClickAction() { DisplayName = "远程连接", Logo = "\xe799", Action = () => Process.Start("mstsc") });

            ClickActions.Add(new ClickAction() { DisplayName = "网络测速", Logo = "\xe799", Action = () => Process.Start("ping", "www.baidu.com -t") });

            ClickActions.Add(new ClickAction() { DisplayName = "记事本", Logo = "\xe799", Action = () => Process.Start("notepad") });

            ClickActions.Add(new ClickAction() { DisplayName = "注册表", Logo = "\xe799", Action = () => Process.Start("regedit") });

            ClickActions.Add(new ClickAction() { DisplayName = "计算器", Logo = "\xe799", Action = () => Process.Start("calc") });

            ClickActions.Add(new ClickAction() { DisplayName = "系统服务", Logo = "\xe799", Action = () => Process.Start("services.msc") });

            ClickActions.Add(new ClickAction() { DisplayName = "设备管理器", Logo = "\xe799", Action = () => Process.Start("devmgmt.msc") });

            ClickActions.Add(new ClickAction()
            {
                DisplayName = "倒计时60s关机",
                Logo = "\xe799",
                Action = () =>
                {
                    bool result = MessageProxy.Windower.ShowDialog("确定开启60s倒计时关机?");
                    if (result)
                    {
                        Process.Start("shutdown");
                    };
                }
            });

            ClickActions.Add(new ClickAction() { DisplayName = "算机管理", Logo = "\xe799", Action = () => Process.Start("compmgmt.msc") });
            ClickActions.Add(new ClickAction() { DisplayName = "本机用户和组", Logo = "\xe799", Action = () => Process.Start("lusrmgr.msc") });
            ClickActions.Add(new ClickAction() { DisplayName = "磁盘管理实用程序", Logo = "\xe799", Action = () => Process.Start("diskmgmt.msc") });
            ClickActions.Add(new ClickAction() { DisplayName = "Windows版本", Logo = "\xe799", Action = () => Process.Start("winver") });
            ClickActions.Add(new ClickAction() { DisplayName = "写字板", Logo = "\xe799", Action = () => Process.Start("write") });
            ClickActions.Add(new ClickAction() { DisplayName = "画图板", Logo = "\xe799", Action = () => Process.Start("mspaint") });
            ClickActions.Add(new ClickAction() { DisplayName = "放大镜", Logo = "\xe799", Action = () => Process.Start("magnify") });
            ClickActions.Add(new ClickAction() { DisplayName = "事件查看器", Logo = "\xe799", Action = () => Process.Start("eventvwr") });
            ClickActions.Add(new ClickAction() { DisplayName = "证书管理", Logo = "\xe799", Action = () => Process.Start("certmgr.msc") });

        }

    }



}
