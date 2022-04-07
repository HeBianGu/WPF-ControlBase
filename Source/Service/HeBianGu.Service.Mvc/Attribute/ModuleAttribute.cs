// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Service.Mvc
{

    /// <summary> 应用再Mvc模式中的特性，根据此特性匹配对应的Controller名称 </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class ModuleAttribute : Attribute
    {
        public string Name { get; set; }

        public int Order { get; set; }
    }
}
