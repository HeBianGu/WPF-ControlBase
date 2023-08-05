// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Service.TypeConverter;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace System
{
    [Displayer(Name = "数据列表", GroupName = SystemSetting.GroupSystem)]
    public class RepositorySetting : LazySettingInstance<RepositorySetting>
    {
        [Browsable(false)]
        public ViewType ViewType { get; set; }

        [DefaultValue(10)]
        [Display(Name = "每页显示的数据数量")]
        public int PageCount { get; set; } = 10;


        private bool _useCheckAll = true;
        /// <summary> 说明  </summary>
        [Display(Name = "启用选中全部功能")]
        public bool UseCheckAll
        {
            get { return _useCheckAll; }
            set
            {
                _useCheckAll = value;
                RaisePropertyChanged();
            }
        }


        private bool _useCheckBox = true;
        [DefaultValue(true)]
        [Display(Name = "启用勾选功能")]
        public bool UseCheckBox
        {
            get { return _useCheckBox; }
            set
            {
                _useCheckBox = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDeleteSelected = true;
        [DefaultValue(true)]
        [Display(Name = "启用删除选中项功能")]
        public bool UseDeleteSelected
        {
            get { return _useDeleteSelected; }
            set
            {
                _useDeleteSelected = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDelete = true;
        [DefaultValue(true)]
        [Display(Name = "启用删除数据功能")]
        public bool UseDelete
        {
            get { return _useDelete; }
            set
            {
                _useDelete = value;
                RaisePropertyChanged();
            }
        }


        private bool _useEdit = true;
        [DefaultValue(true)]
        [Display(Name = "启用编辑数据功能")]
        public bool UseEdit
        {
            get { return _useEdit; }
            set
            {
                _useEdit = value;
                RaisePropertyChanged();
            }
        }


        private bool _useDetial = true;
        [DefaultValue(true)]
        [Display(Name = "启用显示详情功能")]
        public bool UseDetial
        {
            get { return _useDetial; }
            set
            {
                _useDetial = value;
                RaisePropertyChanged();
            }
        }


        private bool _useAdd = true;
        [DefaultValue(true)]
        [Display(Name = "启用添加数据功能")]
        public bool UseAdd
        {
            get { return _useAdd; }
            set
            {
                _useAdd = value;
                RaisePropertyChanged();
            }
        }


        private bool _useClear = true;
        [DefaultValue(true)]
        [Display(Name = "启用清理数据功能")]
        public bool UseClear
        {
            get { return _useClear; }
            set
            {
                _useClear = value;
                RaisePropertyChanged();
            }
        }


        private bool _usePageCount = true;
        [DefaultValue(true)]
        [Display(Name = "启用设置每页显示数量")]
        public bool UsePageCount
        {
            get { return _usePageCount; }
            set
            {
                _usePageCount = value;
                RaisePropertyChanged();
            }
        }


        private bool _usePagination = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "显示分页")]
        public bool UsePagination
        {
            get { return _usePagination; }
            set
            {
                _usePagination = value;
                RaisePropertyChanged();
            }
        }


        private bool _useLoyout = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用布局样式选项")]
        public bool UseLoyout
        {
            get { return _useLoyout; }
            set
            {
                _useLoyout = value;
                RaisePropertyChanged();
            }
        }


        private bool _useTemplate = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用数据样式选项")]
        public bool UseTempate
        {
            get { return _useTemplate; }
            set
            {
                _useTemplate = value;
                RaisePropertyChanged();
            }
        }

        private bool _useGridSet = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用表格设置")]
        public bool UseGridSet
        {
            get { return _useGridSet; }
            set
            {
                _useGridSet = value;
                RaisePropertyChanged();
            }
        }

        private bool _useExportXml = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用导出Xml")]
        public bool UseExportXml
        {
            get { return _useExportXml; }
            set
            {
                _useExportXml = value;
                RaisePropertyChanged();
            }
        }

        private ItemsControlType _iemsControlType;
        /// <summary> 说明  </summary> 
        [Display(Name = "显示样式")]
        public ItemsControlType ItemsControlType
        {
            get { return _iemsControlType; }
            set
            {
                _iemsControlType = value;
                RaisePropertyChanged();
            }
        }


        private InteropMode _interopMode;
        /// <summary> 说明  </summary>
        [Display(Name = "交互方式")]
        public InteropMode InteropMode
        {
            get { return _interopMode; }
            set
            {
                _interopMode = value;
                RaisePropertyChanged();
            }
        }

        [Browsable(false)]
        [XmlIgnore]
        public ActionConfig Action { get; } = new ActionConfig();

        [Browsable(false)]
        [XmlIgnore]
        public IInterop Interop { get; set; } = new Interop();


        private string _value = @"C:\Users\LENOVO\Documents\HeBianGu\HeBianGu.Application.RepositoryWindow\LENOVO\Setting";
        /// <summary> 说明  </summary>
        [PropertyItemType(Type = typeof(ClearPathTextPropertyItem))]
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        private string _value1 = @"C:\Users11\LENOVO\Documents\HeBianGu\HeBianGu.Application.RepositoryWindow\LENOVO\Setting";
        /// <summary> 说明  </summary>
        [PropertyItemType(Type = typeof(OpenPathTextPropertyItem))]
        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged();
            }
        }

        private string _value2 = @"C:\Users11\LENOVO\Documents\HeBianGu\HeBianGu.Application.RepositoryWindow\LENOVO\Setting";
        /// <summary> 说明  </summary>
        [PropertyItemType(Type = typeof(OpenClearPathTextPropertyItem))]
        public string Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                RaisePropertyChanged();
            }
        }

    }

    public class ActionConfig
    {
        public Func<object, bool> Add { get; set; } = l =>
         {
             Diagnostics.Debug.WriteLine(l.GetType().Name);
             return true;
         };

        public Func<object, bool> Save { get; set; } = l =>
         {
             Diagnostics.Debug.WriteLine(l.GetType().Name);
             return true;
         };

        public Func<Array, bool> Delete { get; set; } = l =>
        {
            Diagnostics.Debug.WriteLine(l.GetType().Name);
            return true;
        };

        public Func<Type, Predicate<object>, IEnumerable<object>> GetAll { get; set; } = (l, k) =>
        {
            Diagnostics.Debug.WriteLine(l.GetType().Name);
            return null;
        };
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum ViewType
    {
        [Display(Name = "自定义")]
        RepositoryView = 0,
        [Display(Name = "系统默认")]
        RespositoryBox
    }
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum ItemsControlType
    {
        [Display(Name = "列表")]
        ListBox = 0,
        [Display(Name = "表格")]
        PagedDataGrid
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum InteropMode
    {
        [Display(Name = "注入仓储")]
        IRepository = 0,
        [Display(Name = "自定义行为")]
        CustomAction,
        [Display(Name = "注入互操作")]
        IInterop
    }

}
