using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// MessageUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MessageUserControl : UserControl
    {

        public MessageUserControl()
        {
            InitializeComponent();
        }
        private void sumitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        /// <summary> 消息 </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageUserControl), new FrameworkPropertyMetadata("", (d, e) =>
             {
                 MessageUserControl control = d as MessageUserControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 if (config == null) config = string.Empty;

                 if (control.messageText == null) return;

                 control.messageText.Text = config;
                 

             }));


        /// <summary> 标题 </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MessageUserControl), new FrameworkPropertyMetadata("温馨提示！", (d, e) =>
             {
                 MessageUserControl control = d as MessageUserControl;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 if (config == null) config = string.Empty;

                 if (control.title == null) return;

                 control.title.Content = config;

             }));



        /// <summary> 任务按钮 </summary>
        public BindingActions Actions
        {
            get { return (BindingActions)GetValue(ActionsProperty); }
            set { SetValue(ActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.RegisterAttached("Actions", typeof(BindingActions), typeof(MessageUserControl), new FrameworkPropertyMetadata(null, (d, e) =>
             {
                 MessageUserControl control = d as MessageUserControl;

                 if (control == null) return;

                 BindingActions config = e.NewValue as BindingActions;

             }));



        public List<Tuple<string, Action<MessageUserControl>>> BindingActions
        {
            get { return (List<Tuple<string, Action<MessageUserControl>>>)GetValue(BindingActionsProperty); }
            set { SetValue(BindingActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingActionsProperty =
            DependencyProperty.Register("BindingActions", typeof(List<Tuple<string, Action<MessageUserControl>>>), typeof(MessageUserControl), new PropertyMetadata(GetDefault, (d, e) =>
             {
                 MessageUserControl control = d as MessageUserControl;

                 if (control == null) return;

                 List<Tuple<string, Action<MessageUserControl>>> config = e.NewValue as List<Tuple<string, Action<MessageUserControl>>>;

                 if (config == null) return;

                 control.BuildButtons(config.ToArray());


             }));


        public static List<Tuple<string, Action<MessageUserControl>>> GetDefault
        {
            get
            {
                return new List<Tuple<string, Action<MessageUserControl>>>() { new Tuple<string, Action<MessageUserControl>>("确定", l => l.Visibility = Visibility.Collapsed) };
            }
        }

        public void BuildButtons(params Tuple<string, Action<MessageUserControl>>[] acts)
        {
            int result = -1;

            List<Label> ls = new List<Label>();

            if (acts == null || acts.Length == 0) return;

            this.actionPanel.Children.Clear();

            // Todo ：根据事件加载按钮 2017-07-28 10:46:07
            foreach (var item in acts)
            {
                FButton f = new FButton();
                f.Content = item.Item1;
                f.MinWidth = 300;
                f.Width = double.NaN;
                f.Height = 80;
                f.Margin = new Thickness(0, 0, 20, 0);
                f.FIcon = "";
                //f.SetPressed(false);

                f.Click += (object sender, RoutedEventArgs e) =>
                {
                    if (item.Item2 != null)
                    {
                        item.Item2(this);
                    }

                    result = acts.ToList().IndexOf(item);

                    this.Visibility = Visibility.Collapsed;
                };

                this.actionPanel.Children.Add(f);
            }


        }


    }


    public class BindingActions
{
    private List<Tuple<string, Action<MessageWindow>>> _Action;

    public List<Tuple<string, Action<MessageWindow>>> Action
    {
        get { return _Action; }
        set { _Action = value; }
    }

}


}


