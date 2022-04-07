// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> TabControl支持动态添加TabItem的行为，支持数据的添加，需要有无参构造函数 </summary>
    public class OpenTargetInTabControlAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// AllowMultiInstanceProperty
        /// </summary>
        public static readonly DependencyProperty AllowMultiInstanceProperty =
            DependencyProperty.Register("AllowMultiInstance", typeof(bool), typeof(OpenTargetInTabControlAction), new PropertyMetadata(true));

        ///// <summary>
        ///// TabItemHeaderTemplateProperty
        ///// </summary>
        //public static readonly DependencyProperty TabItemHeaderTemplateProperty =
        //    DependencyProperty.Register("TabItemHeaderTemplate", typeof(DataTemplate), typeof(OpenTargetInTabControlAction), new PropertyMetadata(null));

        /// <summary>
        /// TargetProperty
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
                            DependencyProperty.Register("Target", typeof(TabControl), typeof(OpenTargetInTabControlAction), new PropertyMetadata(null));

        ///// <summary>
        ///// TargetTitleProperty
        ///// </summary>
        //public static readonly DependencyProperty TargetTitleProperty =
        //    DependencyProperty.Register("TargetTitle", typeof(string), typeof(OpenTargetInTabControlAction), new PropertyMetadata(string.Empty));

        /// <summary>
        /// TargetTypeProperty
        /// </summary>
        public static readonly DependencyProperty TargetTypeProperty =
                    DependencyProperty.Register("TargetType", typeof(Type), typeof(OpenTargetInTabControlAction), new PropertyMetadata(null));

        /// <summary>
        /// Allow multi instance, the defalut value is true
        /// </summary>
        public bool AllowMultiInstance
        {
            get { return (bool)GetValue(AllowMultiInstanceProperty); }
            set { SetValue(AllowMultiInstanceProperty, value); }
        }

        ///// <summary>
        ///// The header template of the the new tab item
        ///// </summary>
        //public DataTemplate TabItemHeaderTemplate
        //{
        //    get { return (DataTemplate)GetValue(TabItemHeaderTemplateProperty); }
        //    set { SetValue(TabItemHeaderTemplateProperty, value); }
        //}

        /// <summary>
        /// The target TabControl
        /// </summary>
        public TabControl Target
        {
            get { return (TabControl)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        ///// <summary>
        ///// The title of the new TabItem
        ///// </summary>
        //public string TargetTitle
        //{
        //    get { return (string)GetValue(TargetTitleProperty); }
        //    set { SetValue(TargetTitleProperty, value); }
        //}

        /// <summary>
        /// The type of the control that would be added on the target TabControl
        /// </summary>
        public Type TargetType
        {
            get { return (Type)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        /// <summary>
        /// Overrides Invoke method
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            if (Target == null || TargetType == null)
            {
                throw new ArgumentNullException($"{Target} or {TargetType} cannot be null");
            }

            // if multi instance is not allow, then find whether it has been added or not
            System.Collections.Generic.List<TabItem> currentTabItems = Target.Items.OfType<TabItem>().ToList();
            if (currentTabItems.Count > 0 && !AllowMultiInstance)
            {
                foreach (TabItem item in currentTabItems)
                {
                    if (item.Content?.GetType() == TargetType)
                    {
                        if (Target.SelectedItem != item)
                        {
                            Target.SelectedItem = item;
                        }
                        else
                        {
                            // Todo: notify user the target tabitem has been selected already
                        }
                        return;
                    }
                }
            }

            object obj = Activator.CreateInstance(TargetType);

            if (obj == null)
            {
                throw new ArgumentException("Target type is not valid");
            }

            //Control control = obj as Control;
            //if (control == null)
            //{
            //    throw new ArgumentException("Cannot cast the target type to a control");
            //}

            TabItem newItem = new TabItem
            {
                Content = obj,
                Header = obj,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };

            //newItem.HeaderTemplate = TabItemHeaderTemplate;

            int index = Target.Items.Add(newItem);

            // select the new item
            Target.SelectedIndex = index;
        }
    }
}