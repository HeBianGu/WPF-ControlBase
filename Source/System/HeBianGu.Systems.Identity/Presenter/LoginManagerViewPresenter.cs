// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Identity
{
    public interface ILoginManagerViewPresenterOption : IItemsSettingOption
    {

    }

    [Displayer(Name = "账户管理", GroupName = SystemSetting.GroupSecurity, Icon = "\xe903", Description = "这是一个账户管理页面的信息")]
    public class LoginManagerViewPresenter : ItemsViewPresenterBase<LoginManagerViewPresenter, ILoginManagerViewPresenter>, ILoginManagerViewPresenter, ILoginManagerViewPresenterOption
    {
        public override bool Invoke(out string message, object param)
        {
            message = null;

            if(param is ILogoutViewPresenter logout)
            {
               return logout.Invoke(out message);
            }

            if (param is IPasswordEditViewPresenter passwordEdit)
            {
                return passwordEdit.Invoke(out message);
            }

            if (param is IViewPresenter presenter)
            {
                MessageProxy.Presenter.ShowClose(presenter, null, presenter.Name, x =>
                {
                    x.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
                    x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                    //x.Width = 800;
                    x.Padding = new System.Windows.Thickness(50);
                    //var trans = TranslateTransition.GetDockLeft();
                    //ContainPanel.SetTransition(x, trans);
                    //x.HorizontalAlignment = System.Windows.HorizontalAlignment.lef;
                    //Cattach.SetCornerRadius(x, new System.Windows.CornerRadius(5));
                });
            }


            //MessageProxy.Container.Close();

            //MessageProxy.Presenter.ShowClose(this, null, "提示");
            return true;
        }
    }



}
