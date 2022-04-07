// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;

namespace HeBianGu.Service.Mvc
{
    public abstract class MvcEntityViewModelBase<M> : MvcViewModelBase, IMvcEntityViewModelBase<M>
    {
        private ObservableCollection<M> _collection = new ObservableCollection<M>();
        /// <summary> 实体集合  </summary>
        public ObservableCollection<M> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private M _addItem;
        /// <summary> 要添加的实体  </summary>
        public M AddItem
        {
            get { return _addItem; }
            set
            {
                _addItem = value;
                RaisePropertyChanged("AddItem");
            }
        }

        private M _selectedItem;
        /// <summary> 选中的实体  </summary>
        public M SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcEntityViewModelBase<R, M> : MvcEntityViewModelBase<M> where M : new()
    {
        public R Respository { get; set; } = ServiceRegistry.Instance.GetInstance<R>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcEntityViewModelBase<R1, R2, M> : MvcEntityViewModelBase<R1, M> where M : new()
    {
        public R2 Service1 { get; set; } = ServiceRegistry.Instance.GetInstance<R2>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcEntityViewModelBase<R1, R2, R3, M> : MvcEntityViewModelBase<R1, R2, M> where M : new()
    {
        public R3 Service2 { get; set; } = ServiceRegistry.Instance.GetInstance<R3>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcEntityViewModelBase<R1, R2, R3, R4, M> : MvcEntityViewModelBase<R1, R2, R3, M> where M : new()
    {
        public R4 Service3 { get; set; } = ServiceRegistry.Instance.GetInstance<R4>();
    }

    /// <summary> 带有依赖注入Respository的基类 </summary>
    public class MvcEntityViewModelBase<R1, R2, R3, R4, R5, M> : MvcEntityViewModelBase<R1, R2, R3, R4, M> where M : new()
    {
        public R5 Service4 { get; set; } = ServiceRegistry.Instance.GetInstance<R5>();
    }
}
