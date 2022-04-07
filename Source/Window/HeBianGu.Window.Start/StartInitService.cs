// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Window.Start
{
    public class StartInitService : IStartInitService
    {
        public bool Start(params Func<IStartWindow, bool>[] actions)
        {
            StartWindowBase startWindow = null;

            if (StartSetting.Instance.Type == WindowType.Logo)
            {
                startWindow = new StartWindow();
            }
            else
            {
                startWindow = new MessageStartWindow();
            }

            Task.Run(() =>
            {
                foreach (Func<IStartWindow, bool> item in actions)
                {
                    item?.Invoke(startWindow);
                }

                Thread.Sleep(StartSetting.Instance.SleepMilliseconds);

                startWindow.Dispatcher.Invoke(() =>
                {
                    startWindow.BeginClose();
                });
            });

            startWindow.ShowDialog();

            return true;
        }
    }
}
