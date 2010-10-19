<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SearchModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<title><%=ViewData.Model.Keyword%> iPhone themes - Search results - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
    <meta name="description" content="<%=ViewData.Model.Keyword%> iPhone themes. <!-- #include file="/Views/Inc/siteDescription.inc" -->" />
    <meta name="keywords" content="<%=ViewData.Model.Keyword%>, <!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper" class="clearfix">
	<div id="sidebar">
		<!--Categories begin-->
		<% Html.RenderPartial("ThemeCategories", ViewData.Model.ThemeCategories); %>
		<!--Categories end-->
	</div>
	<div id="main">
		<!--breadcrumb begin-->
		<div class="pageGuide">
			<ul class="breadcrumb">
				<li class="home"><a href="/" title="Homepage">ithemesky.com Homepage</a></li>
				<li><a href="/list/new">All iPhone Themes</a></li>
				<li><a href="#">Search: <%=ViewData.Model.Keyword%></a></li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<!--lists begin-->
		<div class="lists">
			<dl class="listsSort">
				<dt>Sort By:</dt>
				<dd <%=ViewData.Model.Sort == ThemeSortOption.New ? "class=\"selected\"" : "" %>><a href="<%=String.Format(ViewData.Model.UrlPatternWithoutSort, 1, "new") %>">Date</a></dd>
				<dd>-</dd>
				<dd <%=ViewData.Model.Sort == ThemeSortOption.Popular ? "class=\"selected\"" : "" %>><a href="<%=String.Format(ViewData.Model.UrlPatternWithoutSort, 1, "popular") %>">Downloads</a></dd>
				<dd>-</dd>
				<dd <%=ViewData.Model.Sort == ThemeSortOption.Rating ? "class=\"selected\"" : "" %>><a href="<%=String.Format(ViewData.Model.UrlPatternWithoutSort, 1, "rating") %>">Rating</a></dd>
			</dl>
			<!--theme list begin-->
			<div class="themeList clearfix">
				<% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.Themes); %> 
			</div>
			<!--theme list end-->
		</div>
		<!--lists end-->
		<!--pageinfo begin-->
		<div class="pageInfo">
			<% Html.RenderPagination(ViewData.Model.UrlPatternWithoutPageIndex, ViewData.Model.PageIndex, ViewData.Model.PageSize, ViewData.Model.RecordCount, 10); %>
		</div>
		<!--pageinfo end-->
	</div>
</div>
</asp:Content>
