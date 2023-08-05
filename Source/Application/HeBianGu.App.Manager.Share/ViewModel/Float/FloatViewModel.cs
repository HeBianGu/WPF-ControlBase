using HeBianGu.Base.WpfBase;
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


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);

        /// <summary> 命令通用方法 </summary>
        protected void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            if (obj is Style style)
            {
                IconFloatModel model = new IconFloatModel
                {
                    Icon = "\xe7be",

                    DisplayName = "特特特",

                    Action = () =>
                    {
                        Debug.WriteLine("说明");
                    }
                };

                System.Collections.Generic.IEnumerable<IconFloatModel> from = Enumerable.Repeat(model, 10);

                FloatWindow.ShowStyle(style, from?.ToArray());
            }
        }

    }
}
