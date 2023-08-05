using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Encryption
{
    /// <summary>
    /// SHA1加密扩展 不可逆 SHA-1比MD5多32位密文，所以更安全
    /// </summary>
    public static class SHA1Extension
    {
        /// <summary>  
        /// SHA1加密
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string EncryptSHA1(this string value)
        {
            //UTF8编码
            var buffer = Encoding.UTF8.GetBytes(value);
            SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            var data = sha1.ComputeHash(buffer);
            var sb = new StringBuilder();
            foreach (var t in data)
            {
                //转换大写十六进制
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
