<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="SDAPOS.Cart"
    MasterPageFile="SDAPOS.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <asp:Panel ID="pnlCart" runat="server">
    <p align='center'>
        <asp:Label ID="errorMessage" class="error" runat="server" /></p>
    <asp:ValidationSummary CssClass="validation" ID="vsCart" HeaderText="The following validation errors were found:"
        runat="server" ValidationGroup="shoppingCart" DisplayMode="BulletList" ShowSummary="true" />
    <asp:RegularExpressionValidator ID="revDate" ControlToValidate="transactionDate" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"
        Display="None" runat="server" EnableClientScript="true" ErrorMessage="Date is not formatted correctly (MM-DD-YYYY)."
        ValidationGroup="shoppingCart" />
    <asp:RequiredFieldValidator ID="rfvEmployee" Display="None" runat="server" ControlToValidate="employeeId"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Employee ID is required" />
    <asp:RequiredFieldValidator ID="rfvPayment" Display="None" runat="server" ControlToValidate="paymentMethod"
        EnableClientScript="true" ValidationGroup="shoppingCart" ErrorMessage="Payment Type is required" />
    <table class='bcc' align='center'>
        <tr>
            <td colspan='6' class='lheader'>
                <h3>
                    Order for:
                    <asp:Label ID="customerName" runat="server" /></h3>
            </td>
        </tr>
        <tr>
            <td class='header'>
                &nbsp;
            </td>
            <td class='header'>
                <h5>
                    Description</h5>
            </td>
            <td class='header'>
                <h5>
                    Quantity</h5>
            </td>
            <td class='header'>
                <h5>
                    Price</h5>
            </td>
            <td class='header'>
                <h5>
                    Totals</h5>
            </td>
            <td class='header'>
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
            <td colspan='6' class='lheader'>
                <b>Barcoded item:</b>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <b>Add Item:&nbsp;&nbsp;</b>
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
            <td colspan='6' class='lheader'>
                <b>Non-barcoded item:</b>
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
                            <asp:RadioButtonList ID="paymentMethod" runat="server">
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
                            <asp:TextBox ID='employeeId' Style="width: 75px;" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td valign='top'>
                            Date:&nbsp;&nbsp;
                        </td>
                        <td valign='top'>
                            <asp:TextBox ID='transactionDate' Style="width: 75px;" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" colspan='5' class='reg'>
                Note:
                <asp:TextBox ID='otherNote' runat='server' /><br />
            </td>
            <td align="right" class='reg'>
                <asp:Button ID='checkOut' Text='Checkout' runat='server' OnClick="checkOut_Click" /><br />
            </td>
        </tr>
    </table>
    </asp:Panel>
    <asp:Panel ID="pnlComplete" runat="server" style="display:none;">
        <asp:Literal ID="litComplete" runat="server" />
    </asp:Panel>
</asp:Content>
