using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using HeBianGu.Window.Notify;
using System;
using System.Diagnostics;

namespace HeBianGu.App.Tool
{
    [ViewModel("Common")]
    internal class CommonViewModel : MvcEntityViewModelBase<FileBindModel>
    {
        public IAssemblyDomain Respository { get; set; } = ServiceRegistry.Instance.GetInstance<IAssemblyDomain>();

        protected override void Init()
        {

        }

        protected override void Loaded(string args)
        {
            this.Collection = this.Respository.GetCommons();
        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "ListBox.DoubleClick.ShowItem")
            {
                Process.Start(this.SelectedItem?.FilePath);
            }

            //  Do：等待消息
            else if (command == "Menu.Click.Pause")
            {
                var find = this.Respository.GetClipBoardFile();

                if (find == null)
                {
                    Notify.Instance.ShowSysInfoMessage("请先复制文件或文件夹");
                    return;
                }

                this.Collection.Insert(0, find);

                this.Respository.SaveCommons(this.Collection);

                Notify.Instance.ShowSysSuccessMessage("添加成功！");
            }

            //  Do：等待消息
            else if (command == "Menu.Click.Delete")
            {
                if (this.SelectedItem == null) return;

                this.Collection.Remove(this.SelectedItem);

                this.Respository.SaveCommons(this.Collection);

                Notify.Instance.ShowSysSuccessMessage("移除成功！");
            }
        }



    }
}
