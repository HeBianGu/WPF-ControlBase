using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Notification
{
    public interface INotificationViewPresenter : IInvokePresenter
    {

    }

    public interface INotificationViewPresenterOption : IMvpSettingOption
    {
        Dock Dock { get; set; }
    }

    [Displayer(Name = "系统通知", GroupName = SystemSetting.GroupMessage, Icon = "\xe9e4", Description = "应用此功能可以查看最近的收到的系统通知")]
    public class NotificationViewPresenter : ServiceMvpSettingBase<NotificationViewPresenter, INotificationViewPresenter>, INotificationViewPresenter, INotificationViewPresenterOption
    {
        public NotificationViewPresenter()
        {
            for (int i = 0; i < 10; i++)
            {
                this.Notifications.Add(new Notification()
                {
                    Title = "Visul Studio 2022 update",
                    Message = "Version 17.2.6 is now available",
                    DateTime = DateTime.Now.AddDays(-i)
                });
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

        private double _minWidth;
        [DefaultValue(100.0)]
        [Display(Name = "最小宽度")]
        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                RaisePropertyChanged();
            }
        }


        private double _maxWidth;
        [DefaultValue(600.0)]
        [Display(Name = "最大宽度")]
        public double MaxWidth
        {
            get { return _maxWidth; }
            set
            {
                _maxWidth = value;
                RaisePropertyChanged();
            }
        }

        private Notifications _notifications = new Notifications();
        [Browsable(false)]
        public Notifications Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(out string message)
        {
            message = null;
            MessageProxy.Presenter.ShowClose(this.Notifications, null, this.Name, x =>
               {
                   var trans = new TranslateTransition()
                   {
                       StartPoint = new System.Windows.Point(400, 0),
                       EndPoint = new System.Windows.Point(300, 0)
                   };
                   ContainPanel.SetTransition(x, trans);
                   x.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                   Cattach.SetCornerRadius(x, new System.Windows.CornerRadius(0));
               });
            return true;
        }
    }

    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class Notifications : ObservableCollection<Notification>
    {

    }
}
