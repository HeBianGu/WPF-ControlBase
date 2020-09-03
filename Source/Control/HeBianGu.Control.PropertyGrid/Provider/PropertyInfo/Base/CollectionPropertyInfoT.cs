using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Control.PropertyGrid
{
    public abstract class CollectionProperty<T> : PropertyInfo<List<T>>
    {
        protected readonly char[] splitC = new char[] { ',', '，', '|', '\\', '/' };

        public override List<T> ConvertToValue(object value)
        {
            if (string.IsNullOrEmpty(this.Text)) return null;

            return value?.ToString().Split(splitC)?.Select(l => this.ConvertItem(l))?.ToList();
        }

        public abstract T ConvertItem(string value);

        public override bool IsTypeOf()
        {
            // 是空直接调用检查
            if (string.IsNullOrEmpty(this.Text)) return IsTypeOfItem(this.Text);

            return this.Text.Split(splitC).ToList().TrueForAll(l=>IsTypeOfItem(l));
        }

        public abstract bool IsTypeOfItem(string value);

        public override bool IsValidation(Property source)
        {
            if (!IsTypeOf())
            {
                this.Message = $"不是{typeof(T).Name}的有效值";

                return false;
            }

            this.Flag = source.Required != null ? "*" : "";

            string error = null;

            ////  Do ：检查总体
            //if (!source.IsValid(this.Text, this.DisplayName, out error))
            //{
            //    this.Message = error;
            //    return false;
            //}

            //  Do ：检查总体
            var items = this.Text?.Split(splitC).ToList();

            foreach (var item in source.GetValidations())
            {
                //  Do ：长度范围特殊处理，取数组的长度
                if (item.GetType()==typeof(Range))
                {
                    if (!item.IsValid(items?.Count))
                    {
                        this.Message = item.ErrorMessage ?? item.FormatErrorMessage(this.DisplayName);

                        return false;
                    }
                }
                else
                {
                    if (!item.IsValid(this.Text))
                    {
                        this.Message = item.ErrorMessage ?? item.FormatErrorMessage(this.DisplayName);

                        return false;
                    }
                }
            }

            //  Do ：检查子项
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];

                if (!source.ItemValidations.IsValid(item, $"索引[{i.ToString()}] 值'{item}'", out error))
                {
                    this.Message = error;
                    return false;
                }

            }

            return true;
        }

    }
}
