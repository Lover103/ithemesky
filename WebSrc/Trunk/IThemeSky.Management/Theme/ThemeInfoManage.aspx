<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThemeInfoManage.aspx.cs" Inherits="IThemeSky.Management.Theme.ThemeInfoManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Resources/Css/Content.css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidThemeId" Value="" runat="server" />
    <div>
        <table width="100%" cellpadding="6" cellspacing="1" class="InputGrid">
          <thead>
            <tr>
              <th width="120"></th>
              <th></th>
              <th width="200"></th>
            </tr>
          </thead>
          <tbody>
            <tr>
                <td class="InputName">主题名称</td>
                <td><asp:TextBox ID="txtTitle" runat="server" CssClass="TextInput"></asp:TextBox><asp:CheckBox ID="chkDisplayState" runat="server" Text="是否显示" /></td>
                <td><span class="ImportantText">*</span><asp:RequiredFieldValidator ID="val_txtTitle"
                        runat="server" ErrorMessage="主题名称不能为空" ControlToValidate="txtTitle"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="InputName">主题分类</td>
                <td><asp:DropDownList ID="ddlParentCategoryId" runat="server"></asp:DropDownList><asp:DropDownList ID="ddlCategoryId" runat="server"></asp:DropDownList></td>
                <td><span class="ImportantText">*</span><asp:RequiredFieldValidator ID="val_ddlCategoryId"
                        runat="server" ErrorMessage="主题分类必须选择" ControlToValidate="ddlCategoryId"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="InputName">审核状态</td>
                <td><asp:DropDownList ID="ddlCheckState" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">推荐指数</td>
                <td>
                    <asp:DropDownList ID="ddlCommendIndex" runat="server">
                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">缩略图设置</td>
                <td><asp:TextBox ID="txtThumbnailName" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td><span class="ImportantText">*</span></td>
            </tr>
            <tr>
                <td class="InputName">主题文件</td>
                <td><asp:TextBox ID="txtDownloadUrl" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td><span class="ImportantText">*</span></td>
            </tr>
            <tr>
                <td class="InputName">评论设置</td>
                <td>好评：<asp:TextBox ID="txtRateScore" runat="server" CssClass="NumberInput"></asp:TextBox> &nbsp; &nbsp;差评：<asp:TextBox ID="txtRateNumbers" runat="server" CssClass="NumberInput"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">下载设置</td>
                <td>
                    浏览数：<asp:TextBox ID="txtViews" runat="server" CssClass="NumberInput"></asp:TextBox> &nbsp; &nbsp;下载数：<asp:TextBox ID="txtDownloads" runat="server" CssClass="NumberInput"></asp:TextBox>
                    上周下载数：<asp:TextBox ID="txtLastWeekDownloads" runat="server" CssClass="NumberInput"></asp:TextBox> &nbsp; &nbsp;上月下载数：<asp:TextBox ID="txtLastMonthDownloads" runat="server" CssClass="NumberInput"></asp:TextBox> &nbsp; &nbsp;
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">描述信息</td>
                <td><asp:TextBox ID="txtDescription" runat="server" CssClass="TextInput" TextMode="MultiLine" Columns="60" Rows="10"></asp:TextBox></td>
                <td><span class="ImportantText">*</span></td>
            </tr>
            <tr>
                <td class="InputName">作者姓名</td>
                <td><asp:TextBox ID="txtAuthorName" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">作者邮箱</td>
                <td><asp:TextBox ID="txtAuthorMail" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnSave" runat="server" Text="保 存" /></td>
                <td></td>
            </tr>
          </tbody>
        </table>
    </div>
    </form>
</body>
</html>