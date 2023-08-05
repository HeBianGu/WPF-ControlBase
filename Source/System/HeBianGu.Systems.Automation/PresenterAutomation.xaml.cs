// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Component;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Systems.Automation
{
    public partial class PresenterAutomation : System.Windows.Controls.Control
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PresenterAutomation), "S.PresenterAutomation.Default");

        static PresenterAutomation()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PresenterAutomation), new FrameworkPropertyMetadata(typeof(PresenterAutomation)));
        }


        //[Command]
        //[HotKey(Key = Key.A, ModifierKeys = ModifierKeys.Control)]
        //[Style(Icon = "\xe8a5")]
        //[Display(Name = "添加", GroupName = "操作", Order = 5, Prompt = "操作")]

        public PresenterAutomation()
        {

            List<IComponentNode> result = new List<IComponentNode>();

            List<IRepositoryPresenter> finds = PresenterPresenter.Instance.GetRepositoryPresenters();

            foreach (IRepositoryPresenter find in finds)
            {
                PropertyInfo[] ps = find.GetType().GetProperties();

                foreach (PropertyInfo p in ps)
                {
                    CommandAttribute command = p.GetCustomAttribute<CommandAttribute>();

                    if (command == null) continue;

                    DisplayAttribute display = p.GetCustomAttribute<DisplayAttribute>();

                    if (display == null) continue;

                    result.Add(new ActionTest() { Name = display.Name });
                }
            }

            this.Components = result.ToObservable();


            CommandBinding binding = new CommandBinding(Commander.Close);

            binding.Executed += (l, k) =>
              {
                  MessageProxy.Container.Close();
              };

            this.CommandBindings.Add(binding);
        }

        public ObservableCollection<IComponentNode> Components
        {
            get { return (ObservableCollection<IComponentNode>)GetValue(ComponentsProperty); }
            set { SetValue(ComponentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ComponentsProperty =
            DependencyProperty.Register("Components", typeof(ObservableCollection<IComponentNode>), typeof(PresenterAutomation), new FrameworkPropertyMetadata(new ObservableCollection<IComponentNode>(), (d, e) =>
             {
                 PresenterAutomation control = d as PresenterAutomation;

                 if (control == null) return;

                 if (e.OldValue is ObservableCollection<IComponentNode> o)
                 {

                 }

                 if (e.NewValue is ObservableCollection<IComponentNode> n)
                 {

                 }

             }));
    }
}
