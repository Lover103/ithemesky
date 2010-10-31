using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace IThemeSky.Library.Util
{
    /// <summary>
    /// ���ܰ����� 
    /// </summary>
    public static class CryptoHelper
    {
        /// <summary>
        /// DES�ӽ��ܵ�Ĭ����Կ
        /// </summary>
        public static string Key
        {
            get
            {
                return "!@#ASD12";
            }
        }

        #region ʹ��Get�����滻�ؼ��ַ�Ϊȫ�ǺͰ��ת��
        /// <summary>
        /// ʹ��Get�����滻�ؼ��ַ�Ϊȫ��
        /// </summary>
        /// <param name="UrlParam"></param>
        /// <returns></returns>
        public static string UrlParamUrlEncodeRun(string UrlParam)
        {
            UrlParam = UrlParam.Replace("+", "��");
            UrlParam = UrlParam.Replace("=", "��");
            UrlParam = UrlParam.Replace("&", "��");
            UrlParam = UrlParam.Replace("?", "��");
            return UrlParam;
        }

        /// <summary>
        /// ʹ��Get�����滻�ؼ��ַ�Ϊ���
        /// </summary>
        /// <param name="UrlParam"></param>
        /// <returns></returns>
        public static string UrlParamUrlDecodeRun(string UrlParam)
        {
            UrlParam = UrlParam.Replace("��", "+");
            UrlParam = UrlParam.Replace("��", "=");
            UrlParam = UrlParam.Replace("��", "&");
            UrlParam = UrlParam.Replace("��", "?");
            return UrlParam;
        }
        #endregion

        #region  MD5����

        /// <summary>
        /// ��׼MD5����
        /// </summary>
        /// <param name="source">�������ַ���</param>
        /// <param name="addKey">�����ַ���</param>
        /// <param name="encoding">���뷽ʽ</param>
        /// <returns></returns>
        public static string MD5_Encrypt(string source, string addKey, Encoding encoding)
        {
            if (addKey.Length > 0)
            {
                source = source + addKey;
            }

            MD5 MD5 = new MD5CryptoServiceProvider();
            byte[] datSource = encoding.GetBytes(source);
            byte[] newSource = MD5.ComputeHash(datSource);
            string byte2String = null;
            for (int i = 0; i < newSource.Length; i++)
            {
                string thisByte = newSource[i].ToString("x");
                if (thisByte.Length == 1) thisByte = "0" + thisByte;
                byte2String += thisByte;
            }
            return byte2String;
        }

        /// <summary>
        /// ��׼MD5����
        /// </summary>
        /// <param name="source">�������ַ���</param>
        /// <param name="encoding">���뷽ʽ</param>
        /// <returns></returns>
        public static string MD5_Encrypt(string source, string encoding)
        {
            return MD5_Encrypt(source, string.Empty, Encoding.GetEncoding(encoding));
        }
        /// <summary>
        /// ��׼MD5����
        /// </summary>
        /// <param name="source">�����ܵ��ַ���</param>
        /// <returns></returns>
        public static string MD5_Encrypt(string source)
        {
            return MD5_Encrypt(source, string.Empty, Encoding.Default);
        }


        #endregion

        #region �������
        /// <summary>
        /// ����ʹ��MD5���ܺ��ַ���
        /// </summary>
        /// <param name="strpwd">�������ַ���</param>
        /// <returns>���ܺ��ַ���</returns>
        public static string RegUser_MD5_Pwd(string strpwd)
        {
            #region

            string appkey = "fdjf,jkgfkl"; //������һ������ַ����ټ��ܣ���������ȫЩ
            //strpwd += appkey;

            MD5 MD5 = new MD5CryptoServiceProvider();
            byte[] a = System.Text.Encoding.Default.GetBytes(appkey);
            byte[] datSource = System.Text.Encoding.Default.GetBytes(strpwd);
            byte[] b = new byte[a.Length + 4 + datSource.Length];

            int i;
            for (i = 0; i < datSource.Length; i++)
       {
                b[i] = datSource[i];
            }

            b[i++] = 163;
            b[i++] = 172;
            b[i++] = 161;
            b[i++] = 163;

            for (int k = 0; k < a.Length; k++)
            {
                b[i] = a[k];
                i++;
            }

            byte[] newSource = MD5.ComputeHash(b);
            string byte2String = null;
            for (i = 0; i < newSource.Length; i++)
            {
                string thisByte = newSource[i].ToString("x");
                if (thisByte.Length == 1) thisByte = "0" + thisByte;
                byte2String += thisByte;
            }
            return byte2String;

            #endregion
        }
        #endregion

        #region  DES �ӽ���

        /// <summary>
        /// Desc���� Encoding.Default
        /// </summary>
        /// <param name="source">�������ַ�</param>
        /// <param name="key">��Կ</param>
        /// <returns>string</returns>
        public static string DES_Encrypt(string source, string key)
        {
            if (string.IsNullOrEmpty(source))
                return null;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //���ַ����ŵ�byte������  
            byte[] inputByteArray = Encoding.Default.GetBytes(source);

            //�������ܶ������Կ��ƫ����  
            //ԭ��ʹ��ASCIIEncoding.ASCII������GetBytes����  
            //ʹ�����������������Ӣ���ı�  
            //            des.Key = UTF8Encoding.UTF8.GetBytes(key);
            //            des.IV  = UTF8Encoding.UTF8.GetBytes(key);
            des.Key = Encoding.Default.GetBytes(key);
            des.IV = Encoding.Default.GetBytes(key);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }

            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// ʹ��Ĭ��key �� DES���� Encoding.Default
        /// </summary>
        /// <param name="source">����</param>
        /// <returns>����</returns>
        public static string DES_Encrypt(string source)
        {

            return DES_Encrypt(source, "!@#ASD12");
        }
        /// <summary>
        /// ʹ��Ĭ��key �� DES���� Encoding.Default
        /// </summary>
        /// <param name="source">����</param>
        /// <returns>����</returns>
        public static string DES_Decrypt(string source)
        {
            return DES_Decrypt(source, "!@#ASD12");
        }

        /// <summary>
        /// DES���� Encoding.Default
        /// </summary>
        /// <param name="source">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static string DES_Decrypt(string source, string key)
        {
            if (string.IsNullOrEmpty(source))
                return null;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //���ַ���תΪ�ֽ�����  
            byte[] inputByteArray = new byte[source.Length / 2];
            for (int x = 0; x < source.Length / 2; x++)
            {
                int i = (Convert.ToInt32(source.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //�������ܶ������Կ��ƫ��������ֵ��Ҫ�������޸�  
            des.Key = UTF8Encoding.UTF8.GetBytes(key);
            des.IV = UTF8Encoding.UTF8.GetBytes(key);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //����StringBuild����CreateDecryptʹ�õ��������󣬱���ѽ��ܺ���ı����������  
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region SHA1����

        /// <summary>
        /// SHA1���ܣ���Ч�� PHP �� SHA1() ����
        /// </summary>
        /// <param name="source">�����ܵ��ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string SHA1_Encrypt(string source)
        {
            byte[] temp1 = Encoding.UTF8.GetBytes(source);

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
            byte[] temp2 = sha.ComputeHash(temp1);
            sha.Clear();

            //ע�⣬���������
            //string output = Convert.ToBase64String(temp2); 

            string output = BitConverter.ToString(temp2);
            output = output.Replace("-", "");
            output = output.ToLower();
            return output;
        }
        #endregion

        #region ͨ��HTTP���ݵ�Base64����
        /// <summary>
        /// ���� ͨ��HTTP���ݵ�Base64����
        /// </summary>
        /// <param name="source">����ǰ��</param>
        /// <returns>������</returns>
        public static string HttpBase64Encode(string source)
        {
            //�մ�����
            if (source == null || source.Length == 0)
            {
                return "";
            }

            //����
            string encodeString = Convert.ToBase64String(Encoding.UTF8.GetBytes(source));

            //����
            encodeString = encodeString.Replace("+", "~");
            encodeString = encodeString.Replace("/", "@");
            encodeString = encodeString.Replace("=", "$");

            //����
            return encodeString;
        }
        #endregion

        #region ͨ��HTTP���ݵ�Base64����
        /// <summary>
        /// ���� ͨ��HTTP���ݵ�Base64����
        /// </summary>
        /// <param name="source">����ǰ��</param>
        /// <returns>������</returns>
        public static string HttpBase64Decode(string source)
        {
            //�մ�����
            if (source == null || source.Length == 0)
            {
                return "";
            }

            //��ԭ
            string deocdeString = source;
            deocdeString = deocdeString.Replace("~", "+");
            deocdeString = deocdeString.Replace("@", "/");
            deocdeString = deocdeString.Replace("$", "=");

            //Base64����
            deocdeString = Encoding.UTF8.GetString(Convert.FromBase64String(deocdeString));

            //����
            return deocdeString;
        }
        #endregion

    }
}
