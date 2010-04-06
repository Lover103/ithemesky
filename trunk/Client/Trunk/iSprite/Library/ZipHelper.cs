using System;
using System.Collections.Generic;

using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace iSprite
{
    internal class ZipHelper
    {
        public static int UnZip(string fileToUpZip, string destZipedFolder)
        {
            int count = 0;
            if (!File.Exists(fileToUpZip))
            {
                return count;
            }

            if (!Directory.Exists(destZipedFolder))
            {
                Directory.CreateDirectory(destZipedFolder);
            }

            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            try
            {
                s = new ZipInputStream(File.OpenRead(fileToUpZip));
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(destZipedFolder, theEntry.Name);
                        //判断文件路径是否是文件夹
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }
                        using (FileStream streamWriter = File.Create(fileName))
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        count++;
                    }
                }
            }
            finally
            {
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
            }

            return count;
        }        
    }
}
