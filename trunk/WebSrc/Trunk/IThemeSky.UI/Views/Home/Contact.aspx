<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title>Contact Us - iThemeSky.com</title>
<meta name="robots" content="noindex" />
<meta name="robots" content="nofollow" />
<meta name="description" content="Contact Us, <!-- #include file="/Views/Inc/siteDescription.inc" -->" />
<meta name="keywords" content="<!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
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
				<li>Contact Us</li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<!--ispirit begin-->
		<div class="fullContent">
			<style type="text/css">
			<!--
			.iSpiritMain p{ padding:8px 5px;}
			.iSpiritMain p a:link,.iSpiritMain p a:visited{ color:#9FBABF;}
			.iSpiritMain p a:hover,.iSpiritMain p a:active{ text-decoration:underline;}
			-->
			</style>
			<div class="iSpiritMain">
				<p>We appreciate your feedback. All suggestions, comments, talking about cooperation/advertisment/link exchange, iSpirit bug reports, praise or harsh criticism are welcome. If something's bugging you then we'll do what we can to help. We'll try to reply as soon as possible.</p>
				<p>Don't forget to check our <a href="/ispirit/help">iSpirit FAQ's documents</a> before sending us a message about iSpirit as there's a good chance we already have an answer to your question. And the quickest way to get iSpirit help is by <a href="/forum">visiting our Community Forum.</a></p>
				<p>Follow us on Twitter: <a href="http://www.twitter.com/iThemesky" target="_blank">www.twitter.com/ithemesky</a><br />
				Add us to MSN: ithemesky@gmail.com (Not a Live Support)</p>
				<div class="iSpiritForm">
					<iframe id="hiddenIframe" name="hiddenIframe" src="#" style="display:none"></iframe>
                    <% using (Html.BeginForm("AddThemeSupport", "Service", FormMethod.Post, new { target = "hiddenIframe" }))
                       { %>
                       <%= Html.Hidden("SupportType", 2) %>
					<div class="submitForm clearfix">
						<label class="label">Your Name:</label>
						<%= Html.TextBox("Name", "", new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
						<small>(Optional)</small>
					</div>
					<div class="submitForm clearfix">
						<label class="label">Your Email:</label>
						<%= Html.TextBox("Mail", "", new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
						<small>(Required but never published)</small>
					</div>
					<div class="submitForm clearfix">
						<label class="label">Subject:</label>
						<%= Html.TextBox("Subject", "", new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
						<small>(Required)</small>
					</div>
					<div class="submitForm clearfix">
						<label class="label">Message:</label>
						<div class="textarea">
						<%= Html.TextArea("Description", "", new { Class = "textareaNormal", onfocus = "this.className='textareaFocus'", onblur = "this.className='textareaNormal'" })%>
						</div>
					</div>
					<div class="submitBtn"><button type="submit">Submit</button></div>
					 <% } %>
				</div>
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
<script type="text/javascript">
function SubmitSupportSuccess() {
    $('#Description,#Subject').val('');
    alert('Submit success!');
}
</script>
</asp:Content>