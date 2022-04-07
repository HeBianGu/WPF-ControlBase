// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System.Collections.Generic;
using System.Linq;
using Link = HeBianGu.Control.Diagram.Link;

namespace HeBianGu.Systems.Component
{
    /// <summary>
    /// 根据 Unit和Wire加载的数据源
    /// </summary>
    public class ComponentGraphSource : GraphSource<ComponentNode, ComponentLink>
    {
        public ComponentGraphSource(List<Node> nodeSource) : base(nodeSource)
        {

        }

        public ComponentGraphSource(IEnumerable<ComponentNode> nodes, IEnumerable<ComponentLink> links) : base(nodes, links)
        {

        }

        protected override Node ConvertToNode(ComponentNode unit)
        {
            Node node = new Node() { Content = unit };

            node.Id = unit.ID;

            node.Location = new System.Windows.Point(unit.X, unit.Y);

            foreach (IComponentPort socket in unit.PortDatas)
            {
                Port port = Port.Create(node);
                port.Id = socket.ID;
                port.Content = socket;
                port.Dock = socket.Dock;
                port.Visibility = System.Windows.Visibility.Hidden;
                port.PortType = socket.PortType;
                node.AddPort(port);
            }

            return node;
        }

        protected override Link ConvertToLink(ComponentLink wire)
        {
            Node fromNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.FromNodeID);

            Node toNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.ToNodeID);

            Port fromPort = fromNode.GetPorts().FirstOrDefault(l => l.Id == wire.FromPortID);

            Port toPort = toNode.GetPorts().FirstOrDefault(l => l.Id == wire.ToPortID);

            Link result = Link.Create(fromNode, toNode, fromPort, toPort);

            result.Content = wire;

            return result;
        }

        public override List<ComponentNode> GetNodeType()
        {
            List<ComponentNode> result = new List<ComponentNode>();

            foreach (Node node in this.NodeSource)
            {
                ComponentNode unit = node.Content as ComponentNode;

                System.Windows.Point p = NodeLayer.GetPosition(node);

                unit.X = p.X;

                unit.Y = p.Y;

                List<IComponentPort> ports = new List<IComponentPort>();

                foreach (Port port in node.GetPorts())
                {
                    ComponentPort defaultPort = port.Content as ComponentPort;

                    defaultPort.Dock = port.Dock;

                    ports.Add(defaultPort);
                }

                unit.PortDatas = ports;

                result.Add(unit);
            }

            return result;
        }

        public override List<ComponentLink> GetLinkType()
        {
            List<ComponentLink> result = new List<ComponentLink>();

            foreach (Node node in this.NodeSource)
            {
                foreach (Link link in node.LinksOutOf)
                {
                    INodeData from = link.FromNode?.Content as INodeData;
                    INodeData to = link.ToNode?.Content as INodeData;

                    ComponentLink wire = link.Content as ComponentLink;

                    //wire.RefreshData(from,to);

                    wire.FromNodeID = from?.ID;
                    wire.ToNodeID = to?.ID;

                    wire.FromPortID = (link.FromPort?.Content as IPortData)?.ID;
                    wire.ToPortID = (link.ToPort?.Content as IPortData)?.ID;

                    result.Add(wire);
                }
            }

            return result;
        }
    }



}
