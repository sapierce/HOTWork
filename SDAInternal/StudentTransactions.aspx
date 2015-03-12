<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentTransactions.aspx.cs" Inherits="HOTSelfDefense.StudentTransactionsPage" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <asp:HyperLink ID="studentInformation" Text="Return to Student Information" runat="server" />
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
                <asp:Label ID="studentName" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:HyperLink ID="addTransaction" Text="Add A Transaction" runat="server" />
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
        <asp:Literal ID="transactionOutput" runat="server" />
    </table>
</asp:Content>