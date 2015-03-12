<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentArtEdit.aspx.cs" Inherits="HOTSelfDefense.StudentArtEdit" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="studentArtEditHead" ContentPlaceHolderID="headerPlaceHolder" runat="server" />
<asp:Content ID="studentArtEditMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="defense">
        <thead>
            <tr>
                <th colspan="2">Edit Student Art</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="rightAlignHeader">Art:</td>
                <td>
                    <asp:DropDownList ID="studentArt" runat="server" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Belt:</td>
                <td>
                    <asp:DropDownList ID="studentBelt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="studentBelt_SelectedIndexChanged" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">
                    <asp:Label ID="tipOrClass" runat="server" CssClass="rightAlignHeader" /></td>
                <td>
                    <asp:DropDownList ID="studentTip" runat="server" Visible="false" /><asp:TextBox ID="classCount" runat="server" Visible="false" /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Authorizer:</td>
                <td>
                    <asp:TextBox ID="artUpdater" runat="server" /></td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:Button ID="editArt" runat="server" Text="Update Art" OnClick="editArt_Click" /></td>
            </tr>
        </tbody>
    </table>
</asp:Content>