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
using System.Xml;
using System.Collections;
using CE.iPhone.PList;

namespace iSprite
{
    internal class SSHHelper
    {
        static SshShell shell;
        static object SSHLock = new object();
        public static string Prefix = string.Empty;
        static string PhoneName = string.Empty;
        static SSHHelper()
        {
            shell = new SshShell("127.0.0.1", "root");
            shell.Password = iSpriteContext.Current.iSpiritUserCfg.SSHPWD;
        }

        public static SshShell Server
        {
            get
            {
                return shell;
            }
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

            string destpath = "/private/var/mobile/Applications/" + Guid.NewGuid().ToString() + "/";
            string mainAppName = Utility.GetDirName(localdir);


            Hashtable ht = new Hashtable();
            XmlNodeList keys;

            #region 从Info.plist获取当前软件信息
            string infopath = localdir + "/Info.plist";
            if (!File.Exists(infopath))
            {
                msg = " The installation package is not legitimate.";
                return false;
            }

            PListRoot root = PListRoot.Load(infopath);
            string templpath = iSpriteContext.Current.iSpriteTempPath + Path.GetFileName(infopath);
            root.Save(templpath, PListFormat.Xml);
            XmlDocument InfoXmlDoc = new XmlDocument();
            InfoXmlDoc.Load(new XmlTextReader(templpath));
            keys = InfoXmlDoc.SelectNodes("/plist/dict/key");
            foreach (XmlNode node in keys)
            {
                string key = node.InnerText;
                if (!ht.Contains(key))
                {
                    ht.Add(key,node.NextSibling.OuterXml);
                }
            }
            #endregion

            XmlDocument XmlDoc = iphone.GetPlist2XML(iSpriteContext.Current.iPhone_InstallationPath);
            keys = XmlDoc.SelectNodes("/plist/dict/key");
            XmlNode currentnode = null;

            if (keys.Count <= 0)
            {
                msg = "The  com.apple.mobile.installation.plist is Damaged.";
                return false;
            }

            foreach (XmlNode node in keys)
            {
                if (node.InnerText == "User")
                {
                    currentnode = node.NextSibling;
                }
            }

            XmlElement xe1, xe2;
            if (currentnode == null)
            {
                xe1 = XmlDoc.CreateElement("key");
                xe1.InnerText = "User";
                xe2 = XmlDoc.CreateElement("dict");
                xe2.InnerText = "";
                keys[0].ParentNode.AppendChild(xe1);
                keys[0].ParentNode.AppendChild(xe2);
                currentnode = xe2;
            }

            string CFBundleIdentifier = GetValue(ht, "CFBundleIdentifier", "");
            if (CFBundleIdentifier == string.Empty)
            {
                msg = " The installation package is not legitimate.";
                return false;
            }

            #region 构建当前软件信息
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<key>ApplicationType</key>\n{0}\n", "<string>User</string>");
            sb.AppendFormat("<key>CFBundleDevelopmentRegion</key>\n{0}\n", GetValue(ht, "CFBundleDevelopmentRegion", "<string>en</string>"));
            sb.AppendFormat("<key>CFBundleDisplayName</key>\n{0}\n", GetValue(ht, "CFBundleDisplayName", "<string>???</string>"));
            sb.AppendFormat("<key>CFBundleExecutable</key>\n{0}\n", GetValue(ht, "CFBundleExecutable", "<string>???</string>"));
            sb.AppendFormat("<key>CFBundleIconFile</key>\n{0}\n", GetValue(ht, "CFBundleIconFile", "<string>Icon.png</string>"));
            sb.AppendFormat("<key>CFBundleIdentifier</key>\n{0}\n", CFBundleIdentifier);
            sb.AppendFormat("<key>CFBundleInfoDictionaryVersion</key>\n{0}\n", GetValue(ht, "CFBundleInfoDictionaryVersion", "<string>6.0</string>"));
            sb.AppendFormat("<key>CFBundleName</key>\n{0}\n", GetValue(ht, "CFBundleName", "<string>???</string>"));
            sb.AppendFormat("<key>CFBundlePackageType</key>\n{0}\n", GetValue(ht, "CFBundlePackageType", "<string>APPL</string>"));
            sb.AppendFormat("<key>CFBundleResourceSpecification</key>\n{0}\n", GetValue(ht, "CFBundleResourceSpecification", "<string>ResourceRules.plist</string>"));
            sb.AppendFormat("<key>CFBundleSignature</key>\n{0}\n", GetValue(ht, "CFBundleSignature", "<string>???</string>"));
            sb.AppendFormat("<key>CFBundleSupportedPlatforms</key>\n{0}\n", GetValue(ht, "CFBundleSupportedPlatforms", @"<string>iPhoneOS</string>"));
            sb.AppendFormat("<key>CFBundleVersion</key>\n{0}\n", GetValue(ht, "CFBundleVersion", "<string>1.1</string>"));
            sb.AppendFormat("<key>DTPlatformName</key>\n{0}\n", GetValue(ht, "DTPlatformName", "<string>iphoneos</string>"));
            sb.AppendFormat("<key>DTSDKName</key>\n{0}\n", GetValue(ht, "DTSDKName", "<string>iphoneos3.0</string>"));
            sb.AppendFormat("<key>LSRequiresIPhoneOS</key>\n{0}\n", GetValue(ht, "LSRequiresIPhoneOS", "<true />"));
            sb.AppendFormat("<key>MinimumOSVersion</key>\n{0}\n", GetValue(ht, "MinimumOSVersion", "<string>3.0</string>"));
            sb.AppendFormat("<key>Path</key>\n<string>{0}</string>\n", destpath + mainAppName);
            sb.AppendFormat("<key>UIInterfaceOrientation</key>\n{0}\n", GetValue(ht, "UIInterfaceOrientation", "<string>UIInterfaceOrientationLandscapeRight</string>"));
            sb.AppendFormat("<key>UIPrerenderedIcon</key>\n{0}\n", GetValue(ht, "UIPrerenderedIcon", "<true />"));
            sb.AppendFormat("<key>UIRequiresPersistentWiFi</key>\n{0}\n", GetValue(ht, "UIRequiresPersistentWiFi", "<false />"));
            sb.AppendFormat("<key>UIStatusBarHidden</key>\n{0}\n", GetValue(ht, "UIStatusBarHidden", "<false />"));
            sb.AppendFormat("<key>UIStatusBarStyle</key>\n{0}\n", GetValue(ht, "UIStatusBarStyle", "<string>UIStatusBarStyleDefault</string>"));

            foreach (DictionaryEntry item in ht)
            {
                sb.AppendFormat("<key>{0}</key>\n{1}\n", item.Key, item.Value);
            }

            #endregion

            xe1 = XmlDoc.CreateElement("key");
            xe1.InnerText = CFBundleIdentifier.Replace("<string>", "").Replace("</string>", "");
            xe2 = XmlDoc.CreateElement("dict");
            xe2.InnerXml = sb.ToString();
            currentnode.AppendChild(xe1);
            currentnode.AppendChild(xe2);

            iphone.CheckDirectoryExists(destpath);
            iphone.CheckDirectoryExists(destpath + "/Library/");
            iphone.CheckDirectoryExists(destpath + "/Documents/");
            iphone.CheckDirectoryExists(destpath + "/tmp/");

            iphone.Copy2iPhone(localdir, destpath);

            string cmd = "chmod -R 777 \"{0}\"";
            cmd += ";chown -R mobile \"{0}\"";
            cmd = string.Format(cmd, destpath);

            string errMsg = string.Empty;
            bool flag = SSHHelper.RunCmd(cmd, out errMsg);
            if (!flag)
            {
                msg = ("Fail to install (Failing to set permissions(" + errMsg + ")) !");
                return false;
            }

            if (iphone.SetFileText(XmlDoc.OuterXml, iSpriteContext.Current.iPhone_InstallationPath))
            {
                msg = mainAppName + " has been successfully Installed .";
                iphone.RepairAppIcons();
                return true;
            }
            else
            {
                msg = "Fail to update com.apple.mobile.installation.plist.";
                return false;
            }
        }
        #endregion

        static string GetValue(Hashtable ht, string key, string defaultValue)
        {
            if (ht.Contains(key))
            {
                string v= ht[key].ToString();
                ht.Remove(key);
                return v;
            }
            else
            {
                return defaultValue;
            }
        }

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

                try
                {
                    iphone.Copy2iPhone(localdir + source, destination);
                }
                catch
                { 
                }

                match = match.NextMatch();
            }

            if (mainAppPath == "")
            {
                msg = " Current pxl file is invalid.";
                return false;
            }

            match = new Regex("<key>RDPxlPackageName</key>[\\s]+<string>(?<RDPxlPackageName>[\\S ]*?)</string>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(PxlPkg);

            string mainAppName = string.Empty;
            if (match.Success)
            {
                mainAppName = match.Result("${RDPxlPackageName}");
            }
            else
            {
                mainAppName = Utility.GetDirName(mainAppPath); 
            }

            int index = PxlPkg.IndexOf("<key>RDPxlPackagePostflight</key>");
            string errMsg = string.Empty;
            string cmd = string.Empty;
            if (index > -1)
            {
                //获取要执行的脚本
                PxlPkg = PxlPkg.Substring(index);

                match = new Regex("<array>[\\s]+<string>(?<p>[\\S\\s]*?)</string>[\\s]+</array>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(PxlPkg);
                while (match.Success)
                {
                    string p = match.Result("${p}").Replace("</string>", " ").Replace("<string>", "").Replace("\r", "").Replace("\n", "");
                    cmd += p + ";";
                    match = match.NextMatch();
                }
            }
            bool flag = false;
            if (string.IsNullOrEmpty(cmd))
            {
                cmd = string.Format("ln -s \"{0}\" /Applications/", mainAppPath);
            }
            else
            {
                cmd += string.Format("ln -s \"{0}\" /Applications/", mainAppPath);
            }
            flag = RunCmd(cmd, out errMsg);
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

            //设置权限

            /*
            match = new Regex("<array>[\\s]+<string>(?<p0>[\\S]*?)</string>[\\s]+<string>(?<p1>[\\S]*?)</string>[\\s]+<string>(?<p2>[\\S]*?)</string>[\\s]+<string>(?<p3>[\\S ]*?)</string>[\\s]+</array>",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled).Match(PxlPkg);

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

            */
            
        }
        #endregion

        #region 获取权限属性
        /// <summary>
        /// 获取权限属性
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userlist"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static string GetPropertys(string path,List<string> userlist,out string owner)
        {
            owner = string.Empty;
            if (!Connect2SSH())
            {
                return string.Empty;
            }
            else
            {
                string cmd = "ls \"" + path + "\" -ld";
                string msg = RunCmd(cmd);
                int index = msg.IndexOf("-ld");
                if (index != -1)
                {
                    msg = msg.Substring(index+3).Trim();

                    foreach (string user in userlist)
                    {
                        if (msg.Contains(" " + user + " "))
                        {
                            owner = user;
                            break;
                        }
                    }
                    return msg.Substring(1, 9);
                }
                else
                {
                    return string.Empty;
                }
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
        public static bool RunCmd(string cmd, out string errMsg)
        {
            try
            {
                shell.WriteLine(cmd);
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                string s = GetResponse();
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

        #region 运行指定的命令
        /// <summary>
        /// 运行指定的命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string RunCmd(string cmd)
        {
            try
            {
                if (!Connect2SSH())
                {
                    return string.Empty;
                }

                shell.WriteLine(cmd);
                string s = GetResponse();
                return s;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        static string GetResponse()
        {
            string s = string.Empty;
            StringBuilder sb = new StringBuilder();
            while ((s = shell.Expect()) != null && s != string.Empty)
            {
                if (s.StartsWith(PhoneName + ":"))
                {
                    sb.Append(s);
                    Prefix = s;
                    break;
                }
                else
                {
                    sb.Append(s);
                }
            }
            return sb.ToString();
        }

        #region 建立连接
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public static bool Connect2SSH()
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
                            Prefix = shell.Expect();
                            if(Prefix.Contains(":"))
                            {
                                PhoneName = Prefix.Split(':')[0];
                            }
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
