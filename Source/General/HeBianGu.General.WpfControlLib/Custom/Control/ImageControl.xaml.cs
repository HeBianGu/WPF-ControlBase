// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.General.WpfControlLib
{
    public class ImageControl : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ImageControl), "S.ImageControl.Default");
        public static ComponentResourceKey ToolsKey => new ComponentResourceKey(typeof(ImageControl), "S.ImageControl.Tools");

        static ImageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageControl), new FrameworkPropertyMetadata(typeof(ImageControl)));
        }

        public ImageControl()
        {
            {
                CommandBinding binding = new CommandBinding(Commander.Open);
                binding.Executed += (l, k) =>
                {

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    openFileDialog.Filter = "jpg文件(*.jpg)|*.jpg|png文件(*.png)|*.png|所有文件(*.*)|*.*";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.Title = "打开文件";
                    openFileDialog.RestoreDirectory = true;
                    openFileDialog.Multiselect = false;
                    if (openFileDialog.ShowDialog() != true) return;

                    this.Url = openFileDialog.FileName;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.Clear);
                binding.Executed += (l, k) =>
                {
                    this.Source = null;
                    this.BitmapData = null;
                    this.Url = null;
                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.Paste);
                binding.Executed += (l, k) =>
                {
                    BitmapSource bitmap = Clipboard.GetImage();

                    if (bitmap != null)
                    {
                        this.Source = bitmap;
                        this.BitmapData = ImageService.BitmapSourceToString(bitmap);
                        return;

                    }

                    System.Collections.Specialized.StringCollection files = Clipboard.GetFileDropList();

                    foreach (string file in files)
                    {
                        if (!File.Exists(file))
                            continue;

                        if (Path.GetExtension(file).ToLower().EndsWith(".jpg") || Path.GetExtension(file).ToLower().EndsWith(".png"))
                        {
                            this.Url = file;
                            return;
                        }
                    }
                };
                this.CommandBindings.Add(binding);
            }
        }

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ImageControl), new FrameworkPropertyMetadata(default(Stretch), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is Stretch o)
                 {

                 }

                 if (e.NewValue is Stretch n)
                 {

                 }

             }));


        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageControl), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is ImageSource o)
                 {

                 }

                 if (e.NewValue is ImageSource n)
                 {

                 }

             }));


        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(ImageControl), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {
                     control.Refresh();
                 }

             }));



        public string BitmapData
        {
            get { return (string)GetValue(BitmapDataProperty); }
            set { SetValue(BitmapDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BitmapDataProperty =
            DependencyProperty.Register("BitmapData", typeof(string), typeof(ImageControl), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {
                   
                 }
                 control.Refresh();
             }));

        public int DecodePixelWidth
        {
            get { return (int)GetValue(DecodePixelWidthProperty); }
            set { SetValue(DecodePixelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelWidthProperty =
            DependencyProperty.Register("DecodePixelWidth", typeof(int), typeof(ImageControl), new FrameworkPropertyMetadata(default(int), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {
                     control.Refresh();
                 }

             }));


        public int DecodePixelHeight
        {
            get { return (int)GetValue(DecodePixelHeightProperty); }
            set { SetValue(DecodePixelHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelHeightProperty =
            DependencyProperty.Register("DecodePixelHeight", typeof(int), typeof(ImageControl), new FrameworkPropertyMetadata(default(int), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {
                     control.Refresh();
                 }

             }));


        public BitmapCacheOption CacheOption
        {
            get { return (BitmapCacheOption)GetValue(CacheOptionProperty); }
            set { SetValue(CacheOptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CacheOptionProperty =
            DependencyProperty.Register("CacheOption", typeof(BitmapCacheOption), typeof(ImageControl), new FrameworkPropertyMetadata(default(BitmapCacheOption), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is BitmapCacheOption o)
                 {

                 }

                 if (e.NewValue is BitmapCacheOption n)
                 {
                     control.Refresh();
                 }

             }));


        public BitmapCreateOptions CreateOptions
        {
            get { return (BitmapCreateOptions)GetValue(CreateOptionsProperty); }
            set { SetValue(CreateOptionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CreateOptionsProperty =
            DependencyProperty.Register("CreateOptions", typeof(BitmapCreateOptions), typeof(ImageControl), new FrameworkPropertyMetadata(default(BitmapCreateOptions), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is BitmapCreateOptions o)
                 {

                 }

                 if (e.NewValue is BitmapCreateOptions n)
                 {
                     control.Refresh();
                 }

             }));


        public void Refresh()
        {
            if (!string.IsNullOrEmpty(this.BitmapData))
            {
                BitmapImage image = ImageService.StringToBitmapImage(this.BitmapData);
                this.RefreshSource(image);
            }
            else
            {
                this.Source = null;
            }

            if (File.Exists(this.Url))
            {
                BitmapImage image = new BitmapImage(new System.Uri(this.Url, System.UriKind.Absolute));
                this.RefreshSource(image);
                this.BitmapData = ImageService.FileToString(this.Url);
            }
        }

        private void RefreshSource(BitmapImage image)
        {
            image.CreateOptions = this.CreateOptions;
            image.CacheOption = this.CacheOption;
            image.DecodePixelWidth = this.DecodePixelWidth;
            image.DecodePixelHeight = this.DecodePixelHeight;
            this.Source = image;
        }


        public ControlTemplate Warkmark
        {
            get { return (ControlTemplate)GetValue(WarkmarkProperty); }
            set { SetValue(WarkmarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WarkmarkProperty =
            DependencyProperty.Register("Warkmark", typeof(ControlTemplate), typeof(ImageControl), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is ControlTemplate o)
                 {

                 }

                 if (e.NewValue is ControlTemplate n)
                 {

                 }

             }));


        public ControlTemplate Tools
        {
            get { return (ControlTemplate)GetValue(ToolsProperty); }
            set { SetValue(ToolsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolsProperty =
            DependencyProperty.Register("Tools", typeof(ControlTemplate), typeof(ImageControl), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 ImageControl control = d as ImageControl;

                 if (control == null) return;

                 if (e.OldValue is ControlTemplate o)
                 {

                 }

                 if (e.NewValue is ControlTemplate n)
                 {

                 }

             }));



    }
}
