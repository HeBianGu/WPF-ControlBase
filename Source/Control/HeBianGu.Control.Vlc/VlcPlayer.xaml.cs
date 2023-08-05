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
using System.Windows.Media;
using Vlc.DotNet.Core.Interops.Signatures;

namespace HeBianGu.Control.Vlc
{
    [TemplatePart(Name = "media_slider", Type = typeof(Slider))]
    [TemplatePart(Name = "PART_Vlc", Type = typeof(VlcControl))]
    public partial class VlcPlayer : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(VlcPlayer), "S.VlcPlayer.Default");
        public static ComponentResourceKey MouseOverKey => new ComponentResourceKey(typeof(VlcPlayer), "S.VlcPlayer.MouseOver");
        public static ComponentResourceKey ScrollViewerTransforKey => new ComponentResourceKey(typeof(VlcPlayer), "S.VlcPlayer.ScrollViewerTransfor");
        public static ComponentResourceKey NoneKey => new ComponentResourceKey(typeof(VlcPlayer), "S.VlcPlayer.None");


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

                    //if (VlcSetting.Instance.AutoDeleteShotCutFile)
                    //{
                    //    File.Delete(filePath);
                    //}

                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.FullScreen, async (l, k) =>
                {
                    Style temp = this.Style;

                    Action ShowDialg = () =>
                      {
                          FullWindow window = new FullWindow();
                          window.Content = this;
                          window.Owner = Application.Current.MainWindow;
                          this.IsFullScreen = true;
                          var key = VlcSetting.Instance.GetFullScreenKey();
                          this.Style = Application.Current.FindResource(key) as Style;
                          window.ShowDialog();
                          window.Content = null;
                      };

                    Action<Style> Close = s =>
                     {
                         this.Style = s;
                         this.IsFullScreen = false;
                     };

                    if (this.Parent is Panel panel)
                    {
                        int index = panel.Children.IndexOf(this);
                        panel.Children.RemoveAt(index);
                        ShowDialg();
                        panel.Children.Insert(index, this);
                        Close(temp);
                    }

                    if (this.Parent is ContentControl control)
                    {
                        ShowDialg();
                        control.Content = this;
                        Close(temp);
                    }
                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.PlayMode, (l, k) =>
                 {
                     this.IsPlaying = !this.IsPlaying;
                 });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.SpeedUp, (l, k) =>
                {
                    this.Rate = Math.Min((float)Math.Round(this._vlc.SourceProvider.MediaPlayer.Rate * (float)1.2, 2), VlcSetting.Instance.MaxRate);
                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.SpeedDown, (l, k) =>
                {
                    this.Rate = Math.Max((float)Math.Round(this._vlc.SourceProvider.MediaPlayer.Rate / (float)1.2, 2), VlcSetting.Instance.MinRate);
                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.PreviousFrame, (l, k) =>
                {
                    this.Time = Math.Max(0, this.Time - VlcSetting.Instance.FrameSpan * 1000);
                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.NextFrame, (l, k) =>
                {
                    this.Time = Math.Min(this._vlc.SourceProvider.MediaPlayer.Length, this.Time + VlcSetting.Instance.FrameSpan * 1000);
                });

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(VlcPlayerCommands.Stop, (l, k) =>
                {
                    this.Stop();
                });

                this.CommandBindings.Add(binding);
            }

        }

        public float Rate
        {
            get { return (float)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RateProperty =
            DependencyProperty.Register("Rate", typeof(float), typeof(VlcPlayer), new FrameworkPropertyMetadata(1.0f, (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is float o)
                 {

                 }

                 if (e.NewValue is float n)
                 {
                     control._vlc.SourceProvider.MediaPlayer.Rate = n;
                 }

             }));

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
            if (this._vlc?.SourceProvider?.MediaPlayer == null)
                return;
            if (this._vlc?.SourceProvider.MediaPlayer.State == MediaStates.Ended)
            {
                Task.Run(() =>
                {
                    this._vlc.SourceProvider.MediaPlayer.Stop();
                    this._vlc?.SourceProvider.MediaPlayer.Play();
                });
            }
            else
            {
                this._vlc?.SourceProvider.MediaPlayer.Play();
            }
        }

        private void Pause()
        {
            if (this._vlc?.SourceProvider?.MediaPlayer == null)
                return;
            this._vlc?.SourceProvider.MediaPlayer.Pause();
        }

        public void Stop()
        {
            this.IsPlaying = false;
            if (this._vlc?.SourceProvider?.MediaPlayer == null)
                return;

            Task.Run(() => this._vlc.SourceProvider.MediaPlayer.Stop());
            //this._vlc.SourceProvider.MediaPlayer.Stop();
            this._media_slider.Value = 0;
        }

        public void Clear()
        {
            this.IsPlaying = false;
            if (this._vlc?.SourceProvider?.MediaPlayer == null)
                return;
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

        public async Task<string> BeginShootCut()
        {
            if (this._vlc == null)
                return null;

            string name = Path.GetFileNameWithoutExtension(this.VedioSource?.LocalPath);

            string timespan = TimeSpan.FromMilliseconds(this._vlc.SourceProvider.MediaPlayer.Time).Ticks.ToString();

            string filePath = Path.Combine(VlcSetting.Instance.ShotCutPath, name, timespan + ".jpg");

            if (VlcSetting.Instance.IsSaveShotCutWithFile)
            {
                var folder = Path.GetDirectoryName(this.VedioSource?.LocalPath);
                filePath = Path.Combine(folder, name + "_" + timespan + ".jpg");
            }

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

                this.Rate = this._vlc.SourceProvider.MediaPlayer.Rate;
            }


            this._vlc = new VlcControl();

            this.Content = this._vlc;


            DirectoryInfo libDirectory = new DirectoryInfo(VlcSetting.Instance.LibvlcPath);

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


            await Task.Run(() =>
            {
                if (this._vlc.SourceProvider.MediaPlayer == null)
                    return;
                this._vlc.SourceProvider.MediaPlayer.Time = value;
            });

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


        public bool UseLeft
        {
            get { return (bool)GetValue(UseLeftProperty); }
            set { SetValue(UseLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseLeftProperty =
            DependencyProperty.Register("UseLeft", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(bool), (d, e) =>
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


        public bool UseBottom
        {
            get { return (bool)GetValue(UseBottomProperty); }
            set { SetValue(UseBottomProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBottomProperty =
            DependencyProperty.Register("UseBottom", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseOpenFile
        {
            get { return (bool)GetValue(UseOpenFileProperty); }
            set { SetValue(UseOpenFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseOpenFileProperty =
            DependencyProperty.Register("UseOpenFile", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseVolume
        {
            get { return (bool)GetValue(UseVolumeProperty); }
            set { SetValue(UseVolumeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseVolumeProperty =
            DependencyProperty.Register("UseVolume", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseFullScreen
        {
            get { return (bool)GetValue(UseFullScreenProperty); }
            set { SetValue(UseFullScreenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseFullScreenProperty =
            DependencyProperty.Register("UseFullScreen", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseShotCut
        {
            get { return (bool)GetValue(UseShotCutProperty); }
            set { SetValue(UseShotCutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseShotCutProperty =
            DependencyProperty.Register("UseShotCut", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UsePrevious
        {
            get { return (bool)GetValue(UsePreviousProperty); }
            set { SetValue(UsePreviousProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePreviousProperty =
            DependencyProperty.Register("UsePrevious", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseNext
        {
            get { return (bool)GetValue(UseNextProperty); }
            set { SetValue(UseNextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseNextProperty =
            DependencyProperty.Register("UseNext", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseBottomMouseOver
        {
            get { return (bool)GetValue(UseBottomMouseOverProperty); }
            set { SetValue(UseBottomMouseOverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseBottomMouseOverProperty =
            DependencyProperty.Register("UseBottomMouseOver", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(bool), (d, e) =>
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


        public bool UsePlay
        {
            get { return (bool)GetValue(UsePlayProperty); }
            set { SetValue(UsePlayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePlayProperty =
            DependencyProperty.Register("UsePlay", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UsePreviourFrame
        {
            get { return (bool)GetValue(UsePreviourFrameProperty); }
            set { SetValue(UsePreviourFrameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePreviourFrameProperty =
            DependencyProperty.Register("UsePreviourFrame", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public bool UseNextFrame
        {
            get { return (bool)GetValue(UseNextFrameProperty); }
            set { SetValue(UseNextFrameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseNextFrameProperty =
            DependencyProperty.Register("UseNextFrame", typeof(bool), typeof(VlcPlayer), new FrameworkPropertyMetadata(true, (d, e) =>
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


        public Brush ToolBackground
        {
            get { return (Brush)GetValue(ToolBackgroundProperty); }
            set { SetValue(ToolBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolBackgroundProperty =
            DependencyProperty.Register("ToolBackground", typeof(Brush), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        public Brush ToolForeground
        {
            get { return (Brush)GetValue(ToolForegroundProperty); }
            set { SetValue(ToolForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolForegroundProperty =
            DependencyProperty.Register("ToolForeground", typeof(Brush), typeof(VlcPlayer), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 VlcPlayer control = d as VlcPlayer;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }
                 if (e.NewValue is Brush n)
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
        public static RoutedUICommand PlayMode = new RoutedUICommand();

        public static RoutedUICommand NextFrame = new RoutedUICommand();
        public static RoutedUICommand PreviousFrame = new RoutedUICommand();
    }
}
