// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Systems.Component
{
    public class ComponentAttribute : Attribute
    {

        public ComponentAttribute(string displayName)
        {
            this.DisplayName = displayName;

        }
        public string DisplayName { get; set; }

        public string GroupName { get; set; }

    }
}
