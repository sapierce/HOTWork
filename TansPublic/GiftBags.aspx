<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftBags.aspx.cs" Inherits="PublicWebsite.GiftBags"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="giftBagContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <table style="text-align: center;">
        <tr>
            <td style="text-align: center;">
                <div id="LOT1">
                    <a href="<%= HOTBAL.TansConstants.LOTION_PUBLIC_URL %>" class="center">Lotions</a> |
                    <a href="<%= HOTBAL.TansConstants.LIP_BALM_PUBLIC_URL %>" class="center">Lip Balms</a> |
                    Gift Bags |
                    <a href="<%= HOTBAL.TansConstants.MISC_PUBLIC_URL %>" class="center">Other Accessories</a><br />
                </div>
                <table class="tanning" style="width: 400px;">
                    <thead>
                        <tr>
                            <th colspan="4">Gift Bags</th>
                        </tr>
                    </thead>
                    <asp:Literal ID="giftBags" runat="server" />
                </table>
            </td>
        </tr>
    </table>
</asp:Content>