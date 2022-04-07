// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    /// <summary> 带有随机标识的动画 </summary>
    public class RandomDoubleAnimation : DoubleAnimation
    {
        public bool UseFrom { get; set; } = false;

        public bool UseTo { get; set; } = false;

        public static Random _random = new Random();

        public double Max { get; set; }

        public double Min { get; set; }

        private double GetRandom()
        {
            return _random.NextDouble() * (this.Max - this.Min) + this.Min;
        }

        public DoubleAnimation ConvertTo()
        {
            DoubleAnimation animation = this.Clone();

            if (this.UseFrom)
            {
                //  Do ：From随机
                animation.From = this.GetRandom();
            }

            if (this.UseTo)
            {
                //  Do ：To随机
                animation.To = this.GetRandom();
            }

            return animation;

        }
    }
}
