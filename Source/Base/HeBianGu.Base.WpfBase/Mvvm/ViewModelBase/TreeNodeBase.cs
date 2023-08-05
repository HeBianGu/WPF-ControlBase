// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public interface ITreeNode
    {
        bool IsExpanded { get; set; }
        bool? IsChecked { get; set; }
        Visibility Visibility { get; set; }
    }

    public partial class TreeNodeBase<T> : SelectViewModel<T>, ITreeNode, ISearchable
    {
        public TreeNodeBase(T t) : base(t)
        {

        }

        public TreeNodeBase<T> TreeNodeEntity { get; set; }

        #region - 设置是否选中 -

        private bool? _isChecked = false;
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
                this.RefreshParentCheckState();
                this.RefreshChildrenCheckState();
            }
        }

        private void RefreshParentCheckState()
        {
            if (this.Parent == null)
                return;

            bool allChecked = this.Parent.Nodes.All(l => l.IsChecked == true);
            if (allChecked)
            {
                this.Parent.CheckOnlyCurrent(true);
                this.Parent.RefreshParentCheckState();
                return;
            }

            bool allUnChecked = this.Parent.Nodes.All(l => l.IsChecked == false);
            if (allUnChecked)
            {
                this.Parent.CheckOnlyCurrent(false);
                this.Parent.RefreshParentCheckState();
                return;
            }

            this.Parent.CheckOnlyCurrent(null);
            this.Parent.RefreshParentCheckState();

        }

        private void RefreshChildrenCheckState()
        {
            foreach (var item in this.Nodes)
            {
                item.CheckOnlyCurrent(this.IsChecked);
                item.RefreshChildrenCheckState();
            }
        }

        private void CheckOnlyCurrent(bool? value)
        {
            _isChecked = value;
            RaisePropertyChanged("IsChecked");
        }
        #endregion

        #region - 设置是否可见 -

        /// <summary> 系统触发选中效果,不递归触发 </summary>
        private void SetIsVisible(bool value)
        {
            _visibility = value ? Visibility.Visible : Visibility.Collapsed;
            RaisePropertyChanged("Visibility");
        }

        /// <summary> 设置父节点选中状态 </summary>
        private void SetParentVisible(bool value)
        {
            if (this.Parent == null) return;

            if (this.Visibility == Visibility.Visible)
            {
                //  Do ：递归设置父节点选中
                this.Parent.SetIsVisible(true);
                this.Parent.SetParentVisible(true);
            }
            else
            {

                bool isAllFalse = !this.Parent.Nodes.All(l => l.Visibility != Visibility.Visible);
                this.Parent.SetIsVisible(isAllFalse);
                this.Parent.SetParentVisible(isAllFalse);
            }
        }

        /// <summary> 设置子节点状态 </summary>
        private void SetChildVisible(bool value)
        {
            //  Do ：递归设置父节点选中
            foreach (var item in this.Nodes)
            {
                item.SetIsVisible(value);
                item.SetChildVisible(value);
            }
        }
        #endregion

        private Visibility _visibility;

        /// <summary> 是否可见  </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visibility");
                //this.SetParentVisible(value == Visibility.Visible);
                //this.SetChildVisible(value == Visibility.Visible);
            }
        }

        private bool _isExpanded;
        /// <summary> 是否展开  </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                RaisePropertyChanged("IsExpanded");
            }
        }

        public TreeNodeBase<T> Parent { get; set; }

        private ObservableCollection<TreeNodeBase<T>> _nodes = new ObservableCollection<TreeNodeBase<T>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeBase<T>> Nodes
        {
            get { return _nodes; }
            set
            {
                _nodes = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 添加节点 </summary>
        public void AddNode(TreeNodeBase<T> node)
        {
            node.Parent = this;
            this.Nodes.Add(node);
        }

        public void Foreach(Action<TreeNodeBase<T>> action)
        {
            foreach (var node in this.Nodes)
            {
                action?.Invoke(node);
                node.Foreach(action);
            }
        }

        public IEnumerable<TreeNodeBase<T>> FindAll(Predicate<TreeNodeBase<T>> action = null)
        {
            foreach (var node in this.Nodes)
            {
                if (action?.Invoke(node) != false)
                    yield return node;
                var finds = node.FindAll(action);

                foreach (var item in finds)
                {
                    yield return item;
                }
            }
        }


        public IEnumerable<TreeNodeBase<T>> FindAllParent(Predicate<TreeNodeBase<T>> action = null)
        {
            if (this.Parent != null)
            {
                if (action?.Invoke(this.Parent) != false)
                    yield return Parent;
                this.Parent.FindAllParent(action);
            }
        }

        public override bool Filter(string txt)
        {
            foreach (var item in this.Nodes)
            {
                item.Filter(txt);
            }
            var r = this.Nodes.Any(x => x.Visibility == Visibility.Visible) || base.Filter(txt);
            this.Visibility = r ? Visibility.Visible : Visibility.Collapsed;
            return r;
        }
    }
}
