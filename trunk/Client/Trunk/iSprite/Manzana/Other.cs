using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Manzana
{

    /// <summary>
    /// 
    /// </summary>
    public enum FileProgressMode
    {
        /// <summary>
        /// 
        /// </summary>
        iPhone2PC = 0,
        /// <summary>
        /// 
        /// </summary>
        PC2iPhone = 1,
        /// <summary>
        /// 
        /// </summary>
        Internet2PC = 2
    }

    /// <summary>
    /// 文件传输进度委托
    /// </summary>
    /// <param name="success"></param>
    /// <param name="file"></param>
    /// <param name="lastErr"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FileCompletedHandler(bool success, string file, string lastErr);

    /// <summary>
    /// 文件传输完成委托
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="totalSize"></param>
    /// <param name="completeSize"></param>
    /// <param name="speed"></param>
    /// <param name="timeElapse"></param>
    /// <param name="file"></param>
    /// <param name="cancel"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FileProgressHandler(FileProgressMode mode, ulong totalSize, ulong completeSize, int speed, double timeElapse, string file, ref bool cancel);

}
