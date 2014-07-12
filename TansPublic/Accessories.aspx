<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Accessories.aspx.cs" Inherits="PublicWebsite.Accessories"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="accessoriesContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <div class="product">
        <table style="text-align: center;">
            <tr>
                <td style="text-align: center;">
                    <a href="<%= HOTBAL.TansConstants.LOTION_PUBLIC_URL %>">
                        <img src="images/Lotions.gif" alt="Lotions" border="0" /></a><br />
                </td>
                <td style="text-align: center;">
                    <a href="<%= HOTBAL.TansConstants.LIP_BALM_PUBLIC_URL %>">
                        <img src="images/LipBalms.gif" alt="Lip Balms" border="0" /></a><br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <a href="<%= HOTBAL.TansConstants.GIFT_BAG_PUBLIC_URL %>">
                        <img src="images/GiftBags.gif" alt="Gift Bags" border="0" /></a><br />
                </td>
                <td style="text-align: center;">
                    <a href="<%= HOTBAL.TansConstants.MISC_PUBLIC_URL %>">
                        <img src="images/Misc.gif" alt="Other Accessories" border="0" /></a><br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>