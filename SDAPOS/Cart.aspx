<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="SDAPOS.Cart"
    MasterPageFile="SDAPOS.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <asp:Panel ID="pnlCart" runat="server">
    <p align='center'>
        <asp:Label ID="errorMessage" class="error" runat="server" /></p>
    <asp:ValidationSummary CssClass="validation" ID="vsCart" HeaderText="The following validation errors were found:"
        runat="server" ValidationGroup="shoppingCart" DisplayMode="BulletList" ShowSummary="true" />
    <asp:RequiredFieldValidator ID="rfvEmployee" Display="None" runat="server" ControlToValidate="employeeId"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Employee ID is required" />
    <asp:RequiredFieldValidator ID="rfvPayment" Display="None" runat="server" ControlToValidate="paymentMethod"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Payment Type is required" />
    <table class="defense" style="margin: 0px auto;">
        <tr>
            <td colspan='6' class='leftAlignHeader'>
                    Order for:
                    <asp:Label ID="customerName" runat="server" />
            </td>
        </tr>
        <tr>
            <td  class="centerAlignHeader">
                &nbsp;
            </td>
            <td  class="centerAlignHeader">
                Description
            </td>
            <td  class="centerAlignHeader">
                Quantity
            </td>
            <td  class="centerAlignHeader">
                Price
            </td>
            <td  class="centerAlignHeader">
                Totals
            </td>
            <td  class="centerAlignHeader">
                Remove
            </td>
        </tr>
        <asp:Literal ID="shoppingCartOutput" runat="server" />
        <tr>
            <td colspan='6'>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan='6' class="leftAlignHeader">
                <b>Barcoded item:</b>
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
                <b>Quantity:&nbsp;&nbsp;</b>
            </td>
            <td>
                <asp:TextBox ID="barCodeQuantity" runat="server" Text="1" MaxLength="3" Style="width: 65px;" />
            </td>
            <td>
                Member? <asp:CheckBox ID="isMember" runat="server" checked="true" />
            </td>
            <td>
                <asp:Button ID="addBarCode" Text='Add' runat='server' OnClick="addBarCode_Click" />
            </td>
        </tr>
        <tr>
            <td colspan='6' class="leftAlignHeader">
                Non-barcoded item:
            </td>
        </tr>
        <tr>
            <td>
                <b>Description:</b>
            </td>
            <td colspan='2'>
                <asp:TextBox ID='itemName' runat="server" />
            </td>
            <td>
                <b>Price:</b>
            </td>
            <td>
                <asp:TextBox ID='itemPrice' Style="width: 65px;" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <b>Quantity:</b>
            </td>
            <td>
                <asp:TextBox ID='itemQuantity' Style="width: 65px;" Text="1" runat="server" />
            </td>
            <td>
                <b>Taxable:</b>
            </td>
            <td>
                <asp:CheckBox ID='itemTaxed' runat="server" />
            </td>
            <td colspan='2'>
                <asp:Button ID="addNonBarcode" Text='Add' runat='server' OnClick="addNonBarcode_Click" />
            </td>
        </tr>
        <tr>
            <td colspan='3' class='reg'>
                <table width='100%'>
                    <tr>
                        <td>
                            <b>Payment by:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="paymentMethod" runat="server" ValidationGroup="shoppingCart">
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
            <td colspan='3' class='reg'>
                <table width='100%'>
                    <tr>
                        <td valign='top'>
                            Trade Number<br />
                            (if applicable):
                        </td>
                        <td valign='top'>
                            <asp:TextBox ID="tradeNumber" Style="width: 75px;" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign='top'>
                            Employee ID:&nbsp;&nbsp;
                        </td>
                        <td valign='top'>
                            <asp:TextBox ID='employeeId' Style="width: 75px;" runat="server" ValidationGroup="shoppingCart" />
                        </td>
                    </tr>
                    <tr>
                        <td valign='top'>
                            Date:&nbsp;&nbsp;
                        </td>
                        <td valign='top'>
                            <asp:TextBox ID='transactionDate' Style="width: 75px;" runat="server" ValidationGroup="shoppingCart" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan='5'>
                Note:
                <asp:TextBox ID='otherNote' runat='server' /><br />
            </td>
            <td align="right" class='reg'>
                <asp:Button ID='checkOut' Text='Checkout' runat='server' OnClick="checkOut_Click" CausesValidation="true" ValidationGroup="shoppingCart" /><br />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlComplete" runat="server" style="display:none;">
        <asp:Literal ID="litComplete" runat="server" />
    </asp:Panel>
</asp:Content>
