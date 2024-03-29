<%@ Control Language="C#" AutoEventWireup="true"
	Inherits="YAF.Controls.ForumCategoryList" Codebehind="ForumCategoryList.ascx.cs" %>
<%@ Register TagPrefix="YAF" TagName="ForumList" Src="ForumList.ascx" %>
<asp:UpdatePanel ID="UpdatePanelCategory" runat="server" UpdateMode="Conditional">
	<ContentTemplate>
	<table class="content" width="100%">
		<asp:Repeater ID="CategoryList" runat="server">
			<HeaderTemplate>			
				<tr class="forumRowTitle">
						<th colspan="2" align="left" class="header1 headerForum" style="padding-left:10px;">
							<YAF:LocalizedLabel ID="ForumHeaderLabel" runat="server" LocalizedTag="FORUM" />
						</th>
						<th id="Td1" class="header1 headerModerators" width="15%" align="left" runat="server" visible="<%# PageContext.BoardSettings.ShowModeratorList %>">
							<YAF:LocalizedLabel ID="ModeratorsHeaderLabel" runat="server" LocalizedTag="MODERATORS" />
						</th>
						<th class="header1 headerTopics" width="7%" align="left">
							<YAF:LocalizedLabel ID="TopicsHeaderLabel" runat="server" LocalizedTag="TOPICS" />
						</th>
						<th class="header1 headerPosts" width="7%" align="left">
							<YAF:LocalizedLabel ID="PostsHeaderLabel" runat="server" LocalizedTag="POSTS" />
						</th>
						<th class="header1 headerLastPost" width="25%" align="left">
							<YAF:LocalizedLabel ID="LastPostHeaderLabel" runat="server" LocalizedTag="LASTPOST" />
						</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr class="forumRowCat header2">
					<td colspan="<%# (PageContext.BoardSettings.ShowModeratorList ? "6" : "5" ) %>">
						<YAF:CollapsibleImage ID="CollapsibleImage" runat="server" BorderWidth="0" ImageAlign="Bottom"
							PanelID='<%# "categoryPanel" + DataBinder.Eval(Container.DataItem, "CategoryID").ToString() %>'
							AttachedControlID="forumList" />
						&nbsp;&nbsp; <a href='<%# YAF.Classes.Utils.YafBuildLink.GetLink(ForumPages.forum,"c={0}",DataBinder.Eval(Container.DataItem, "CategoryID")) %>'>
							
							<asp:Image ID="uxCategoryImage" CssClass="category_image" AlternateText=" " ImageUrl='<%# YafForumInfo.ForumClientFileRoot + YafBoardFolders.Current.Categories + "/" + DataBinder.Eval(Container.DataItem, "CategoryImage") %>' Visible='<%# !String.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "CategoryImage" ).ToString()) %>' runat="server" />
							
							<%# DataBinder.Eval(Container.DataItem, "Name") %>
						</a>
					</td>
				</tr>
				<YAF:ForumList runat="server" Visible="true" ID="forumList" DataSource='<%# ((System.Data.DataRowView)Container.DataItem).Row.GetChildRows("FK_Forum_Category") %>' />
			</ItemTemplate>
			<FooterTemplate>
				<tr class="forumRowFoot footer1">
					<td colspan="<%# (PageContext.BoardSettings.ShowModeratorList ? "6" : "5" ) %>" align="right">
						<asp:LinkButton runat="server" OnClick="MarkAll_Click" ID="MarkAll" Text='<%# PageContext.Localization.GetText("MARKALL") %>' />
						<YAF:RssFeedLink ID="RssFeed1" runat="server" FeedType="Forum" AdditionalParameters='<%# this.PageContext.PageCategoryID != 0 ? string.Format("c={0}", this.PageContext.PageCategoryID) : null %>' ShowSpacerBefore="true" Visible="<%# PageContext.BoardSettings.ShowRSSLink %>" TitleLocalizedTag="RSSICONTOOLTIPFORUM" />
					</td>
				</tr>				
			</FooterTemplate>
		</asp:Repeater>
	  </table>
	</ContentTemplate>
</asp:UpdatePanel>
