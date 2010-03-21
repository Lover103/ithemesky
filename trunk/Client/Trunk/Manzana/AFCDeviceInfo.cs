using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Manzana
{
    [StructLayout(LayoutKind.Sequential)]
    public struct AFCDeviceInfo
    {
        public string Model;
        public long FileSystemTotalBytes;
        public long FileSystemFreeBytes;
        public int FileSystemBlockSize;
    }
}
