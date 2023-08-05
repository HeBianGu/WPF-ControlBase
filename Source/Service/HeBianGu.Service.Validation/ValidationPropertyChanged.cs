// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HeBianGu.Service.Validation
{
    /// <summary> 带有数据验证功能的MVVM绑定模型基类</summary>
    public class ValidationPropertyChanged : NotifyPropertyChanged, IDataErrorInfo
    {
        public ValidationPropertyChanged() : base()
        {
            //var froms = this.GetErrorMessage();

            //this.Error = froms.FirstOrDefault();

            this.Error = "ddgd";

        }
        #region - 属性 -
        [Browsable(false)]
        public string Error { get; private set; }

        [Browsable(false)]
        public string this[string columnName]
        {
            get
            {
                List<string> results = new List<string>();

                System.Reflection.PropertyInfo property = this.GetType().GetProperty(columnName);

                IEnumerable<ValidationAttribute> attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();

                if (attrs == null || attrs.Count() == 0) return string.Empty;

                DisplayAttribute display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();


                object value = property.GetValue(this);

                foreach (ValidationAttribute r in attrs)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
                    }
                }

                return this.Error = results.FirstOrDefault();

                //return this.Error = results.FirstOrDefault();

                //return string.Join(Environment.NewLine, results);


                //var vc = new ValidationContext(this, null, null);

                //vc.MemberName = columnName;

                //var res = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

                //var result = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), vc, res);

                //if (res.Count > 0)
                //{
                //    return string.Join(Environment.NewLine, res.Select(r => r.ErrorMessage).ToArray());
                //}
                //return string.Empty;
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        /// <summary> 获取所有数据错误信息 </summary>
        public List<string> GetErrorMessage()
        {
            List<string> results = new List<string>();

            System.Reflection.PropertyInfo[] propertys = this.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo item in propertys)
            {
                IEnumerable<ValidationAttribute> collection = item.GetCustomAttributes(false)?.OfType<ValidationAttribute>();

                //  Do：检验数据有效性
                if (collection == null || collection.Count() == 0) 
                    continue;

                object value = item.GetValue(this);

                foreach (ValidationAttribute r in collection)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage ?? r.FormatErrorMessage(item.Name));
                    }
                }
            }

            return results;
        }

        /// <summary> 检查数据是否都有效 </summary>
        public bool IsValid()
        {
            List<string> message = this.GetErrorMessage();

            this.Error = message.FirstOrDefault();

            return message.Count == 0;
        }
        #endregion
    }

    public class ValidationSelectedViewModel<T> : SelectViewModel<T>, IDataErrorInfo
    {
        public ValidationSelectedViewModel(T t) : base(t)
        {

        }

        public string Error { get; private set; }

        // 验证
        public string this[string columnName]
        {
            get
            {
                if (this.Validation(columnName, out string error))
                {
                    return this.Error = null;
                }
                return this.Error = error;
            }
        }


        protected virtual bool Validation(string columnName, out string error)
        {
            error = null;
            List<string> results = new List<string>();

            System.Reflection.PropertyInfo property = this.GetType().GetProperty(columnName);

            IEnumerable<ValidationAttribute> attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();

            if (attrs == null || attrs.Count() == 0)
                return true;

            DisplayAttribute display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();
            object value = property.GetValue(this);
            foreach (ValidationAttribute r in attrs)
            {
                if (!r.IsValid(value))
                {
                    results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
                }
            }
            error = results.FirstOrDefault();
            return string.IsNullOrEmpty(error);
        }

    }

}
