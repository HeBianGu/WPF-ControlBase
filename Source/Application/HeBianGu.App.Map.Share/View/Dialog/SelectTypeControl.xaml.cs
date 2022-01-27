using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Map.View.Dialog
{
    /// <summary>
    /// SelectTypeControl.xaml 的交互逻辑
    /// </summary>
    public partial class SelectTypeControl : UserControl
    {
        public SelectTypeControl()
        {
            InitializeComponent();
        }



        public TestViewModel Model
        {
            get { return (TestViewModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(TestViewModel), typeof(SelectTypeControl), new PropertyMetadata(default(TestViewModel), (d, e) =>
             {
                 SelectTypeControl control = d as SelectTypeControl;

                 if (control == null) return;

                 TestViewModel config = e.NewValue as TestViewModel;

             }));

    }
}
