<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PackageDetails.aspx.cs"
    Inherits="PublicWebsite.PackageDetails" MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <div id="packages">
        <table cellpadding='5' align='center'>
            <tr>
                <td align="center" valign="top">
                    <asp:Label ID="bedPicture" runat="server" />
                </td>
                <td valign="top" align="left">
                    <asp:Label ID="bedFeatures" runat="server" />
                    <asp:Label ID="errorMessage" CssClass="errorLabel" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                </td>
                <td valign="top">
                    <asp:Label ID="bedPackages" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:Label ID="bedAdditional" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
