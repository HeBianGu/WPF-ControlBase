// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace HeBianGu.Base.WpfBase
{
    public class ClipBoardService
    {
        #region - 监视剪切板 -

        /// <summary>
        /// 剪贴板内容改变时API函数向windows发送的消息
        /// </summary>
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        /// <summary>
        /// windows用于监视剪贴板的API函数
        /// </summary>
        /// <param name="hwnd">要监视剪贴板的窗口的句柄</param>
        /// <returns>成功则返回true</returns>
        [DllImport("user32.dll")]//引用dll,确保API可用
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        /// <summary>
        /// 取消对剪贴板的监视
        /// </summary>
        /// <param name="hwnd">监视剪贴板的窗口的句柄</param>
        /// <returns>成功则返回true</returns>
        [DllImport("user32.dll")]//引用dll,确保API可用
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        /// <summary> 添加监视消息 </summary>
        private void win_SourceInitialized(object sender)
        {

            HwndSource hwndSource = PresentationSource.FromVisual(sender as Window) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }

        }

        private int isclipDoubleClick = 0;

        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_CLIPBOARDUPDATE:
                    {
                        if (isclipDoubleClick <= 0)
                        {
                            if (ClipBoardChanged != null)
                            {
                                // HTodo  ：触发剪贴板变化事件 
                                ClipBoardChanged.Invoke();
                            }
                        }

                        isclipDoubleClick--;
                    }
                    break;
            }

            return IntPtr.Zero;
        }


        #endregion

        public void Register(Window window)
        {
            HwndSource hwndSource = PresentationSource.FromVisual(window) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }

            // HTodo  ：添加剪贴板监视 
            System.IntPtr handle = (new System.Windows.Interop.WindowInteropHelper(window)).Handle;

            AddClipboardFormatListener(handle);
        }
        public Action ClipBoardChanged;
    }
}
