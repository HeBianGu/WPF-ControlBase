// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace System
{

    /// <summary> 应用再Mvc模式中的特性，根据此特性匹配对应的Controller名称 </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public sealed class ControllerAttribute : Attribute
    {
        public int Order { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Logo { get; set; } = "\xe7fd";

        public ControllerAttribute(string name)
        {
            this.Name = name;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RouteAttribute : Attribute
    {
        public string Path { get; set; }
        public RouteAttribute(string path)
        {
            Path = path;
        }
    }





    /// <summary> 设置只加载一次 </summary>
    public sealed class InitializeOperationAttribute : Attribute
    {

    }
}
