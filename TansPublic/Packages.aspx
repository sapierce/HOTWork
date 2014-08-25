<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Packages.aspx.cs" Inherits="PublicWebsite.Packages"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <div id='about'>
        <table style="text-align:center;">
            <tr>
                <td style="text-align:right;">
                    <a href="PackageDetails.aspx?Type=MY">
                        <img src="images/mystics.gif" border="0" alt="Mystic Tanning Packages" width="200" height="181" /></a>
                </td>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <br />
                </td>
                <td>
                    <a href="PackageDetails.aspx?Type=BB">
                        <img src="images/super-beds.gif" border="0" alt="Super Bed Tanning Packages" /></a>
                </td>
            </tr>
            <tr>
                <td style="text-align:right;">
                    <a href="PackageDetails.aspx?Type=SB">
                        <img src="images/single-beds.gif" border="0" alt="Single Bed Tanning Packages" /></a>
                </td>
                <td>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
