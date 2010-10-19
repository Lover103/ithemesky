<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <title>Free iPhone Themes, Best iPhone Themes, Best Winterboard Themes Download.</title>
    <meta name="description" content="<!-- #include file="/Views/Inc/siteDescription.inc" -->" />
    <meta name="keywords" content="<!-- #include file="/Views/Inc/siteKeyword.inc" -->" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper" class="clearfix">
	    <div id="sidebar">
		    <!--Categories begin-->
		    <% Html.RenderPartial("ThemeCategories", ViewData.Model.ThemeCategories); %>
		    <!--Categories end-->
			<% if (!ViewData.Model.ComeFromISpirit){ %>
			<% Html.RenderPartial("gSenseSidebar", ViewData.Model.ThemeCategories); %>
			<%} %>
		    <!--Tags begin-->
		    <% Html.RenderPartial("HotTags"); %>
		    <!--Tags end-->
	    </div>
	    <div id="mainContent">
			<div class="banner">
				<dl>
					<dt><img src="/Content/images/banner01.jpg" title="" alt="iSpirit - iPhone apps, themes and files manager" /></dt>
					<dd class="text">One-click install DEB/IPA file. Download and install iPhone theme. Manage files between PC and iPhone. Fully supports iPhone 4.<p>One-click install DEB/IPA file. Download and install iPhone theme. Manage files between PC and iPhone. Fully supports <span style="color:#FF9;">iPhone 4</span>.</p></dd>
					<dd class="btn">
						<a href="/ispirit/download" class="s1">Download</a>
						<a href="/ispirit" class="s2">Learn More</a>
					</dd>
				</dl>
			</div>
		    <!--last update begin-->
		    <div class="mainCol">
			    <div class="mainColHead">
				    <h3 class="colTitle">Latest Updates</h3>
				    <span class="colMore"><a href="/list/new">MORE</a></span>
			    </div>
			    <div class="mainColContent themeList clearfix">
				    <% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.LastUpdateThemes); %>
			    </div>
		    </div>
		    <!--last update end-->
		    <!--Most Popular begin-->
		    <div class="mainCol">
			    <div class="mainColHead">
				    <h3 class="colTitle">Most Popular</h3>
				    <ul class="mainColTab">
					    <li class="selected"><a href="javascript:;" sort="Rank_Of_Week">Week</a></li>
					    <li><a href="javascript:;" sort="Rank_Of_Month">Month</a></li>
					    <li><a href="javascript:;" sort="Popular">Total</a></li>
				    </ul>
				    <script type="text/javascript">BindSortEvent();</script>
			    </div>
			    <div id="divSortThemes" class="mainColContent themeList clearfix">
				    <% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.PopularThemes); %>
			    </div>
		    </div>
		    <!--Most Popular end-->
	    </div>
	    <div id="subContent">
		    <!--help begin-->
		    <div class="subCol">
			    <h3 class="subColHead colTitle">Help</h3>
			    <ul class="subColContent subHelp">
				    <li><a href="/ispirit/help"><span class="orange">How to install theme on jailbroken iPhone?</span></a></li>
				    <li><a href="/help/how-to-use-winterboard">How to Use WinterBoard?</a></li>
					<li><a href="/help/why-do-winterboard-themes-not-work">Why do winterboard themes not work?</a></li>
				    <li><a href="/help/how-to-jailbreak-iphone">How to jailbreak iPhone?</a></li>
					<li><a href="/help/faq">Get more help in FAQ.</a></li>
			    </ul>
		    </div>
		    <!--help end-->
			<div class="quickContact">
				<ul>
					<li class="s1"><a href="http://www.twitter.com/iThemesky" title="Follow us on Twitter">Follow us on Twitter</a></li>
					<li class="s2"><a href="http://www.facebook.com/profile.php?id=100001719707699" title="Find us on Facebook">Find us on Facebook</a></li>
					<li class="s3"><a href="/contact" title="Contact us">Contact us</a></li>
					<li class="s4">
						<form name="_xclick" action="https://www.paypal.com/cgi-bin/webscr" method="post" />
							<input type="hidden" name="cmd" value="_xclick" />
							<input type="hidden" name="business" value="ithemesky@gmail.com" />
							<input type="hidden" name="item_name" value="Donate to iThemeSky.com" />
							<input type="hidden" name="currency_code" value="USD" />
							<input type="hidden" name="amount" value="" />
							<input type="image" src="/Content/images/btn_donate.png" title="Donate to iThemeSky" border="0" name="submit" alt="Donate to iThemeSky" />
						</form>
					</li>
				</ul>
			</div>
		    <!--I'm Feeling Lucky begin-->
		    <div class="subCol">
			    <h3 class="subColHead colTitle">I'm Feeling Lucky</h3>
			    <div class="subColContent themeList">
				    <% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.LuckyThemes); %>
			    </div>
		    </div>
		    <!--I'm Feeling Lucky end-->
	    </div>
    </div>
</asp:Content>