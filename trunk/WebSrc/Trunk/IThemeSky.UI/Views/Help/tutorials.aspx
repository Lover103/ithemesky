<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>Tutorials - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
<meta name="description" content="Frequently Asked Questions. <!-- #include file="/Views/Inc/siteDescription.inc" -->" />
<meta name="keywords" content="faq, Frequently Asked Questions, <!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
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
				<li>Tutorials</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent" class="commonPage">
			<!--help content begin-->
			<div class="commonContent">
				<ul class="faqList">
					<li><a href="#faqContent0">Get help for iSpirit</a></li>
					<li><a href="#faqContent1">How to install theme on jailbroken iPhone?</a></li>
					<li><a href="#faqContent2">How to Use WinterBoard?</a></li>
					<li><a href="#faqContent3">Why jailbreak iPhone?</a></li>
					<li><a href="#faqContent4">How to jailbreak iPhone?</a></li>
					<li><a href="#faqContent5">Why do winterboard themes not work?</a></li>
				</ul>
				<dl class="faqContent">
					<dt class="title"><span class="text">Get help for iSpirit?</span><a href="#top" name="faqContent0" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>
							If you have any questions about iSpirit, please read the <a href="/ispirit/help"><span style="color:#FFF;">iSpirit FAQ's documents</span></a> first. You can also get help by visiting our <a href="/forum"><span style="color:#FFF;">Community Forum</span></a>.
						</p>
					</dd>
				</dl>
				<dl class="faqContent">
					<dt class="title"><span class="text">How to install theme on jailbroken iPhone?</span><a href="#top" name="faqContent1" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>1. The easiest way to install theme on iPhone is by using iSpirit. <a href="/ispirit/help"><span style="color:#FFF;">Learn more about it</span></a>.</p>
						<p>2. If you have wifi, you could install the winterboard theme via cydia directly.</p>
						<p>3. Manually Install<br />
							&nbsp;&nbsp;&nbsp;(1). Connect to your iPhone (with iSpirit or other way)<br />
							&nbsp;&nbsp;&nbsp;(2). Drag &amp; drop the theme folder to "<strong>/Library/Themes</strong>" – alternatively, you can copy the theme to "<strong>/private/var/stash/Themes…</strong>" or "<strong>/var/stash/Themes…</strong>"<br />
							&nbsp;&nbsp;&nbsp;(3). Launch Winterboard and apply the new theme as usual. done!
						</p>
					</dd>
				</dl>
				<dl class="faqContent">
					<dt class="title"><span class="text">How to Use WinterBoard?</span><a href="#top" name="faqContent2" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>Please follow our <a href="/help/how-to-use-winterboard">WinterBoard use guide <span style="color:#FFF;">here</span></a>.</p>
					</dd>
				</dl>
				<dl class="faqContent">
					<dt class="title"><span class="text">Why jailbreak iPhone?</span><a href="#top" name="faqContent3" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>It cite some of the most compelling <a href="/help/why-jailbreak-iphone">reasons to jailbreak your iPhone <span style="color:#FFF;">here</span></a>.</p>
					</dd>
				</dl>
				<dl class="faqContent">
					<dt class="title"><span class="text">How to jailbreak iPhone?</span><a href="#top" name="faqContent4" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>Please follow our <a href="/help/how-to-jailbreak-iphone">iPhone jailbreak guide <span style="color:#FFF;">here</span></a>.</p>
					</dd>
				</dl>
				<dl class="faqContent">
					<dt class="title"><span class="text">Why do winterboard themes not work?</span><a href="#top" name="faqContent5" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>Ever since the winterboard update someone had trouble in applying a new winterboard theme. For example, winterboard themes could not work, some icons do not dim. Now, <a href="/help/why-do-winterboard-themes-not-work"><span style="color:#FFF;">here</span> is the way to fix them</a>. If you install themes with iSpirit, do not have to do these.
</p>
					</dd>
				</dl>
			</div>
			<!--help content end-->
		</div>
		<!--right col begin-->
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
		<!--right col end-->
	</div>
</div>
</asp:Content>