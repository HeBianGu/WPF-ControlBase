using System.Collections.Generic;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Design
{
    public interface IDesignDataSource
    {
        void AddDataContext(IDesignDataContext designDataContext);
        bool IsMatch(IDesignPresenter presenter);
        void Filter(IDesignPresenter designPresenter);
    }

    public abstract class DesignDataSourceBase : DisplayerViewModelBase, IDesignDataSource
    {
        private List<IDesignDataContext> _cacheContexts = new List<IDesignDataContext>();

        public void AddDataContext(IDesignDataContext designDataContext)
        {
            _cacheContexts.Add(designDataContext);
        }

        private IEnumerable<IDesignDataContext> _contexts = new ObservableCollection<IDesignDataContext>();
        [Browsable(false)]
        [XmlIgnore]
        public IEnumerable<IDesignDataContext> Contexts
        {
            get { return _contexts; }
            set
            {
                _contexts = value;
                RaisePropertyChanged();
            }
        }

        private IDesignDataContext _selectedContext;
        [XmlIgnore]
        [Display(Name = "选择上下文")]
        [BindingGetSelectSourceProperty(nameof(Contexts))]
        [PropertyItemType(typeof(ComboBoxSelectSourcePropertyItem))]
        public IDesignDataContext SelectedContext
        {
            get { return _selectedContext; }
            set
            {
                _selectedContext = value;
                RaisePropertyChanged();
                DesginViewPresenter.Instance.DataContextChanged?.Invoke(value);
            }
        }

        public void Filter(IDesignPresenter designPresenter)
        {
            this.Contexts = this._cacheContexts.Where(x => x.IsMatch(designPresenter));
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool IsMatch(IDesignPresenter presenter)
        {
            this.Filter(presenter);
            return this.Contexts.Count() > 0;
        }
    }

}
