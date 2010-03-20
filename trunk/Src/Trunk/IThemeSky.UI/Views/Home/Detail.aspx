﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DetailModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<title>iphone themes free download, ithemesky.com provided iphone themes</title>
    <meta name="description" content="ithemesky.com provided iphone themes, more than 2,000 iphone themes free download!" />
    <meta name="keywords" content="iphone theme, iphone themes, jailbroken iphone, install iphone themes, free download, iphone" />
    <link rel="stylesheet" href="/Content/css/fancybox.css" type="text/css" />
    <script type="text/javascript" src="/Content/js/fancybox.js"></script>
    <script type="text/javascript">    jQuery(document).ready(function() { jQuery("img#themeCut").parent().fancybox({}); });</script>
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
				<li class="home"><a href="#" title="Homepage">ithemesky.com Homepage</a></li>
				<li><a href="#">All Categories</a></li>
				<li><a href="#">Cartoons</a></li>
				<li><a href="#">Janpanese sexy girl</a></li>
			</ul>
		</div>
		<!--breadcrumb end-->
		<div id="mainContent">
			<!--theme detail begin-->
			<div class="themeDetail clearfix">
				<div class="detailSide">
					<div class="themeCut"><a href="/<%=ViewData.Model.CurrentTheme.ThumbnailName %>"><img src="/<%=ViewData.Model.CurrentTheme.ThumbnailName %>" width="170" height="255" alt="Japanese girl" id="themeCut" /></a></div>
					<ul class="btn">
						<li class="previous"><a href="/iphone-themes/<%=ViewData.Model.PrevThemeName %>/<%=ViewData.Model.PrevThemeId %>" title="Previous:<%=ViewData.Model.PrevThemeName %>">Previous</a></li>
						<li class="next"><a href="/iphone-themes/<%=ViewData.Model.NextThemeName %>/<%=ViewData.Model.NextThemeId %>" title="Next:<%=ViewData.Model.PrevThemeName %>">Next</a></li>
					</ul>
				</div>
				<div class="detailInfo">
					<h1 class="title"><%=ViewData.Model.CurrentTheme.Title %></h1>
					<dl class="details clearfix">
						<dt>Category:</dt>
						<dd><%=ViewData.Model.CurrentTheme.CategoryName %></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Tags:</dt>
						<dd><a href="#">dock</a>, <a href="#">Icons</a>, <a href="#">psd</a>, <a href="#">status bar</a>, <a href="#">wallpaper</a>, <a href="#">sexy girl</a>, <a href="#">pink</a></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Time:</dt>
						<dd><%=ViewData.Model.CurrentTheme.AddTime.ToString("D", ViewData.Model.USACultureInfo)%></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Views:</dt>
						<dd><%=ViewData.Model.CurrentTheme.Views.ToString("N00") %> views</dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Downloads:</dt>
						<dd><%=ViewData.Model.CurrentTheme.Downloads.ToString("N00") %></dd>
					</dl>
					<dl class="details clearfix"> 
           				<dt>Rate now:</dt>
						<dd class="detailRate">
							<span class="rateResult star4">4/5 stars</span>
							<ul>
								<li><a href="javascript:;" class="rateS1" title="1 Star">1 Star</a></li>
								<li><a href="javascript:;" class="rateS2" title="2 Stars">2 Stars</a></li>
								<li><a href="javascript:;" class="rateS3" title="3 Stars">3 Stars</a></li>
								<li><a href="javascript:;" class="rateS4" title="4 Stars">4 Stars</a></li>
								<li><a href="javascript:;" class="rateS5" title="5 Stars">5 Stars</a></li>
							</ul>
						</dd>
					</dl>
					<div class="btnDownload"><a href="#" title="Download Now">Download Now</a></div>
					<div class="installIip">
						<a href="#"><span class="orange">Tutorial of installing iphone themes!</span></a> Install the theme to iphone with isprite, <a href="#">click here to download isprite</a>. 
					</div>
					<dl class="themeShare">
						<dt>Share this theme:</dt>
						<dd class="shareTwitter"><a href="#" title="Tweet this!">Tweet this!</a></dd>
						<dd class="shareFacebook"><a href="#" title="Share on Facebook.">Share on Facebook.</a></dd>
						<dd class="shareDigg"><a href="#" title="Digg This!">Digg This!</a></dd>
						<dd class="shareStumbleUpon"><a href="#" title="StumbleUpon.">StumbleUpon.</a></dd>
						<dd class="shareReddit"><a href="#" title="Vote on Reddit.">Vote on Reddit.</a></dd>
						<dd class="shareDelicious"><a href="#" title="Bookmark on Delicious.">Bookmark on Delicious.</a></dd>
					</dl>
				</div>
			</div>
			<!--theme detail end-->
			<!--I’m Feeling Lucky begin-->
			<div class="mainCol themeDetailCol">
				<div class="mainColHead">
					<h3 class="colTitle">I’m Feeling Lucky</h3>
				</div>
				<div class="mainColContent themeList clearfix">
					<% Html.RenderPartial("NormalThemeRepeater", ViewData.Model.LuckyThemes); %> 
				</div>
			</div>
			<!--I’m Feeling Lucky end-->
			<!--comment col begin-->
			<div class="themeComment">
				<h3 class="commentHead">22 Comments:</h3>
				<ul class="commentList">
					<!--comment list col begin-->
					<li class="commentContent">
						<div class="commentData">
							<span class="commentNumber">#1</span>
							<cite>Marquee</cite>
							<small class="commentMeta">on April 23th, 2010 at 10:47 am</small>
						</div>
						<div class="commentEntry">
							<p>Wow, thats very inspiring! I definitly want to learn more about webdesign for mobile devices.<br />I was wondering if you have any links with iphone webdesign tips?<br />Great use of space and colors.</p>
						</div>
					</li>
					<!--comment list col end-->
					<!--comment list col begin-->
					<li class="commentContent">
						<div class="commentData">
							<span class="commentNumber">#2</span>
							<cite>Daniele De Nobili</cite>
							<small class="commentMeta">on April 25th, 2010 at 12:36 pm </small>
						</div>
						<div class="commentEntry">
							<p>i’ve just found out another one.</p>
						</div>
					</li>
					<!--comment list col end-->
					<!--comment list col begin-->
					<li class="commentContent">
						<div class="commentData">
							<span class="commentNumber">#3</span>
							<cite>Phaoloo</cite>
							<small class="commentMeta">on May 3th, 2010 at 11:53 pm </small>
						</div>
						<div class="commentEntry">
							<p>Thank u, cool theme.</p>
						</div>
					</li>
					<!--comment list col end-->
					<!--comment list col begin-->
					<li class="commentContent">
						<div class="commentData">
							<span class="commentNumber">#4</span>
							<cite>Rachel</cite>
							<small class="commentMeta">on May 10th, 2010 at 5:21 am </small>
						</div>
						<div class="commentEntry">
							<p>I think i might be the first to actually go through all of these using my iPhone. Took me like 6 Hours lol, but they are all really impressive. Thanks a Lot nick.</p>
						</div>
					</li>
					<!--comment list col end-->
					<!--comment list col begin-->
					<li class="commentContent">
						<div class="commentData">
							<span class="commentNumber">#5</span>
							<cite>Nora Reed</cite>
							<small class="commentMeta">on May 27th, 2010 at 8:49 pm </small>
						</div>
						<div class="commentEntry">
							<p>That airphone site is seriously hacked together… maybe put it on a list of sites with rushed vector work and no attention to css detail.</p>
						</div>
					</li>
					<!--comment list col end-->
				</ul>
				<div class="commentPage clearfix">
					<ul>
						<li><a href="#">Previous</a></li>
						<li><a href="#">5</a></li>
						<li><a href="#">6</a></li>
						<li><a href="#">7</a></li>
						<li class="current">8</li>
						<li><a href="#">9</a></li>
						<li><a href="#">10</a></li>
						<li><a href="#">11</a></li>
						<li><a href="#">12</a></li>
						<li><a href="#">13</a></li>
						<li><a href="#">14</a></li>
						<li><a href="#">Next</a></li>
					</ul>
				</div>
				<div class="commentAdd">
					<form action="">
					<div class="commentForm clearfix">
						<label class="label">Name:</label>
						<input type="text" class="inputNormal" /><!--<input type="text" class="inputFocus" />-->
						<small>(required)</small>
					</div>
					<div class="commentForm clearfix">
						<label class="label">Email:</label>
						<input type="text" class="inputNormal" />
					</div>
					<div class="commentForm clearfix">
						<label class="label">Your Comments:</label>
						<div class="textarea"><textarea class="textareaNormal"></textarea><!--<textarea class="textareaFocus"></textarea>--></div>
					</div>
					<div class="commentBtn"><button type="submit">Add Comment</button></div>
					</form>
				</div>
			</div>
			<!--comment col end-->
		</div>
		<!--theme detail right col begin-->
		<div id="subContent">
			<div class="subCol themeDetailSub">
				<h3 class="subColHead colTitle">Top Download</h3>
				<ul class="subColContent subTopDownload clearfix">
					<li class="champion">
						<dl class="clearfix">
							<dt><a href="#"><img src="images/temp/theme_normal06.jpg" width="32" height="48" alt="Japanese girl" /></a></dt>
							<dd class="title"><a href="#">Japanese girl</a></dd>
							<dd><span class="rateResult star3" title="3/5 stars">3/5 stars</span></dd>
							<dd><small>Downloads:</small><span class="downloadNum">35302</span></dd>
						</dl>
					</li>
					<li class="normal"><span class="rankNum">2</span><a href="#" class="title">Campo di grano</a><span class="downloadNum">2207</span></li>
					<li class="normal"><span class="rankNum">3</span><a href="#" class="title">SeaBound the</a><span class="downloadNum">2156</span></li>
					<li class="normal"><span class="rankNum">4</span><a href="#" class="title">silver bullet</a><span class="downloadNum">2098</span></li>
					<li class="normal"><span class="rankNum">5</span><a href="#" class="title">Sunny Days</a><span class="downloadNum">1980</span></li>
					<li class="normal"><span class="rankNum">6</span><a href="#" class="title">Clear White</a><span class="downloadNum">1742</span></li>
					<li class="normal"><span class="rankNum">7</span><a href="#" class="title">SeaBound the</a><span class="downloadNum">1206</span></li>
					<li class="normal"><span class="rankNum">8</span><a href="#" class="title">silver bullet</a><span class="downloadNum">986</span></li>
					<li class="normal"><span class="rankNum">9</span><a href="#" class="title">Campo di grano</a><span class="downloadNum">821</span></li>
					<li class="normal"><span class="rankNum">10</span><a href="#" class="title">Green White</a><span class="downloadNum">679</span></li>
				</ul>
			</div>
			<div class="subCol themeDetailSub">
				<h3 class="subColHead colTitle">Recommended</h3>
				<ul class="subColContent subRecommended">
					<li><span class="downloadNum">3530</span><a href="#">Japanese girl</a></li>
					<li><span class="downloadNum">2207</span><a href="#">Campo di grano</a></li>
					<li><span class="downloadNum">2156</span><a href="#">SeaBound the</a></li>
					<li><span class="downloadNum">2098</span><a href="#">silver bullet</a></li>
					<li><span class="downloadNum">1980</span><a href="#">Sunny Days</a></li>
					<li><span class="downloadNum">1742</span><a href="#">Clear White</a></li>
					<li><span class="downloadNum">1206</span><a href="#">SeaBound the</a></li>
					<li><span class="downloadNum">986</span><a href="#">silver bullet</a></li>
					<li><span class="downloadNum">821</span><a href="#">Campo di grano</a></li>
					<li><span class="downloadNum">679</span><a href="#">Green White</a></li>
				</ul>
			</div>
		</div>
		<!--theme detail right col end-->
	</div>
</div>
</asp:Content>
