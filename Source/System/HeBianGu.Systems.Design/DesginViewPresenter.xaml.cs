using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Design
{
    public interface IDesginViewPresenterOption : IMvpSettingOption
    {
        void AddSource(IDesignDataSource source);
    }
    public interface IDesginViewPresenter : IInvokePresenter
    {
        void Filter(IDesignPresenter designPresenter);
        Action<IDesignDataContext> DataContextChanged { get; set; }
    }

    [Displayer(Name = "设计器", GroupName = SystemSetting.GroupMessage, Icon = "\xe6e7", Description = "设计器")]
    public class DesginViewPresenter : ServiceMvpSettingBase<DesginViewPresenter, IDesginViewPresenter>, IDesginViewPresenter, IDesginViewPresenterOption
    {
        List<IDesignDataSource> _cacheDataSources = new List<IDesignDataSource>();
        public void AddSource(IDesignDataSource source)
        {
            _cacheDataSources.Add(source);
        }

        private ObservableCollection<IDesignDataSource> _dataSources = new ObservableCollection<IDesignDataSource>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IDesignDataSource> DataSources
        {
            get { return _dataSources; }
            set
            {
                _dataSources = value;
                RaisePropertyChanged();
            }
        }

        private IDesignDataSource _selectedDataSource;
        [XmlIgnore]
        [Display(Name = "选择数据源")]
        [BindingGetSelectSourceProperty(nameof(DataSources))]
        [PropertyItemType(typeof(ComboBoxSelectSourcePropertyItem))]
        public IDesignDataSource SelectedDataSource
        {
            get { return _selectedDataSource; }
            set
            {
                _selectedDataSource = value;
                RaisePropertyChanged();
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Action<IDesignDataContext> DataContextChanged { get; set; }

        public void Filter(IDesignPresenter designPresenter)
        {
            this.DataSources = this._cacheDataSources.Where(x => x.IsMatch(designPresenter)).ToObservable();
        }
    }
}
