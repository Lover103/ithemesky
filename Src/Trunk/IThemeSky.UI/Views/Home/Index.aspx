<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <title>iphone themes free download, ithemesky.com provided iphone themes</title>
    <meta name="description" content="ithemesky.com provided iphone themes, more than 2,000 iphone themes free download!" />
    <meta name="keywords" content="iphone theme, iphone themes, jailbroken iphone, install iphone themes, free download, iphone" />
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
				    <li><a href="#"><span class="orange">How to install themes on jailbroken iPhone?</span></a></li>
				    <li><a href="#">How to design an iphone theme?</a></li>
				    <li><a href="#">isprite can not be found?</a></li>
				    <li><a href="#">My iphone is not jailbroken.</a></li>
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
