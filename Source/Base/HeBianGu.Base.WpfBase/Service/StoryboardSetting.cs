// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
   
    public static class StoryboardSetting
    {
        [DefaultValue(20)]
        [Range(0, 60)]
        public static int DesiredFrameRate { get; set; } = 25;

    }

    public static class StoryboardFactory
    {
        public static Storyboard Create()
        {
            Storyboard storyboard = new Storyboard();
            Timeline.SetDesiredFrameRate(storyboard, StoryboardSetting.DesiredFrameRate);
            return storyboard;
        }

        public static DoubleAnimation CreateDoubleAnimation()
        {
            DoubleAnimation storyboard = new DoubleAnimation();
            Timeline.SetDesiredFrameRate(storyboard, StoryboardSetting.DesiredFrameRate);
            return storyboard;
        }
    }
}
