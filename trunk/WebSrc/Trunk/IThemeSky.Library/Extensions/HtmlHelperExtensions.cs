using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace IThemeSky.Library.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static void RenderPagination(this HtmlHelper helper, string urlPattern, int pageIndex, int pageSize, int recordCount, int displayPageLinkNumber)
        {
            RenderPagination(helper, urlPattern, pageIndex, pageSize, recordCount, displayPageLinkNumber, true, true);
        }

        public static void RenderPagination(this HtmlHelper helper, string urlPattern, int pageIndex, int pageSize, int recordCount, int displayPageLinkNumber, bool displayGoLink, bool displayPageSTAT)
        {
            HttpResponse response = HttpContext.Current.Response;
            if (recordCount > pageSize)
            {
                //初始化分页参数
                int pageCount = recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1;
                int beginIndex = 1;
                if (pageIndex % displayPageLinkNumber == 0)
                {
                    beginIndex = ((pageIndex / displayPageLinkNumber) - 1) * displayPageLinkNumber + 1;
                }
                else
                {
                    beginIndex = (pageIndex / displayPageLinkNumber) * displayPageLinkNumber + 1;
                }
                int endIndex = beginIndex + displayPageLinkNumber;
                if (endIndex > pageCount)
                {
                    endIndex = pageCount;
                }
                response.Write("<ul class=\"pageNumber\">");
                //如果页码大于1才输出向前的按钮
                if (pageIndex > 1)
                {
                    response.Write(string.Format("<li class=\"previous\"><a href=\"{0}\">Previous</a></li>", string.Format(urlPattern, pageIndex - 1)));
                }
                //输出每页的页码
                for (int i = beginIndex; i <= endIndex; i++)
                {
                    response.Write(string.Format("<li{2}><a href=\"{0}\">{1}</a></li>", string.Format(urlPattern, i), i, i == pageIndex ? " class=\"current\"" : ""));
                }
                //判断是否输出向后翻页链接
                if (pageIndex < pageCount)
                {
                    response.Write(string.Format("<li class=\"next\"><a href=\"{0}\">Next</a></li>", string.Format(urlPattern, pageIndex + 1)));
                }
                response.Write("</ul>");

                if (displayPageSTAT)
                {
                    response.Write("<ul class=\"pageStat\">");
                    response.Write(string.Format("<li>{0}/{1} page(s)</li>", pageIndex, pageCount));
                    if (displayGoLink)
                    {
                        string textBoxId = Guid.NewGuid().ToString();
                        response.Write("<li>Go to</li>");
                        response.Write(string.Format("<li class=\"inputNum\"><input id=\"{0}\" value=\"{1}\" type=\"text\" onkeydown=\"__PageSearchKeyDown('{0}', false, event)\" /></li>", textBoxId, pageIndex));
                        response.Write(string.Format("<li><input type=\"button\" value=\"GO\" class=\"btnGo\" onclick=\"__PageSearchKeyDown('{0}', true, event)\" /></li>", textBoxId));
                        response.Write(string.Format(@"
                        <script type=""text/javascript"">
                            function __PageSearchKeyDown(objId, flag, evt) {{
                                evt = window.event ? window.event : evt;
                                if (flag || evt.keyCode == 13) {{
                                    var pageIndex = document.getElementById(objId).value;
                                    if (pageIndex > {1}) {{
                                        pageIndex = {1};
                                    }}
                                    window.location.href = '{0}'.replace('{{0}}', pageIndex);
                                }}
                            }}
                        </script>"
                            , urlPattern
                            , pageCount
                            ));
                    }
                    response.Write("</ul>");
                }
            }
        }
    }
}
