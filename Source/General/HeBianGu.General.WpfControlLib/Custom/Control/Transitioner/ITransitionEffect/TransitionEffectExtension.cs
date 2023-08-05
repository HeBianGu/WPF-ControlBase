// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Markup;

namespace HeBianGu.General.WpfControlLib
{
    [MarkupExtensionReturnType(typeof(TransitionEffectBase))]
    public class TransitionEffectExtension : MarkupExtension
    {
        public TransitionEffectExtension()
        {
            Kind = TransitionEffectKind.None;
        }

        public TransitionEffectExtension(TransitionEffectKind kind)
        {
            Kind = kind;
        }

        public TransitionEffectExtension(TransitionEffectKind kind, TimeSpan duration)
        {
            Kind = kind;
            Duration = duration;
        }

        [ConstructorArgument("kind")]
        public TransitionEffectKind Kind { get; set; }

        [ConstructorArgument("duration")]
        public TimeSpan? Duration { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Duration.HasValue ? new TransitionEffect(Kind, Duration.Value) : new TransitionEffect(Kind);
        }
    }
}