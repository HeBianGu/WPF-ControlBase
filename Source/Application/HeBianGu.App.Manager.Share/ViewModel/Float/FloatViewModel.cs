using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using HeBianGu.Window.Float;

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
