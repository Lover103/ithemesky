<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<ThemeCategory>>" %>
<div class="sideCol">
    <h3 class="sideColHead colTitle">Categories</h3>
    <ul class="sideColContent sideMenu">
	    <% foreach (ThemeCategory category in ViewData.Model)
        { %>
	    <li><a href="/list/new/<%=HttpUtility.UrlEncode(category.CategoryName)%>_<%=category.CategoryId%>"><%=category.CategoryName%></a></li>
	    <%} %>
    </ul>
</div>