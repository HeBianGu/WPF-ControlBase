using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.App.Media.ViewModel.Performer
{
    /// <summary> 说明</summary>
    [ViewModel("Performer")]
    internal class PerformerViewModel : MvcViewModelBase
    {
        #region - 属性 - 
        private ObservableCollection<TestViewModel> _performers = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Performers
        {
            get { return _performers; }
            set
            {
                _performers = value;
                RaisePropertyChanged("Performers");
            }
        }


        private IEnumerable _outPerformers;
        /// <summary> 说明  </summary>
        public IEnumerable OutPerformers
        {
            get { return _outPerformers; }
            set
            {
                _outPerformers = value;
                RaisePropertyChanged("OutPerformers");
            }
        }


        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        Random random = new Random();
        protected override void Init()
        {
            base.Init();

            string s = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";

            var ss = s.ToArray();

            List<TestViewModel> source = new List<TestViewModel>();
            for (int i = 0; i < 100; i++)
            {
                string c = ss[random.Next(ss.Length - 1)].ToString();

                TestViewModel testView = new TestViewModel(); 
                testView.Value = "黄蓉";
                testView.Value1 = "中国";
                testView.Value3 = c;

                source.Add(testView);
            }

            this.Performers = source.OrderBy(l=>l.Value3).ToObservable();
        }

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
        }

        #endregion
    }
}
