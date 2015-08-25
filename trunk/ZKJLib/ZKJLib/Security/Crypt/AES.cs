using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace ZKJLib.Security.Crypt
{
    public class AES
    {
        byte[] Key;   //密钥
        byte[] IV;    //偏移向量

        Encoding EncodingType = Encoding.Default;  //编码类型

        /// <summary>
        /// 构造函数 密钥和偏移向量输入为比特流 编码类型默认
        /// </summary>
        /// <param name="bytesKey">密钥</param>
        /// <param name="bytesIV">偏移向量</param>
        public AES(byte[] bytesKey, byte[] bytesIV)
        {
            Key = bytesKey;
            IV = bytesIV;
        }

        /// <summary>
        /// 构造函数 构造函数 密钥和偏移向量输入为比特流
        /// </summary>
        /// <param name="bytesKey">密钥</param>
        /// <param name="bytesIV">偏移向量</param>
        /// <param name="EncodingType">编码类型</param>
        public AES(byte[] bytesKey, byte[] bytesIV, Encoding encodingType)
            :this(bytesKey,bytesIV)
        {
           EncodingType = encodingType;
        }

        /// <summary>
        /// 构造函数 密钥输入为字符串，偏移向量输入为比特流 编码类型默认
        /// </summary>
        /// <param name="bytesKey">密钥</param>
        /// <param name="bytesIV">偏移向量</param>

        public AES(string strKey, byte[] bytesIV)
        {
            
            Key = EncodingType.GetBytes(Commen.StdLen(strKey,16));
            IV = bytesIV;
        }

        /// <summary>
        /// 构造函数 密钥输入为字符串，偏移向量输入为比特流
        /// </summary>
        /// <param name="bytesKey">密钥</param>
        /// <param name="bytesIV">偏移向量</param>
        /// <param name="EncodingType">编码类型</param>
        public AES(string strKey, byte[] bytesIV,Encoding encodingType)
        {
            EncodingType = encodingType;
            Key = EncodingType.GetBytes(Commen.StdLen(strKey, 16));
            IV = bytesIV;
        }

        /// <summary>
        /// 构造函数 密钥输入为字符串，偏移向量输入为字符串 编码类型默认
        /// </summary>
        /// <param name="bytesKey">密钥</param>
        /// <param name="bytesIV">偏移向量</param>
        public AES(string strKey, string strIV)
        {
            Key = EncodingType.GetBytes(Commen.StdLen(strKey,16));
            IV = EncodingType.GetBytes(Commen.StdLen(strIV, 16));          
        }

        /// <summary>
        /// 构造函数 密钥输入为字符串，偏移向量输入为字符串 
        /// </summary>
        /// <param name="bytesKey">密钥</param>
        /// <param name="bytesIV">偏移向量</param>
        /// <param name="plainType">明文类型</param>
        /// /// <param name="EncodingType">编码类型</param>
        public AES(string strKey, string strIV,  Encoding encodingType)
        {
            EncodingType = encodingType;
            Key = EncodingType.GetBytes(Commen.StdLen(strKey, 16));
            IV = EncodingType.GetBytes(Commen.StdLen(strIV, 16));         
        }


        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="plain">明文字符串</param>
        /// <returns>加密后的字符串</returns>
        public string Encrypt(string plain)
        {
            Aes aes = new AesCryptoServiceProvider();
            byte[] inputByteArray = EncodingType.GetBytes(plain);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(
                        ms, aes.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            cs.Close();
            string ret = Convert.ToBase64String(ms.ToArray());
            return ret;                   
        }


        /// <summary>
        /// 对字符串进行解密
        /// </summary>
        /// <param name="cipher">密文字符串</param>
        /// <returns>密文字符串</returns>
        public string Decrypt(string cipher)
        {
            Aes aes = new AesCryptoServiceProvider();
            byte[] inputBytesArray = Convert.FromBase64String(cipher);
            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(
                ms, aes.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
            cs.Write(inputBytesArray, 0, inputBytesArray.Length);
            cs.FlushFinalBlock();
            cs.Close();

            string ret = EncodingType.GetString(ms.ToArray());
            ms.Close();
            return ret;
        }

        public void Encrypt(string inName, string outName)
        {
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            byte[] bin = new byte[100];
            long rdlen = 0;
            long totlen = fin.Length;
            int len;

            AesCryptoServiceProvider des = new AesCryptoServiceProvider();
            CryptoStream cs = new CryptoStream(
                fout, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                cs.Write(bin, 0, len);
                rdlen = rdlen + len;
            }
            cs.Close();
            fout.Close();
            fin.Close();
        }

        public void Decrypt(string inName, string outName)
        {
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);

            byte[] bin = new byte[100];
            long rdlen = 0;
            long totlen = fin.Length;
            int len;

            AesCryptoServiceProvider des = new AesCryptoServiceProvider();
            CryptoStream cs = new CryptoStream(
                fout, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write);

            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                cs.Write(bin, 0, len);
                rdlen += len;
            }

            cs.Close();
            fin.Close();
            fout.Close();
        }
    }
}

