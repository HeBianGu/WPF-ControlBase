// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Panel
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [Displayer(Name = "弹窗容器", GroupName = SystemSetting.GroupStyle)]
    public class ContainPanelSetting : LazySettingInstance<ContainPanelSetting>
    {
        public ContainPanelSetting()
        {
            this._canAddMatch = (x, y) =>
              {
                  if (x.GetType() == y.GetType())
                  {
                      if (x is ContentControl cx && y is ContentControl cy)
                          if (cx.Content != cy.Content) return true;
                      return false;
                  }
                  return true;
              };
        }
        private bool _useClickClose;
        [DefaultValue(true)]
        [Display(Name = "启用点击灰色区域自动隐藏")]
        public bool UseClickClose
        {
            get { return _useClickClose; }
            set
            {
                _useClickClose = value;
                RaisePropertyChanged();
            }
        }

        private Func<UIElement, UIElement, bool> _canAddMatch;
        [Browsable(false)]
        public Func<UIElement, UIElement, bool> CanAddMatch
        {
            get { return _canAddMatch; }
            set
            {
                _canAddMatch = value;
                RaisePropertyChanged();
            }
        }


    }
}
