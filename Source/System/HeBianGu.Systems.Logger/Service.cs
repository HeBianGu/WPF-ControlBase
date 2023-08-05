
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.AppConfig;
using log4net;
using log4net.Appender;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace HeBianGu.Systems.Logger
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }


    //[SettingConfig(Name = "参数设置", Group = "基本设置")]
    public class Setting : LazySettingInstance<Setting>
    {

    }
}
