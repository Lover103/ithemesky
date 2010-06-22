using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Tamir.SharpSsh;
using Tamir.SharpSsh.jsch;

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
        static SshStatusOption CheckSshStatus(out string errMsg)
        {
            errMsg=string.Empty;
            if (shell.Connected && shell.ShellOpened)
            {
                return SshStatusOption.Connected;
            }
            else
            {
                RuniTunnel();

                try
                {
                    shell.Connect();
                    if (shell.Connected && shell.ShellOpened)
                    {
                        return SshStatusOption.Connected;
                    }
                    else
                    {
                        return SshStatusOption.NotConnected;
                    }
                }
                catch (JSchException ex)
                {
                    if (ex.Message == "Auth fail")
                    {
                        //密码错误
                        return SshStatusOption.ErrorPassword;
                    }
                    else if (ex.Message.Contains("System.Net.Sockets.SocketException"))
                    {
                        //没有安装openssh
                        return SshStatusOption.OpenSSHNoInstalled;
                    }
                    else
                    {
                        //其他错误
                        errMsg = ex.Message;
                        return SshStatusOption.UnknownError;
                    }
                }
            }
            return SshStatusOption.UnknownError;
        }

        public static bool InstallDeb(iPhoneFileDevice iphone, string localfile, out string msg)
        {
            msg = string.Empty;
            iphone.CheckDirectoryExists("/tmp/");
            iphone.Copy2iPhone(localfile, "/tmp/");
            bool flag = RunCmd("dpkg -i \"/tmp/" + Path.GetFileName(localfile) + "\"");
            if (flag)
            {
                iphone.Respring();
                msg = Path.GetFileName(localfile) + " has been successfully Installed .";
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
            bool flag = RunCmd("dpkg -r \"" + debName + "\"");
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
        public static bool RunCmd(string cmd)
        {
            string errMsg = string.Empty;
            SshStatusOption status = CheckSshStatus(out errMsg);
            if (status == SshStatusOption.Connected)
            {
                shell.WriteLine(cmd);
                string s = shell.Expect();
                return true;
            }
            else
            {
                switch (status)
                {
                    case SshStatusOption.ErrorPassword:
                        break;
                    case SshStatusOption.iTunnelNoRun:
                        MessageHelper.ShowInfo("SSHDevice can not run!");
                        break;
                    case SshStatusOption.NotConnected:
                        MessageHelper.ShowInfo("Can not connect to ssh service, you can reinstall OpenSSH.");
                        break;
                    case SshStatusOption.OpenSSHNoInstalled:
                        MessageHelper.ShowInfo("You must Install OpenSSH first.");
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
                Process[] ps = Process.GetProcessesByName("SSHDevice");
                if (ps.Length > 0)
                {
                    return true;
                }

                Process processiTunnel = new Process();
                processiTunnel.StartInfo.FileName = iSpriteContext.Current.iSpriteApplicationDataPath + @"\SSHDevice.exe";
                processiTunnel.StartInfo.Arguments = "22 " + port;
                processiTunnel.StartInfo.ErrorDialog = false;
                processiTunnel.StartInfo.CreateNoWindow = true;
                processiTunnel.StartInfo.UseShellExecute = false;
                processiTunnel.Start();
                Thread.Sleep(500);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
