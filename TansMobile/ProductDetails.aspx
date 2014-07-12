<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="MobileSite.ProductDetails"
    MasterPageFile="Site.Master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="text-align: center;">
        <tr>
            <td style="text-align: center;">

                <div id="LOT2">
                    <table style="width: 300px;">
                        <asp:Literal ID="productInformation" runat="server" />
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>