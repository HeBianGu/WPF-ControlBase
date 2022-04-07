using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace HeBianGu.App.Touch
{
    public static partial class KeyHelper
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(
        byte bVk, //虚拟键值  
        byte bScan,// 一般为0  
        int dwFlags, //这里是整数类型 0 为按下，2为释放  
        int dwExtraInfo //这里是整数类型 一般情况下设成为0  
        );

        [DllImport("user32.dll")]
        public static extern int GetFocus();
    }


    /// <summary> 模拟键盘按键 </summary>
    public static partial class KeyHelper
    {
        #region 模拟按键
        public static void Play()
        {
            keybd_event(179, 0, 0, 0);
            keybd_event(179, 0, 2, 0);
        }

        public static void Stop()
        {
            keybd_event(178, 0, 0, 0);
            keybd_event(178, 0, 2, 0);
        }

        public static void Last()
        {
            keybd_event(177, 0, 0, 0);
            keybd_event(177, 0, 2, 0);
        }

        public static void Next()
        {
            keybd_event(176, 0, 0, 0);
            keybd_event(176, 0, 2, 0);
        }
        #endregion

        /// <summary> 模拟按下指定建 Keys.ControlKey </summary>
        public static void OnKeyPress(byte keyCode)
        {
            keybd_event(keyCode, 0, 0, 0);
            keybd_event(keyCode, 0, 2, 0);
        }

        public static void OnKeyDown(byte keyCode)
        {
            keybd_event(keyCode, 0, 0, 0);
        }

        public static void OnKeyUp(byte keyCode)
        {
            keybd_event(keyCode, 0, 2, 0);
        }

        private static Dictionary<string, string> _dic = new Dictionary<string, string>();

        public static Dictionary<string, string> CodeToByte
        {
            get
            {
                if (_dic.Count == 0)
                {
                    ConvertASCII();
                }

                return _dic;
            }
        }

        private static void ConvertASCII()
        {
            string configer = @"0010 0000	32	20	（空格）(␠)
0010 0001	33	21	!
0010 0010	34	22	双引号
00100011   35  23	#
00100100   36  24  $
00100101   37  25 %
00100110   38  26 &
00100111   39  27  '
00101000   40  28 (
00101001   41  29 )
00101010   42  2A *
00101011   43  2B +
00101100   44  2C  ,
00101101   45  2D -
00101110   46  2E .
00101111   47  2F /
00110000   48  30  0
00110001   49  31  1
00110010   50  32  2
00110011   51  33  3
00110100   52  34  4
00110101   53  35  5
00110110   54  36  6
00110111   55  37  7
00111000   56  38  8
00111001   57  39  9
00111010   58  3A :
00111011   59  3B ;
00111100   60  3C <
00111101   61  3D =
00111110   62  3E >
00111111   63  3F ?
01000000   64  40  @
01000001   65  41  A
01000010   66  42  B
01000011   67  43  C
01000100   68  44  D
01000101   69  45  E
01000110   70  46  F
01000111   71  47  G
01001000   72  48  H
01001001   73  49  I
01001010   74  4A J
01001011   75  4B K
01001100   76  4C L
01001101   77  4D  M
01001110   78  4E  N
01001111   79  4F  O
01010000   80  50  P
01010001   81  51  Q
01010010   82  52  R
01010011   83  53  S
01010100   84  54  T
01010101   85  55  U
01010110   86  56  V
01010111   87  57  W
01011000   88  58  X
01011001   89  59  Y
01011010   90  5A Z
01011011   91  5B [
01011100   92  5C	\
01011101   93  5D ]
01011110   94  5E ^
01011111   95  5F  _
01100000   96  60  `
01100001   97  61  a
01100010   98  62  b
01100011   99  63  c
01100100   100 64  d
01100101   101 65  e
01100110   102 66  f
01100111   103 67  g
01101000   104 68  h
01101001   105 69  i
01101010   106 6A j
01101011   107 6B k
01101100   108 6C l
01101101   109 6D  m
01101110   110 6E  n
01101111   111 6F  o
01110000   112 70  p
01110001   113 71  q
01110010   114 72  r
01110011   115 73  s
01110100   116 74  t
01110101   117 75  u
01110110   118 76  v
01110111   119 77  w
01111000   120 78  x
01111001   121 79  y
01111010   122 7A z
01111011   123 7B  {
01111100   124 7C |
01111101   125 7D  }
01111110   126 7E  ~";


            string[] collection = configer.Split('\r');

            foreach (string item in collection)
            {
                List<string> current = item.Split(' ', '\t').ToList();

                current.RemoveAll(l => string.IsNullOrEmpty(l));

                string result = current.ToList().Aggregate((l, k) => l + "-" + k);


                if (current.Count != 4)
                {
                    Debug.WriteLine("ERR:" + result);
                }
                else
                {
                    _dic.Add(current[3], current[1]);

                    Debug.WriteLine(result);
                }
            }

        }

    }
}
