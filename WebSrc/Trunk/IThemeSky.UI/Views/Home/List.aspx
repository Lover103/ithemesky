<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<title><%=ViewData.Model.CategoryId > 0 ? ViewData.Model.CategoryName : "All Categories"%> - <%=ViewData.Model.Sort%> iphone themes free download, ithemesky.com provided iphone themes</title>
    <meta name="description" content="ithemesky.com provided iphone themes, more than 2,000 iphone themes free download!" />
    <meta name="keywords" content="iphone theme, iphone themes, jailbroken iphone, install iphone themes, free download, iphone" />
    <script type="text/javascript">
        $(document).ready(
            function() {
                InitTagsEvent('<%=ViewData.Model.UrlPatternWithouTags %>', '<%=ViewData.Model.TagNames %>');
            }
        );
    </script>
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
				<li><a href="/list/new">All Categories</a></li>
				<% if (ViewData.Model.CategoryId > 0)
                   { %>
				<li><a href="/list/new/<%=ViewData.Model.CategoryName%>_<%=ViewData.Model.CategoryId%>"><%=ViewData.Model.CategoryName%></a></li>
				<% } %>
			</ul>
		</div>
		<!--breadcrumb end-->
		<!--select tag begin-->
		<div class="listSelect">
			<!--tips begin-->
			<div class="selectedTips">
				<p class="clearfix">
					<span class="tips">Please choose anyone of the tags or any tags together below. (e.g. blue, red, and Christmas.)</span>
					<a href="#" class="close" onclick="$('div.selectedTips').hide()">I got it</a>
				</p>
			</div>
			<!--tips end-->
			<!--tags sort list begin-->
			<div class="selectTags">
				<dl class="clearfix">
					<dt>Color:</dt>
					<dd>
						<ul class="clearfix">
							<li><a href="#">Blue</a></li>
							<li><a href="#">White</a></li>
							<li><a href="#">Orange</a></li>
							<li><a href="#">Pink</a></li>
							<li><a href="#">Gray</a></li>
							<li><a href="#">Green</a></li>
							<li><a href="#">Red</a></li>
							<li><a href="#">Black</a></li>
							<li><a href="#">Yellow</a></li>
						</ul>
					</dd>
				</dl>
				<dl class="clearfix last">
					<dt>Holiday:</dt>
					<dd>
						<ul class="clearfix">
							<li><a href="#">New Year’s Day</a></li>
							<li><a href="#">Valentines Day</a></li>
							<li><a href="#">Easter Day</a></li>
							<li><a href="#">Halloween</a></li>
							<li><a href="#">Thanksgiving Day</a></li>
							<li><a href="#">Christmas</a></li>
							<li><a href="#">Boxing Day</a></li>
							<li><a href="#">Mothers Day</a></li>
							<li><a href="#">Spring Festival</a></li>
							<li><a href="#">Moon Festival</a></li>
						</ul>
					</dd>
				</dl>
			</div>
			<!--tags sort list end-->
		</div>
		<!--select tag end-->
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
