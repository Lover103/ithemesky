<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SimpleThemeView>>" %>
<h3 class="searchHead">Hot Results</h3>
<div class="list">
    <% foreach(SimpleThemeView theme in ViewData.Model){ %>
    <dl>
	    <dt><a href="#"><img src="/<%=theme.Thumbnail_112x168 %>" width="60" height="90" alt="Sexy Lady" /></a></dt>
	    <dd>
		    <span class="title"><a href="<%=theme.ThemeDetailUrl %>"><%=theme.Title %></a></span>
		    <span class="rateResult star<%=theme.CommendIndex %>" title="<%=theme.CommendIndex %>/5 stars"><%=theme.CommendIndex %>/5 stars</span>
		    <span class="b">Downloads:</span> <%=theme.Downloads %>
		    <a href="/Service/Download/122" title="Download" class="btnDownload">Download</a>
	    </dd>
    </dl>
    <%} %>
    <% if (ViewData.Model.Count == 0){ %>
	    No Result. Please try another key word.
	<%} %>
</div>
<div class="searchTip">
    Total <%=ViewData["RecordCount"] %> results. 
    <% if (ViewData.Model.Count > 0){ %>
    <a href="#">View all results</a>
    <%} %>
</div>