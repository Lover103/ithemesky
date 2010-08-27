﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>iSpirit - iPhone apps, themes and files manager - <!-- #include file="/Views/Inc/siteTitle.inc" --></title>
<meta name="description" content="iSpirit is a utility that help you manage files between PC and iPhone. Install deb files and change iPhone themes easily with it." />
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
					<li class="s3"><a href="/ispirit/help" title="Get Help - FAQs and Support"><span class="link">Get Help - FAQs and Support</span></a></li>
				</ul>
			</div>
			<div class="iSpiritMain">
				<h3 class="featureTitle">
					<span class="titleIco ico_6">8 </span>
					<span class="titleEntry">Install <span class="orange">.DEB</span> software easily.<a href="http://forum.ithemesky.com/default.aspx?g=posts&m=5&#post5"   target=_blank> Get Help.</a><br /><br /></span>
				</h3>
				
				<h3 class="featureTitle">
					<span class="titleIco ico_8">4 </span>
					<span class="titleEntry" style="padding-top:0;">Powerful Winterboard theme management, available to install theme via zip file, local folder or online, and manage the theme you got.</span>
				</h3>
				
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/09.jpg" class="imgZoom"><img src="/Content/images/ispirit/features/09_thumb.png" alt="" width="178" height="120" /></a>&nbsp;&nbsp;&nbsp;
					<a href="/Content/images/ispirit/features/10.jpg" class="imgZoom"><img src="/Content/images/ispirit/features/10_thumb.png" alt="" width="178" height="120" /></a>
				</p>
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
					<span class="titleEntry">Restart the Springboard/Reboot/Shutdown via PC, no deamon install make you more comfortable.</span>
				</h3>
				<p class="featureContent">
					<a href="/Content/images/ispirit/features/08.png" class="imgZoom"><img src="/Content/images/ispirit/features/08_thumb.png" alt="" width="178" height="120" /></a>
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
			</div>
		</div>
		<!--ispirit end-->
	</div>
</div>
</asp:Content>
