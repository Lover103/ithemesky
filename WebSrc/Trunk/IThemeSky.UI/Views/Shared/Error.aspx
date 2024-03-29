<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="robots" content="noindex" />
<meta name="robots" content="nofollow" />
<title>We're sorry... | ithemesky.com</title>
<style type="text/css">
<!--
/* css reset */
body,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,form,fieldset,input,textarea,p,blockquote,th,td{ margin:0; padding:0;}
h1, h2, h3, h4, h5, h6{ font-weight:normal; font-size:100%;}
address,caption,cite,code,dfn,em,th,var { font-weight:normal; font-style:normal;} 
table{ border-collapse:collapse; border-spacing:0;}
fieldset,img,abbr,acronym{border:0;}
input,textarea,select,button{ font-family:"Lucida Grande", "Lucida Sans Unicode", Verdana, Arial, Helvetica, sans-serif; font-size:12px; color:#769FA3;}
caption,th{ text-align:left;}
q:before, q:after{ content: '';}
ul,ol,dl{ list-style:none;}

.clearfix:after{content:"."; display:block; height:0; clear:both; visibility:hidden;}
.clearfix{display:inline-block;}
/* Hide from IE Mac \*/
.clearfix {display:block;}

body{ background:url(/Content/images/error_bg.jpg) no-repeat center top #000807; font:normal 12px/1.5 "Lucida Grande", "Lucida Sans Unicode", sans-serif; color:#527679;}
a:link,a:visited{ text-decoration:none; color:#527679;}
a:hover,a:active{ color:#FFF;}
#container{ min-width:964px; background:url(/Content/images/error_bg.jpg) no-repeat center top;}
#main{ width:480px; height:300px; margin:0 auto; background:url(/Content/images/error_sorry.png) no-repeat 0 150px; padding:150px 0 0 140px;}
#footer{ padding:30px 192px; width:580px; height:50px; margin:0 auto;}
#main h1{ font-weight:bold; font-size:20px; color:#FFF; padding:15px 0;}
#main p{ font-size:14px; line-height:1.7; color:#9FBCBF;}
#main p a:link,#main p a:visited{ border-bottom:1px dashed #9FBCBF; color:#9FBCBF;}
#main p a:hover,#main p a:active{ border-bottom:1px dashed #FFF; color:#FFF;}
#main .logo{ clear:both; display:block; text-align:right; padding:18px;}
.copyright{ float:left;}
.siteNav{ float:right; white-space:nowrap;}
.siteNav .line{ padding:0 12px;}
-->
</style>
</head>
<body>
<div id="container">
	<div id="main">
		<h1>iThemesky.com, We're sorry...</h1>
		<p>
		    <%=ViewData.Model.Exception.Message %>
		</p>
		<small class="logo"><a href="/"><img src="/Content/images/error_logo.png" alt="ithemesky.com" width="128" height="40" /></a></small>
	</div>
	<div id="footer">
		<span class="copyright">Copyright &copy; 2010 ithemesky.com All rights reserved.</span>
		<p class="siteNav">
			<a href="/">Homepage</a><span class="line">|</span><a href="/contact">Contact us</a>
		</p>
	</div>
</div>
<!--
    <%=ViewData.Model.Exception.ToString() %>
-->
</body>
</html>