using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AppNetCore.Utility
{
    /// <summary>
    /// DES加密工具类(ECB,PKCS7)
    /// </summary>
    public class DESHelper
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
        public DESHelper()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DESHelper(Encoding encoding)
        {
            m_Encoding = encoding;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">加密的密钥，必须为32位字符或数字</param>
        public DESHelper(string key)
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
        public DESHelper(string key, Encoding encoding)
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

            //--------------------------------------------------------------------------------
            //数据检查
            if (encryptValue == null)
            {
                return null;
            }
            if (encryptValue.Equals(""))
            {
                return "";
            }
            //--------------------------------------------------------------------------------
            //设定加密器
            SymmetricAlgorithm objCSP = new TripleDESCryptoServiceProvider();
            //设置加密的密钥
            objCSP.Key = Convert.FromBase64String(m_CstrKey);
            //设置加密的运算模式
            objCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //设置置加密算法的填充模式
            objCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            //--------------------------------------------------------------------------------
            //建立加密器
            ICryptoTransform objCryptoTransform = objCSP.CreateEncryptor(objCSP.Key, objCSP.IV);
            if (objCryptoTransform == null)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            //获得指定编码格式的子节数组
            byte[] aryByte = m_Encoding.GetBytes(encryptValue);
            //--------------------------------------------------------------------------------
            //定义加密流
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,
                objCryptoTransform, CryptoStreamMode.Write);
            //--------------------------------------------------------------------------------
            if (objCryptoStream == null)
            {
                return null;
            }
            objCryptoStream.Write(aryByte, 0, aryByte.Length);
            //--------------------------------------------------------------------------------
            aryByte = null;
            //--------------------------------------------------------------------------------
            //释放并关闭加密流
            objCryptoStream.FlushFinalBlock();
            objCryptoStream.Close();
            objCryptoStream = null;
            //--------------------------------------------------------------------------------
            return Convert.ToBase64String(objMemoryStream.ToArray());
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

            //--------------------------------------------------------------------------------
            //数据检查
            if (decryptValue == null)
            {
                return null;
            }
            if (decryptValue.Equals(""))
            {
                return "";
            }
            //--------------------------------------------------------------------------------
            //设定加密器
            SymmetricAlgorithm objCSP = new TripleDESCryptoServiceProvider();
            //设置加密的密钥
            objCSP.Key = Convert.FromBase64String(m_CstrKey);
            //设置加密的运算模式
            objCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            //设置置加密算法的填充模式
            objCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            //--------------------------------------------------------------------------------
            //建立解密器
            ICryptoTransform objCryptoTransform = objCSP.CreateDecryptor(objCSP.Key, objCSP.IV);
            if (objCryptoTransform == null)
            {
                return null;
            }
            //--------------------------------------------------------------------------------
            //把要解密的字符串变为字符数组
            byte[] aryByte = Convert.FromBase64String(decryptValue);
            //--------------------------------------------------------------------------------
            //定义解密流
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream,
                objCryptoTransform, CryptoStreamMode.Write);
            //--------------------------------------------------------------------------------
            if (objCryptoStream == null)
            {
                return null;
            }
            objCryptoStream.Write(aryByte, 0, aryByte.Length);
            //--------------------------------------------------------------------------------
            aryByte = null;
            //--------------------------------------------------------------------------------
            //释放并关闭加密流
            objCryptoStream.FlushFinalBlock();
            objCryptoStream.Close();
            objCryptoStream = null;
            //--------------------------------------------------------------------------------
            return m_Encoding.GetString(objMemoryStream.ToArray());
        }

        #endregion

    }
}
