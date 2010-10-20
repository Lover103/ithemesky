<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% if (!(Request.Cookies["ComeFromISpirit"] != null && Request.Cookies["ComeFromISpirit"].Value.Equals("true"))) { %>
		<div class="sideCol gsenseSide">
			<h3 class="sideColHead colTitle">Sponsors</h3>
			<p>
<script type="text/javascript">
<!--
google_ad_client = "pub-6585346932275782";
/* side link */
google_ad_slot = "3706832885";
google_ad_width = 180;
google_ad_height = 90;
//-->
</script>
<script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
			</p>
		</div>
<%} %>