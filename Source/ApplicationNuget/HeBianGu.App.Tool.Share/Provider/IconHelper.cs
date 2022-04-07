using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HeBianGu.App.Tool
{
    /// <summary> 获取文件关联图标 </summary>
    public partial class IconHelper
    {
        /// <summary> 返回系统设置的图标 </summary>  
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>  
        /// <param name="dwFileAttributes">0</param>  
        /// <param name="psfi">结构体</param>  
        /// <param name="cbSizeFileInfo">结构体大小</param>  
        /// <param name="uFlags">枚举类型</param>  
        /// <returns>-1失败</returns>  
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        /// <summary> 获取文件图标 </summary>  
        public Icon GetFileIcon(string p_Path)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(p_Path, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON | SHGFI.SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary> 获取文件夹图标 </summary>  
        public Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI.SHGFI_ICON | SHGFI.SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary> 获取系统图标 </summary>  
        public Icon GetSystemInfoIcon(string p_Path)
        {
            if (System.IO.Path.HasExtension(p_Path))
            {
                try
                {
                    return System.Drawing.Icon.ExtractAssociatedIcon(p_Path);
                }
                catch
                {
                    return null;
                }

            }
            else
            {
                return GetDirectoryIcon();
            }
        }

    }

    public partial class IconHelper
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static IconHelper t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static IconHelper Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new IconHelper();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private IconHelper()
        {

        }
        #endregion - 单例模式 End -

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }


    public enum SHGFI
    {
        SHGFI_ICON = 0x100,
        SHGFI_LARGEICON = 0x0,
        SHGFI_USEFILEATTRIBUTES = 0x10
    }


}
