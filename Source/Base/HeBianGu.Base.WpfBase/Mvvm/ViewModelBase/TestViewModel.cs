
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
    /// <summary> 开发原型用的模型 </summary>
    public partial class TestViewModel : SelectViewModel<DateTime> 
    {

        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


        private string _value1;
        /// <summary> 说明  </summary>
        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged("Value1");
            }
        }


        private string _value2;
        /// <summary> 说明  </summary>
        public string Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                RaisePropertyChanged("Value2");
            }
        }


        private string _value3;
        /// <summary> 说明  </summary>
        public string Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
                RaisePropertyChanged("Value3");
            }
        }


        private string _value4;
        /// <summary> 说明  </summary>
        public string Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
                RaisePropertyChanged("Value4");
            }
        }


        private string _value5;
        /// <summary> 说明  </summary>
        public string Value5
        {
            get { return _value5; }
            set
            {
                _value5 = value;
                RaisePropertyChanged("Value5");
            }
        }


        private string _value6;
        /// <summary> 说明  </summary>
        public string Value6
        {
            get { return _value6; }
            set
            {
                _value6 = value;
                RaisePropertyChanged("Value6");
            }
        }


        private string _value7;
        /// <summary> 说明  </summary>
        public string Value7
        {
            get { return _value7; }
            set
            {
                _value7 = value;
                RaisePropertyChanged("Value7");
            }
        }


        private string _value9;
        /// <summary> 说明  </summary>
        public string Value9
        {
            get { return _value9; }
            set
            {
                _value9 = value;
                RaisePropertyChanged("Value9");
            }
        }
    }
}
