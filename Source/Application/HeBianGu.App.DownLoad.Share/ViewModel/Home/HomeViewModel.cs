using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HeBianGu.App.DownLoad.ViewModel.Home
{
    /// <summary> 说明</summary>
    [ViewModel("Home")]
    internal class HomeViewModel : MvcViewModelBase
    {
        #region - 属性 -

        private IAction _selectedAction;
        /// <summary> 说明  </summary>
        public IAction SelectedAction
        {
            get { return _selectedAction; }
            set
            {
                _selectedAction = value;
                RaisePropertyChanged();
            }
        }


        private DownLoadItem _selectedItem;
        /// <summary> 说明  </summary>
        public DownLoadItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<DownLoadItem> _downLoadItems = new ObservableCollection<DownLoadItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<DownLoadItem> DownLoadItems
        {
            get { return _downLoadItems; }
            set
            {
                _downLoadItems = value;
                RaisePropertyChanged("DownLoadItems");
            }
        }


        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -
        private static Random _random = new Random();
        protected override void Init()
        {
            base.Init();
            string[] names = { "天龙八部", "鹿鼎记", "射雕英雄传", "飞狐外传", "雪山飞狐", "连城诀", "白马啸西风", "书剑恩仇录", "笑傲江湖", "神雕侠侣", "侠客行", "倚天屠龙记", "碧血剑", "鸳鸯刀", "小李飞刀", "三少爷的剑" };

            foreach (string name in names)
            {
                DownLoadItem? item = new DownLoadItem() { Value = name };
                item.Bool1 = _random.Next(3) == 1;
                item.Double1 = _random.NextDouble() * 100;
                item.Value2 = item.Bool1 ? "已暂停" : "下载中";
                double total = _random.NextDouble() * 1024;
                item.Value1 = $"{Math.Round(total * item.Double1 / 100, 2)}MB/{Math.Round(total, 2)}MB";
                item.Items.Clear();
                int c = _random.Next(1, 10);
                for (int i = 0; i < c; i++)
                {
                    TestViewModel? model = new TestViewModel() { Value = item.Value + " - " + i + ".mp4" };
                    TreeNodeBase<TestViewModel>? node = new TreeNodeBase<TestViewModel>(model);
                    item.Items.Add(node);
                }
                DownLoadItems.Add(item);
            }

            SelectedItem = DownLoadItems.FirstOrDefault();
        }

        #endregion
    }


}
