// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

//namespace HeBianGu.Application.MainWindow
//{
//    public class AssemblyDomain : IThemeSerializeService,ILogService
//    {
//        public static AssemblyDomain Instance = new AssemblyDomain();

//        LocalConfigService _localConfigService = new LocalConfigService();

//        public ThemeConfig LoadTheme()
//        {
//            return _localConfigService.LoadConfig<ThemeConfig>(); 
//        }

//        public bool SaveTheme(ThemeConfig theme)
//        {
//            return _localConfigService.SaveConfig(theme); 
//        }

//        public void Debug(params string[] messages)
//        {
//            foreach (var item in messages)
//            {
//               System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Error(params Exception[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Error(params string[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Fatal(params string[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Fatal(params Exception[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Info(params string[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Trace(params string[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }

//        public void Warn(params string[] messages)
//        {
//            foreach (var item in messages)
//            {
//                System.Diagnostics.Debug.WriteLine(item);
//            }
//        }
//    }
//}
