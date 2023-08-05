// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{

    public interface IWindowComponentViewPresenter : ITreeViewPresenter, IViewPresenter
    {
        ObservableCollection<IViewPresenter> Messages { get; set; }
        ObservableCollection<IViewPresenter> SideEidts { get; set; }
        ObservableCollection<IViewPresenter> Statuses { get; set; }
        ObservableCollection<IViewPresenter> Captions { get; set; }
        ObservableCollection<IViewPresenter> Toolbars { get; set; }
        ObservableCollection<IViewPresenter> Trees { get; set; }
        ObservableCollection<IViewPresenter> SideMenus { get; set; }
        ObservableCollection<IViewPresenter> Menus { get; set; }
    }

    public interface IWindowComponentViewPresenterOption : ITreeViewItemPresenterOption
    {

    }

    public abstract class WindowComponentViewPresenter<Setting, Interface> : TreeViewItemPresenter<Setting, Interface>, IWindowComponentViewPresenter, IWindowComponentViewPresenterOption
        where Setting : class, Interface, new()
        where Interface : IViewPresenter
    {

        private IViewPresenter _persenter;
        [XmlIgnore]
        [Browsable(false)]
        public IViewPresenter Persenter
        {
            get { return _persenter; }
            set
            {
                _persenter = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IViewPresenter> _messages = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                RaisePropertyChanged("Messages");
            }
        }

        private ObservableCollection<IViewPresenter> _sideEidts = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> SideEidts
        {
            get { return _sideEidts; }
            set
            {
                _sideEidts = value;
                RaisePropertyChanged("SideEidts");
            }
        }

        private ObservableCollection<IViewPresenter> _statuses = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                RaisePropertyChanged("Statuses");
            }
        }


        private ObservableCollection<IViewPresenter> _captions = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Captions
        {
            get { return _captions; }
            set
            {
                _captions = value;
                RaisePropertyChanged("Captions");
            }
        }

        private ObservableCollection<IViewPresenter> _tolbars = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Toolbars
        {
            get { return _tolbars; }
            set
            {
                _tolbars = value;
                RaisePropertyChanged("Toolbars");
            }
        }

        private ObservableCollection<IViewPresenter> _trees = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Trees
        {
            get { return _trees; }
            set
            {
                _trees = value;
                RaisePropertyChanged("Trees");
            }
        }

        private ObservableCollection<IViewPresenter> _sideMenus = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> SideMenus
        {
            get { return _sideMenus; }
            set
            {
                _sideMenus = value;
                RaisePropertyChanged("SideMenus");
            }
        }


        private ObservableCollection<IViewPresenter> _menus = new ObservableCollection<IViewPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<IViewPresenter> Menus
        {
            get { return _menus; }
            set
            {
                _menus = value;
                RaisePropertyChanged("Menus");
            }
        }

        public virtual bool Invoke(out string message)
        {
            message = null;
            WindowContentViewPresenter.Instance.AddPersenter(this);
            WindowToolbarViewPresenter.Instance.ResetDynamic(this.Toolbars.ToArray());
            WindowSideEditViewPresenter.Instance.ResetDynamic(this.SideEidts.ToArray());
            WindowCaptionViewPresenter.Instance.ResetDynamic(this.Captions.ToArray());
            WindowMessageViewPresenter.Instance.ResetDynamic(this.Messages.ToArray());
            WindowStatusViewPresenter.Instance.ResetDynamic(this.Statuses.ToArray());
            return true;
        }
    }
}
