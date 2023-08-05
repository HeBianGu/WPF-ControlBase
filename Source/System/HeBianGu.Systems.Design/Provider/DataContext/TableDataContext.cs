using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Design
{
    [Displayer(Name = "表格数据表格", GroupName = "[表格]", Icon = "\xe6e7", Description = "表格数据")]
    public class TableDataContext<T> : RangeDesignDataContextBase<DataGridDesignPresenter>
    {
        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<T> ItemsSource { get; }

        [Display(Name = "列头设置")]
        [ReadOnly(true)]
        public IEnumerable<ColumnPropertyInfo> ColumnPropertyInfos { get; }

        public TableDataContext(IEnumerable<T> itemsSource, int visibleCount = 0)
        {
            ItemsSource = itemsSource;
            List<ColumnPropertyInfo> cps = new List<ColumnPropertyInfo>();
            foreach (var p in typeof(T).GetProperties())
            {
                ColumnPropertyInfo cp = new ColumnPropertyInfo(p);
                cp.Header = p.GetCustomAttribute<DisplayAttribute>()?.Name ?? p.Name;
                cps.Add(cp);
            }

            foreach (var item in cps.Take(visibleCount))
            {
                item.IsVisible = true;
            }
            ColumnPropertyInfos = cps;
        }

        public TableDataContext(IEnumerable<T> itemsSource, IEnumerable<ColumnPropertyInfo> columnPropertyInfos)
        {
            ItemsSource = itemsSource;
            ColumnPropertyInfos = columnPropertyInfos;
        }
        public override void RefreshPresenter(DataGridDesignPresenter presenter)
        {
            presenter.ItemsSource = ItemsSource.Skip(Skip).Take(Take);
            presenter.ColumnPropertyInfos = ColumnPropertyInfos.ToObservable();
        }
    }

    [Displayer(Name = "仓储模型表格数据", GroupName = "[表格]", Icon = "\xe6e7", Description = "仓储模型表格数据")]
    public class RepositoryTableDataContext<T> : TableDataContext<T> where T : StringEntityBase
    {
        public RepositoryTableDataContext() : base(ServiceRegistry.Instance.GetInstance<IStringRepository<T>>().GetList())
        {
            this.Name = typeof(T).GetCustomAttribute<DisplayerAttribute>()?.Name ?? typeof(T).Name;
        }
    }
}
