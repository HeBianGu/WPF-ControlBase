// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> Mvvm绑定模型基类 </summary>
    public abstract class NotifyPropertyChanged : NotifyPropertyChangedBase
    {
        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand RelayCommand { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand LoadedCommand =>  new RelayCommand(Loaded);

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand CallMethodCommand { get; set; }


        [Browsable(false)]
        [XmlIgnore]
        public ILogService Logger => ServiceRegistry.Instance.GetInstance<ILogService>();

        protected virtual void RelayMethod(object obj)
        {

        }

        public NotifyPropertyChanged()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            CallMethodCommand = new RelayCommand(CallMethod);

            RelayMethod("init");
        }

        protected virtual void Loaded(object obj)
        {

        }

        protected virtual void CallMethod(object obj)
        {
            string methodName = obj?.ToString();

            System.Reflection.MethodInfo method = this.GetType().GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException("no found method :" + method);
            }

            object[] parameters = method.GetParameters().Select(l => l.RawDefaultValue is DBNull ? null : l.RawDefaultValue).ToArray();

            method.Invoke(this, parameters);
        }
    }
}
