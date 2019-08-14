using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
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
           var  tt= Assembly.GetEntryAssembly().GetType("HeBianGu.Applications.ControlBase.LinkWindow.ViewModel.LoyoutViewModel");


            string stss = string.Empty;
        }


        [TestMethod]
        public void TestMethod3()
        { 
          
            //BitmapImage bitUserLogo = BitmapToBitmapImage(userHeadPic);

            //Assert.AreEqual(bitUserLogo, null);
         
        }


        public BitmapImage BitmapToBitmapImage(string  url)
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

            MyClass result= service.LoadConfig<MyClass>();

        }
    }

   public class MyClass
    {
        public int MyProperty { get; set; }

        public int MyProperty1 { get; set; }

        public string Name { get; set; }


           //public string Name1 { get; set; }
    }
   
}
