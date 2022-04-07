using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace HeBianGu.App.Touch
{
    public class MonitorActiveService
    {

        public static MonitorActiveService Instance = new MonitorActiveService();

        private DispatcherTimer mouseOrKeyboardOpTimer;
        public int defaultCheckCount = 120; // 设置默认时间是30s
        public int checkCount = 3120; // 可以在其他页面也获取计数，显示跳转倒计时


        public void StartMonitor()
        {
            StopMonitor();

            checkCount = defaultCheckCount;

            mouseOrKeyboardOpTimer = new DispatcherTimer();
            mouseOrKeyboardOpTimer.Tick += new EventHandler(mouseOrKeyboardOpTimer_Tick);
            mouseOrKeyboardOpTimer.Interval = new TimeSpan(0, 0, 1);
            mouseOrKeyboardOpTimer.Start();

        }

        public void StopMonitor()
        {
            if (mouseOrKeyboardOpTimer == null) return;

            mouseOrKeyboardOpTimer.Stop();
        }

        public Action<bool> OnCheckCount;

        /// <summary>
        /// 处理键鼠操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mouseOrKeyboardOpTimer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine(checkCount);

            if (HaveUsedTo())
            {
                if (--checkCount == 0)
                {
                    checkCount = defaultCheckCount;

                    mouseOrKeyboardOpTimer.Stop();

                    if (OnCheckCount != null)
                    {
                        OnCheckCount(true);
                    }

                    Debug.WriteLine("超时自动关闭");

                    // 跳转到其他页面或者其他操作
                    mouseOrKeyboardOpTimer.Start();
                }
            }
            else
            {
                checkCount = defaultCheckCount;
            }
        }

        /// <summary>
        /// 未操作时间超过1s后才开始计数
        /// </summary>
        /// <returns>返回未操作时间是否超过1s</returns>
        private bool HaveUsedTo()
        {
            return GetNoMouseOrKeyboardOpTime() / 1000 > 1;
        }

        /// <summary>
        /// Windows API 获取上次键鼠操作时间间隔
        /// </summary>
        /// <param name="plii"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref PLASTINPUTINFO plii);

        private static long GetNoMouseOrKeyboardOpTime()
        {
            PLASTINPUTINFO lastInputInfo = new PLASTINPUTINFO();
            lastInputInfo.cbSize = Marshal.SizeOf(lastInputInfo);
            if (!GetLastInputInfo(ref lastInputInfo)) return 0;
            return Environment.TickCount - lastInputInfo.dwTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PLASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
    }
}
