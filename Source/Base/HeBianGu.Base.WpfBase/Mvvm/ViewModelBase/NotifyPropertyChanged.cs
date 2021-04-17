
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> Mvvm绑定模型基类 </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public RelayCommand LoadedCommand { get; set; }

        protected virtual void RelayMethod(object obj)
        {

        }

        public NotifyPropertyChanged()
        {
            RelayCommand = new RelayCommand(RelayMethod); 

            LoadedCommand = new RelayCommand(Loaded);

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

        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
