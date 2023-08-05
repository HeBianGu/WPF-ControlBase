// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Windows;

namespace HeBianGu.Service.Mvp
{
    public interface IMvpSettingOption : ISettingOption
    {
        bool IsVisible { get; set; }
        bool IsEnabled { get; set; }
    }

    public abstract class ServiceMvpSettingBase<Setting, Interface> : ServiceSettingInstance<Setting, Interface>, IInvokePresenter
        where Setting : class, Interface, new()
        where Interface : IViewPresenter
    {

        [XmlIgnore]
        [Browsable(false)]
        public virtual RelayCommand InvokeCommand => new RelayCommand(async (s, e) =>
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
            MessageProxy.Presenter.ShowClose(this, null, this.Name, x =>
             {
                 x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                 x.VerticalContentAlignment = VerticalAlignment.Stretch;
                 x.Padding = new Thickness(20);
             });
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

        private object _flag;
        [XmlIgnore]
        [Browsable(false)]
        public object Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;
                RaisePropertyChanged();
            }
        }

        private bool _isVisible;
        [Browsable(false)]
        [DefaultValue(true)]
        [Display(Name = "设置显示此功能")]
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabled;
        [Browsable(false)]
        [DefaultValue(true)]
        [Display(Name = "设置启用此功能")]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }
    }
}
