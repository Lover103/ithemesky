using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace iSprite
{
    internal class SSHHelper
    {
        static SshShell shell;
        static object SSHLock = new object();
        static SSHHelper()
        {
            shell = new SshShell("127.0.0.1", "root");
            shell.Password = iSpriteContext.Current.iSpiritUserCfg.SSHPWD;
        }

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="errMsg">errMsg</param>
        /// <returns></returns>
        public static bool Shutdown(out string errMsg)
        {
            errMsg = string.Empty;
            if (!Connect2SSH())
            {
                return false;
            }
            //return true;
            bool flag = RunCmd("halt -q", out errMsg);
            if (!flag)
            {
                errMsg = " Fail to Shutdown (" + errMsg + ").";
            }
            return flag;
        }
        #endregion

        #region 重启
        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="errMsg">errMsg</param>
        /// <returns></returns>
        public static bool Reboot(out string errMsg)
        {
            errMsg = string.Empty;
            if (!Connect2SSH())
            {
                return false;
            }
            //return true;
            bool flag = RunCmd("reboot", out errMsg);
            if (!flag)
            {
                errMsg = " Fail to Reboot (" + errMsg + ").";
            }
            return flag;
        }
        #endregion

        #region 安装软件
        /// <summary>
        /// 安装软件
        /// </summary>
        /// <param name="iphone"></param>
        /// <param name="toinstalllist"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool InstallDebs(iPhoneFileDevice iphone, List<string> toinstalllist, out string msg)
        {
            msg = string.Empty;

            if (!Connect2SSH())
            {
                return false;
            }

            iphone.CheckDirectoryExists("/tmp/");
            if (toinstalllist.Count > 0)
            {
                string mainFilePath = toinstalllist[toinstalllist.Count - 1];
                string mainfileName = Path.GetFileName(mainFilePath);
                foreach (string localfile in toinstalllist)
                {
                    string fileName = Path.GetFileName(localfile);
                    iphone.Copy2iPhone(localfile, "/tmp/");
                    string errMsg = string.Empty;
                    bool flag = RunCmd("dpkg -i \"/tmp/" + fileName + "\"", out errMsg);
                    if (flag)
                    {
                        iphone.DeleteFile("/tmp/" + fileName);
                        if (mainFilePath == localfile)
                        {
                            msg = mainfileName + " has been successfully installed .";
                        }
                    }
                    else
                    {
                        if (mainFilePath != localfile)
                        {
                            msg = " Fail to install " + mainfileName + ", because can not install " + fileName + "(" + errMsg + ").";
                        }
                        else
                        {
                            msg = " Fail to install " + mainfileName + " (" + errMsg + ").";
                        }
                        return false;
                    }
                }
                iphone.RepairAppIcons();
                return true;
            }
            else
            {
                msg = " no file to be installed .";
                return false;
            }
        }
        #endregion

        #region 安装软件
        /// <summary>
        /// 安装软件
        /// </summary>
        /// <param name="iphone"></param>
        /// <param name="localfile"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool InstallDeb(iPhoneFileDevice iphone, string localfile, out string msg)
        {
            msg = string.Empty;

            if (!Connect2SSH())
            {
                return false;
            }

            string mainfileName = Path.GetFileName(localfile);
            iphone.CheckDirectoryExists("/tmp/");
            iphone.Copy2iPhone(localfile, "/tmp/");
            string errMsg = string.Empty;
            bool flag = RunCmd("dpkg -i \"/tmp/" + mainfileName + "\"", out errMsg);
            if (flag)
            {
                iphone.DeleteFile("/tmp/" + Path.GetFileName(localfile));
                msg = Path.GetFileName(localfile) + " has been successfully Installed .";
                iphone.RepairAppIcons();
                return true;
            }
            else
            {
                msg = " Fail to install " + mainfileName + " (" + errMsg + ").";
                return false;
            }
        }
        #endregion

        #region 安装IPA软件
        /// <summary>
        /// 安装IPA软件
        /// </summary>
        /// <param name="iphone"></param>
        /// <param name="localfile"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool InstallIPA(iPhoneFileDevice iphone, string localdir, out string msg)
        {
            msg = string.Empty;

            if (!Connect2SSH())
            {
                return false;
            }

            string destpath = "/private/var/mobile/iSpirit/Applications/";
            string mainAppName = Utility.GetDirName(localdir);
            if (iphone.DirectoryExists(destpath + mainAppName))
            {
                iphone.DeleteDirectory(destpath + mainAppName, true);
            }
            iphone.CheckDirectoryExists(destpath);
            iphone.Copy2iPhone(localdir, destpath);
            destpath = destpath + mainAppName;
            string errMsg = string.Empty;
            bool flag = RunCmd(string.Format("ln -s \"{0}\" /Applications/", destpath), out errMsg);
            if (flag)
            {
                RunCmd(string.Format("chmod -R 777 \"{0}\"", destpath), out errMsg);
                msg = mainAppName + " has been successfully Installed .";
                iphone.RepairAppIcons();
                return true;
            }
            else
            {
                msg = " Fail to install " + mainAppName + " (" + errMsg + ").";
                return false;
            }
        }
        #endregion

        #region 安装PXL软件
        /// <summary>
        /// 安装PXL软件
        /// </summary>
        /// <param name="iphone"></param>
        /// <param name="localfile"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool InstallPXL(iPhoneFileDevice iphone, string localdir, out string msg)
        {
            msg = string.Empty;

            if (!Connect2SSH())
            {
                return false;
            }


            if (!File.Exists(localdir + "PxlPkg.plist"))
            {
                msg = " Current pxl file is invalid.";
                return false;
            }

            string destpath = "/private/var/mobile/iSpirit/Applications/";
            string mainAppPath = string.Empty;

            string PxlPkg = File.ReadAllText(localdir + "PxlPkg.plist", Encoding.UTF8);

            //拷贝文件
            Match match = new Regex("<dict>[\\s]*?<key>destination</key>[\\s]*?<string>(?<destination>[\\S ]*?)</string>[\\s]*?<key>overwrite</key>[\\s]*?<(?<overwrite>[\\S]*?) />[\\s]*?<key>permanent</key>[\\s]*?<(?<permanent>[\\S]*?) />[\\s]*?<key>source</key>[\\s]*?<string>(?<source>[\\S ]*?)</string>[\\s]*?</dict>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(PxlPkg);

            while (match.Success)
            {
                string destination = match.Result("${destination}");
                string source = match.Result("${source}");
                if (destination.StartsWith("/Applications/"))
                {
                    destination = destination.Replace("/Applications/", destpath);
                    mainAppPath = destination;
                }

                iphone.Copy2iPhone(localdir + source, destination);

                match = match.NextMatch();
            }

            if (mainAppPath == "")
            {
                msg = " Current pxl file is invalid.";
                return false;
            }

            //设置权限
            match = new Regex("<array>[\\s]+<string>(?<p0>[\\S]*?)</string>[\\s]+<string>(?<p1>[\\S]*?)</string>[\\s]+<string>(?<p2>[\\S]*?)</string>[\\s]+<string>(?<p3>[\\S ]*?)</string>[\\s]+</array>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(PxlPkg);

            string errMsg = string.Empty;
            while (match.Success)
            {
                string p3 = match.Result("${p3}");
                if (p3.StartsWith("/Applications/"))
                {
                    p3 = p3.Replace("/Applications/", destpath);
                }
                RunCmd(string.Format("{0} {1} {2} \"{3}\"", match.Result("${p0}"), match.Result("${p1}"), match.Result("${p2}"), p3), out errMsg);

                match = match.NextMatch();
            }

            string mainAppName = Utility.GetDirName(mainAppPath);

            bool flag = RunCmd(string.Format("ln -s \"{0}\" /Applications/", mainAppPath), out errMsg);
            if (flag)
            {
                msg = mainAppName + " has been successfully Installed .";
                iphone.RepairAppIcons();
                return true;
            }
            else
            {
                msg = " Fail to install " + mainAppName + " (" + errMsg + ").";
                return false;
            }
        }
        #endregion

        #region 卸载deb
        /// <summary>
        /// 卸载deb
        /// </summary>
        /// <param name="iphone"></param>
        /// <param name="debName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool UnInstallDeb(iPhoneFileDevice iphone, string debName, out string msg)
        {
            msg = string.Empty;
            if (!Connect2SSH())
            {
                return false;
            }
            string errMsg = string.Empty;
            bool flag = RunCmd("dpkg -P \"" + debName + "\"", out errMsg);
            if (flag)
            {
                msg = debName + " has been successfully uninstalled .";
                return true;
            }
            else
            {
                msg = " Fail to uninstall " + debName + " (" + errMsg + ").";
                return false;
            }
        }
        #endregion

        #region 运行指定的命令
        /// <summary>
        /// 运行指定的命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        static bool RunCmd(string cmd, out string errMsg)
        {
            try
            {
                shell.WriteLine(cmd);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                string s = shell.Expect();
                Utility.WriteLog(string.Format("cmd:{0}\r\nresult{1}", cmd, s));
                if (s.Contains("error") || s.Contains("Error"))
                {
                    errMsg = s;
                    return false;
                }
                else
                {
                    errMsg = string.Empty;
                    return true;
                }
            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 建立连接
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        static bool Connect2SSH()
        {
            SshStatusOption status = SshStatusOption.UnknownError;
            string errMsg = string.Empty;
            if (shell.Connected && shell.ShellOpened)
            {
                status = SshStatusOption.Connected;
            }
            else
            {
                lock (SSHLock)
                {
                    try
                    {
                        if (!RuniTunnel())
                        {
                            status = SshStatusOption.iTunnelNoRun;
                        }
                        if (shell.Connected && shell.ShellOpened)
                        {
                            status = SshStatusOption.Connected;
                        }
                        else
                        {
                            shell.Connect();
                            string r = shell.Expect();
                            if (shell.Connected && shell.ShellOpened)
                            {
                                status = SshStatusOption.Connected;
                            }
                            else
                            {
                                status = SshStatusOption.NotConnected;
                            }
                        }
                    }
                    catch (JSchException ex)
                    {
                        try
                        {
                            shell.Close();
                            Process[] ps = Process.GetProcessesByName("iTunnel");
                            for (int index = 0; index < ps.Length; index++)
                            {
                                ps[index].Kill();
                            }
                        }
                        catch
                        {
                        }
                        if (ex.Message == "Auth fail")
                        {
                            //密码错误
                            status = SshStatusOption.ErrorPassword;
                        }
                        else if (ex.Message.Contains("System.Net.Sockets.SocketException"))
                        {
                            //没有安装openssh
                            status = SshStatusOption.OpenSSHNoInstalled;
                        }
                        else
                        {
                            //其他错误
                            errMsg = ex.Message;
                            status = SshStatusOption.UnknownError;

                            Utility.WriteLog("pwd:" + shell.Password);
                            Utility.WriteLog("Message:" + ex.toString());
                        }
                    }
                }
            }
            if (status == SshStatusOption.Connected)
            {
                if (shell.Password != iSpriteContext.Current.iSpiritUserCfg.SSHPWD)
                {
                    iSpriteContext.Current.iSpiritUserCfg.SSHPWD = shell.Password;
                    iSpriteContext.Current.SaveUserCfg();
                }
                return true;
            }
            else
            {
                switch (status)
                {
                    case SshStatusOption.ErrorPassword:
                        string newpwd = string.Empty;
                        DialogResult result = InputBox.Show("Input root password to connect shh service", ref newpwd, true);
                        if (result == DialogResult.OK)
                        {
                            shell.Password = newpwd;
                            return Connect2SSH();
                        }
                        else
                        {
                            return false;
                        }
                    case SshStatusOption.iTunnelNoRun:
                        MessageHelper.ShowInfo("SSHDevicer can not run!");
                        break;
                    case SshStatusOption.NotConnected:
                    case SshStatusOption.OpenSSHNoInstalled:
                        MessageHelper.ShowInfo("Can not connect to ssh service,please make sure you have installed OpenSSH correctly.");
                        break;
                    case SshStatusOption.UnknownError:
                        MessageHelper.ShowError("Can not connect to ssh service(" + errMsg + ") .");
                        break;
                }
                return false;
            }           
        }
        #endregion

        #region 虚拟端口
        /// <summary>
        /// 虚拟端口
        /// </summary>
        /// <returns></returns>
        static bool RuniTunnel()
        {
            try
            {
                int port = 22;
                Process[] ps = Process.GetProcessesByName("iTunnel");
                if (ps.Length > 0)
                {
                    return true;
                }

                Process processiTunnel = new Process();
                processiTunnel.StartInfo.FileName = iSpriteContext.Current.iSpriteApplicationDataPath + @"\iTunnel.exe";
                processiTunnel.StartInfo.Arguments = "22 " + port;
                processiTunnel.StartInfo.ErrorDialog = false;
                processiTunnel.StartInfo.CreateNoWindow = true;
                processiTunnel.StartInfo.UseShellExecute = false;
                processiTunnel.Start();
                Thread.Sleep(TimeSpan.FromSeconds(1.5));
                int tryNum = 0;

                bool flag = false;
                while (tryNum++<6)
                {
                    flag = Process.GetProcessesByName("iTunnel").Length > 0;
                    if (flag)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }

                return flag;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
