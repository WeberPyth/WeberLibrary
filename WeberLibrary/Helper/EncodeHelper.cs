using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WeberLibrary.Extend;

namespace WeberLibrary.Helper
{
    /// <summary>
    /// AES加密算法帮助类
    /// </summary>
    public class AESHelper
    {
        

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes, byte[] key, byte[] iv) 
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv; 
                ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] data = null;
                using (MemoryStream msEncrypt = new MemoryStream()) 
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, cryptoTransform, CryptoStreamMode.Write)) 
                    {
                        csEncrypt.Write(bytes, 0, bytes.Length);
                    }
                    data = msEncrypt.ToArray();
                }
                return data;
            }
        }
        #region 编码AES多态实现

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="strEncoding"></param>
        /// <param name="keyEncoding"></param>
        /// <param name="ivEncoding"></param>
        /// <returns>AES as Base64</returns>
        public static string Encrypt(string str, string key, string iv, Encoding strEncoding, Encoding keyEncoding, Encoding ivEncoding)
        {
            var bytes = String2BytesHelper.TransToBytes(str, strEncoding);
            var result = Encrypt(bytes, key, iv, keyEncoding, ivEncoding);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string key, string iv, Encoding encoding)
        {
            return Encrypt(str, key, iv, encoding, encoding, encoding);
        }

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Encrypt(string str, string key, string iv)
        {
            return Encrypt(str, key, iv, Encoding.UTF8);
        }

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="keyEncoding"></param>
        /// <param name="ivEncoding"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes, string key, string iv, Encoding keyEncoding, Encoding ivEncoding) 
        {
            byte[] aesKey = String2BytesHelper.TransToBytes(key, 32, keyEncoding);
            byte[] aesIv = String2BytesHelper.TransToBytes(iv, 16, ivEncoding);

            return Encrypt(bytes, aesKey, aesIv);
        }

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes, string key, string iv, Encoding encoding)
        {
            return Encrypt(bytes, key, iv, encoding, encoding);
        }

        /// <summary>
        /// 编码为AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytes, string key, string iv)
        {
            return Encrypt(bytes, key, iv, Encoding.UTF8);
        }
        #endregion 编码AES多态实现

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Decrpt(byte[] bytes, byte[] key, byte[] iv) 
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] data = null;
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Read))
                    {
                        using (MemoryStream msOutPut = new MemoryStream()) 
                        {
                            csDecrypt.CopyTo(msOutPut);
                            data = msOutPut.ToArray();
                            return data;
                        }
                    }
                    
                }
                
            }
        }

        #region 解码AES多态实现

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="keyEncoding"></param>
        /// <param name="ivEncoding"></param>
        /// <returns></returns>
        public static byte[] Decrpt(byte[] bytes, string key, string iv, Encoding keyEncoding, Encoding ivEncoding) 
        {
            byte[] aesKey = String2BytesHelper.TransToBytes(key, 32, keyEncoding);
            byte[] aesIv = String2BytesHelper.TransToBytes(iv, 16, ivEncoding);

            return Decrpt(bytes, aesKey, aesIv);
        }

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Decrpt(byte[] bytes, string key, string iv, Encoding encoding) 
        {
            return Decrpt(bytes, key, iv, encoding, encoding);
        }

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static byte[] Decrpt(byte[] bytes, string key, string iv)
        {
            return Decrpt(bytes, key, iv, Encoding.UTF8);
        }

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="strEncoding"></param>
        /// <param name="keyEncoding"></param>
        /// <param name="ivEncoding"></param>
        /// <returns></returns>
        public static string Decrpt(string str, string key, string iv, Encoding strEncoding, Encoding keyEncoding, Encoding ivEncoding) 
        {
            
            var result = Decrpt(Convert.FromBase64String(str), key, iv, keyEncoding, ivEncoding);
            return String2BytesHelper.TransToString(result, strEncoding);
        }

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decrpt(string str, string key, string iv, Encoding encoding) 
        {
            return Decrpt(str, key, iv, encoding, encoding, encoding);
        }

        /// <summary>
        /// 解码AES
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string Decrpt(string str, string key, string iv) 
        {
            return Decrpt(str, key, iv, Encoding.UTF8);
        }

        #endregion 解码AES多态实现

    }

    /// <summary>
    /// 字符串转化字节数组帮助类
    /// </summary>
    public class String2BytesHelper 
    {
        /// <summary>
        /// 从字符串转换到字节数组
        /// </summary>
        public static byte[] TransToBytes(string str, int length, Encoding encoding) 
        {
            byte[] bytes = encoding.GetBytes(str);
            Array.Resize(ref bytes, length);
            return bytes;
        }

        /// <summary>
        /// 从字符串转换到字节数组
        /// </summary>
        public static byte[] TransToBytes(string str, Encoding encoding) 
        {
            return TransToBytes(str, str.Length, encoding);
        }

        /// <summary>
        /// 从字符串转换到字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] TransToBytes(string str) 
        {
            return TransToBytes(str, Encoding.UTF8);
        }

        /// <summary>
        /// 从字符串转换到字节数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] TransToBytes(string str, int length) 
        {
            return TransToBytes(str, length, Encoding.UTF8);
        }

        /// <summary>
        /// 从字节数组转换到字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string TransToString(byte[] bytes, Encoding encoding)
        {
            string str = encoding.GetString(bytes);

            return str;
        }

        /// <summary>
        /// 从字节数组转换到字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string TransToString(byte[] bytes) 
        {
            return TransToString(bytes, Encoding.UTF8);
        }

    }
}
