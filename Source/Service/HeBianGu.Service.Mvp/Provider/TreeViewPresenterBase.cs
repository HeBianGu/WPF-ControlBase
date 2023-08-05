// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{
    public interface ITreeViewPresenter : IInvokePresenter
    {
        string PredefinePath { get; set; }
    }

    public interface ITreeSettingOption : IMvpSettingOption
    {
        void AddPreDefinePath(string path);
        void AddPresenter(ITreeViewItemPresenter presenter);
        void AddHomePresenter(ITreeViewItemPresenter presenter);
    }


    public abstract class TreeViewPresenterBase<Setting, Interface> : ServiceMvpSettingBase<Setting, Interface>, ITreeSettingOption
         where Setting : class, Interface, new()
        where Interface : IViewPresenter
    {
        [XmlIgnore]
        [Browsable(false)]
        List<string> _predefinePaths = new List<string>();

        private TreeNodeBase<ITreeViewItemPresenter> _selectedItem;
        [XmlIgnore]
        [Browsable(false)]
        public TreeNodeBase<ITreeViewItemPresenter> SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private bool _isExpandAll;
        [DefaultValue(false)]
        [Displayer(Name = "全部展开")]
        public bool IsExpandAll
        {
            get { return _isExpandAll; }
            set
            {
                _isExpandAll = value;
                this.RefreshExpand();
                RaisePropertyChanged();
            }
        }

        void RefreshExpand()
        {
            foreach (var item in this.Collection)
            {
                item.IsExpanded = this.IsExpandAll;
                item.Foreach(x => x.IsExpanded = this.IsExpandAll);
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        List<ITreeViewItemPresenter> _presenters { get; set; } = new List<ITreeViewItemPresenter>();

        [XmlIgnore]
        [Browsable(false)]
        List<ITreeViewItemPresenter> _homePresenters { get; set; } = new List<ITreeViewItemPresenter>();

        public void AddPreDefinePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            if (_predefinePaths.Contains(path)) return;
            _predefinePaths.Add(path);

            Action<string> action = null;
            action = p =>
            {
                string parent = Path.GetDirectoryName(p);
                if (string.IsNullOrEmpty(parent)) return;
                if (!_predefinePaths.Contains(parent))
                    _predefinePaths.Add(parent);
                action.Invoke(parent);
            };

            action.Invoke(path);
            this.RefreshData();
        }

        public void AddHomePresenter(ITreeViewItemPresenter presenter)
        {
            if (_homePresenters.Contains(presenter)) return;
            _homePresenters.Add(presenter);
            this.RefreshData();
        }

        public void AddPresenter(ITreeViewItemPresenter presenter)
        {
            if (_presenters.Contains(presenter)) return;
            _presenters.Add(presenter);

            this.AddPreDefinePath(presenter.PredefinePath);
            this.RefreshData();
        }

        public void RefreshData()
        {
            this.SelectedItem = null;
            this.Collection.Clear();
            Action<TreeNodeBase<ITreeViewItemPresenter>, string> action = null;
            action = (n, p) =>
            {
                if (string.IsNullOrEmpty(p)) return;

                var childrenPaths = _predefinePaths.Where(x => Path.GetDirectoryName(x) == p);
                foreach (var item in childrenPaths)
                {
                    //  Do ：添加根
                    TreeViewItemPresenter treeViewPresenter = new TreeViewItemPresenter();
                    treeViewPresenter.Name = Path.GetFileName(item);
                    TreeNodeBase<ITreeViewItemPresenter> node = new TreeNodeBase<ITreeViewItemPresenter>(treeViewPresenter);
                    n.Nodes.Add(node);
                    action.Invoke(node, item);
                };


                //  Do ：添加叶
                var finds = _presenters.Where(x => x.PredefinePath == p);
                foreach (var item in finds)
                {
                    TreeNodeBase<ITreeViewItemPresenter> leaf = new TreeNodeBase<ITreeViewItemPresenter>(item);
                    n.Nodes.Add(leaf);
                    if (this.SelectedItem == null)
                    {
                        this.SelectedItem = leaf;
                    }
                }
            };

            //  Do ：添加Home
            foreach (var item in _homePresenters)
            {
                TreeNodeBase<ITreeViewItemPresenter> leaf = new TreeNodeBase<ITreeViewItemPresenter>(item);
                this.Collection.Add(leaf);

                if (this.SelectedItem == null)
                {
                    this.SelectedItem = leaf;
                }
            }

            var roots = _predefinePaths.Where(x => string.IsNullOrEmpty(Path.GetDirectoryName(x)));
            foreach (var root in roots)
            {
                //  Do ：添加根
                TreeViewItemPresenter treeViewPresenter = new TreeViewItemPresenter();
                treeViewPresenter.Name = Path.GetFileName(root);
                TreeNodeBase<ITreeViewItemPresenter> node = new TreeNodeBase<ITreeViewItemPresenter>(treeViewPresenter);
                this.Collection.Add(node);
                action.Invoke(node, root);
            }

            //  Do ：添加叶
            var rootLeafs = _presenters.Where(x => string.IsNullOrEmpty(x.PredefinePath));
            foreach (var item in rootLeafs)
            {
                TreeNodeBase<ITreeViewItemPresenter> leaf = new TreeNodeBase<ITreeViewItemPresenter>(item);
                this.Collection.Add(leaf);
            }

            this.RefreshExpand();

            if (this.SelectedItem != null)
            {
                this.SelectedItem.IsChecked = true;
                foreach (var item in this.SelectedItem.FindAllParent())
                {
                    item.IsExpanded = true;
                }
            }

            this.Invoke(out string message, this.SelectedItem?.Model);
        }

        private ObservableCollection<TreeNodeBase<ITreeViewItemPresenter>> _collection = new ObservableCollection<TreeNodeBase<ITreeViewItemPresenter>>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<TreeNodeBase<ITreeViewItemPresenter>> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }
}
