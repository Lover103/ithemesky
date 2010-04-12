<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThemeListManage.aspx.cs" Inherits="IThemeSky.Management.Theme.ThemeListManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Resources/Css/Content.css" rel="Stylesheet" />
    <script type="text/javascript" src="../Resources/js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(
            function() {
                if ($('.message').html() != '') {
                    setTimeout(function() {
                        $('.message').slideUp('slow');
                    }, 1500);
                }
                else {
                    $('.message').hide();
                }
            }
        );
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="message ImportantText">
        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>
    <div style="border:solid 1px #333">
        过滤器 -> 
        审核状态：
        <asp:DropDownList ID="ddlCheckStateFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_Changed">
            <asp:ListItem Text="全部" Value="-2" Selected="True"></asp:ListItem>
            <asp:ListItem Text="待审核" Value="0"></asp:ListItem>
            <asp:ListItem Text="通过" Value="1"></asp:ListItem>
            <asp:ListItem Text="不通过" Value="-1"></asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        显示状态:
        <asp:DropDownList ID="ddlDisplayStateFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="filter_Changed">
            <asp:ListItem Text="全部" Value="-2" Selected="True"></asp:ListItem>
            <asp:ListItem Text="显示" Value="1"></asp:ListItem>
            <asp:ListItem Text="隐藏" Value="0"></asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        标题: <asp:TextBox ID="txtTitleFilter" runat="server"></asp:TextBox> <asp:Button ID="btnSearchTitle" runat="server" Text="检索" OnClick="filter_Changed" />
    </div>
    <div>
        <table width="100%" cellpadding="6" cellspacing="1" class="InputGrid">
          <thead>
            <tr>
              <th>主题名称</th>
              <th width="120">所属分类</th>
              <th width="60">主题大小</th>
              <th width="60">推荐指数</th>
              <th width="80">审核状态</th>
              <th width="80">显示状态</th>
              <th width="120">上传时间</th>
            </tr>
          </thead>
          <tbody>
            <asp:Repeater ID="rptThemes" runat="server" OnItemDataBound="rptThemes_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                        <asp:TextBox ID="txtTitle" runat="server" value='<%#Eval("Title") %>' OnTextChanged="propertyList_SelectedIndexChanged" AutoPostBack="true"></asp:TextBox>
                        (id:<%#Eval("ThemeId") %>)(<a href="ThemeInfoManage.aspx?themeId=<%#Eval("ThemeId") %>" target="_blank">编辑</a>)(<a href="/<%#Eval("DownloadUrl") %>" target="_blank">下载</a>)(<a href="/iphone-themes/<%#Eval("Title").ToString().Trim().Replace(" ", "-") %>/<%#Eval("ThemeId") %>" target="_blank">浏览</a>)<br />
                        所属标签：<asp:TextBox ID="txtTags" runat="server" value='<%#GetThemeTags(Eval("ThemeId")) %>' OnTextChanged="propertyList_SelectedIndexChanged" AutoPostBack="true"></asp:TextBox><br />
                        <a href="/<%#Eval("ThumbnailName") %>" target="_blank"><img src="/<%#Eval("ThumbnailName").ToString().Replace(".jpg", "_112x168.jpg") %>" border="0" /></a>
                        <asp:HiddenField ID="hidThemeId" runat="server" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategoryId" runat="server" DataTextField="CategoryName" DataValueField="CategoryId" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="propertyList_SelectedIndexChanged">
                                <asp:ListItem Text="未归类" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td><%#Eval("FileSize") %></td>
                        <td>
                            <asp:DropDownList ID="ddlCommendIndex" runat="server" AutoPostBack="true" OnSelectedIndexChanged="propertyList_SelectedIndexChanged">
                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCheckState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="propertyList_SelectedIndexChanged">
                                <asp:ListItem Text="待审核" Value="0"></asp:ListItem>
                                <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                                <asp:ListItem Text="不通过" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDisplayState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="propertyList_SelectedIndexChanged">
                                <asp:ListItem Text="显示" Value="1"></asp:ListItem>
                                <asp:ListItem Text="隐藏" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <%#Eval("AddTime") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
          </tbody>
        </table>
        <div class="pager">
            <asp:AspNetPager ID="pager" runat="server" PageSize="20" FirstPageText="首页" LastPageText="末页" PrevPageText="上页" NextPageText="下页" OnPageChanged="pager_PageChanged"></asp:AspNetPager>
        </div>
    </div>
    </form>
</body>
</html>
