// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;
using System.Xml.Serialization;

namespace HeBianGu.Systems.Component
{
    public class SerializeData
    {
        [XmlAttribute]
        public string Class { get; set; }

        [XmlAttribute]
        public string Component { get; set; }

        [XmlAttribute]
        public string FullName { get; set; }

        [XmlAttribute]
        public string Dll { get; set; }

        public List<SerializeDataProperty> Propertys { get; set; } = new List<SerializeDataProperty>();
    }

}
