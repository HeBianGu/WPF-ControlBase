// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.MessageContainer
{
    public class DailogMessage : DailogMessageBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DailogMessage), "S.DailogMessage.Default");

        static DailogMessage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DailogMessage), new FrameworkPropertyMetadata(typeof(DailogMessage)));
        }

        public static RoutedUICommand SumitCommand = new RoutedUICommand() { Text = "确定" };

        public DailogMessage()
        {
            {
                CommandBinding binding = new CommandBinding(SumitCommand, (l, k) =>
                {
                    //  Do ：点击确定先检查是否合法
                    if (this.IsMatch?.Invoke(this) == true)
                    {
                        this.Close();
                    }
                });

                this.CommandBindings.Add(binding);
            }
        }

        public Predicate<DailogMessage> IsMatch { get; set; }
    }
}
