<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="BedsAdd.aspx.cs" Inherits="HOTTropicalTans.admin.BedsAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table style="text-align:center;">
        <tr>
            <td style="vertical-align:top;">
                <asp:Label ID="errorMessage" CssClass="error" runat="server" />
                <table class='standardTable' style="text-align:center;">
                    <tr>
                        <td colspan='2' class='standardHeader'>Add Bed</td>
                    </tr>
                    <tr>
                        <td class='rightAlignHeader'>Bed Number:</td>
                        <td class='standardField'>
                            <asp:TextBox ID="bedNumber" runat="server" MaxLength="1" /></td>
                    </tr>
                    <tr>
                        <td class='standardField' colspan='2'><span style="font-size: smaller">"M" for Mystic</span></td>
                    </tr>
                    <tr>
                        <td class='rightAlignHeader'>Website Bed Description:</td>
                        <td class='standardField'>
                            <asp:TextBox ID="bedDescription" runat="server" MaxLength="25" /></td>
                    </tr>
                    <tr>
                        <td class='standardField' colspan='2'><span style="font-size: smaller">IE - 20 minute bed, 12 minute bed</span></td>
                    </tr>
                    <tr>
                        <td class='rightAlignHeader'>Bed Type:</td>
                        <td class='standardField'>
                            <asp:DropDownList ID="bedType" runat="server">
                                <asp:ListItem Value='BB'>Super Bed</asp:ListItem>
                                <asp:ListItem Value='SB'>Regular Bed</asp:ListItem>
                                <asp:ListItem Value='MY'>Mystic</asp:ListItem>
                                <asp:ListItem Value='OT'>Other Bed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class='rightAlignHeader'>Allow scheduling?</td>
                        <td class='standardField'>
                            <asp:CheckBox ID="bedDisplay" runat="server" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan='2'>
                <asp:Button Text="Add Bed" runat="server" ID="addBed" OnClick="addBed_OnClick" /></td>
        </tr>
    </table>
</asp:Content>