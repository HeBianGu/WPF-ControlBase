// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;

namespace HeBianGu.Base.WpfBase
{
    public class LinkActionGroups : ObservableCollection<LinkActionGroup>
    {
        public LinkActionGroups(bool isAutoExpand)
        {
            if (isAutoExpand)
                this.CollectionChanged += LinkActionGroups_CollectionChanged;
        }


        private void LinkActionGroups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LinkActionGroup item in e.NewItems)
                {
                    item.ExpandChanged -= Item_ExpandChanged;
                    item.ExpandChanged += Item_ExpandChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (LinkActionGroup item in e.OldItems)
                {
                    item.ExpandChanged -= Item_ExpandChanged;
                }
            }
        }

        private void Item_ExpandChanged(LinkActionGroup obj)
        {
            foreach (LinkActionGroup item in this)
            {
                if (item == obj) continue;

                item.IsExpanded = false;
            }
        }
    }
}
