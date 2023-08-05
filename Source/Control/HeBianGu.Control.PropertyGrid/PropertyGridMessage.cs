// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace HeBianGu.Control.PropertyGrid
{
    public interface IPropertyGridMessageOption
    {
        Action<PropertyGrid> Builder { get; set; }
    }

    [Displayer(Name = "属性消息", GroupName = SystemSetting.GroupMessage)]
    public class PropertyGridMessage : ServiceSettingInstance<PropertyGridMessage, IPropertyGridMessage>, IPropertyGridMessage, IPropertyGridMessageOption
    {
        [XmlIgnore]
        [Browsable(false)]
        public Action<PropertyGrid> Builder { get; set; }

        public async Task<bool> ShowEdit<T>(T value, Predicate<T> match = null, string title = "编辑", Action<IPropertyGridOption> builder = null, ComponentResourceKey key = null)
        {
            Action<PropertyGrid> action = x =>
            {
                x.Title = title;
                x.MinWidth = 400;
                this.Builder?.Invoke(x);
                builder?.Invoke(x);
                x.UsePropertyView = false;
            };
            return await PropertyGrid.Show(value, match, title, action, key);
        }

        public async Task<bool> ShowView<T>(T value, Predicate<T> match = null, string title = "详情", Action<IPropertyGridOption> builder = null, ComponentResourceKey key = null)
        {
            Action<PropertyGrid> action = x =>
            {
                x.Title = title;
                x.MinWidth = 400;
                this.Builder?.Invoke(x);
                builder?.Invoke(x);
                x.UsePropertyView = true;
            };
            return await PropertyGrid.Show(value, match, title, action, key);
        }

        public async Task<bool> ShowEdits(string title = "编辑", params object[] value)
        {
            ItemsPropertyGridPresenter presenter = new ItemsPropertyGridPresenter();
            presenter.Objs = value.ToObservable();
            return await MessageProxy.Presenter.Show(presenter, x =>
            {
                List<string> messages = null;
                bool r = x.Objs.All(l => l.ModelState(out messages));
                if (r == false)
                    MessageProxy.Snacker.ShowTime(messages?.FirstOrDefault());
                return r;
            }, title);
        }
    }

    public class ItemsPropertyGridPresenter : NotifyPropertyChangedBase
    {
        private ObservableCollection<object> _objs = new ObservableCollection<object>();

        public ObservableCollection<object> Objs
        {
            get { return _objs; }
            set
            {
                _objs = value;
                RaisePropertyChanged("Objs");
            }
        }
    }


    [Displayer(Name = "属性表单", GroupName = SystemSetting.GroupControl)]
    public class PropertyGridSetting : LazySettingInstance<PropertyGridSetting>
    {
        private bool _useHistory;
        /// <summary> 说明  </summary>
        public bool UseHistory
        {
            get { return _useHistory; }
            set
            {
                _useHistory = value;
                RaisePropertyChanged();
            }
        }
    }


}
