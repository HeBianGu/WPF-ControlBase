// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Control.Shape;
using HeBianGu.Control.Shuttle;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace HeBianGu.Application.TestWindow
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<ShuttleItem> _shuttles = new ObservableCollection<ShuttleItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ShuttleItem> Shuttles
        {
            get { return _shuttles; }
            set
            {
                _shuttles = value;
                RaisePropertyChanged("Shuttles");
            }
        }

        private ObservableCollection<Student> _teachers = new ObservableCollection<Student>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Student> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                RaisePropertyChanged("Teachers");
            }
        }

        private AutoViewModel autoViewModel = new AutoViewModel();
        public AutoViewModel AutoViewModel
        {
            get
            {
                return autoViewModel;
            }
            set
            {
                autoViewModel = value;
                RaisePropertyChanged();
            }
        }


        private Thickness _editValue;
        /// <summary> 说明  </summary>
        public Thickness EditValue
        {
            get { return _editValue; }
            set
            {
                _editValue = value;
                RaisePropertyChanged();
            }
        }

        private string _editValuetring;
        /// <summary> 说明  </summary>
        public string EditValueString
        {
            get { return _editValuetring; }
            set
            {
                _editValuetring = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<Thickness> _editValues = new ObservableCollection<Thickness>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Thickness> EditValues
        {
            get { return _editValues; }
            set
            {
                _editValues = value;
                RaisePropertyChanged("EditValues");
            }
        }


        private Student _student = new Student();
        /// <summary> 说明  </summary>
        public Student Student
        {
            get { return _student; }
            set
            {
                _student = value;
                RaisePropertyChanged();
            }
        }


        private int _value = 44;
        /// <summary> 说明  </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }


        private string _value1;
        /// <summary> 说明  </summary>
        public string Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                RaisePropertyChanged();
            }
        }


        private TestGeneric<string> _tstGeneric = new TestGeneric<string>();
        /// <summary> 说明  </summary>
        public TestGeneric<string> TestGeneric
        {
            get { return _tstGeneric; }
            set
            {
                _tstGeneric = value;
                RaisePropertyChanged();
            }
        }


        private bool _checked1;
        /// <summary> 说明  </summary>
        public bool Checked1
        {
            get { return _checked1; }
            set
            {
                _checked1 = value;
                RaisePropertyChanged();
            }
        }


        private bool _checked2;
        /// <summary> 说明  </summary>
        public bool Checked2
        {
            get { return _checked2; }
            set
            {
                _checked2 = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<TestViewModel> _collection = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }



        public RelayCommand ExportExcelCommand => new RelayCommand((s, e) =>
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "excel.xlsx");
            var r = ExcelServiceProxy.Instance.Export(this.Teachers, path, "teacher", out string message);
            if (!r)
            {
                MessageProxy.Snacker.ShowTime(message);
                return;
            }
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        });

        public RelayCommand ShowWindowCommand => new RelayCommand((s, e) =>
        {
            MessageProxy.WindowPresenter?.ShowSumit(e);
        });

        public RelayCommand ProgressButtonCommand => new RelayCommand(async (s, e) =>
        {
            s.IsBusy = true;
            await Task.Run(() =>
            {
                Thread.Sleep(2000);

            });
            s.IsBusy = false;
        });

        public RelayCommand ProgressButtonPercentCommand => new RelayCommand(async (s, e) =>
        {
            s.IsBusy = true;
            s.IsIndeterminate = false;
            await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    s.Percent = 1.0 * i / 100.0;
                    s.Message = $"{i}/{100}";
                    Thread.Sleep(20);
                }
            });
            s.IsBusy = false;
        });

        protected override void Init()
        {
            this.TestGeneric.Value = "泛型类测试";
            {
                ShuttleItem shuttle = new ShuttleItem();

                shuttle.Header = "第一组";

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 0; i < 20; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString() });
                }

                shuttle.ItemSource = source;

                this.Shuttles.Add(shuttle);
            }

            {
                ShuttleItem shuttle = new ShuttleItem();

                shuttle.Header = "第二组";

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 20; i < 30; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString() });
                }

                shuttle.ItemSource = source;

                this.Shuttles.Add(shuttle);
            }

            {
                ShuttleItem shuttle = new ShuttleItem();

                shuttle.Header = "第三组";

                ObservableCollection<TestViewModel> source = new ObservableCollection<TestViewModel>();

                for (int i = 30; i < 35; i++)
                {
                    source.Add(new TestViewModel() { Value = (i + 1).ToString() });
                }

                shuttle.ItemSource = source;

                this.Shuttles.Add(shuttle);
            }



            //  Do ：设置筛选器控件数据
            for (int i = 0; i < 15; i++)
            {
                this.Teachers.Add(Student.Random());
            }

            for (int i = 0; i < 5; i++)
            {
                this.EditValues.Add(new Thickness(1));
            }

            for (byte i = 0; i < 255; i++)
            {
                if (i % 50 != 0) continue;
                for (byte a = 0; a < 255; a++)
                {
                    if (a % 50 != 0) continue;
                    this.Colors.Add(Color.FromRgb(i, a, a));
                }
            }


            for (int i = 0; i < 30; i++)
            {
                this.Collection.Add(new TestViewModel() { Value = (i + 1).ToString() });
            }

        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "Button.tEST")
            {
                PropertyGrid.Show(this.Teachers?.FirstOrDefault());
            }
            else if (command == "2")
            {

            }

        }


        private double _progress;
        /// <summary> 说明  </summary>
        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                RaisePropertyChanged();
            }
        }


        //private Geometry _pathGeometry = new RectangleGeometry(new Rect(0,0,200,200));
        private Geometry _pathGeometry = GeometryFactory.File;

        /// <summary> 说明  </summary>
        public Geometry PathGeometry
        {
            get { return _pathGeometry; }
            set
            {
                _pathGeometry = value;
                RaisePropertyChanged();
            }
        }


        private Point _pathPoint;
        /// <summary> 说明  </summary>
        public Point PathPoint
        {
            get { return _pathPoint; }
            set
            {
                _pathPoint = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand GetProgressPointCommand => new RelayCommand((s, e) =>
        {
            PathGeometry geometry = this.PathGeometry.GetFlattenedPathGeometry();
            geometry.GetPointAtFractionLength(Progress, out Point point, out Point _);
            this.PathPoint = point;
        });


        private ObservableCollection<Color> _colors = new ObservableCollection<Color>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Color> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                RaisePropertyChanged("Colors");
            }
        }

    }

    internal class AutoViewModel : NotifyPropertyChanged
    {

        private bool _isShowLink;
        [Display(Name = "是否显示", GroupName = "链接", Order = 0, Prompt = "开始")]
        public bool IsShowLink
        {
            get { return _isShowLink; }
            set
            {
                _isShowLink = value;
                RaisePropertyChanged();
            }
        }


        private int _isShowLink1;
        [Display(Name = "长度", GroupName = "链接", Order = 0, Prompt = "开始")]

        public int IsShowLink1
        {
            get { return _isShowLink1; }
            set
            {
                _isShowLink1 = value;
                RaisePropertyChanged();
            }
        }


        private int _isShowLink2;
        [Display(Name = "长度", GroupName = "链接", Order = 0, Prompt = "开始")]

        public int IsShowLink2
        {
            get { return _isShowLink2; }
            set
            {
                _isShowLink2 = value;
                RaisePropertyChanged();
            }
        }


        private int _isShowLink3;
        [Display(Name = "长度", GroupName = "链接", Order = 0, Prompt = "开始")]

        public int IsShowLink3
        {
            get { return _isShowLink3; }
            set
            {
                _isShowLink3 = value;
                RaisePropertyChanged();
            }
        }


        private int _isShowLink4;
        [Display(Name = "长度", GroupName = "链接", Order = 0, Prompt = "开始")]

        public int IsShowLink4
        {
            get { return _isShowLink4; }
            set
            {
                _isShowLink4 = value;
                RaisePropertyChanged();
            }
        }

        private HorizontalAlignment _HorizontalAlignment;
        [Display(Name = "长度", GroupName = "链接", Order = 0, Prompt = "开始")]

        public HorizontalAlignment HorizontalAlignment
        {
            get { return _HorizontalAlignment; }
            set
            {
                _HorizontalAlignment = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _DateTime;
        [Display(Name = "长度", GroupName = "链接", Order = 0, Prompt = "开始")]

        public DateTime DateTime
        {
            get { return _DateTime; }
            set
            {
                _DateTime = value;
                RaisePropertyChanged();
            }
        }



        [Style(Icon = "\xe6c0")]
        [Display(Name = "运行", GroupName = "运行", Order = 0, Prompt = "开始")]
        public RelayCommand StartCommand => new RelayCommand(l => { });


        [Style(Icon = "\xe6c0")]
        [Display(Name = "停止", GroupName = "运行", Order = 1, Prompt = "开始")]
        public RelayCommand StopCommand => new RelayCommand(l =>
        {

        });


        [Style(Icon = "\xe6c0")]
        [Display(Name = "导入", GroupName = "文件", Order = 0, Prompt = "开始")]
        public RelayCommand ImportCommand => new RelayCommand(l =>
        {

        });


        [Style(Icon = "\xe6c0")]
        [Display(Name = "查找", GroupName = "页面", Order = 0, Prompt = "插入")]
        public RelayCommand FindCommand => new RelayCommand(l =>
        {

        });


        [Style(Icon = "\xe6c0")]
        [Display(Name = "添加", GroupName = "页面", Order = 0, Prompt = "插入")]
        public RelayCommand AddCommand => new RelayCommand(l =>
        {

        });


        [Style(Icon = "\xe6c0")]
        [Display(Name = "表格", GroupName = "文件", Order = 0, Prompt = "设计")]
        public RelayCommand TableCommand => new RelayCommand(l =>
        {

        });


        [Style(Icon = "\xe6c0")]
        [Display(Name = "加载", GroupName = "文件", Order = 0, Prompt = "设计")]
        public RelayCommand LoadingCommand => new RelayCommand(l =>
        {

        });

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "1")
            {

            }
            else if (command == "2")
            {

            }

        }
    }


    public class TestGeneric<T>
    {
        public T Value { get; set; }

        public static Type Type = typeof(T);
    }

    public static class TestGeneric
    {
        public static Type StringType { get; set; } = typeof(TestGeneric<string>);
    }

    public class CheckBoxTreeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool parent = (bool)values[0];
            bool current = (bool)values[1];
            CheckBox cb = values[2] as CheckBox;
            ShellViewModel shellViewModel = cb.DataContext as ShellViewModel;

            if (!parent)
            {
                if (shellViewModel.Checked2 != false)
                {
                    shellViewModel.Checked2 = false;
                    return false;
                }

            }
            return current;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { value, value };
        }
    }

    public class Courses : ObservableCollection<Course>
    {
        public Courses()
        {
            this.Add(new Course
            {
                Name = "Learning WPF",
                Id = 1001,
                StartDate = new DateTime(2010, 1, 11),
                EndDate = new DateTime(2010, 1, 22)
            });
            this.Add(new Course
            {
                Name = "Learning Silverlight",
                Id = 1002,
                StartDate = new DateTime(2010, 1, 25),
                EndDate = new DateTime(2010, 2, 5)
            });
            this.Add(new Course
            {
                Name = "Learning Expression Blend",
                Id = 1003,
                StartDate = new DateTime(2010, 2, 8),
                EndDate = new DateTime(2010, 2, 19)
            });
            this.Add(new Course
            {
                Name = "Learning LINQ",
                Id = 1004,
                StartDate = new DateTime(2010, 6, 22),
                EndDate = new DateTime(2010, 3, 5)
            });
        }
    }

    public class Course : IEditableObject, INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int _number;
        public int Id
        {
            get
            {
                return _number;
            }
            set
            {
                if (_number == value) return;
                _number = value;
                OnPropertyChanged("Id");
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (_startDate == value) return;
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (_endDate == value) return;
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        #region IEditableObject

        private Course backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as Course;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.Name = backupCopy.Name;
            this.Id = backupCopy.Id;
            this.StartDate = backupCopy.StartDate;
            this.EndDate = backupCopy.EndDate;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }

    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is BindingGroup group)
            {
                if (group.Items.Count == 0)
                    return ValidationResult.ValidResult;
                Course course = group.Items[0] as Course;
                if (course.StartDate > course.EndDate)
                {
                    return new ValidationResult(false,
                        "Start Date must be earlier than End Date.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// This class overrides the OnCanExecuteBeginEdit method of the standard grid
    /// </summary>
    public partial class EditDataGrid : System.Windows.Controls.DataGrid
    {

        /// <summary>
        /// This method overrides the 
        /// if (canExecute && HasRowValidationError) condition of the base method to allow
        /// ----entering edit mode when there is a pending validation error
        /// ---editing of other rows
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCanExecuteBeginEdit(System.Windows.Input.CanExecuteRoutedEventArgs e)
        {

            bool hasCellValidationError = false;
            bool hasRowValidationError = false;
            BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Instance;
            //Current cell
            PropertyInfo cellErrorInfo = this.GetType().BaseType.GetProperty("HasCellValidationError", bindingFlags);
            //Grid level
            PropertyInfo rowErrorInfo = this.GetType().BaseType.GetProperty("HasRowValidationError", bindingFlags);
            if (cellErrorInfo != null) hasCellValidationError = (bool)cellErrorInfo.GetValue(this, null);
            if (rowErrorInfo != null) hasRowValidationError = (bool)rowErrorInfo.GetValue(this, null);

            base.OnCanExecuteBeginEdit(e);
            if (!e.CanExecute && !hasCellValidationError && hasRowValidationError)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
        }

        #region baseOnCanExecuteBeginEdit
        //protected virtual void OnCanExecuteBeginEdit(CanExecuteRoutedEventArgs e)
        //{
        //    bool canExecute = !IsReadOnly && (CurrentCellContainer != null) && !IsEditingCurrentCell && !IsCurrentCellReadOnly && !HasCellValidationError;

        //    if (canExecute && HasRowValidationError)
        //    {
        //        DataGridCell cellContainer = GetEventCellOrCurrentCell(e);
        //        if (cellContainer != null)
        //        {
        //            object rowItem = cellContainer.RowDataItem;

        //            // When there is a validation error, only allow editing on that row
        //            canExecute = IsAddingOrEditingRowItem(rowItem);
        //        }
        //        else
        //        {
        //            // Don't allow entering edit mode when there is a pending validation error
        //            canExecute = false;
        //        }
        //    }

        //    e.CanExecute = canExecute;
        //    e.Handled = true;
        //}
        #endregion baseOnCanExecuteBeginEdit
    }
}
