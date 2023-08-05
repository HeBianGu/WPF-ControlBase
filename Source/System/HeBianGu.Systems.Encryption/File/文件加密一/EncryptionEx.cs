using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Encryption
{
    /// <summary> 文件加密扩展方法  目前使用本加密 </summary>
    public static class FileEncryptionExtension
    {

        /// <summary> 加密文件 p1= 输出文件 p2 = 密码</summary>
        public static void EncryptFile(this string filePath, string outFilePath, string passWord)
        {
            Encryption.EncryptFile(filePath, outFilePath, passWord);
        }


        /// <summary> 解密文件 p1= 输出文件 p2 = 密码</summary>
        public static void DecryptFile(this string filePath, string outFilePath, string passWord)
        {
            Encryption.DecryptFile(filePath, outFilePath, passWord);
        }


        /// <summary> 加密文件成 .dat 后缀的文件 p1 = 密码 r1 = 加密后的文件路径 </summary>
        public static string EncryptFileToDat(this string filePath, string passWord)
        {

            EncryptFile(filePath, filePath + ".dat", passWord);

            return filePath + ".dat";
        }


        /// <summary> 揭秘成去掉 .dat 后缀的文件 p1 = password r = 解密后文件路径 </summary>
        public static string DecryptFileFromDat(this string filePath, string passWord)
        {

            DecryptFile(filePath, filePath.Substring(0, filePath.Length - 4), passWord);

            return filePath.Substring(0, filePath.Length - 4);
        }
    }
}
