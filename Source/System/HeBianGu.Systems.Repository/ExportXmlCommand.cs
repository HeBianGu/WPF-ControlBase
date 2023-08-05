// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace HeBianGu.Systems.Repository
{
    public class ExportXmlCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        private ISerializerService XmlSerializer => ServiceRegistry.Instance.GetInstance<ISerializerService>();

        public void Execute(object parameter)
        {
            if (XmlSerializer == null) return;

            if (parameter == null) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "文本文件(*.xml)|*.xml|所有文件|*.*";//设置文件类型
            saveFileDialog.FileName = "设置默认文件名";//设置默认文件名
            saveFileDialog.DefaultExt = "xml";//设置默认格式（可以不设）
            saveFileDialog.AddExtension = true;//设置自动在文件名中添加扩展名
            saveFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目
            saveFileDialog.Title = "保存文件"; //获取或设置文件对话框标题 
            if (saveFileDialog.ShowDialog() != true) return;

            try
            {

                XmlSerializer.Save(saveFileDialog.FileName, parameter);
                MessageProxy.Snacker.ShowTime("导出成功!");
            }
            catch (Exception ex)
            {
                MessageProxy.Windower.ShowSumit("保存错误,详情请查看日志");
                Logger.Instance.Error(ex);
            }

        }

        public event EventHandler CanExecuteChanged;
    }

    public static class RepositoryCommands
    {
        public static ExportXmlCommand ExportXml { get; set; } = new ExportXmlCommand();
    }
}
