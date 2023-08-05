//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;

//namespace HeBianGu.Systems.Encryption
//{
//    /// <summary> 可逆加密解密 </summary>
//    public static class CodeEx
//    {
//        /// <summary> 用于对称算法的密钥 </summary>
//        static string KEY_64 = "";

//        /// <summary>  用于对称算法的初始化向量 </summary>
//        static string IV_64 = "";

//        /// <summary> 加密 </summary>
//        public static string Encode(this string data)
//        {
//            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
//            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

//            //DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();

//            var cryptoProvider = DES.Create();
//            int i = cryptoProvider.KeySize;
//            MemoryStream ms = new MemoryStream();
//            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

//            StreamWriter sw = new StreamWriter(cst);
//            sw.Write(data);
//            sw.Flush();
//            cst.FlushFinalBlock();
//            sw.Flush();
//            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);

//        }

//        /// <summary> 解密 </summary>
//        public static string Decode(string data)
//        {
//            byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(KEY_64);
//            byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV_64);

//            byte[] byEnc;
//            try
//            {
//                byEnc = Convert.FromBase64String(data);
//            }
//            catch
//            {
//                return null;
//            }

//            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
//            MemoryStream ms = new MemoryStream(byEnc);
//            CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
//            StreamReader sr = new StreamReader(cst);
//            return sr.ReadToEnd();
//        }
//    }
//}
