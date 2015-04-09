<%@ Page Title="" Language="C#" MasterPageFile="~/RexWebFormsClient.master" AutoEventWireup="true" CodeFile="Connections.aspx.cs" Inherits="Connections" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadingContent" runat="Server">
    Database Connections
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="Server">
    &nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:GridView runat="server" ID="gvConnections" AutoGenerateColumns="false" OnRowCommand="gvConnections_RowCommand">
        <Columns>
            <asp:ButtonField DataTextField="Name" HeaderText="Database Connection Name" ButtonType="Link" Text="Name" CommandName="NameClick" />
            <asp:BoundField DataField="Provider" HeaderText="Provider" />
            <asp:BoundField DataField="Server" HeaderText="Server" />
            <asp:BoundField DataField="Database" HeaderText="Database" />
            <asp:BoundField DataField="ConnectAs" HeaderText="Connect as..." />
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="QueryExecutionLog" runat="Server">
</asp:Content>


