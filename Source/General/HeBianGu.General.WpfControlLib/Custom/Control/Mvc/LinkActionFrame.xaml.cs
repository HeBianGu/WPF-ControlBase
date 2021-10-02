using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 自定义导航框架 </summary>

    public class LinkActionFrame : ContentControl
    {
        static LinkActionFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkActionFrame), new FrameworkPropertyMetadata(typeof(LinkActionFrame)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        public ILinkActionBase LinkAction
        {
            get { return (ILinkActionBase)GetValue(LinkActionProperty); }
            set { SetValue(LinkActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionProperty =
            DependencyProperty.Register("LinkAction", typeof(ILinkActionBase), typeof(LinkActionFrame), new PropertyMetadata(default(LinkAction), (d, e) =>
              {
                  LinkActionFrame control = d as LinkActionFrame;

                  if (control == null) return;

                  if (e.NewValue is ILinkActionBase)
                  {
                      ILinkActionBase config = e.NewValue as ILinkActionBase;

                      control.RefreshLinkAction(config);
                  }

              }));

        Random random = new Random();

        async Task RefreshLinkAction(ILinkActionBase linkActionBase)
        {
            try
            {
                if (this.UseRandomWipe)
                {
                    this.CurrentWipe = this.RandomWipes[random.Next(this.RandomWipes.Count)];
                }
                else
                {
                    if (linkActionBase is LinkAction linkAction)
                    {
                        this.CurrentWipe = linkAction.TransitionWipe ?? new CircleWipe();
                    }
                }

                var result = await Task.Run(() =>
                {
                    return linkActionBase?.CreateActionResult();
                });

                //var result = await MessageService.ShowWaittingResultMessge(() =>
                // {
                //     return linkActionBase?.ActionResult().Result;
                // });


                await this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
                  {
                      if (this.Content == result?.View)
                      {
                          //this.Content = new Button() { Visibility = Visibility.Collapsed };
                          this.Content = result?.View;
                      }
                      else
                      {
                          this.Content = result?.View;
                      }

                      //this.Content = result?.View;
                  }));

                // this.Dispatcher.Invoke(() =>
                //{
                //    if (this.Content == result?.View)
                //    {
                //        this.Content = new Button() { Visibility = Visibility.Collapsed };
                //        this.Content = result?.View;
                //    }
                //    else
                //    {
                //        this.Content = result?.View;
                //    }
                //});
            }
            catch (Exception ex)
            {
                this.Content = ex;
            }
        }

        public ITransitionWipe CurrentWipe
        {
            get { return (ITransitionWipe)GetValue(CurrentWipeProperty); }
            set { SetValue(CurrentWipeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentWipeProperty =
            DependencyProperty.Register("CurrentWipe", typeof(ITransitionWipe), typeof(LinkActionFrame), new PropertyMetadata(new CircleWipe(), (d, e) =>
             {
                 LinkActionFrame control = d as LinkActionFrame;

                 if (control == null) return;

                 ITransitionWipe config = e.NewValue as ITransitionWipe;

             }));


        public ObservableCollection<ITransitionWipe> RandomWipes
        {
            get { return (ObservableCollection<ITransitionWipe>)GetValue(RandomWipesProperty); }
            set { SetValue(RandomWipesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RandomWipesProperty =
            DependencyProperty.Register("RandomWipes", typeof(ObservableCollection<ITransitionWipe>), typeof(LinkActionFrame), new PropertyMetadata(new ObservableCollection<ITransitionWipe>()
            {
                 new CircleWipe(),
                 new SlideWipe(){ Direction=SlideDirection.Left},
                 new SlideWipe(){ Direction=SlideDirection.Right},
                 new SlideWipe(){ Direction=SlideDirection.Down},
                 new SlideWipe(){ Direction=SlideDirection.Up},
                 new SlideOutWipe(),
                 new FadeWipe()
             }));


        public bool UseRandomWipe
        {
            get { return (bool)GetValue(UseRandomWipeProperty); }
            set { SetValue(UseRandomWipeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseRandomWipeProperty =
            DependencyProperty.Register("UseRandomWipe", typeof(bool), typeof(TransitionEffectBase), new PropertyMetadata(true));


    }

    /// <summary> 带有数据集选项的导航框架 </summary>
    [ContentProperty("LinkActions")]
    public class LinkActionsContianer : LinkActionFrame
    {
        public LinkActionCollection LinkActions
        {
            get { return (LinkActionCollection)GetValue(LinkActionsProperty); }
            set { SetValue(LinkActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionsProperty =
            DependencyProperty.Register("LinkActions", typeof(LinkActionCollection), typeof(LinkActionsContianer), new PropertyMetadata(new LinkActionCollection(), (d, e) =>
             {
                 LinkActionsContianer control = d as LinkActionsContianer;

                 if (control == null) return;

                 LinkActionCollection config = e.NewValue as LinkActionCollection;

             }));



        public Style ListStyle
        {
            get { return (Style)GetValue(ListStyleProperty); }
            set { SetValue(ListStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ListStyleProperty =
            DependencyProperty.Register("ListStyle", typeof(Style), typeof(LinkActionsContianer), new PropertyMetadata(default(Style), (d, e) =>
             {
                 LinkActionsContianer control = d as LinkActionsContianer;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

    }

    
    public class LinkActionCollection: ObservableCollection<ILinkActionBase>
    {

    }

}
