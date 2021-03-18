using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AppNetCore.Utility
{
    /// <summary>
    /// AES加密工具类(ECB,PKCS7)
    /// </summary>
    public class AESHelper
    {
        #region 变量

        /// <summary>
        /// 密钥
        /// </summary>
        private string m_CstrKey = "6687D317BFb6E2d29fdZaiB7NQM86666";                                    

        /// <summary>
        /// 编码方式
        /// </summary>
        private Encoding m_Encoding = Encoding.UTF8;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AESHelper()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AESHelper(Encoding encoding)
        {
            m_Encoding = encoding;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">加密的密钥，必须为32位字符或数字</param>
        public AESHelper(string key)
        {
            if (key.Length == 32)
            {
                m_CstrKey = key;
            }
            else if (key.Length > 0)
            {
                throw new Exception("key必须为32位字符或数字");
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">加密的密钥，必须为32位字符或数字</param>
        /// <param name="encoding">编码方式</param>
        public AESHelper(string key, Encoding encoding)
        {
            if (key.Length == 32)
            {
                m_CstrKey = key;
            }
            else if (key.Length > 0)
            {
                throw new Exception("key必须为32位字符或数字");
            }

            m_Encoding = encoding;
        }

        #endregion

        #region Encrypt

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptValue">要加密的字符串</param>
        /// <returns>加密后的Base64格式字符串</returns>
        public string Encrypt(string encryptValue) => Encrypt(encryptValue, m_Encoding);

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="encryptValue">要加密的字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>加密后的Base64格式字符串</returns>
        public string Encrypt(string encryptValue, Encoding encoding)
        {
            m_Encoding = encoding;            

            //数据加密
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = m_Encoding.GetBytes(m_CstrKey);
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                var iv = aesAlg.IV;


                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor();
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(encryptValue);
                        }
                        byte[] bytes = msEncrypt.ToArray();
                        return FromBytes(bytes);
                    }
                }
            }
        }

        #endregion

        #region Decrypt

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptValue">要解密的Base64格式字符串</param>
        /// <returns>解密的字符串</returns>
        public string Decrypt(string decryptValue) => Decrypt(decryptValue, m_Encoding);

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptValue">要解密的Base64格式字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>解密的字符串</returns>
        public string Decrypt(string decryptValue, Encoding encoding)
        {
            m_Encoding = encoding;

            //数据解密
            byte[] inputBytes = Convert.FromBase64String(decryptValue);
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = m_Encoding.GetBytes(m_CstrKey);
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                var iv = aesAlg.IV;

                using (MemoryStream msEncrypt = new MemoryStream(inputBytes))
                {
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor();
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srEncrypt = new StreamReader(csEncrypt))
                        {
                            return srEncrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion


        #region FromBytes

        /// <summary>
        /// 将Byte[]转换成Base64编码文本
        /// </summary>
        /// <param name="buffer">Byte[]</param>
        /// <returns></returns>
        public static string FromBytes(byte[] buffer)
        {
            int base64ArraySize = (int)Math.Ceiling(buffer.Length / 3d) * 4;
            char[] charBuffer = new char[base64ArraySize];
            Convert.ToBase64CharArray(buffer, 0, buffer.Length, charBuffer, 0);
            string s = new string(charBuffer);
            return s;
        }

        #endregion

    }
}
