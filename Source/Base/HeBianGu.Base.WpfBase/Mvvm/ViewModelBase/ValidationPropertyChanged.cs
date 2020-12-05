
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 带有数据验证功能的MVVM绑定模型基类</summary>
    public class ValidationPropertyChanged : NotifyPropertyChanged, IDataErrorInfo
    {
        public ValidationPropertyChanged() : base()
        {
            //var froms = this.GetErrorMessage();

            //this.Error = froms.FirstOrDefault();

        }
        #region - 属性 -

        public string Error { get; private set; }

        // 验证
        public string this[string columnName]
        {
            get
            {
                List<string> results = new List<string>();

                var property = this.GetType().GetProperty(columnName);

                var attrs = property.GetCustomAttributes(true)?.OfType<ValidationAttribute>();

                if (attrs == null || attrs.Count() == 0) return string.Empty;

                var display = property.GetCustomAttributes(true)?.OfType<DisplayAttribute>()?.FirstOrDefault();


                var value = property.GetValue(this);

                foreach (var r in attrs)
                {
                    if (!r.IsValid(value))
                    {
                        results.Add(r.ErrorMessage ?? r.FormatErrorMessage(display == null ? columnName : display.Name));
                    }
                }

               return this.Error = results.FirstOrDefault();

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

            var propertys = this.GetType().GetProperties();

            foreach (var item in propertys)
            {
                var collection = item.GetCustomAttributes(false)?.Cast<ValidationAttribute>();

                //  Do：检验数据有效性
                if (collection == null || collection.Count() == 0) continue;

                var value = item.GetValue(this);

                foreach (var r in collection)
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
            var message = this.GetErrorMessage();

            this.Error = message.FirstOrDefault();

            return message.Count == 0;
        }
        #endregion
    }

}
