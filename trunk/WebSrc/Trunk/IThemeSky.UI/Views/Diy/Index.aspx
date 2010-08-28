<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NormalPageModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<title><%=ViewData["ThemeName"].ToString().Replace("-", " ") %> iPhone theme DIY - iPhone Themes, Winterboard Themes Creator Online</title>
<meta name="description" content="<%=ViewData["ThemeName"].ToString().Replace("-", " ") %> iPhone theme DIY. Create Online your iPhone theme." />
<meta name="keywords" content="<%=ViewData["ThemeName"].ToString().Replace("-", " ") %> iPhone theme DIY, iPhone theme Maker, iPhone theme Editor, iPhone theme Creator, iPhone theme Generator, Create iPhone theme, Modify iPhone theme" />
<!--  BEGIN Browser History required section -->
<link rel="stylesheet" type="text/css" href="/Content/Diy/history/history.css" />
<!--  END Browser History required section -->
<script src="/Content/Diy/AC_OETags.js" language="javascript"></script>
<!--  BEGIN Browser History required section -->
<script src="/Content/Diy/history/history.js" language="javascript"></script>
<!--  END Browser History required section -->
<script language="JavaScript" type="text/javascript">
<!--
// -----------------------------------------------------------------------------
// Globals
// Major version of Flash required
var requiredMajorVersion = 10;
// Minor version of Flash required
var requiredMinorVersion = 0;
// Minor version of Flash required
var requiredRevision = 0;
// -----------------------------------------------------------------------------
// -->
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="wrapper" style="width:860px;">
<script language="JavaScript" type="text/javascript">
<!--
// Version check for the Flash Player that has the ability to start Player Product Install (6.0r65)
var hasProductInstall = DetectFlashVer(6, 0, 65);

// Version check based upon the values defined in globals
var hasRequestedVersion = DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision);

if ( hasProductInstall && !hasRequestedVersion ) {
	// DO NOT MODIFY THE FOLLOWING FOUR LINES
	// Location visited after installation is complete if installation is required
	var MMPlayerType = (isIE == true) ? "ActiveX" : "PlugIn";
	var MMredirectURL = window.location;
    document.title = document.title.slice(0, 47) + " - Flash Player Installation";
    var MMdoctitle = document.title;

	AC_FL_RunContent(
		"src", "playerProductInstall",
		"FlashVars", "MMredirectURL="+MMredirectURL+'&MMplayerType='+MMPlayerType+'&MMdoctitle='+MMdoctitle+"&ThemePath=aaa",
		"width", "100%",
		"height", "817",
		"align", "middle",
		"id", "ThemeEditor",
		"quality", "high",
		"wmode", "transparent",
		"name", "ThemeEditor",
		"allowScriptAccess","sameDomain",
		"type", "application/x-shockwave-flash",
		"pluginspage", "http://www.adobe.com/go/getflashplayer"
	);
} else if (hasRequestedVersion) {
	// if we've detected an acceptable version
	// embed the Flash Content SWF when all tests are passed
	AC_FL_RunContent(
			"src", "/Content/diy/ThemeEditor",
			"FlashVars", "ThemeName=<%=ViewData["ThemeName"] %>_DIY&ThemePath=/service/ParseThemeZip/<%=ViewData["ThemeId"] %>",
			"width", "100%",
			"height", "817",
			"align", "middle",
			"id", "ThemeEditor",
			"quality", "high",
			"wmode", "transparent",
			"name", "ThemeEditor",
			"allowScriptAccess","sameDomain",
			"type", "application/x-shockwave-flash",
			"pluginspage", "http://www.adobe.com/go/getflashplayer"
	);
  } else {  // flash is too old or we can't detect the plugin
    var alternateContent = 'Alternate HTML content should be placed here. '
  	+ 'This content requires the Adobe Flash Player. '
   	+ '<a href=http://www.adobe.com/go/getflash/>Get Flash</a>';
    document.write(alternateContent);  // insert non-flash content
  }
// -->
</script>
</div>
</asp:Content>