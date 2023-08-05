// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.App.Track
{
    internal interface IAxis
    {
        string Icon { get; set; }
        string Id { get; set; }
        string Name { get; set; } 
        double GetMax();
    }

    internal abstract class AxisBase<T> : NotifyPropertyChangedBase, IAxis where T : MaterialBase
    {
        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }
        private string _id;
        /// <summary>
        /// 素材ID
        /// </summary>
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        private string _icon;
        /// <summary> 说明  </summary>
        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        private T _selectedItem;
        /// <summary> 说明  </summary>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<T> _collection = new ObservableCollection<T>();
        /// <summary> 指令  </summary>
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        public double GetMax()
        {
            if (this.Collection == null || this.Collection.Count == 0) 
                return 0;
           return this.Collection.Max(l=>l.Start+l.Size);
        }
    }

    /// <summary> 媒体轴</summary>
    internal class MediaAxis : AxisBase<Media>
    {
        public MediaAxis()
        {
            this.Name = "媒体轴";
            this.Icon = "\xeabe";
        }
    }
    /// <summary> 指令轴</summary>
    internal class CommandAxis : AxisBase<Command>
    {
        public CommandAxis()
        {
            this.Name = "指令轴";
            this.Icon = "\xeada";
        }
        public CommandAxis(string name = "指令轴")
        {

            if (!string.IsNullOrEmpty(name))
                this.Name = name;
            else
                this.Name = "指令轴";
            this.Icon = "\xeada";
        } 
    }

    /// <summary> DMX轴</summary>
    internal class DmxAxis : AxisBase<Dmx>
    {
        public DmxAxis()
        {
            this.Name = "DMX信号";
            this.Icon = "\xeada";
        }
    }
}
