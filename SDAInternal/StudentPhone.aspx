<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentPhone.aspx.cs" Inherits="HOTSelfDefense.StudentPhone" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <asp:Panel ID="editPhonePanel" runat="server">
        <table class="defense">
            <tr>
                <th colspan="2">Edit Student Number</th>
            </tr>
            <tr>
                <td class="rightAlignHeader">Number:</td>
                <td>
                    <asp:TextBox ID="editPhoneNumber" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Relationship:</td>
                <td>
                    <asp:TextBox ID="editRelationShip" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="editPhone" runat="server" Text="Edit Number" OnClick="editPhone_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="deletePhone" runat="server" Text="Delete Number" OnClick="deletePhone_Click" /></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="addPhonePanel" runat="server">
        <table class="defense">
            <tr>
                <th colspan="2">Add Student Number</th>
            </tr>
            <tr>
                <td class="rightAlignHeader">Number:</td>
                <td>
                    <asp:TextBox ID="addPhoneNumber" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Relationship:</td>
                <td>
                    <asp:TextBox ID="addRelationship" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="addPhone" runat="server" Text="Add Number" OnClick="addPhone_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>