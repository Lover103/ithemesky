<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        Put content here.
        <%
            if (IThemeSky.Library.Util.CacheHelper.Contains("test"))
            {
                Response.Write(IThemeSky.Library.Util.CacheHelper.Get<string>("test"));
            }
            else
            {
                IThemeSky.Library.Util.CacheHelper.Set<string>("test", DateTime.Now.ToString());
                Response.Write(DateTime.Now);
            }
        %>
    </p>
</asp:Content>
