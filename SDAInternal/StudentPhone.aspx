<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentPhone.aspx.cs" Inherits="HOTSelfDefense.StudentPhone" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align='center'>
        <asp:Label ID="lblError" class="error" runat="server" /></p>
    <asp:Panel ID="editPhonePanel" runat="server">
        <table class='bcc' align='center'>
            <tr>
                <td class='header' colspan='2'>Edit Student Number</td>
            </tr>
            <tr>
                <td class='rheader'>Number:</td>
                <td class='reg'>
                    <asp:TextBox ID='editPhoneNumber' runat="server" /></td>
            </tr>
            <tr>
                <td class='rheader'>Relationship:</td>
                <td class='reg'>
                    <asp:TextBox ID='editRelationShip' runat="server" /></td>
            </tr>
            <tr>
                <td class='reg' colspan='2'>
                    <asp:Button ID="editPhone" runat="server" Text="Edit Number" OnClick="editPhone_Click" />
                </td>
            </tr>
            <tr>
                <td class='reg' colspan='2'>
                    <asp:Button ID="deletePhone" runat="server" Text="Delete Number" OnClick="deletePhone_Click" /></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="addPhonePanel" runat="server">
        <table class='bcc' align='center'>
            <tr>
                <td class='header' colspan='2'>Add Student Number</td>
            </tr>
            <tr>
                <td class='rheader'>Number:</td>
                <td class='reg'>
                    <asp:TextBox ID='addPhoneNumber' runat="server" /></td>
            </tr>
            <tr>
                <td class='rheader'>Relationship:</td>
                <td class='reg'>
                    <asp:TextBox ID='addRelationship' runat="server" /></td>
            </tr>
            <tr>
                <td class='reg' colspan='2'>
                    <asp:Button ID="addPhone" runat="server" Text="Add Number" OnClick="addPhone_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>