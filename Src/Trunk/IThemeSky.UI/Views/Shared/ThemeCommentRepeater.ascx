<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CommentListModel>" %>
<h3 class="commentHead"><%=ViewData.Model.RecordCount %> Comments:</h3>
<ul class="commentList">
    <% 
        int index = 1;
        foreach (ThemeComment comment in ViewData.Model.Comments)
       { %>
	<li class="commentContent">
		<div class="commentData">
			<span class="commentNumber">#<%=(ViewData.Model.PageIndex-1) * ViewData.Model.PageSize + index %></span>
			<cite><%=comment.UserName %></cite>
			<small class="commentMeta">on <%=comment.AddTime.ToString("F", ViewData.Model.USACultureInfo)%> </small>
		</div>
		<div class="commentEntry">
			<p>
			    <%=comment.Content %>
			</p>
		</div>
	</li>
	<%
        index++;
        } %>
</ul>
<div class="commentPage clearfix">
	<% Html.RenderPagination("javascript:LoadComments({0})", ViewData.Model.PageIndex, ViewData.Model.PageSize, ViewData.Model.RecordCount, 10, false, false); %>
</div>