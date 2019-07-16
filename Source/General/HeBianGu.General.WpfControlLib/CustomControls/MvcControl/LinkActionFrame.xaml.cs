using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 自定义导航框架 </summary>
    [TemplatePart(Name = "PART_TransitionerSlide")]
    [TemplatePart(Name = "PART_TransitionerSlideBottom")]

    public class LinkActionFrame : ContentControl, IZIndexController
    {
        static LinkActionFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkActionFrame), new FrameworkPropertyMetadata(typeof(LinkActionFrame)));
        }


        TransitionerSlide _transitionerSlide = null;
        TransitionerSlide _transitionerSlideBottom = null;
        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            _transitionerSlide = GetTemplateChild("PART_TransitionerSlide") as TransitionerSlide;
            _transitionerSlideBottom = GetTemplateChild("PART_TransitionerSlideBottom") as TransitionerSlide;


        }
        public ILinkActionBase LinkAction
        {
            get { return (ILinkActionBase)GetValue(LinkActionProperty); }
            set { SetValue(LinkActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionProperty =
            DependencyProperty.Register("LinkAction", typeof(ILinkActionBase), typeof(LinkActionFrame), new PropertyMetadata(default(LinkAction), async (d, e) =>
             {
                 LinkActionFrame control = d as LinkActionFrame;

                 if (control == null) return;


                 if (e.NewValue is LinkAction)
                 {
                     LinkAction config = e.NewValue as LinkAction;

                     try
                     {
                         var result = await Task.Run(() => config?.ActionResult());

                         control.Content = result?.View;

                         if (control._transitionerSlide == null) return;

                         if (control.UseRandomEffects)
                         {
                             control._transitionerSlide.OpeningEffects.Clear();

                             control._transitionerSlide.OpeningEffects.Add(control.RandomOpeningEffects[new Random().Next(control.RandomOpeningEffects.Count)]);
                         }
                         else
                         {
                             if (config.OpeningEffects.Count > 0)
                             {
                                 control._transitionerSlide.OpeningEffects.Clear();

                                 foreach (var item in config.OpeningEffects)
                                 {
                                     control._transitionerSlide.OpeningEffects.Add(item);
                                 }
                             }

                             if (config.OpeningEffect != null)
                             {
                                 control._transitionerSlide.OpeningEffects.Clear();

                                 control._transitionerSlide.OpeningEffects.Add(config.OpeningEffect);
                             }
                         }


                         control._transitionerSlide.State = TransitionerSlideState.None;

                         control._transitionerSlide.State = TransitionerSlideState.Current;
                     }
                     catch (Exception ex)
                     {
                         control.Content = ex;
                     }
                 }
                 else
                 {
                     ILinkActionBase config = e.NewValue as ILinkActionBase;

                     if (control._transitionerSlide == null) return;
                     try
                     {

                         var result = await Task.Run(() => config?.ActionResult());

                         control.Content = result?.View;

                         if (control.UseRandomEffects)
                         {
                             control._transitionerSlide.OpeningEffects.Clear();

                             control._transitionerSlide.OpeningEffects.Add(control.RandomOpeningEffects[new Random().Next(control.RandomOpeningEffects.Count)]);
                         }
                         else
                         {
                             control._transitionerSlide.OpeningEffects.Clear();

                             control._transitionerSlide.OpeningEffects.Add(new TransitionEffect(TransitionEffectKind.FadeIn));
                         }


                         control._transitionerSlide.State = TransitionerSlideState.None;

                         control._transitionerSlide.State = TransitionerSlideState.Current;
                     }
                     catch (Exception ex)
                     {
                         control.Content = ex;
                     }
                 }




             }));


        public ObservableCollection<TransitionEffectBase> RandomOpeningEffects
        {
            get { return (ObservableCollection<TransitionEffectBase>)GetValue(RandomOpeningEffectsProperty); }
            set { SetValue(RandomOpeningEffectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RandomOpeningEffectsProperty =
            DependencyProperty.Register("RandomOpeningEffects", typeof(ObservableCollection<TransitionEffectBase>),
             typeof(LinkActionFrame), new PropertyMetadata(new ObservableCollection<TransitionEffectBase>()
            {
                 new TransitionEffect(TransitionEffectKind.ExpandIn),
                 new TransitionEffect(TransitionEffectKind.FadeIn),
                 new TransitionEffect(TransitionEffectKind.SlideInFromBottom),
                 new TransitionEffect(TransitionEffectKind.SlideInFromLeft),
                 new TransitionEffect(TransitionEffectKind.SlideInFromTop)
             }));


        public bool UseRandomEffects
        {
            get { return (bool)GetValue(UseRandomEffectsProperty); }
            set { SetValue(UseRandomEffectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseRandomEffectsProperty =
            DependencyProperty.Register("UseRandomEffects", typeof(bool), typeof(TransitionEffectBase), new PropertyMetadata(true, (d, e) =>
             {
                 TransitionEffectBase control = d as TransitionEffectBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        void IZIndexController.Stack(params TransitionerSlide[] highestToLowest)
        {
            //if (highestToLowest == null) return;

            //var pos = highestToLowest.Length;

            //foreach (var slide in highestToLowest.Where(s => s != null))
            //{
            //    Panel.SetZIndex(slide, pos--);
            //}
        }

    }

}
