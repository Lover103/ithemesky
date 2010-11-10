<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FullThemeView>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=ViewData.Model.Title %> - Purchasing - iThemeSky Theme Store</title>
<link href="/Content/css/store.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
</head>
<body>
<div id="headerWrapper">
	<div id="header">
		<h2 class="logo"><img src="/Content/images/store/logo.png" alt="iThemeSky.com" /></h2>
		<ul class="step">
			<li class="current"><span class="s1">Confirm Order</span></li>
			<li><span class="s2">Make Payment</span></li>
			<li class="last"><span class="s3">Return to Download</span></li>
		</ul>
	</div>
</div>
<div id="mainHeader"><h1 class="title">Theme Store</h1></div>
<div id="main">
	<div class="themeCut"><img src="<%=ViewData.Model.ThumbnailName %>" width="100" height="150" alt="<%=ViewData.Model.Title %>" /></div>
	<ul class="orderForm">
		<li class="clearfix">
			<label class="label">Theme Name:</label>
			<div class="input"><a href="<%=ViewData.Model.ThemeDetailUrl %>"><%=ViewData.Model.Title %></a></div>
		</li>
		<li class="clearfix">
			<label class="label">Theme's Author:</label>
			<div class="input"><%=ViewData.Model.AuthorName %></div>
		</li>
		<li class="clearfix price">
            <label class="label"><span>Price:</span></label>
			<div class="input">$<%=ViewData.Model.Price.ToString("0.00") %></div>
		</li>
	</ul>
	<div class="orderTips clearfix">
	    <ul>
	   <% foreach (ThemeDownloadHistory history in ViewData["History"] as List<ThemeDownloadHistory>){ %>
		    <li><%=history.UserIp %>&nbsp:&nbsp:&nbsp:<%=history.AddTime %>&nbsp:&nbsp:&nbsp:<%=history.DownloadCode.Substring(0, history.DownloadCode.Length-4) %>****</li>
		<%} %>
		</ul>
	</div>
</div>
<div id="footer">
	<ul>
		<li class="copyright">Copyright &copy; 2010 iThemeSky&nbsp;&nbsp;|&nbsp;&nbsp; All iPhone Themes are copyrighted by their respective authors.</li>
		<li class="siteNav"><a href="/contact">Contact Us</a></li>
	</ul>
</div>
</body>
</html>

