// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{

    public interface IInvokePresenter : IViewPresenter
    {
        bool Invoke(out string message);

        Task<Tuple<bool,string>> InvokeAsync(object param);

        bool Invoke(out string message, object param);
    }

    public interface ITogglePresenter : IViewPresenter
    {
        bool Invoke(bool value, out string message);
    }
    public abstract class InvokePresenterBase : SettingBase, IInvokePresenter
    {
        private Dock _dock;
        [DefaultValue(Dock.Right)]
        [Display(Name = "停靠方向")]
        public Dock Dock
        {
            get { return _dock; }
            set
            {
                _dock = value;
                RaisePropertyChanged();
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public RelayCommand InvokeCommand => new RelayCommand(async (s, e) =>
        {
            var r = await this.InvokeAsync(e);
            if (!r.Item1)
            {
                MessageProxy.Snacker.ShowTime(r.Item2);
            }
        });
     

        public virtual bool Invoke(out string message)
        {
            message = null;
            MessageProxy.Presenter.ShowClose(this, null, this.Name);
            return true;
        }

        public virtual async Task<Tuple<bool, string>> InvokeAsync(object param)
        {
            string message = null;
            var r = await Task.Run(() =>
            {
                return this.Invoke(out message, param);
            });
            return new Tuple<bool, string>(r, message);
        }


        public virtual bool Invoke(out string message, object param)
        {
            return this.Invoke(out message);
        }

    }

    public abstract class TogglePresenterBase<ViewPresenter, Interface> : ServiceMvpSettingBase<ViewPresenter, Interface>, ITogglePresenter
        where ViewPresenter :class, Interface, new()
        where Interface : IViewPresenter
    {
        private bool _isChecked;
        [DefaultValue(true)]
        [Display(Name = "设置启用此功能")]
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
            }
        }


        private Dock _dock;
        [DefaultValue(Dock.Right)]
        [Display(Name = "停靠方向")]
        public Dock Dock
        {
            get { return _dock; }
            set
            {
                _dock = value;
                RaisePropertyChanged();
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public RelayCommand InvokeCommand => new RelayCommand((s, e) =>
        {
            if (bool.TryParse(e?.ToString(), out bool value))
            {
                var r = this.Invoke(value, out string message);
                if (!r)
                {
                    MessageProxy.Snacker.ShowTime(message);
                }
            }
        });

        public abstract bool Invoke(bool value, out string message);
    }
}
