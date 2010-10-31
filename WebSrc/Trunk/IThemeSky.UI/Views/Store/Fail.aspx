<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PayResultModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Payment Failed - iThemeSky Theme Store</title>
<link href="/Content/css/store.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="headerWrapper">
	<div id="header">
		<h2 class="logo"><img src="/Content/images/store/logo.png" alt="iThemeSky.com" /></h2>
		<ul class="step">
			<li class="done"><span class="s1">Confirm Order</span></li>
			<li><span class="s2">Make Payment</span></li>
			<li class="last current"><span class="s3">Return to Download</span></li>
		</ul>
	</div>
</div>
<div id="mainHeader"><h1 class="title">Theme Store</h1></div>
<div id="main">
	<div class="result clearfix">
		<p class="icon fail">Result</p>
		<dl class="resultInfo">
			<dt class="titleFail">Payment Failed</dt>
			<dd class="list">Sorry, your payment has failed, please <a href="/Store/SubmitOrder/<%=ViewData.Model.Theme.ThemeId %>">confirm order and make payment again</a>.</dd>
			<dd class="note">If you have been charged, but the failure page exists, please send a mail to us. The following information must be included in your mail: <br />1. Your Email address which used in Paypal;<br />2. The name or the URL of the theme you purchased.<br />We will reply you within 2 business days.</dd>
			<dd class="note">
			    <%=ViewData.Model.Description %>
			</dd>
		</dl>
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
