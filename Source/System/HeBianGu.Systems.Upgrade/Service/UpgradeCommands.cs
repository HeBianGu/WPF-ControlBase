// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Reflection;
using System.Windows.Input;

namespace HeBianGu.Systems.Upgrade
{
    public static class UpgradeCommands
    {
        public static CheckUpgradeCommand CheckUpgrade { get; } = new CheckUpgradeCommand();
    }


    public class CheckUpgradeCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return Upgrader.Instance != null && UpgradeIniter.Instance != null;
        }

        public override void Execute(object parameter)
        {
            //var versionData = HeBianGu.Base.WpfBase.Upgrader.Instance.GetVersion(out string message);

            //if (versionData == null)
            //{
            //    MessageProxy.Snacker.ShowTime("未发现新版本:V" + Assembly.GetEntryAssembly().GetName().Version.ToString());
            //    return;
            //}

            if (UpgradeIniter.Instance.CheckUpgrade(out string error) == false)
            {
                MessageProxy.Snacker.Show(error);
            }
        }
    }
}
