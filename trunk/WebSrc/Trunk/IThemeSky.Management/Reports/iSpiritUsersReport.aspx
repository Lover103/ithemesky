<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iSpiritUsersReport.aspx.cs" Inherits="IThemeSky.Management.Reports.iSpiritUsersReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="JavaScript" src="../Resources/Js/FusionCharts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        报表表现形式：
        <input type="radio" checked="checked" value="1" name="reportType" id="d" onclick="ShowReport('MSLine')" /><label for="d">曲线图</label>
        <input type="radio" value="1" name="reportType" id="a" onclick="ShowReport('MSColumn2D')" /><label for="a">柱状图</label>
        <input type="radio" value="1" name="reportType" id="b" onclick="ShowReport('MSArea')" /><label for="b">区域图</label>
        <input type="radio" value="1" name="reportType" id="c" onclick="ShowReport('MSColumn3D')" /><label for="c">3D柱状</label>
    </div>
    <div id="divChart">
        
    </div>
    <script type="text/javascript">
        function ShowReport(type) {
            var chart = new FusionCharts("../Resources/Charts/" + type + ".swf", "ispritChart", "900", "350", "0", "0");
            chart.setDataURL("ReportData.ashx<%=Request.Url.Query %>");
            chart.render("divChart");
        }
        ShowReport('MSLine');
    </script>
    </form>
</body>
</html>
