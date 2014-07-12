<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudentArtHistory.aspx.cs" Inherits="HOTSelfDefense.StudentArtHistory" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="defense">
        <thead>
            <tr>
                <th colspan="4">Art History for:
                <asp:Label ID="studentName" runat="server" />
                </th>
            </tr>
        </thead>
        <tr>
            <td class="centerAlignHeader">Date</td>
            <td class="centerAlignHeader">Art</td>
            <td class="centerAlignHeader">Belt</td>
            <td class="centerAlignHeader">Tip</td>
        </tr>
        <asp:Label ID="currentHistory" runat="server" />
        <asp:Label ID="previousHistory" runat="server" />
    </table>
</asp:Content>