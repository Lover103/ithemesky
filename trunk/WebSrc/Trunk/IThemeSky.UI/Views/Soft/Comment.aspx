<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SoftCommentDetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<title><%=ViewData.Model.SoftTitle %> - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
    <meta name="description" content="<!-- #include file="/Views/Inc/siteDescription.inc" -->" />
    <meta name="keywords" content="<!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
    <link rel="stylesheet" href="/Content/css/fancybox.css" type="text/css" />
    <script type="text/javascript" src="/Content/js/fancybox.js"></script>
    <script type="text/javascript">
        $(document).ready(
            function() {
                $("img#themeCut").parent().fancybox({});
                LoadSoftComments(1);
                BindRateEvent();
            });
        _softIdentify = '<%=ViewData.Model.SoftIdentfiy %>';
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
				<li><a href="#"><%=ViewData.Model.SoftTitle %></a></li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent">
		    <div class="themeDetail clearfix">
		        <%=ViewData.Model.SoftDescription %>
		    </div>
			<!--comment col begin-->
			<div class="themeComment">
				<div id="commentListContainer"></div>
				<div class="commentAdd">
					<% Html.RenderPartial("PostSoftCommentForm", ViewData.Model.PostComment); %>
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
