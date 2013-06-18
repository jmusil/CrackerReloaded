<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>About this site</h1>
    <p>
        This site serves for tracking purposes during bugfixing. 
    </p>
    <p>
        Each bugfixer should check out the bug he's currently working on (i.e. check out ABC-123), so there's no duplicated work. 
        After he's finished fixing the bug, he checks the bug in and moves on to a new one.
    </p>
    <p>
        The statistics page allows performance monitoring per team member. 
    </p>
</asp:Content>

