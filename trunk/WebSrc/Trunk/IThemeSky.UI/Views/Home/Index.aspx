<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <title>iPhone Themes free download, ithemesky.com provided iPhone Themes</title>
    <meta name="description" content="ithemesky.com provided iPhone themes, more than 2,000 iPhone themes free download!" />
    <meta name="keywords" content="iSpirit, iPhone theme, iPhone themes, jailbroken iPhone, install iPhone themes, free download, iPhone, WinterBoard, jailbreak" />
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
	    <div id="mainContent">
		    <!--last update begin-->
		    <div class="mainCol">
			    <div class="mainColHead">
				    <h3 class="colTitle">Last Update</h3>
				    <span class="colMore"><a href="/list/new">MORE</a></span>
			    </div>
			    <div class="mainColContent themeList clearfix">
				    <% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.LastUpdateThemes); %>
			    </div>
		    </div>
		    <!--last update end-->
		    <!--Recommended begin-->
		    <div class="mainCol">
			    <div class="mainColHead">
				    <h3 class="colTitle">Recommended</h3>
			    </div>
			    <div class="mainColContent themeList clearfix">
				    <% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.RecommendedThemes); %>
			    </div>
		    </div>
		    <!--Recommended end-->
		    <!--Most Popular begin-->
		    <div class="mainCol">
			    <div class="mainColHead">
				    <h3 class="colTitle">Most Popular</h3>
				    <ul class="mainColTab">
					    <li><a href="javascript:;" sort="Rank_Of_Week">Week</a></li>
					    <li><a href="javascript:;" sort="Rank_Of_Month">Month</a></li>
					    <li class="selected"><a href="javascript:;" sort="Popular">Total</a></li>
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
				    <li><a href="/ispirit"><span class="orange">How to install themes on jailbroken iPhone?</span></a></li>
				    <li><a href="/help/how-to-use-winterboard">How to Use WinterBoard?</a></li>
				    <li><a href="/help/why-jailbreak-iphone">Why jailbreak iPhone?</a></li>
				    <li><a href="/help/how-to-jailbreak-iphone">How to jailbreak iPhone?</a></li>
					<li><a href="/help/faq">Get more help in FAQ.</a></li>
			    </ul>
		    </div>
		    <!--help end-->
		    <!--I’m Feeling Lucky begin-->
		    <div class="subCol">
			    <h3 class="subColHead colTitle">I’m Feeling Lucky</h3>
			    <div class="subColContent themeList">
				    <% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.LuckyThemes); %>
			    </div>
		    </div>
		    <!--I’m Feeling Lucky end-->
	    </div>
    </div>
</asp:Content>
