using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HeBianGu.Systems.Design
{
    public interface IDesignDataContext
    {
        void Refresh(IDesignPresenter presenter);
        bool IsMatch(IDesignPresenter presenter);
    }

    public abstract class RangeDesignDataContextBase<T> : DesignDataContextBase<T> where T : class
    {
        private int _take = 10;
        [Display(Name = "提取数量")]
        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
                DispatcherRaisePropertyChanged();
            }
        }

        private int _skip = 0;
        [Display(Name = "略过数量")]
        public int Skip
        {
            get { return _skip; }
            set
            {
                _skip = value;
                DispatcherRaisePropertyChanged();
            }
        }
    }

    public abstract class DesignDataContextBase<T> : DisplayerViewModelBase, IDesignDataContext where T : class
    {
        public bool IsMatch(IDesignPresenter presenter)
        {
            if (presenter == null)
                return false;
            return typeof(T).IsAssignableFrom(presenter.GetType());
        }

        public virtual void Refresh(IDesignPresenter presenter)
        {
            var p = presenter as T;
            if (p == null)
                return;
            this.RefreshPresenter(presenter as T);
        }

        public abstract void RefreshPresenter(T presenter);

        protected override void OnDispatcherPropertyChanged()
        {
            DesginViewPresenter.Instance.DataContextChanged?.Invoke(this);
        }

        public override string ToString()
        {
            return this.GroupName + this.Name;
        }
    }
}
