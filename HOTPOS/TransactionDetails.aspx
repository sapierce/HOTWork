<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactionDetails.aspx.cs" Inherits="HOTPOS.TransactionDetails" MasterPageFile="TansPOS.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Transaction
                    <asp:Label ID="transactionID" runat="server" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Date:</td>
                <td>
                    <asp:TextBox ID="transactionDate" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Buyer:</td>
                <td>
                    <asp:Label ID="transactionBuyer" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Seller:</td>
                <td>
                    <asp:TextBox ID="transactionSeller" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Payment:</td>
                <td>
                    <asp:DropDownList ID="transactionPayment" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Total:</td>
                <td>
                    <asp:TextBox ID="transactionTotal" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Items:</td>
                <td>
                    <asp:Label ID="transactionItemsList" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Paid?</td>
                <td>
                    <asp:CheckBox ID="isPaid" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Void?</td>
                <td>
                    <asp:CheckBox ID="isVoid" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="buyerID" runat="server" Style="display: none" />
                    <asp:Button ID="transactionEdit" runat="server" OnClick="transactionEdit_Click" Text="Edit Transaction" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="transactionReceipt" runat="server" OnClick="transactionReceipt_Click" Text="Get Receipt" /></td>
            </tr>   
        </tbody>
    </table>
</asp:Content>