<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FullThemeView>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=ViewData.Model.Title %> - Purchasing - iThemeSky Theme Store</title>
<link href="/Content/css/store.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
<style type="text/css">
<!--
.log{ clear:both; width:760px; margin:0 auto;}
.log h4{ font-size:14px; color:#FFF; font-weight:bold; padding:15px 5px;}
.log table,.log table th,.log table td{ border-collapse:collapse; border:1px solid #243E3E;}
.log table th{ padding:8px 10px; text-align:left; font-weight:bold; background:url(/Content/images/store/bg_main.png) no-repeat -1100px -8px; color:#CCD2DC;}
.log table td{ padding:5px 10px; text-align:left;}
.log table tr:hover td{ background:#003338;}
-->
</style>
</head>
<body>
<div id="headerWrapper">
	<div id="header">
		<h2 class="logo"><img src="/Content/images/store/logo.png" alt="iThemeSky.com" /></h2>
		<ul class="step">
			<li><span class="s1">Confirm Order</span></li>
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
	<div class="log">
		<h4><%=ViewData.Model.Title %> Download Log</h4>
	    <table width="100%" cellpadding="0" cellspacing="0" border="0">
			<colgroup>
				<col width="30%" />
				<col width="30%" />
				<col width="40%" />
			</colgroup>
			<thead>
				<tr>
					<th>Download Code</th>
					<th>IP Adress</th>
					<th>Time</th>
				</tr>
			</thead>
			<tbody>
			   <% foreach (ThemeDownloadHistory history in ViewData["History"] as List<ThemeDownloadHistory>){ %>
				<tr>
					<td><%=history.DownloadCode.Substring(0, history.DownloadCode.Length-4) %>****</td>
					<td><%=history.UserIp %></td>
					<td><%=history.AddTime %></td>
				</tr>
				<%} %>
			</tbody>
		</table>
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

