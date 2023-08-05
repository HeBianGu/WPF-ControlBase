//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.IO;
//using System.Security.Cryptography;

//namespace HeBianGu.Systems.Encryption
//{
//    /// <summary>
//    /// 软创加密类
//    /// </summary>
//    public static class Cryptography
//    {
//        /// <summary>
//        /// MD5 加密静态方法
//        /// </summary>
//        /// <param name="EncryptString">待加密的密文</param>
//        /// <returns>returns</returns>
//        public static string MD5Encrypt(string EncryptString)
//        {
//            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }

//            MD5 m_ClassMD5 = new MD5CryptoServiceProvider();

//            string m_strEncrypt = "";

//            try
//            {
//                m_strEncrypt = BitConverter.ToString(m_ClassMD5.ComputeHash(Encoding.Default.GetBytes(EncryptString))).Replace("-", "");
//            }
//            catch (ArgumentException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_ClassMD5.Clear(); }

//            return m_strEncrypt;
//        }
//        /// <summary>
//        /// DES 加密(数据加密标准，速度较快，适用于加密大量数据的场合)
//        /// </summary>
//        /// <param name="EncryptString">待加密的密文</param>
//        /// <param name="EncryptKey">加密的密钥</param>
//        /// <returns>returns</returns>
//        public static string DESEncrypt(string EncryptString, string EncryptKey)
//        {
//            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }

//            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }

//            if (EncryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }

//            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

//            string m_strEncrypt = "";

//            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();

//            try
//            {
//                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);

//                MemoryStream m_stream = new MemoryStream();

//                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);

//                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);

//                m_cstream.FlushFinalBlock();

//                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());

//                m_stream.Close(); m_stream.Dispose();

//                m_cstream.Close(); m_cstream.Dispose();
//            }
//            catch (IOException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (ArgumentException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_DESProvider.Clear(); }

//            return m_strEncrypt;
//        }
//        /// <summary>
//        /// DES 解密(数据加密标准，速度较快，适用于加密大量数据的场合)
//        /// </summary>
//        /// <param name="DecryptString">待解密的密文</param>
//        /// <param name="DecryptKey">解密的密钥</param>
//        /// <returns>returns</returns>
//        public static string DESDecrypt(string DecryptString, string DecryptKey)
//        {
//            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }

//            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }

//            if (DecryptKey.Length != 8) { throw (new Exception("密钥必须为8位")); }

//            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

//            string m_strDecrypt = "";

//            DESCryptoServiceProvider m_DESProvider = new DESCryptoServiceProvider();

//            try
//            {
//                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);

//                MemoryStream m_stream = new MemoryStream();

//                CryptoStream m_cstream = new CryptoStream(m_stream, m_DESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);

//                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);

//                m_cstream.FlushFinalBlock();

//                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

//                m_stream.Close(); m_stream.Dispose();

//                m_cstream.Close(); m_cstream.Dispose();
//            }
//            catch (IOException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (ArgumentException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_DESProvider.Clear(); }

//            return m_strDecrypt;
//        }
//        /// <summary>
//        /// RC2 加密(用变长密钥对大量数据进行加密)
//        /// </summary>
//        /// <param name="EncryptString">待加密密文</param>
//        /// <param name="EncryptKey">加密密钥</param>
//        /// <returns>returns</returns>
//        public static string RC2Encrypt(string EncryptString, string EncryptKey)
//        {
//            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }

//            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }

//            if (EncryptKey.Length < 5 || EncryptKey.Length > 16) { throw (new Exception("密钥必须为5-16位")); }

//            string m_strEncrypt = "";

//            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

//            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();

//            try
//            {
//                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);

//                MemoryStream m_stream = new MemoryStream();

//                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);

//                m_cstream.Write(m_btEncryptString, 0, m_btEncryptString.Length);

//                m_cstream.FlushFinalBlock();

//                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());

//                m_stream.Close(); m_stream.Dispose();

//                m_cstream.Close(); m_cstream.Dispose();
//            }
//            catch (IOException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (ArgumentException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_RC2Provider.Clear(); }

//            return m_strEncrypt;
//        }
//        /// <summary>
//        /// RC2 解密(用变长密钥对大量数据进行加密)
//        /// </summary>
//        /// <param name="DecryptString">待解密密文</param>
//        /// <param name="DecryptKey">解密密钥</param>
//        /// <returns>returns</returns>
//        public static string RC2Decrypt(string DecryptString, string DecryptKey)
//        {
//            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }

//            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }

//            if (DecryptKey.Length < 5 || DecryptKey.Length > 16) { throw (new Exception("密钥必须为5-16位")); }

//            byte[] m_btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

//            string m_strDecrypt = "";

//            RC2CryptoServiceProvider m_RC2Provider = new RC2CryptoServiceProvider();

//            try
//            {
//                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);

//                MemoryStream m_stream = new MemoryStream();

//                CryptoStream m_cstream = new CryptoStream(m_stream, m_RC2Provider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);

//                m_cstream.Write(m_btDecryptString, 0, m_btDecryptString.Length);

//                m_cstream.FlushFinalBlock();

//                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

//                m_stream.Close(); m_stream.Dispose();

//                m_cstream.Close(); m_cstream.Dispose();
//            }
//            catch (IOException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (ArgumentException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_RC2Provider.Clear(); }

//            return m_strDecrypt;
//        }
//        /// <summary>
//        /// 3DES 加密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
//        /// </summary>
//        /// <param name="EncryptString">待加密密文</param>
//        /// <param name="EncryptKey1">密钥一</param>
//        /// <param name="EncryptKey2">密钥二</param>
//        /// <param name="EncryptKey3">密钥三</param>
//        /// <returns>returns</returns>
//        public static string DES3Encrypt(string EncryptString, string EncryptKey1, string EncryptKey2, string EncryptKey3)
//        {
//            string m_strEncrypt = "";

//            try
//            {
//                m_strEncrypt = DESEncrypt(EncryptString, EncryptKey3);

//                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey2);

//                m_strEncrypt = DESEncrypt(m_strEncrypt, EncryptKey1);
//            }
//            catch (Exception ex) { throw ex; }

//            return m_strEncrypt;
//        }
//        /// <summary>
//        /// 3DES 解密(基于DES，对一块数据用三个不同的密钥进行三次加密，强度更高)
//        /// </summary>
//        /// <param name="DecryptString">待解密密文</param>
//        /// <param name="DecryptKey1">密钥一</param>
//        /// <param name="DecryptKey2">密钥二</param>
//        /// <param name="DecryptKey3">密钥三</param>
//        /// <returns>returns</returns>
//        public static string DES3Decrypt(string DecryptString, string DecryptKey1, string DecryptKey2, string DecryptKey3)
//        {
//            string m_strDecrypt = "";

//            try
//            {
//                m_strDecrypt = DESDecrypt(DecryptString, DecryptKey1);

//                m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey2);

//                m_strDecrypt = DESDecrypt(m_strDecrypt, DecryptKey3);
//            }
//            catch (Exception ex) { throw ex; }

//            return m_strDecrypt;
//        }
//        /// <summary>
//        /// AES 加密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
//        /// </summary>
//        /// <param name="EncryptString">待加密密文</param>
//        /// <param name="EncryptKey">加密密钥</param>
//        /// <returns></returns>
//        public static string AESEncrypt(string EncryptString, string EncryptKey)
//        {
//            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }

//            if (string.IsNullOrEmpty(EncryptKey)) { throw (new Exception("密钥不得为空")); }

//            string m_strEncrypt = "";

//            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");

//            Rijndael m_AESProvider = Rijndael.Create();

//            try
//            {
//                byte[] m_btEncryptString = Encoding.Default.GetBytes(EncryptString);

//                MemoryStream m_stream = new MemoryStream();

//                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateEncryptor(Encoding.Default.GetBytes(EncryptKey), m_btIV), CryptoStreamMode.Write);

//                m_csstream.Write(m_btEncryptString, 0, m_btEncryptString.Length); m_csstream.FlushFinalBlock();

//                m_strEncrypt = Convert.ToBase64String(m_stream.ToArray());

//                m_stream.Close(); m_stream.Dispose();

//                m_csstream.Close(); m_csstream.Dispose();
//            }
//            catch (IOException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (ArgumentException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_AESProvider.Clear(); }

//            return m_strEncrypt;
//        }
//        /// <summary>
//        /// AES 解密(高级加密标准，是下一代的加密算法标准，速度快，安全级别高，目前 AES 标准的一个实现是 Rijndael 算法)
//        /// </summary>
//        /// <param name="DecryptString">待解密密文</param>
//        /// <param name="DecryptKey">解密密钥</param>
//        /// <returns></returns>
//        public static string AESDecrypt(string DecryptString, string DecryptKey)
//        {
//            if (string.IsNullOrEmpty(DecryptString)) { throw (new Exception("密文不得为空")); }

//            if (string.IsNullOrEmpty(DecryptKey)) { throw (new Exception("密钥不得为空")); }

//            string m_strDecrypt = "";

//            byte[] m_btIV = Convert.FromBase64String("Rkb4jvUy/ye7Cd7k89QQgQ==");

//            Rijndael m_AESProvider = Rijndael.Create();

//            try
//            {
//                byte[] m_btDecryptString = Convert.FromBase64String(DecryptString);

//                MemoryStream m_stream = new MemoryStream();

//                CryptoStream m_csstream = new CryptoStream(m_stream, m_AESProvider.CreateDecryptor(Encoding.Default.GetBytes(DecryptKey), m_btIV), CryptoStreamMode.Write);

//                m_csstream.Write(m_btDecryptString, 0, m_btDecryptString.Length); m_csstream.FlushFinalBlock();

//                m_strDecrypt = Encoding.Default.GetString(m_stream.ToArray());

//                m_stream.Close(); m_stream.Dispose();

//                m_csstream.Close(); m_csstream.Dispose();
//            }
//            catch (IOException ex) { throw ex; }
//            catch (CryptographicException ex) { throw ex; }
//            catch (ArgumentException ex) { throw ex; }
//            catch (Exception ex) { throw ex; }
//            finally { m_AESProvider.Clear(); }

//            return m_strDecrypt;
//        }
//    }
//}