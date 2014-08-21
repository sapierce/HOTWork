<%@ Page Title="" Language="C#" MasterPageFile="../../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="BedInformation.aspx.cs" Inherits="HOTTropicalTans.admin.BedInformation" %>

<asp:Content ID="bedInformationHeader" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="bedInformation" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th class="centerAlignHeader" colspan='3'>Waco Bed Information</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class="centerAlignHeader">Bed</td>
                <td class="centerAlignHeader">Tanners</td>
                <td class="centerAlignHeader">Time</td>
            </tr>
            <asp:Literal ID="bedInformationList" runat="server" />
        </tbody>
    </table>
</asp:Content>