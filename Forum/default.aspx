<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" %>
<%@ Register TagPrefix="YAF" Assembly="YAF" Namespace="YAF" %>
<script runat="server">
	public void Page_Error( object sender, System.EventArgs e )
	{
		Exception x = Server.GetLastError();
		YAF.Classes.Data.DB.eventlog_create(YafServices.InitializeDb.Initialized ? (int?)YafContext.Current.PageUserID : null , this, x );
		YAF.Classes.Core.CreateMail.CreateLogEmail( x );
	}		
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"> 
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="YafHead" runat="server">
<meta id="YafMetaScriptingLanguage" http-equiv="Content-Script-Type" runat="server" name="scriptlanguage" content="text/javascript" />
<meta id="YafMetaStyles" http-equiv="Content-Style-Type" runat="server" name="styles" content="text/css" />
<meta id="YafMetaDescription" runat="server" name="description" content="Talking all things about iPhone, and get help for using iSpirit." />
<meta id="YafMetaKeywords" runat="server" name="keywords" content="iSpirit, iPhone theme, iPhone themes, winterboard themes, free iPhone themes, iPhone theme creator, iPhone manager" />
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<style type="text/css">
<!--
body,div{ margin:0; padding:0;}
img{border:0;}
.clearfix:after{content:"."; display:block; height:0; clear:both; visibility:hidden;}
.clearfix{display:inline-block;}
/* Hide from IE Mac \*/
.clearfix {display:block;}
/* End hide from IE Mac */
/*common style*/
body{ background:url(/Content/images/bg_body.jpg) no-repeat center 0 #000807; color:#769FA3;}
#header,#wrapper,#footer{ width:980px; margin:0 auto;}
#headerWrapper{ background:url(/Content/images/bg_header.jpg) no-repeat center top; min-width:980px;}
#footerWrapper{ background:url(/Content/images/bg_footer.png) no-repeat center top; min-width:980px; height:110px;}
#header{ height:100px;}

/*header*/
.logo{ float:left; width:225px; margin-top:8px; font:0/0 Arial;}
.nav{ float:left; height:40px; list-style:none; padding:0; margin:25px 0 0 0;}
.nav li{ float:left; height:24px; padding:10px 11px 6px 9px; margin:0; background:url(/Content/images/sprites_header.png) no-repeat right 0;}
.nav li.last{ background:none;}
.nav a{ display:block; padding:5px; height:14px; text-indent:-999em; overflow:hidden; background:url(/Content/images/sprites_header.png) no-repeat;}
.nav li.s1 a:link,.nav li.s1 a:visited{ width:36px; background-position:0 -45px;}
.nav li.s1 a:hover,.nav li.s1 a:active{ background-position:0 -70px;}
.nav li.s2 a:link,.nav li.s2 a:visited{ width:73px; background-position:-60px -45px;}
.nav li.s2 a:hover,.nav li.s2 a:active{ background-position:-60px -70px;}
.nav li.s3 a:link,.nav li.s3 a:visited{ width:112px; background-position:-160px -45px;}
.nav li.s3 a:hover,.nav li.s3 a:active{ background-position:-160px -70px;}
.nav li.s4 a:link,.nav li.s4 a:visited{ width:38px; background-position:-300px -45px;}
.nav li.s4 a:hover,.nav li.s4 a:active{ background-position:-300px -70px;}
.nav li.s5 a:link,.nav li.s5 a:visited{ width:99px; background-position:-365px -45px;}
.nav li.s5 a:hover,.nav li.s5 a:active{ background-position:-365px -70px;}
.nav li.s6 a:link,.nav li.s6 a:visited{ width:29px; background-position:-495px -45px;}
.nav li.s6 a:hover,.nav li.s6 a:active{ background-position:-495px -70px;}
.nav li.s7 a:link,.nav li.s7 a:visited{ width:39px; background-position:-555px -45px;}
.nav li.s7 a:hover,.nav li.s7 a:active{ background-position:-555px -70px;}
/*footer*/
#footer{ padding-top:35px; color:#3C5C60; font:normal 12px/1.5 "Lucida Grande", "Lucida Sans Unicode", Verdana, Arial, Helvetica, sans-serif;}
#footer a:link,#footer a:visited{ color:#527679; text-decoration:none;}
#footer a:hover,#footer a:active{ color:#769FA3;}
.copyright{ float:left; padding:0 10px; width:180px; margin:0;}
.footNav{ float:right; width:780px; padding:0; margin:0; list-style:none;}
.footNav li{ float:left; padding:0 12px 0 13px; margin:0; background:url(/Content/images/sprites_header.png) no-repeat 0 -484px;}
.footNav li.first{ background:none;}
.footNav li.go2Top{ float:right; padding:0 30px 0 0; background:none; font-size:11px; line-height:18px; overflow:hidden;}
.footNav li.go2Top a{ float:left; display:block; padding-left:18px; height:18px; overflow:hidden; background:url(/Content/images/sprites_header.png) no-repeat -260px -453px;}
.footNav li.go2Top a:hover{ background-position:-260px -473px;}
.disclaimer{ clear:both; width:760px; padding:8px 0 0 212px; font-size:11px; line-height:1.4; margin:0;}
.content #themecredit{ display:none;}
-->
</style>
<!--[if lte IE 6]>
<style type="text/css">  
img,div,input,ul,li,dl,dt,dd,a:link,a:visited,a:hover,a:active,h1,h2,h3,h4,h5,h6,span,p,button,textarea{behavior:url(http://www.ithemesky.com/Content/js/iepngfix.htc);} 
</style>
<script type="text/javascript" src="http://www.ithemesky.com/Content/js/iepngfix_tilebg.js"></script>
<![endif]-->
</head>
<body>
<!--header begin-->
<a name="top"></a>
<div id="headerWrapper">
	<div id="header">
		<h2 class="logo"><a href="/" title="ithemesky.com provided iPhone themes"><img src="/Content/images/logo.png" alt="ithemesky.com provided iPhone themes" width="200" height="62" /></a></h2>
		<ul class="nav">
			<li class="s1"><a href="/" title="ithemesky.com Homepage">ithemesky.com Homepage</a></li>
			<li class="s2"><a href="/list" title="All iPhone Themes">All iPhone Themes</a></li>
			<li class="s3"><a href="/iphone-4-themes" title="iPhone 4 Themes">iPhone 4 Themes</a></li>
			<li class="s4"><a href="/ispirit" title="iSpirit">iSpirit</a></li>
			<li class="s5"><a href="/creator" title="iPhone Theme Creator">iPhone Theme Creator</a></li>
			<li class="s7 last"><a href="/forum" title="Forum">Forum</a></li>
		</ul>
	</div>
</div>
<!--header end-->
<div id="wrapper">
	<form id="form1" runat="server" enctype="multipart/form-data">
		<YAF:Forum runat="server" ID="forum"></YAF:Forum>
	</form>
</div>
<!--footer begin-->
<div id="footerWrapper">
	<div id="footer">
		<p class="copyright">Copyright &copy; 2010 <a href="/">iThemeSky</a></p>
		<ul class="footNav">
			<li class="first"><a href="/list">All Themes</a></li>
			<li><a href="/iphone-4-themes">iPhone 4 Themes</a></li>
			<li><a href="/ispirit">iSpirit</a></li>
			<li><a href="/creator">iPhone Theme Creator</a></li>
			<li><a href="/forum">Forum</a></li>
			<li><a href="/help/tutorials">Tutorials</a></li>
			<li><a href="/contact">Contact Us</a></li>
			<li class="go2Top"><a href="#top" title="Go to top">TOP</a></li>
		</ul>
		<p class="disclaimer">Forum powered by YAF. Other product may be trademarks of their respective owners.</p>
	</div>
</div>
<!--footer end-->
<script type="text/javascript">
$(document).ready(function(){
	  $('a[href*=#top]').click(function() {
		if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'')
		&& location.hostname == this.hostname) {
		  var $target = $(this.hash);
		  $target = $target.length && $target
		  || $('[name=' + this.hash.slice(1) +']');
		  if ($target.length) {
			var targetOffset = $target.offset().top;
			$('html,body')
			.animate({scrollTop: targetOffset}, 700);
		   return false;
		  }
		}
	  });
	});
</script>
</body>
</html>