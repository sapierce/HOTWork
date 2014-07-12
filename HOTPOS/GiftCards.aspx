<%@ Page Title="" Language="C#" MasterPageFile="TansPOS.Master" AutoEventWireup="true" CodeBehind="GiftCards.aspx.cs" Inherits="HOTPOS.GiftCards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeholderMain" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Gift Cards</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">To Last Name:</td>
            <td>
                <asp:TextBox ID="toLastName" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">To First Name:</td>
            <td>
                <asp:TextBox ID="toFirstName" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Literal ID="customerList" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">New Customer?</td>
            <td>
                <asp:CheckBox ID="newCustomer" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">From:</td>
            <td>
                <asp:TextBox ID="fromName" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Amount:</td>
            <td>
                <asp:TextBox ID="giftAmount" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Description:</td>
            <td>
                <asp:TextBox ID="giftDescription" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Employee number:</td>
            <td>
                <asp:TextBox ID="employeeNumber" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Payment by:</td>
            <td>
                <asp:RadioButtonList ID="paymentMethod" runat="server">
                    <asp:ListItem Value='CC' Text="Credit" runat="server" />
                    <asp:ListItem Value='Check' Text='Check' runat="server" />
                    <asp:ListItem Value='Cash' Text='Cash' runat="server" />
                    <asp:ListItem Value='Trade' Text='Trade' runat="server" />
                    <asp:ListItem Value='Other' Text='Other' runat="server" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan='2'>
                <asp:Button ID="submitGift" runat="server" Text="Submit" OnClick="submitGift_Click" />
            </td>
        </tr>
    </table>
</asp:Content>