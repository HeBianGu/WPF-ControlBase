// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Text;

namespace HeBianGu.Systems.Setting
{
    /// <summary> 说明</summary>
    public class SystemSettingService : ISystemSettingService
    {
        public virtual void Load()
        {
            SystemSettingConfig.Instance.Settings.Foreach(l => l.Load());
        }

        public virtual bool Save(out string message)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ISetting item in SystemSettingConfig.Instance.Settings)
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
            SystemSettingConfig.Instance.Settings.Foreach(l => l.LoadDefault());
        }

        public void Cancel()
        {
            SystemSettingConfig.Instance.Settings.Foreach(l => l.Load());
        }

        public void Add(params ISetting[] settings)
        {
            SystemSettingConfig.Instance.Settings.AddRange(settings);
        }
    }
}
