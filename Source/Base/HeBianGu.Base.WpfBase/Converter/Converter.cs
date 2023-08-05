// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Data;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Text;

namespace HeBianGu.Base.WpfBase
{
    public static class Converter
    {
        #region - TimeSpan -
        public static ConverterBase<int, string> GetTimeSpanStrFromSeconds => new ConverterBase<int, string>(x => TimeSpan.FromSeconds(x).TimespanToString());
        public static ConverterBase<int, string> GetTimeSpanStrFromMilliseconds => new ConverterBase<int, string>(x => TimeSpan.FromMilliseconds(x).TimespanToString());
        public static ConverterBase<int, string> GetTimeSpanStrFromMinutes => new ConverterBase<int, string>(x => TimeSpan.FromMinutes(x).TimespanToString());
        public static ConverterBase<int, string> GetTimeSpanStrFromDays => new ConverterBase<int, string>(x => TimeSpan.FromDays(x).TimespanToString());
        public static ConverterBase<int, string> GetTimeSpanStrFromHours => new ConverterBase<int, string>(x => TimeSpan.FromHours(x).TimespanToString());
        public static ConverterBase<long, string> GetTimeSpanStrFromTicks => new ConverterBase<long, string>(x => TimeSpan.FromTicks(x).TimespanToString());
        public static ConverterBase<TimeSpan, string> GetTimeSpanStr => new ConverterBase<TimeSpan, string>(x => x.TimespanToString());



        public static string TimespanToDislay(this TimeSpan t)
        {
            StringBuilder sb = new StringBuilder();
            if (t.TotalDays > 0)
                sb.Append($"{t.Days}天 ");
            if (t.TotalHours > 0)
                sb.Append($"{t.Hours}小时 ");
            if (t.TotalMinutes > 0)
                sb.Append($"{t.Minutes}分 ");
            if (t.TotalSeconds > 0)
                sb.Append($"{t.Seconds}秒");
            return sb.ToString();
        }


        public static string TimespanToString(this TimeSpan t)
        {
            StringBuilder sb = new StringBuilder();
            if (t.TotalDays > 0)
                sb.Append($"{t.Days}.");
            if (t.TotalHours > 0)
                sb.Append($"{t.Hours.ToString().PadLeft(2, '0')}:");
            if (t.TotalMinutes > 0)
                sb.Append($"{t.Minutes.ToString().PadLeft(2, '0')}:");
            if (t.TotalSeconds > 0)
                sb.Append($"{t.Seconds.ToString().PadLeft(2, '0')}");
            return sb.ToString();
        }


        #endregion

        #region - Math -
        public static ConverterBase<double, double> GetMathAbs => new ConverterBase<double, double>(x => Math.Abs(x));
        public static ConverterBase<double, double, double> GetMathPow => new ConverterBase<double, double, double>((x, y) => Math.Pow(x, y));
        public static ConverterBase<double, double> GetMathSqrt => new ConverterBase<double, double>(x => Math.Sqrt(x));
        public static ConverterBase<double, double> GetMathFloor => new ConverterBase<double, double>(x => Math.Floor(x));
        public static ConverterBase<double, double> GetMathExp => new ConverterBase<double, double>(x => Math.Exp(x));
        public static ConverterBase<double, double> GetMathLog => new ConverterBase<double, double>(x => Math.Log(x));
        public static ConverterBase<double, double> GetMathSin => new ConverterBase<double, double>(x => Math.Sin(x));
        public static ConverterBase<double, double> GetMathCos => new ConverterBase<double, double>(x => Math.Cos(x));
        public static ConverterBase<double, double> GetMathTan => new ConverterBase<double, double>(x => Math.Tan(x));
        public static ConverterBase<double, double, double> GetMathMin => new ConverterBase<double, double, double>((x, y) => Math.Min(x, y));
        public static ConverterBase<double, double, double> GetMathMax => new ConverterBase<double, double, double>((x, y) => Math.Max(x, y));
        public static ConverterBase<double, int, double> GetMathRound => new ConverterBase<double, int, double>((x, y) => Math.Round(x, y));

        public static ConverterBase<double, double, double> GetMathAddition => new ConverterBase<double, double, double>((x, y) => x + y);
        public static ConverterBase<double, double, double> GetMathMultiplication => new ConverterBase<double, double, double>((x, y) => x * y);


        #endregion

        #region - Oparetion -
        public static ConverterBase<double, double, bool> GetGreaterThan => new ConverterBase<double, double, bool>((x, y) => x > y);
        public static ConverterBase<double, double, bool> GetLessThan => new ConverterBase<double, double, bool>((x, y) => x < y);

        #endregion


        #region - string -
        public static ConverterBase<string, string> GetStringTrim => new ConverterBase<string, string>(x => x.Trim());
        public static ConverterBase<string, string> GetStringToUpper => new ConverterBase<string, string>(x => x.ToUpper());
        public static ConverterBase<string, string> GetStringToLower => new ConverterBase<string, string>(x => x.ToLower());
        public static ConverterBase<string, char[]> GetStringToCharArray => new ConverterBase<string, char[]>(x => x.ToCharArray());
        public static ConverterBase<string, bool> GetStringIsNullOrEmpty => new ConverterBase<string, bool>(x => string.IsNullOrEmpty(x));
        public static ConverterBase<string, string, bool> GetStringContains => new ConverterBase<string, string, bool>((x, y) => x.Contains(y));
        public static ConverterBase<string, string, bool> GetStringEndsWith => new ConverterBase<string, string, bool>((x, y) => x.EndsWith(y));
        public static ConverterBase<string, string, bool> GetStringStartsWith => new ConverterBase<string, string, bool>((x, y) => x.StartsWith(y));
        public static ConverterBase<string, int, string> GetStringPadLeft => new ConverterBase<string, int, string>((x, y) => x.PadLeft(y));
        public static ConverterBase<string, int, string> GetStringPadRight => new ConverterBase<string, int, string>((x, y) => x.PadRight(y));
        public static ConverterBase<string, char[], string[]> GetStringSplit => new ConverterBase<string, char[], string[]>((x, y) => x.Split(y));
        public static ConverterBase<string, int, string> GetStringSubstring => new ConverterBase<string, int, string>((x, y) => x.Substring(y));
        public static ConverterBase<object, string, string> GetStringFormat => new ConverterBase<object, string, string>((x, y) => string.Format(y, x));
        public static ConverterBase<string[], string, string> GetStringJoin => new ConverterBase<string[], string, string>((x, y) => string.Join(y, x));

        public static ConverterBase<bool, string> GetBoolenToConnectState => new ConverterBase<bool, string>(x => x ? "已连接" : "未连接");
        public static ConverterBase<bool?, string> GetBoolenNullToConnectState => new ConverterBase<bool?, string>(x => !x.HasValue ? "未启动" : x.Value ? "已连接" : "未连接") { DefaultR = "未启动" };
        public static ConverterBase<bool, string> GetBoolenToPassState => new ConverterBase<bool, string>(x => x ? "合格" : "不合格");

        public static ConverterBase<int, string, string> GetIntLessZeroString => new ConverterBase<int, string, string>((x, y) => x < 0 ? y : x.ToString());

        #endregion

        #region - IConvertible -
        public static IConvertibleConverter GetIConvertibler { get; } = new Lazy<IConvertibleConverter>().Value;
        #endregion

        #region - IEnumerable -
        public static IEnumerableConverterBase<object, object> GetIEnumerableMax => new IEnumerableConverterBase<object, object>(x => x.Max());
        public static IEnumerableConverterBase<object, object> GetIEnumerableMin => new IEnumerableConverterBase<object, object>(x => x.Min());
        public static IEnumerableConverterBase<object, object> GetIEnumerableCount => new IEnumerableConverterBase<object, object>(x => x.Count());
        public static IEnumerableConverterBase<object, object> GetIEnumerableFirstOrDefault => new IEnumerableConverterBase<object, object>(x => x.FirstOrDefault());
        public static IEnumerableConverterBase<object, object> GetIEnumerableLastOrDefault => new IEnumerableConverterBase<object, object>(x => x.LastOrDefault());

        public static IEnumerableTypeConverterBase<object, int> GetIEnumerableTake => new IEnumerableTypeConverterBase<object, int>((x, p, t) =>
        {
            IList list = Activator.CreateInstance(x.GetType()) as IList;

            foreach (var item in x.Take(p))
            {
                list.Add(item);
            }
            return list;
        });
        public static IEnumerableTypeConverterBase<object, int> GetIEnumerableCast => new IEnumerableTypeConverterBase<object, int>((x, p, t) =>
        {
            IList list = Activator.CreateInstance(x.GetType()) as IList;
            foreach (var item in x)
            {
                list.Add(item);
            }
            return list;
        });
        public static IEnumerableTypeConverterBase<object, int> GetIEnumerableElementAt => new IEnumerableTypeConverterBase<object, int>((x, p, t) =>
        {
            return x.ElementAt(p);
        });
        public static IEnumerableTypeConverterBase<object> GetIEnumerableDistinct => new IEnumerableTypeConverterBase<object>((x, t) =>
        {
            IList list = Activator.CreateInstance(x.GetType()) as IList;
            foreach (var item in x.Distinct())
            {
                list.Add(item);
            }
            return list;
        });
        public static IEnumerableTypeConverterBase<object, IEnumerable<object>> GetIEnumerableExcept => new IEnumerableTypeConverterBase<object, IEnumerable<object>>((x, p, t) =>
        {
            IList list = Activator.CreateInstance(x.GetType()) as IList;
            foreach (var item in x.Except(p))
            {
                list.Add(item);
            }
            return list;
        });
        public static IEnumerableTypeConverterBase<object> GetIEnumerableReverse => new IEnumerableTypeConverterBase<object>((x, t) =>
        {
            IList list = Activator.CreateInstance(x.GetType()) as IList;
            foreach (var item in x.Reverse())
            {
                list.Add(item);
            }
            return list;
        });
        #endregion

        #region - ICompare -
        public static ConverterBase<IComparable, IComparable, int> GetComparable => new ConverterBase<IComparable, IComparable, int>((x, y) => x.CompareTo(y));

        #endregion

        #region - Type -
        public static ConverterBase<object, Type> GetObjType => new ConverterBase<object, Type>(x => x.GetType());
        public static ConverterBase<object, string> GetObjTypeName => new ConverterBase<object, string>(x => x.GetType().Name);
        public static ConverterBase<object, string> GetObjTypeFullName => new ConverterBase<object, string>(x => x.GetType().FullName);
        public static ConverterBase<object, string> GetDiaplayName => new ConverterBase<object, string>(x => x.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? x.GetType().GetCustomAttribute<DisplayerAttribute>()?.Name);
        public static ConverterBase<object, string> GetDiaplayDescription => new ConverterBase<object, string>(x => x.GetType().GetCustomAttribute<DisplayAttribute>()?.Description);
        public static ConverterBase<object, Type, bool> GetIsAssignableFrom => new ConverterBase<object, Type, bool>((x, y) =>
        {
            var r = y.IsAssignableFrom(x.GetType());
            return r;
        });
        public static ConverterBase<object, bool> GetIsValueType => new ConverterBase<object, bool>(x => x.GetType().IsValueType);
        public static ConverterBase<object, bool> GetIsClass => new ConverterBase<object, bool>(x => x.GetType().IsClass);
        public static ConverterBase<object, bool> GetIsEnum => new ConverterBase<object, bool>(x => x.GetType().IsEnum);
        public static ConverterBase<object, bool> GetIsGenericType => new ConverterBase<object, bool>(x => x.GetType().IsGenericType);
        public static ConverterBase<object, bool> IsInterface => new ConverterBase<object, bool>(x => x.GetType().IsInterface);
        public static ConverterBase<object, bool> GetIsAbstract => new ConverterBase<object, bool>(x => x.GetType().IsAbstract);
        public static ConverterBase<object, bool> GetIsArray => new ConverterBase<object, bool>(x => x.GetType().IsArray);

        public static ConverterBase<object, IList<Displayer<ICommand>>> GetCommandDisplayers => new ConverterBase<object, IList<Displayer<ICommand>>>(x =>
        {
            var ps = x.GetType().GetProperties().Where(k => typeof(ICommand).IsAssignableFrom(k.PropertyType));

            List<Displayer<ICommand>> result = new List<Displayer<ICommand>>();
            foreach (var item in ps)
            {
                if (item.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                    continue;
                DisplayerAttribute displayer = item.GetCustomAttribute<DisplayerAttribute>(true);

                var r = new Displayer<ICommand>()
                {
                    Description = displayer.Description,
                    Icon = displayer.Icon,
                    Order = displayer.Order,
                    Name = displayer.Name,
                    GroupName = displayer.GroupName,
                    ID = displayer.ID,
                    TabName = displayer.TabName
                };
                r.Value = item.GetValue(x) as ICommand;
                result.Add(r);
            }
            return result.OrderBy(k => k.Order).ToList();
        });
        #endregion

        #region - Path -
        public static ConverterBase<string, string> GetDirectoryName => new ConverterBase<string, string>(x => System.IO.Path.GetDirectoryName(x));
        public static ConverterBase<string, string> GetExtension => new ConverterBase<string, string>(x => System.IO.Path.GetExtension(x));
        public static ConverterBase<string, string> GetFileName => new ConverterBase<string, string>(x => System.IO.Path.GetFileName(x));
        public static ConverterBase<string, string> GetFileNameWithoutExtension => new ConverterBase<string, string>(x => System.IO.Path.GetFileNameWithoutExtension(x));
        public static ConverterBase<string, string> GetFullPath => new ConverterBase<string, string>(x => System.IO.Path.GetFullPath(x));
        public static ConverterBase<string, string> GetPathRoot => new ConverterBase<string, string>(x => System.IO.Path.GetPathRoot(x));
        public static ConverterBase<string, string, string> GetChangeExtension => new ConverterBase<string, string, string>((x, y) => System.IO.Path.ChangeExtension(x, y));
        public static ConverterBase<string, bool> GetHasExtension => new ConverterBase<string, bool>(x => System.IO.Path.HasExtension(x));
        #endregion

        #region - File -
        public static ConverterBase<string, string> GetFileReadAllText => new ConverterBase<string, string>(x => System.IO.File.ReadAllText(x));
        public static ConverterBase<string, bool> GetFileExists => new ConverterBase<string, bool>(x => System.IO.File.Exists(x)) { DefaultR = false };
        public static ConverterBase<string, DateTime> GetFileCreationTime => new ConverterBase<string, DateTime>(x => System.IO.File.GetCreationTime(x));
        public static ConverterBase<string, DateTime> GetFileLastAccessTime => new ConverterBase<string, DateTime>(x => System.IO.File.GetLastAccessTime(x));
        public static ConverterBase<string, System.IO.FileAttributes> GetFileFileAttributes => new ConverterBase<string, System.IO.FileAttributes>(x => System.IO.File.GetAttributes(x));
        public static ConverterBase<string, ImageSource> GetFileImageSource => new ConverterBase<string, ImageSource>(x => new BitmapImage(new Uri(x, UriKind.RelativeOrAbsolute)));
        public static ConverterBase<string, long> GetFileLength => new ConverterBase<string, long>(x => new FileInfo(x).Length);
        public static ConverterBase<string, string> GetFileLengthDisplay => new ConverterBase<string, string>(x =>
        {
            if (x == null) return null;
            if (!File.Exists(x.ToString()))
                return null;
            FileInfo info = new FileInfo(x.ToString());
            return XConverter.ByteSizeDisplayConverter.Convert(info.Length, null, null, null)?.ToString();
        });
        #endregion

        #region - Image -
        public static ConverterBase<string, string> GetImagePixelDisplay => new ConverterBase<string, string>(x =>
        {
            if (x == null)
                return null;
            var filePath = x.ToString();
            if (!File.Exists(filePath))
                return null;
            BitmapImage bmp = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            if (bmp == null)
                return null;
            return bmp.PixelWidth + "×" + bmp.PixelHeight;
        });

        static List<Tuple<string, int, BitmapImage>> _fileCache = new List<Tuple<string, int, BitmapImage>>();
        public static ConverterBase<string, int, ImageSource> GetFileImageSourceInMemory => new ConverterBase<string, int, ImageSource>((x, p) =>
        {
            if (x == null)
                return null;
            var filePath = x.ToString();
            if (!File.Exists(filePath))
                return null;
            var find = _fileCache.FirstOrDefault(k => k.Item1 == filePath && k.Item2 == p);
            if (find != null && find.Item3 != null)
                return find.Item3;

            try
            {
                using (var filestream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (BinaryReader reader = new BinaryReader(filestream))

                    //using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open,FileAccess.ReadWrite, FileShare.ReadWrite)))
                    {
                        FileInfo fi = new FileInfo(filePath);
                        byte[] bytes = reader.ReadBytes((int)fi.Length);
                        reader.Close();
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        if (p > 0)
                            bitmapImage.DecodePixelWidth = p;
                        bitmapImage.StreamSource = new MemoryStream(bytes);
                        bitmapImage.EndInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        _fileCache.Add(Tuple.Create(filePath, p, bitmapImage));
                        return bitmapImage;
                    }
                }

            }
            catch (Exception ex)
            {
                if (p > 0)
                    return new BitmapImage(new Uri(filePath, UriKind.Absolute)) { DecodePixelWidth = p };
                return new BitmapImage(new Uri(filePath, UriKind.Absolute));
            }
        });

        #endregion

        #region - Directory -
        public static ConverterBase<string, bool> GetDirectoryExists => new ConverterBase<string, bool>(x => System.IO.Directory.Exists(x));
        public static ConverterBase<string, string[]> GetDirectoryFiles => new ConverterBase<string, string[]>(x => System.IO.Directory.GetFiles(x));
        public static ConverterBase<string, DateTime> GetDirectoryCreationTime => new ConverterBase<string, DateTime>(x => System.IO.Directory.GetCreationTime(x));
        public static ConverterBase<string, string[]> GetDirectoryDirectories => new ConverterBase<string, string[]>(x => System.IO.Directory.GetDirectories(x));
        public static ConverterBase<string, string[]> GetDirectoryFileSystemEntries => new ConverterBase<string, string[]>(x => System.IO.Directory.GetFileSystemEntries(x));
        public static ConverterBase<string, DateTime> GetDirectoryLastAccessTime => new ConverterBase<string, DateTime>(x => System.IO.Directory.GetLastAccessTime(x));
        public static ConverterBase<string, DateTime> GetDirectoryLastWriteTime => new ConverterBase<string, DateTime>(x => System.IO.Directory.GetLastWriteTime(x));
        public static ConverterBase<string, System.IO.DirectoryInfo> GetDirectoryParent => new ConverterBase<string, System.IO.DirectoryInfo>(x => System.IO.Directory.GetParent(x));

        public static ConverterBase<string, List<string>> GetAllFile => new ConverterBase<string, List<string>>(x => x.GetAllFiles());

        /// <summary> 获取当前文件夹下所有匹配的文件 </summary>
        public static List<string> GetAllFiles(this string dirPath, Predicate<FileInfo> match = null)
        {
            List<string> ss = new List<string>();

            if (!Directory.Exists(dirPath)) return ss;

            DirectoryInfo dir = new DirectoryInfo(dirPath);

            foreach (FileSystemInfo d in dir.GetFileSystemInfos())
            {
                if (d is DirectoryInfo)
                {
                    DirectoryInfo dd = d as DirectoryInfo;
                    ss.AddRange(dd.FullName.GetAllFiles(match));
                }
                else if (d is FileInfo)
                {
                    FileInfo dd = d as FileInfo;

                    if (match == null || match(dd))
                    {
                        ss.Add(d.FullName);
                    }
                }
            }

            return ss;
        }
        #endregion

        #region - Bool -
        public static ConverterBase<bool, bool> GetTrueToFalse => new ConverterBase<bool, bool>(x => !x);
        public static ConverterBase<int, bool> GetIntToBoolen => new ConverterBase<int, bool>(x => x == 1);
        public static ConverterBase<object, bool> GetNullToFalse => new ConverterBase<object, bool>(x => x != null);
        public static ConverterBase<object, bool> GetTrue => new ConverterBase<object, bool>(x => true);
        public static ConverterBase<object, bool> GetFalse => new ConverterBase<object, bool>(x => false);
        #endregion

        #region - Visibility -
        public static ConverterBase<bool?, Visibility> GetBoolNullFalseToVisble => new ConverterBase<bool?, Visibility>(x => x != false ? Visibility.Collapsed : Visibility.Visible) { DefaultR = Visibility.Collapsed };
        public static ConverterBase<bool?, Visibility> GetBoolNullAndTrueToVisble => new ConverterBase<bool?, Visibility>(x => x != false ? Visibility.Visible : Visibility.Collapsed) { DefaultR = Visibility.Visible };

        public static ConverterBase<bool, Visibility> GetTrueToCollapsed => new ConverterBase<bool, Visibility>(x => x ? Visibility.Collapsed : Visibility.Visible);
        public static ConverterBase<bool, Visibility> GetTrueToHidden => new ConverterBase<bool, Visibility>(x => x ? Visibility.Hidden : Visibility.Visible);
        public static ConverterBase<bool, Visibility> GetFalseToHidden => new ConverterBase<bool, Visibility>(x => x ? Visibility.Visible : Visibility.Hidden);
        public static ConverterBase<bool, Visibility> GetTrueToVisible => new ConverterBase<bool, Visibility>(x => x ? Visibility.Visible : Visibility.Collapsed);
        public static MultiConverterBase<bool, Visibility> GetTrueAllToVisible => new MultiConverterBase<bool, Visibility>(x => x.All(l => l == true) ? Visibility.Visible : Visibility.Collapsed);
        public static ConverterBase<object, Visibility> GetNullToCollapsed => new ConverterBase<object, Visibility>(x => x == null ? Visibility.Collapsed : Visibility.Visible) { DefaultR = Visibility.Collapsed };
        public static ConverterBase<object, Visibility> GetNullToVisible => new ConverterBase<object, Visibility>(x => x == null ? Visibility.Visible : Visibility.Collapsed) { DefaultR = Visibility.Visible };
        public static ConverterBase<IList, Visibility> GetListEmptyToCollapsed => new ConverterBase<IList, Visibility>(x => x == null || x.Count == 0 ? Visibility.Collapsed : Visibility.Visible) { DefaultR = Visibility.Collapsed };
        public static ConverterBase<IList, Visibility> GetListEmptyToVisible => new ConverterBase<IList, Visibility>(x => x == null || x.Count == 0 ? Visibility.Visible : Visibility.Collapsed) { DefaultR = Visibility.Visible };

        public static MultiConverterBase<bool, Visibility> GetAllTrueToVisible => new MultiConverterBase<bool, Visibility>(x => x.All(k => k) ? Visibility.Visible : Visibility.Collapsed);

        public static ConverterBase<int, int, Visibility> GetIntToCollapsed => new ConverterBase<int, int, Visibility>((x, y) => x == y ? Visibility.Collapsed : Visibility.Visible);
        public static ConverterBase<int, int, Visibility> GetIntToVisible => new ConverterBase<int, int, Visibility>((x, y) => x == y ? Visibility.Visible : Visibility.Collapsed);


        public static MultiConverterBase<object, Visibility> GetAllEqualsToVisible => new MultiConverterBase<object, Visibility>(x =>
        {
            var first = x.FirstOrDefault();
            if (first == null)
                return Visibility.Collapsed;
            var SSS = x.All(l => l.Equals(first));
            return x.All(l => l.Equals(first)) ? Visibility.Visible : Visibility.Collapsed;
        });

        public static MultiConverterBase<IComparable, Visibility> GetComparableAllTrueToVisible => new MultiConverterBase<IComparable, Visibility>(x =>
        {
            var first = x.FirstOrDefault();
            if (first == null)
                return Visibility.Collapsed;
            return x.All(l => l.CompareTo(first) == 0) ? Visibility.Visible : Visibility.Collapsed;
        });

        #endregion

        #region - DateTime -
        public static ConverterBase<DateTime, string> GetDateTimeToString => new ConverterBase<DateTime, string>(x => x.ToString("yyyy-MM-dd HH:mm:ss"));
        public static ConverterBase<DateTime, string> GetDateTimeToDateString => new ConverterBase<DateTime, string>(x => x.ToString("yyyy-MM-dd"));
        public static ConverterBase<DateTime, DateTime> GetDateTimeToDate => new ConverterBase<DateTime, DateTime>(x => x.Date);
        public static ConverterBase<DateTime, double, DateTime> GetDateTimeToAddDays => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddDays(y));
        public static ConverterBase<DateTime, double, DateTime> GetDateTimeToAddHours => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddHours(y));
        public static ConverterBase<DateTime, double, DateTime> GetDateTimeToAddMilliseconds => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddMilliseconds(y));
        public static ConverterBase<DateTime, double, DateTime> GetDateTimeToAddSeconds => new ConverterBase<DateTime, double, DateTime>((x, y) => x.AddSeconds(y));
        public static ConverterBase<DateTime, int, DateTime> GetDateTimeToAddMonths => new ConverterBase<DateTime, int, DateTime>((x, y) => x.AddMonths(y));
        public static ConverterBase<DateTime, long, DateTime> GetDateTimeToAddTicks => new ConverterBase<DateTime, long, DateTime>((x, y) => x.AddTicks(y));
        public static ConverterBase<DateTime, int, DateTime> GetDateTimeToAddYears => new ConverterBase<DateTime, int, DateTime>((x, y) => x.AddYears(y));
        public static ConverterBase<string, DateTime> GetDateTimeParseExact => new ConverterBase<string, DateTime>(x => DateTime.ParseExact(x, "yyyy-MM-dd HH:mm:ss", new CultureInfo("zh-CN")));
        public static ConverterBase<DateTime, long> GetDateTimeToTicks => new ConverterBase<DateTime, long>(x => x.Ticks);
        #endregion

        #region - Uri -
        public static ConverterBase<string, Uri> GetUri => new ConverterBase<string, Uri>(x => Uri.TryCreate(x, UriKind.RelativeOrAbsolute, out Uri uri) ? uri : null);
        public static ConverterBase<string, Uri> GetUriRelative => new ConverterBase<string, Uri>(x => Uri.TryCreate(x, UriKind.Relative, out Uri uri) ? uri : null);
        public static ConverterBase<string, Uri> GetUriAbsolute => new ConverterBase<string, Uri>(x => Uri.TryCreate(x, UriKind.Absolute, out Uri uri) ? uri : null);

        #endregion

        #region - Brush -
        public static ConverterBase<bool?, Brush> GetBoolenNullBrush => new ConverterBase<bool?, Brush>(x =>
        {
            if (!x.HasValue) return Brushes.Transparent;
            return x.Value ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush;
        });

        public static ConverterBase<bool?, Brush> GetBoolNullBrushDefaultGray => new ConverterBase<bool?, Brush>(x => !x.HasValue ? Brushes.Gray : x.Value ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush) { DefaultR = Brushes.Gray };

        public static ConverterBase<bool, Brush> GetBoolenBrush => new ConverterBase<bool, Brush>(x => x ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush);
        public static ConverterBase<int, Brush> GetIntBrush => new ConverterBase<int, Brush>(x =>
        {
            if (x <= 0)
                return Application.Current.FindResource(BrushKeys.Green) as Brush;
            if (x == 1)
                return Brushes.SkyBlue;
            if (x == 2)
                return Application.Current.FindResource(BrushKeys.Yellow) as Brush;
            if (x == 3)
                return Application.Current.FindResource(BrushKeys.Orange) as Brush;
            if (x == 4)
                return Application.Current.FindResource(BrushKeys.Red) as Brush;
            if (x == 5)
                return Brushes.Purple;

            return null;
        });

        public static ConverterBase<string, Brush> GetPassStateBrush => new ConverterBase<string, Brush>(x => x == "合格" ? Application.Current.FindResource(BrushKeys.Green) as Brush : Application.Current.FindResource(BrushKeys.Red) as Brush);

        public static ConverterBase<string, Brush> GetStateBrush => new ConverterBase<string, Brush>(x =>
        {
            if (x.Contains("不") || x.Contains("失败") || x.Contains("错误") || x.Contains("断开") || x.Contains("否") || x.Contains("停止"))
                return Application.Current.FindResource(BrushKeys.Red) as Brush;
            return Application.Current.FindResource(BrushKeys.Green) as Brush;
        });


        public static ConverterBase<string, Color> GetStateColor => new ConverterBase<string, Color>(x =>
        {
            if (x.Contains("不") || x.Contains("失败") || x.Contains("错误") || x.Contains("断开") || x.Contains("否") || x.Contains("停止"))
                return (Application.Current.FindResource(BrushKeys.Red) as SolidColorBrush).Color;
            return (Application.Current.FindResource(BrushKeys.Red) as SolidColorBrush).Color;
        });

        public static ConverterBase<Color, SolidColorBrush> GetColorToSolidColorBrush => new ConverterBase<Color, SolidColorBrush>(x => new SolidColorBrush(x), x => x.Color);

        public static ConverterBase<SolidColorBrush, Color> GetSolidColorBrushToColor => new ConverterBase<SolidColorBrush, Color>(x => x.Color, x => new SolidColorBrush(x));


        #endregion


        #region - TreeViewItem -
        public static ConverterBase<TreeViewItem, double, Thickness> GetTreeViewItemMargin => new ConverterBase<TreeViewItem, double, Thickness>((x, e) =>
         {
             int level = GetTreeViewItemLevel.Set(x);
             return new Thickness(level * e, 0, 0, 0);
         });

        public static ConverterBase<TreeViewItem, int> GetTreeViewItemLevel => new ConverterBase<TreeViewItem, int>(x =>
        {
            int level = 0;
            Action<TreeViewItem> action = null;
            action = t =>
            {
                var parent = ItemsControl.ItemsControlFromItemContainer(t);
                if (parent is TreeViewItem item)
                {
                    level++;
                    action(item);
                }

                return;
            };
            action.Invoke(x);
            return level;
        });

        public static ConverterBase<TreeViewItem, bool> GetTreeViewItemContainSelected => new ConverterBase<TreeViewItem, bool>(x => x.GetChild<TreeViewItem>(k => k.IsSelected) != null);

        #endregion

        #region - CornerRadius -
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadius => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, x, x, x));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusLeft => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, 0, 0, x));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusTop => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, x, 0, 0));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusRigth => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, x, x, 0));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusBottm => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, 0, x, x));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusLeftTop => new ConverterBase<double, CornerRadius>(x => new CornerRadius(x, 0, 0, 0));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusLeftBottom => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, 0, 0, x));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusRightTop => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, x, 0, 0));
        public static ConverterBase<double, CornerRadius> GetDoubleToCornerRadiusRightBottom => new ConverterBase<double, CornerRadius>(x => new CornerRadius(0, 0, x, 0));
        #endregion



    }

    public class ConverterBase<T, P, R> : IValueConverter
    {
        public ConverterBase(Func<T, P, R> set)
        {
            this.Set = set;
        }

        public ConverterBase(Func<T, P, R> set, Func<R, P, T> get) : this(set)
        {
            this.Get = get;
        }
        public Func<T, P, R> Set { get; set; }

        public Func<R, P, T> Get { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null) return default(R);
            if (value == null) return default(R);
            if (value is T t && parameter is P p)
            {
                return this.Set.Invoke(t, p);
            }
            if (!value.TryChangeType(out T t1))
            {
                return default(R);
            }

            if (!parameter.TryChangeType(out P p1))
            {
                return default(R);
            }

            return this.Set.Invoke(t1, p1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Get == null) return default(T);
            if (value == null) return default(T);

            if (value is R t && parameter is P p)
            {
                return this.Get.Invoke(t, p);
            }

            //if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(typeof(R)) && parameter is P p1)
            //{
            //    var t1 = (R)System.Convert.ChangeType(value, typeof(T));
            //    return this.Get.Invoke(t1, p1);
            //}
            if (!value.TryChangeType(out T t1))
            {
                return default(T);
            }

            if (!parameter.TryChangeType(out P p1))
            {
                return default(T);
            }

            return this.Set.Invoke(t1, p1);
        }
    }

    public class ConverterBase<T, R> : IValueConverter
    {
        public ConverterBase(Func<T, R> set)
        {
            this.Set = set;
        }

        public ConverterBase(Func<T, R> set, Func<R, T> get) : this(set)
        {
            this.Get = get;
        }
        public Func<T, R> Set { get; set; }

        public Func<R, T> Get { get; set; }

        public R DefaultR { get; set; } = default(R);
        public T DefaultT { get; set; } = default(T);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null) return this.DefaultR;
            if (value == null) return this.DefaultR;

            if (value is T t)
            {
                return this.Set.Invoke(t);
            }

            //if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(typeof(T)))
            //{
            //    var t1 = (T)System.Convert.ChangeType(value, typeof(T));
            //    return this.Set.Invoke(t1);
            //} 

            //return default(R);

            if (value.TryChangeType(out T t1))
            {
                return this.Set.Invoke(t1);
            }

            return this.DefaultR;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Get == null) return DefaultT;
            if (value == null) return DefaultT;

            if (value is R t)
            {
                return this.Get.Invoke(t);
            }

            Type ss = value.GetType();
            Type sss = typeof(R);

            //if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(typeof(R)))
            //{
            //    var t1 = (R)System.Convert.ChangeType(value, typeof(T));
            //    return this.Get.Invoke(t1);
            //}

            //return default(T);

            if (value.TryChangeType(out R t1))
            {
                return this.Get.Invoke(t1);
            }

            return DefaultT;
        }
    }

    public class MultiConverterBase<T, R> : IMultiValueConverter
    {
        public MultiConverterBase(Func<T[], R> set)
        {
            this.Set = set;
        }

        public MultiConverterBase(Func<T[], R> set, Func<R, T[]> get) : this(set)
        {
            this.Get = get;
        }
        public Func<T[], R> Set { get; set; }

        public Func<R, T[]> Get { get; set; }

        public R DefaultR { get; set; } = default(R);
        public T DefaultT { get; set; } = default(T);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null) return this.DefaultR;
            if (values == null) return this.DefaultR;
            try
            {
                T[] ts = values.Cast<T>().ToArray();
                return this.Set.Invoke(ts);
            }
            catch
            {
                return this.DefaultR;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IConvertibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IConvertible convertible && typeof(IConvertible).IsAssignableFrom(targetType))
            {
                return System.Convert.ChangeType(value, targetType);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IEnumerableConverterBase<T, R> : IValueConverter
    {
        public IEnumerableConverterBase(Func<IEnumerable<T>, R> set)
        {
            this.Set = set;
        }

        public Func<IEnumerable<T>, R> Set { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null) return default(R);
            if (value == null) return default(R);

            if (value is IEnumerable<T> t)
            {
                return this.Set.Invoke(t);
            }

            return default(R);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IEnumerableConverterBase<T, P, R> : IValueConverter
    {
        public IEnumerableConverterBase(Func<IEnumerable<T>, P, R> set)
        {
            this.Set = set;
        }

        public Func<IEnumerable<T>, P, R> Set { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null) return default(R);
            if (value == null) return default(R);

            if (value is IEnumerable<T> t && parameter is P p)
            {
                return this.Set.Invoke(t, p);
            }

            return default(R);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IEnumerableTypeConverterBase<T, P> : IValueConverter
    {
        public IEnumerableTypeConverterBase(Func<IEnumerable<T>, P, Type, object> set)
        {
            this.Set = set;
        }

        public Func<IEnumerable<T>, P, Type, object> Set { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null)
                return value;
            if (value == null)
                return null;
            if (value is IEnumerable<T> t)
            {
                if (parameter.TryChangeType(out P p))
                    return this.Set.Invoke(t, p, targetType);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IEnumerableTypeConverterBase<T> : IValueConverter
    {
        public IEnumerableTypeConverterBase(Func<IEnumerable<T>, Type, object> set)
        {
            this.Set = set;
        }

        public Func<IEnumerable<T>, Type, object> Set { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.Set == null) return value;
            if (value == null) return null;

            if (value is IEnumerable<T> t)
            {
                return this.Set.Invoke(t, targetType);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class Displayer<T>
    {
        public T Value { get; set; }
        public int Order { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public string Icon { get; set; }
        public string TabName { get; set; }
    }
}
