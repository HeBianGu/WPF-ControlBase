// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Step
{
    public class Step : ListBox, IDemo
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Step), "S.StepState.Default");

        static Step()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Step), new FrameworkPropertyMetadata(typeof(Step)));
        }

        public void Build()
        {
            List<StepItem> stepItems = new List<StepItem>();

            stepItems.Add(new StepItem() { DisplayName = "1", Message = "准备开始" });
            stepItems.Add(new StepItem() { DisplayName = "2", Message = "步骤一" });
            stepItems.Add(new StepItem() { DisplayName = "3", Message = "步骤二" });
            stepItems.Add(new StepItem() { DisplayName = "4", Message = "步骤三" });
            stepItems.Add(new StepItem() { DisplayName = "5", Message = "步骤四" });
            stepItems.Add(new StepItem() { DisplayName = "6", Message = "步骤五" });
            stepItems.Add(new StepItem() { DisplayName = "7", Message = "完成" });

            this.ItemsSource = stepItems;
        }


        public double LineWidth
        {
            get { return (double)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth", typeof(double), typeof(Step), new FrameworkPropertyMetadata(100.0, (d, e) =>
             {
                 Step control = d as Step;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));

    }

    //public class StepItemStateTemplateSelector : DataTemplateSelector
    //{
    //    public DataTemplate UnkownTempate { get; set; }

    //    public DataTemplate RunningTempate { get; set; }

    //    public DataTemplate ErrorTempate { get; set; }

    //    public DataTemplate SuccessTempate { get; set; }
    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {
    //        if (item is StepItem stepItem)
    //        {
    //            if (stepItem.State == -1) return ErrorTempate;

    //            if (stepItem.State == 0) return UnkownTempate;

    //            if (stepItem.State == 1) return RunningTempate;

    //            if (stepItem.State == 2) return SuccessTempate;


    //            throw new ArgumentException("未识别状态" + stepItem.State);
    //        }

    //        return null;
    //    }
    //}

    /// <summary> 说明</summary>
    public class StepItem : DisplayerViewModelBase
    {
        #region - 属性 -
        private string _displayName;
        /// <summary> 说明  </summary>
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }


        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        private int _percent;
        /// <summary> 说明  </summary>
        public int Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                RaisePropertyChanged("Percent");
            }
        }

        private int _state = 0;
        /// <summary> -1 错误 0 未开始 1 正在运行 2 运行成功  </summary>
        public int State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }

        #endregion
    }


}
