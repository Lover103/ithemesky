using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace iSprite
{
    internal class ZipHelper
    {
        public static int UnZip(string zipfilename, string directory)
        {
            int filecount = 0;
            if (!Directory.Exists(directory))
            {
                //生成解压目录
                Directory.CreateDirectory(directory);
            }
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipfilename)))
            {
                try
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string fileName = Path.GetFileName(theEntry.Name);                       

                        if (fileName != String.Empty)
                        {
                            filecount++;
                            //解压文件到指定的目录
                            using (FileStream streamWriter = File.Create(Path.Combine(directory, fileName)))
                            {
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    int size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                    s.Close();
                }
                catch
                {
                    filecount = 0;
                }
                finally
                {
                    s.Close();
                }
            }
            return filecount;
        }
    }
}
