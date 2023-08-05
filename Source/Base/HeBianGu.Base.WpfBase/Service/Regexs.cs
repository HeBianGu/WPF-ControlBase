// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public static class Regexs
    {
        /// <summary>
        /// 只允许英文字母、数字、下划线、英文句号、以及中划线组成
        /// </summary>
        public const string Mail = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";

        /// <summary>
        /// 13012345678 手机号
        /// </summary>
        public const string Phone = @"^1(3|4|5|6|7|8|9)\d{9}$";


        /// <summary>
        /// https://google.com/
        /// </summary>
        public const string Http = @"^((http:\/\/)|(https:\/\/))?([a-zA-Z0-9]([a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])?\.)+[a-zA-Z]{2,6}(\/)";


        /// <summary>
        /// 只允许英文字母、数字、下划线、英文句号、以及中划线组成
        /// </summary>
        public const string IP = @"((?:(?:25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(?:25[0-5]|2[0-4]\d|[01]?\d?\d))";


        /// <summary>
        /// gaozihang_001 字母开头，允许5-16字节，允许字母数字下划线
        /// </summary>
        public const string Account = @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$";


        /// <summary>
        /// 汉字
        /// </summary>
        public const string Chinese = @"^[\u4e00-\u9fa5]{0,}$";


        /// <summary>
        /// 英文和数字
        /// </summary>
        public const string EnglishAndNum = @"^[A-Za-z0-9]+$";


        /// <summary>
        /// 长度为3-20的所有字符
        /// </summary>
        public const string Range = @"^.{3,20}$";


        /// <summary>
        /// 由26个英文字母组成的字符串
        /// </summary>
        public const string English = @"^[A-Za-z]+$";


        /// <summary>
        /// 由26个大写英文字母组成的字符串
        /// </summary>
        public const string EnglishUpper = @"^[A-Z]+$";


        /// <summary>
        /// 由26个小写英文字母组成的字符串
        /// </summary>
        public const string EnglishLower = @"^[a-z]+$";


        /// <summary>
        /// 由数字和26个英文字母组成的字符串
        /// </summary>
        public const string NumAndEnglish = @"^[A-Za-z0-9]+$";

        /// <summary>
        /// 由数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        public const string NumAndEnlishAndLine = @"^\w+$";

        /// <summary>
        /// 中文、英文、数字包括下划线
        /// </summary>
        public const string ChineseEnlishNumLine = @"^[\u4E00-\u9FA5A-Za-z0-9_]+$";

        /// <summary>
        /// 中文、英文、数字但不包括下划线等符号
        /// </summary>
        public const string ChineseEnlishNum = @"^[\u4E00-\u9FA5A-Za-z0-9]+$";

        /// <summary>
        /// 禁止输入含有%&',;=?$"等字符
        /// </summary>
        public const string ExcepCode = @"[^%&',;=?$\x22]+";

    }
}
