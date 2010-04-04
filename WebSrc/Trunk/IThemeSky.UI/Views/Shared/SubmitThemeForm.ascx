<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<PostThemeModel>" %>
<iframe id="hiddenIframe" name="hiddenIframe" src="#" style="display:none"></iframe>
<% using (Html.BeginForm("SubmitTheme", "Service", FormMethod.Post, new { target = "hiddenIframe", enctype = "multipart/form-data" }))
{ %>
<div class="submitForm clearfix">
	<label class="label">Title:</label>
	<%= Html.TextBoxFor(m => m.Title, new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
	<small>(required)</small>
</div>
<div class="submitForm clearfix">
	<label class="label">Category:</label>
	<select class="select" name="CategoryId" id="CategoryId">
	    <option value="-1">=Please choose a category=</option>
	    <% foreach (ThemeCategory category in ViewData.Model.ThemeCategories)
        { %>
            <option value="<%=category.CategoryId %>"><%=category.CategoryName %></option>
	    <%} %>
	</select>
	<small>(required)</small>
</div>
<div class="submitForm clearfix">
	<label class="label">Tags:</label>
	<%= Html.TextBoxFor(m => m.Tags, new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
	<small>(Please enter ”,” between different tags.)</small>
</div>
<div class="submitForm clearfix">
	<label class="label">Theme file:</label>
	<input name="ThemeFile" id="ThemeFile" type="file" class="inputFile" />
	<small>(required)</small>
</div>
<div class="submitForm clearfix">
	<label class="label">Screenshot:</label>
	<input name="ThemeThumbnail" id="ThemeThumbnail" type="file" class="inputFile" />
	<small>(320px X 480px, JPG/PNG/GIF)</small>
</div>
<div class="submitForm clearfix">
	<label class="label">Your Email:</label>
	<%= Html.TextBoxFor(m => m.AuthorMail, new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
	<small>(required)</small>
</div>
<div class="submitForm clearfix">
	<label class="label">Author’s Name:</label>
	<%= Html.TextBoxFor(m => m.AuthorName, new { Class="inputNormal", onfocus="this.className='inputFocus'", onblur="this.className='inputNormal'" })%>
</div>
<div class="submitForm clearfix">
	<label class="label">description:</label>
	<div class="textarea">
	    <%= Html.TextAreaFor(m => m.Description, new { Class = "textareaNormal", onfocus = "this.className='textareaFocus'", onblur = "this.className='textareaNormal'" })%>
	</div>
</div>
<div class="submitBtn"><button type="submit">Submit Now</button></div>
<% } %>
