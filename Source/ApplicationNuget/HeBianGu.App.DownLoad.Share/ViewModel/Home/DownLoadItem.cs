using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;

namespace HeBianGu.App.DownLoad
{
    /// <summary> 说明</summary>
    internal class DownLoadItem : TestViewModel
    {

        #region - 属性 -
        private ObservableCollection<TreeNodeBase<TestViewModel>> _items = new ObservableCollection<TreeNodeBase<TestViewModel>>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeBase<TestViewModel>> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged("Items");
            }
        }
        #endregion

        #region - 命令 -

        #endregion 
    }


}
