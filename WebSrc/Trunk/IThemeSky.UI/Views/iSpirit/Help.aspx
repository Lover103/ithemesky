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
				<h4 class="iSpiritFaq"><a href="#">How to install the theme on jailbroken iPhone?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. If you have wifi, you could install the winterboard via cydia directly.<br />
						2. If no wifi there, will install automatically when you use the iSpirit, please step as instructed.<br />
						3. Download and install iSpirit.<br />
						4. In the theme management, you could install the theme from Zip directly or install from folder.<br />
						5. If you need more theme, you could download from http://www.ithemesky.com<br />
						6. And also you could download from the website indicated in the iSpirit, iSpirit will help you install the theme automatically.
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to install the .deb software with iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Run the iSpirit, and open the favorite folder and choose the "Cydia AutoInstall". (or search for /private/var/root/Media/Cydia/AutoInstall/ in the address)<br />
						2. Copy the .deb file to this folder.<br />
						3. Restart your iPhone.<br />
						4. Then restart the Springboard with iSpirit.<br />
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to view and edit the .plist file?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Download and install the iSpirit<br />
						2. Choose the .plist file you want to view via iSpirit, double click it.<br />
						3. You could edit and save the file on the text editor lanuched.<br />						
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">How to manage your file in iPhone?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Download and install iSpirit.<br />
						2. With the help of iSpirit, it is easy for you to establish, amend or delete the folder. upload file or folder to your ipone. copy file or folder to PC.<br />
						3. iSpirit support you to drag the file and folder which is useful for to you exchange the file between pc and your iPhone.<br />						
					</p>
				</div>
				<h4 class="iSpiritFaq"><a href="#">When to use the Respring of iSpirit</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						You can use Respring On this condition as below:<br />
						1. To restart the springboard.<br />
						2. Installing certain soft, but no appearance on the screen.<br />
						3. Updated the configuration files and want to make it work at once.<br />						
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
