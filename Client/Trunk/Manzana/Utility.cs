using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Manzana
{
    internal class Utility
    {
        /// <summary>
        /// 从内存中获取字符串(UTF8)
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static unsafe string HeapToString(IntPtr p)
        { 
            return  HeapToString(p, Encoding.UTF8);
        }
        /// <summary>
        /// 从内存中获取字符串
        /// </summary>
        /// <param name="p">字符串头指针</param>
        /// <param name="encoding">编码</param>
        /// <returns>字符串</returns>
        public static unsafe string HeapToString(IntPtr p, Encoding encoding)
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
        public static IntPtr StringToHeap(string s)
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
        public static IntPtr StringToHeap(string s, Encoding encoding)
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
    }
}
