// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace HeBianGu.Control.PropertyGrid
{
    public class CommandPropertyItem : ObjectPropertyItem<ICommand>
    {
        public CommandPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();
            this.ReadOnly = readyOnly == null || readyOnly.IsReadOnly == false ? true : false;
            //CommandAttribute command = property.GetCustomAttribute<CommandAttribute>();
            //this.Icon = command?.Icon;
            HotKeyAttribute hotkey = property.GetCustomAttribute<HotKeyAttribute>();
            this.HotKey = hotkey?.ToString();
        }

        public string HotKey { get; set; }

        //private string _icon;
        ///// <summary> 说明  </summary>
        //public string Icon
        //{
        //    get { return _icon; }
        //    set
        //    {
        //        _icon = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private object _commandParameter;
        /// <summary> 说明  </summary>
        public object CommandParameter
        {
            get { return _commandParameter; }
            set
            {
                _commandParameter = value;
                RaisePropertyChanged();
            }
        }

    }

}
