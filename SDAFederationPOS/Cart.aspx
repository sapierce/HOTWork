<%@ Page Title="" Language="C#" MasterPageFile="~/FederationPOS.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="SDAFederationPOS.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeholderMain" runat="server">
<asp:Panel ID="pnlCart" runat="server">
    <asp:ValidationSummary CssClass="validation" ID="vsCart" HeaderText="The following validation errors were found:"
        runat="server" ValidationGroup="shoppingCart" DisplayMode="BulletList" ShowSummary="true" />
    <asp:RegularExpressionValidator ID="revDate" ControlToValidate="orderDate" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"
        Display="None" runat="server" EnableClientScript="true" ErrorMessage="Date is not formatted correctly (MM-DD-YYYY)."
        ValidationGroup="shoppingCart" />
    <asp:RequiredFieldValidator ID="rfvEmployee" Display="None" runat="server" ControlToValidate="employeeId"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Employee ID is required" />
    <asp:RequiredFieldValidator ID="rfvPayment" Display="None" runat="server" ControlToValidate="paymentMethod"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Payment Type is required" />
    <table>
        <tr>
            <td colspan='6'>
                <h3>
                    Order for:
                    <asp:Label ID="customerName" runat="server" /></h3>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <h5>
                    Description</h5>
            </td>
            <td>
                <h5>
                    Quantity</h5>
            </td>
            <td>
                <h5>
                    Price</h5>
            </td>
            <td>
                <h5>
                    Totals</h5>
            </td>
            <td>
                <h5>
                    Remove</h5>
            </td>
        </tr>
        <asp:Literal ID="shoppingCartOutput" runat="server" />
        <tr>
            <td colspan='6'>
                &nbsp;
            </td>
        </tr>
         <tr>
            <td colspan='2'>
                <b>Add Item:&nbsp;&nbsp;</b>
            </td>
            <td>
                <asp:TextBox ID='barCodeText' runat="server" />
            </td>
            <td>
                <b>Quantity:&nbsp;&nbsp;</b>
            </td>
            <td>
                <asp:TextBox ID="quantity" runat="server" Text="1" Style="width: 65px;" />
            </td>
            <td>
                <asp:Button ID="addItem" Text='Add' runat='server' OnClick="addItem_Click" />
            </td>
        </tr>
        <tr>
            <td colspan='3'>
                <table>
                    <tr>
                        <td>
                            <b>Payment by:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="paymentMethod" runat="server">
                                <asp:ListItem Value='CC' id="paymentCredit" runat="server" />
                                <asp:ListItem Value='Check' id="paymentCheck" runat="server" />
                                <asp:ListItem Value='Cash' id="paymentCash" runat="server" />
                                <asp:ListItem Value='Other' id="paymentOther" runat="server" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan='3'>
                <table>
                    <tr>
                        <td>
                            Employee ID:&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID='employeeId' Style="width: 75px;" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date:&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID='orderDate' Style="width: 75px;" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID='checkout' Text='Checkout' runat='server' OnClick="checkout_Click" /><br />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlComplete" runat="server" style="display:none;">
        <asp:Literal ID="litComplete" runat="server" />
    </asp:Panel>
</asp:Content>
