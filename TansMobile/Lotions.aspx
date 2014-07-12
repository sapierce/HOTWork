<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Lotions.aspx.cs" Inherits="MobileSite.Lotions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="product">
        <table style="text-align: center;">
            <tr>
                <td style="text-align: center;">
                    <div id="LOT2">
                        <table style="width: 300px;">
                            <asp:Literal ID="lotionsList" runat="server" />
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
