using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using IThemeSky.Library.Util;

namespace IThemeSky.UI.Controllers
{
    public class ThemeControllerBase : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (IpDefender.IsRobot())
            {
                logger.Info(string.Format("<p>UserIp:{0}, Agent:{1}, Url:{2} </p>"
                    , filterContext.RequestContext.HttpContext.Request.UserHostAddress
                    , filterContext.RequestContext.HttpContext.Request.UserAgent
                    , filterContext.RequestContext.HttpContext.Request.Url));
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
