<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>iSpirit Help - iPhone Manager, manage iPhone apps, iPhone themes and files easily.</title>
<meta name="description" content="iSpirit is a iPhone Manager which help you manage files between PC and iPhone, install iPhone apps and change iPhone themes easily." />
<meta name="keywords" content="iSpirit help, iSpirit download, <!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
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
				<li><a href="/ispirit">iSpirit</a></li>
				<li>Help</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<!--ispirit begin-->
		<div class="fullContent">
			<% Html.RenderPartial("iSpirit"); %>
			<div class="iSpiritNav">
				<ul>
					<li class="s1"><% Html.RenderPartial("iSpiritDownload"); %></li>
					<li class="s2"><a href="/ispirit" title="Features - Learn more about iSpirit"><span class="link">Features - Learn more about iSpirit</span></a></li>
					<li class="s3"><a href="/ispirit/help" class="current" title="Get Help - iSpirit FAQs and Support"><span class="link">Get Help - iSpirit FAQs and Support</span></a></li>
				</ul>
			</div>
			<div class="iSpiritMain">
				<h3 class="supportSort s1">Frequently Asked Questions</h3>
				<h4 class="iSpiritFaq"><a href="#">What is the system requirement for iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						iSpirit is only for Windows (Windows XP/ Windows Vista/ Windows 7). Please install both .net Framework 2.0 or higher and iTunes 7.5 or higher before you use iSpirit. The .net Framework 4.0 and iTunes 9.1 are been recommended.
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to install iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						iSpirit is a Free software installation (green software). Decompress the iSpirit  compressed package to your disk, then click it to run. Run iSpirit as Administrator is recommended if you use it in Windows 7.
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to connect iPhone to computer?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Plug in your iPhone to your computer using the included universal serial bus (USB) cable.<br />
						2. You must install iTunes to your computer first, and turn iPodService on.<br />
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to install themes to iPhone with iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Switch on Theme Manage (Menu in Spirit)<br />
						2. Click "Install from Zip" or "Install from folder" to select a theme from your disk then install it. Or find a theme on our site in iSpirit, click "Download" then click the "Apply" to install it.					
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to install DEB or IPA files?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Click the button "Install DEB" or "Install IPA".<br />
						2. Select the DEB or IPA file.<br />
						3. Then click "Install" to finish installation.				
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">Why iSpirit crashes sometimes?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Did not install .net framework would result in iSpirit crashes.<br />
						2. If you don't know why it crashes, send us your iTunes and Windows OS version to us, We are glad to help you.			
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to fix white/blank icons in my iPhone?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						Click "Repair Icons" in iSpirit to fix them directly. Learn more about <a href="/help/why-do-winterboard-themes-not-work">Why do winterboard themes not work?</a>.			
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to change iPhone files permissions with iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						Switch to File Manage, select a file, right click it then select Propertys, you can change the permission in the next interface.			
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">Why I can't browse all the iPhone files in iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						Maybe you didn't install afc2add. <a href="/forum/default.aspx?g=posts&t=6">Learn more about afc2add here</a>.		
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">Why it always remind me that Openssh did not install correctly?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. If you use iSpirit 1.0.6 or former version in Windows 7, you should use Compatibility Mode for it.<br />
						2. Your computer's 22 Port is used by another program may results in that.			
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to delete a Winterboard theme?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Switch on Theme Manage.<br />
						2. Click "Themes in iPhone"<br />
						3. Select a theme then delete it directly.		
					</p>
				</div>
				<h3 class="supportSort s2">Get Support</h3>
				<ul class="supportBtn clearfix">
					<li class="s1"><span>Send us suggestions, bug reports, talk <br />about cooperation or advertisment.</span><a href="/contact" rel="nofollow">Contact Us Now</a></li>
					<li class="s2"><span>The quickest way to get help is by <br />visiting our Community Forum.</span><a href="/forum">Visit Community Forum</a></li>
				</ul>
			</div>
		</div>
		<!--ispirit end-->
	</div>
</div>
<script type="text/javascript">
$(document).ready(function(){
	
	$(".iSpiritFaqContent").hide();

	$("h4.iSpiritFaq").toggle(function(){
		$(this).addClass("active"); 
		}, function () {
		$(this).removeClass("active");
	});
	
	$("h4.iSpiritFaq").click(function(){
		$(this).next(".iSpiritFaqContent").slideToggle("slow,");
	});

});
</script>
</asp:Content>
