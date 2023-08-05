using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Diagnostics;

namespace HeBianGu.App.Tool
{
    [ViewModel("Favorite")]
    internal class FavoriteViewModel : MvcEntityViewModelBase<FileBindModel>
    {
        public IAssemblyDomain Respository { get; set; } = ServiceRegistry.Instance.GetInstance<IAssemblyDomain>();

        protected override void Init()
        {


        }

        protected override void Loaded(string args)
        {
            this.Collection = this.Respository.GetFavorites();
        }


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);

        /// <summary> 命令通用方法 </summary>
        protected async void RelayMethod(object obj)

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
                //var find = this.Respository.GetClipBoardFile();

                //if (find == null)
                //{
                //    Messager.Instance.ShowSystemNotifyMessageWithInfo("请先复制文件或文件夹");
                //    return;
                //}

                //this.Collection.Insert(0, find);

                //this.Respository.SaveCommons(this.Collection);

                //MessageService.ShowSystemNotifyMessageWithSuccess("添加成功！");
            }

            //  Do：等待消息
            else if (command == "Menu.Click.Delete")
            {
                //if (this.SelectedItem == null) return;

                //this.Collection.Remove(this.SelectedItem);

                //this.Respository.SaveCommons(this.Collection);

                //MessageService.ShowSystemNotifyMessageWithSuccess("移除成功！");
            }


        }
    }
}
