// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Repository
{
    public interface IRepositoryViewPresenter : ITreeViewItemPresenter
    {

    }

    public interface IRepositoryPresenterOption : ITreeViewItemPresenterOption
    {

    }


    public abstract class RepositoryViewPresenterBase<Setting, Interface> : TreeViewItemPresenter<Setting, Interface>, IRepositoryViewPresenter
          where Setting : class, Interface, new()
        where Interface : IViewPresenter
    {
        private IRepositoryViewModel _viewModel;
        [XmlIgnore]
        [Browsable(false)]
        public IRepositoryViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                RaisePropertyChanged();
            }
        }

        private bool _useCheckAll;
        [DefaultValue(true)]
        [Display(Name = "启用全选")]
        public bool UseCheckAll
        {
            get { return _useCheckAll; }
            set
            {
                _useCheckAll = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDelete;
        [DefaultValue(true)]
        [Display(Name = "启用删除")]
        public bool UseDelete
        {
            get { return _useDelete; }
            set
            {
                _useDelete = value;
                RaisePropertyChanged();
            }
        }


        private bool _useEdit;
        [DefaultValue(true)]
        [Display(Name = "启用编辑")]
        public bool UseEdit
        {
            get { return _useEdit; }
            set
            {
                _useEdit = value;
                RaisePropertyChanged();
            }
        }

        private bool _useView;
        [DefaultValue(true)]
        [Display(Name = "启用查看")]
        public bool UseView
        {
            get { return _useView; }
            set
            {
                _useView = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDeleteChecked;
        [DefaultValue(true)]
        [Display(Name = "启用删除选中")]
        public bool UseDeleteChecked
        {
            get { return _useDeleteChecked; }
            set
            {
                _useDeleteChecked = value;
                RaisePropertyChanged();
            }
        }


        private bool _useExport;
        [DefaultValue(true)]
        [Display(Name = "启用导出")]
        public bool UseExport
        {
            get { return _useExport; }
            set
            {
                _useExport = value;
                RaisePropertyChanged();
            }
        }


        private bool _useClear;
        [DefaultValue(true)]
        [Display(Name = "启用清空")]
        public bool UseClear
        {
            get { return _useClear; }
            set
            {
                _useClear = value;
                RaisePropertyChanged();
            }
        }


        private bool _useAdd;
        [DefaultValue(true)]
        [Display(Name = "启用新增")]
        public bool UseAdd
        {
            get { return _useAdd; }
            set
            {
                _useAdd = value;
                RaisePropertyChanged();
            }
        }


        private bool _useSearch;
        [DefaultValue(true)]
        [Display(Name = "启用搜索")]
        public bool UseSearch
        {
            get { return _useSearch; }
            set
            {
                _useSearch = value;
                RaisePropertyChanged();
            }
        }


        private bool _useLayout;
        [DefaultValue(true)]
        [Display(Name = "启用搜索")]
        public bool UseLayout
        {
            get { return _useLayout; }
            set
            {
                _useLayout = value;
                RaisePropertyChanged();
            }
        }


        private bool _usePageCount;
        [DefaultValue(true)]
        [Display(Name = "启用搜索")]
        public bool UsePageCount
        {
            get { return _usePageCount; }
            set
            {
                _usePageCount = value;
                RaisePropertyChanged();
            }
        }


        private int _pageCount;
        [DefaultValue(15)]
        [Display(Name = "每页显示的数量")]
        public int PageCount
        {
            get { return _pageCount; }
            set
            {
                _pageCount = value;
                RaisePropertyChanged();
            }
        }

        private RepositoryDiplayMode _displayMode;
        [DefaultValue(RepositoryDiplayMode.DataGrid)]
        [Display(Name = "呈现的样式")]
        public RepositoryDiplayMode DisplayMode
        {
            get { return _displayMode; }
            set
            {
                _displayMode = value;
                RaisePropertyChanged();
            }
        }


        private int _layoutSelectedIndex;
        [DefaultValue(0)]
        [Display(Name = "布局方式")]
        public int LayoutSelectedIndex
        {
            get { return _layoutSelectedIndex; }
            set
            {
                _layoutSelectedIndex = value;
                RaisePropertyChanged();
            }
        }

    }

    public class RepositoryViewPresenter : RepositoryViewPresenterBase<RepositoryViewPresenter, IRepositoryViewPresenter>, IRepositoryPresenterOption
    {

    }

    public class RepositoryViewPresenter<Setting, Interface> : RepositoryViewPresenter
        where Setting : class, Interface, new()
        where Interface : IViewPresenter
    {
        public new static Setting Instance => ServiceRegistry.Instance.GetInstance<Interface>() as Setting;

    }

    public enum RepositoryDiplayMode
    {
        DataGrid, ListBox
    }
}
