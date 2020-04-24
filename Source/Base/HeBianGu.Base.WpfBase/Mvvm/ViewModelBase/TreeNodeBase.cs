using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    public partial class TreeNodeBase<T> : NotifyPropertyChanged
    {
        public TreeNodeBase(T t)
        {
            this.Model = t;
        }

        private T _model;
        /// <summary> 节点附加实体模型 </summary>
        public T Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        public TreeNodeBase<T> TreeNodeEntity { get; set; }

        private bool _isSelected = false;

        /// <summary> 是否选中  </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                RaisePropertyChanged("IsSelected");

                this.SetParentSelected(value);

                this.SetChildSelected(value);
            }
        }


        #region - 设置是否选中 -

        /// <summary> 系统触发选中效果,不递归触发 </summary>
        void SetIsSelect(bool value)
        {
            _isSelected = value;

            RaisePropertyChanged("IsSelected");
        }

        /// <summary> 设置父节点选中状态 </summary>
        void SetParentSelected(bool value)
        {
            if (this.Parent == null) return;

            if (this.IsSelected)
            {
                //  Do ：递归设置父节点选中
                this.Parent.SetIsSelect(true);

                this.Parent.SetParentSelected(true);
            }
            else
            {

                bool isAllFalse = !this.Parent.Nodes.TrueForAll(l => l.IsSelected == false);

                this.Parent.SetIsSelect(isAllFalse);

                this.Parent.SetParentSelected(isAllFalse);
            }
        }

        /// <summary> 设置子节点状态 </summary>
        void SetChildSelected(bool value)
        {
            //  Do ：递归设置父节点选中
            this.Nodes.ForEach(l =>
            {
                l.SetIsSelect(this.IsSelected);
                l.SetChildSelected(this.IsSelected);
            });
        }
        #endregion

        #region - 设置是否可见 -

        /// <summary> 系统触发选中效果,不递归触发 </summary>
        void SetIsVisible(bool value)
        {
            _visibility = value ? Visibility.Visible : Visibility.Collapsed;

            RaisePropertyChanged("Visibility");
        }

        /// <summary> 设置父节点选中状态 </summary>
        void SetParentVisible(bool value)
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

                bool isAllFalse = !this.Parent.Nodes.TrueForAll(l => l.Visibility != Visibility.Visible);

                this.Parent.SetIsVisible(isAllFalse);

                this.Parent.SetParentVisible(isAllFalse);
            }
        }

        /// <summary> 设置子节点状态 </summary>
        void SetChildVisible(bool value)
        {
            //  Do ：递归设置父节点选中
            this.Nodes.ForEach(l =>
            {
                l.SetIsVisible(value);
                l.SetChildVisible(value);
            });
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

                this.SetParentVisible(value == Visibility.Visible);

                this.SetChildVisible(value == Visibility.Visible);
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

        /// <summary> 父节点 </summary>
        public TreeNodeBase<T> Parent { get; set; }

        /// <summary> 子节点 </summary>
        public List<TreeNodeBase<T>> Nodes { get; set; } = new List<TreeNodeBase<T>>();

        /// <summary> 添加节点 </summary>
        public void AddNode(TreeNodeBase<T> node)
        {
            node.Parent = this;

            this.Nodes.Add(node);
        }

    }
}
