using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using IThemeSky.Library.Util;

namespace IThemeSky.UI.Controllers
{
    [HandleError(ExceptionType=typeof(ApplicationException))]
    public class ThemeControllerBase : Controller
    {
        protected static readonly string ORDER_CHECKSUM_KEY = "ITHEMESKY_ORDER_CHECKSUM";
        protected static readonly string PAYPAY_GATEWAY = "https://www.paypal.com/cgi-bin/webscr";
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (IpDefender.IsRobot())
            {
                logger.Info(string.Format("<p>Time:{3}, UserIp:{0}, Agent:{1}, Url:{2} </p>"
                    , filterContext.RequestContext.HttpContext.Request.UserHostAddress
                    , filterContext.RequestContext.HttpContext.Request.UserAgent
                    , filterContext.RequestContext.HttpContext.Request.Url
                    , DateTime.Now
                    ));
                filterContext.Result = new RedirectResult("/Defender.html");
            }
            base.OnActionExecuted(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            string message = string.Format("<fieldset><legend>Time:{3}</legend><div>Url:{0}<br />UserIp:{1}<br />UrlReferer:{2}<hr /><pre>{4}</pre></div></fieldset>"
                , filterContext.RequestContext.HttpContext.Request.Url
                , filterContext.RequestContext.HttpContext.Request.UserHostAddress
                , filterContext.RequestContext.HttpContext.Request.UrlReferrer ?? filterContext.RequestContext.HttpContext.Request.UrlReferrer
                , DateTime.Now
                , filterContext.Exception
                );
            logger.Error(message);
            base.OnException(filterContext);
        }
    }
}
