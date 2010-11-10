using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IThemeSky.Library.Extensions;
using IThemeSky.UI.Models;
using System.Net;
using IThemeSky.DataAccess;
using IThemeSky.Model;
using IThemeSky.Library.Util;
using System.Net.Mail;

namespace IThemeSky.UI.Controllers
{
    public class StoreController : ThemeControllerBase
    {
        public ActionResult QueryDownloadHistory(int themeId)
        {
            IOrderRepository repository2 = ThemeRepositoryFactory.Default.GetOrderRepository();
            List<ThemeDownloadHistory> list = repository2.GetPaidThemeDownloadHistory(themeId);
            ViewData["History"] = list;

            IThemeViewRepository repository = ThemeRepositoryFactory.Default.GetThemeViewRepository();
            FullThemeView theme = repository.GetTheme(themeId);
            return View(theme);
        }

        public ActionResult SubmitOrder(int themeId)
        {
            IThemeViewRepository repository = ThemeRepositoryFactory.Default.GetThemeViewRepository();
            FullThemeView theme = repository.GetTheme(themeId);
            return View(theme);
        }

        public ActionResult Result()
        {
            //定义您的身份标记
            string authToken = "3C3GQgRHwYNQOpxiaJPlkyi5YPtBtpL7RlAmCN2rgFib04hH8xV9LDKKyHm";
            string txToken = Request.QueryString["tx"];
            int themeId = Request.QueryString["themeid"].ToInt32();
            double price = Request.QueryString["price"].ToDouble();
            string mail = Request.QueryString["mail"];
            string userName = Request.QueryString["userName"];
            string checksum = Request.QueryString["checksum"];
            string correctChecksum = CryptoHelper.MD5_Encrypt(string.Format("{0}|{1}|{2}|{3}|{4}"
                , themeId
                , price
                , mail
                , userName
                , ORDER_CHECKSUM_KEY
                ), "utf-8");

            string query = "cmd=_notify-synch&tx=" + txToken + "&at=" + authToken;
            //判断校验码是否正确 checksum
            if (correctChecksum.Equals(checksum))
            {
                //订单入库
                IOrderRepository repository = ThemeRepositoryFactory.Default.GetOrderRepository();
                repository.AddOrder(new IThemeSky.Model.UserOrder()
                {
                    AddTime = DateTime.Now,
                    Checksum = checksum,
                    Description = "",
                    OrderNumber = txToken,
                    PayerMail = mail,
                    Price = price,
                    Status = 0,
                    ThemeId = themeId,
                    UpdateTime = DateTime.Now,
                    UserMail = mail,
                    UserName = userName
                });

                //提交paypal作二次验证
                WebClient client = new WebClient();
                string content = client.UploadString(PAYPAY_GATEWAY, "POST", query);
                logger.Debug("<p>#" + DateTime.Now + ":PDT[" + themeId + "][" + mail + "]" + content + "</p>");
                PayResultModel model = new PayResultModel();
                string[] arrParams = content.Split('\n');
                for (int i = 0; i < arrParams.Length; i++)
                {
                    if (i == 0)
                    {
                        if (arrParams[0].Equals("success", StringComparison.CurrentCultureIgnoreCase))
                        {
                            model.Success = true;
                        }
                        else
                        {
                            return View("Fail", new PayResultModel() { Description = "Waitting..." });
                        }
                    }
                    string[] arr = arrParams[i].Split('=');
                    if (arr.Length > 1)
                    {
                        model.Results.Add(arr[0].ToLower(), arr[1]);
                    }
                }
                //更新订单状态
                if (content.Length > 0)
                {
                    if (model.Success)
                    {
                        bool success = model.Success && Convert.ToDouble(model.payment_gross) == price;
                        repository.UpdateOrderStatus(model.txn_id, model.payer_email, success ? 1 : 0, content);
                    }
                    else
                    { 
                        return View("Fail", new PayResultModel() { Description  = content });
                    }
                }
                model.UserMail = mail;
                model.Theme = ThemeRepositoryFactory.Default.GetThemeViewRepository().GetTheme(themeId);
                ViewData["content"] = content;
                string mailContent = string.Format(@"
                    <table width=""640"" cellpadding=""0"" cellspacing=""0"" border=""0"">
	                    <tr>
		                    <td style="" padding:20px; font-family:Verdana, Geneva, sans-serif; font-size:12PX; line-height:1.5; color:#333;"">
			                    <h1 style=""font-size:14px;"">Dear {0},</h1>
			                    <p>
			                    Thanks for your purchase.
			                    </p>
			                    <p>Your purchased theme: <a href=""http://www.ithemesky.com{2}"">http://www.ithemesky.com{2}</a><br />
				                    Your Download Code for this theme: <strong style=""color:#F30;"">{1}</strong>
			                    </p><p>Download this theme with the Download Code, you would not be charged again. Please keep this message. If you miss it, send a Download Code request to us via your current Email address. We will reply you within 2 business days.</p>
			                    <p>This theme is permitted to install for personal and non-profit, non-commercial use. It is protected by copyright. It is not allowed to post this theme's copy or Download Code on other third websites. </p>
			                    <p>Sincerely,<br />
			                    <strong>iThemeSky.Com</strong>
			                    </p>
		                    </td>
	                    </tr>
                    </table>
                ", userName, model.txn_id, model.Theme.ThemeDetailUrl);
                SendMail(mail
                    , "Thanks for purchasing " + model.Theme.Title + " iPhone theme on iThemesky"
                    , mailContent);
                return View("Success", model);
            }
            else
            {
                return View("Fail", new PayResultModel() { Description  = "Invaild checksum" });
            }
        }

        private void SendMail(string mailTo, string subject, string body)
        {
            try
            {
                string mailServerName = "smtp.gmail.com";
                string mailFrom = "ithemesky@gmail.com";

                using (MailMessage message = new MailMessage(mailFrom, mailTo, subject, body))
                {
                    message.IsBodyHtml = true;
                    //SmtpClient是发送邮件的主体，这个构造函数是告知SmtpClient发送邮件时使用哪个SMTP服务器
                    SmtpClient mailClient = new SmtpClient(mailServerName);
                    //将认证实例赋予mailClient,也就是访问SMTP服务器的用户名和密码
                    mailClient.Credentials = new NetworkCredential("ithemesky@gmail.com", "itheme2010");
                    mailClient.EnableSsl = true;
                    mailClient.Port = 587;
                    //最终的发送方法
                    mailClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("<fieldset><legend>Time:{3}</legend><div>Url:{0}<br />UserIp:{1}<br />UrlReferer:{2}<hr /><pre>{4}</pre></div></fieldset>"
                , Request.Url
                , Request.UserHostAddress
                , Request.UrlReferrer ?? Request.UrlReferrer
                , DateTime.Now
                , ex
                );
                logger.ErrorException("Send mail error[" + mailTo + "][" + body + "]", ex);
            }
        }
    }
}
