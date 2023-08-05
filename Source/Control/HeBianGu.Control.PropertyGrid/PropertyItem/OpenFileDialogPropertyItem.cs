// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class OpenFileDialogPropertyItem : TextPropertyItem
    {
        public OpenFileDialogPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        [Displayer(Name = "浏览", Icon = "\xe83a", Order = 2)]
        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (File.Exists(this.Value))
                openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(this.Value); //设置初始路径
            openFileDialog.Filter = "所有文件(*.*)|*.*"; //设置“另存为文件类型”或“文件类型”框中出现的选择内容
            openFileDialog.FilterIndex = 2; //设置默认显示文件类型为Csv文件(*.csv)|*.csv
            openFileDialog.Title = "打开文件"; //获取或设置文件对话框标题
            openFileDialog.RestoreDirectory = true; //设置对话框是否记忆上次打开的目录
            openFileDialog.Multiselect = false;//设置多选
            if (openFileDialog.ShowDialog() != true)
                return;
            this.Value = openFileDialog.FileName;
        });
    }
}
