// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Component;
using System;
using System.Threading;
using System.Threading.Tasks;
using IAction = HeBianGu.Systems.Component.IAction;
using IActionResult = HeBianGu.Systems.Component.IActionResult;

namespace HeBianGu.Systems.Automation
{
    public class AutomationPersenter : InvokePresenterBase, IAutomationPersenter
    {
        //public string Name { get; set; }

        public override bool Invoke(out string message)
        {
            message = null;
            PresenterAutomation automation = new PresenterAutomation();

            MessageProxy.Container.Show(automation);

            return true;
        }

    }


    public class ActionTest : ComponentNode
    {
        public ActionTest()
        {
            this.Name = "Action";
        }

        public override IActionResult Invoke(IAction previors)
        {
            throw new NotImplementedException();
        }

        private static Random random = new Random();

        public override async Task<IActionResult> InvokeAsync(IAction previors)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });

            if (random.Next(30) == 1)
            {
                throw new ArgumentNullException();
            }

            return random.Next(50) == 1 ? this.Bad() : this.OK();
        }


        private double _startValue;
        /// <summary> 说明  </summary>
        public double StartValue
        {
            get { return _startValue; }
            set
            {
                _startValue = value;
                RaisePropertyChanged("StartValue");
            }
        }

        private double _endValue;
        /// <summary> 说明  </summary>
        public double EndValue
        {
            get { return _endValue; }
            set
            {
                _endValue = value;
                RaisePropertyChanged("EndValue");
            }
        }


    }
}
