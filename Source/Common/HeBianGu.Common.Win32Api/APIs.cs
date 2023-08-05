using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace HeBianGu.Common.Win32Api
{
    /// <summary>
    /// Windows API Functions
    /// </summary>
    public partial class Win32Api
    {
        #region .ctor()
        // No need to construct this object
        private Win32Api()
        {
        }
        #endregion

        #region CallBacks
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        #endregion

        #region Kernel32.dll functions
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetCurrentThreadId();
        #endregion

        #region Gdi32.dll functions
        [DllImport("gdi32.dll")]
        static public extern bool StretchBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
            IntPtr hDCSrc, int XOriginScr, int YOriginSrc, int WidthScr, int HeightScr, uint Rop);
        [DllImport("gdi32.dll")]
        static public extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        static public extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int Width, int Heigth);
        [DllImport("gdi32.dll")]
        static public extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("gdi32.dll")]
        static public extern bool BitBlt(IntPtr hDCDest, int XOriginDest, int YOriginDest, int WidthDest, int HeightDest,
            IntPtr hDCSrc, int XOriginScr, int YOriginSrc, uint Rop);
        [DllImport("gdi32.dll")]
        static public extern IntPtr DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        static public extern bool PatBlt(IntPtr hDC, int XLeft, int YLeft, int Width, int Height, uint Rop);
        [DllImport("gdi32.dll")]
        static public extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        static public extern uint GetPixel(IntPtr hDC, int XPos, int YPos);
        [DllImport("gdi32.dll")]
        static public extern int SetMapMode(IntPtr hDC, int fnMapMode);
        [DllImport("gdi32.dll")]
        static public extern int GetObjectType(IntPtr handle);
        [DllImport("gdi32")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_FLAT bmi,
            int iUsage, ref int ppvBits, IntPtr hSection, int dwOffset);
        [DllImport("gdi32")]
        public static extern int GetDIBits(IntPtr hDC, IntPtr hbm, int StartScan, int ScanLines, int lpBits, BITMAPINFOHEADER bmi, int usage);
        [DllImport("gdi32")]
        public static extern int GetDIBits(IntPtr hdc, IntPtr hbm, int StartScan, int ScanLines, int lpBits, ref BITMAPINFO_FLAT bmi, int usage);
        [DllImport("gdi32")]
        public static extern IntPtr GetPaletteEntries(IntPtr hpal, int iStartIndex, int nEntries, byte[] lppe);
        [DllImport("gdi32")]
        public static extern IntPtr GetSystemPaletteEntries(IntPtr hdc, int iStartIndex, int nEntries, byte[] lppe);
        [DllImport("gdi32")]
        public static extern uint SetDCBrushColor(IntPtr hdc, uint crColor);
        [DllImport("gdi32")]
        public static extern IntPtr CreateSolidBrush(uint crColor);
        [DllImport("gdi32")]
        public static extern int SetBkMode(IntPtr hDC, BackgroundMode mode);
        [DllImport("gdi32")]
        public static extern int SetViewportOrgEx(IntPtr hdc, int x, int y, int param);
        [DllImport("gdi32")]
        public static extern uint SetTextColor(IntPtr hDC, uint colorRef);
        [DllImport("gdi32")]
        public static extern int SetStretchBltMode(IntPtr hDC, int StrechMode);
        #endregion

        #region Uxtheme.dll functions
        [DllImport("uxtheme.dll")]
        static public extern int SetWindowTheme(IntPtr hWnd, string AppID, string ClassID);
        #endregion

        #region User32.dll functions
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool ShowWindow(IntPtr hWnd, short State);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool UpdateWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool CloseClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool EmptyClipboard();
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr SetClipboardData(uint Format, IntPtr hData);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, uint Item, ref RECT rc);
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref RECT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref POINT lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTON lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TBBUTTONINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, ref REBARBANDINFO lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref TVITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref LVITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HDITEM lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, ref HD_HITTESTINFO hti);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int DrawText(IntPtr hdc, string lpString, int nCount, ref RECT lpRect, int uFormat);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        // See http://msdn.microsoft.com/en-us/library/ms633541%28v=vs.85%29.aspx
        // See http://msdn.microsoft.com/en-us/library/ms649033%28VS.85%29.aspx
        public extern static IntPtr SetParent(IntPtr hChild, IntPtr hParent);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr GetDlgItem(IntPtr hDlg, int nControlID);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int GetClientRect(IntPtr hWnd, ref RECT rc);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static int InvalidateRect(IntPtr hWnd, IntPtr rect, int bErase);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool WaitMessage();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax, uint wFlag);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMessage(ref MSG msg, int hWnd, uint wFilterMin, uint wFilterMax);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool TranslateMessage(ref MSG msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool DispatchMessage(ref MSG msg);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, uint cursor);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetFocus();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT ps);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENTS tme);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool redraw);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern ushort GetKeyState(int virtKey);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, out STRINGBUFFER ClassName, int nMaxCount);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hRegion, uint flags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT wp);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowText(IntPtr hWnd, string text);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, out STRINGBUFFER text, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int GetSystemMetrics(int nIndex);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int SetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si, int fRedraw);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int EnableScrollBar(IntPtr hWnd, uint flags, uint arrows);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int BringWindowToTop(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetScrollInfo(IntPtr hwnd, int bar, ref SCROLLINFO si);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int ScrollWindowEx(IntPtr hWnd, int dx, int dy,
            ref RECT rcScroll, ref RECT rcClip, IntPtr UpdateRegion, ref RECT rcInvalidated, uint flags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int IsWindow(IntPtr hWnd);
        [DllImport("user32", CharSet = CharSet.Auto)]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, //[in] Specifies the virtual-key code to be translated. 
            int uScanCode, // [in] Specifies the hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up (not pressed). 
            byte[] lpbKeyState, // [in] Pointer to a 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function, only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys is ignored.
            byte[] lpwTransKey, // [out] Pointer to the buffer that receives the translated character or characters. 
            int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise.
        #endregion

        #region Common Controls functions
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControlsEx(INITCOMMONCONTROLSEX icc);
        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControls();
        [DllImport("comctl32.dll", EntryPoint = "DllGetVersion")]
        public extern static int GetCommonControlDLLVersion(ref DLLVERSIONINFO dvi);
        [DllImport("comctl32.dll")]
        public static extern IntPtr ImageList_Create(int width, int height, uint flags, int count, int grow);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Destroy(IntPtr handle);
        [DllImport("comctl32.dll")]
        public static extern int ImageList_Add(IntPtr imageHandle, IntPtr hBitmap, IntPtr hMask);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Remove(IntPtr imageHandle, int index);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_BeginDrag(IntPtr imageHandle, int imageIndex, int xHotSpot, int yHotSpot);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragEnter(IntPtr hWndLock, int x, int y);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragMove(int x, int y);
        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DragLeave(IntPtr hWndLock);
        [DllImport("comctl32.dll")]
        public static extern void ImageList_EndDrag();
        #endregion

        #region Win32 Macro-Like helpers
        public static int GET_X_LPARAM(int lParam)
        {
            return (lParam & 0xffff);
        }


        public static int GET_Y_LPARAM(int lParam)
        {
            return (lParam >> 16);
        }

        public static System.Windows.Point GetPointFromLPARAM(int lParam)
        {
            return new System.Windows.Point(GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam));
        }

        public static int LOW_ORDER(int param)
        {
            return (param & 0xffff);
        }

        public static int HIGH_ORDER(int param)
        {
            return (param >> 16);
        }

        #endregion

        #region Process related

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hSnapshot);
        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool CreateEnvironmentBlock(ref IntPtr lpEnvironment, IntPtr hToken, bool bInherit);
        [DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern bool CreateProcessAsUser(IntPtr hToken, string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandle, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("userenv.dll", SetLastError = true)]
        private static extern bool DestroyEnvironmentBlock(IntPtr lpEnvironment);
        [DllImport("advapi32.dll")]
        private static extern bool DuplicateTokenEx(IntPtr ExistingTokenHandle, uint dwDesiredAccess, IntPtr lpThreadAttributes, int TokenType, int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);
        private static bool GetSessionUserToken(ref IntPtr phUserToken)
        {
            bool flag = false;
            IntPtr zero = IntPtr.Zero;
            uint maxValue = uint.MaxValue;
            IntPtr ppSessionInfo = IntPtr.Zero;
            int pCount = 0;
            if (WTSEnumerateSessions(phUserToken, 0, 1, ref ppSessionInfo, ref pCount) != 0)
            {
                int num3 = Marshal.SizeOf(typeof(WTS_SESSION_INFO));
                int num4 = (int)ppSessionInfo;
                for (int i = 0; i < pCount; i++)
                {
                    WTS_SESSION_INFO wts_session_info = (WTS_SESSION_INFO)Marshal.PtrToStructure((IntPtr)num4, typeof(WTS_SESSION_INFO));
                    num4 += num3;
                    if (wts_session_info.State == WTS_CONNECTSTATE_CLASS.WTSActive)
                    {
                        maxValue = wts_session_info.SessionID;
                    }
                }
            }
            if (maxValue == uint.MaxValue)
            {
                maxValue = WTSGetActiveConsoleSessionId();
            }
            if (WTSQueryUserToken(maxValue, ref zero) != 0)
            {
                flag = DuplicateTokenEx(zero, 0, IntPtr.Zero, 2, 1, ref phUserToken);
                CloseHandle(zero);
            }
            return flag;
        }


        public static bool StartProcessAsCurrentUser(string appPath, string cmdLine = null, string workDir = null, bool visible = true)
        {
            IntPtr zero = IntPtr.Zero;
            STARTUPINFO lpStartupInfo = new STARTUPINFO();
            PROCESS_INFORMATION lpProcessInformation = new PROCESS_INFORMATION();
            IntPtr lpEnvironment = IntPtr.Zero;
            lpStartupInfo.cb = Marshal.SizeOf(typeof(STARTUPINFO));
            try
            {
                if (!GetSessionUserToken(ref zero))
                {
                    throw new Exception("GetSessionUserToken failed.");
                }
                uint dwCreationFlags = (uint)(0x400 | (visible ? 0x10 : 0x8000000));
                lpStartupInfo.wShowWindow = visible ? ((short)5) : ((short)0);
                lpStartupInfo.lpDesktop = @"winsta0\default";
                if (!CreateEnvironmentBlock(ref lpEnvironment, zero, false))
                {
                    throw new Exception("CreateEnvironmentBlock failed.");
                }
                if (!CreateProcessAsUser(zero, appPath, cmdLine, IntPtr.Zero, IntPtr.Zero, false, dwCreationFlags, lpEnvironment, workDir, ref lpStartupInfo, out lpProcessInformation))
                {
                    throw new Exception("CreateProcessAsUser failed.");
                }
                int num = Marshal.GetLastWin32Error();
            }
            finally
            {
                CloseHandle(zero);
                if (lpEnvironment != IntPtr.Zero)
                {
                    DestroyEnvironmentBlock(lpEnvironment);
                }
                CloseHandle(lpProcessInformation.hThread);
                CloseHandle(lpProcessInformation.hProcess);
            }
            return true;
        }

        [DllImport("wtsapi32.dll", SetLastError = true)]
        private static extern int WTSEnumerateSessions(IntPtr hServer, int Reserved, int Version, ref IntPtr ppSessionInfo, ref int pCount);
        [DllImport("kernel32.dll")]
        private static extern uint WTSGetActiveConsoleSessionId();
        [DllImport("Wtsapi32.dll")]
        private static extern uint WTSQueryUserToken(uint SessionId, ref IntPtr phToken);

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        private enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        private enum SW
        {
            SW_HIDE = 0,
            SW_MAX = 10,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_NORMAL = 1,
            SW_RESTORE = 9,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOWNORMAL = 1
        }

        private enum TOKEN_TYPE
        {
            TokenImpersonation = 2,
            TokenPrimary = 1
        }

        private enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WTS_SESSION_INFO
        {
            public readonly uint SessionID;
            [MarshalAs(UnmanagedType.LPStr)]
            public readonly string pWinStationName;
            public readonly WTS_CONNECTSTATE_CLASS State;
        }


        [DllImport("wtsapi32.dll", SetLastError = true)]
        public static extern bool WTSSendMessage(IntPtr hServer, uint SessionId, String pTitle, int TitleLength,
            String pMessage, int MessageLength, int Style, int Timeout, out int pResponse, bool bWait);

        public static void ShowWTSMessage(string message, string title)
        {
            int resp = 0;
            WTSSendMessage(IntPtr.Zero,
                WTSGetActiveConsoleSessionId(),
                title, title.Length, message, message.Length,
                0, 0, out resp, false);
        }

        #endregion

        #region Clipboard related

        //Constants for API Calls...
        private const int WM_DRAWCLIPBOARD = 0x308;
        private const int WM_CHANGECBCHAIN = 0x30D;
        private const int WM_POWERBROADCAST = 0x218;

        private const int PBT_APMRESUMEAUTOMATIC = 0x12;
        private const int PBT_APMSUSPEND = 0x4;

        //Handle for next clipboard viewer...
        private IntPtr mNextClipBoardViewerHWnd;

        ////API declarations...
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //static public extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //static public extern bool ChangeClipboardChain(IntPtr HWnd, IntPtr HWndNext);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);


        // See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
        public const int WM_CLIPBOARDUPDATE = 0x031D;

        // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        #endregion

        #region ConsoleEx

        public struct COORD
        {
            public ushort X;
            public ushort Y;
        };

        public struct CONSOLE_FONT
        {
            public uint index;
            public COORD dim;
        };

        public static class ConsoleEx
        {
            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32", CharSet = CharSet.Auto)]
            internal static extern bool AllocConsole();

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32", CharSet = CharSet.Auto)]
            internal static extern bool SetConsoleFont(IntPtr consoleFont, uint index);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32", CharSet = CharSet.Auto)]
            internal static extern bool SetConsoleMode(IntPtr consoleFont, uint mode);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32", CharSet = CharSet.Auto)]
            internal static extern bool GetConsoleFontInfo(IntPtr hOutput, byte bMaximize, uint count, [In, Out] CONSOLE_FONT[] consoleFont);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32", CharSet = CharSet.Auto)]
            internal static extern uint GetNumberOfConsoleFonts();

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32", CharSet = CharSet.Auto)]
            internal static extern COORD GetConsoleFontSize(IntPtr HANDLE, uint DWORD);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32.dll ")]
            internal static extern IntPtr GetStdHandle(int nStdHandle);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern int GetConsoleTitle(String sb, int capacity);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("user32.dll", EntryPoint = "UpdateWindow")]
            internal static extern int UpdateWindow(IntPtr hwnd);

            [System.Security.SuppressUnmanagedCodeSecurity]
            [DllImport("user32.dll")]
            internal static extern IntPtr FindWindow(String sClassName, String sAppName);

            const int ENABLE_QUICK_EDIT = 0x0040;

            // STD_INPUT_HANDLE (DWORD): -10 is the standard input device.
            const int STD_INPUT_HANDLE = -10;

            [DllImport("kernel32.dll")]
            static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);

            [DllImport("kernel32.dll")]
            static extern bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);

            public static void OpenConsole()
            {
                var consoleTitle = "> Debug Console";
                AllocConsole();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WindowWidth = 80;
                Console.CursorVisible = false;
                Console.Title = consoleTitle;

                //IntPtr consoleHandle = GetConsoleWindow();
                //UInt32 consoleMode;

                //// get current console mode
                //if (!GetConsoleMode(consoleHandle, out consoleMode))
                //{
                //    // Error: Unable to get console mode.
                //    return;
                //}

                //// Clear the quick edit bit in the mode flags
                //mode &= ~ENABLE_QUICK_EDIT;

                //// set the new mode
                //if (!SetConsoleMode(consoleHandle, consoleMode))
                //{
                //    // ERROR: Unable to set console mode
                //}

                try
                {
                    IntPtr hwnd = FindWindow(null, consoleTitle);
                    IntPtr hOut = GetStdHandle(-10);

                    //const uint MAX_FONTS = 40;
                    //uint num_fonts = GetNumberOfConsoleFonts();
                    //if (num_fonts > MAX_FONTS) num_fonts = MAX_FONTS;
                    //CONSOLE_FONT[] fonts = new CONSOLE_FONT[MAX_FONTS];
                    //GetConsoleFontInfo(hOut, 0, num_fonts, fonts);
                    //for (var n = 7; n < num_fonts; ++n)
                    //{
                    //    //fonts[n].dim = GetConsoleFontSize(hOut, fonts[n].index);
                    //    //if (fonts[n].dim.X == 106 && fonts[n].dim.Y == 33)
                    //    //{
                    //    SetConsoleFont(hOut, fonts[n].index);
                    //    UpdateWindow(hwnd);
                    //    return;
                    //    //}
                    //}

                    const int ENABLE_QUICK_EDIT = 0x0040;

                    int consoleMode;

                    // get current console mode
                    if (!GetConsoleMode(hOut, out consoleMode))
                    {
                        // Error: Unable to get console mode.
                        return;
                    }

                    // Clear the quick edit bit in the mode flags
                    consoleMode &= ~ENABLE_QUICK_EDIT;

                    // set the new mode
                    if (!SetConsoleMode(hOut, consoleMode))
                    {
                        // ERROR: Unable to set console mode
                    }
                    UpdateWindow(hwnd);
                }
                catch
                {

                }

                Console.WriteLine("DEBUG CONSOLE WAIT OUTPUTING...{0}\n", DateTime.Now.ToLongTimeString());
            }

            public static void Log(String format, params object[] args)
            {
                Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] " + format, args);
            }
            public static void Log(Object arg)
            {
                Console.WriteLine(arg);
            }
        }

        #endregion

        #region window, state related

        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        public static void BringToBackground(Window win)
        {
            SetWindowPos(new WindowInteropHelper(win).Handle, HWND_BOTTOM, 0, 0, 0, 0, 0x13);
        }

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern bool BringWindowToTop(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BringWindowToTop(HandleRef hWnd);
        //[DllImport("user32.dll")]
        //public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);
        //[DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject);
        public static void ForceForegroundWindow(IntPtr hWnd)
        {
            uint windowThreadProcessId = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            uint currentThreadId = (uint)GetCurrentThreadId();
            if (windowThreadProcessId != currentThreadId)
            {
                AttachThreadInput(windowThreadProcessId, currentThreadId, true);
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, 5);
                AttachThreadInput(windowThreadProcessId, currentThreadId, false);
            }
            else
            {
                BringWindowToTop(hWnd);
                ShowWindow(hWnd, 5);
            }
        }

        public static bool GetCurrentProcessOnFocus()
        {
            try
            {
                ForceForegroundWindow(Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName)[0].MainWindowHandle);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //[DllImport("kernel32.dll")]
        //public static extern uint GetCurrentThreadId();
        //[DllImport("user32.dll")]
        //public static extern IntPtr GetDesktopWindow();
        public static string GetForegroundProcessName()
        {
            uint lpdwProcessId = 0;
            GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId);
            return Process.GetProcessById((int)lpdwProcessId).ProcessName;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        public static void HideFromTaskView(Window window)
        {
            try
            {
                WindowInteropHelper helper = new WindowInteropHelper(window);
                int windowLong = (int)GetWindowLong(helper.Handle, -20);
                windowLong |= 0x80;
                SetWindowLong(helper.Handle, -20, (IntPtr)windowLong);
            }
            catch (Exception exception)
            {
                //log and throw.
            }
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return (int)intPtr.ToInt64();
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int IntSetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        public static bool IsForeground()
        {
            bool flag = false;
            try
            {
                int num;
                GetWindowThreadProcessId(GetForegroundWindow(), out num);
                int id = Process.GetCurrentProcess().Id;
                flag = num == id;
            }
            catch
            {
            }
            return flag;
        }

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        //[DllImport("user32.dll")]
        //public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, int Flags);
        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(int hWnd);
        [DllImport("kernel32.dll")]
        public static extern void SetLastError(int dwErrorCode);
        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr zero = IntPtr.Zero;
            SetLastError(0);
            if (IntPtr.Size == 4)
            {
                int num2 = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                zero = new IntPtr(num2);
            }
            else
            {
                zero = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }
            if ((zero == IntPtr.Zero) && (error != 0))
            {
                throw new Win32Exception(error);
            }
            return zero;
        }

        //[DllImport("user32.dll")]
        //public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        //public static unsafe void SuppressWininetBehavior()
        //{
        //    int num = 0;
        //    int* numPtr = &num;
        //    if (!InternetSetOption(0, 0x51, new IntPtr((void*)numPtr), 4))
        //    {
        //        MessageBox.Show("Something went wrong !>?");
        //    }
        //}

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(int hWnd, bool fAltTab);

        public static class CursorInfo
        {
            [DllImport("user32.dll")]
            private static extern void GetCursorPos(out POINT pt);
            public static System.Windows.Point GetNowPosition(Visual v)
            {
                POINT point;
                point.X = -2147483648;
                point.Y = -2147483648;
                try
                {
                    GetCursorPos(out point);
                    HwndSource source = PresentationSource.FromVisual(v) as HwndSource;
                    ScreenToClient(source.Handle, ref point);
                }
                catch
                {
                }
                return new System.Windows.Point((double)point.X, (double)point.Y);
            }

            public static System.Windows.Point GetNowPositionScr(Visual v)
            {
                POINT point;
                GetCursorPos(out point);
                return new System.Windows.Point((double)point.X, (double)point.Y);
            }

            [DllImport("user32.dll")]
            private static extern int ScreenToClient(IntPtr hwnd, ref POINT pt);

            [StructLayout(LayoutKind.Sequential)]
            private struct POINT
            {
                public int X;
                public int Y;
            }
        }

        public enum ExtendedWindowStyles
        {
            WS_EX_TOOLWINDOW = 0x80
        }

        public enum GetWindowLongFields
        {
            GWL_EXSTYLE = -20
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(System.Windows.Point pt)
            {
                this.X = Convert.ToInt32(pt.X);
                this.Y = Convert.ToInt32(pt.Y);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POWERBROADCAST_SETTING
        {
            public Guid PowerSetting;
            public uint DataLength;
            public byte Data;
        }

        public class PowerSettingGUIDs
        {
            public static int ConsoleDisplayStateDimmed = 2;
            public static int ConsoleDisplayStateOff = 0;
            public static int ConsoleDisplayStateOn = 1;
            public static Guid GUID_CONSOLE_DISPLAY_STATE = new Guid("6fe69556-704a-47a0-8f24-c28d936fda47");
            public static Guid GUID_LIDSWITCH_STATE_CHANGE = new Guid(0xba3e0f4d, 0xb817, 0x4094, 0xa2, 0xd1, 0xd5, 0x63, 0x79, 230, 160, 0xf3);
            public static Guid GUID_MONITOR_POWER_ON = new Guid("02731015-4510-4526-99e6-e5a17ebd1aea");
            public static Guid GUID_SESSION_DISPLAY_STATUS = new Guid("2B84C20E-AD23-4ddf-93DB-05FFBD7EFCA5");
            public static int MonitorPowerOnIsOff = 0;
            public static int MonitorPowerOnIsOn = 1;
        }

        #endregion

    }


    partial class Win32Api
    {
        /// <summary>
        /// 设置鼠标的坐标
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>    
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        //public struct POINT
        //{
        //    public int X;
        //    public int Y;
        //    public POINT(int x, int y)
        //    {
        //        this.X = x;
        //        this.Y = y;
        //    }

        //}

        /// <summary>
        /// 获取鼠标的坐标
        /// </summary>
        /// <param name="lpPoint">传址参数，坐标point类型</param>
        /// <returns>获取成功返回真</returns>   
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);
    }

}
