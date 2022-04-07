using HeBianGu.Base.WpfBase;
using HeBianGu.Systems.Component;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeBianGu.App.Creator
{
    public class ComponentData
    {
        public List<SerializeData> Nodes { get; set; }

        public List<SerializeData> Ports { get; set; }

        public List<SerializeData> Links { get; set; }


        public Tuple<IEnumerable<ComponentNode>, IEnumerable<ComponentLink>> GetDatas()
        {
            List<ComponentNode> nodes = new List<ComponentNode>();

            List<ComponentLink> links = new List<ComponentLink>();

            if (Nodes == null) return null;

            List<System.Reflection.Assembly> ass = ComponentService.GetAssemblys();

            ass.Add(typeof(ComponentPort).Assembly);

            List<ComponentPort> ports = new List<ComponentPort>();

            foreach (SerializeData config in Ports)
            {
                System.Reflection.Assembly a = ass.FirstOrDefault(l => l.FullName == config.Dll);

                ComponentPort c = a.CreateInstance(config.FullName) as ComponentPort;

                c.DeSerialize(config);

                ports.Add(c);
            }

            foreach (SerializeData config in Nodes)
            {
                System.Reflection.Assembly a = ass.FirstOrDefault(l => l.FullName == config.Dll);

                if (a == null) continue;

                ComponentNode node = a.CreateInstance(config.FullName) as ComponentNode;

                node.DeSerialize(config);

                nodes.Add(node);

                IEnumerable<IComponentPort> ps = ports.Where(l => l.NodeID == node.ID).ToList();

                node.PortDatas = ps.ToList();
            }

            foreach (SerializeData config in Links)
            {
                System.Reflection.Assembly a = ass.FirstOrDefault(l => l.FullName == config.Dll);

                ComponentLink c = a.CreateInstance(config.FullName) as ComponentLink;

                c.DeSerialize(config);

                ComponentNode fromNode = nodes.FirstOrDefault(l => l.ID == c.FromNodeID);

                ComponentNode toNode = nodes.FirstOrDefault(l => l.ID == c.ToNodeID);

                c.RefreshData(fromNode, toNode);

                links.Add(c);
            }

            return new Tuple<IEnumerable<ComponentNode>, IEnumerable<ComponentLink>>(nodes, links);

        }
    }



    public class ProjectData : NotifyPropertyChangedBase
    {
        private SystemSet _systemSet = new SystemSet();
        /// <summary> 说明  </summary>
        public SystemSet SystemSet
        {
            get { return _systemSet; }
            set
            {
                _systemSet = value;
                RaisePropertyChanged("SystemSet");
            }
        }


        private ComponentData _diagramData = new ComponentData();

        /// <summary> 说明  </summary>
        public ComponentData DiagramData
        {
            get { return _diagramData; }
            set
            {
                _diagramData = value;
                RaisePropertyChanged("DiagramData");
            }
        }

    }

    /// <summary> 说明</summary>
    public class SystemSet : NotifyPropertyChangedBase
    {
        private double _diagramWidth = 100;
        /// <summary> 说明  </summary>
        public double DiagramWidth
        {
            get { return _diagramWidth; }
            set
            {
                _diagramWidth = value;
                RaisePropertyChanged("DiagramWidth");
            }
        }

        private double _diagramHeight = 1000;
        /// <summary> 说明  </summary>
        public double DiagramHeight
        {
            get { return _diagramHeight; }
            set
            {
                _diagramHeight = value;
                RaisePropertyChanged("DiagramHeight");
            }
        }

        private bool _isToolboxVisible = true;
        /// <summary> 说明  </summary>
        public bool IsToolboxVisble
        {
            get { return _isToolboxVisible; }
            set
            {
                _isToolboxVisible = value;
                RaisePropertyChanged("IsToolboxVisble");
            }
        }

        private bool _isPropertyGridVisible = true;
        /// <summary> 说明  </summary>
        public bool IsPropertyGridVisible
        {
            get { return _isPropertyGridVisible; }
            set
            {
                _isPropertyGridVisible = value;
                RaisePropertyChanged("IsPropertyGridVisible");
            }
        }

        private bool _isLocatorVisible = true;
        /// <summary> 说明  </summary>
        public bool IsLocatorVisible
        {
            get { return _isLocatorVisible; }
            set
            {
                _isLocatorVisible = value;
                RaisePropertyChanged("IsLocatorVisible");
            }
        }

        private bool _isLogVisible = false;
        /// <summary> 说明  </summary>
        public bool IsLogVisible
        {
            get { return _isLogVisible; }
            set
            {
                _isLogVisible = value;
                RaisePropertyChanged("IsLogVisible");
            }
        }

    }
}
