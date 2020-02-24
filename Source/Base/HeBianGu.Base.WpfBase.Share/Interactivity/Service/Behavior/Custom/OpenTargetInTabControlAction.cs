using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// An action that can show a control (mostly User Control) on the target TabControl as a new TabItem.
    /// </summary>
    public class OpenTargetInTabControlAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// AllowMultiInstanceProperty
        /// </summary>
        public static readonly DependencyProperty AllowMultiInstanceProperty =
            DependencyProperty.Register("AllowMultiInstance", typeof(bool), typeof(OpenTargetInTabControlAction), new PropertyMetadata(true));

        /// <summary>
        /// TabItemHeaderTemplateProperty
        /// </summary>
        public static readonly DependencyProperty TabItemHeaderTemplateProperty =
            DependencyProperty.Register("TabItemHeaderTemplate", typeof(DataTemplate), typeof(OpenTargetInTabControlAction), new PropertyMetadata(null));

        /// <summary>
        /// TargetTabControlProperty
        /// </summary>
        public static readonly DependencyProperty TargetTabControlProperty =
                            DependencyProperty.Register("TargetTabControl", typeof(TabControl), typeof(OpenTargetInTabControlAction), new PropertyMetadata(null));

        /// <summary>
        /// TargetTitleProperty
        /// </summary>
        public static readonly DependencyProperty TargetTitleProperty =
            DependencyProperty.Register("TargetTitle", typeof(string), typeof(OpenTargetInTabControlAction), new PropertyMetadata(string.Empty));

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

        /// <summary>
        /// The header template of the the new tab item
        /// </summary>
        public DataTemplate TabItemHeaderTemplate
        {
            get { return (DataTemplate)GetValue(TabItemHeaderTemplateProperty); }
            set { SetValue(TabItemHeaderTemplateProperty, value); }
        }

        /// <summary>
        /// The target TabControl
        /// </summary>
        public TabControl TargetTabControl
        {
            get { return (TabControl)GetValue(TargetTabControlProperty); }
            set { SetValue(TargetTabControlProperty, value); }
        }

        /// <summary>
        /// The title of the new TabItem
        /// </summary>
        public string TargetTitle
        {
            get { return (string)GetValue(TargetTitleProperty); }
            set { SetValue(TargetTitleProperty, value); }
        }

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
            if (TargetTabControl == null || TargetType == null)
            {
                throw new ArgumentNullException($"{TargetTabControl} or {TargetType} cannot be null");
            }

            // if multi instance is not allow, then find whether it has been added or not
            var currentTabItems = TargetTabControl.Items.OfType<TabItem>().ToList();
            if (currentTabItems.Count > 0 && !AllowMultiInstance)
            {
                foreach (var item in currentTabItems)
                {
                    if (item.Content?.GetType() == TargetType)
                    {
                        if (TargetTabControl.SelectedItem != item)
                        {
                            TargetTabControl.SelectedItem = item;
                        }
                        else
                        {
                            // Todo: notify user the target tabitem has been selected already
                        }
                        return;
                    }
                }
            }

            var obj = Activator.CreateInstance(TargetType);

            if (obj == null)
            {
                throw new ArgumentException("Target type is not valid");
            }

            Control control = obj as Control;
            if (control == null)
            {
                throw new ArgumentException("Cannot cast the target type to a control");
            }

            var newItem = new TabItem
            {
                Content = control,
                Header = TargetTitle,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch
            };

            newItem.HeaderTemplate = TabItemHeaderTemplate;

            var index = TargetTabControl.Items.Add(newItem);

            // select the new item
            TargetTabControl.SelectedIndex = index;
        }
    }
}