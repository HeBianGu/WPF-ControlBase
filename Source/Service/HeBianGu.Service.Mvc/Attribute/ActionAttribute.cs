// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace System
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]

    public sealed class ActionAttribute : Attribute
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Logo { get; set; } = "\xe7fd";

        public int Order { get; set; }
    }
}
