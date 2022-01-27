using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace HeBianGu.Base.WpfBase
{
    static partial class Cattach
    {
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
            "Behaviors", typeof(Behaviors), typeof(Cattach), new FrameworkPropertyMetadata(new Behaviors(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBehaviorsChanged));

        public static Behaviors GetBehaviors(DependencyObject d)
        {
            return (Behaviors)d.GetValue(BehaviorsProperty);
        }

        public static void SetBehaviors(DependencyObject obj, Behaviors value)
        {
            obj.SetValue(BehaviorsProperty, value);
        }

        static void OnBehaviorsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            Behaviors o = args.OldValue as Behaviors;

            Behaviors n = args.NewValue as Behaviors;

            BehaviorCollection bc = Interaction.GetBehaviors(sender);

            //  Do ：先删除
            if (o != null)
            {
                foreach (var b in o)
                {
                    var behavior = bc.FirstOrDefault(beh => beh.GetType() == b.GetType());

                    if (behavior != null)
                        bc.Remove(behavior);
                }
            }

            //  Do ：再更新
            if (n != null)
            {
                foreach (var b in n)
                {
                    var behavior = bc.FirstOrDefault(beh => beh.GetType() == b.GetType());

                    var instance = Activator.CreateInstance(b.GetType()) as Behavior;

                    if (behavior != null)
                    {
                        bc.Remove(behavior);
                    }


                    foreach (var property in b.GetType().GetProperties())
                    {
                        if (property.CanRead && property.CanWrite)
                        {
                            property.SetValue(instance, property.GetValue(b));
                        }
                    }

                    bc.Add(instance);
                }
            }

        } 
    }

    public class Behaviors : ObservableCollection<Behavior>
    {

    }

}
