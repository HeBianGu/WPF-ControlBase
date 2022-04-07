// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Component
{
    public class ProgressComponentNode : ComponentNode
    {
        private int _value;
        /// <summary> 说明  </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        public override IActionResult Invoke(IAction previors)
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10);

                this.Value = i + 1;
            }

            return OK();
        }

        public override async Task<IActionResult> InvokeAsync(IAction previors)
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(10);

                    this.Value = i + 1;
                }
            });

            return OK();
        }
    }
}
