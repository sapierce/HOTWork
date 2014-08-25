<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Misc.aspx.cs" Inherits="PublicWebsite.Misc"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <table style="text-align: center;">
        <tr>
            <td style="text-align: center;">
                <div id="LOT1">
                    <a href="<%= HOTBAL.TansConstants.LOTION_PUBLIC_URL %>" class="center">Lotions</a> |
                        <a href="<%= HOTBAL.TansConstants.LIP_BALM_PUBLIC_URL %>" class="center">Lip Balms</a> |
                        <a href="<%= HOTBAL.TansConstants.GIFT_BAG_PUBLIC_URL %>" class="center">Gift Bags</a> |
                        Other Accessories<br />
                </div>
                <div id="LOT2">
                    <table style="width: 400px;">
                        <asp:Literal ID="miscAccessories" runat="server" />
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>