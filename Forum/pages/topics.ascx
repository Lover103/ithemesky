<%@ Control Language="c#" AutoEventWireup="True" Inherits="YAF.Pages.topics" Codebehind="topics.ascx.cs" %>
<%@ Register TagPrefix="YAF" TagName="ForumList" Src="../controls/ForumList.ascx" %>
<YAF:PageLinks runat="server" ID="PageLinks" />
<div class="DivTopSeparator">
</div>
<asp:PlaceHolder runat="server" ID="SubForums" Visible="false">
    <table class="content" cellspacing="1" cellpadding="0" width="100%">
        <tr class="header1">
            <td colspan="6" class="headersub">
                <%=GetSubForumTitle()%>
            </td>
        </tr>
        <tr class="header2">
            <th width="1%">&nbsp;</th>
            <th align="left" class="headerForum">
                <YAF:LocalizedLabel ID="LocalizedLabel1" runat="server" LocalizedTag="FORUM" />
            </th>
            <th width="15%" runat="server" class="headerModerators" align="left" visible="<%# PageContext.BoardSettings.ShowModeratorList %>">
                <YAF:LocalizedLabel ID="LocalizedLabel2" runat="server" LocalizedTag="moderators" />
            </th>
            <th width="5%" class="headerTopics" align="left">
                <YAF:LocalizedLabel ID="LocalizedLabel3" runat="server" LocalizedTag="topics" />
            </th>
            <th width="5%" class="headerPosts" align="left">
                <YAF:LocalizedLabel ID="LocalizedLabel4" runat="server" LocalizedTag="posts" />
            </th>
            <th width="25%" class="headerLastPost" align="left">
                <YAF:LocalizedLabel ID="LocalizedLabel5" runat="server" LocalizedTag="lastpost" />
            </th>
        </tr>
        <YAF:ForumList AltLastPost="<%# this.lastPostImageTT %>" runat="server" ID="ForumList" />
    </table>
</asp:PlaceHolder>
<table class="command" cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td>
            <YAF:Pager runat="server" ID="Pager" UsePostBack="False" />
        </td>
        <td>
            <YAF:ThemeButton ID="moderate1" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_MODERATE" TitleLocalizedTag="BUTTON_MODERATE_TT" OnClick="moderate_Click" />
            <YAF:ThemeButton ID="NewTopic1" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_NEWTOPIC" TitleLocalizedTag="BUTTON_NEWTOPIC_TT" OnClick="NewTopic_Click" />
        </td>
    </tr>
</table>
<table class="content" width="100%">
    <tr class="topicTitle">
        <td class="header1" colspan="5" style="padding-left:10px;">
            <asp:Label ID="PageTitle" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class="topicSubTitle">
        <td class="header2" width="1%">&nbsp;</td>
        <td class="header2" align="left">
            <YAF:LocalizedLabel ID="LocalizedLabel6" runat="server" LocalizedTag="topics" />
        </td>
        <td class="header2" align="left" width="7%">
            <YAF:LocalizedLabel ID="LocalizedLabel8" runat="server" LocalizedTag="replies" />
        </td>
        <td class="header2" align="left" width="7%">
            <YAF:LocalizedLabel ID="LocalizedLabel9" runat="server" LocalizedTag="views" />
        </td>
        <td class="header2" align="left" width="15%">
            <YAF:LocalizedLabel ID="LocalizedLabel10" runat="server" LocalizedTag="lastpost" />
        </td>
    </tr>
    <asp:Repeater ID="Announcements" runat="server">
        <ItemTemplate>
            <YAF:TopicLine runat="server" AltLastPost="<%# this.lastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Repeater ID="TopicList" runat="server">
        <ItemTemplate>
            <YAF:TopicLine runat="server" AltLastPost="<%# this.lastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </ItemTemplate>
        <AlternatingItemTemplate>
            <YAF:TopicLine runat="server" IsAlt="True" AltLastPost="<%# this.lastPostImageTT %>" DataRow="<%# Container.DataItem %>" />
        </AlternatingItemTemplate>
    </asp:Repeater>
    <YAF:ForumUsers runat="server" />
    <tr>
        <td align="center" colspan="5" class="footer1">
            <table cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td width="1%" style="white-space: nowrap">
                        <YAF:LocalizedLabel ID="LocalizedLabel11" runat="server" LocalizedTag="showtopics" />
                        <asp:DropDownList ID="ShowList" runat="server" AutoPostBack="True" />
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="WatchForum" runat="server" /><span id="WatchForumID" runat="server"
                            visible="false" /><span id="delimiter1" runat="server" visible="<%# this.WatchForum.Text.Length > 0 %>"> | </span>
                        <asp:LinkButton runat="server" ID="MarkRead" />
                        <YAF:RssFeedLink ID="RssFeed" runat="server" FeedType="Topics" ShowSpacerBefore="true"
                            Visible="<%# PageContext.BoardSettings.ShowRSSLink %>" TitleLocalizedTag="RSSICONTOOLTIPFORUM" />                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table class="command" width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td align="left">
            <YAF:Pager ID="PagerBottom" runat="server" LinkedPager="Pager" UsePostBack="False" />
        </td>
        <td>
            <YAF:ThemeButton ID="moderate2" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_MODERATE" TitleLocalizedTag="BUTTON_MODERATE_TT" OnClick="moderate_Click" />
            <YAF:ThemeButton ID="NewTopic2" runat="server" CssClass="yafcssbigbutton rightItem"
                TextLocalizedTag="BUTTON_NEWTOPIC" TitleLocalizedTag="BUTTON_NEWTOPIC_TT" OnClick="NewTopic_Click" />
        </td>
    </tr>
</table>
<asp:PlaceHolder ID="ForumJumpHolder" runat="server">
    <div id="DivForumJump">
        <YAF:LocalizedLabel ID="ForumJumpLabel" runat="server" LocalizedTag="FORUM_JUMP" />
        &nbsp;<YAF:ForumJump ID="ForumJump1" runat="server" />
    </div>
</asp:PlaceHolder>
<div id="DivIconLegend">
    <YAF:IconLegend ID="IconLegend1" runat="server" />
</div>
<div id="DivPageAccess" class="smallfont">
    <YAF:PageAccess ID="PageAccess1" runat="server" />
</div>
<div id="DivSmartScroller">
    <YAF:SmartScroller ID="SmartScroller1" runat="server" />
</div>
