// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System.Windows;

namespace HeBianGu.Systems.Component
{
    /// <summary>
    /// 拖拽时放置Node类型
    /// </summary>
    public class DiagramDropSystemNodeBehavior : DiagramDropBehaviorBase
    {
        protected override Node Create(INodeData nodeData)
        {
            IComponentNode componentNode = nodeData as IComponentNode;

            Node node = new Node();

            foreach (IComponentPort p in componentNode.PortDatas)
            {
                Port port = Port.Create(node);
                port.Dock = p.Dock;
                port.Visibility = Visibility.Hidden;
                port.PortType = p.PortType;
                port.Content = p;
                node.AddPort(port);
            }

            if (nodeData is ITemplate template)
            {
                template.IsTemplate = false;
            }

            return node;
        }
    }
}
