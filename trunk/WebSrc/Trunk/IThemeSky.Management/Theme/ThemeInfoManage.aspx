<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThemeInfoManage.aspx.cs" Inherits="IThemeSky.Management.Theme.ThemeInfoManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Resources/Css/Content.css" rel="Stylesheet" />
    <link href="../Resources/Css/fileuploader.css" rel="Stylesheet" />
    <script type="text/javascript" src="../Resources/js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="../Resources/js/fileuploader.js"></script>
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
                var uploader = new qq.FileUploader({
                    // pass the dom node (ex. $(selector)[0] for jQuery users)
                    element: document.getElementById('file-uploader'),
                    // path to server-side upload script
                    action: '/Services/Upload.ashx',
                    onComplete: function(id, fileName, responseJSON) {
                        $('#ulThemeImages').append('<li style="float:left"><input type="hidden" value="' + responseJSON.fileName + '" name="hidThemeImages" /><img src="' + responseJSON.thumbnail + '" width="112" height="168" onclick="setDefault(\'' + responseJSON.fileName + '\')" /><br /><a href="javascript:;" onclick="deleteImage(this.parentNode, \'' + responseJSON.fileName + '\')">[删]</a></li>');
                    }
                });
            }
        );
            function deleteImage(li, url) {
                var themeId = $('#hidThemeId').val();
                $.get('/Services/DeleteFile.ashx?themeId=' + themeId + '&fileName=' + url, function(data) {
                    if (data == 'ok') {
                        li.parentNode.removeChild(li);
                        alert('删除成功');
                    }
                    else {
                        alert('删除失败');
                    }
                });                
            }
            function setDefault(url) {
                $('#txtThumbnailName').val(url);
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="message ImportantText">
        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>
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
                <td class="InputName">主题价格</td>
                <td><asp:TextBox ID="txtPrice" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td><span class="ImportantText">*填 0 就是免费</span></td>
            </tr>
            <tr>
                <td class="InputName">主题分类</td>
                <td>
                    <asp:DropDownList ID="ddlCategoryId" runat="server" AppendDataBoundItems="true" DataTextField="CategoryName" DataValueField="CategoryId">
                        <asp:ListItem Text="未归类" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><span class="ImportantText">*</span><asp:RequiredFieldValidator ID="val_ddlCategoryId"
                        runat="server" ErrorMessage="主题分类必须选择" ControlToValidate="ddlCategoryId"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td class="InputName">审核状态</td>
                <td>
                    <asp:DropDownList ID="ddlCheckState" runat="server">
                        <asp:ListItem Text="待审核" Value="0"></asp:ListItem>
                        <asp:ListItem Text="通过" Value="1"></asp:ListItem>
                        <asp:ListItem Text="不通过" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">支持iPhone4</td>
                <td>
                    <asp:CheckBox ID="chkSupportIPhone4" runat="server" />
                </td>
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
                <td class="InputName">主题图片</td>
                <td>
                    <ul id="ulThemeImages">
                        <asp:Repeater ID="rptThemeImages" runat="server">
                            <ItemTemplate>
                                <li style="float:left"><input type="hidden" value="<%#Eval("ImageUrl") %>" name="hidThemeImages" /><img src="<%#Eval("ImageUrl").ToString().Replace(".jpg", "_112x168.jpg") %>" width="112" height="168" onclick="setDefault('<%#Eval("ImageUrl") %>')" /><br /><a href="javascript:;" onclick="deleteImage(this.parentNode, '<%#Eval("ImageUrl") %>')">[删]</a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div id="file-uploader" style="clear:both"> 
                    </div>
                </td>
                <td><span class="ImportantText">*</span></td>
            </tr>
            <tr>
                <td class="InputName">主题文件</td>
                <td>
                    <asp:TextBox ID="txtDownloadUrl" runat="server" CssClass="TextInput"></asp:TextBox>
                </td>
                <td><span class="ImportantText">*</span></td>
            </tr>
            <tr>
                <td class="InputName">主题大小</td>
                <td><asp:TextBox ID="txtFileSize" runat="server" CssClass="TextInput"></asp:TextBox></td>
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
                <td class="InputName">修改时间</td>
                <td><asp:TextBox ID="txtUpdateTime" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="InputName">添加时间</td>
                <td><asp:TextBox ID="txtAddTime" runat="server" CssClass="TextInput"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnSave" runat="server" Text="保 存" onclick="btnSave_Click" /></td>
                <td></td>
            </tr>
          </tbody>
        </table>
    </div>
    </form>
</body>
</html>