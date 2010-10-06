<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<title><%=ViewData.Model.CurrentTheme.Title %> iPhone<%=ViewData.Model.CurrentTheme.SupportIPhone4 ? " 4" : "" %> theme - <%=ViewData.Model.CurrentTheme.CategoryName %> - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
    <meta name="description" content="<%=ViewData.Model.CurrentTheme.Title %> iPhone theme. <!-- #include file="/Views/Inc/siteDescription.inc" -->" />
    <meta name="keywords" content="<% foreach (string tag in ViewData.Model.Tags){ %><%=tag %>, <%} %> <%=ViewData.Model.CurrentTheme.CategoryName %>, <!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
    <link rel="stylesheet" href="/Content/css/fancybox.css" type="text/css" />
    <script type="text/javascript" src="/Content/js/fancybox.js"></script>
    <script type="text/javascript">
        $(document).ready(
            function() {
                $("img#themeCut").parent().fancybox({});
                LoadComments(1);
                BindRateEvent();
            });
        _themeId = '<%=ViewData.Model.CurrentTheme.ThemeId %>';
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper" class="clearfix">
	<div id="sidebar">
		<!--Categories begin-->
		<% Html.RenderPartial("ThemeCategories", ViewData.Model.ThemeCategories); %>
		<!--Categories end-->
		<!--Tags begin-->
		<% Html.RenderPartial("HotTags"); %>
		<!--Tags end-->
	</div>
	<div id="main">
		<!--breadcrumb begin-->
		<div class="pageGuide">
			<ul class="breadcrumb">
				<li class="home"><a href="/" title="Homepage">ithemesky.com Homepage</a></li>
				<li><a href="/list/new/">All iPhone Themes</a></li>
				<li><a href="/list/new/<%=ViewData.Model.CurrentTheme.CategoryName.Replace(" ", "").Replace("&", "-") %>_<%=ViewData.Model.CurrentTheme.CategoryId %>/"><%=ViewData.Model.CurrentTheme.CategoryName %></a></li>
				<li><%=ViewData.Model.CurrentTheme.Title %> iPhone<%=ViewData.Model.CurrentTheme.SupportIPhone4 ? " 4" : "" %> theme</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent">
			<!--theme detail begin-->
			<div class="themeDetail clearfix">
				<div class="detailSide">
					<div class="themeCut">
					    <a href="<%=ViewData.Model.CurrentTheme.ThumbnailName %>"><img src="<%=ViewData.Model.CurrentTheme.ThumbnailName %>" width="170" height="255" alt="<%=ViewData.Model.CurrentTheme.Title %> iPhone theme" id="themeCut" /></a>
					    <% if (ViewData.Model.CurrentTheme.SupportIPhone4) { %>
					        <span title="<%=ViewData.Model.CurrentTheme.Title %> is a 480x960(HD) iphone 4 theme" class="forIphone4">iPhone 4 Theme</span>
					    <%} %>
					</div>
					<ul class="btn">
						<li class="previous"><a <%=ViewData.Model.PrevThemeId <=0 ? "class=\"none\" onclick=\"return false;\"" : "" %> href="/iphone-themes/<%=ViewData.Model.PrevThemeName.Trim().Replace(" ", "-") %>/<%=ViewData.Model.PrevThemeId %>" title="Previous theme: <%=ViewData.Model.PrevThemeName %> iPhone theme">Previous</a></li>
						<li class="next"><a <%=ViewData.Model.NextThemeId <=0 ? "class=\"none\" onclick=\"return false;\"" : "" %> href="/iphone-themes/<%=ViewData.Model.NextThemeName.Trim().Replace(" ", "-") %>/<%=ViewData.Model.NextThemeId %>" title="Next theme: <%=ViewData.Model.NextThemeName %> iPhone theme">Next</a></li>
					</ul>
				</div>
				<div class="detailInfo">
					<h1 class="title"><%=ViewData.Model.CurrentTheme.Title %> iPhone<%=ViewData.Model.CurrentTheme.SupportIPhone4 ? " 4" : "" %> theme</h1>
					<dl class="details clearfix">
						<dt>Resolution:</dt>
						<dd><%=ViewData.Model.CurrentTheme.SupportIPhone4 ? "480X960" : "320X480"%></dd>
					</dl>
					<dl class="details clearfix">
						<dt>Category:</dt>
						<dd><%=ViewData.Model.CurrentTheme.CategoryName %></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Tags:</dt>
						<dd>
							<% for (int i = 0; i < ViewData.Model.Tags.Count; i++)
								{ %>
								<%= i > 0 ? ", " : ""%><a href="/list/new/1/<%=ViewData.Model.Tags[i] %>"><%=ViewData.Model.Tags[i] %></a>
							<%} %>
						</dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Update:</dt>
						<dd><%=ViewData.Model.CurrentTheme.AddTime.ToString("D", ViewData.Model.USACultureInfo)%></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Views:</dt>
						<dd><%=ViewData.Model.CurrentTheme.Views.ToString("N00") %> views</dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Downloads:</dt>
						<dd><%=ViewData.Model.CurrentTheme.Downloads.ToString("N00") %></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Size:</dt>
						<dd><%=ViewData.Model.CurrentTheme.FileSize.ToFileSize() %></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Rate now:</dt>
						<dd class="detailRate">
							<span class="rateResult star<%=ViewData.Model.CurrentTheme.CommendIndex %>"><%=ViewData.Model.CurrentTheme.CommendIndex %>/5 stars</span>
							<ul>
								<li><a href="javascript:;" class="rateS1" value="1" title="1 Star">1 Star</a></li>
								<li><a href="javascript:;" class="rateS2" value="2" title="2 Stars">2 Stars</a></li>
								<li><a href="javascript:;" class="rateS3" value="3" title="3 Stars">3 Stars</a></li>
								<li><a href="javascript:;" class="rateS4" value="4" title="4 Stars">4 Stars</a></li>
								<li><a href="javascript:;" class="rateS5" value="5" title="5 Stars">5 Stars</a></li>
							</ul>
						</dd>
					</dl>
					<ul class="detailBtn clearfix">
						<li class="btnDownload"><a href="/Service/Download/<%=ViewData.Model.CurrentTheme.ThemeId %>,<%=ViewData.Model.CurrentTheme.Title %>" title="Download">Download</a></li>
						<li class="btnCreate">
						<% if (ViewData.Model.CurrentTheme.SupportIPhone4){ %>
						    <span class="off" title="Theme Creator for iPhone 4 comming soon">Can't be modified</span>
						<%} else { %>
						    <a href="/Creator/<%=ViewData.Model.CurrentTheme.Title.Replace(" ", "-") %>,<%=ViewData.Model.CurrentTheme.ThemeId %>" title="Modify <%=ViewData.Model.CurrentTheme.Title %> iPhone theme with Theme Creator" target="_blank" rel="nofollow">Modify <%=ViewData.Model.CurrentTheme.Title %> iPhone theme with Theme Creator</a>
						<%} %>
						</li>
					</ul>
					<div class="installIip">
						<% if (ViewData.Model.CurrentTheme.SupportIPhone4){ %>
						    <a href="/ispirit/"><span class="orange">Install theme to iPhone 4 with <span style=" text-decoration:underline;">iSpirit</span></span></a>. Some iPhone 4 themes can also be used in former iPhones.
						<%} else { %>
						Click "Create Now" above to modify this theme in your mind with iPhone theme Creator Online and then download it. <a href="/ispirit/"><span class="orange">Install theme to iPhone with <span style=" text-decoration:underline;">iSpirit</span></span></a>.
						<%} %>
					</div>
					<dl class="themeShare">
						<dt>Share this theme:</dt>
						<dd class="shareTwitter"><a rel="nofollow" target="_blank" href="http://twitter.com/home?status=<%=ViewData.Model.CurrentTheme.Title %>+<%=Request.Url.ToString()%>" title="Tweet this!">Tweet this!</a></dd>
						<dd class="shareFacebook"><a rel="nofollow" target="_blank" href="http://www.facebook.com/share.php?u=<%=Request.Url.ToString()%>" title="Share on Facebook.">Share on Facebook.</a></dd>
						<dd class="shareDigg"><a rel="nofollow" target="_blank" href="http://digg.com/submit?phase=2&amp;url=<%=Request.Url.ToString()%>&amp;title=<%=ViewData.Model.CurrentTheme.Title %>" title="Digg This!">Digg This!</a></dd>
						<dd class="shareStumbleUpon"><a rel="nofollow" target="_blank" href="http://www.stumbleupon.com/submit?url=<%=Request.Url.ToString()%>&amp;title=<%=ViewData.Model.CurrentTheme.Title %>" title="StumbleUpon.">StumbleUpon.</a></dd>
						<dd class="shareReddit"><a rel="nofollow" target="_blank" href="http://reddit.com/submit?url=<%=Request.Url.ToString()%>&amp;title=<%=ViewData.Model.CurrentTheme.Title %>" title="Vote on Reddit.">Vote on Reddit.</a></dd>
						<dd class="shareDelicious"><a rel="nofollow" target="_blank" href="http://del.icio.us/post?url=<%=Request.Url.ToString()%>&amp;title=<%=ViewData.Model.CurrentTheme.Title %>" title="Bookmark on Delicious.">Bookmark on Delicious.</a></dd>
					</dl>
				</div>
			</div>
			<!--theme detail end-->
			<!--I'm Feeling Lucky begin-->
			<div class="mainCol themeDetailCol">
				<div class="mainColHead">
					<h3 class="colTitle">I'm Feeling Lucky</h3>
				</div>
				<div class="mainColContent themeList clearfix">
					<% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.LuckyThemes); %> 
				</div>
			</div>
			<!--I'm Feeling Lucky end-->
			<!--comment col begin-->
			<div class="themeComment">
				<div id="commentListContainer"></div>
				<div class="commentAdd">
					<% Html.RenderPartial("PostCommentForm", ViewData.Model.PostComment); %>
				</div>
			</div>
			<!--comment col end-->
		</div>
		<!--theme detail right col begin-->
		<div id="subContent">
			<div class="subCol themeDetailSub">
				<h3 class="subColHead colTitle">Top Download</h3>
				<ul class="subColContent subTopDownload clearfix">
				    <% 
                    int index = 0;	        
	    	        foreach (SimpleThemeView theme in ViewData.Model.TopDownloadThemes)
                    { %>
                    <% if (index == 0)
                       { %>
					    <li class="champion">
						    <dl class="clearfix">
							    <dt><a href="<%=theme.ThemeDetailUrl %>"><img src="<%=theme.Thumbnail_112x168 %>" width="32" height="48" alt="<%=theme.Title %>" /></a></dt>
							    <dd class="title"><a href="<%=theme.ThemeDetailUrl %>"><%=theme.Title.SubStr(12) %></a></dd>
							    <dd><span class="rateResult star<%=theme.CommendIndex %>" title="<%=theme.CommendIndex %>/5 stars"><%=theme.CommendIndex %>/5 stars</span></dd>
							    <dd><small>Downloads:</small><span class="downloadNum"><%=theme.Downloads %></span></dd>
						    </dl>
					    </li>
					<%  }
                       else
                       {%>
					    <li class="normal"><span class="rankNum"><%=index+1%></span><a href="<%=theme.ThemeDetailUrl %>" class="title" title="<%=theme.Title %>"><%=theme.Title.SubStr(12) %></a><span class="downloadNum"><%=theme.Downloads %></span></li>
					<%
                        }
                        index++;
                    } %>
				</ul>
			</div>
			<div class="subCol themeDetailSub">
				<h3 class="subColHead colTitle">Recommended</h3>
				<ul class="subColContent subRecommended">
				    <% foreach (SimpleThemeView theme in ViewData.Model.CommendThemes)
                    { %>
					<li><span class="downloadNum"><%=theme.Downloads %></span><a href="<%=theme.ThemeDetailUrl %>" title="<%=theme.Title %>"><%=theme.Title.SubStr(12) %></a></li>
					<%} %>
				</ul>
			</div>
		</div>
		<!--theme detail right col end-->
	</div>
</div>
</asp:Content>
