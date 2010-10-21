<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>Why do winterboard themes not work - Tutorials - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
<meta name="description" content="Why do winterboard themes not work. <!-- #include file="/Views/Inc/siteDescription.inc" -->" />
<meta name="keywords" content="dim icon, white icon, blank icon, <!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
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
				<li><a href="/help/tutorials">Tutorials</a></li>
				<li>Why do winterboard themes not work</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent" class="commonPage">
			<!--help content begin-->
			<div class="commonContent">
				<h1 class="normalTitle">Why do winterboard themes not work</h1>
				<div class="normalEntry">
					<p>Ever since the winterboard update someone had trouble in applying a new winterboard theme. For example, winterboard themes could not work, some icons do not dim. Now, here is the way to fix them.</p>
					<p>1. If you install a new version of winterboard, you must turn the summerboard mode on.</p>
					<p>2. If you are using ios4.x, besides, it is not working in English, you must rename the file icons under rules. If not, winterboard could not recognize the icon in your current language. The reason is that the document <i>ios4.x/System/Library/CoreServices/SpringBoard.app/(language)/LocalizedApplicationNames.strings</i> no longer exists.</p>
					<p><strong>Specific conversion rules are as follows:</strong></p>
					<p><strong>System:</strong><br />
						Messages.png &minus;&gt; com.apple.MobileSMS.png<br />
						Photos.png &minus;&gt; com.apple.mobileslideshow-Photos.png<br />
						Camera.png &minus;&gt; com.apple.mobileslideshow-Camera.png<br />
						Music.png &minus;&gt; com.apple.mobileipod-AudioPlayer.png<br />
						Videos.png &minus;&gt; com.apple.mobileipod-VideoPlayer.png<br />
						iTunes.png &minus;&gt; com.apple.MobileStore.png<br />
						Weather.png &minus;&gt; com.apple.weather.png<br />
						Installer.png &minus;&gt; com.nullriver.iphone.Installer.png<br />
						Calendar.png &minus;&gt; com.apple.mobilecal.png<br />
						Youtube.png &minus;&gt; com.apple.youtube.png<br />
						Notes.png &minus;&gt; com.apple.mobilenotes.png<br />
						Contacts.png &minus;&gt; com.apple.MobileAddressBook.png<br />
						Maps.png &minus;&gt; com.apple.Maps.png<br />
						Clock.png &minus;&gt; com.apple.mobiletimer.png<br />
						Calculator.png &minus;&gt; com.apple.calculator.png<br />
						Settings.png &minus;&gt; com.apple.Preferences.png<br />
						Stocks.png &minus;&gt; com.apple.stocks.png<br />
						VoiceMemos.png &minus;&gt; com.apple.VoiceMemos.png<br />
						AppStore.png &minus;&gt; com.apple.AppStore.png<br /><br />
						Safari.png &minus;&gt; com.apple.mobilesafari.png<br />
						Mail.png &minus;&gt; com.apple.mobilemail.png<br />
						Phone.png &minus;&gt; com.apple.mobilephone.png<br />
						iPod.png &minus;&gt; com.apple.mobileipod.png
					</p>
					<p><strong>Popular third-party Apps:</strong><br />
						Finder.png &minus;&gt; com.googlecode.MobileFinder<br />
						Terminal.png &minus;&gt; com.googlecode.mobileterminal.Terminal<br />
						winterboard.png &minus;&gt; com.saurik.WinterBoard.png
						Cydia.png &minus;&gt; com.saurik.Cydia
					</p>
					<p>After conversion, no matter which language you used, all themes can work perfectly.</p>
					<p>Also, if you install themes with <a href="/ispirit">iSpirit</a>, do not have to manually rename icons.</p>
				</div>
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
