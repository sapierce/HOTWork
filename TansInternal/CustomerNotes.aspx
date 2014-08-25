<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerNotes.aspx.cs" Inherits="HOTTropicalTans.CustomerNotes" validateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Panel ID='addNote' runat="server">
        <table class='tanning' style="text-align: center;">
            <tr>
                <th colspan='4'>Add a Note</th>
            </tr>
            <tr>
                <td class='centerAlignHeader'>Note</td>
                <td class='centerAlignHeader'>Owes Money</td>
                <td class='centerAlignHeader'>Owed Product</td>
                <td class='centerAlignHeader'>Check Transactions</td>
            </tr>
            <tr>
                <td style="width: 300px;">
                    <asp:TextBox ID='noteText' runat="server" Width="300px" TextMode="MultiLine" /></td>
                <td class="owes" style="width: 50px;">
                    <asp:CheckBox ID="owesMoney" runat="server" /></td>
                <td class="owed" style="width: 50px;">
                    <asp:CheckBox ID="owedProduct" runat="server" /></td>
                <td class="check" style="width: 50px;">
                    <asp:CheckBox ID="checkTransactions" runat="server" /></td>
            </tr>
            <tr>
                <td colspan='4'>
                    <asp:Button ID="addNoteSubmit" Text="Add Note" runat="server" OnClick="addNoteSubmit_OnClick" /></td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="editNote" runat="server">
        <table class='tanning' style="text-align: center;">
            <tr>
                <th colspan='4'>Edit a Note</th>
            </tr>
            <tr>
                <td class='centerAlignHeader'>Note</td>
                <td class='centerAlignHeader'>Owes Money</td>
                <td class='centerAlignHeader'>Owed Product</td>
                <td class='centerAlignHeader'>Check Transactions</td>
            </tr>
            <tr>
                <td style="width: 300px;">
                    <asp:TextBox ID="editNoteText" runat="server" Width="300px" TextMode="MultiLine" /></td>
                <td class="owes" style="width: 50px;">
                    <asp:CheckBox ID="editOwesMoney" runat="server" /></td>
                <td class="owed" style="width: 50px;">
                    <asp:CheckBox ID="editOwedProduct" runat="server" /></td>
                <td class="check" style="width: 50px;">
                    <asp:CheckBox ID="editCheckTransactions" runat="server" /></td>
            </tr>
            <tr>
                <td colspan='4'>
                    <asp:Button ID="editNoteSubmit" Text="Edit Note" runat="server" OnClick="editNoteSubmit_OnClick" /></td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>