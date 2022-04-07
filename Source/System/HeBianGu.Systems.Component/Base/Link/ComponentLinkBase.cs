// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Diagram;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Component
{
    public abstract class ComponentLinkBase : Action, ILinkData
    {
        [Browsable(false)]
        public string FromNodeID { get; set; }

        [Browsable(false)]
        public string ToNodeID { get; set; }

        [Browsable(false)]
        public string FromPortID { get; set; }

        [Browsable(false)]
        public string ToPortID { get; set; }


        private ObservableCollection<Mapper> _mapper = new ObservableCollection<Mapper>();
        /// <summary> 说明  </summary> 
        [XmlIgnore]
        public ObservableCollection<Mapper> Mappers
        {
            get { return _mapper; }
            set
            {
                _mapper = value;
                RaisePropertyChanged("Mappers");
            }
        }

        private ObservableCollection<string> _selectionSource = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<string> SelectionSource
        {
            get { return _selectionSource; }
            set
            {
                _selectionSource = value;
                RaisePropertyChanged("SelectionSource");
            }
        }


        public void RefreshData(INodeData fromNode, INodeData toNode)
        {
            this.Mappers.Clear();

            this.SelectionSource.Clear();

            System.Collections.Generic.List<System.Reflection.PropertyInfo> tps = toNode.GetType().GetProperties()?.ToList();

            this.SelectionSource = tps.Select(l => l.Name)?.ToObservable();

            System.Collections.Generic.List<System.Reflection.PropertyInfo> fps = fromNode.GetType().GetProperties().ToList();

            foreach (System.Reflection.PropertyInfo fp in fps)
            {
                Mapper mapper = new Mapper();

                mapper.From = fp.Name;

                System.Reflection.PropertyInfo to = tps.FirstOrDefault(l => l.PropertyType == fp.PropertyType && l.Name == fp.Name);

                mapper.To = to?.Name;

                Mappers.Add(mapper);
            }

        }
    }
}
