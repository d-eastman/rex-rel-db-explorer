<%@ Page Title="" Language="C#" MasterPageFile="~/RexWebFormsClient.master" AutoEventWireup="true" CodeFile="Database.aspx.cs" Inherits="Database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadingContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="Server">
    <a href="Connections.aspx">Database Connections</a>
    &nbsp;&nbsp;&nbsp;Database: <asp:Label ID="lblDatabase" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="SchemaName" HeaderText="Schema" />
            <asp:HyperLinkField DataTextField="TVName" DataNavigateUrlFields="TVLink" HeaderText="Table/View" />
            <asp:BoundField DataField="TVTypeName" HeaderText="Type" />
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="QueryExecutionLog" runat="Server">
    <asp:Label ID="lblQueryExecutionLog" runat="server"></asp:Label>
</asp:Content>
