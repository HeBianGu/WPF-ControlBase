using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using HeBianGu.Service.Validation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.App.Above
{
    [ViewModel("Loyout")]
    internal class LoyoutViewModel : MvcViewModelBase
    {

        //private ObservableCollection<SearchComboBoxItemModel> _comboBoxItems = new ObservableCollection<SearchComboBoxItemModel>();
        ///// <summary> 说明  </summary>
        //public ObservableCollection<SearchComboBoxItemModel> ComboBoxItems
        //{
        //    get { return _comboBoxItems; }
        //    set
        //    {
        //        _comboBoxItems = value;
        //        RaisePropertyChanged("ComboBoxItems");
        //    }
        //}

        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }


        private TextBoxViewModel _textBoxViewModel = new TextBoxViewModel();
        /// <summary> 说明  </summary>
        public TextBoxViewModel TextBoxViewModel
        {
            get { return _textBoxViewModel; }
            set
            {
                _textBoxViewModel = value;
                RaisePropertyChanged("TextBoxViewModel");
            }
        }


        private bool _isChecked;
        /// <summary> 说明  </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
            }
        }


        private int _value;
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


        private DateTime? _endTime = DateTime.Now.AddDays(3);
        /// <summary> 说明  </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged();
            }
        }


        private DateTime? _startTime = DateTime.Now.AddDays(-3);
        /// <summary> 说明  </summary>
        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<string> _selectedItems = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                _selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }


        protected override void Init()
        {
            _selectedItems.CollectionChanged +=(l, k) =>
             {
               
 
             };

            //for (int i = 0; i < 60; i++)
            //{
            //    ComboBoxItems.Add(new SearchComboBoxItemModel()
            //    {
            //        Header = "ComboBoxItem" + (ComboBoxItems.Count + 1),
            //        Value = (ComboBoxItems.Count + 1),
            //        CanDelete = true
            //    });
            //}


            for (int i = 0; i < 50; i++)
            {
                Students.Add(Student.Random());
            }
        }


        protected override void Loaded(string args)
        {

        }


        ///// <summary> 命令通用方法 </summary>
        //protected override async void RelayMethod(object obj)

        //{
        //    string command = obj?.ToString();

        //    //  Do：对话消息
        //    if (command == "Sumit")
        //    {

        //    }

        //    //  Do：等待消息
        //    else if (command == "Cancel")
        //    {

        //    }
        //}

    }


    internal class TextBoxViewModel : ValidationPropertyChanged
    {
        private string _name;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[\u4e00-\u9fa5]{0,}$", ErrorMessage = "只能输入汉字！")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                RaisePropertyChanged("Name");
            }
        }


        private string _age;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "只能输入数字")]
        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }


        private string _email;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "邮箱地址不合法！")]
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }


        private string _phone;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]{9}$", ErrorMessage = "手机号码不合法！")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged("Phone");
            }
        }


        private string _account;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9_]{4,15}$", ErrorMessage = "字母开头，允许5-16字节，允许字母数字下划线！")]
        public string Accont
        {
            get { return _account; }
            set
            {
                _account = value;
                RaisePropertyChanged("Accont");
            }
        }


        private string _passWord;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "以字母开头，长度在6~18之间，只能包含字母、数字和下划线！！")]
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }


        private string _regin;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "只能5位的数字")]
        public string Regin
        {
            get { return _regin; }
            set
            {
                _regin = value;
                RaisePropertyChanged("Regin");
            }
        }


        private string _limit;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{5,8}$", ErrorMessage = "只能5-8位的数字")]
        public string Limit
        {
            get { return _limit; }
            set
            {
                _limit = value;
                RaisePropertyChanged("Limit");
            }
        }


        private string _cardID;
        /// <summary> 说明  </summary>
        [Required(ErrorMessage = "数据不能为空")]
        [RegularExpression(@"^\d{15}|\d{18}$", ErrorMessage = "身份证号码不合法！")]
        public string CardID
        {
            get { return _cardID; }
            set
            {
                _cardID = value;
                RaisePropertyChanged("CardID");
            }
        }

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Button.Click.CheckDataSumit")
            {
                if (IsValid())
                {
                    MessageProxy.Snacker.ShowTime("数据校验成功！");
                }
                else
                {
                    MessageProxy.Snacker.ShowTime("数据校验错误 - " + Error);
                }

            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion

    }
}
