<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IThemeSky.Management.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        您的名字是？
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox> 
        <asp:Button ID="btnLogin" runat="server" Text="提交答案" onclick="btnLogin_Click" /><br />
    </div>
    </form>
</body>
</html>
