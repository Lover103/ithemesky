using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography;

namespace Manzana
{
    internal class Utility
    {
        static bool isdebug = false;
        static string decryptkey = "iSpriite";
        internal static void WriteLog(string content)
        {
            if (isdebug)
            {
                File.AppendAllText("c:/iSprite.log", DateTime.Now + content + "\r\n");
            }
        }

        /// <summary>
        /// 从内存中获取字符串(UTF8)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static unsafe string HeapToString(IntPtr p)
        { 
            return  HeapToString(p, Encoding.UTF8);
        }
        /// <summary>
        /// 从内存中获取字符串
        /// </summary>
        /// <param name="p">字符串头指针</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        internal static unsafe string HeapToString(IntPtr p, Encoding encoding)
        {
            if (encoding == null)
                return Marshal.PtrToStringAnsi(p);

            if (p == IntPtr.Zero)
                return null;

            int len = 0;
            while (Marshal.ReadByte(p, len) != 0)
                checked { ++len; }

            string s = new string((sbyte*)p, 0, len, encoding);
            len = s.Length;
            while (len > 0 && s[len - 1] == 0)
                --len;
            if (len == s.Length)
                return s;
            return s.Substring(0, len);
        }

        /// <summary>
        /// 将字符串写入内存
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        internal static IntPtr StringToHeap(string s)
        { 
            return StringToHeap(s, Encoding.UTF8);
        }
        /// <summary>
        /// 将字符串写入内存
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="encoding">编码</param>
        /// <exception cref="System.NotSupportedException">不支持的操作</exception>
        /// <exception cref="System.OutOfMemoryException">没有足够的内存</exception>
        /// <returns>字符串头指针地址</returns>
        internal static IntPtr StringToHeap(string s, Encoding encoding)
        {
            if (encoding == null)
                return Marshal.StringToCoTaskMemAnsi(s);

            int min_byte_count = encoding.GetMaxByteCount(1);
            char[] copy = s.ToCharArray();
            byte[] marshal = new byte[encoding.GetByteCount(copy) + min_byte_count];

            int bytes_copied = encoding.GetBytes(copy, 0, copy.Length, marshal, 0);

            if (bytes_copied != (marshal.Length - min_byte_count))
                throw new NotSupportedException("encoding.GetBytes() doesn't equal encoding.GetByteCount()!");

            IntPtr mem = Marshal.AllocCoTaskMem(marshal.Length);
            if (mem == IntPtr.Zero)
                throw new OutOfMemoryException();

            bool copied = false;
            try
            {
                Marshal.Copy(marshal, 0, mem, marshal.Length);
                copied = true;
            }
            finally
            {
                if (!copied)
                    Marshal.FreeCoTaskMem(mem);
            }

            return mem;
        }
        internal static string Decrypt(string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //将字符串转为字节数组  
            byte[] inputByteArray = new byte[source.Length / 2];
            for (int x = 0; x < source.Length / 2; x++)
            {
                int i = (Convert.ToInt32(source.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改  
            des.Key = UTF8Encoding.UTF8.GetBytes(decryptkey);
            des.IV = UTF8Encoding.UTF8.GetBytes(decryptkey);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
