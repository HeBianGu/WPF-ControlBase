using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public class EnumProperty<T> : PropertyInfo
    {
        public EnumProperty()
        {
            var es = Enum.GetValues(typeof(T));

            this.ItemSource.Clear();

            foreach (T item in es)
            {
                this.ItemSource.Add(item);
            }
        }

        List<T> _itemsource = new List<T>();

        public List<T> ItemSource
        {
            get => _itemsource;
            set
            {
                _itemsource = value;
                RaisePropertyChanged(nameof(ItemSource));
            }
        }


        private T _selectItem;
        /// <summary> 说明  </summary>
        public T SelectItem
        {
            get { return _selectItem; }
            set
            {
                _selectItem = value;

                RaisePropertyChanged("SelectItem");

                this.Text = value?.ToString();
            }
        }

        public override void LoadDefault()
        {
            this.SelectItem = (T)Enum.Parse(typeof(T), this.Default);
        }

    }
}
