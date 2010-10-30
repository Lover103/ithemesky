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

namespace IThemeSky.UI.Controllers
{
    public class StoreController : ThemeControllerBase
    {
        //
        // GET: /Help/
        public ActionResult SubmitOrder(int themeId)
        {
            IThemeViewRepository repository = ThemeRepositoryFactory.Default.GetThemeViewRepository();
            FullThemeView theme = repository.GetTheme(themeId);
            return View(theme);
        }

        public ActionResult Result()
        {
            //定义您的身份标记
            string authToken = "WkNr55q-JIZRjIOuvSMlEargEe72x_slxR2a7pSpn1dAn1jKg7OXdaTugfi";
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
                logger.Info("<p>#" + DateTime.Now + ":PDT[" + themeId + "][" + mail + "]" + content + "</p>");
                PayResultModel model = new PayResultModel();
                string[] arrParams = content.Split('\n');
                for (int i = 0; i < arrParams.Length; i++)
                {
                    if (i == 0 && arrParams[i].Equals("success", StringComparison.CurrentCultureIgnoreCase))
                    {
                        model.Success = true;
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
                return View("Success", model);
            }
            else
            {
                return View("Fail", new PayResultModel() { Description  = "Invaild checksum" });
            }
        }
    }
}
