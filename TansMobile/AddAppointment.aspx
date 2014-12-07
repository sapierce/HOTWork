<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAppointment.aspx.cs"
    Inherits="MobileSite.AddAppointment" MasterPageFile="Site.Master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="appointment">
        <asp:Panel ID="pnlAppointment" runat="server">
            <table>
                <tr>
                    <td>
                        <div id="appt" data-enhance="false" data-ajax="false">
                            <asp:Label ID="lblError" runat="server" CssClass="errorLabel" />
                            <table>
                                <tr>
                                    <td colspan='2'>
                                        <h4>Add Appointment for:
                                            <asp:Label ID="lblCustName" runat="server" /></h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='2'>
                                        <asp:Label ID="unavailableMessage" runat="server" CssClass="errorLabel" />
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td class="label">
                                        Date:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlDate" runat="server" data-mini="true" data-theme="a" data-icon="arrow-d" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Bed Type:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlBed" runat="server" class="bedTarget" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddlBed_SelectedIndexChanged" data-mini="true" data-theme="a" data-icon="arrow-d" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style='text-align: left; font-size: small;'>Please note that selecting a bed does not guarantee that you will be placed in that bed.
										    <asp:Label ID="lblPackage" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Bed Preference:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlPreference" runat="server" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddlPreference_SelectedIndexChanged" data-mini="true" data-theme="a" data-icon="arrow-d"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="label">
                                        Available Times:
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlTimes" runat="server" data-mini="true" data-theme="a" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='2'>
                                        <asp:CheckBox ID="chkEmailRemind" runat="server" Text="Would you like an e-mail reminder of your appointment?" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan='2' style="text-align:right;">
                                        <asp:Button ID='btnSubmit' Text='Schedule Appointment' runat='server' OnClick="btnSubmit_onSubmit" CausesValidation="true" ValidationGroup="appointmentTime" data-mini="true" data-theme="a" />
                                    </td>
                                </tr>--%>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--<asp:Panel ID="pnlConfirmation" runat="server">
            <asp:Label ID="lblConfirmation" runat="server" />
            <br />
            <asp:Label ID="lblAddToCalendars" runat="server" />
    </asp:Panel>--%>
    </div>
</asp:Content>