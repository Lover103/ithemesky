<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SimpleThemeView>>" %>
<% foreach(SimpleThemeView theme in ViewData.Model){ %>
    <dl>
	    <dt><a href="#"><%=theme.Title %></a></dt>
	    <dd class="themeCut"><a href="#"><img src="<%=theme.Thumbnail_112x168 %>" width="112" height="168" alt="<%=theme.Title %>" /></a></dd>
	    <dd><span class="rateResult star<%=theme.CommendIndex %>" title="<%=theme.CommendIndex %>/5 stars"><%=theme.CommendIndex %>/5 stars</span></dd>
	    <dd class="btnDownload"><a href="#" title="Free Download">Free Download</a></dd>
    </dl>
<%} %>