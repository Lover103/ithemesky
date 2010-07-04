<%@ Page Language="C#" AutoEventWireup="true" %>

<%
    string qid = Request.QueryString["qid"];
    string url = string.Empty;
    switch (qid)
    {
        case "1":
            url = "http://forum.ithemesky.com/default.aspx?g=posts&t=5";
            break;
        case "2":
            url = "http://forum.ithemesky.com/default.aspx?g=posts&t=4";
            break;
        case "3":
            url = "http://forum.ithemesky.com/default.aspx?g=posts&t=6";
            break;
    }
    if (!string.IsNullOrEmpty(url))
    { 
        Response.Redirect(url);
    }
%>