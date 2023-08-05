// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class OpenClearPathTextPropertyItem : TextPropertyItem
    {
        public OpenClearPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            Process.Start(new ProcessStartInfo(this.Value) { UseShellExecute = true });

            //Process.Start(this.Value);
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });


        public RelayCommand ClearCommand => new RelayCommand(l =>
        {
            try
            {
                if (File.Exists(this.Value))
                {
                    File.Delete(this.Value);
                }

                if (Directory.Exists(this.Value))
                {
                    Directory.Delete(this.Value, true);
                }

                MessageProxy.Snacker?.ShowTime("删除成功");
            }
            catch (Exception ex)
            {
                MessageProxy.Snacker?.ShowTime("删除失败,详情请查看日志");
                Logger.Instance?.Error(ex);
            }
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });

    }
}
