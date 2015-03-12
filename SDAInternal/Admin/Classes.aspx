<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Classes.aspx.cs" Inherits="HOTSelfDefense.ClassesPage" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="defense" style="margin: auto;">
        <tr>
            <td class="rightAlignHeader">Class Art</td>
            <td>
                <asp:DropDownList ID="sltArtFirst" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Secondary Class Art</td>
            <td>
                <asp:DropDownList ID="sltArtSecond" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Time:</td>
            <td>
                <asp:TextBox ID="txtTime" size="5" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Instructor:</td>
            <td>
                <asp:DropDownList ID="sltInstructor" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Title:</td>
            <td>
                <asp:TextBox runat="server" ID="txtTitle" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Recurring?</td>
            <td>
                <asp:DropDownList ID="sltRecurringClass" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnEdit" runat="server" Text="Edit Class" OnClick="btnEdit_onClick" /><br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnDelete" runat="server" Text="Delete Class" OnClick="btnDelete_onClick" /></td>
        </tr>
    </table>
</asp:Content>