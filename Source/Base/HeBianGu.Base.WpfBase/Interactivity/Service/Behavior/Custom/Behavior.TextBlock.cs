// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public class QinBehavior : Behavior<TextBlock>
    {
        #region 成员变量
        /// <summary>
        /// 鼠标位置
        /// </summary>
        private Point m_Position;
        /// <summary>
        /// 部件容器
        /// </summary>
        private StackPanel m_PartContainer;
        /// <summary>
        /// 父级容器
        /// </summary>
        private DependencyObject m_Parent;
        /// <summary>
        /// 文字所在父级容器集合的索引
        /// </summary>
        private int m_Index;
        #endregion

        #region 依赖属性
        #region MaxOffset
        public static readonly DependencyProperty MaxOffsetProperty = DependencyProperty.Register(
            "MaxOffset"
            , typeof(double)
            , typeof(QinBehavior)
            , new FrameworkPropertyMetadata(20.00)
            );
        /// <summary>
        /// 最大偏移量
        /// </summary>
        public double MaxOffset
        {
            get
            {
                return (double)this.GetValue(MaxOffsetProperty);
            }
            set
            {
                this.SetValue(MaxOffsetProperty, value);
            }
        }
        #endregion
        #region DiffOffset
        public static readonly DependencyProperty DiffOffsetProperty = DependencyProperty.Register(
            "DiffOffset"
            , typeof(double)
            , typeof(QinBehavior)
            , new FrameworkPropertyMetadata(1.00)
            );
        /// <summary>
        /// 相邻字符的递减量
        /// </summary>
        public double DiffOffset
        {
            get
            {
                return (double)this.GetValue(DiffOffsetProperty);
            }
            set
            {
                this.SetValue(DiffOffsetProperty, value);
            }
        }
        #endregion
        #region Duration
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            "Duration"
            , typeof(double)
            , typeof(QinBehavior)
            , new FrameworkPropertyMetadata(300.00)
            );
        /// <summary>
        /// 动画时间(毫秒)
        /// </summary>
        public double Duration
        {
            get
            {
                return (double)this.GetValue(DurationProperty);
            }
            set
            {
                this.SetValue(DurationProperty, value);
            }
        }
        #endregion
        #endregion

        #region 重写方法
        #region 注册事件
        /// <summary>
        /// 注册事件
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        }
        #endregion
        #region 注销事件
        /// <summary>
        /// 注销事件
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
        }
        #endregion
        #endregion

        #region 事件
        #region 鼠标进入
        private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
        {

            /*
             * 将文字拆分成一个个TextBlock(这里称为部件 Part),放入到一个StackPanel中(这里成为部件容器 m_PartContainer),StackPanel水平排列
             * 用StackPanel把文字替换掉
             */

            m_Parent = VisualTreeHelper.GetParent(this.AssociatedObject);
            m_Position = e.GetPosition((IInputElement)m_Parent);

            //部件容器
            m_PartContainer = new StackPanel();
            m_PartContainer.Orientation = Orientation.Horizontal;
            //布局属性放到容器上
            m_PartContainer.Width = this.AssociatedObject.Width;
            m_PartContainer.Height = this.AssociatedObject.Height;
            if (this.AssociatedObject.HorizontalAlignment == HorizontalAlignment.Stretch)
                m_PartContainer.HorizontalAlignment = HorizontalAlignment.Left;
            else
                m_PartContainer.HorizontalAlignment = this.AssociatedObject.HorizontalAlignment;
            if (this.AssociatedObject.VerticalAlignment == VerticalAlignment.Stretch)
                m_PartContainer.VerticalAlignment = VerticalAlignment.Top;
            else
                m_PartContainer.VerticalAlignment = this.AssociatedObject.VerticalAlignment;
            m_PartContainer.Margin = this.AssociatedObject.Margin;

            //拆分文字
            string text = this.AssociatedObject.Text;
            for (int i = 0; i < text.Length; i++)
            {
                TextBlock part = new TextBlock();
                //字体属性放到部件上
                part.Foreground = this.AssociatedObject.Foreground;
                part.FontFamily = this.AssociatedObject.FontFamily;
                part.FontSize = this.AssociatedObject.FontSize;
                part.FontWeight = this.AssociatedObject.FontWeight;
                part.FontStretch = this.AssociatedObject.FontStretch;
                part.FontStyle = this.AssociatedObject.FontStyle;

                part.RenderTransform = new TranslateTransform();
                part.MouseMove += part_MouseMove;
                part.MouseLeave += part_MouseLeave;
                part.Text = text.Substring(i, 1);
                m_PartContainer.Children.Add(part);
            }

            //用StackPanel把文字替换掉
            if (m_Parent.GetType().IsSubclassOf(typeof(Decorator)))
                ((Decorator)m_Parent).Child = m_PartContainer;

            if (m_Parent.GetType().IsSubclassOf(typeof(Panel)))
            {
                if (m_Parent.GetType() == typeof(Grid))
                {
                    Grid.SetColumn(m_PartContainer, Grid.GetColumn(this.AssociatedObject));
                    Grid.SetRow(m_PartContainer, Grid.GetRow(this.AssociatedObject));
                }
                if (m_Parent.GetType() == typeof(DockPanel))
                {
                    DockPanel.SetDock(m_PartContainer, DockPanel.GetDock(this.AssociatedObject));
                }
                if (m_Parent.GetType() == typeof(Canvas))
                {
                    Canvas.SetLeft(m_PartContainer, Canvas.GetLeft(this.AssociatedObject));
                    Canvas.SetTop(m_PartContainer, Canvas.GetTop(this.AssociatedObject));
                }
                m_Index = ((Panel)m_Parent).Children.IndexOf(this.AssociatedObject);
                ((Panel)m_Parent).Children.Remove(this.AssociatedObject);
                ((Panel)m_Parent).Children.Insert(m_Index, m_PartContainer);
            }
        }
        #endregion
        #region 部件鼠标移动
        private void part_MouseMove(object sender, MouseEventArgs e)
        {
            /*
             * 文字部件朝鼠标移动的方向移动
             * 附近的文字部件移动距离递减,最小为0
             */

            //部件移动
            Point curPosition = e.GetPosition((IInputElement)m_Parent);
            double offset = curPosition.Y - m_Position.Y;
            if (offset > 0 && offset > MaxOffset)
                offset = MaxOffset;
            if (offset < 0 && offset < -MaxOffset)
                offset = -MaxOffset;
            TextBlock part = (TextBlock)sender;
            TranslateTransform ttf = (TranslateTransform)part.RenderTransform;
            ttf.Y = offset;
            //两侧的部件移动
            int index = m_PartContainer.Children.IndexOf(part);
            if (offset > 0)
            {
                double flag = offset;
                int i = 1;
                while (flag > 0)
                {
                    flag -= DiffOffset;
                    if (index + i < m_PartContainer.Children.Count)
                        ((TranslateTransform)(((TextBlock)(m_PartContainer.Children[index + i])).RenderTransform)).Y = flag;
                    if (index - i >= 0)
                        ((TranslateTransform)(((TextBlock)(m_PartContainer.Children[index - i])).RenderTransform)).Y = flag;
                    i++;
                }
            }
            if (offset < 0)
            {
                double flag = offset;
                int i = 1;
                while (flag < 0)
                {
                    flag += DiffOffset;
                    if (index + i < m_PartContainer.Children.Count)
                        ((TranslateTransform)(((TextBlock)(m_PartContainer.Children[index + i])).RenderTransform)).Y = flag;
                    if (index - i >= 0)
                        ((TranslateTransform)(((TextBlock)(m_PartContainer.Children[index - i])).RenderTransform)).Y = flag;
                    i++;
                }
            }
        }
        #endregion
        #region 部件鼠标离开
        private void part_MouseLeave(object sender, MouseEventArgs e)
        {
            /*
             * 缓动函数位移动画
             */

            Storyboard sb = StoryboardFactory.Create();
            sb.Duration = TimeSpan.FromMilliseconds(Duration);
            sb.Completed += sb_Completed;
            for (int i = 0; i < m_PartContainer.Children.Count; i++)
            {
                TextBlock part = (TextBlock)m_PartContainer.Children[i];
                if (((TranslateTransform)(part.RenderTransform)).Y == 0)
                    continue;

                DoubleAnimation da = new DoubleAnimation();
                da.To = 0;
                da.Duration = TimeSpan.FromMilliseconds(Duration);
                da.EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut };
                Storyboard.SetTarget(da, part);
                Storyboard.SetTargetProperty(da, new PropertyPath("(TextBlock.RenderTransform).(TranslateTransform.Y)"));
                sb.Children.Add(da);
            }
            sb.Begin();
        }
        #endregion
        #region 动画播放完成
        private void sb_Completed(object sender, EventArgs e)
        {
            /*
             * 用文字替换掉部件容器
             */

            if (m_Parent.GetType().IsSubclassOf(typeof(Decorator)))
                ((Decorator)m_Parent).Child = this.AssociatedObject;
            if (m_Parent.GetType().IsSubclassOf(typeof(Panel)))
            {
                ((Panel)m_Parent).Children.Remove(m_PartContainer);
                if (!((Panel)m_Parent).Children.Contains(this.AssociatedObject))
                    ((Panel)m_Parent).Children.Insert(m_Index, this.AssociatedObject);
            }
        }
        #endregion
        #endregion
    }
}
