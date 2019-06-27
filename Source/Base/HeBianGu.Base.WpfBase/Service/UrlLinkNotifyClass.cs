using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{

    /// <summary> 连接绑定集合 </summary>
    public class LinkCollection : ObservableCollection<Link>
    {
      
        public LinkCollection()
        {

        }

        public LinkCollection(IEnumerable<Link> links)
        {
            if (links == null)
            {
                throw new ArgumentNullException("links");
            }
            foreach (var link in links)
            {
                Add(link);
            }
        }
    }

    /// <summary> 连接绑定对象 </summary>
    public class Link : LinkBase
    {
        private Link _leftLink;
        /// <summary> 连接地址 </summary>
        public Link LeftLink
        {
            get { return this._leftLink; }
            set
            {
                if (this._leftLink != value)
                {
                    this._leftLink = value;
                    RaisePropertyChanged("Source");
                }
            }
        }


        private Link _rightLink;
        /// <summary> 说明  </summary>
        public Link RightLink
        {
            get { return _rightLink; }
            set
            {
                _rightLink = value;
                RaisePropertyChanged("RightLink");
            }
        }


        private Link _topLink;
        /// <summary> 说明  </summary>
        public Link TopLink
        {
            get { return _topLink; }
            set
            {
                _topLink = value;
                RaisePropertyChanged("TopLink");
            }
        }


        private Link _bottomLink;
        /// <summary> 说明  </summary>
        public Link BottomLink
        {
            get { return _bottomLink; }
            set
            {
                _bottomLink = value;
                RaisePropertyChanged("BottomLink");
            }
        }


    }
    /// <summary> 连接绑定对象基类 </summary>
    public class LinkBase : NotifyPropertyChanged
    {
        private Uri source;
        /// <summary> 连接地址 </summary>
        public Uri Source
        {
            get { return this.source; }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    RaisePropertyChanged("Source");
                }
            }
        }

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


    }

    /// <summary>
    /// Represents a named group of links.
    /// </summary>
    public class LinkGroup : NotifyPropertyChanged
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

        private string groupKey;
        public string GroupKey
        {
            get { return this.groupKey; }
            set
            {
                if (this.groupKey != value)
                {
                    this.groupKey = value;
                    RaisePropertyChanged("GroupKey");
                }
            }
        }

        private Link selectedLink;
        public Link SelectedLink
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

        private LinkCollection links = new LinkCollection();
        public LinkCollection Links
        {
            get { return this.links; }
            set { this.links=value; }
        }

    }


   
}
