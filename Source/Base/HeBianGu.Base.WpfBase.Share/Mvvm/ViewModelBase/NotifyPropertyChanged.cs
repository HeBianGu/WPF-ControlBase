
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public RelayCommand LoadedCommand { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand CallMethodCommand { get; set; }


        [Browsable(false)]
        [XmlIgnore]
        public ILogService Logger =>ServiceRegistry.Instance.GetInstance<ILogService>();

        protected virtual void RelayMethod(object obj)
        {

        }

        public NotifyPropertyChanged()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            LoadedCommand = new RelayCommand(Loaded);

            CallMethodCommand = new RelayCommand(CallMethod);

            RelayMethod("init");

            Init();


        }

        /// <summary> 初始化方法 </summary>
        protected virtual void Init()
        {

        }

        /// <summary> 加载方法 </summary>
        protected virtual void Loaded(object obj)
        {

        }

        /// <summary> 调用当前类指定方法 </summary>
        protected virtual void CallMethod(object obj)
        {
            string methodName = obj?.ToString();

            var method = this.GetType().GetMethod(methodName);

            if (method == null)
            {
                throw new ArgumentException("no found method :" + method);
            }

            var parameters = method.GetParameters().Select(l => l.RawDefaultValue is DBNull ? null : l.RawDefaultValue).ToArray();

            method.Invoke(this, parameters);
        }
    }
}
