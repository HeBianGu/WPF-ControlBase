// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.App.Track
{
    /// <summary> 节目</summary>
    internal class Program : NotifyPropertyChangedBase
    {
        public Program()
        {
            this.Collection.Add(this.MediaAxis);
            this.Collection.Add(this.DmxAxis);
            this.Collection.Add(this.CommandAxis);
            this.Image = new BitmapImage(new Uri("/HeBianGu.App.Track;component/Resources/program_icon1.png", UriKind.Relative));
            this.SelectedImage = new BitmapImage(new Uri("/HeBianGu.App.Track;component/Resources/program_icon2.png", UriKind.Relative));
        }
        private string _name;
        [Display(Name = "节目名称")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }


        private double _size;
        [Browsable(false)]
        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 节目GUID
        /// </summary>
        private string _id;
        [Browsable(false)]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged();
            }
        }

        private MediaAxis _mediaAxis = new MediaAxis();

        /// <summary>
        /// 媒体轴
        /// </summary>
        [Browsable(false)]
        public MediaAxis MediaAxis
        {
            get { return _mediaAxis; }
            set
            {
                _mediaAxis = value;
                RaisePropertyChanged();
            }
        }
        private CommandAxis _commandAxis = new CommandAxis();

        /// <summary>
        /// 指令轴
        /// </summary>
        [Browsable(false)]
        public CommandAxis CommandAxis
        {
            get { return _commandAxis; }
            set
            {
                _commandAxis = value;
                RaisePropertyChanged();
            }
        }

        private DmxAxis _dmxAxis = new DmxAxis();

        /// <summary>
        /// 指令轴
        /// </summary>
        [Browsable(false)]
        public DmxAxis DmxAxis
        {
            get { return _dmxAxis; }
            set
            {
                _dmxAxis = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IAxis> _collection = new ObservableCollection<IAxis>();
        [Browsable(false)]
        public ObservableCollection<IAxis> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private ImageSource _image;
        [Browsable(false)]
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

        private ImageSource _selectedImage;
        [Browsable(false)]
        public ImageSource SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;
                RaisePropertyChanged();
            }
        }


        public double GetMax()
        {
            if (this.Collection == null || this.Collection.Count == 0)
                return 0;

            return this.Collection.Max(l => l.GetMax());
            //var many = this.Collection.SelectMany(l => l.Collection);
            //var end = many.Count() == 0 ? 0 : many.Max(l => l.Start + l.Size);

            //return end;
        }
    }

    internal class ProgramSelector : NotifyPropertyChangedBase
    {
        private ObservableCollection<Program> _collection = new ObservableCollection<Program>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Program> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private ObservableCollection<Program> _selectedItems = new ObservableCollection<Program>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Program> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }

    }
}
