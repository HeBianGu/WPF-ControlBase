using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Encryption
{
    /// <summary> 加密解密文件 </summary>
    public static class DESEncryptionExtension
    {
        /// <summary> 加密文件 </summary>
        /// <param name="inName"></param>
        /// <param name="outName"></param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
        private static void EncryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files. 
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write. 
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption. 
            long rdlen = 0;              //This is the total number of bytes written. 
            long totlen = fin.Length;    //This is the total length of the input file. 
            int len;                     //This is the number of bytes to be written at a time. 

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file. 
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();
        }

        /// <summary> 解密文件 </summary>
        /// <param name="inName"></param>
        /// <param name="outName"></param>
        /// <param name="desKey"></param>
        /// <param name="desIV"></param>
 
        private static void DecryptData(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files. 
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            //Create variables to help with read and write. 
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption. 
            long rdlen = 0;              //This is the total number of bytes written. 
            long totlen = fin.Length;    //This is the total length of the input file. 
            int len;                     //This is the number of bytes to be written at a time. 

            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

            //Read from the input file, then encrypt and write to the output file. 
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }

            encStream.Close();
            fout.Close();
            fin.Close();

        }
    }
}
