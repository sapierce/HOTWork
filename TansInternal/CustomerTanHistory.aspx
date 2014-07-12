<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerTanHistory.aspx.cs" Inherits="HOTTropicalTans.CustomerTanHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <a href="<%= HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL  %>?ID=<%= Request.QueryString["ID"].ToString() %>">Return to Customer Information</a><br />
    <br />
    <asp:GridView ID="tanningHistory" AllowPaging="true" runat="server" AutoGenerateColumns="false" PageSize="25"
        OnPageIndexChanging="tanningHistory_PageIndexChanging" HeaderStyle-BackColor="#CC9900" HeaderStyle-HorizontalAlign="Center"
        RowStyle-BackColor="#fed583" FooterStyle-BackColor="#CC9900" EmptyDataRowStyle-BackColor="#fed583" CellPadding="5"
        EnableViewState="true" EnableSortingAndPagingCallbacks="true" PagerStyle-BackColor="#CC9900">
        <Columns>
            <asp:BoundField DataField="Date" HeaderText="Date" />
            <asp:BoundField DataField="Time" HeaderText="Time" />
            <asp:BoundField DataField="Bed" HeaderText="Bed" />
            <asp:BoundField DataField="Length" HeaderText="Length" />
            <asp:BoundField DataField="OnlineIndicator" HeaderText="Online?" />
            <asp:BoundField DataField="DeletedIndicator" HeaderText="Deleted?" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="noTansFound" runat="server" Text="No tan history found." />
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>