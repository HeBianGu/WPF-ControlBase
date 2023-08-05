// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{
    /// <summary>
    /// 异步操作ED进度
    /// </summary>
    public interface IAsnycProgressViewPresenter : IInvokePresenter
    {
        void Enqueue(Action<IAsnycProgressViewPresenter> action);
        double Value { get; set; }
        string Message { get; set; }
    }
    public interface IAsnycProgressViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "异步进度", GroupName = SystemSetting.GroupMessage, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class AsnycProgressViewPresenter : ServiceMvpSettingBase<AsnycProgressViewPresenter, IAsnycProgressViewPresenter>, IAsnycProgressViewPresenter, IAsnycProgressViewPresenterOption
    {
        private bool _useVisibleInBusy;
        [DefaultValue(true)]
        [Display(Name = "启用自动隐藏")]
        public bool UseVisibleInBusy
        {
            get { return _useVisibleInBusy; }
            set
            {
                _useVisibleInBusy = value;
                RaisePropertyChanged();
            }
        }

        private double _value;
        [Browsable(false)]
        [XmlIgnore]
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        private string _message;
        [Browsable(false)]
        [XmlIgnore]
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        private bool _isBusy;
        [Browsable(false)]
        [XmlIgnore]
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private bool _isIndeterminate;
        [Browsable(false)]
        [XmlIgnore]
        public bool IsIndeterminate
        {
            get { return _isIndeterminate; }
            set
            {
                _isIndeterminate = value;
                RaisePropertyChanged();
            }
        }

        Queue<Action<IAsnycProgressViewPresenter>> _tasks = new Queue<Action<IAsnycProgressViewPresenter>>();
        public void Enqueue(Action<IAsnycProgressViewPresenter> action)
        {
            System.Diagnostics.Debug.WriteLine("说明1");

            _tasks.Enqueue(action);
            this.Start();
        }

        void Start()
        {
            if (this.IsBusy) return;
            Task.Run(() =>
            {
                this.IsBusy = true;
                while (_tasks.Count > 0)
                {
                    var action = _tasks.Dequeue();
                    action?.Invoke(this);
                }
                this.IsBusy = false;
                this.Value = 0.0;
                this.Message = null;
            });
        }

        public override bool Invoke(out string message)
        {
            Action<IAsnycProgressViewPresenter> action = l =>
                {
                    for (int i = 0; i < 101; i++)
                    {
                        l.Value = i;
                        l.Message = i.ToString();
                        Thread.Sleep(30);
                    }
                };

            this.Enqueue(action);
            message = null;
            return true;
        }
    }
}
