// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// Represents a named group of links.
    /// </summary>
    public class LinkActionGroup : NotifyPropertyChangedBase
    {

        private string displayName;
        /// <summary> 显示名称 </summary>
        public string DisplayName
        {
            get { return this.displayName; }
            set
            {
                if (this.displayName != value)
                {
                    this.displayName = value;
                    RaisePropertyChanged("DisplayName");
                }
            }
        }

        private IAction selectedLink;
        public IAction SelectedLink
        {
            get { return this.selectedLink; }
            set
            {
                if (this.selectedLink != value)
                {
                    this.selectedLink = value;
                    RaisePropertyChanged("SelectedLink");
                }
            }
        }

        private ObservableCollection<IAction> links = new ObservableCollection<IAction>();
        public ObservableCollection<IAction> Links
        {
            get { return this.links; }
            set { this.links = value; }
        }

        private string _logo;
        /// <summary> 说明  </summary>
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged("Logo");
            }
        }


        private bool _isExpanded = false;
        /// <summary> 是否展开  </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    if (value)
                    {
                        ExpandChanged?.Invoke(this);
                    }
                }

                _isExpanded = value;

                RaisePropertyChanged("IsExpanded");

            }
        }

        public event Action<LinkActionGroup> ExpandChanged;
    }

}
