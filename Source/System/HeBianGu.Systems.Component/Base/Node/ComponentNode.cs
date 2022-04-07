// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Component
{
    public class ComponentNode : ComponentNodeBase, IComponentNode, ITemplate
    {
        public ComponentNode()
        {
            this.InitPort();
        }

        private bool _isTemplate = true;

        /// <summary>
        /// 标识是否是模板
        /// </summary>
        [Browsable(false)]
        public bool IsTemplate
        {
            get { return _isTemplate; }
            set
            {
                _isTemplate = value;
                RaisePropertyChanged("IsTemplate");
            }
        }

        public override IActionResult Invoke(IAction previors)
        {
            throw new NotImplementedException();
        }


        protected virtual IActionResult OK(params IComponentPort[] ports)
        {
            return new NodeActionResult() { State = ResultState.OK, Ports = ports.Count() == 0 ? this.PortDatas : ports?.ToList() };
        }

        protected virtual IActionResult Bad(params IComponentPort[] ports)
        {
            return new NodeActionResult() { State = ResultState.Bad, Ports = ports.Count() == 0 ? this.PortDatas : ports?.ToList() };
        }

        protected virtual IActionResult Error(params IComponentPort[] ports)
        {
            return new NodeActionResult() { State = ResultState.Error, Ports = ports.Count() == 0 ? this.PortDatas : ports?.ToList() };
        }

        protected virtual void InitPort()
        {
            Array vs = Enum.GetValues(typeof(Dock));

            foreach (object v in vs)
            {
                ComponentPort port = new ComponentPort(this.ID);
                port.Dock = (Dock)v;
                port.PortType = PortType.Both;
                this.PortDatas.Add(port);
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public List<IComponentPort> PortDatas { get; set; } = new List<IComponentPort>();
    }

}
