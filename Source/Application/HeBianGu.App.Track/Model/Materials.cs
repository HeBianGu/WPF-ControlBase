// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;

namespace HeBianGu.App.Track
{
    /// <summary>
    /// 媒体素材
    /// </summary>
    internal class Materials : NotifyPropertyChangedBase
    {
        private ObservableCollection<Video> _videos = new ObservableCollection<Video>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Video> Videos
        {
            get { return _videos; }
            set
            {
                _videos = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Image> _images = new ObservableCollection<Image>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Image> Images
        {
            get { return _images; }
            set
            {
                _images = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Audio> _audios = new ObservableCollection<Audio>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Audio> Audios
        {
            get { return _audios; }
            set
            {
                _audios = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Dmx> _dmxs = new ObservableCollection<Dmx>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Dmx> Dmxs
        {
            get { return _dmxs; }
            set
            {
                _dmxs = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<UDP> _udps = new ObservableCollection<UDP>();
        /// <summary> 说明  </summary>
        public ObservableCollection<UDP> Udps
        {
            get { return _udps; }
            set
            {
                _udps = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<TCP> _tcps = new ObservableCollection<TCP>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TCP> Tcps
        {
            get { return _tcps; }
            set
            {
                _tcps = value;
                RaisePropertyChanged();
            }
        }
    }
}
