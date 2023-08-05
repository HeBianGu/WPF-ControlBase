// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace HeBianGu.Base.WpfBase
{
    public interface IExcelService
    {
        bool Export(IEnumerable<object> collection, string path, string sheetName, out string message);
        IEnumerable<T> Read<T>(string filePath, Func<object, T> convert, out string message);
    }

    public class ExcelServiceProxy : ServiceInstance<IExcelService>
    {

    }
}