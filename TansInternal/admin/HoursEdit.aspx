<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="HoursEdit.aspx.cs" Inherits="HOTTropicalTans.admin.HoursEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Label ID="errorMessage" runat="server" CssClass="error" />
    <table style="text-align: center;">
        <tr>
            <td valign="top">
                <table class="tanning" style="text-align: center;">
                    <thead>
                        <tr>
                            <th colspan="3">Waco Tanning Hours</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="rightAlignHeader"><br /></td>
                            <td class="centerAlignHeader">
                                Open</td>
                            <td class="centerAlignHeader">
                                Close</td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">MONDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningMondayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningMondayEndTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">TUESDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningTuesdayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningTuesdayEndTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">WEDNESDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningWednesdayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningWednesdayEndTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">THURSDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningThursdayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningThursdayEndTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">FRIDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningFridayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningFridayEndTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">SATURDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningSaturdayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningSaturdayEndTime" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="rightAlignHeader">SUNDAY</td>
                            <td>
                                <asp:TextBox ID="wacoTanningSundayBeginTime" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="wacoTanningSundayEndTime" runat="server" /></td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td valign="top">
                <%--<table class="standardTable" style="text-align: center;">
                    <tr>
                        <td colspan="3" class="standardHeader">Waco Massage Hours</td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">MONDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageMondayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageMondayEndTime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">TUESDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageTuesdayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageTuesdayEndTime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">WEDNESDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageWednesdayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageWednesdayEndTime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">THURSDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageThursdayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageThursdayEndTime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">FRIDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageFridayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageFridayEndTime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">SATURDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageSaturdayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageSaturdayEndTime" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">SUNDAY</td>
                        <td>
                            <asp:TextBox ID="wacoMassageSundayBeginTime" runat="server" /></td>
                        <td>
                            <asp:TextBox ID="wacoMassageSundayEndTime" runat="server" /></td>
                    </tr>
                </table>--%>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button Text="Update" runat="server" ID="updateHours" OnClick="updateHours_OnClick" /></td>
        </tr>
    </table>
</asp:Content>