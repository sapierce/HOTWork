<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentTransactions.aspx.cs" Inherits="HOTSelfDefense.StudentTransactionsPage" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align='center'>
        <asp:Label ID="lblError" class="error" runat="server" /></p>
    <a href='StudentInfo.aspx?ID=<%=Request.QueryString["UserID"]%>&Date=<%=Request.QueryString["Date"]%>'>Return to user information</a>
    <br />
    <br />
    <table class='bcc' align='center'>
        <tr>
            <td class='rheader'><b>Name:</b></td>
            <td class='reg'>
                <asp:Label ID="lblCustName" runat="server" /></td>
        </tr>
        <tr>
            <td colspan='2' class='reg' align='center'>
                <a href='/SDAPOS/Cart.aspx?ID=M<%=Request.QueryString["ID"]%>'>Add A Transaction</a>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <table class='bcc' align='center'>
        <tr>
            <td colspan='6' class='header'>Student Transactions</td>
        </tr>
        <tr>
            <td class='header'>
                <br>
            </td>
            <td class='header'>Date</td>
            <td class='header'>Items/Quantity</td>
            <td class='header'>Method</td>
            <td class='header'>Total</td>
            <td class='header'>Paid?</td>
        </tr>
        <asp:Literal ID="litTransactions" runat="server" />
    </table>
</asp:Content>