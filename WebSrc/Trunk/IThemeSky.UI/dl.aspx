<%@ Page Language="C#" AutoEventWireup="true" %>

<%
    Response.Redirect(Request.QueryString["url"]);
%>