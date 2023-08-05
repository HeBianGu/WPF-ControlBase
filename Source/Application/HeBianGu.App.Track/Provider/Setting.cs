// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeBianGu.App.Track
{
    [Displayer(Name = "素材配置", GroupName = SystemSetting.GroupBase)]
    public class MaterialSetting : LazySettingInstance<MaterialSetting>
    {
        private Color _videoColor = Colors.Green;
        [Display(Name = "视频色")]
        public Color VideoColor
        {
            get { return _videoColor; }
            set
            {
                _videoColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _imageColor = Colors.Orange;
        [Display(Name = "图片色")]
        public Color ImageColor
        {
            get { return _imageColor; }
            set
            {
                _imageColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _audioColor = Colors.Purple;
        [Display(Name = "音频色")]
        public Color AudioColor
        {
            get { return _audioColor; }
            set
            {
                _audioColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _dmxdColor = Colors.Brown;
        [Display(Name = "DMX色")]
        public Color DmxColor
        {
            get { return _dmxdColor; }
            set
            {
                _dmxdColor = value;
                RaisePropertyChanged();
            }
        }
        private Color _udpColor = Colors.DarkBlue;
        [Display(Name = "UDP色")]
        public Color UdpColor
        {
            get { return _udpColor; }
            set
            {
                _udpColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _tcpColor = Colors.DeepPink;
        [Display(Name = "TCP色")]
        public Color TcpColor
        {
            get { return _tcpColor; }
            set
            {
                _tcpColor = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "总线调度设置", GroupName = SystemSetting.GroupBase)]
    public class DispatchSetting : LazySettingInstance<DispatchSetting>
    {
        private Byte _value1;
        [Display(Name = "子网号")]
        [DefaultValue(254)]
        public Byte Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged();
            }
        }

        private Byte _value2;
        [Display(Name = "设备号")]
        [DefaultValue(254)]
        public Byte Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                RaisePropertyChanged();
            }
        }
    }
    [Displayer(Name = "DMX解码器设置", GroupName = SystemSetting.GroupBase)]
    public class DMXSetting : LazySettingInstance<DMXSetting>
    {
        private string _value1;

        [Display(Name = "解码器IP")]
        [DefaultValue("192.168.10.100")]
        public string Value
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "UDP通用设置", GroupName = SystemSetting.GroupBase)]
    public class UDPSetting : LazySettingInstance<UDPSetting>
    {
        private string _value1;
        [Display(Name = "IP")]
        [DefaultValue("172.24.208,1")]
        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged();
            }
        }

        private uint _value2;
        [Display(Name = "端口号")]
        [DefaultValue(7000)]
        public uint Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "本机串口设置", GroupName = SystemSetting.GroupBase)]
    public class ComSetting : LazySettingInstance<ComSetting>
    {
        private string _value1;
        [Display(Name = "端口")]
        [DefaultValue("Com1")]
        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged();
            }
        }

        private uint _value2;
        [Display(Name = "波特率")]
        [DefaultValue(9600)]
        public uint Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                RaisePropertyChanged();
            }
        }

        private uint _value3;
        [Display(Name = "数据位")]
        [DefaultValue(8)]
        public uint Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
                RaisePropertyChanged();
            }
        }

        private string _value4;
        [Display(Name = "校验位")]
        [DefaultValue("EVEN")]
        public string Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
                RaisePropertyChanged();
            }
        }

        private uint _value5;
        [Display(Name = "停止位")]
        [DefaultValue(1)]
        public uint Value5
        {
            get { return _value5; }
            set
            {
                _value5 = value;
                RaisePropertyChanged();
            }
        }
    } 

    [Displayer(Name = "播放器设置", GroupName = SystemSetting.GroupBase)]
    public class ParamSetting : LazySettingInstance<ParamSetting>
    { 
        private PlayDisplayMode _playDisplayModel;
        [Display(Name = "播放节目时进入")]
        [DefaultValue(PlayDisplayMode.FullScreen)]
        public PlayDisplayMode PlayDisplayModel
        {
            get { return _playDisplayModel; }
            set
            {
                _playDisplayModel = value;
                RaisePropertyChanged();
            }
        }

        private PlayDisplayMode _startDisplayModel;
        [Display(Name = "软件启动时进入")]
        [DefaultValue(PlayDisplayMode.FullScreen)]
        public PlayDisplayMode StartDisplayModel
        {
            get { return _startDisplayModel; }
            set
            {
                _startDisplayModel = value;
                RaisePropertyChanged();
            }
        }
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum PlayDisplayMode
    {
        [Display(Name = "全屏模式")]
        FullScreen,
        [Display(Name = "非全屏模式")]
        NotFullScreen
    }
  
}
