// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;
using System.Xml;
using System.Windows.Controls;
using HeBianGu.Service.TypeConverter;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Control.Filter
{


    [Displayer(Name = "设置条件", Icon = IconAll.Filter)]
    public class PropertyConfidtionPrensenter : DisplayerViewModelBase, IConditionable, IMetaSettingSerilize, IMetaSetting
    {
        public PropertyConfidtionPrensenter()
        {

        }

        public PropertyConfidtionPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            var ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
            if (predicate != null)
                this.Properties = ps.Where(predicate).ToObservable();
            else
                this.Properties = ps.ToObservable();
        }

        private ObservableCollection<IPropertyConfidtion> _conditions = new ObservableCollection<IPropertyConfidtion>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IPropertyConfidtion> Conditions
        {
            get { return _conditions; }
            set
            {
                _conditions = value;
                RaisePropertyChanged();
            }
        }

        private ConditionOperate _conditionOperate = ConditionOperate.All;
        /// <summary> 说明  </summary>
        public ConditionOperate ConditionOperate
        {
            get { return _conditionOperate; }
            set
            {
                _conditionOperate = value;
                RaisePropertyChanged();
            }
        }


        [XmlIgnore]
        public RelayCommand AddConditionCommand => new RelayCommand(l =>
        {
            var first = this.Properties.FirstOrDefault();
            PropertyConfidtion confidtion = new PropertyConfidtion(first);
            confidtion.Filter.IsSelected = true;
            this.Conditions.Add(confidtion);
        });

        [XmlIgnore]
        public RelayCommand ClearConditionCommand => new RelayCommand(l =>
        {
            this.Conditions.Clear();
        }, l => this.Conditions.Count > 0);

        [XmlIgnore]
        public RelayCommand SaveCommand => new RelayCommand(l =>
        {
            this.Save();
        });

        private ObservableCollection<PropertyInfo> _properties = new ObservableCollection<PropertyInfo>();
        [XmlIgnore]
        public ObservableCollection<PropertyInfo> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                RaisePropertyChanged();
            }
        }

        public bool IsMatch(object obj)
        {
            if (this.ConditionOperate == ConditionOperate.All)
                return this.Conditions.All(x => x.IsMatch(obj));
            if (this.ConditionOperate == ConditionOperate.Any)
                return this.Conditions.Any(x => x.IsMatch(obj));
            if (this.ConditionOperate == ConditionOperate.AnyNot)
                return this.Conditions.Any(x => !x.IsMatch(obj));
            return !this.Conditions.All(x => x.IsMatch(obj));
        }

        [XmlIgnore]
        public IMetaSettingService MetaSettingService => ServiceRegistry.Instance.GetInstance<IMetaSettingService>();

        public void Save()
        {
            if (string.IsNullOrEmpty(this.ID))
                return;
            this.MetaSettingService?.Serilize(this, this.ID);

        }

        bool _isLoaded = false;
        public void Load()
        {
            if (_isLoaded)
                return;
            if (string.IsNullOrEmpty(this.ID))
                return;
            var find = this.MetaSettingService?.Deserilize<PropertyConfidtionPrensenter>(this.ID);
            if (find == null)
                return;

            foreach (var item in find.Conditions)
            {
                var propertyInfo = this.Properties.FirstOrDefault(x => x.Name == item.Filter.Name);
                var pc = new PropertyConfidtion();
                item.Filter.PropertyInfo = propertyInfo;
                pc.Filter = item.Filter;
                this.Conditions.Add(pc);
            }
            this.ConditionOperate = find.ConditionOperate;
            _isLoaded = true;
        }
    }

    public class PropertyConfidtionsPrensenter : DisplayerViewModelBase, IConditionable, IMetaSettingSerilize, IMetaSetting
    {
        public PropertyConfidtionsPrensenter()
        {

        }

        public PropertyConfidtionsPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            var ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
            if (predicate != null)
                this.Properties = ps.Where(predicate).ToObservable();
            else
                this.Properties = ps.ToObservable();
        }

        private ObservableCollection<PropertyInfo> _properties = new ObservableCollection<PropertyInfo>();
        [XmlIgnore]
        public ObservableCollection<PropertyInfo> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PropertyConfidtionPrensenter> _propertyConfidtions = new ObservableCollection<PropertyConfidtionPrensenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<PropertyConfidtionPrensenter> PropertyConfidtions
        {
            get { return _propertyConfidtions; }
            set
            {
                _propertyConfidtions = value;
                RaisePropertyChanged();
            }
        }

        private PropertyConfidtionPrensenter _selectedItem;
        [XmlIgnore]
        public PropertyConfidtionPrensenter SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        private int _selectedIndex;
        /// <summary> 说明  </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand AddCommand => new RelayCommand(l =>
        {
            var item = new PropertyConfidtionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            item.Properties = this.Properties;
            item.Load();
            this.PropertyConfidtions.Add(item);
        });


        public RelayCommand ClearSelectionCommand => new RelayCommand(l =>
        {
            this.SelectedItem = null;
            this.Save();
        }, x => this.SelectedItem != null);

        public bool IsMatch(object obj)
        {
            if (this.SelectedItem == null)
                return true;
            return this.SelectedItem.IsMatch(obj);
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(this.ID))
                return;
            this.SelectedIndex = this.PropertyConfidtions.IndexOf(this.SelectedItem);
            this.MetaSettingService?.Serilize(this, this.ID);
        }

        [XmlIgnore]
        public IMetaSettingService MetaSettingService => ServiceRegistry.Instance.GetInstance<IMetaSettingService>();

        bool _loaded = false;
        public void Load()
        {
            if (_loaded == true)
                return;
            var find = this.MetaSettingService?.Deserilize<PropertyConfidtionsPrensenter>(this.ID);
            if (find == null)
                return;
            this.PropertyConfidtions = find.PropertyConfidtions;
            if (find.SelectedIndex >= 0)
                this.SelectedItem = find.PropertyConfidtions[find.SelectedIndex];

            foreach (var item in find.PropertyConfidtions)
            {
                item.Properties = this.Properties;
                foreach (var confidtion in item.Conditions)
                {
                    var propertyInfo = item.Properties.FirstOrDefault(x => x.Name == confidtion.Filter.Name);
                    confidtion.Filter.PropertyInfo = propertyInfo;
                }
            }
            _loaded = true;
        }
    }

    public interface IPropertyConditonable : IConditionable
    {
        event EventHandler<IConditionable> ConditionChanged;
    }

    [Displayer(Name = "Filter")]
    public class ButtonPropertyConfidtionsPrensenter : DisplayerViewModelBase, IConditionable, IPropertyConditonable
    {
        private readonly PropertyConfidtionsPrensenter _propertyConfidtions;
        public PropertyConfidtionsPrensenter PropertyConfidtions => _propertyConfidtions;
        public ButtonPropertyConfidtionsPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            this._propertyConfidtions = new PropertyConfidtionsPrensenter(modelTyle, predicate) { ID = modelTyle.Name };
            this._propertyConfidtions.Load();
        }

        public RelayCommand FilterCommand => new RelayCommand(async l =>
        {
            var r = await MessageProxy.Presenter.Show(_propertyConfidtions, null, this.Name);
            if (r == true)
            {
                _propertyConfidtions.Save();
                //this.Sessions = this._cache.Where(x => this._propertyConfidtions.IsMatch(x)).ToObservable();
                this.OnConditionChanged();
                MessageProxy.Snacker.ShowTime("保存成功");
            }
        });

        public RelayCommand FilterChangedCommand => new RelayCommand(l =>
        {
            if (l is Popup popup && popup.IsOpen == false)
                return;
            this.OnConditionChanged();
            //this.Sessions = this._cache.Where(x => this._propertyConfidtions.IsMatch(x)).ToObservable();
            this._propertyConfidtions.Save();
        });

        public bool IsMatch(object obj)
        {
            return this._propertyConfidtions.IsMatch(obj);
        }

        protected void OnConditionChanged()
        {
            this.ConditionChanged?.Invoke(this, this);
        }

        public event EventHandler<IConditionable> ConditionChanged;
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum ConditionOperate
    {
        [Display(Name = "满足所有条件")]
        All = 0,
        [Display(Name = "满足任一条件")]
        Any,
        [Display(Name = "任一条件不满足")]
        AnyNot,
        [Display(Name = "全部不满足条件")]
        None
    }

    public interface IPropertyConfidtion : IConditionable
    {
        IPropertyFilter Filter { get; set; }
    }

    public class PropertyConfidtion : NotifyPropertyChanged, IConditionable, IPropertyConfidtion
    {
        public PropertyConfidtion()
        {

        }

        public PropertyConfidtion(PropertyInfo propertyInfo)
        {
            this.Filter = FilterFactory.Create(propertyInfo, null) as IPropertyFilter;
        }

        private IPropertyFilter _filter;
        public IPropertyFilter Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                RaisePropertyChanged();
            }
        }

        [XmlIgnore]
        public RelayCommand SelectionChangedCommand => new RelayCommand(l =>
        {
            if (l is SelectionChangedEventArgs arg)
            {
                if (arg.AddedItems[0] is PropertyInfo info)
                    this.Filter = FilterFactory.Create(info, null) as IPropertyFilter;

            }
        });

        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            return this.Filter.IsMatch(obj);
        }
    }
}
