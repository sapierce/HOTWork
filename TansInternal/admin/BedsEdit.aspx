<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="BedsEdit.aspx.cs" Inherits="HOTTropicalTans.admin.BedsEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        function deleteBed() {
            if (confirm("Are you sure you want to delete this bed?")) {
                //document.location = "<% =HOTBAL.TansConstants.CUSTOMER_INFO_DELETE_INTERNAL_URL %>?ID=<% =Request.QueryString["ID"] %>";
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Edit Bed Information</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Bed Number:</td>
                <td>
                    <asp:TextBox ID="bedNumber" runat="server" MaxLength="1" /><br />
                    <span class="detailInformation">'M' for Mystic</span>
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Website Bed Description:</td>
                <td>
                    <asp:TextBox ID="bedDescription" runat="server" MaxLength="25" /><br />
                    <span class="detailInformation">IE - 20 minute bed, 12 minute bed</span>
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Bed Type:</td>
                <td>
                    <asp:DropDownList ID="bedType" runat="server">
                        <asp:ListItem Value="BB">Super Bed</asp:ListItem>
                        <asp:ListItem Value="SB">Regular Bed</asp:ListItem>
                        <asp:ListItem Value="MY">Mystic</asp:ListItem>
                        <asp:ListItem Value="OT">Other Bed</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Allow scheduling?</td>
                <td>
                    <asp:CheckBox ID="bedDisplay" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button Text="Edit Bed" runat="server" ID="editBed" OnClick="editBed_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button Text="Delete Bed" runat="server" ID="deleteBed" OnClick="deleteBed_Click" /></td>
            </tr>
        </tbody>
    </table>
</asp:Content>