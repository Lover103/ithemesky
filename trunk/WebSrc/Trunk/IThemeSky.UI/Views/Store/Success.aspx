<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<PayResultModel>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Purchase Success - iThemeSky Theme Store</title>
<link href="/Content/css/store.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="headerWrapper">
	<div id="header">
		<h2 class="logo"><img src="/Content/images/store/logo.png" alt="iThemeSky.com" /></h2>
		<ul class="step">
			<li class="done"><span class="s1">Confirm Order</span></li>
			<li class="done"><span class="s2">Make Payment</span></li>
			<li class="last current"><span class="s3">Return to Download</span></li>
		</ul>
	</div>
</div>
<div id="mainHeader"><h1 class="title">Theme Store</h1></div>
<div id="main">
	<div class="result clearfix">
		<p class="icon success">Result</p>
		<dl class="resultInfo">
			<dt class="titleSuccess">Purchase Success</dt>
			<dd class="list">Paypal Order Number: <%=ViewData.Model.txn_id %></dd>
			<dd class="list">Theme Name: <a href="<%=ViewData.Model.Theme.ThemeDetailUrl %>"><%=ViewData.Model.Theme.Title %></a></dd>
			<dd class="list">Download Code: <span class="downloadCode"><%=ViewData.Model.txn_id %></span></dd>
			<dd class="note">We have sent a mail to your <%=ViewData.Model.UserMail %>. You can find the order information and Download Code in that mail. If you lose it, please send the name of the theme you purchased to us via your Email adress <%=ViewData.Model.UserMail %>. We will send you the Download Code within 2 business days.</dd>
			<dd class="btnDownload">
				<a href="/Service/Download/<%=ViewData.Model.Theme.ThemeId %>,<%=ViewData.Model.Theme.Title %>,<%=ViewData.Model.txn_id %>" title="Download Now">Download</a>
			</dd>
		</dl>
	</div>
</div>
<div id="footer">
	<ul>
		<li class="copyright">Copyright &copy; 2010 iThemeSky&nbsp;&nbsp;|&nbsp;&nbsp; All iPhone Themes are copyrighted by their respective authors.</li>
		<li class="siteNav"><a href="#">Contact Us</a></li>
	</ul>
</div>
</body>
</html>
