<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="HOTPOS.Cart" MasterPageFile="TansPOS.Master" %>

<asp:Content ID="cartMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <asp:Panel ID="pnlCart" runat="server">
    <asp:ValidationSummary CssClass="validation" ID="vsCart" HeaderText="The following validation errors were found:"
        runat="server" ValidationGroup="shoppingCart" DisplayMode="BulletList" ShowSummary="true" />
    <asp:RequiredFieldValidator ID="rfvDate" Display="None" runat="server" ControlToValidate="transactionDate"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Date is required." />
    <asp:RequiredFieldValidator ID="rfvEmployee" Display="None" runat="server" ControlToValidate="employeeID"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Employee ID is required." />
    <asp:RequiredFieldValidator ID="rfvPayment" Display="None" runat="server" ControlToValidate="paymentMethod"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Payment Type is required." />
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="6">
                    Order for:
                        <asp:Label ID="customerName" runat="server" />
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="centerAlignHeader">
                    &nbsp;
                </td>
                <td class="centerAlignHeader">
                    Description
                </td>
                <td class="centerAlignHeader">
                    Quantity
                </td>
                <td class="centerAlignHeader">
                    Price
                </td>
                <td class="centerAlignHeader">
                    Totals
                </td>
                <td class="centerAlignHeader">
                    Remove
                </td>
            </tr>
            <asp:Literal ID="shoppingCartOutput" runat="server" />
            <tr>
                <td colspan="6">
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="6" class="leftAlignHeader">
                    Barcoded item:
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Add Item:&nbsp;&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="barCodeText" runat="server" />
                </td>
                <td>
                    Quantity:&nbsp;&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="barCodeQuantity" runat="server" Text="1" MaxLength="3" Style="width: 50px;" />
                </td>
                <td>
                    Member? <asp:CheckBox ID="isMember" runat="server" />
                </td>
                <td>
                    <asp:Button ID="addBarcodedItem" runat="server" OnClick="addBarcodedItem_Click" Text="Add" />
                </td>
            </tr>
            <tr>
                <td colspan="6" class="leftAlignHeader">
                    Non-barcoded item:
                </td>
            </tr>
            <tr>
                <td>
                    Description:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="itemName" runat="server" Style="width: 250px;" />
                </td>
                <td>
                    Price:
                </td>
                <td>
                    <asp:TextBox ID="itemPrice" Style="width: 65px;" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Quantity:
                </td>
                <td>
                    <asp:TextBox ID="itemQuantity" Style="width: 50px;" Text="1" MaxLength="3" runat="server" />
                </td>
                <td>
                    Taxable:
                </td>
                <td>
                    <asp:CheckBox ID="itemTax" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Button ID="addOtherItem" Text="Add" runat="server" OnClick="addOtherItem_OnClick" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                Payment by:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="paymentMethod" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="CC" Text="Credit Card"/>
                                    <asp:ListItem Value="Check" Text="Check" />
                                    <asp:ListItem Value="Cash" Text="Cash" />
                                    <asp:ListItem Value="Trade" Text="MerchanTrade" />
                                    <asp:ListItem Value="Other" Text="Other" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td colspan="3" style="vertical-align: top;">
                    <table style="width: 100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                Trade Number:<br />
                                <span class="detailInformation">(if applicable)</span>
                            </td>
                            <td style="vertical-align: top;">
                                <asp:TextBox ID="tradeNumber" Style="width: 75px;" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                Employee ID:
                            </td>
                            <td style="vertical-align: top;">
                                <asp:TextBox ID="employeeID" Style="width: 75px;" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                Date:&nbsp;&nbsp;
                            </td>
                            <td style="vertical-align: top;">
                                <asp:TextBox ID="transactionDate" Style="width: 75px;" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="5">
                    Note:
                    <asp:TextBox ID="otherNote" runat="server" /><br />
                </td>
                <td style="text-align: right;" class="standardField">
                    <asp:Button ID="doTransaction" Text="Checkout" runat="server" OnClick="doTransaction_OnClick" CausesValidation="true" ValidationGroup="shoppingCart" /><br />
                </td>
            </tr>
        </tbody>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlComplete" runat="server" style="display:none;">
        <table class="tanning">
            <thead>
                <tr>
                    <th>Transaction Recorded</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:HyperLink ID="receiptView" runat="server" Text="Click here for a receipt" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="backToPOS" runat="server" Text="Back to Point of Sale" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="backToSchedule" runat="server" Text="Back to Schedule" />
                    </td>
                </tr>
            </tbody>
            </table>
    </asp:Panel>
</asp:Content>
