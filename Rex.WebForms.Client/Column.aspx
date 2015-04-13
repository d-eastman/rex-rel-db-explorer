<%@ Page Title="" Language="C#" MasterPageFile="~/RexWebFormsClient.master" AutoEventWireup="true" CodeFile="Column.aspx.cs" Inherits="Column" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadingContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="Server">
    <a href="Connections.aspx">Database Connections</a>
    &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkDatabase" runat="server"></asp:HyperLink>
    &nbsp;&nbsp;&nbsp;<asp:HyperLink ID="lnkTV" runat="server"></asp:HyperLink>
    &nbsp;&nbsp;&nbsp;Column: <asp:Label ID="lblColumn" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="Server">
    More column content will go here
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="QueryExecutionLog" runat="Server">
</asp:Content>