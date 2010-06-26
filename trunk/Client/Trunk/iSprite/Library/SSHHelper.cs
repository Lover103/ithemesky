using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;
using System.Collections.Generic;

namespace iSprite
{
    internal class SSHHelper
    {
        static SshShell shell;
        static SSHHelper()
        {
            shell = new SshShell("127.0.0.1", "root");
            shell.Password = "alpine";
        }

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
            return RunCmd("reboot", out errMsg);
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
                            msg = " Fail to install " + mainfileName + ", because can not install " + fileName + ".";
                        }
                        else
                        {
                            msg = " Fail to install " + mainfileName + ".";
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

        public static bool InstallDeb(iPhoneFileDevice iphone, string localfile, out string msg)
        {
            msg = string.Empty;

            if (!Connect2SSH())
            {
                return false;
            }

            iphone.CheckDirectoryExists("/tmp/");
            iphone.Copy2iPhone(localfile, "/tmp/");
            bool flag = RunCmd("dpkg -i \"/tmp/" + Path.GetFileName(localfile) + "\"", out msg);
            if (flag)
            {
                iphone.DeleteFile("/tmp/" + Path.GetFileName(localfile));
                msg = Path.GetFileName(localfile) + " has been successfully Installed .";
                iphone.RepairAppIcons();
                return true;
            }
            else
            {
                //todo;
                return false;
            }
        }

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

            bool flag = RunCmd("dpkg -P \"" + debName + "\"", out msg);
            if (flag)
            {
                msg = debName + " has been successfully uninstalled .";
                return true;
            }
            else
            {
                //todo;
                return false;
            }
        }

        /// <summary>
        /// 运行指定的命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        static bool RunCmd(string cmd, out string errMsg)
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
                if (!RuniTunnel())
                {
                    status = SshStatusOption.iTunnelNoRun;
                }
                try
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
                catch (JSchException ex)
                {
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
                    }
                }
            }
            if (status == SshStatusOption.Connected)
            {
                return true;
            }
            else
            {
                switch (status)
                {
                    case SshStatusOption.ErrorPassword:
                        break;
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

                return Process.GetProcessesByName("iTunnel").Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
