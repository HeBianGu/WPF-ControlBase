using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.Mvp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace HeBianGu.Systems.WinTool
{

    public interface ICounterViewPresenterOption
    {
        CounterSettingPresenter Collection { get; }
    }


    public interface ICounterViewPresenter : IViewPresenter, IStartWindowLoad
    {

    }


    [Displayer(Name = "电脑监视器", GroupName = SystemSetting.GroupApp, Icon = "\xe8d7", Description = "电脑资源监视器：可以监视CPU、内存、磁盘、网络等参数")]
    public class CounterViewPresenter : ServiceMvpSettingBase<CounterViewPresenter, ICounterViewPresenter>, ICounterViewPresenter, ICounterViewPresenterOption
    {
        //[XmlIgnore]
        //[Browsable(false)]
        //public ObservableCollection<CounterCategoryBase> Collection { get; } = new ObservableCollection<CounterCategoryBase>();


        private CounterSettingPresenter _collection = new CounterSettingPresenter();
        [Display(Name = "报警参数配置")]
        [Property(UsePresenter = true)]
        public CounterSettingPresenter Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        public bool Load(IStartWindow window, out string message)
        {
            message = null;
            //if (this.Collection.Count == 0)
            //{
            this.Collection.Clear();
            var types = GetType().Assembly.GetTypes().Where(x => typeof(CounterCategoryBase).IsAssignableFrom(x)).Where(x => x.IsClass && !x.IsAbstract && x.GetCustomAttribute<DisplayerAttribute>() != null).OrderBy(x => x.GetCustomAttribute<DisplayerAttribute>()?.Order);
            foreach (var x in types)
            {
                var item = Activator.CreateInstance(x);
                if (item is CounterCategoryBase counter)
                {
                    window?.SetMessage("正在加载" + counter.Name);
                    counter.LoadCounters(counter.GroupName);
                    this.Collection.Add(counter);
                }
                Thread.Sleep(100);
            }
            //}
            //else
            //{
            //    foreach (var item in this.Collection)
            //    {
            //        if (item is CounterCategory category)
            //        {
            //            category.LoadCounters(category.CategoryName);
            //        }
            //        else if (item is CounterCategoryBase counter)
            //        {
            //            window?.SetMessage("正在加载" + counter.Name);
            //            counter.LoadCounters(counter.GroupName);
            //        }
            //        Thread.Sleep(100);
            //    }
            //}
            window?.SetMessage("加载完成" + Name);
            return true;
        }

        protected override void Loaded(object obj)
        {
            foreach (var item in Collection)
            {
                item.Start();
            }
        }
        [XmlIgnore]
        [Browsable(false)]
        public RelayCommand AddCounterCategoryCommand => new RelayCommand(async (s, e) =>
        {
            CounterCategory category = new CounterCategory();
            var r = await MessageProxy.PropertyGrid.ShowEdit(category, null, "新增自定义监视器");
            if (!r) return;
            await MessageProxy.Messager.ShowWaitter(() =>
             {
                 category.LoadCounters(category.CategoryName);
             });
            this.Collection.Add(category);
            category.Start();
            MessageProxy.Snacker.ShowTime("添加成功");
        });
    }

    public class CounterSettingPresenter : ObservableCollection<CounterCategoryBase>
    {
        public CounterSettingPresenter()
        {

        }
        public CounterSettingPresenter(IEnumerable<CounterCategoryBase> collection) : base(collection)
        {

        }
    }

}
