// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Track
{
    /// <summary>
    /// 播放项
    /// </summary>
    internal class Play : SelectViewModel<string>
    {
        public Play() : base(null)
        {

        }
        public Play(string t) : base(t)
        {
        }

        private string _name;
        [Display(Name = "列表名称")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Program> _collection = new ObservableCollection<Program>();



        [Browsable(false)]
        public ObservableCollection<Program> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

    }

}
