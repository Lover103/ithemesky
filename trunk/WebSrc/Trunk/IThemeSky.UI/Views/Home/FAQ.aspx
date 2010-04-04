<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <title>FAQ - iphone themes free download, ithemesky.com provided iphone themes</title>
    <meta name="description" content="ithemesky.com provided iphone themes, more than 2,000 iphone themes free download!" />
    <meta name="keywords" content="iphone theme, iphone themes, jailbroken iphone, install iphone themes, free download, iphone" />
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
				<li><a href="#">FAQ</a></li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent" class="commonPage">
			<!--faq begin-->
			<div class="commonContent">
				<ul class="faqList">
					<li><a href="#faqContent1" name="top">How to install themes on your jailbroken iPhone?</a></li>
					<li><a href="#faqContent2">Trouble installing themes? Please check FAQs for solutions?</a></li>
					<li><a href="#">How to Use WinterBoard (SummerBoard Replacement): A Walkthrough</a></li>
					<li><a href="#">How to install themes on your jailbroken iPhone?</a></li>
				</ul>
				<dl class="faqContent">
					<dt class="title"><span class="text">How to Use WinterBoard (SummerBoard Replacement): A Walkthrough</span><a href="#top" name="faqContent1" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>After downloading and installing iPhoneBrowser (Windows) or iFuntastic (MAC) to your computer Plug in your iPhone to your computer, then open iPhoneBrowser (Windows) or iFuntastic (MAC)</p>
						<p>i. Windows: For iPhoneBrowser<br />
						On the top click "Goto Location", a menu will appear, click "WinterBoard Themes",<br /><br />
						<img src="images/temp/theme_cut01.jpg" width="170" height="255" /><br /><br />it will take you to winterboard directory of your iphone... this is where you can paste the downloaded themes.</p>
						<p>ii. Mac: For iFuntastic<br />
						On the left sidebar, find and click "Winterboard Themes", it will take you to winterboard directory of your iphone... this is where you can paste the downloaded themes
						</p>
					</dd>
				</dl>
				<dl class="faqContent">
					<dt class="title"><span class="text">How to install themes on your jailbroken iPhone?</span><a href="#top" name="faqContent2" class="goTop" title="TOP">TOP</a></dt>
					<dd class="faqEntry">
						<p>All of the themes we feature here at iPhone Theme Gallery require your iPhone to be jailbroken. Currently the themes we feature require an intermediate knowledge of FTP/SSH, as you have to “install” the theme yourself. However, in the future we will try to include more themes that are available in Cydia.</p>
						<p>Before you start downloading any of the themes we feature, please ensure the following is available to you on your iPhone and computer:
							<ol>
								<li>From your iPhone you will need to ensure that you have enabled SSH, as well as grabbing your WIFI network IP address. To view this you can go into settings, then into wifi. Click on the SSID of your network, and it will show an IP (eg: 192.168.1.103)
								</li>
								<li>Now you’ll want to open up your SSH app (WinSCP or Cyberduck) and enter in the following values in their appropriate fields
									<ol>
										<li>File Protocol: SCP (WinSCP) or SFTP (Cyberduck)</li>
										<li>Hostname: Your iPhones IP address</li>
										<li>Username: root</li>
										<li>Password: alpine</li>
										<li>File Protocol: SCP (WinSCP) or SFTP (Cyberduck)</li>
										<li>Hostname: Your iPhones IP address</li>
										<li>Username: root</li>
										<li>Password: alpine</li>
									</ol>
								</li>
								<li>You may then press enter to log in, and then you’ll want to answer Yes/Accept for the key prompt.</li>
							</ol>
						</p>
					</dd>
				</dl>
			</div>
			<!--faq end-->
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
							    <dt><a href="<%=theme.ThemeDetailUrl %>"><img src="/<%=theme.Thumbnail_112x168 %>" width="32" height="48" alt="<%=theme.Title %>" /></a></dt>
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
