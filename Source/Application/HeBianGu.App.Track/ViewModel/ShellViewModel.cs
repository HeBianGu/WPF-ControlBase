// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace HeBianGu.App.Track
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<Play> _plays = new ObservableCollection<Play>();
        /// <summary> 播放列表  </summary>
        public ObservableCollection<Play> Plays
        {
            get { return _plays; }
            set
            {
                _plays = value;
                RaisePropertyChanged("Plays");
            }
        }


        private Play _selectedPlay;
        /// <summary> 说明  </summary>
        public Play SelectedPlay
        {
            get { return _selectedPlay; }
            set
            {
                _selectedPlay = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Program> _programs = new ObservableCollection<Program>();
        /// <summary> 节目  </summary>
        public ObservableCollection<Program> Programs
        {
            get { return _programs; }
            set
            {
                _programs = value;
                RaisePropertyChanged("Programs");
            }
        }


        private System.Windows.Visibility _showSaveProgram;
        /// <summary> 说明  </summary>
        public System.Windows.Visibility ShowSaveProgram
        {

            get { return _showSaveProgram; }
            set
            {
                _showSaveProgram = value;
                RaisePropertyChanged();
            }
        }
        private Program _selectedProgram;

        /// <summary>
        /// 当前选择的节目
        /// </summary>
        public Program SelectedProgram
        {
            get { return _selectedProgram; }
            set
            {

                _selectedProgram = value;

                this.SelectedObject = value;
                if (value != null)
                {
                    ShowSaveProgram = System.Windows.Visibility.Visible;
                }
                else
                {

                    ShowSaveProgram = System.Windows.Visibility.Collapsed;
                }
                RaisePropertyChanged();
            }
        }

        private object _selectedObject;
        /// <summary> 说明  </summary>
        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                RaisePropertyChanged();
            }
        }


        private bool _isProgramPlay;
        /// <summary> 说明  </summary>
        public bool IsProgramPlay
        {
            get { return _isProgramPlay; }
            set
            {
                _isProgramPlay = value;
                RaisePropertyChanged();
            }
        }

        private double _programPlayValue = 0.0;
        /// <summary> 说明  </summary>
        public double ProgramPlayValue
        {
            get { return _programPlayValue; }
            set
            {
                _programPlayValue = value;
                RaisePropertyChanged("ProgramPlayValue");
            }
        }


        private ObservableCollection<ToorBar> _collection = new ObservableCollection<ToorBar>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ToorBar> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private StoryBoardPlayerViewModel _storyBoardPlayerViewModel = new StoryBoardPlayerViewModel();
        /// <summary> 说明  </summary>
        public StoryBoardPlayerViewModel StoryBoardPlayerViewModel
        {
            get { return _storyBoardPlayerViewModel; }
            set
            {
                _storyBoardPlayerViewModel = value;
                RaisePropertyChanged("StoryBoardPlayerViewModel");
            }
        }

        private Materials _materials = new Materials();
        /// <summary> 说明  </summary>
        public Materials Materials
        {
            get { return _materials; }
            set
            {
                _materials = value;
                RaisePropertyChanged();
            }
        }

        private Timers _timers = new Timers();
        /// <summary> 说明  </summary>
        public Timers Timers
        {
            get { return _timers; }
            set
            {
                _timers = value;
                RaisePropertyChanged();
            }
        }

        private Random _random = new Random();
        protected override void Init()
        {
            Brush[] brushes = new Brush[] { Brushes.Red, Brushes.Orange, Brushes.Blue, Brushes.Green, Brushes.Purple };

            Type[] types = new Type[] { typeof(Image), typeof(Video), typeof(Audio) };

            for (int i = 0; i < 5; i++)
            {
                Program program = new Program();
                program.Name = i.ToString();

                //  Do ：媒体轴
                double temp = 0;
                for (int j = 0; j < _random.Next(4, 8); j++)
                {
                    Media mediaMaterial = Activator.CreateInstance(types[_random.Next(types.Length - 1)]) as Media;
                    mediaMaterial.Name = j + ".mp4";
                    mediaMaterial.Start = temp + _random.Next(0, 100);
                    mediaMaterial.Size = _random.Next(0, 200);
                    temp = mediaMaterial.Start + mediaMaterial.Size;
                    program.MediaAxis.Collection.Add(mediaMaterial);
                }

                //  Do ：指令轴
                for (int j = 0; j < 4; j++)
                {
                    CommandAxis commandAxis = new CommandAxis();
                    commandAxis.Name = commandAxis.Name + j.ToString();
                    temp = 0;
                    //for (int k = 0; k < _random.Next(4, 10); k++)
                    //{
                    //    Dmx commandMaterial = new Dmx();
                    //    commandAxis.Collection.Add(commandMaterial);
                    //    commandMaterial.Start = temp + _random.Next(0, 100);
                    //    temp = commandMaterial.Start + commandMaterial.Size;
                    //}

                    for (int k = 0; k < _random.Next(4, 10); k++)
                    {
                        UDP commandMaterial = new UDP();
                        commandAxis.Collection.Add(commandMaterial);
                        commandMaterial.Start = temp + _random.Next(0, 100);
                        temp = commandMaterial.Start + commandMaterial.Size;
                    }

                    for (int k = 0; k < _random.Next(4, 10); k++)
                    {
                        TCP commandMaterial = new TCP();
                        commandAxis.Collection.Add(commandMaterial);
                        commandMaterial.Start = temp + _random.Next(0, 100);
                        temp = commandMaterial.Start + commandMaterial.Size;
                    }

                    program.Collection.Add(commandAxis);
                }

                this.Programs.Add(program);

                this.SelectedProgram = program;
            }

            for (int i = 0; i < _random.Next(8, 15); i++)
            {
                Play play = new Play();
                play.Name = "播放列表" + i;
                int skip = _random.Next(0, this.Programs.Count - 3);
                int take = _random.Next(2, this.Programs.Count - skip);
                play.Collection = this.Programs.Skip(skip).Take(take).ToObservable();
                this.Plays.Add(play);
            }

            this.SelectedPlay = this.Plays?.FirstOrDefault();

            this.Materials.Images = Enumerable.Range(5, _random.Next(5, 15)).Select(l => new Image() { Name = l + ".jpg" }).ToObservable();
            this.Materials.Videos = Enumerable.Range(5, _random.Next(5, 15)).Select(l => new Video() { Name = l + ".mp4" }).ToObservable();
            this.Materials.Audios = Enumerable.Range(5, _random.Next(5, 15)).Select(l => new Audio() { Name = l + ".mp3" }).ToObservable();
            this.Materials.Dmxs = Enumerable.Range(5, _random.Next(5, 15)).Select(l => new Dmx() { Name = l.ToString() }).ToObservable();
            this.Materials.Tcps = Enumerable.Range(5, _random.Next(5, 15)).Select(l => new TCP() { Name = "192.168.1." + _random.Next(1, 255).ToString() + ":" + _random.Next(1, 10000), IP = "192.168.1." + _random.Next(1, 255).ToString(), Port = _random.Next(1, 10000) }).ToObservable();
            this.Materials.Udps = Enumerable.Range(5, _random.Next(5, 15)).Select(l => new UDP() { Name = "192.168.1." + _random.Next(1, 255).ToString() + ":" + _random.Next(1, 10000), IP = "192.168.1." + _random.Next(1, 255).ToString(), Port = _random.Next(1, 10000) }).ToObservable();

            for (int i = 0; i < _random.Next(3, 6); i++)
            {
                Timer timer = new Timer();
                timer.Play = this.Plays[_random.Next(0, this.Plays.Count - 1)];
                timer.Time = DateTime.Now.AddMinutes(_random.Next(0, 50000));
                timer.Name = timer.Mode + "," + timer.Time.ToString("HH:mm");
                timer.Open = _random.Next(0, 3) == 1;
                this.Timers.Collection.Add(timer);
            }
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)
        {
            string command = obj?.ToString();

            if (command == "Button.Add")
            {

                if (this.StoryBoardPlayerViewModel.PlayMode)
                {
                    MessageProxy.Snacker.ShowTime("请先停止播放再进行添加！");
                    return;
                }
                this.StoryBoardPlayerViewModel.Create("\xeada", "指令轴" + this.StoryBoardPlayerViewModel.Collection.Count);
            }
        }


        public RelayCommand ProgramPlayCommand => new RelayCommand(l =>
        {
            if (this.IsProgramPlay)
            {
                this.ProgramPlay();
            }
            else
            {
                this.ProgramStop();
            }
        });

        public RelayCommand TimerEidtCommand => new RelayCommand(async l =>
        {
            if (l is Timer timer)
            {
                TimerEditer editer = new TimerEditer(timer);

                editer.Plays = this.Plays.ToObservable();

                foreach (var item in editer.Plays)
                {
                    item.IsSelected = timer.Plays == null ? false : timer.Plays.Contains(item);
                }

                var r = await MessageProxy.Presenter.Show(editer, null, "定时设置");

                if (r == false)
                    return;

                timer.Plays = editer.Plays.Where(x => x.IsSelected).ToObservable();
            }
        });

        public RelayCommand ProgramStopCommand => new RelayCommand(l =>
        {
            this.ProgramStop();
            this.ProgramPlayValue = 0;
            this.IsProgramPlay = false;
        });

        System.Timers.Timer _timer;

        public void ProgramStop()
        {
            if (_timer == null)
                return;
            _timer.Stop();
        }

        public void ProgramPlay()
        {
            this.ProgramStop();

            double max = this.SelectedProgram.GetMax();
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += (l, k) =>
            {
                if (this.ProgramPlayValue < max / 3.0)
                {
                    this.ProgramPlayValue++;
                }
                else
                {
                    this.ProgramPlayValue = 0;
                }
            };

            _timer.Start();
        }

        public RelayCommand SelectedObjectCommand => new RelayCommand(l =>
        {
            this.SelectedObject = l;
        });


        public RelayCommand AddProgramCommand => new RelayCommand(async l =>
        {
            Program program = new Program();

            bool r = await PropertyGrid.Show(program, null, "新增节目");

            if (!r) return;
            this.Programs.Add(program);
        });

        public RelayCommand AddPlayCommand => new RelayCommand(async l =>
        {
            Play program = new Play();

            bool r = await PropertyGrid.Show(program, null, "新增播放列表");

            if (!r) return;
            this.Plays.Add(program);
        });

        public RelayCommand EditPlayCommand => new RelayCommand(async l =>
        {
            if (this.SelectedPlay == null)
            {
                MessageProxy.Snacker.ShowTime("没有选择列表");
                return;
            }


            System.Collections.Generic.IEnumerable<Program> source = this.Programs.Except(this.SelectedPlay.Collection);

            if (source.Count() == 0)
            {
                MessageProxy.Snacker.ShowTime("没有需要选择的数据");
                return;
            }

            ProgramSelector selector = new ProgramSelector();
            selector.Collection = source.ToObservable();
            bool r = await MessageProxy.Presenter.Show(selector, x => true, "选择节目", x =>
                  {
                      //x.Width = 800;
                      //x.Padding = new System.Windows.Thickness(10);
                      //x.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                      //x.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                  }, ObjectContentDialog.ClearKey);

            if (!r) return;

            if (selector.SelectedItems == null || selector.SelectedItems.Count == 0)
            {
                MessageProxy.Snacker.ShowTime("没有选择数据");
                return;
            }

            this.SelectedPlay.Collection.AddRange(selector.SelectedItems);
        });


        public RelayCommand EditTimerCommand => new RelayCommand(async l =>
        {
            bool r = await MessageProxy.Presenter.Show(this.Timers, x => true, "定时任务", x =>
            {
                //x.Width = 800;
                //x.Padding = new System.Windows.Thickness(10);
                //x.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                //x.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            }, ObjectContentDialog.ClearKey);

            if (!r) return;
        });

        public RelayCommand SaveCommand => new RelayCommand(async l =>
        {
            bool r = await Messager.Instance.ShowWaitter<bool>(() =>
            {
                return this.SaveProgram();
            });
            if (!r) return;

            MessageProxy.Snacker.ShowTime("保存成功");
        }, l => this.SelectedProgram != null);

        //  ToDo：保存节目
        public bool SaveProgram()
        {
            Thread.Sleep(2000);

            return true;
        }
    }

    public partial class StoryBoardPlayerViewModel : NotifyPropertyChanged
    {
        public StoryBoardPlayerViewModel()
        {
            this.IndexChanged += l =>
            {
                //  Do ：触发子项进度
                foreach (StoryBoardItemViewModel item in this.Collection)
                {
                    item.OnIndexChanged(l);
                }
            };
            {
                StoryBoardItemViewModel find = this.Create("\xeabe", "媒体库");
                find.Left = 10;
                find.Width = 100;
            }
            {
                StoryBoardItemViewModel find = this.Create("\xeada", "指令轴1");
                find.Left = 200;
                find.Width = 160;
            }
            {
                StoryBoardItemViewModel find = this.Create("\xeada", "指令轴2");
                find.Left = 400;
                find.Width = 150;
            }
            {
                StoryBoardItemViewModel find = this.Create("\xeada", "指令轴3");
                find.Left = 600;
                find.Width = 20;
            }
        }
        private ObservableCollection<StoryBoardItemViewModel> _collection = new ObservableCollection<StoryBoardItemViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<StoryBoardItemViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        public StoryBoardItemViewModel Create(string icon, string name)
        {
            StoryBoardItemViewModel item = new StoryBoardItemViewModel(this);
            item.Icon = icon;
            item.Name = name;
            //  Do ：注册子项进度
            item.IndexChanged += l =>
            {
                //  Do ：触发子项进度外部事件
                ItemIndexChanged?.Invoke(item, l);
            };

            this.Collection.Add(item);

            return item;
        }


        private double _maxValue = 100.0;
        /// <summary> 说明  </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }


        private double _minValue = 0.0;
        /// <summary> 说明  </summary>
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }


        private double _value = 0.0;
        /// <summary> 说明  </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


        private int _speed = 0;
        /// <summary> 说明  </summary>
        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged("Speed");
            }
        }

        private System.Timers.Timer timer = new System.Timers.Timer();

        public void Start()
        {
            this.Stop();

            timer = new System.Timers.Timer();

            //timer.Interval = 1000 - Speed;

            timer.Elapsed += (l, k) =>
            {
                timer.Interval = Speed == 0 ? 1000 : 1010 - 125 * Speed;

                if (this.Value < MaxValue)
                {
                    this.Value++;
                }
                else
                {
                    this.Value = 0;
                }

                IndexChanged?.Invoke(this.Value);
            };

            timer.Start();
        }

        public void StartWith(StoryBoardItemViewModel item)
        {
            this.Stop();

            this.Collection.Foreach(l =>
            {
                if (l != item)
                    l.PlayMode = false;

                l.IsEnbled = l != item;
            });


            timer = new System.Timers.Timer();

            this.Value = MaxValue * item.LeftPercent;

            timer.Interval = Speed == 0 ? 1000 : 1010 - 125 * Speed;

            timer.Elapsed += (l, k) =>
            {
                timer.Interval = Speed == 0 ? 1000 : 1010 - 125 * Speed;



                if (this.Value < (int)MaxValue * item.RightPercent && this.Value >= MaxValue * item.LeftPercent)
                {
                    this.Value++;
                }
                else
                {
                    this.Value = MaxValue * item.LeftPercent;
                }


                IndexChanged?.Invoke(this.Value);
            };

            timer.Start();


        }

        public void Stop()
        {
            timer.Stop();
        }


        private bool _playMode;
        /// <summary> 说明  </summary>
        public bool PlayMode
        {
            get { return _playMode; }
            set
            {
                this._playMode = value;
                RaisePropertyChanged("PlayMode");
            }
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
            else if (command == "ToggleButton.Click.Play")
            {
                if (this.Collection.Count == 0)
                {
                    this.PlayMode = false;

                    MessageProxy.Snacker.ShowTime("请至少添加一个条目！");

                    return;
                }

                this.Collection.Foreach(l => l.PlayMode = true);

                this.Collection.Foreach(l => l.IsEnbled = false);

                this.Start();


            }

            else if (command == "ToggleButton.Click.Stop")
            {
                this.Collection.Foreach(l => l.PlayMode = false);

                this.Collection.Foreach(l => l.IsEnbled = true);

                this.Stop();
            }
        }

        public event Action<double> IndexChanged;

        public event Action<StoryBoardItemViewModel, double> ItemIndexChanged;

    }

    public partial class StoryBoardItemViewModel : NotifyPropertyChanged
    {
        private static int Index = 0;

        private string _name;
        /// <summary> 说明  </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string _icon = "\xeabe";
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


        public StoryBoardItemViewModel()
        {

        }

        private StoryBoardPlayerViewModel _parent;

        public StoryBoardItemViewModel(StoryBoardPlayerViewModel parent)
        {
            _parent = parent;

            this.Name = (Index++).ToString();
        }

        public event Action<double> IndexChanged;

        private bool _isEnbled = false;
        /// <summary> 说明  </summary>
        public bool IsEnbled
        {
            get { return _isEnbled; }
            set
            {
                _isEnbled = value;
                RaisePropertyChanged("IsEnbled");
            }
        }


        private double _leftPercent = 0.0;
        /// <summary> 说明  </summary>
        public double LeftPercent
        {
            get { return _leftPercent; }
            set
            {
                _leftPercent = value;
                RaisePropertyChanged("LeftPercent");
            }
        }


        private double _rightPercent = 1.0;
        /// <summary> 说明  </summary>
        public double RightPercent
        {
            get { return _rightPercent; }
            set
            {
                _rightPercent = value;
                RaisePropertyChanged("RightPercent");
            }
        }

        private bool _playMode;
        /// <summary> 说明  </summary>
        public bool PlayMode
        {
            get { return _playMode; }
            set
            {
                _playMode = value;

                RaisePropertyChanged("PlayMode");
            }
        }


        private double _value = 0;
        /// <summary> 说明  </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }


        private double _left;
        /// <summary> 说明  </summary>
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                RaisePropertyChanged();
            }
        }


        private double _width;
        /// <summary> 说明  </summary>
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }



        public void OnIndexChanged(double value)
        {
            if (this.IsEnbled == false && this.PlayMode)
            {
                this.IndexChanged?.Invoke(value);

                this.Value = value;
            }
        }

        public RelayCommand<ToggleButton> ToggleButtonCheckChangedCommand => new Lazy<RelayCommand<ToggleButton>>(() => new RelayCommand<ToggleButton>(l => ToggleButtonCheckedChanged(l))).Value;

        internal void ToggleButtonCheckedChanged(ToggleButton commandParamer)
        {
            //  Do ：避免通过属性修改触发此事件
            if (!commandParamer.IsFocused) return;

            if (this.PlayMode)
            {
                this._parent.StartWith(this);
            }
            else
            {
                this._parent.Stop();
            }
        }


        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "StoryBoardItem.Button.Delelte")
            {
                if (this._parent.PlayMode)
                {
                    MessageProxy.Snacker.ShowTime("请先停止播放再进行此操作！");
                    return;
                }

                bool result = await Messager.Instance.ShowResult("确定要删除当前项目？");

                if (!result) return;

                this._parent.Collection.Remove(this);

                this.IndexChanged = null;
            }
        }
    }

    /// <summary> 说明oo</summary>
    internal class ToorBar : NotifyPropertyChanged
    {

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


        private string _icon;
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
        #region - 属性 - 
        private ObservableCollection<ToolBarItem> _collection = new ObservableCollection<ToolBarItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ToolBarItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

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


    /// <summary> 说明</summary>
    public class ToolBarItem : NotifyPropertyChanged
    {
        #region - 属性 -


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


        private Brush _brush = Brushes.Orange;
        /// <summary> 说明  </summary>
        public Brush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                RaisePropertyChanged();
            }
        }


        private double _left;
        /// <summary> 说明  </summary>
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                RaisePropertyChanged();
            }
        }

        private double _width = 5;
        /// <summary> 说明  </summary>
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

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
