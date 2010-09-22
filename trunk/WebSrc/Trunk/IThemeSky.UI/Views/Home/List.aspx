<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ListModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<title><%=ViewData.Model.CategoryId > 0 ? ViewData.Model.CategoryName.Replace("-", " & ") : "All Categories"%> iPhone themes - <%=ViewData.Model.Sort%> - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
    <meta name="description" content="<%=ViewData.Model.CategoryId > 0 ? ViewData.Model.CategoryName.Replace("-", " & ") : "All Categories"%> iPhone themes, <!-- #include file="/Views/Inc/siteDescription.inc" -->" />
    <meta name="keywords" content="<!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
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
		<a href="http://www.twitter.com/iThemesky/" title="Follow us on Twitter" class="sideTwitter" target="_blank">Follow us on Twitter</a>
	</div>
	<div id="main">
		<!--breadcrumb begin-->
		<div class="pageGuide">
			<ul class="breadcrumb">
				<li class="home"><a href="/" title="Homepage">ithemesky.com Homepage</a></li>
				<li><a href="/list/new">All Categories</a></li>
				<% if (ViewData.Model.CategoryId > 0)
                   { %>
				<li><a href="/list/new/<%=ViewData.Model.CategoryName%>_<%=ViewData.Model.CategoryId%>"><%=ViewData.Model.CategoryName.Replace("-", " & ")%></a></li>
				<% } %>
			</ul>
		</div>
		<!--breadcrumb end-->
		<!--select tag begin-->
		<div class="listSelect">
			<!--tips begin-->
			<div class="selectedTips">
				<p class="clearfix">
					<span class="tips">Please choose anyone of the tags or any tags together below. (e.g. Blue, Red, and cool.)</span>
					<a href="#" class="close" onclick="createCookie('GotTagsHelp', '1', 365);$('div.selectedTips').hide();">I got it</a>
				</p>
			</div>
			<script type="text/javascript">
			    if (readCookie('GotTagsHelp') != null) {
			        $('div.selectedTips').hide();
			    }
			</script>
			<!--tips end-->
			<!--tags sort list begin-->
			<div class="selectTags">
				<dl class="clearfix">
					<dt>Color:</dt>
					<dd>
						<ul class="clearfix">
							<li><a href="#">Black</a></li>
							<li><a href="#">Blue</a></li>
							<li><a href="#">Brown</a></li>
							<li><a href="#">Cyan</a></li>
							<li><a href="#">Gray</a></li>
							<li><a href="#">Green</a></li>
							<li><a href="#">Orange</a></li>
							<li><a href="#">Pink</a></li>
							<li><a href="#">Purple</a></li>
							<li><a href="#">Red</a></li>
							<li><a href="#">White</a></li>
							<li><a href="#">Yellow</a></li>
						</ul>
					</dd>
				</dl>
				<dl class="clearfix last">
					<dt>Hot Tags:</dt>
					<dd>
						<ul class="clearfix">
							<li><a href="#">sexy</a></li>
							<li><a href="#">sweet</a></li>
							<li><a href="#">3D</a></li>
							<li><a href="#">Chinese</a></li>
							<li><a href="#">cute</a></li>
							<li><a href="#">girl</a></li>
							<li><a href="#">beauty</a></li>
							<li><a href="#">boy</a></li>
							<li><a href="#">cool</a></li>
							<li><a href="#">glass</a></li>
							<li><a href="#">dark</a></li>
							<li><a href="#">Apple</a></li>
							<li><a href="#">war</a></li>
							<li><a href="#">hero</a></li>
							<li><a href="#">lovely</a></li>
							<li><a href="#">funny</a></li>
							<li><a href="#">fun</a></li>
							<li><a href="#">man</a></li>
							<li><a href="#">legs</a></li>
							<li><a href="#">Japan</a></li>
							<li><a href="#">night</a></li>
							<li><a href="#">gun</a></li>
							<li><a href="#">fantasy</a></li>
							<li><a href="#">WOW</a></li>
							<li><a href="#">simple</a></li>
							<li><a href="#">Disney</a></li>
							<li><a href="#">pure</a></li>
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
