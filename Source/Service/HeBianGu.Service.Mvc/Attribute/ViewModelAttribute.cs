// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace System
{
    /// <summary> 应用再Mvc中的特性，根据此特性查找ViewModel中的对应的名称 </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]

    public sealed class ViewModelAttribute : Attribute
    {
        public string Name { get; set; }

        public ViewModelAttribute(string path)
        {
            this.Name = path;
        }
    }
}
