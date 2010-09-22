﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SimpleThemeView>>" %>
<% foreach(SimpleThemeView theme in ViewData.Model){ %>
    <dl>
	    <dt><a href="<%=theme.ThemeDetailUrl %>"><%=theme.Title.SubStr(20) %></a></dt>
	    <dd class="themeCut">
	    <a href="<%=theme.ThemeDetailUrl %>" title="<%=theme.Title %>"><img src="<%=theme.Thumbnail_112x168 %>" width="112" height="168" alt="<%=theme.Title %>" /></a>
	    <% if (theme.SupportIPhone4)
        { %>
	        <span title="This is a 480x960(HD) iphone 4 theme" class="forIphone4">iPhone 4 Theme</span>
	    <%} %>
	    </dd>
	    <dd><span class="rateResult star<%=theme.CommendIndex %>" title="<%=theme.CommendIndex %>/5 stars"><%=theme.CommendIndex %>/5 stars</span></dd>
	    <dd class="btnDownload"><a href="<%=theme.ThemeDetailUrl %>" title="Free Download">Free Download</a></dd>
    </dl>
<%} %>
<% if (ViewData.Model.Count < 1) { %>
No result.
<%} %>