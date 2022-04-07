// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System.Linq;
using System.Windows.Controls;

namespace HeBianGu.Systems.Component
{
    public class If : ComponentNode
    {
        protected override void InitPort()
        {

            {
                ComponentPort port = new ComponentPort(this.ID);
                port.Dock = Dock.Top;
                port.PortType = PortType.Input;
                this.PortDatas.Add(port);
            }

            {
                ComponentPort port = new ComponentPort(this.ID);
                port.Dock = Dock.Left;
                port.DisplayName = "是";
                port.PortType = PortType.OutPut;
                this.PortDatas.Add(port);
            }

            {
                ComponentPort port = new ComponentPort(this.ID);
                port.Dock = Dock.Right;
                port.DisplayName = "否";
                port.PortType = PortType.OutPut;
                this.PortDatas.Add(port);
            }
        }

        protected IActionResult True(params IPortData[] ports)
        {
            return OK(this.PortDatas.FirstOrDefault(l => l.DisplayName == "是"));
        }

        protected IActionResult False(params IPortData[] ports)
        {
            return OK(this.PortDatas.FirstOrDefault(l => l.DisplayName == "否"));
        }
    }
}
