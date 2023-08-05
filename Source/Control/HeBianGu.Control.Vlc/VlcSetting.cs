// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Windows;

namespace HeBianGu.Control.Vlc
{
    [Displayer(Name = "视频控件", GroupName = SystemSetting.GroupControl)]
    public class VlcSetting : LazySettingInstance<VlcSetting>
    {
        private bool _isSaveShotCutWithFile = true;
        [Display(Name = "截图时保存到影片目录")]
        [DefaultValue(true)]
        public bool IsSaveShotCutWithFile
        {
            get { return _isSaveShotCutWithFile; }
            set
            {
                _isSaveShotCutWithFile = value;
                RaisePropertyChanged();
            }
        }

        private bool _autoDeleteShotCutFile = false;
        [Display(Name = "自动清除截图保存的数据")]
        [DefaultValue(true)]
        public bool AutoDeleteShotCutFile
        {
            get { return _autoDeleteShotCutFile; }
            set
            {
                _autoDeleteShotCutFile = value;
                RaisePropertyChanged();
            }
        }

        private string _shotCutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HeBianGu", Assembly.GetExecutingAssembly().GetName().Name, "ShootCut");
        [Display(Name = "截图保存的路径")]
        public string ShotCutPath
        {
            get { return _shotCutPath; }
            set
            {
                _shotCutPath = value;
                RaisePropertyChanged();
            }
        }


        private FullScreenType _fullScreenType;
        [Display(Name = "全屏播放时显示样式")]
        [DefaultValue(FullScreenType.MouseOver)]
        public FullScreenType FullScreenType
        {
            get { return _fullScreenType; }
            set
            {
                _fullScreenType = value;
                RaisePropertyChanged();
            }
        }

        private string _libvlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64");
        [Display(Name = "libvlc库地址")]
        public string LibvlcPath
        {
            get { return _libvlcPath; }
            set
            {
                _libvlcPath = value;
                RaisePropertyChanged();
            }
        }

        private float _maxRate = 3f;
        [Display(Name = "最大播放速度限制")]
        public float MaxRate
        {
            get { return _maxRate; }
            set
            {
                _maxRate = value;
                RaisePropertyChanged();
            }
        }

        private float _minRate = 0.1f;
        [Display(Name = "最小播放速度限制")]
        public float MinRate
        {
            get { return _minRate; }
            set
            {
                _minRate = value;
                RaisePropertyChanged();
            }
        }

        private int _frameSpan = 5;
        [Display(Name = "上一帧下一帧跳转间隔(s)")]
        public int FrameSpan
        {
            get { return _frameSpan; }
            set
            {
                _frameSpan = value;
                RaisePropertyChanged();
            }
        }


        public ComponentResourceKey GetFullScreenKey()
        {
            if (this.FullScreenType == FullScreenType.Default)
                return VlcPlayer.DefaultKey;
            if (this.FullScreenType == FullScreenType.None)
                return VlcPlayer.NoneKey;
            if (this.FullScreenType == FullScreenType.MouseOver)
                return VlcPlayer.MouseOverKey;
            if (this.FullScreenType == FullScreenType.ScrollViewerTransfor)
                return VlcPlayer.ScrollViewerTransforKey;

            throw new NotImplementedException();
        }

    }
}
