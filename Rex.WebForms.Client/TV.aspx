<%@ Page Title="" Language="C#" MasterPageFile="~/RexWebFormsClient.master" AutoEventWireup="true" CodeFile="TV.aspx.cs" Inherits="TV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadingContent" runat="Server">
    Table/View:
    <asp:Label ID="lblTV" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="Server">
    <a href="Connections.aspx">Database Connections</a>
    &nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="lnkDatabase" runat="server"></asp:HyperLink>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:GridView runat="server" ID="gv" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="PK" HeaderText="PK" />
            <asp:HyperLinkField DataTextField="ColumnName" DataNavigateUrlFields="ColumnLink" HeaderText="Column" />
            <asp:BoundField DataField="ColumnTypeName" HeaderText="Type" />
            <asp:BoundField DataField="Nullability" HeaderText="Null" />
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="QueryExecutionLog" runat="Server">
</asp:Content>
