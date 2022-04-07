// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System.ComponentModel;
using System.Windows.Controls;

namespace HeBianGu.Systems.Component
{
    public abstract class ComponentPortBase : Action, IPortData
    {
        public ComponentPortBase()
        {

        }

        public ComponentPortBase(string nodeID)
        {
            this.NodeID = nodeID;
        }

        [Browsable(false)]
        public Dock Dock { get; set; }

        [Browsable(false)]
        public string NodeID { get; set; }

        public PortType PortType { get; set; }
    }
}
