using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class StepState : ListBox
    {
        static StepState()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepState), new FrameworkPropertyMetadata(typeof(StepState)));
        }
    }

    public class StepItemStateTemplateSelector : DataTemplateSelector
    {
        public DataTemplate UnkownTempate { get; set; }

        public DataTemplate RunningTempate { get; set; }

        public DataTemplate ErrorTempate { get; set; }

        public DataTemplate SuccessTempate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
           if(item is StepItem stepItem)
            {
                if (stepItem.State == -1) return ErrorTempate;

                if (stepItem.State == 0) return UnkownTempate;

                if (stepItem.State == 1) return RunningTempate;

                if (stepItem.State == 2) return SuccessTempate;


                throw new ArgumentException("未识别状态"+ stepItem.State);
            }
            else
            {

                throw new ArgumentException("参数类型不对"+ item.GetType());
            }
        }
    }

    /// <summary> 说明</summary>
    public class StepItem : NotifyPropertyChanged
    {
        #region - 属性 -


        private string _displayName;
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }


        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }


        private int _percent;
        /// <summary> 说明  </summary>
        public int Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                RaisePropertyChanged("Percent");
            }
        }



        private int _state=0;
        /// <summary> -1 错误 0 未开始 1 正在运行 2 运行成功  </summary>
        public int State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion
    }


}
