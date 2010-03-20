<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PostCommentModel>" %>
<iframe id="hiddenIframe" name="hiddenIframe" src="#" style="display:none"></iframe>
<% using (Html.BeginForm("AddThemeComment", "Service", FormMethod.Post, new { target = "hiddenIframe" }))
   { %>
    <%= Html.HiddenFor(m=> m.ThemeId) %>
	<div class="commentForm clearfix">
		<label class="label">Name:</label>
		<%= Html.TextBoxFor(m => m.UserName, new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
		<small>(required)<%= Html.ValidationMessageFor(m => m.UserName) %></small>
	</div>
	<div class="commentForm clearfix">
		<label class="label">Email:</label>
		<%= Html.TextBoxFor(m => m.UserMail, new { Class = "inputNormal", onfocus = "this.className='inputFocus'", onblur = "this.className='inputNormal'" })%>
	</div>
	<div class="commentForm clearfix">
		<label class="label">Your Comments:</label>
		<div class="textarea"><%= Html.TextAreaFor(m => m.Content, new { Class = "textareaNormal", onfocus = "this.className='textareaFocus'", onblur = "this.className='textareaNormal'" })%></div>
	</div>
	<div class="commentBtn"><button type="submit">Add Comment</button></div>
    <% } %>
