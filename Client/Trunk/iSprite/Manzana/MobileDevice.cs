// Software License Agreement (BSD License)
// 
// Copyright (c) 2007, Peter Dennis Bartok <PeterDennisBartok@gmail.com>
// All rights reserved.
// 
// Redistribution and use of this software in source and binary forms, with or without modification, are
// permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above
//   copyright notice, this list of conditions and the
//   following disclaimer.
// 
// * Redistributions in binary form must reproduce the above
//   copyright notice, this list of conditions and the
//   following disclaimer in the documentation and/or other
//   materials provided with the distribution.
// 
// * Neither the name of Peter Dennis Bartok nor the names of its
//   contributors may be used to endorse or promote products
//   derived from this software without specific prior
//   written permission of Yahoo! Inc.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
// WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
// LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR
// TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

//
// Based on code developed by geohot, ixtli, nightwatch, warren
// See http://iphone.fiveforty.net/wiki/index.php?title=Main_Page
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;

namespace Manzana {
	internal enum AppleMobileErrors
	{

	}

	/// <summary>
	/// Provides the fields representing the type of notification
	/// </summary>
	public enum NotificationMessage {
		/// <summary>The iPhone was connected to the computer.</summary>
		Connected		= 1,
		/// <summary>The iPhone was disconnected from the computer.</summary>
		Disconnected	= 2,

		/// <summary>Notification from the iPhone occurred, but the type is unknown.</summary>
		Unknown			= 3,
	}

	/// <summary>
	/// Structure describing the iPhone - no longer used
	/// </summary>
	/// Just opaque block of memory - give a decent chunk
	/// 
#if false
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	public struct AMDevice {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
		internal byte[]		unknown0;		/* 0 - zero */
		internal uint		device_id;		/* 16 */
		internal uint		product_id;		/* 20 - set to AMD_IPHONE_PRODUCT_ID */
		/// <summary>Write Me</summary>
		public string		serial;			/* 24 - set to AMD_IPHONE_SERIAL */
		internal uint		unknown1;		/* 28 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
		internal byte[]		unknown2;		/* 32 */
		internal uint		lockdown_conn;	/* 36 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		internal byte[]		unknown3;		/* 40 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=6*16+1)]
		internal byte[]		unknown4;		/* 48  + in iTunes 8.0, by iFunbox.dev */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		internal byte[]		unknown5;		/* 97  + in iTunes 8.0, by iFunbox.dev */
	}

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMDeviceNotification {
		uint						unknown0;	/* 0 */
		uint						unknown1;	/* 4 */
		uint						unknown2;	/* 8 */
		DeviceNotificationCallback	callback;   /* 12 */ 
		uint						unknown3;	/* 16 */
	}
#endif
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMDeviceNotificationCallbackInfo {
		unsafe public void* dev {
			get {
				return dev_ptr;
			}
		}
		unsafe internal void* dev_ptr;
		public NotificationMessage msg;
	}


	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct AMRecoveryDevice {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
		public byte[]	unknown0;			/* 0 */
		public DeviceRestoreNotificationCallback	callback;		/* 8 */
		public IntPtr	user_info;			/* 12 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)]
		public byte[]	unknown1;			/* 16 */
		public uint		readwrite_pipe;		/* 28 */
		public byte		read_pipe;          /* 32 */
		public byte		write_ctrl_pipe;    /* 33 */
		public byte		read_unknown_pipe;  /* 34 */
		public byte		write_file_pipe;    /* 35 */
		public byte		write_input_pipe;   /* 36 */
	};

#if false
	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct afc_directory {
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=0)]
		byte[] unknown;   /* size unknown */
	};

	[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1)]
	internal struct afc_connection {
		uint handle;            /* 0 */
		uint unknown0;          /* 4 */
		byte unknown1;         /* 8 */
		[MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
		byte[] padding;			/* 9 */
		uint unknown2;          /* 12 */
		uint unknown3;          /* 16 */
		uint unknown4;          /* 20 */
		uint fs_block_size;     /* 24 */
		uint sock_block_size;   /* 28: always 0x3c */
		uint io_timeout;        /* 32: from AFCConnectionOpen, usu. 0 */
		IntPtr afc_lock;                 /* 36 */
		uint context;           /* 40 */
	};
#endif

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DeviceNotificationCallback(ref AMDeviceNotificationCallbackInfo callback_info);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void DeviceRestoreNotificationCallback(ref AMRecoveryDevice callback_info);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DeviceEventSink(IntPtr str, IntPtr user);

	internal class MobileDevice 
    {
        const string DLLName = "iTunesMobileDevice.dll";
        static string iTunesPath = string.Empty;
        internal static string iTunesVer = string.Empty;

        static MobileDevice()
        {
            FileInfo iTunesMobileDeviceFile;
            object c = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Mobile Device Support\Shared", "iTunesMobileDeviceDLL", DLLName);
            if (null != c)
            {
                iTunesMobileDeviceFile = new FileInfo(c.ToString());
                if (iTunesMobileDeviceFile.Exists)
                {
                    iTunesPath = iTunesMobileDeviceFile.DirectoryName + "\\";
                }
            }

            string ApplicationSupportPath = string.Empty;
            object o = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Apple Inc.\Apple Application Support", "InstallDir", "");
            if (null != o)
            {
                DirectoryInfo ApplicationSupportDirectory = new DirectoryInfo(o.ToString());
                if (ApplicationSupportDirectory.Exists)
                {
                    ApplicationSupportPath = ApplicationSupportDirectory.FullName;
                }
            }


            if (iTunesPath == string.Empty)
            {
                List<string> paths = new List<string>();
                iTunesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles).TrimEnd('\\')
                    + @"\Apple\Mobile Device Support\";
                paths.Add(iTunesPath);
                paths.Add(iTunesPath + "bin\\");
                paths.Add(iTunesPath.Replace(@"Program Files\Common Files", @"Program Files (x86)\Common Files"));
                paths.Add(iTunesPath.Replace(@"Program Files\Common Files", @"Program Files (x86)\Common Files") + "bin\\");

                foreach (string path in paths)
                {
                    if (File.Exists(path + DLLName))
                    {
                        iTunesPath = path;
                        break;
                    }
                }
            }
            if (File.Exists(iTunesPath + DLLName))
            {
                // try to find the dll automatically
                if (ApplicationSupportPath != string.Empty)
                {
                    Environment.SetEnvironmentVariable("Path",
                        string.Join(";", new String[] 
                            { 
                                Environment.GetEnvironmentVariable("Path"), 
                                iTunesPath,
                                ApplicationSupportPath
                            }
                            )
                            );
                }
                else
                {
                    Environment.SetEnvironmentVariable("Path",
                        string.Join(";", new String[] 
                            { 
                                Environment.GetEnvironmentVariable("Path"), 
                                iTunesPath
                            }
                            )
                            );
                }
                try
                {
                    FileVersionInfo fInfo = FileVersionInfo.GetVersionInfo(iTunesPath + DLLName);//为磁盘上的物理文件提供版本信息
                    iTunesVer = fInfo.ProductVersion;
                }
                catch
                { 
                }
            }
        }

        /// <summary>
        /// 判断iTunes是否已经安装
        /// </summary>
        /// <returns></returns>
        public static bool IsInstalliTunes()
        {
            return File.Exists(iTunesPath + "\\" + DLLName);
        }

		[DllImport("CoreFoundation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern IntPtr __CFStringMakeConstantString(byte[] s);

        public static unsafe IntPtr CFStringMakeConstantString(string s)
        {
			return __CFStringMakeConstantString(StringToCString(s));
		}

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static IntPtr AMDeviceCopyDeviceIdentifier(void* device);

        //@"C:\Program Files\Common Files\Apple\Mobile Device Support\iTunesMobileDevice.dll"
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceNotificationSubscribe(DeviceNotificationCallback callback, uint unused1, uint unused2, uint unused3, out void* am_device_notification_ptr);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AMDPostNotification(void* ptr, IntPtr text, uint unknown);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceConnect(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceDisconnect(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceIsPaired(void* device);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceValidatePairing(void* device);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceStartSession(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceStopSession(void* device);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AMDeviceGetConnectionID(void* device);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		public extern static int AMRestoreModeDeviceCreate(uint unknown0, int connection_id, uint unknown1);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static int AFCDirectoryOpen(void* conn, IntPtr path, ref void* dir);

        //[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        //unsafe public extern static int AFCDirectoryOpen(void* conn, byte[] path, ref void* dir);

        //[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        //unsafe public static int AFCDirectoryOpen(void* conn, string path, ref void* dir) {
        //    return AFCDirectoryOpen(conn, Encoding.UTF8.GetBytes(path), ref dir);
        //}
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AMDListenForNotifications(void* conn, DeviceEventSink callback, IntPtr userdata);
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AMDObserveNotification(void* conn, IntPtr notification);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AMSBeginSync(uint unknown, void* device, uint unk2, uint unk3);
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int AMSInitialize();

		unsafe public static int AFCDirectoryRead(void* conn, void* dir, ref string buffer) {
			int ret;

            IntPtr ptr = IntPtr.Zero;
			ret = AFCDirectoryRead(conn, dir, ref ptr);
			if ((ret == 0) && (ptr != null)) {
                buffer = Utility.HeapToString(ptr);
			} else {
				buffer = null;
			}
			return ret;
		}
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static int AFCDirectoryRead(void* conn, void* dir, ref IntPtr dirent);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCDirectoryClose(void* conn, void* dir);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AMRestoreRegisterForDeviceNotifications(
			DeviceRestoreNotificationCallback dfu_connect, 
			DeviceRestoreNotificationCallback recovery_connect, 
			DeviceRestoreNotificationCallback dfu_disconnect,
			DeviceRestoreNotificationCallback recovery_disconnect,
			uint unknown0,
			void* user_info);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static int AMDeviceStartService(void* device, IntPtr service_name, ref void* handle, void* unknown);

        unsafe public static int AMDeviceStartService(void* device, string service_name, ref void* handle, void* unknown)
        {
            return AMDeviceStartService(device, StringToCFString(service_name), ref handle, unknown);
        }

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionOpen(void* handle, uint io_timeout, ref void* conn);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionIsValid(void* conn);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
		unsafe public extern static int AFCConnectionInvalidate(void* conn);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AFCConnectionClose(void* conn);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AFCFileRefLock(void* conn, long handle);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AFCFileRefUnlock(void* conn, long handle);
        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern int AFCDeviceInfoOpen(void* conn, ref void* buffer);

		/*
	Valid Value Names:
			ActivationState
			ActivationStateAcknowledged
			BasebandBootloaderVersion
			BasebandVersion
			BluetoothAddress
			BuildVersion
			DeviceCertificate
			DeviceClass
			DeviceName
			DevicePublicKey
			FirmwareVersion
			HostAttached
			IntegratedCircuitCardIdentity
			InternationalMobileEquipmentIdentity
			InternationalMobileSubscriberIdentity
			ModelNumber
			PhoneNumber
			ProductType
			ProductVersion
			ProtocolVersion
			RegionInfo
			SBLockdownEverRegisteredKey
			SIMStatus
			SerialNumber
			SomebodySetTimeZone
			TimeIntervalSince1970
			TimeZone
			TimeZoneOffsetFromUTC
			TrustedHostAttached
			UniqueDeviceID
			Uses24HourClock
			WiFiAddress
			iTunesHasConnected
 */

		unsafe public static string AMDeviceCopyValue(void* device, string name) {
			IntPtr result = AMDeviceCopyValue_IntPtr(device, 0, CFStringMakeConstantString(name));

			if (result != IntPtr.Zero) {
				byte length = Marshal.ReadByte(result, 8);
				if (length > 0) {
					return Marshal.PtrToStringAnsi(new IntPtr(result.ToInt64() + 9), length);
				}
			}
			return String.Empty;
		}

		[DllImport(DLLName, EntryPoint="AMDeviceCopyValue", CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static IntPtr AMDeviceCopyValue_IntPtr(void* device, uint unknown, IntPtr cfstring);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AFCFileInfoOpen(void* conn, IntPtr path, ref void* dict);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AFCKeyValueRead(void* dict, out void* key, out void* val);

		[DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AFCKeyValueClose(void* dict);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int AFCRemovePath(void* conn, IntPtr path);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static int AFCRenamePath(void* conn, IntPtr old_path, IntPtr new_path);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static int AFCFileRefOpen(void* conn, IntPtr path, int mode, int unknown, out Int64 handle);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefClose(void* conn, Int64 handle);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefRead(void* conn, Int64 handle, byte[] buffer, ref uint len);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefWrite(void* conn, Int64 handle, byte[] buffer, uint len);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFlushData(void* conn, Int64 handle);

		// FIXME - not working, arguments? Always returns 7
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefSeek(void* conn, Int64 handle, uint pos, uint origin);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefTell(void* conn, Int64 handle, ref uint position);

		// FIXME - not working, arguments?
		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
		unsafe public extern static int AFCFileRefSetFileSize(void* conn, Int64 handle, uint size);

		[DllImport(DLLName, CallingConvention=CallingConvention.Cdecl)]
        unsafe public extern static int AFCDirectoryCreate(void* conn, IntPtr path);




        //public static byte[] StringToCFString(string value) {
        //    byte[] b;

        //    b = new byte[value.Length + 10];
        //    b[4] = 0x8c;
        //    b[5] = 07;
        //    b[6] = 01;
        //    b[8] = (byte)value.Length;
        //    Encoding.ASCII.GetBytes(value, 0, value.Length, b, 9);
        //    return b;
        //}

		public static byte[] StringToCString(string value) {
			byte[] bytes = new byte[value.Length + 1];
			Encoding.ASCII.GetBytes(value, 0, value.Length, bytes, 0);
			return bytes;
		}

		public static string CFStringToString(byte[] value) {
			return Encoding.ASCII.GetString(value, 9, value[9]);
		}


        public static string CFStringRefToString(IntPtr stringRef)
        {
            return Marshal.PtrToStringAnsi(new IntPtr(stringRef.ToInt64() + 9L));
        }

        public static IntPtr StringToCFString(string value)
        {
            byte[] bytes = new byte[value.Length + 1];
            Encoding.ASCII.GetBytes(value, 0, value.Length, bytes, 0);
            return __CFStringMakeConstantString(bytes);
        }

        unsafe internal static Dictionary<string, string> ReadDictionary(void* dictPtr)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            try
            {
                void* pname, pvalue;
                while (MobileDevice.AFCKeyValueRead(dictPtr, out pname, out pvalue) == 0 && pname != null && pvalue != null)
                {
                    string name = Marshal.PtrToStringAnsi(new IntPtr(pname));
                    string value = Marshal.PtrToStringAnsi(new IntPtr(pvalue));
                    dictionary.Add(name, value);
                }
            }
            catch (AccessViolationException)
            {
            }
            finally
            {
                AFCKeyValueClose(dictPtr);
            }
            return dictionary;
        }
	}

}
