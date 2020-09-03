using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 用于绑定验证Model的实体，可以应用Model中的验证特性判断输入数据是否合法</summary>
    public abstract class ValidationProperty: ValidationPropertyBase
    {

        private string _text;
        /// <summary> 值  </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                //  Do ：应用实体验证数据有效性
                this.TextChanged?.Invoke(value);

                RaisePropertyChanged("Text");
            }
        }



        private string _default;
        /// <summary> 默认值  </summary>
        public string Default
        {
            get { return _default; }
            set
            {
                _default = value;
                RaisePropertyChanged("Default");
            }
        }


        /// <summary>
        /// 验证数据是否有效 并更新数据
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsValidation()
        {
            //  Do ：判断数据验证
            var collection = this.GetType().GetCustomAttributes<ValidationAttribute>()?.ToList();

            if (collection == null) return true;
           
                foreach (var r in collection)
                {
                    if (!r.IsValid(this.Text))
                    {
                        this.Message = r.ErrorMessage ?? r.FormatErrorMessage(this.DisplayName ?? this.Name);

                        //  Do ：保存实体验证失败
                        return false;
                    }
                }
            return true;
        }

        /// <summary>
        /// 用配置文件验证是否有效
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public override bool IsValidation(Property source)
        {
            List<Validation> result = source.GetValidations();

            this.Flag = source.Required != null ? "*" : "";

            foreach (var r in result)
            {
                if (!r.IsValid(this.Text))
                {
                    this.Message = r.ErrorMessage ?? r.FormatErrorMessage(this.DisplayName);

                    //  Do ：保存实体验证失败
                    return false;
                }
            }

            return base.IsValidation(source);

        }

        public override void LoadValue(object obj)
        {
            base.LoadValue(obj);

            this.Text = obj?.ToString();
        }

        public Action<string> TextChanged;
   

     
    }
}
