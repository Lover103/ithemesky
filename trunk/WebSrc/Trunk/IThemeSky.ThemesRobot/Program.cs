using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace IThemeSky.ThemesRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            //MBThemeRobot robot = new MBThemeRobot();
            //for (int i = 29; i <= 36; i++)       //34
            //{
            //    robot.AnalyseList(i);
            //}
            //robot.AnalyseFromErrorUrl();
            //ThemeFileChecker.Check();
            //MRobot r = new MRobot();
            //r.Start();
            //RepairTheme rt = new RepairTheme();
            //rt.RepairCode();
           // ThemeFileChecker.Check();
            //MRobot2 robot = new MRobot2();
            //robot.Start();
            SendMail("hello marquee, i'm from .net", "hello marquee, i'm from .net");
            Console.WriteLine("bingo");
            Console.Read();
        }

        private static void SendMail(string subject, string body)
        {
            //string mailServerName = "smtp.gmail.com";  //发送邮件的SMTP服务器
            string mailFrom = "ithemesky@gmail.com";   //发件人邮箱（用126的邮件服务器，就必须用126邮箱的用户名）
            string mailTo = "zeromarquee@gmail.com";   //收件人邮箱

            using (MailMessage message = new MailMessage(mailFrom, mailTo, subject, body))
            {
                //SmtpClient是发送邮件的主体，这个构造函数是告知SmtpClient发送邮件时使用哪个SMTP服务器
                SmtpClient mailClient = new SmtpClient();
                //将认证实例赋予mailClient,也就是访问SMTP服务器的用户名和密码
                //mailClient.Credentials = new NetworkCredential("ithemesky@gmail.com", "itheme2010");
                //最终的发送方法
                mailClient.EnableSsl = true;
                mailClient.Send(message);
            }
        }
    }
}











