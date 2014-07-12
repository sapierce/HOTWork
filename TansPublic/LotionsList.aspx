<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LotionsList.aspx.cs" Inherits="PublicWebsite.LotionsList"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <div class="product">
    <table style="text-align: center;">
        <tr>
            <td style="text-align: center;">
                <div id="LOT1">
                    Lotions | <a href="LipBalms.aspx" class="center">Lip Balms</a> | <a href="GiftBags.aspx" class="center">Gift Bags</a>
                    | <a href="Misc.aspx" class="center">Other Accessories</a><br />
                    <asp:Literal ID="litLotions" runat="server" />
                </div>
                <div id="LOT2">
                    <table style="width: 400px;">
                        <asp:Literal ID="lotionsList" runat="server" />
                    </table>
                </div>
            </td>
        </tr>
    </table>
        </div>
</asp:Content>