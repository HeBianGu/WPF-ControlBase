// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;
using Path = System.IO.Path;

namespace HeBianGu.Control.Vlc
{
    [TemplatePart(Name = "media_slider", Type = typeof(Slider))]
    [TemplatePart(Name = "PART_Vlc", Type = typeof(VlcControl))]
    public partial class VlcPlayer : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(VlcPlayer), "S.VlcPlayer.Default");
        public static ComponentResourceKey MouseOverKey => new ComponentResourceKey(typeof(VlcPlayer), "S.VlcPlayer.MouseOver");

        static VlcPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VlcPlayer), new FrameworkPropertyMetadata(typeof(VlcPlayer)));
        }

        public VlcPlayer()
        {
            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.OpenFile, (l, k) =>
                {
                    OpenFileDialog dialog = new OpenFileDialog();

                    bool? r = dialog.ShowDialog();

                    if (r.HasValue && r.Value)
                    {
                        this.VedioSource = new Uri(dialog.FileName, UriKind.Absolute);
                    }

                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.ShootCut, async (l, k) =>
                {
                    string filePath = await this.BeginShootCut();

                    this.OnShootCut(filePath);

                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.FullScreen, async (l, k) =>
                {
                    if (this.Parent is Panel panel)
                    {
                        int index = panel.Children.IndexOf(this);
                        panel.Children.RemoveAt(index);
                        FullWindow window = new FullWindow();
                        window.Content = this;
                        this.IsFullScreen = true;
                        window.Owner = Application.Current.MainWindow;
                        window.ShowDialog();
                        window.Content = null;
                        panel.Children.Insert(index, this);
                        this.IsFullScreen = false;
                    }

                    if (this.Parent is ContentControl control)
                    {
                        FullWindow window = new FullWindow();
                        window.Content = this;
                        this.IsFullScreen = true;
                        window.Owner = Application.Current.MainWindow;
                        window.ShowDialog();
                        control.Content = this;
                        this.IsFullScreen = false;

                    }
                });

                this.CommandBindings.Add(binding);
            }

            this.MouseDown += (l, k) =>
              {
                  this.IsPlaying = !this.IsPlaying;
              };
        }


        public bool IsFullScreen
        {
            get { return (bool)GetValue(IsFullScreenProperty); }
            set { SetValue(IsFullScreenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFullScreenProperty =
            DependencyProperty.Register("IsFullScreen", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));
        private Slider _media_slider;
        private VlcControl _vlc;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._media_slider = Template.FindName("media_slider", this) as Slider;
            this._media_slider.ValueChanged += media_slider_ValueChanged;
        }


        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPlayingProperty =
            DependencyProperty.Register("IsPlaying", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {
                     if (n)
                     {
                         control.Play();
                     }
                     else
                     {
                         control.Pause();
                     }
                 }

             }));

        private void Play()
        {
            if (this._vlc?.SourceProvider?.MediaPlayer == null) return;

            this._vlc?.SourceProvider.MediaPlayer.Play();
        }

        private void Pause()
        {
            if (this._vlc?.SourceProvider?.MediaPlayer == null) return;

            this._vlc?.SourceProvider.MediaPlayer.Pause();
        }

        public void Stop()
        {
            //this._toggle_play.IsChecked = true;

            this.IsPlaying = false;

            if (this._vlc?.SourceProvider?.MediaPlayer == null) return;

            this._vlc.SourceProvider.MediaPlayer.PositionChanged -= MediaPlayer_PositionChanged;

            this._vlc.SourceProvider.MediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;

            this._media_slider.Value = 0;

            Task.Run(() => this._vlc?.Dispose());
        }

        //声明和注册路由事件
        public static readonly RoutedEvent ShootCutRoutedEvent =
            EventManager.RegisterRoutedEvent("ShootCut", RoutingStrategy.Bubble, typeof(EventHandler<ObjectRoutedEventArgs<string>>), typeof(VlcPlayer));
        //CLR事件包装
        public event RoutedEventHandler ShootCut
        {
            add { this.AddHandler(ShootCutRoutedEvent, value); }
            remove { this.RemoveHandler(ShootCutRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnShootCut(string uri)
        {
            ObjectRoutedEventArgs<string> args = new ObjectRoutedEventArgs<string>(ShootCutRoutedEvent, this);

            args.Entity = uri;

            this.RaiseEvent(args);
        }

        private string ShotCutPat { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HeBianGu", Assembly.GetExecutingAssembly().GetName().Name, "ShootCut");

        public async Task<string> BeginShootCut()
        {
            if (this._vlc == null) 
                return null;

            string name = Path.GetFileNameWithoutExtension(this.VedioSource?.LocalPath);

            string timespan = TimeSpan.FromMilliseconds(this._vlc.SourceProvider.MediaPlayer.Time).Ticks.ToString();

            string filePath = Path.Combine(ShotCutPat, name, timespan + ".jpg");

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            return await Task.Run(() =>
            {
                this._vlc.SourceProvider.MediaPlayer.TakeSnapshot(new FileInfo(filePath));

                return filePath;
            });
        }

        /// <summary> 当前播放的路径 </summary>
        public Uri VedioSource
        {
            get { return (Uri)GetValue(VedioSourceProperty); }
            set { SetValue(VedioSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VedioSourceProperty =
            DependencyProperty.Register("VedioSource", typeof(Uri), typeof(VlcPlayer), new PropertyMetadata(default(Uri), (d, e) =>
            {
                VlcPlayer control = d as VlcPlayer;

                if (control == null) return;

                Uri config = e.NewValue as Uri;
                control.InitVlc();
                control._vlc.SourceProvider.MediaPlayer.Play(config);
                control.IsPlaying = true;
            }));

        private void InitVlc()
        {

            if (this._vlc?.SourceProvider?.MediaPlayer != null)
            {
                this._vlc.SourceProvider.MediaPlayer.PositionChanged -= MediaPlayer_PositionChanged;

                this._vlc.SourceProvider.MediaPlayer.LengthChanged -= MediaPlayer_LengthChanged;
            }


            this._vlc = new VlcControl();

            this.Content = this._vlc;

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            this._vlc.SourceProvider.CreatePlayer(libDirectory/* pass your player parameters here */);

            //this.vlccontrol.SourceProvider.MediaPlayer.Video.IsMouseInputEnabled = false;
            //this.vlccontrol.SourceProvider.MediaPlayer.Video.IsKeyInputEnabled = false;

            this._vlc.SourceProvider.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
            this._vlc.SourceProvider.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
        }

        private void MediaPlayer_LengthChanged(object sender, VlcMediaPlayerLengthChangedEventArgs e)
        {
            this.InitSlider();
        }

        private void MediaPlayer_PositionChanged(object sender, VlcMediaPlayerPositionChangedEventArgs e)
        {
            this.RefreshSlider();
        }

        /// <summary> 初始化播放进度条 </summary>
        private void InitSlider()
        {
            if (this._vlc?.SourceProvider?.MediaPlayer == null) return;

            this.Dispatcher.Invoke(() =>
            {
                this._media_slider.Maximum = this._vlc.SourceProvider.MediaPlayer.Length;
            });

        }

        /// <summary> 刷新当前进度 </summary>
        private void RefreshSlider()
        {
            this.Dispatcher.Invoke(() =>
            {
                this._media_slider.ValueChanged -= this.media_slider_ValueChanged;

                this._media_slider.Value = this._vlc.SourceProvider.MediaPlayer == null ? 0 : this._vlc.SourceProvider.MediaPlayer.Time;

                this.Time = (long)this._media_slider.Value;

                this._media_slider.ValueChanged += this.media_slider_ValueChanged;
            });
        }

        private async void media_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            long value = (long)this._media_slider.Value;

            await Task.Run(() => this._vlc.SourceProvider.MediaPlayer.Time = value);

            this.Time = value;
        }


        public long Time
        {
            get { return (long)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(long), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(long), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is long o)
                 {

                 }

                 if (e.NewValue is long n)
                 {
                     control._media_slider.Value = n;
                 }
             }));

        //private void slider_sound_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    this.RefreshAudio();
        //}

        //void RefreshAudio()
        //{
        //    if (this._vlc?.SourceProvider?.MediaPlayer?.Audio == null) return;

        //    this._vlc.SourceProvider.MediaPlayer.Audio.Volume = (int)this._sound_slider.Value;
        //}


        public int Volume
        {
            get { return (int)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VolumeProperty =
            DependencyProperty.Register("Volume", typeof(int), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(int), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {
                     if (control._vlc?.SourceProvider?.MediaPlayer?.Audio == null)
                         return;

                     control._vlc.SourceProvider.MediaPlayer.Audio.Volume = n;
                 }

             }));


        public bool UseRight
        {
            get { return (bool)GetValue(UseRightProperty); }
            set { SetValue(UseRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseRightProperty =
            DependencyProperty.Register("UseRight", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));

    }


    public class VlcPlayerCommands
    {
        public static RoutedUICommand OpenFile = new RoutedUICommand();
        public static RoutedUICommand ShootCut = new RoutedUICommand();
        public static RoutedUICommand Stop = new RoutedUICommand();
        public static RoutedUICommand SpeedUp = new RoutedUICommand();
        public static RoutedUICommand SpeedDown = new RoutedUICommand();
        public static RoutedUICommand FullScreen = new RoutedUICommand();
    }
}
