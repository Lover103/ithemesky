<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>Help - iSpirit -iPhone themes and files manager - ithemesky.com</title>
<meta name="description" content="iSpirit is specially designed for iPhone, used on your PC for file and theme mangement. ithemesky.com provided iPhone themes, more than 2,000 iPhone themes free download!" />
<meta name="keywords" content="iSpirit, iSpirit help, iPhone theme, iPhone themes, jailbroken iPhone, install iPhone themes, free download, iPhone, WinterBoard, jailbreak" />
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
			<div class="iSpirit">
				<dl>
					<dt><img src="/Content/images/ispirit_title.png" width="345" height="72" alt="iSpirit - iPhone themes and files manager" /></dt>
					<dd>
						<p>iSpirit is specially designed for iPhone, used on your PC for iPhone files and themes mangement. With iSpirit, you would install theme on your iPhone, and manage the files in your iphone easily. You would free download iPhone themes from iSpirit. It works with iPhone, iPhone 3G, iPhone 3Gs in Firmware1.x, 2.x and 3.x.</p>
						<p style="margin-top:5px;"><strong>System Requirement:</strong></p>
						<p class="req">
							<strong>&nbsp;&nbsp;&nbsp;OS:</strong> Windows XP / Windows Vista / Windows 7<br />
							<strong>Base:</strong> <a href="http://appldnld.apple.com.edgesuite.net/content.info.apple.com/iTunes9/061-7204.20100330.Cdr4T/iTunesSetup.exe" target="_blank" class="linkEct" rel="nofollow" title="Click here to download iTunes 9.1">iTunes 7.5 or higher</a> + <a href="http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en" target="_blank" class="linkEct" rel="nofollow" title="Click here to download .net Framework 2.0">.net Framework 2.0/3.0/3.5/4.0</a>
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
					<li class="s2"><a href="/ispirit" title="Features - Learn more about iSpirit"><span class="link">Features - Learn more about iSpirit</span></a></li>
					<li class="s3"><a href="/ispirit/help" class="current" title="Get Help - FAQs and Support"><span class="link">Get Help - FAQs and Support</span></a></li>
				</ul>
			</div>
			<div class="iSpiritMain">
				<h3 class="supportSort s1">Frequently  Asked  Questions</h3>
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
				<h4 class="iSpiritFaq"><a href="#">How to install the .deb software via iSpirit?</a></h4>
				<div class="iSpiritFaqContent">
					<p>
						1. Run the iSpirit, and open the favorite folder and choose the "Cydia AutoInstall". (or search for /private/var/root/Media/Cydia/AutoInstall/ in the address)<br />
						2. Copy the .deb file to this folder.<br />
						3. Restart your iPhone.<br />
						4. Then restart the Springboard via iSprite.<br />
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
				<div class="iSpiritForm">
					<p>If you have an issue or a specific question regarding iSpirit or you want to give a bug report, please contact us. You can use the form below or email us directly at ithemesky@gmail.com. If you use the form please make sure there isn’t a typo in your email address. Please give us a few business days to respond!<br /><br /></p>
					<iframe id="hiddenIframe" name="hiddenIframe" src="#" style="display:none"></iframe>
                    <% using (Html.BeginForm("AddThemeSupport", "Service", FormMethod.Post, new { target = "hiddenIframe" }))
                       { %>
                       <%= Html.Hidden("SupportType", 2) %>
					<div class="submitForm clearfix">
						<label class="label">Name:</label>
						<%= Html.TextBox("Name", "", new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
						<small>(optional)</small>
					</div>
					<div class="submitForm clearfix">
						<label class="label">Email:</label>
						<%= Html.TextBox("Mail", "", new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
						<small>(required)</small>
					</div>
					<div class="submitForm clearfix">
						<label class="label">Subject:</label>
						<%= Html.TextBox("Subject", "", new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
						<small>(required)</small>
					</div>
					<div class="submitForm clearfix">
						<label class="label">description:</label>
						<div class="textarea">
						<%= Html.TextArea("Description", "", new { Class = "textareaNormal", onfocus = "this.className='textareaFocus'", onblur = "this.className='textareaNormal'" })%>
						</div>
					</div>
					<div class="submitBtn"><button type="submit">Submit</button></div>
					 <% } %>
				</div>
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
function SubmitSupportSuccess() {
    $('#Description,#Subject').val('');
    alert('Submit success!');
}
</script>
</asp:Content>
