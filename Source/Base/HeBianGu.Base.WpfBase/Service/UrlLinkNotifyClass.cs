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
    public class Link : NotifyPropertyChanged
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
        private Link selectedLink;
        private LinkCollection links = new LinkCollection();

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

        /// <summary>
        /// Gets or sets the selected link in this group.
        /// </summary>
        /// <value>The selected link.</value>
        internal Link SelectedLink
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

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        public LinkCollection Links
        {
            get { return this.links; }
        }

    }
}
