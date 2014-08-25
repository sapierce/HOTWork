<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAppointment.aspx.cs"
    Inherits="PublicWebsite.MembersArea.AddAppointment" MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <asp:Panel ID="appointment" runat="server">
        <table>
            <tr>
                <td>
                    <div id="appt">
                        <asp:ValidationSummary runat="server" ID="addSummary" HeaderText="The following validation errors were found..." DisplayMode="BulletList" ShowSummary="true" ValidationGroup="addAppointment" />
                        <asp:RequiredFieldValidator ID="hasAppointmentDate" runat="server" ErrorMessage="Please select an appointment date." ControlToValidate="appointmentDate" ValidationGroup="addAppointment" Visible="false" />
                        <asp:RegularExpressionValidator ValidationExpression="^[A-Za-z -]$" ID="validName" runat="server" ErrorMessage="Customer name can only contain A-Z." ControlToValidate="bedType" ValidationGroup="addAppointment" Visible="false" />
                        <asp:RegularExpressionValidator ValidationExpression="^[A-Z]{1,2}$" ID="validBedType" runat="server" ErrorMessage="Bed type can only be alphanumeric characters." ControlToValidate="bedType" ValidationGroup="addAppointment" Visible="false" />
                        <asp:RegularExpressionValidator ValidationExpression="^[1-7M]{1}$" ID="validBed" runat="server" ErrorMessage="Bed preference can only be a single digit." ControlToValidate="bedPreference" ValidationGroup="addAppointment" Visible="false" />
                        <asp:RegularExpressionValidator ValidationExpression="(0*[1-9]|1[012])[- /.](0*[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d" ID="validAppointmentDate" runat="server" ErrorMessage="Appointment date is invalid (MM/DD/YYYY)." ControlToValidate="appointmentDate" ValidationGroup="addAppointment" Visible="false" />
                        <asp:RegularExpressionValidator ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|PM))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" ID="validTime" runat="server" ErrorMessage="Time must be in a valid time format (HH:MM AM/PM)." ControlToValidate="availableTimes" ValidationGroup="addAppointment" Visible="false" />

                        <table style="text-align: center;">
                            <tr>
                                <td colspan='2'>
                                    <h4>Add Appointment for:
											<asp:Label ID="customerName" runat="server" /></h4>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <b>Bed Type:</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="bedType" runat="server" ValidationGroup="addAppointment" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <b>Date:</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="appointmentDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="bedType_SelectedIndexChanged" ValidationGroup="addAppointment" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">Please note that selecting a bed does not guarentee that you will be placed in that bed.
										<asp:Label ID="packageNotice" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <b>Bed Preference:</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="bedPreference" runat="server" AutoPostBack="true" OnSelectedIndexChanged="bedPreference_SelectedIndexChanged" ValidationGroup="addAppointment" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <b>Available Times:</b>
                                </td>
                                <td>
                                    <asp:DropDownList ID="availableTimes" runat="server" ValidationGroup="addAppointment" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan='2' style="text-align: right;">
                                    <asp:CheckBox ID="emailReminder" runat="server" Text="Would you like an e-mail reminder of your appointment?" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan='2' style="text-align: right;">
                                    <asp:Button ID='scheduleAppointment' Text='Schedule Appointment' runat='server' OnClick="scheduleAppointment_onClick" ValidationGroup="addAppointment" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="confirmation" runat="server">
        <asp:Label ID="confirmationNote" runat="server" />
        <br />
        <asp:Label ID="addToCalendars" runat="server" />
    </asp:Panel>
</asp:Content>