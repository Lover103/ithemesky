<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>How to Use WinterBoard: A Walkthrough - Help - iPhone Themes free download, ithemesky.com provided iPhone Themes</title>
<meta name="description" content="How to Use WinterBoard. ithemesky.com provided iPhone themes, more than 2,000 iPhone themes free download!" />
<meta name="keywords" content="How to Use WinterBoard, iSpirit, iPhone theme, iPhone themes, jailbroken iPhone, install iPhone themes, free download, iPhone, WinterBoard, jailbreak" />
<link type="text/css" rel="stylesheet" media="screen" href="/Content/css/fancybox_normal.css">
<script type="text/javascript" src="/Content/js/fancybox_normal.js"></script>
<script type="text/javascript">
$(document).ready(function() {
	$("a.imgZoom").fancybox({
		'titleShow'		: false,
		'transitionIn'	: 'elastic',
		'transitionOut'	: 'elastic'
	});
});
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
				<li>Help</li>
				<li>How to Use WinterBoard: A Walkthrough</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent" class="commonPage">
			<!--help content begin-->
			<div class="commonContent">
				<h1 class="normalTitle">How to Use WinterBoard: A Walkthrough</h1>
				<div class="normalEntry">
					<p>While we might not see a free version of SummerBoard, one of the most highly-anticipated jailbroken apps, iPhone customization-junkies are turning to an alternative: WinterBoard.</p>
					<p>For those of you who don't know what SummerBoard is, it's an application that runs in the background of your iPhone and allows you to easily apply pre-made and custom themes that you can download directly from Installer / Cydia.  The themes can have customized elements such as icons, backgrounds, title bars, docks, etc..</p>
					<p>SummerBoard was created by a company named Nullriver (also the original creators of the famous Installer.app or Apptapp Installer, as well as the controversial Netshare App). Nullriver licensed the rights of SummerBoard, and Installer, to a new company called RIP Dev.  RIP Dev has an application you can purchase ($45 for an unlimited subscription) that now has a more advanced feature-set of SummerBoard included (called FaceLift). The application is called Kate.</p>
					<p>For those of us who have no money, and wish to have SummerBoard style theming anyway, Saurik (the creator of Cydia) has blessed us with WinterBoard.  WinterBoard is an open source application that promises to apply any SummerBoard theme, as well as allow for some new amazing effects (ie. animated/changing backgrounds, theming batteries, sliders, etc.).</p>
					<p>Here, I'll walk through installing WinterBoard and downloading a theme from Cydia to apply to the iPhone Springboard.  I'll also show you a few of the different variations of theming that can be applied using WinterBoard.</p>
					<h3>Walkthrough<br /><br /></h3>
					<h4>1. Download WinterBoard from Cydia</h4>
					<p><a href="/Content/images/help/winterboard/img_00019.png" class="imgZoom"><img width="150" src="/Content/images/help/winterboard/img_00019-200x300.png" alt="" /></a>&nbsp;&nbsp; &nbsp;<a href="/Content/images/help/winterboard/img_00028.png" class="imgZoom"><img width="150" alt="" src="/Content/images/help/winterboard/img_00028-200x300.png" /></a>&nbsp;&nbsp; &nbsp;<a href="/Content/images/help/winterboard/img_00034.png" class="imgZoom"><img width="150"  alt="" src="/Content/images/help/winterboard/img_00034-200x300.png" /></a></p>
					<h4>2. <span style="color:#FC0;">RESTART YOUR PHONE</span></h4>
					<p>In order for WinterBoard to work correctly you MUST restart your phone after installation.  To do this hold down the power button until the "slide to power off" slider appears.  Power off the phone, and then press the power button again to turn it back on.</p>
					<p><a href="/Content/images/help/winterboard/1426.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/1426-200x300.png" /></a></p>
					<h4>3. Open WinterBoard</h4>
					<p>Tap the WinterBoard icon on the Springboard.</p>
					<p><a href="/Content/images/help/winterboard/img_00018.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/img_00018-200x300.png" /></a></p>
					<h4>4. Apply Selected Theme(s)</h4>
					<p>WinterBoard comes with a few built in themes.  One is Saurik's own to show off changing backgrounds and other elements that WinterBoard is capable of.  I say 'Theme(s)' in the title of this step because WinterBoard allows you to apply multiple themes at one time.  For instance, you might apply one of Saurik's themes and also want 'Transparent Icon Labels'.  You could select both, and elements from both would be applied to your Springboard.</p>
					<p><a href="/Content/images/help/winterboard/img_00068.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/img_00068-200x300.png" /></a>&nbsp;&nbsp;&nbsp;<a href="/Content/images/help/winterboard/img_00086.png" class="imgZoom"><img width="200" height="300"alt="" src="/Content/images/help/winterboard/img_00086-200x300.png" /></a></p>
					<h4>5. Download Custom Themes From ithemesky.com or Cydia</h4>
					<p>Now that we've verified theming is working ok, let's try a custom theme from Cydia. I'll use a NBA theme for this example.</p>
					<ul>
						<li>Open Cydia</li>
						<li>Tap 'Sections'</li>
						<li>Tap 'Planet-iPhones Mods'</li>
						<li>Install 'NBA Theme'</li>
						<li>Open WinterBoard</li>
						<li>Uncheck everything</li>
						<li>Check NBA-Theme</li>
						<li>Exit WinterBoard</li>
					</ul>
					<p style="color:#FC0;">If you are not satisfied with the themes on Cydia, visit www.ithemesky.com, it provided thousands of free iphone themes, enjoy it. If you don't know how to download themes from ithemesky, please <a href="/ispirit">click here</a>.</p>
					<p><a href="/Content/images/help/winterboard/img_00095.png" class="imgZoom"><img width="150" alt="" src="/Content/images/help/winterboard/img_00095-200x300.png" /></a>&nbsp;&nbsp;&nbsp;<a href="/Content/images/help/winterboard/img_00103.png" class="imgZoom"><img width="150" alt="" src="/Content/images/help/winterboard/img_00103-200x300.png" /></a>&nbsp;&nbsp; &nbsp;<a href="/Content/images/help/winterboard/img_00115.png" class="imgZoom"><img width="150" alt="" src="/Content/images/help/winterboard/img_00115-200x300.png" /></a></p>
					<p><a href="/Content/images/help/winterboard/img_00143.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/img_00143-200x300.png" /></a>&nbsp;&nbsp;&nbsp;<a href="/Content/images/help/winterboard/img_00164.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/img_00164-200x300.png" /></a></p>
					<h4>6. Applying Multiple Themes</h4>
					<p>I mentioned earlier you could apply multiple themes to combine elements.  Let's try adding Saurik's theme to the NBA-Theme.  We should get Saurik's Background and Dock and NBA-Theme's Icons.</p>
					<ul>
						<li>Open WinterBoard</li>
						<li>Leave NBA-Theme Checked</li>
						<li>Check Saurik</li>
						<li>Press Home to return to the Springboard</li>
					</ul>
					<p><a href="/Content/images/help/winterboard/img_00174.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/img_00174-200x300.png" /></a></p>
					<p>Ok, I admit it's a really bad example as these two themes look hideous together, however it does show the power of WinterBoard compared to the original SummerBoard.</p>
					<h4>7. Remove All Themes</h4>
					<p>If you want to return the phone to a stock look, simply open WinterBoard and uncheck all themes.</p>
					<p><a href="/Content/images/help/winterboard/img_00018.png" class="imgZoom"><img width="200" height="300" alt="" src="/Content/images/help/winterboard/img_00018-200x300.png" /></a></p>
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
