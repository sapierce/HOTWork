<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerTransactions.aspx.cs" Inherits="HOTTropicalTans.CustomerTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <a href="<%= HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL %>?ID=<%=Request.QueryString["ID"]%>">Return to user information</a>
    <br />
    <br />
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Customer Transactions</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Name:</td>
                <td>
                    <asp:Label ID="customerName" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <a href="<%= HOTBAL.POSConstants.CART_URL %>?ID=<%=Request.QueryString["ID"]%>">Add A Transaction</a>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <br />
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="7">Last 10 Transactions</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="centerAlignHeader">
                    <br />
                </td>
                <td class="centerAlignHeader">Date</td>
                <td class="centerAlignHeader">Items/Quantity</td>
                <td class="centerAlignHeader">Location</td>
                <td class="centerAlignHeader">Method</td>
                <td class="centerAlignHeader">Total</td>
                <td class="centerAlignHeader">Paid?</td>
            </tr>
            <asp:Literal ID="transactionLog" runat="server" />
        </tbody>
    </table>
</asp:Content>