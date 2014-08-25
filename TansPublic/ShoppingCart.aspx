<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="PublicWebsite.ShoppingCart"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <table>
        <tr>
            <td style="text-align:center;">
                <table class='main' style="text-align:center;">
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
                    <asp:Label ID="shoppingCartOutput" runat="server" />
                    <tr>
                        <td colspan='6'>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan='6' class='reg'>
                            <asp:Button ID='inStore' Text='Checkout In Store' runat='server' OnClick="onClick_inStore" />&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton runat="server" ID="btnPaypal" OnClick="onClick_online" AlternateText="checkout with paypal" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>