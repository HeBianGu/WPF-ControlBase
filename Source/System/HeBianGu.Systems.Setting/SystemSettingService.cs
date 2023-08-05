// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace HeBianGu.Systems.Setting
{
    /// <summary> 说明</summary>
    public class SystemSettingService : ISystemSettingService
    {
        public string Name => "配置数据";
        public virtual bool Load(out string message)
        {
            message = null;
            List<string> list = new List<string>();
            list.Add(SystemSetting.GroupApp);
            list.Add(SystemSetting.GroupBase);
            list.Add(SystemSetting.GroupSystem);
            list.Add(SystemSetting.GroupData);
            list.Add(SystemSetting.GroupStyle);
            list.Add(SystemSetting.GroupControl);
            list.Add(SystemSetting.GroupMessage);
            list.Add(SystemSetting.GroupSecurity);
            list.Add(SystemSetting.GroupAuthority);
            list.Add(SystemSetting.GroupOther);
            Comparison<ISetting> comparison = (x, y) =>
              {
                  if (x == null) return -1;
                  if (y == null) return 1;
                  if (x.GroupName == y.GroupName)
                      return x.Order.CompareTo(y.Order);
                  return list.IndexOf(x.GroupName).CompareTo(list.IndexOf(y.GroupName));
              };
            SystemDisplay.Instance.Settings.RemoveAll(x => x == null);
            SystemDisplay.Instance.Settings = SystemDisplay.Instance.Settings.Sort(comparison);
            SystemDisplay.Instance.Settings.Foreach(l => l.Load());
            return true;
        }

        public virtual bool Save(out string message)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ISetting item in SystemDisplay.Instance.Settings)
            {
                try
                {
                    if (!item.Save(out string error))
                    {
                        sb.AppendLine(error);
                    }
                }
                catch (Exception ex)
                {
                    sb.AppendLine(ex.Message);
                }
            }
            message = sb.ToString();
            return string.IsNullOrEmpty(message);
        }

        public void SetDefault()
        {
            SystemDisplay.Instance.Settings.Foreach(l => l.LoadDefault());
        }

        public void Cancel()
        {
            SystemDisplay.Instance.Settings.Foreach(l => l.Load());
        }

        public void Add(params ISetting[] settings)
        {
            foreach (var setting in settings)
            {
                if (SystemDisplay.Instance.Settings.Contains(setting))
                    continue;
                SystemDisplay.Instance.Settings.Add(setting);
            }
        }
    }
}
