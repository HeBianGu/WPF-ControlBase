// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Guide
{
    public interface INewGuideViewPresenterOption : IMvpSettingOption
    {
        bool UseOnUpdated { get; set; }
    }

    public interface INewGuideViewPresenter : IInvokePresenter
    {

    }

    [Displayer(Name = "新增功能向导", GroupName = SystemSetting.GroupSystem, Icon = IconAll.QuesitionFill, Description = "点击显示新增功能向导")]
    public class NewGuideViewPresenter : ServiceMvpSettingBase<NewGuideViewPresenter, INewGuideViewPresenter>, INewGuideViewPresenter, INewGuideViewPresenterOption
    {
        //  ToDo：增加程序第一启动时显示新手向导
        private bool _useOnUpdated;
        [DefaultValue(true)]
        [Display(Name = "在程序第一次更新启动时显示")]
        public bool UseOnUpdated
        {
            get { return _useOnUpdated; }
            set
            {
                _useOnUpdated = value;
                RaisePropertyChanged();
            }
        }


        private string _guideGroup;
        [Browsable(false)]
        public string GuideGroup
        {
            get { return _guideGroup; }
            set
            {
                _guideGroup = value;
                RaisePropertyChanged();
            }
        }


        public override void Load()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                base.Load();
            });

            if(this.UseOnUpdated)
            {
                Cattach.SetIsNewGuide(Application.Current.MainWindow, true);
                this.UseOnUpdated = false;
            }
        }


        public override bool Invoke(out string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var use = Cattach.GetIsNewGuide(Application.Current.MainWindow);
                Cattach.SetIsNewGuide(Application.Current.MainWindow, !use);
            });
            message = null;
            return true;
        }
    }
}
