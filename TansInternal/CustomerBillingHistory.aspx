<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerBillingHistory.aspx.cs" Inherits="HOTTropicalTans.CustomerBillingHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <a href="<%= HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL %>?ID=<%= Request.QueryString["ID"].ToString() %>">Return to Customer Information</a>
    <br />
    <br />
    <table class="tanning">
        <tr>
            <th>Full Billing History For:</th>
            <th>
                <asp:Label ID="customerName" runat="server" /></th>
        </tr>
        <tr>
            <td class="rightAlignHeader">Current Plan:</td>
            <td>
                <asp:Label ID="currentPlan" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Current Renewal Date:</td>
            <td>
                <asp:Label ID="renewalDate" runat="server" /></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="billingHistory" AllowPaging="true" runat="server" AutoGenerateColumns="false" PageSize="25" OnPageIndexChanging="billingHistory_PageIndexChanging"
        HeaderStyle-BackColor="#CC9900" HeaderStyle-HorizontalAlign="Center"
        RowStyle-BackColor="#fed583" FooterStyle-BackColor="#CC9900" EmptyDataRowStyle-BackColor="#fed583" CellPadding="5"
        EnableViewState="true" EnableSortingAndPagingCallbacks="true" PagerStyle-BackColor="#CC9900">
        <Columns>
            <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="RenewalDate" HeaderText="Renewal Date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="Package" HeaderText="Package" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="noBillingFound" runat="server" Text="No billing history found." />
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>