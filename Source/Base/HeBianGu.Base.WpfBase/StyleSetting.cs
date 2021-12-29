using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Base.WpfBase
{
    [SettingConfig(Name = "样式设置", Group = "基本设置")]
    public class StyleSetting : LazySettingInstance<StyleSetting>
    {

        private Duration _itemAnimationDuration = new Duration(TimeSpan.FromSeconds(3));
        [Display(Name = "动画间隔")]
        public Duration ItemAnimationDuration
        {
            get { return _itemAnimationDuration; }
            set
            {
                _itemAnimationDuration = value;
                RaisePropertyChanged();
            }
        }

        private StyleType _styleType = StyleType.Single;
        [Display(Name = "皮肤样式")]
        public StyleType StyleType
        {
            get { return _styleType; }
            set
            {
                _styleType = value;
                RaisePropertyChanged();

                StyleLoader.Instance?.RefreshAll();
            }
        }
    }

    public enum StyleType
    {
        Default = 0, Single, Accent, Clear
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
        List<Type> _styleTypes = new List<Type>();

        public void LoadDefault(Type type)
        {
            if (!this._styleTypes.Contains(type))
            {
                this._styleTypes.Add(type);
            }

            ResourceDictionary resource = Application.Current.Resources;

            try
            {
                {
                    foreach (StyleType item in typeof(StyleType).GetEnumValues())
                    {
                        string uriString = $"/{type.Assembly.GetName().Name};component/Themes/Generic.{item.ToString()}.xaml";

                        var uri = new Uri(uriString, UriKind.Relative);

                        var find = resource.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

                        if (item == StyleSetting.Instance.StyleType)
                        {
                            if (find == null)
                            { 
                                resource.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
                            }

                            //find = resource.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

                            //this.LoadCurrent(find, type);

                        }
                        else
                        {
                            if (find != null)
                            {
                                resource.MergedDictionaries.Remove(find);
                            }
                        }

                      
                    }

                }
            }
            catch (Exception ex)
            { 
                System.Diagnostics.Debug.WriteLine(ex); 
            }
        }

        public void LoadCurrent(ResourceDictionary resource, Type type)
        {
            try
            {
                string uriString = $"/{type.Assembly.GetName().Name};component/{type.Name}.xaml";

                var uri = new Uri(uriString, UriKind.Relative);

                var find = resource.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

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
            foreach (var type in this._styleTypes)
            {
                this.LoadDefault(type);
            }

            this.LoadWpfControlLib();


            foreach (var item in Application.Current.Resources.MergedDictionaries)
            {
                System.Diagnostics.Debug.WriteLine(item.Source);
            }
        }

        public void LoadWpfControlLib()
        {
            ResourceDictionary resource = Application.Current.Resources;

            try
            {
                {
                    foreach (StyleType item in typeof(StyleType).GetEnumValues())
                    {
                        string uriString = $"/HeBianGu.General.WpfControlLib;component/Themes/Generic.{item.ToString()}.xaml";

                        var uri = new Uri(uriString, UriKind.Relative);

                        var find = resource.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

                        if (item == StyleSetting.Instance.StyleType)
                        {
                            if (find == null)
                            {
                                resource.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
                            }
                        }
                        else
                        {
                            if (find != null)
                            {
                                resource.MergedDictionaries.Remove(find);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class StyleLoader : ServiceInstance<IStyleLoadService>
    {

    }

    public class BrushExtionsion
    {

    }
}
