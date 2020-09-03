using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class RangeProperyBase<T> : PropertyInfo<T>
    {
        T _max;
        public T Max
        {
            get => _max;
            set
            {
                _max = value;
                RaisePropertyChanged();
            }
        }

        T _min;
        public T Min
        {
            get => _min;
            set
            {
                _min = value;
                RaisePropertyChanged();
            }
        }


        T _step;
        public T Step
        {
            get => _step;
            set
            {
                _step = value;
                RaisePropertyChanged();
            }
        }
    }
}
