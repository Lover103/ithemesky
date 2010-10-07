<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>iSpirit - iPhone Manager, manage iPhone apps, iPhone themes and files easily.</title>
<meta name="description" content="iSpirit is a iPhone Manager which help you manage files between PC and iPhone, install iPhone apps and change iPhone themes easily." />
<meta name="keywords" content="iSpirit download, <!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
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
				<li>iSpirit</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<!--ispirit begin-->
		<div class="fullContent">
			<% Html.RenderPartial("iSpirit"); %>
			<div class="iSpiritNav">
				<ul>
					<li class="s1"><% Html.RenderPartial("iSpiritDownload"); %></li>
					<li class="s2"><a href="/ispirit" class="current" title="Features - Learn more about iSpirit"><span class="link">Features - Learn more about iSpirit</span></a></li>
					<li class="s3"><a href="/ispirit/help" title="Get Help - iSpirit FAQs and Support"><span class="link">Get Help - iSpirit FAQs and Support</span></a></li>
				</ul>
			</div>
			<div class="iSpiritMain">
				<div class="featuresList clearfix">
					<dl>
						<dt>Powerful File Manager</dt>
						<dd class="ico ico_1">Intro:</dd>
						<dd class="entry">Explore any directory on idevice and drag "n" drop files or folders between PC and iPhone.</dd>
					</dl>
					<dl>
						<dt>Winterboard Theme Manager</dt>
						<dd class="ico ico_2">Intro:</dd>
						<dd class="entry">Install theme via zip file, local folder or online. Shows the themes installed on idevice and manage them.</dd>
					</dl>
					<dl>
						<dt>Install .DEB  and .IPA Files Easily</dt>
						<dd class="ico ico_3">Intro:</dd>
						<dd class="entry">Select a .DEB/.IPA file you want to install. Click the button "Install" then iSpirit will install it completly.</dd>
					</dl>
					<dl>
						<dt>App Manager</dt>
						<dd class="ico ico_4">Intro:</dd>
						<dd class="entry">Shows Cydia sources, installed packages and list various categories to download apps.</dd>
					</dl>
					<dl>
						<dt>Control and Set Smartly</dt>
						<dd class="ico ico_5">Intro:</dd>
						<dd class="entry">Restart Springboard, shutdown iPhone, reboot iPhone, support to manage iPhone through the Command-Line.</dd>
					</dl>
					<dl>
						<dt>More cool Features</dt>
						<dd class="ico ico_6">Intro:</dd>
						<dd class="entry">Connect via WIFI or USB; Fix white/blank icon; View and edit .PLIST file; Adding files to favorite folders. Etc.</dd>
					</dl>
				</div>
				<dl class="featureScreen clearfix">
					<dt>iSpirit Screenshots:</dt>
					<dd><a href="/Content/images/ispirit/features/01.png" class="imgZoom"><img src="/Content/images/ispirit/features/01_thumb.png" alt="" width="178" height="120" /></a></dd>
					<dd><a href="/Content/images/ispirit/features/02.png" class="imgZoom"><img src="/Content/images/ispirit/features/02_thumb.png" alt="" width="178" height="120" /></a></dd>
					<dd><a href="/Content/images/ispirit/features/03.png" class="imgZoom"><img src="/Content/images/ispirit/features/03_thumb.png" alt="" width="178" height="120" /></a></dd>
					<dd><a href="/Content/images/ispirit/features/04.png" class="imgZoom"><img src="/Content/images/ispirit/features/04_thumb.png" alt="" width="178" height="120" /></a></dd>
					<dd><a href="/Content/images/ispirit/features/05.png" class="imgZoom"><img src="/Content/images/ispirit/features/05_thumb.png" alt="" width="178" height="120" /></a></dd>
					<dd><a href="/Content/images/ispirit/features/06.png" class="imgZoom"><img src="/Content/images/ispirit/features/06_thumb.png" alt="" width="178" height="120" /></a></dd>
				</dl>
				<p class="featureContent clearfix">
					<span style="float:left; width:550px;">If you would like to donate to help support the growing of our free software and service, you may do so with the button to the right.</span>
					<span style="float:right; width:92px; margin-top:5px;">
						<form name="_xclick" action="https://www.paypal.com/cgi-bin/webscr" method="post" />
						<input type="hidden" name="cmd" value="_xclick" />
						<input type="hidden" name="business" value="ithemesky@gmail.com" />
						<input type="hidden" name="item_name" value="Donate to iThemeSky.com" />
						<input type="hidden" name="currency_code" value="USD" />
						<input type="hidden" name="amount" value="" />
						<input type="image" src="https://www.paypal.com/en_US/i/btn/btn_donate_LG.gif" border="0" name="submit" alt="Donate to iThemeSky" />
						</form>
					</span>
				</p>
			</div>
		</div>
		<!--ispirit end-->
	</div>
</div>
</asp:Content>
