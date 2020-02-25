using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 高亮查找文本 </summary>
    public partial class HighlightTextBlock : TextBlock
    {
        static HighlightTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HighlightTextBlock), new FrameworkPropertyMetadata(typeof(HighlightTextBlock)));
        }

        #region 依赖属性

        public static readonly DependencyProperty HighlightContentProperty =
            DependencyProperty.Register("HlContent", typeof(HighlightContent), typeof(HighlightTextBlock),
                                        new FrameworkPropertyMetadata(null, OnHtContentChanged));

        [Description("获取或设置高亮显示的内容")]
        [Category("Common Properties")]
        public HighlightContent HlContent
        {
            get { return (HighlightContent)GetValue(HighlightContentProperty); }
            set { SetValue(HighlightContentProperty, value); }
        }

        #endregion

        private static void OnHtContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs arg)
        {
            if (sender is HighlightTextBlock)
            {
                HighlightTextBlock ctrl = sender as HighlightTextBlock;
                HighlightContent content = ctrl.HlContent;
                ctrl.Inlines.Clear();
                if (content != null)
                {
                    ctrl.Inlines.AddRange(MakeRunsFromContent(content));
                }
            }
        }

        private class KeyItem
        {
            public string Key { get; set; }
            public bool Finished { get; set; }
        }

        private static IEnumerable<Run> MakeRunsFromContent(HighlightContent content)
        {
            List<Run> listRtn = new List<Run>();

            if (!string.IsNullOrWhiteSpace(content.Content))
            {
                if (string.IsNullOrWhiteSpace(HighlightContent.ToHighlight))
                {
                    listRtn.Add(new Run(content.Content));
                }
                else
                {
                    string strContent = content.Content.Trim();
                    string strToHl = HighlightContent.ToHighlight.Trim();

                    if (HighlightContent.Mode == HighlightContentMode.FullMatch)
                    {
                        int iCurrIndex = 0;
                        while (true)
                        {
                            int iFoundIndex = strContent.IndexOf(strToHl, iCurrIndex, StringComparison.OrdinalIgnoreCase);
                            if (iFoundIndex != -1)
                            {
                                if (iFoundIndex > iCurrIndex)
                                {
                                    listRtn.Add(new Run(strContent.Substring(iCurrIndex, iFoundIndex - iCurrIndex)));
                                }
                                listRtn.Add(new Run(strContent.Substring(iFoundIndex, strToHl.Length)) { Background = Brushes.Yellow });
                            }
                            else
                            {
                                if (iCurrIndex < strContent.Length)
                                {
                                    listRtn.Add(new Run(strContent.Substring(iCurrIndex, strContent.Length - iCurrIndex)));
                                }
                                break;
                            }

                            iCurrIndex = iFoundIndex + strToHl.Length;
                        }
                    }
                    else
                    {
                        int iCurrIndex = 0;
                        string[] keys = strToHl.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (keys.Length == 0)
                        {
                            listRtn.Add(new Run(content.Content));
                        }
                        else
                        {
                            var keysToAnalysis = keys.Select(k => new KeyItem { Key = k, Finished = false });
                            while (true)
                            {
                                int iFoundIndex = strContent.Length;
                                int iFoundKeyLength = 0;
                                foreach (var key in keysToAnalysis)
                                {
                                    if (!key.Finished)
                                    {
                                        int iThisFoundIndex = strContent.IndexOf(key.Key, iCurrIndex, StringComparison.OrdinalIgnoreCase);
                                        if (iThisFoundIndex == -1)
                                        {
                                            key.Finished = true;
                                        }
                                        else
                                        {
                                            if (iThisFoundIndex < iFoundIndex)
                                            {
                                                iFoundIndex = iThisFoundIndex;
                                                iFoundKeyLength = key.Key.Length;
                                            }
                                        }
                                    }
                                }

                                if (iFoundIndex == strContent.Length)
                                {
                                    listRtn.Add(new Run(strContent.Substring(iCurrIndex, strContent.Length - iCurrIndex)));
                                    break;
                                }
                                else
                                {
                                    if (iFoundIndex > iCurrIndex)
                                    {
                                        listRtn.Add(new Run(strContent.Substring(iCurrIndex, iFoundIndex - iCurrIndex)));
                                    }
                                    listRtn.Add(new Run(strContent.Substring(iFoundIndex, iFoundKeyLength)) { Background = Brushes.Yellow });
                                }

                                iCurrIndex = iFoundIndex + iFoundKeyLength;
                            }
                        }
                    }
                }
            }

            return listRtn;
        }
    }

    public enum HighlightContentMode
    {
        FullMatch,
        AnyKey
    };

    public class HighlightContent
    {
        public string Content { get; set; }
        public static string ToHighlight { get; set; }
        public static HighlightContentMode Mode { get; set; }
    }

    [ValueConversion(typeof(string), typeof(HighlightContent))]
    public class HlContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new HighlightContent { Content = (string)value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
