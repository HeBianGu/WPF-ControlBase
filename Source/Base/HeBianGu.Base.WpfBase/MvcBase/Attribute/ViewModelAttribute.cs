using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 应用再Mvc中的特性，根据此特性查找ViewModel中的对应的名称 </summary>
    public sealed class ViewModelAttribute : Attribute
    {
        public string Name { get; set; }

        public ViewModelAttribute(string path)
        {
            this.Name = path;
        }
    }
}
