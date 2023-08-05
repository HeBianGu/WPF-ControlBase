// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HeBianGu.Window.Link
{
    /// <summary> 自定义导航框架 </summary>

    public class LinkActionFrame : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LinkActionFrame), "S.LinkActionFrame.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(LinkActionFrame), "S.LinkActonFrame.Single");

        static LinkActionFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkActionFrame), new FrameworkPropertyMetadata(typeof(LinkActionFrame)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
        public IAction LinkAction
        {
            get { return (IAction)GetValue(LinkActionProperty); }
            set { SetValue(LinkActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionProperty =
            DependencyProperty.Register("LinkAction", typeof(IAction), typeof(LinkActionFrame), new PropertyMetadata(default(LinkAction), async (d, e) =>
              {
                  LinkActionFrame control = d as LinkActionFrame;

                  if (control == null) return;

                  if (e.NewValue is IAction)
                  {
                      IAction config = e.NewValue as IAction;

                      await control.RefreshLinkAction(config);
                  }

              }));
        private Random random = new Random();


        private static bool _isRefreshing;
        private async Task RefreshLinkAction(IAction action)
        {
            if (_isRefreshing)
                return;
            _isRefreshing = true;
            try
            {
                if (this.UseRandomWipe)
                {
                    this.CurrentWipe = this.RandomWipes[random.Next(this.RandomWipes.Count)];
                }
                else
                {
                    if (this.LinkAction is LinkAction linkAction)
                    {
                        this.CurrentWipe = linkAction.TransitionWipe ?? new CircleWipe();
                    }
                }

                //IActionResult result = await Task.Run(() =>
                //{
                //    return action?.GetActionResult();
                //});


                IActionResult result = await this.LinkAction?.GetActionResult();
                //await this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>

                await this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
                                          {
                                              _isRefreshing = false;
                                              this.Content = result?.View;
                                              //if (this.Content == result?.View)
                                              //{
                                              //    this.Content = result?.View;
                                              //}
                                              //else
                                              //{
                                              //    this.Content = result?.View;
                                              //}
                                          }));
            }
            catch (Exception ex)
            {
                this.Content = ex;
                Logger.Instance?.Error("UseMvc?");
                Logger.Instance?.Error(ex);
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

    public class LinkActionCollection : ObservableCollection<IAction>
    {

    }

}
