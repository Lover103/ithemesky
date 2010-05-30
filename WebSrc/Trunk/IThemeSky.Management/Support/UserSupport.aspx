<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSupport.aspx.cs" Inherits="IThemeSky.Management.Support.UserSupport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%" cellpadding="6" cellspacing="1" class="InputGrid">
          <thead>
            <tr>
              <th width="80">用户名</th>
              <th width="120">邮箱</th>
              <th width="60">反馈类型</th>
              <th width="120">标题</th>
              <th>描述</th>
              <th width="120">上传时间</th>
              <th width="120">用户ip</th>
            </tr>
          </thead>
          <tbody>
            <asp:Repeater ID="rptThemes" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Name") %></td>
                        <td><%#Eval("Mail")%></td>
                        <td><%#Eval("SupportType")%></td>
                        <td><%#Eval("Subject")%></td>
                        <td><%#Eval("Description")%></td>
                        <td><%#Eval("AddTime")%></td>
                        <td><%#Eval("UserIp")%></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
          </tbody>
    </table>
    <asp:AspNetPager ID="pager" runat="server" PageSize="5" FirstPageText="首页" LastPageText="末页" PrevPageText="上页" NextPageText="下页" OnPageChanged="pager_PageChanged"></asp:AspNetPager>
    </div>
    </form>
</body>
</html>
