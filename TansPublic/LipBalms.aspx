<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LipBalms.aspx.cs" Inherits="PublicWebsite.LipBalms"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="lipBalmContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <table style="text-align: center;">
        <tr>
            <td style="text-align: center;">
                <div id="LOT1">
                    <a href="<%= HOTBAL.TansConstants.LOTION_PUBLIC_URL %>" class="center">Lotions</a> |
                    Lip Balms |
                    <a href="<%= HOTBAL.TansConstants.GIFT_BAG_PUBLIC_URL %>" class="center">Gift Bags</a> |
                    <a href="<%= HOTBAL.TansConstants.MISC_PUBLIC_URL %>" class="center">Other Accessories</a><br>
                </div>
                <table class="tanning" style="width: 400px;">
                    <thead>
                        <tr>
                            <th colspan="4">Lip Balms
                            </th>
                        </tr>
                    </thead>
                    <asp:Literal ID="lipBalms" runat="server" />
                </table>
            </td>
        </tr>
    </table>
</asp:Content>