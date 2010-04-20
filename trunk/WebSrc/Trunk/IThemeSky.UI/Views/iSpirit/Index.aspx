<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>iSpirit -iPhone themes and files manager - ithemesky.com</title>
<meta name="description" content="iSpirit is specially designed for iPhone, used on your PC for file and theme mangement. ithemesky.com provided iPhone themes, more than 2,000 iPhone themes free download!" />
<meta name="keywords" content="iSpirit, iPhone theme, iPhone themes, jailbroken iPhone, install iPhone themes, free download, iPhone, WinterBoard, jailbreak" />
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
			<div class="iSpirit">
				<dl>
					<dt><img src="/Content/images/ispirit_title.png" width="345" height="72" alt="iSpirit - iPhone themes and files manager" /></dt>
					<dd>
						<p>iSpirit is specially designed for iPhone, used on your PC for files and themes mangement. With iSpirit, you would install theme on your iPhone, and manage the files in your iphone easily. And also, you would free download iPhone themes from iSpirit. It works with iPhone, iPhone 3G, iPhone 3Gs in Firmware1.x, 2.x and 3.x.</p>
						<p style="margin-top:5px;"><strong>System Requirement:</strong></p>
						<p class="req">
							<strong>&nbsp;&nbsp;&nbsp;OS:</strong> Windows XP / Windows Vista / Windows 7<br />
							<strong>Base:</strong> <a href="http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iTunes9/061-7204.20100330.Cdr4T/iTunesSetup.exe" target="_blank" class="linkEct" rel="nofollow" title="Click here to download iTunes 9.1">iTunes 7.5 or higher</a> + <a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en" target="_blank" class="linkEct" rel="nofollow" title="Click here to download .net Framework 2.0">.net Framework 2.0/3.0/3.5</a>
						</p>
					</dd>
				</dl>
			</div>
			<div class="iSpiritNav">
				<ul>
					<li class="s1"><a href="/iSpirit/download/" title="Download - Click here to get iSpirit" target="_blank"><span class="link">Download - Click here to get iSpirit</span>
						<p class="dlPopup">
							<span class="label">Ver:</span><span class="entry">iSpirit 1.00</span>
							<span class="label">Date:</span><span class="entry">4/19/10</span>
							<span class="label">Size:</span><span class="entry">526KB</span>
							<span class="label">Req:</span><span class="entry"> windows XP/Vista/7<br />+ iTunes + .net Framework</span>
						</p>
					</a>	
					</li>
					<li class="s2"><a href="/ispirit" class="current" title="Features - Learn more about iSpirit"><span class="link">Features - Learn more about iSpirit</span></a></li>
					<li class="s3"><a href="/ispirit/help" title="Get Help - FAQs and Support"><span class="link">Get Help - FAQs and Support</span></a></li>
				</ul>
			</div>
			<div class="iSpiritMain">
				<h3 class="featureTitle">
					<span class="titleIco ico_1">1 </span>
					<span class="titleEntry">Powerful file management, support for various file operation and drag.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/01.png" class="imgZoom"><img src="/Content/images/ispirit/features/01_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_2">2 </span>
					<span class="titleEntry">Various operation appearance.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/02.png" class="imgZoom"><img src="/Content/images/ispirit/features/02_thumb.png" alt="" width="178" height="120" /></a>&nbsp;&nbsp;&nbsp;<a href="/Content/images/ispirit/features/03.png" class="imgZoom"><img src="/Content/images/ispirit/features/03_thumb.png" alt="" width="178" height="120" /></a>&nbsp;&nbsp;&nbsp;<a href="/Content/images/ispirit/features/04.png" class="imgZoom"><img src="/Content/images/ispirit/features/04_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_7">3 </span>
					<span class="titleEntry">Restart the Springboard via PC, no deamon install make you more comfortable.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/08.png" class="imgZoom"><img src="/Content/images/ispirit/features/08_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_8">4 </span>
					<span class="titleEntry" style="padding-top:0;">Powerful Winterboard theme management, available to install theme via zip file, local folder or online, and manage the theme you got.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/09.jpg" class="imgZoom"><img src="/Content/images/ispirit/features/09_thumb.png" alt="" width="178" height="120" /></a>&nbsp;&nbsp;&nbsp;
					<a href="/Content/images/ispirit/features/10.jpg" class="imgZoom"><img src="/Content/images/ispirit/features/10_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_3">5 </span>
					<span class="titleEntry">Transform and view <span class="orange">.PNG</span> file automatically.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/05.png" class="imgZoom"><img src="/Content/images/ispirit/features/05_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_4">6 </span>
					<span class="titleEntry">View and edit the <span class="orange">.PLIST</span> file.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/06.png" class="imgZoom"><img src="/Content/images/ispirit/features/06_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_5">7 </span>
					<span class="titleEntry">Adding files to favorite folders.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/07.png" class="imgZoom"><img src="/Content/images/ispirit/features/07_thumb.png" alt="" width="178" height="120" /></a>
				</p>
				<h3 class="featureTitle">
					<span class="titleIco ico_6">8 </span>
					<span class="titleEntry">Install <span class="orange">.DEB</span> software easily.<br /><br /></span>
				</h3>
			</div>
		</div>
		<!--ispirit end-->
	</div>
</div>
</asp:Content>
