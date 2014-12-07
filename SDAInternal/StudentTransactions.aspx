<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentTransactions.aspx.cs" Inherits="HOTSelfDefense.StudentTransactionsPage" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align='center'>
        <asp:Label ID="lblError" class="error" runat="server" />
    </p>
    <a href='StudentInfo.aspx?ID=<%=Request.QueryString["UserID"]%>&Date=<%=Request.QueryString["Date"]%>'>Return to user information</a>
    <br />
    <br />
    <table class="defense">
        <thead>
            <tr>
                <th colspan="2">Student Transactions
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
            <td class="rightAlignHeader">Name:</td>
            <td>
                <asp:Label ID="lblCustName" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <a href='/SDAPOS/Cart.aspx?ID=<%=Request.QueryString["ID"]%>&Action='>Add A Transaction</a>
            </td>
        </tr>
            </tbody>
    </table>
    <br />
    <br />
    <table class="defense">
        <thead>
            <tr>
                <th colspan="6">Last 10 Transactions
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="centerAlignHeader">
                    <br>
                </td>
                <td class="centerAlignHeader">Date</td>
                <td class="centerAlignHeader">Items/Quantity</td>
                <td class="centerAlignHeader">Method</td>
                <td class="centerAlignHeader">Total</td>
                <td class="centerAlignHeader">Paid?</td>
            </tr>
        </tbody>
        <asp:Literal ID="litTransactions" runat="server" />
    </table>
</asp:Content>