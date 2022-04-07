// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    [SettingConfig(Name = "样式设置", Group = "基本设置")]
    public class StyleSetting : LazySettingInstance<StyleSetting>
    {
        private LayoutStyle _layoutStyle;
        [Display(Name = "Layout")]
        public LayoutStyle LayoutStyle
        {
            get { return _layoutStyle; }
            set
            {
                _layoutStyle = value;
                RaisePropertyChanged();
            }
        }

        private StyleType _styleType;
        [Display(Name = "Style")]
        public StyleType StyleType
        {
            get { return _styleType; }
            set
            {
                _styleType = value;
                RaisePropertyChanged();
            }
        }

        private MouseOverStyleType _mouseOverStyleType;
        [Display(Name = "MouseOverStyleType")]
        public MouseOverStyleType MouseOverStyleType
        {
            get { return _mouseOverStyleType; }
            set
            {
                _mouseOverStyleType = value;
                RaisePropertyChanged();
            }
        }


        private EffectType _effectType;
        [Display(Name = "Effect")]
        public EffectType EffectType
        {
            get { return _effectType; }
            set
            {
                _effectType = value;
                RaisePropertyChanged();
            }
        }

        private EffectType _mouseOverEffect;
        [Display(Name = "MouseOverEffect")]
        public EffectType MouseOverEffect
        {
            get { return _mouseOverEffect; }
            set
            {
                _mouseOverEffect = value;
                RaisePropertyChanged();
            }
        }

        private EffectType _selectEffect;
        [Display(Name = "SelectEffect")]
        public EffectType SelectEffect
        {
            get { return _selectEffect; }
            set
            {
                _selectEffect = value;
                RaisePropertyChanged();
            }
        }

        private bool _useClear;
        /// <summary> 说明  </summary>
        [Display(Name = "启用清空")]
        public bool UseClear
        {
            get { return _useClear; }
            set
            {
                _useClear = value;
                RaisePropertyChanged();
            }
        }


        private bool _useTitle;
        [Display(Name = "启用标题")]
        public bool UseTitle
        {
            get { return _useTitle; }
            set
            {
                _useTitle = value;
                RaisePropertyChanged();
            }
        }

        private bool _useBackground = true;
        [Display(Name = "启用背景")]
        public bool UseBackground
        {
            get { return _useBackground; }
            set
            {
                _useBackground = value;
                RaisePropertyChanged();
            }
        }

        private bool _useBorder = true;
        /// <summary> 说明  </summary>
        public bool UseBorder
        {
            get { return _useBorder; }
            set
            {
                _useBorder = value;
                RaisePropertyChanged();
            }
        }


    }

    public static class StyleExtention
    {
        public static IServiceCollection AddStyle(this IServiceCollection services)
        {
            services.AddSingleton<IStyleLoadService, StyleLoadService>();

            return services;
        }

        /// <summary>
        /// 设置
        /// </summary>  
        public static IApplicationBuilder UseStyle(this IApplicationBuilder builder, Action<StyleSetting> systemPath = null)
        {
            systemPath?.Invoke(StyleSetting.Instance);

            SystemSetting.Instance?.Add(StyleSetting.Instance);


            return builder;
        }
    }

    public interface IStyleLoadService
    {
        void LoadDefault(Type type);

        void RefreshAll();

        void LoadWpfControlLib();
    }

    public class StyleLoadService : IStyleLoadService
    {
        private List<Type> _styleTypes = new List<Type>();

        public void LoadDefault(Type type)
        {
            //if (!this._styleTypes.Contains(type))
            //{
            //    this._styleTypes.Add(type);
            //}

            //this.Load(Application.Current.Resources, type.Assembly.GetName().Name);

        }

        public void LoadCurrent(ResourceDictionary resource, Type type)
        {
            try
            {
                string uriString = $"/{type.Assembly.GetName().Name};component/{type.Name}.xaml";

                Uri uri = new Uri(uriString, UriKind.Relative);

                ResourceDictionary find = resource.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

                if (find == null)
                {
                    resource.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RefreshAll()
        {
            //foreach (var type in this._styleTypes)
            //{
            //    this.LoadDefault(type);
            //}

            this.LoadWpfControlLib();


            foreach (ResourceDictionary item in Application.Current.Resources.MergedDictionaries)
            {
                System.Diagnostics.Debug.WriteLine(item.Source);
            }
        }

        private void Load(ResourceDictionary resource, string assmblyName)
        {
            string uriStringDefault = $"/{assmblyName};component/Themes/Generic.Dynamic.xaml";
            Uri uri = new Uri(uriStringDefault, UriKind.Relative);
            ResourceDictionary find = resource.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

            if (find == null)
            {
                ResourceDictionary current = new ResourceDictionary() { Source = uri };
                resource.MergedDictionaries.Insert(0, current);
            }
        }

        public void LoadWpfControlLib()
        {
            this.Load(Application.Current.Resources, "HeBianGu.General.WpfControlLib");
        }
    }

    public class StyleLoader : ServiceInstance<IStyleLoadService>
    {

    }

    public class BrushExtionsion
    {

    }
}
