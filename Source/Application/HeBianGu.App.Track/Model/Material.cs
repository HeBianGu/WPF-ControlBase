// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.StoryBoard;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.App.Track
{

    internal abstract class MaterialBase : NotifyPropertyChangedBase, IThumbBarItem
    {
        private string _name;
        [Display(Name = "名称")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _toolTip;
        /// <summary> 说明  </summary>
        public string ToolTip
        {
            get { return _toolTip; }
            set
            {
                _toolTip = value;
                RaisePropertyChanged();
            }
        }

        private string _icon = "\xe92b";
        /// <summary> 说明  </summary>
        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }


        private double _start;
        /// <summary> 说明  </summary>
        public double Start
        {
            get { return _start; }
            set
            {
                _start = value;
                RaisePropertyChanged();
            }
        }


        private double _size = 10;
        /// <summary> 说明  </summary>
        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }

        private long _totalSeconds;
        /// <summary> 说明  </summary>
        public long TotalSeconds
        {
            get { return _totalSeconds; }
            set
            {
                _totalSeconds = value;
                RaisePropertyChanged();
            }
        }


        //private ImageSource _image;
        ///// <summary> 说明  </summary>
        //public ImageSource Image
        //{
        //    get { return _image; }
        //    set
        //    {
        //        _image = value;
        //        RaisePropertyChanged();
        //    }
        //}
        private string _guid;
        /// <summary> 说明  </summary>
        public string Id
        {
            get { return _guid; }
            set
            {
                _guid = value;
                RaisePropertyChanged();
            }
        }


        private string _source_name;
        /// <summary> 说明  </summary>
        public string SourceName
        {
            get { return _source_name; }
            set
            {
                _source_name = value;
                RaisePropertyChanged();
            }
        }


        //private ImageSource _selectedImage;
        ///// <summary> 说明  </summary>
        //public ImageSource SelectedImage
        //{
        //    get { return _selectedImage; }
        //    set
        //    {
        //        _selectedImage = value;
        //        RaisePropertyChanged();
        //    }
        //}


        public object Clone()
        {
            object n = Activator.CreateInstance(this.GetType());

            System.Reflection.PropertyInfo[] ps = this.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo p in ps)
            {
                p.SetValue(n, p.GetValue(this));
            }

            return n;
        }
    }


    /// <summary>
    /// 媒体素材
    /// </summary>
    internal class Media : MaterialBase
    {
         
        private string _path;
        /// <summary> 说明  </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                RaisePropertyChanged();
            }
        }

        private long _pixel;
        /// <summary> 说明  </summary>
        public long Pixel
        {
            get { return _pixel; }
            set
            {
                _pixel = value;
                RaisePropertyChanged();
            }
        }
    }

    /// <summary>
    /// 图片
    /// </summary>
    internal class Image : Media
    {
        public Image()
        {
            this.Icon = "\xe606"; 
        }
    }

    /// <summary>
    /// 音频
    /// </summary>
    internal class Audio : Media
    {
        public Audio()
        {
            this.Icon = "\xe6ba";
        }
    }

    /// <summary>
    /// 视频
    /// </summary>
    internal class Video : Media
    {
        public Video()
        {
            this.Icon = "\xe92b";
        }
    }

    /// <summary>
    /// 命令素材
    /// </summary>
    internal abstract class Command : MaterialBase
    {
        public Command()
        {
            this.Icon = "\xe718";
            this.Size = 10;
            this.Name = "zlz-003";
        }

        private string _code = "tsdm-0034";
        /// <summary> 说明  </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged();
            }
        }
    }

    internal class Dmx : MaterialBase
    { 
        public Dmx()
        {
            this.Icon = "\xe718";
            this.Size = 10;
            this.Name = "zlz-003";
            this.Code = "Art-net";
        }
        private string _code = "tsdm-0034";
        /// <summary> 说明  </summary>
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                RaisePropertyChanged();
            }
        }

        private string _channel = "All";
        /// <summary> 说明  </summary>
        public string Channel
        {
            get { return _channel; }
            set
            {
                _channel = value;
                RaisePropertyChanged();
            }
        }
    }

    internal class UDP : Command
    {
        public UDP()
        {
            this.Icon = "\xe718";
            this.Size = 10;
            this.Name = "192.168.1.234:8080";
        }

        private string _ip;
        /// <summary> 说明  </summary>
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                RaisePropertyChanged();
            }
        }


        private int _port;
        /// <summary> 说明  </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }


    }

    internal class TCP : Command
    {
        public TCP()
        {
            this.Icon = "\xe718";
            this.Size = 10;
            this.Name = "192.168.1.234:8080";
        }

        private string _ip;
        /// <summary> 说明  </summary>
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                RaisePropertyChanged();
            }
        }


        private int _port;
        /// <summary> 说明  </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }
    }
}
