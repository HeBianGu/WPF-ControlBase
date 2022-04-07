using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;

namespace HeBianGu.Application.LinkWindow
{
    [ViewModel("Grid")]
    public class GridViewModel : MvcViewModelBase
    {

        private ObservableCollection<TreeNodeEntity> _collection = new ObservableCollection<TreeNodeEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeEntity> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private Random random = new Random();
        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
            //  Do：取消
            else if (command == "init")
            {
                Collection.Clear();

                System.Collections.Generic.List<TreeNodeEntity> data = AssemblyDomain.Instance.GetTreeListData();

                foreach (TreeNodeEntity item in data)
                {
                    Collection.Add(item);
                }

            }
        }

    }
}
