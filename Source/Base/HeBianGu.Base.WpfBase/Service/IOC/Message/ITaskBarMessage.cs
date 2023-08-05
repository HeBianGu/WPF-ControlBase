// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shell;

namespace HeBianGu.Base.WpfBase
{
    public interface ITaskBarMessage
    {
        void Show(Action<TaskbarItemInfo> action);
        void ShowImage(ImageSource image);
        void ShowNormal(Action<TaskbarItemInfo> action);
        Task ShowPercent(Action<TaskbarItemInfo> action);
        Task<bool> ShowWaitting(Func<bool> action);
    }
}
