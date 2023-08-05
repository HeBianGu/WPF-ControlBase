// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{

    public interface IItemsViewPresenter : IInvokePresenter
    {
        ObservableCollection<IViewPresenter> Presenters { get; }
        bool AddPersenter(IViewPresenter presenter);
    }

    public interface IItemsSettingOption : IMvpSettingOption
    {
        bool AddPersenter(IViewPresenter presenter);
    }

    public abstract class ItemsViewPresenterBase<ViewPresenter, Interface> : ServiceMvpSettingBase<ViewPresenter, Interface>, IItemsSettingOption, IInvokePresenter
        where ViewPresenter : class, Interface, new()
        where Interface : IViewPresenter
    {


        //private IWindowComponentViewPresenter _windowComponentViewPresenter;
        //[XmlIgnore]
        //[Browsable(false)]
        //public IWindowComponentViewPresenter WindowComponentViewPresenter
        //{
        //    get { return _windowComponentViewPresenter; }
        //    set
        //    {
        //        _windowComponentViewPresenter = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private ObservableCollection<IViewPresenter> _dynamicpresenters = new ObservableCollection<IViewPresenter>();
        //[XmlIgnore]
        //[Browsable(false)]
        //public ObservableCollection<IPresenter> DynamicPresenters
        //{
        //    get { return _dynamicpresenters; }
        //    set
        //    {
        //        _dynamicpresenters = value;
        //        RaisePropertyChanged();
        //    }
        //}


        private IViewPresenter _selectedItem;
        [XmlIgnore]
        [Browsable(false)]
        public IViewPresenter SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<IViewPresenter> _presenters = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Presenters
        {
            get { return _presenters; }
            set
            {
                _presenters = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(out string message, object param)
        {
            if (param is IViewPresenter presenter)
            {
                MessageProxy.Presenter.ShowClose(presenter, null, presenter.Name);
            }
            message = null;
            return true;
        }

        public virtual bool AddPersenter(IViewPresenter presenter)
        {
            if (presenter == null) return false;
            if (this.Presenters == null) return false;
            if (this.Presenters.Contains(presenter))
            {
                this.SelectedItem = presenter;
                return false;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.Presenters.Add(presenter);
                this.SelectedItem = presenter;
            });

            return true;
        }

        public void ClearDynamic()
        {
            foreach (var item in this._dynamicpresenters)
            {
                this.Presenters.Remove(item);
            }
            this._dynamicpresenters.Clear();
        }

        public void ResetDynamic(params IViewPresenter[] presenters)
        {
            this.ClearDynamic();
            foreach (var presenter in presenters)
            {
                this._dynamicpresenters.Add(presenter);
                this.AddPersenter(presenter);
            }
        }
    }
}
