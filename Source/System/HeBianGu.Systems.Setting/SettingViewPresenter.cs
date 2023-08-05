// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PasswordBox;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Systems.Setting
{
    public interface ISettingViewPresenterOption : ISettingOption
    {
        double TitleWidth { get; set; }
        bool UsePassword { get; set; }
    }

    public interface ISettingViewPresenter : IInvokePresenter
    {

    }

    [Displayer(Name = "设置", GroupName = SystemSetting.GroupBase, Icon = Icons.Set, Description = "点击此处弹出设置页面")]
    public class SettingViewPresenter : ServiceMvpSettingBase<SettingViewPresenter, ISettingViewPresenter>, ISettingViewPresenter, ISettingViewPresenterOption
    {
        private bool _usePassword;
        [DefaultValue(false)]
        [Display(Name = "启用密码")]
        public bool UsePassword
        {
            get { return _usePassword; }
            set
            {
                _usePassword = value;
                RaisePropertyChanged();
            }
        }

        private double _titleWidth = double.NaN;
        [DefaultValue(double.NaN)]
        [Display(Name = "标题宽度")]
        public double TitleWidth
        {
            get { return _titleWidth; }
            set
            {
                _titleWidth = value;
                RaisePropertyChanged();
            }
        }


        public override bool Invoke(out string message)
        {
            message = null;
            if (PasswordDialogService.Instance == null || !this.UsePassword)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SettingDialog setting = new SettingDialog();
                    MessageProxy.Container.Show(setting);
                });
                return true;
            }

            var r = PasswordDialogService.Instance?.ShowPasswordDialog();
            r.ContinueWith(x =>
            {
                if (!x.Result) return false;
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SettingDialog setting = new SettingDialog();
                    MessageProxy.Container.Show(setting);
                });

                return true;
            });
            return true;
        }
    }
}
