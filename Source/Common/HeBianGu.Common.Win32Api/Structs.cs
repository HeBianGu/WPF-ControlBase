
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HeBianGu.Common.Win32Api
{
	
	/// <summary>
	/// Structures to interoperate with the Windows 32 API  
	/// </summary>
	
	#region SIZE
	[StructLayout(LayoutKind.Sequential)]
	public struct SIZE
	{
		public int cx;
		public int cy;
	}
	#endregion

	#region RECT
	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
	#endregion

	#region INITCOMMONCONTROLSEX
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public class INITCOMMONCONTROLSEX 
	{
		public int dwSize;
		public int dwICC;
	}
	#endregion

	#region TBBUTTON
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct TBBUTTON 
	{
		public int iBitmap;
		public int idCommand;
		public byte fsState;
		public byte fsStyle;
		public byte bReserved0;
		public byte bReserved1;
		public int dwData;
		public int iString;
	}
	#endregion

	#region POINT
	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int x;
		public int y;
	}
	#endregion

	#region NMHDR
	[StructLayout(LayoutKind.Sequential)]
	public struct NMHDR
	{
		public IntPtr hwndFrom;
		public int idFrom;
		public int code;
	}
	#endregion

	#region TOOLTIPTEXTA
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
	public struct TOOLTIPTEXTA
	{
		public NMHDR hdr;
		public IntPtr lpszText;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
		public string szText;
		public IntPtr hinst;
		public int uFlags;
	}
	#endregion

	#region TOOLTIPTEXT
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TOOLTIPTEXT
	{
		public NMHDR hdr;
		public IntPtr lpszText;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=80)]
		public string szText;
		public IntPtr hinst;
		public int uFlags;
	}
	#endregion

	#region NMCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	public struct NMCUSTOMDRAW
	{
		public NMHDR hdr;
		public int dwDrawStage;
		public IntPtr hdc;
		public RECT rc;
		public int dwItemSpec;
		public int uItemState;
		public int lItemlParam;
	}
	#endregion

	#region NMTBCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	public struct NMTBCUSTOMDRAW
	{
		public NMCUSTOMDRAW nmcd;
		public IntPtr hbrMonoDither;
		public IntPtr hbrLines;
		public IntPtr hpenLines;
		public int clrText;
		public int clrMark;
		public int clrTextHighlight;
		public int clrBtnFace;
		public int clrBtnHighlight;
		public int clrHighlightHotTrack;
		public RECT rcText;
		public int nStringBkMode;
		public int nHLStringBkMode;
	}
	#endregion
	
	#region NMLVCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	public struct NMLVCUSTOMDRAW 
	{
		public NMCUSTOMDRAW nmcd;
		public uint clrText;
		public uint clrTextBk;
		public int iSubItem;
	} 
	#endregion

	#region TBBUTTONINFO
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TBBUTTONINFO
	{
		public int cbSize;
		public int dwMask;
		public int idCommand;
		public int iImage;
		public byte fsState;
		public byte fsStyle;
		public short cx;
		public IntPtr lParam;
		public IntPtr pszText;
		public int cchText;
	}
	#endregion

	#region REBARBANDINFO
	[StructLayout(LayoutKind.Sequential)]
	public struct REBARBANDINFO
	{
		public int cbSize;
		public int fMask;
		public int fStyle;
		public int clrFore;
		public int clrBack;
		public IntPtr lpText;
		public int cch;
		public int iImage;
		public IntPtr hwndChild;
		public int cxMinChild;
		public int cyMinChild;
		public int cx;
		public IntPtr hbmBack;
		public int wID;
		public int cyChild;
		public int cyMaxChild;
		public int cyIntegral;
		public int cxIdeal;
		public int lParam;
		public int cxHeader;
	}
	#endregion

	#region MOUSEHOOKSTRUCT
	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSEHOOKSTRUCT 
	{ 
		public POINT     pt; 
		public IntPtr    hwnd; 
		public int       wHitTestCode; 
		public IntPtr    dwExtraInfo; 
	}
	#endregion

	#region NMTOOLBAR
	[StructLayout(LayoutKind.Sequential)]
	public struct NMTOOLBAR 
	{
		public NMHDR		hdr;
		public int		    iItem;
		public TBBUTTON	    tbButton;
		public int		    cchText;
		public IntPtr		pszText;
		public RECT		    rcButton; 
	}
	#endregion
	
	#region NMREBARCHEVRON
	[StructLayout(LayoutKind.Sequential)]
	public struct NMREBARCHEVRON
	{
		public NMHDR hdr;
		public int uBand;
		public int wID;
		public int lParam;
		public RECT rc;
		public int lParamNM;
	}
	#endregion

	#region BITMAP
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAP
	{
		public long   bmType; 
		public long   bmWidth; 
		public long   bmHeight; 
		public long   bmWidthBytes; 
		public short  bmPlanes; 
		public short  bmBitsPixel; 
		public IntPtr bmBits; 
	}
	#endregion
 
	#region BITMAPINFO_FLAT
	[StructLayout(LayoutKind.Sequential)]
	public struct BITMAPINFO_FLAT 
	{
		public int      bmiHeader_biSize;
		public int      bmiHeader_biWidth;
		public int      bmiHeader_biHeight;
		public short    bmiHeader_biPlanes;
		public short    bmiHeader_biBitCount;
		public int      bmiHeader_biCompression;
		public int      bmiHeader_biSizeImage;
		public int      bmiHeader_biXPelsPerMeter;
		public int      bmiHeader_biYPelsPerMeter;
		public int      bmiHeader_biClrUsed;
		public int      bmiHeader_biClrImportant;
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
		public byte[] bmiColors; 
	}
	#endregion

	#region RGBQUAD
	public struct RGBQUAD 
	{
		public byte		rgbBlue;
		public byte		rgbGreen;
		public byte		rgbRed;
		public byte		rgbReserved;
	}
	#endregion
	
	#region BITMAPINFOHEADER
	[StructLayout(LayoutKind.Sequential)]
	public class BITMAPINFOHEADER 
	{
		public int      biSize = Marshal.SizeOf(typeof(BITMAPINFOHEADER));
		public int      biWidth;
		public int      biHeight;
		public short    biPlanes;
		public short    biBitCount;
		public int      biCompression;
		public int      biSizeImage;
		public int      biXPelsPerMeter;
		public int      biYPelsPerMeter;
		public int      biClrUsed;
		public int      biClrImportant;
	}
	#endregion

	#region BITMAPINFO
	[StructLayout(LayoutKind.Sequential)]
	public class BITMAPINFO 
	{
		public BITMAPINFOHEADER bmiHeader = new BITMAPINFOHEADER();
		[MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst=1024)]
		public byte[] bmiColors; 
	}
	#endregion

	#region PALETTEENTRY
	[StructLayout(LayoutKind.Sequential)]
	public struct PALETTEENTRY 
	{
		public byte		peRed;
		public byte		peGreen;
		public byte		peBlue;
		public byte		peFlags;
	}
	#endregion

	#region MSG
	[StructLayout(LayoutKind.Sequential)]
	public struct MSG 
	{
		public IntPtr hwnd;
		public int message;
		public IntPtr wParam;
		public IntPtr lParam;
		public int time;
		public int pt_x;
		public int pt_y;
	}
	#endregion

	#region HD_HITTESTINFO
	[StructLayout(LayoutKind.Sequential)]
	public struct HD_HITTESTINFO 
	{  
		public POINT pt;  
		public uint flags; 
		public int iItem; 
	}
	#endregion
 
	#region DLLVERSIONINFO
	[StructLayout(LayoutKind.Sequential)]
	public struct DLLVERSIONINFO
	{
		public int cbSize;
		public int dwMajorVersion;
		public int dwMinorVersion;
		public int dwBuildNumber;
		public int dwPlatformID;
	}
	#endregion

	#region PAINTSTRUCT
	[StructLayout(LayoutKind.Sequential)]
	public struct PAINTSTRUCT
	{
		public IntPtr hdc;
		public int fErase;
		public Rectangle rcPaint;
		public int fRestore;
		public int fIncUpdate;
		public int Reserved1;
		public int Reserved2;
		public int Reserved3;
		public int Reserved4;
		public int Reserved5;
		public int Reserved6;
		public int Reserved7;
		public int Reserved8;
	}
	#endregion

	#region BLENDFUNCTION
	[StructLayout(LayoutKind.Sequential, Pack=1)]
	public struct BLENDFUNCTION
	{
		public byte BlendOp;
		public byte BlendFlags;
		public byte SourceConstantAlpha;
		public byte AlphaFormat;
	}

	#endregion
	
	#region TRACKMOUSEEVENTS
	[StructLayout(LayoutKind.Sequential)]
	public struct TRACKMOUSEEVENTS
	{
		public uint cbSize;
		public uint dwFlags;
		public IntPtr hWnd;
		public uint dwHoverTime;
	}
	#endregion

	#region STRINGBUFFER
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct STRINGBUFFER
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=512)]
		public string szText;
	}
	#endregion

	#region NMTVCUSTOMDRAW
	[StructLayout(LayoutKind.Sequential)]
	public struct NMTVCUSTOMDRAW 
	{
		public NMCUSTOMDRAW nmcd;
		public uint clrText;
		public uint clrTextBk;
		public int iLevel;
	}
	#endregion

	#region TVITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct TVITEM 
	{
		public	uint      mask;
		public	IntPtr    hItem;
		public	uint      state;
		public	uint      stateMask;
		public	IntPtr    pszText;
		public	int       cchTextMax;
		public	int       iImage;
		public	int       iSelectedImage;
		public	int       cChildren;
		public	int       lParam;
	} 
	#endregion

	#region LVITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct LVITEM
	{
		public	uint mask;
		public	int iItem;
		public	int iSubItem;
		public	uint state;
		public	uint stateMask;
		public	IntPtr pszText;
		public	int cchTextMax;
		public	int iImage;
		public	int lParam;
		public	int iIndent;
	}
	#endregion

	#region HDITEM
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct HDITEM
	{
		public	uint    mask;
		public	int     cxy;
		public	IntPtr  pszText;
		public	IntPtr  hbm;
		public	int     cchTextMax;
		public	int     fmt;
		public	int     lParam;
		public	int     iImage;      
		public	int     iOrder;
	}	
	#endregion

	#region WINDOWPLACEMENT
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct WINDOWPLACEMENT
	{	
		public uint length; 
		public uint flags; 
		public uint showCmd; 
		public POINT ptMinPosition; 
		public POINT ptMaxPosition; 
		public RECT  rcNormalPosition; 
	}
	#endregion

	#region SCROLLINFO
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
	public struct SCROLLINFO
	{
		public 	uint   cbSize;
		public 	uint   fMask;
		public 	int    nMin;
		public 	int    nMax;
		public 	uint   nPage;
		public 	int    nPos;
		public 	int    nTrackPos;
	}
	#endregion

	#region MouseHookStruct
	[StructLayout(LayoutKind.Sequential)]
	public class MouseHookStruct 
	{
		public POINT pt;
		public int hwnd;
		public int wHitTestCode;
		public int dwExtraInfo;
	}
	#endregion

	#region KeyBoardHook
	[StructLayout(LayoutKind.Sequential)]
	public class KeyboardHookStruct
	{
		public int vkCode;	//Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
		public int scanCode; // Specifies a hardware scan code for the key. 
		public int flags;  // Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
		public int time; // Specifies the time stamp for this message.
		public int dwExtraInfo; // Specifies extra information associated with the message. 
	}
	#endregion
				
}

