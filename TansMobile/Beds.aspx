<%@ Page Title="Tanning Beds" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Beds.aspx.cs" Inherits="MobileSite.Beds" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        function toggleSectionDisplay(elementId) {
            if (elementId != null) {
                var sectionInfo = document.getElementById(elementId);
                if (sectionInfo != null) {
                    if (sectionInfo.style.display == "none") {
                        sectionInfo.style.display = "";
                    }
                    else {
                        sectionInfo.style.display = "none";
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id='about'>
        <table style="text-align:center;">
            <tr>
                <td style="text-align:center;">
                    <asp:Label ID="errorMessage" runat="server" CssClass="error" />
                    <img src="/images/mystics.gif" border="0" alt="Mystic Tanning Packages" />
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">
                    <a onclick="toggleSectionDisplay('MysticPackagesSection')">More Information ></a>
                    <div id='MysticPackagesSection' style="display: none; font-size: small;">
                        <asp:Label ID="mysticFeatures" runat="server" />
                        <asp:Label ID="mysticPackages" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <img src="/images/super-beds.gif" border="0" alt="Super Bed Tanning Packages" />
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">
                    <a onclick="toggleSectionDisplay('SuperPackagesSection')">More Information ></a>
                    <div id='SuperPackagesSection' style="display: none; font-size: small;">
                        <asp:Label ID="superFeatures" runat="server" />
                        <asp:Label ID="superPackages" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <img src="/images/single-beds.gif" border="0" alt="Single Bed Tanning Packages" />
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">
                    <a onclick="toggleSectionDisplay('SinglePackagesSection')">More Information ></a>
                    <div id='SinglePackagesSection' style="display: none; font-size: small;">
                        <asp:Label ID="singleFeatures" runat="server" />
                        <asp:Label ID="singlePackages" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
