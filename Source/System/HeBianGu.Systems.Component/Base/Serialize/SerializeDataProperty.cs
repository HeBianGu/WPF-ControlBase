// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Xml.Serialization;

namespace HeBianGu.Systems.Component
{
    [XmlType("Property")]
    public class SerializeDataProperty
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Value { get; set; }

    }

}
