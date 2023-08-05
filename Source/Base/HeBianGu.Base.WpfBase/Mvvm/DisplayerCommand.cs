// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    //public class CommandAttribute : Attribute
    //{
    //    /// <summary>
    //    /// 命令的类型
    //    /// </summary>
    //    public string Group { get; set; }
    //    public string ID { get; set; }
    //    public string Icon { get; set; }
    //}

    public class DisplayerCommand : RelayCommand
    {
        public DisplayerCommand(Action<object> action) : base(action)
        {

        }

        public DisplayerCommand(Action<IRelayCommand, object> action) : base(action)
        {

        }

        public DisplayerCommand(Action<object> execute, Predicate<object> canExecute) : base(execute, canExecute)
        {

        }

        public DisplayerCommand(Action<IRelayCommand, object> execute, Func<IRelayCommand, object, bool> canExecute) : base(execute, canExecute)
        {

        }

        private string _icon;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        private string _groupName;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }


        private int _order;
        [XmlIgnore]
        [Browsable(false)]
        public virtual int Order
        {
            get { return _order; }
            set
            {
                _order = value;
                RaisePropertyChanged();
            }
        }

    }
}
