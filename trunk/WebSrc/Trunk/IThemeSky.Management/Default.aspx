<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IThemeSky.Management._Default" %>
<%@ Register Assembly="IThemeSky.WebControls" Namespace="IThemeSky.WebControls" TagPrefix="JT" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="heigth:35px; font-weight:bold">
        <a href="Theme/ThemeListManage.aspx" target="hideIframe">&gt;主题管理</a>&nbsp;
        <a href="Theme/ThemeInfoManage.aspx" target="hideIframe">&gt;添加新主题</a>&nbsp;
        <a href="Reports/iSpiritUsersReport.aspx" target="hideIframe">&gt;iSpirit用户报表</a>&nbsp;
        <a href="Support/UserSupport.aspx" target="hideIframe">&gt;iSpirit用户反馈</a>&nbsp;
    </div>
    <iframe width="100%" height="100%" name="hideIframe" scrolling="no" frameborder="0" src="Theme/ThemeListManage.aspx" onload="this.height=this.contentWindow.document.body.scrollHeight+30">
    
    </iframe>
    </form>
</body>
</html>
