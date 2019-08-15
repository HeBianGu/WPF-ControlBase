using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media.Imaging;
using HeBianGu.Applications.ControlBase.LinkWindow.Controler;
using HeBianGu.Common.LocalConfig;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            //TreeListController controller = new TreeListController();

            //var uri = controller.TreeList();

            //var content = controller.TreeList();
        }

        [TestMethod]
        public void TestMethod2()
        {
            var tt = Assembly.GetEntryAssembly().GetType("HeBianGu.Applications.ControlBase.LinkWindow.ViewModel.LoyoutViewModel");


            string stss = string.Empty;
        }


        [TestMethod]
        public void TestMethod3()
        {

            //BitmapImage bitUserLogo = BitmapToBitmapImage(userHeadPic);

            //Assert.AreEqual(bitUserLogo, null);


            string ss = "15 00 13 7C D0 02 00 00 FF 3F 00 32 00 50 04 00 00 07 80 06 66 00 00 00 82 00 B9 04 12 00 BD 93 8E 12 00 09 04 15 00 13 7C D0 02 00 00 FF 07 40 06 00 4A";


            var collection = ss.Split(' ').ToList();

            collection.Remove(" ");

            StringBuilder sb = new StringBuilder();

            foreach (var item in collection)
            {
                var v16 = Convert.ToByte(item, 16);

                var v2 = Convert.ToString(v16, 2);

                sb.Append(v2);

            }


            Debug.WriteLine(sb.ToString());


            //System.Convert.ToString(d, 2);
            //int v = 15;

            //byte[] b = Convert;

            UserData user = new UserData();


            var filels = user.GetType().GetFields();

            foreach (var item in filels)
            {
                var marshalAs =item.GetCustomAttribute(typeof(MarshalAsAttribute));
            }


        }


        public BitmapImage BitmapToBitmapImage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
            Bitmap bitmap = new Bitmap(img);


            Bitmap bitmapSource = new Bitmap(bitmap.Width, bitmap.Height);
            int i, j;
            for (i = 0; i < bitmap.Width; i++)
                for (j = 0; j < bitmap.Height; j++)
                {
                    System.Drawing.Color pixelColor = bitmap.GetPixel(i, j);
                    System.Drawing.Color newColor = System.Drawing.Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
                    bitmapSource.SetPixel(i, j, newColor);
                }
            MemoryStream ms = new MemoryStream();
            bitmapSource.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
            bitmapImage.EndInit();
            return bitmapImage;
        }


        [TestMethod]
        public void TestMethod4()
        {

            MyClass myClass = new MyClass();

            myClass.MyProperty = 11111;
            myClass.MyProperty1 = 22222;
            myClass.Name = "hebiangu";
            //myClass.Name1 = "hebiangu1";
            LocalConfigService service = new LocalConfigService();

            service.Init(@"C:\Users\Healthy\Documents\HeBianGu\Tester\Config");

            service.SaveConfig(myClass);


        }


        [TestMethod]
        public void TestMethod5()
        {
            LocalConfigService service = new LocalConfigService();

            service.Init(@"C:\Users\Healthy\Documents\HeBianGu\Tester\Config");

            MyClass result = service.LoadConfig<MyClass>(); 

        }
    }

    public class MyClass
    {
        public int MyProperty { get; set; }

        public int MyProperty1 { get; set; }

        public string Name { get; set; }


        //public string Name1 { get; set; }
    }

    public struct UserData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, IidParameterIndex = 16)]
        public ushort id;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, IidParameterIndex = 16)]
        public ushort user;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, IidParameterIndex = 16)]
        public ushort year;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, IidParameterIndex = 23)]
        public ushort month;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, IidParameterIndex = 27)]
        public ushort date;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, IidParameterIndex = 33)]
        public ushort hour;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, IidParameterIndex = 38)]
        public ushort min;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, IidParameterIndex = 0)]
        public ushort sec;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14, IidParameterIndex = 0)]
        public ushort item;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort URO;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort BLD;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort BIL;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort KET;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort GLU;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort PRO;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort PH;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort NIT;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort LEU;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort SG;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort VC;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort MAL;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort CR;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, IidParameterIndex = 0)]
        public ushort UCA;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, IidParameterIndex = 0)]
        public ushort URO1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, IidParameterIndex = 0)]
        public ushort BLD1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, IidParameterIndex = 0)]
        public ushort BIL1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, IidParameterIndex = 0)]
        public ushort KET1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10, IidParameterIndex = 0)]
        public ushort GLU1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, IidParameterIndex = 0)]
        public ushort PRO1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, IidParameterIndex = 0)]
        public ushort PH1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, IidParameterIndex = 0)]
        public ushort NIT1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, IidParameterIndex = 0)]
        public ushort LEU1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, IidParameterIndex = 0)]
        public ushort SG1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, IidParameterIndex = 0)]
        public ushort VC1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, IidParameterIndex = 0)]
        public ushort MAL1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9, IidParameterIndex = 0)]
        public ushort CR1;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, IidParameterIndex = 0)]
        public ushort UCA1;

    }

}
