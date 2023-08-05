// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;

namespace HeBianGu.General.WpfControlLib
{
    internal static class UriHelper
    {
        public static Uri MakePackUri(string relativeFile)
        {
            //string uriString = "pack://application:,,,/" + AssemblyShortName + ";component/" + relativeFile;

            string uriString = relativeFile;

            return new Uri(uriString, UriKind.RelativeOrAbsolute);
        }

        private static string _assemblyShortName;

        private static string AssemblyShortName
        {
            get
            {
                if (_assemblyShortName == null)
                {
                    Assembly assembly = typeof(UriHelper).Assembly;

                    // pull out the short name
                    _assemblyShortName = assembly.ToString().Split(',')[0];
                }

                return _assemblyShortName;
            }
        }
    }
}