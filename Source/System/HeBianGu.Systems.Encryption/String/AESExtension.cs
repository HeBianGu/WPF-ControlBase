// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Encryption
{
    /// <summary>
    /// AES用于加密内容较少强度较高的信息
    /// </summary>
    public static class AESExtension
    {
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptAES(this string value, string key = "11111111111111111111111111111111")
        {
            Aes aesProvider = Aes.Create();
            aesProvider.Key = Encoding.UTF8.GetBytes(key);
            aesProvider.Mode = CipherMode.ECB;
            aesProvider.Padding = PaddingMode.PKCS7;
            using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
            {
                byte[] inputBuffers = Encoding.UTF8.GetBytes(value);
                byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                aesProvider.Clear();
                aesProvider.Dispose();
                return Convert.ToBase64String(results, 0, results.Length);
            }
        }
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="key">aes密钥，长度必须32位</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptAES(this string value, string key = "11111111111111111111111111111111")
        {
            Aes aesProvider = Aes.Create();
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.Mode = CipherMode.ECB;
                aesProvider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(value);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
