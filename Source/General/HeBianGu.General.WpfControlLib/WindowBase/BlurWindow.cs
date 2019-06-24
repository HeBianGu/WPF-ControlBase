using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 磨砂类型窗口 </summary>
    public partial class BlurWindow : WindowBase
    {
        public BlurWindow() : base()
        {
            this.Loaded +=(l, k) =>
             {
                 this.EnableBlur();
             };
        }
    }

    internal static class BlurWindowExtensions
    {
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttribData data);


            [StructLayout(LayoutKind.Sequential)]
            internal struct WindowCompositionAttribData
            {
                public WindowCompositionAttribute Attribute;
                public IntPtr Data;
                public int SizeOfData;
            }


            [StructLayout(LayoutKind.Sequential)]
            internal struct AccentPolicy
            {
                public AccentState AccentState;
                public AccentFlags AccentFlags;
                public int GradientColor;
                public int AnimationId;
            }


            [Flags]
            internal enum AccentFlags
            {
                // ... 
                DrawLeftBorder = 0x20,
                DrawTopBorder = 0x40,
                DrawRightBorder = 0x80,
                DrawBottomBorder = 0x100,
                DrawAllBorders = (DrawLeftBorder | DrawTopBorder | DrawRightBorder | DrawBottomBorder),
                DrawNoBorder = 0
                // ... 
            }


            internal enum WindowCompositionAttribute
            {
                // ... 
                WCA_ACCENT_POLICY = 19
                // ... 
            }


            internal enum AccentState
            {
                ACCENT_DISABLED = 0,
                ACCENT_ENABLE_GRADIENT = 1,
                ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
                ACCENT_ENABLE_BLURBEHIND = 3,
                ACCENT_INVALID_STATE = 4
            }
        }

        public static void EnableBlur(this Window window)
        {
            if (SystemParameters.HighContrast)
            {
                return; // Blur is not useful in high contrast mode 
            }

            SetAccentPolicy(window, NativeMethods.AccentState.ACCENT_ENABLE_BLURBEHIND);
        }


        public static void DisableBlur(this Window window)
        {
            SetAccentPolicy(window, NativeMethods.AccentState.ACCENT_DISABLED);
        }


        private static void SetAccentPolicy(Window window, NativeMethods.AccentState accentState)
        {
            var windowHelper = new WindowInteropHelper(window);


            var accent = new NativeMethods.AccentPolicy
            {
                AccentState = accentState,
                AccentFlags = GetAccentFlagsForTaskbarPosition()
            };


            var accentStructSize = Marshal.SizeOf(accent);


            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);


            var data = new NativeMethods.WindowCompositionAttribData
            {
                Attribute = NativeMethods.WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            NativeMethods.SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }


        private static NativeMethods.AccentFlags GetAccentFlagsForTaskbarPosition()
        {
            return NativeMethods.AccentFlags.DrawNoBorder;
        }
    }
}
