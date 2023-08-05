// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace HeBianGu.Control.MessageListBox
{
    public interface IErrorMessageViewPresenterOption
    {

    }

    [Displayer(Name = "错误日志", GroupName = SystemSetting.GroupMessage, Icon = IconAll.QuesitionFill, Description = "点击显示新手向导")]
    public class ErrorMessageViewPresenter : ServiceMvpSettingBase<ErrorMessageViewPresenter, IErrorMessageViewPresenter>, IErrorMessageViewPresenter, IErrorMessageViewPresenterOption
    {
        public ErrorMessageViewPresenter()
        {
            //for (int i = 0; i < 50; i++)
            //{
            //    //  ToDo：ErrorMessage 建议data修改成object 通过模板控制，增加Action后续操作跳转
            //    this.Collection.Add(new ErrorMessage() { Title = "运行失败", Data = "这是一条运行成功的信息" });
            //}
        }
        private ObservableCollection<ErrorMessage> _collection = new ObservableCollection<ErrorMessage>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<ErrorMessage> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private int _capacity = 50;
        [Range(10, 100)]
        [Displayer(Name = "消息容量")]
        public int Capacity
        {
            get { return _capacity; }
            set
            {
                _capacity = value;
                RaisePropertyChanged();
            }
        }

        public void AddMessage(Exception ex, string data = null, string title = "错误消息")
        {
            ErrorMessage message = new ErrorMessage()
            {
                Data = data ?? ex.Message,
                Title = title,
                Exception = ex,
            };

            Application.Current?.Dispatcher?.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                this.Collection.Add(message);
                this.Collection = this.Collection.Skip(this.Collection.Count - this.Capacity).Take(this.Capacity).ToObservable();

            }));
        }

        public void AddMessage(string data, string title = "错误消息")
        {
            ErrorMessage message = new ErrorMessage()
            {
                Data = data,
                Title = title
            };
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                this.Collection.Add(message);
                this.Collection = this.Collection.Skip(this.Collection.Count - this.Capacity).Take(this.Capacity).ToObservable();
            });
        }
    }
}
