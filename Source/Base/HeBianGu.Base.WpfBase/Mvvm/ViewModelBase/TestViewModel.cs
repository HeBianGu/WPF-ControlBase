
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
       static  Random _random = new Random();

        protected override void Init()
        {
            base.Init();

            var ps = this.GetType().GetProperties();

            foreach (var item in ps)
            {
                if (item.PropertyType == typeof(double))
                {
                    if (item.CanWrite)
                    {
                        item.SetValue(this, Math.Round(_random.NextDouble() * 100,2));
                    }
                }
            }
        }
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


        private int _int1;
        /// <summary> 说明  </summary>
        public int Int1
        {
            get { return _int1; }
            set
            {
                _int1 = value;
                RaisePropertyChanged("Int1");
            }
        }

        private int _int2;
        /// <summary> 说明  </summary>
        public int Int2
        {
            get { return _int2; }
            set
            {
                _int2 = value;
                RaisePropertyChanged("Int2");
            }
        }


        private int _int3;
        /// <summary> 说明  </summary>
        public int Int3
        {
            get { return _int3; }
            set
            {
                _int3 = value;
                RaisePropertyChanged("Int3");
            }
        }



        private DateTime _datetime1;
        /// <summary> 说明  </summary>
        public DateTime DateTime1
        {
            get { return _datetime1; }
            set
            {
                _datetime1 = value;
                RaisePropertyChanged("DateTime1");
            }
        }


        private DateTime _datetime2;
        /// <summary> 说明  </summary>
        public DateTime DateTime2
        {
            get { return _datetime2; }
            set
            {
                _datetime2 = value;
                RaisePropertyChanged("DateTime2");
            }
        }

        private DateTime _datetime3;
        /// <summary> 说明  </summary>
        public DateTime DateTime3
        {
            get { return _datetime3; }
            set
            {
                _datetime3 = value;
                RaisePropertyChanged("DateTime3");
            }
        }


        private double _double1;
        /// <summary> 说明  </summary>
        public double Double1
        {
            get { return _double1; }
            set
            {
                _double1 = value;
                RaisePropertyChanged("Double1");
            }
        }

        private double _double2;
        /// <summary> 说明  </summary>
        public double Double2
        {
            get { return _double2; }
            set
            {
                _double2 = value;
                RaisePropertyChanged("Double2");
            }
        }
        private double _double3;
        /// <summary> 说明  </summary>
        public double Double3
        {
            get { return _double3; }
            set
            {
                _double3 = value;
                RaisePropertyChanged("Double3");
            }
        }
        private double _double4;
        /// <summary> 说明  </summary>
        public double Double4
        {
            get { return _double4; }
            set
            {
                _double4 = value;
                RaisePropertyChanged("Double4");
            }
        }
        private double _double5;
        /// <summary> 说明  </summary>
        public double Double5
        {
            get { return _double5; }
            set
            {
                _double5 = value;
                RaisePropertyChanged("Double5");
            }
        }
        private double _double6;
        /// <summary> 说明  </summary>
        public double Double6
        {
            get { return _double6; }
            set
            {
                _double6 = value;
                RaisePropertyChanged("Double6");
            }
        }
        private double _double7;
        /// <summary> 说明  </summary>
        public double Double7
        {
            get { return _double7; }
            set
            {
                _double7 = value;
                RaisePropertyChanged("Double7");
            }
        }
        private double _double8;
        /// <summary> 说明  </summary>
        public double Double8
        {
            get { return _double8; }
            set
            {
                _double8 = value;
                RaisePropertyChanged("Double8");
            }
        }
        private double _double9;
        /// <summary> 说明  </summary>
        public double Double9
        {
            get { return _double9; }
            set
            {
                _double9 = value;
                RaisePropertyChanged("Double9");
            }
        }
        private double _double10;
        /// <summary> 说明  </summary>
        public double Double10
        {
            get { return _double10; }
            set
            {
                _double10 = value;
                RaisePropertyChanged("Double10");
            }
        }
        private double _double11;
        /// <summary> 说明  </summary>
        public double Double11
        {
            get { return _double11; }
            set
            {
                _double11 = value;
                RaisePropertyChanged("Double11");
            }
        }
        private double _double12;
        /// <summary> 说明  </summary>
        public double Double12
        {
            get { return _double12; }
            set
            {
                _double12 = value;
                RaisePropertyChanged("Double12");
            }
        }
        private double _double13;
        /// <summary> 说明  </summary>
        public double Double13
        {
            get { return _double13; }
            set
            {
                _double13 = value;
                RaisePropertyChanged("Double13");
            }
        }
        private double _double14;
        /// <summary> 说明  </summary>
        public double Double14
        {
            get { return _double14; }
            set
            {
                _double14 = value;
                RaisePropertyChanged("Double14");
            }
        }
        private double _double15;
        /// <summary> 说明  </summary>
        public double Double15
        {
            get { return _double15; }
            set
            {
                _double15 = value;
                RaisePropertyChanged("Double15");
            }
        }

        private double _double16;
        /// <summary> 说明  </summary>
        public double Double16
        {
            get { return _double16; }
            set
            {
                _double16 = value;
                RaisePropertyChanged("Double16");
            }
        }
    }
}
