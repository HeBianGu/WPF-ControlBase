// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;

namespace HeBianGu.App.Track
{
    /// <summary> 说明</summary>
    internal class Timer : NotifyPropertyChangedBase
    {

        private ObservableCollection<Play> _plays = new ObservableCollection<Play>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Play> Plays
        {
            get { return _plays; }
            set
            {
                _plays = value;
                RaisePropertyChanged("Plays");
            }
        }

        private Play _play;
        /// <summary> 说明  </summary>
        public Play Play
        {
            get { return _play; }
            set
            {
                _play = value;
                RaisePropertyChanged();
            }
        }


        private string _mode = "每天";
        /// <summary> 说明  </summary>
        public string Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                RaisePropertyChanged();
            }
        }


        private bool _open;
        /// <summary> 说明  </summary>
        public bool Open
        {
            get { return _open; }
            set
            {
                _open = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _time;
        /// <summary> 说明  </summary>
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

    }

    internal class Timers : NotifyPropertyChangedBase
    {
        private ObservableCollection<Timer> _collection = new ObservableCollection<Timer>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Timer> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }
    }

    internal class TimerEditer : NotifyPropertyChangedBase
    {
        public TimerEditer(Timer timer)
        {
            this.Model = timer;
        }

        public Timer Model { get; set; }

        private ObservableCollection<Play> _plays = new ObservableCollection<Play>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Play> Plays
        {
            get { return _plays; }
            set
            {
                _plays = value;
                RaisePropertyChanged();
            }
        }

    }


}
