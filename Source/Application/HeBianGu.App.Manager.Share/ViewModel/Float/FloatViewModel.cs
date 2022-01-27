using HeBianGu.Service.Mvc;
using HeBianGu.Window.Float;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace HeBianGu.App.Manager
{
    [ViewModel("Float")]
    internal class FloatViewModel : MvcViewModelBase
    {
        protected override void Loaded(string args)
        {

        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (obj is Style style)
            {
                IconFloatModel model = new IconFloatModel();

                model.Icon = "\xe7be";

                model.DisplayName = "特特特";

                model.Action = () =>
                {
                    Debug.WriteLine("说明");
                };

                var from = Enumerable.Repeat(model, 10);

                FloatWindow.ShowStyle(style, from?.ToArray());
            }
        }

    }
}
