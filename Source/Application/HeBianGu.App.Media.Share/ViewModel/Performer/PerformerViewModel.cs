using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

   
        private Random random = new Random();
        protected override void Init()
        {
            base.Init();

            string s = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";

            char[]? ss = s.ToArray();

            List<TestViewModel> source = new List<TestViewModel>();
            for (int i = 0; i < 100; i++)
            {
                string c = ss[random.Next(ss.Length - 1)].ToString();

                TestViewModel testView = new TestViewModel
                {
                    Value = "黄蓉",
                    Value1 = "中国",
                    Value3 = c
                };

                source.Add(testView);
            }

            Performers = source.OrderBy(l => l.Value3).ToObservable();
        }

    }
}
