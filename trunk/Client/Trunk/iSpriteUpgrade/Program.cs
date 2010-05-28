using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace iSpriteUpgrade
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始升级");
            if (args.Length > 0)
            {
                //先杀原有进程
                try
                {
                    string appname = args[0].Split(';')[0];
                    Console.WriteLine(appname);
                    //string appname = "iSprite";
                    string currentpath = args[0].Split(';')[1];
                    System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcesses();
                    foreach (Process p in ps)
                    {
                        Console.WriteLine(p.ProcessName);
                        if (p.ProcessName.Equals(appname, StringComparison.CurrentCultureIgnoreCase))
                        {
                            p.Kill();
                            break;
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.WriteLine(currentpath);
                    if (currentpath != string.Empty)
                    {
                        //替换文件
                        string iSpriteApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles).TrimEnd('\\')
                   + @"\iSpirit\";
                        foreach (string file in Directory.GetFiles(iSpriteApplicationDataPath + "\\UpdateFiles\\"))
                        {
                            if (Path.GetExtension(file) != ".xml")
                            {
                                Console.WriteLine(file);
                                File.Copy(file, currentpath + "\\" + Path.GetFileName(file), true);
                            }
                        }

                        try
                        {
                            Directory.Delete(iSpriteApplicationDataPath + "\\UpdateFiles\\", true);
                        }
                        catch (Exception ex1)
                        {
                            Console.WriteLine(ex1.Message);
                        }

                        //启动程序
                        Process.Start(currentpath + @"\iSpirit.exe");

                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("完成升级");
            Thread.Sleep(2000);
            //Console.ReadLine();
        }
    }

}
