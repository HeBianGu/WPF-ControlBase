// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowTreeViewPresenter : IViewPresenter
    {

    }

    public interface IWindowTreeViewPresenterOption : ITreeSettingOption
    {
        double Width { get; set; }

        bool UseTool { get; set; }
    }

    [Displayer(Name = "菜单栏", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Tree, Description = "点击显示其他功能")]
    public class WindowTreeViewPresenter : TreeViewPresenterBase<WindowTreeViewPresenter, IWindowTreeViewPresenter>, IWindowTreeViewPresenter, IWindowTreeViewPresenterOption
    {
        private double _width;
        /// <summary> 说明  </summary>
        [DefaultValue(250.0)]
        [Display(Name = "宽度")]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }

        private bool _useTool;
        [DefaultValue(true)]
        [Display(Name = "启用控制按钮")]
        public bool UseTool
        {
            get { return _useTool; }
            set
            {
                _useTool = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(out string message)
        {
            return base.Invoke(out message);
        }

        public override bool Invoke(out string message, object param)
        {
            if (WindowContentViewPresenter.Instance == null)
            {
                throw new ArgumentNullException("请先注册<IWindowContentViewPresenter>接口");
            }
            message = null;
            if (param is IViewPresenter presenter)
                WindowContentViewPresenter.Instance.AddPersenter(presenter);
            return true;
            //return base.Invoke(out message, param);
        }
    }
}
