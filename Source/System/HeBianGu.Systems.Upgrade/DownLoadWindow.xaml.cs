// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Systems.Upgrade
{
    /// <summary>
    /// DownLoadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoadWindow
    {
        public DownLoadWindow()
        {
            InitializeComponent();

            this.Loaded += DownLoadWindow_Loaded;
        }


        public string Log
        {
            get { return (string)GetValue(LogProperty); }
            set { SetValue(LogProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogProperty =
            DependencyProperty.Register("Log", typeof(string), typeof(DownLoadWindow), new PropertyMetadata("正在加载...", (d, e) =>
             {
                 DownLoadWindow control = d as DownLoadWindow;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public static string FormatBytes(long bytes)
        {
            string[] Suffix = { "Byte", "KB", "MB", "GB", "TB" };
            int i = 0;
            double dblSByte = bytes;

            if (bytes > 1024)
                for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                    dblSByte = bytes / 1024.0;

            return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }


        private void DownLoadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Url))
            {
                this.Log = "请求的下载地址是空，请检查！";
                return;
            }

            string save = UpgradeSetting.Instance.SavePath;

            if (!Directory.Exists(save))
            {
                Directory.CreateDirectory(save);
            }

            string fileName = System.IO.Path.GetFileName(this.Url);
            string savePath = System.IO.Path.Combine(save, fileName);

            Action<string, string> action = (current, total) =>
               {
                   this.Dispatcher.Invoke(() =>
                   {
                       this.Log = string.Format(UpgradeSetting.Instance.LoadFormat, FormatBytes(long.Parse(current)).PadLeft(10, ' '), FormatBytes(long.Parse(total)));

                       this.progress.Value = (int)((double.Parse(current) / double.Parse(total)) * 100);

                       if (current == total)
                       {
                           this.progress.Value = 100;

                           Task.Delay(1000).ContinueWith(l =>
                           {
                               this.Dispatcher.Invoke(() =>
                               {
                                   this.Log = $"下载完成！";
                               });

                           });

                           //  Do：关闭
                           this.CloseAnimation(this);

                           if (MessageProxy.Windower == null)
                           {
                               if (MessageBox.Show("下载完成，是否立即安装", "提示", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                                   return;
                           }
                           else
                           {
                               bool result = MessageProxy.Windower.ShowDialog("下载完成，是否立即安装");
                               if (result == false)
                                   return;
                           }

                           string extend = Path.GetExtension(savePath).ToLower();

                           if (extend.Equals(".msi") || extend.Equals(".exe"))
                               //Process.Start(savePath);
                               Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });


                           if (Path.GetExtension(savePath).ToLower().EndsWith("zip") || Path.GetExtension(savePath).ToLower().EndsWith("exe"))
                               //Process.Start(savePath);
                               Process.Start(new ProcessStartInfo(savePath) { UseShellExecute = true });

                       }
                   });

               };

            string url = this.Url;

            Task.Run(() =>
            {
                DownloadService.DownloadFile(url, savePath, action, 50);

            });
        }

        public List<string> Message
        {
            get { return (List<string>)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(List<string>), typeof(DownLoadWindow), new PropertyMetadata(default(List<string>), (d, e) =>
            {
                DownLoadWindow control = d as DownLoadWindow;

                if (control == null) return;

                List<string> config = e.NewValue as List<string>;

            }));


        public string TitleMessage
        {
            get { return (string)GetValue(TitleMessageProperty); }
            set { SetValue(TitleMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleMessageProperty =
            DependencyProperty.Register("TitleMessage", typeof(string), typeof(DownLoadWindow), new PropertyMetadata("当前更新版本:V1.0.0", (d, e) =>
             {
                 DownLoadWindow control = d as DownLoadWindow;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(DownLoadWindow), new PropertyMetadata(default(string), (d, e) =>
             {
                 DownLoadWindow control = d as DownLoadWindow;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

    }

    internal class DownloadService
    {
        /// <summary> 下载文件 </summary>        
        /// <param name="URL">下载文件地址</param>  
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public static void DownloadFile(string URL, string filename, Action<string, string> percentAction = null, int refreshTime = 1000)
        {
            float percent = 0;
            int total = 0;
            int current = 0;
            HttpWebRequest Myrq = HttpWebRequest.Create(URL) as HttpWebRequest;
            Myrq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
            //Myrq.Headers.Add("Token", Token);
            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();


            long totalBytes = myrp.ContentLength;
            total = (int)totalBytes;
            Stream st = myrp.GetResponseStream();
            Stream so = new FileStream(filename, FileMode.Create);

            long totalDownloadedByte = 0;
            byte[] by = new byte[1024];

            int osize = st.Read(by, 0, by.Length);


            // Todo ：定时刷新进度 
            if (percentAction != null)
            {
                Action action = () =>
                {
                    while (true)
                    {
                        Thread.Sleep(refreshTime);

                        // Todo ：返回进度 
                        percentAction(current.ToString(), total.ToString());

                        if (current == total) break;
                    }
                };

                Task task = new Task(action);
                task.Start();
            }


            while (osize > 0)
            {
                totalDownloadedByte = osize + totalDownloadedByte;
                so.Write(by, 0, osize);
                current = (int)totalDownloadedByte;

                osize = st.Read(by, 0, by.Length);

                percent = totalDownloadedByte / (float)totalBytes * 100;
            }
            so.Close();
            st.Close();
        }
    }

}
