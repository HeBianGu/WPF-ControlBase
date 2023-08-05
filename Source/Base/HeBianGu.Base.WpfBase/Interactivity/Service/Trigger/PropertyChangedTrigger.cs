// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    using System.Windows;


    /// <summary>
    /// Represents a trigger that performs actions when the bound data have changed. 
    /// </summary>
    public class PropertyChangedTrigger : TriggerBase<DependencyObject>
    {
        public static readonly DependencyProperty BindingProperty = DependencyProperty.Register("Binding", typeof(object), typeof(PropertyChangedTrigger), new PropertyMetadata(OnBindingChanged));
        /// <summary>
        /// A binding object that the trigger will listen to, and that causes the trigger to fire when it changes.
        /// </summary>
        public object Binding
        {
            get { return this.GetValue(BindingProperty); }
            set { this.SetValue(BindingProperty, value); }
        }

        /// <summary>
        /// Called when the binding property has changed. 
        /// </summary>
        /// <param name="args"><see cref="T:System.Windows.DependencyPropertyChangedEventArgs"/> argument.</param>
        protected virtual void EvaluateBindingChange(object args)
        {
            // Fire the actions when the binding data has changed
            this.InvokeActions(args);
        }

        /// <summary>
        /// Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.PreviewInvoke += this.OnPreviewInvoke;
        }

        /// <summary>
        /// Called when the trigger is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            this.PreviewInvoke -= this.OnPreviewInvoke;
            this.OnDetaching();
        }

        private void OnPreviewInvoke(object sender, PreviewInvokeEventArgs e)
        {
            DataBindingHelper.EnsureDataBindingOnActionsUpToDate(this);
        }

        private static void OnBindingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            PropertyChangedTrigger propertyChangedTrigger = (PropertyChangedTrigger)sender;
            propertyChangedTrigger.EvaluateBindingChange(args);
        }

    }
}