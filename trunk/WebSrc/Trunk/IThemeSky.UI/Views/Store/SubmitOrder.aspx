<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<FullThemeView>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title><%=ViewData.Model.Title %> - Purchasing - iThemeSky Theme Store</title>
<link href="/Content/css/store.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript">
    function PostOrder() {
        var mail = $('#txtMail').val();
        var userName = $('#txtUserName').val();
        if (userName == '') {
            alert('Your Full Name is required.');
            $('#txtUserName').focus();
            return;
        }
        if (mail == '') {
            alert('Your Email Adress is required.');
            $('#txtMail').focus();
            return;
        }
        var themeId = '<%=ViewData.Model.ThemeId %>';
        var url = '/Service/GetPayReturnUrl/' + themeId + ',' + mail + ',' + userName;
        $.get(url, function(data) {
            $('#hidReturnUrl').val('http://www.ithemesky.com/Store/Result/?' + data);
            $('#hidNotifyUrl').val('http://www.ithemesky.com/service/checkipn/' + themeId);
            $('#frmOrder').submit();
        });
    }
    </script>
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
		<li class="clearfix">
			<label class="label">Your Full Name:</label>
			<div class="input"><input id="txtUserName" type="text" class="inputNormal" onblur="this.className='inputNormal';$('#nameTips').hide()" onfocus="this.className='inputFocus';$('#nameTips').show()" /></div>
			<div class="tips" id="nameTips" style="display:none">Please fill in your real name here, it never be published.</div>
		</li>
		<li class="clearfix">
			<label class="label">Your Email Adress:</label>
			<div class="input"><input id="txtMail" type="text" class="inputNormal" onblur="this.className='inputNormal';$('#mailTips').hide()" onfocus="this.className='inputFocus';$('#mailTips').show()" /></div>
			<p class="tips" id="mailTips" style="display:none">It's important, we will send you the order information and a Download Code via Email.</p>
		</li>
		<li class="btnBg">
			<input type="button" class="btnPurchase" onclick="PostOrder()" />
			<div class="tipsPayment"><img src="/Content/images/icon_payment.png" alt="Payments" /></div>
		</li>
	</ul>
	<div style="display:none">
        <form action="https://www.paypal.com/cgi-bin/webscr" method="post" id="frmOrder">
            <input type="hidden" name="cmd" value="_xclick">
            <input type="hidden" name="business" value="DK6YRC494PA4L">
            <input type="hidden" name="item_name" value="<%=ViewData.Model.Title %>">
            <input type="hidden" name="amount" value="<%=ViewData.Model.Price.ToString("0.00") %>">
            <input type="hidden" name="currency_code" value="USD">
            <input type="hidden" name="lc" value="US">
            <input type="hidden" name="return" id="hidReturnUrl" 
            value="">
            <input type="hidden" name="notify_url" id="hidNotifyUrl" value="" />
            <input type="image" src="https://www.paypal.com/en_US/i/btn/x-click-but23.gif" 
            border="0" name="submit" alt="Make payments with PayPal - it's fast, free and secure!">
        </form>
     </div>
	<div class="orderTips clearfix">
		<dl>
			<dt>The Download Code</dt>
			<dd>You will receive a mail includes the Download Code after you purchased a theme. Use the Download Code, you would not be charged again while you downloading a theme you have purchased. If you lose it, please send us the theme's name via the Email address you used when you purchased. We will send you the Download Code within 2 business days.</dd>
		</dl>
		<dl class="sr">
			<dt>Terms and Conditions</dt>
			<dd>All the paid themes are permitted to install for personal and non-profit, non-commercial use. All themes are protected by copyright. It is not allowed to post the paid theme's copy or Download Code on other third websites.</dd>
		</dl>
		<dl>
			<dt>Payments by PayPal</dt>
			<dd>PayPal provides an easy and secure checkout for your purchases. You do not need to have an account to purchase, and all major credit cards are accepted.<br /><img src="/Content/images/icon_payment.png" alt="Payments" style="margin-top:8px;" /></dd>
		</dl>
		<dl class="sr">
			<dt>Install Theme</dt>
			<dd>We have checked that every paid theme can be installed on iPhone successfully before selling. If you don't know how to install a theme, please read the <a href="/help/tutorials#faqContent1">tutorial</a>. If you find something wrong with the theme, please contact us.</dd>
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

