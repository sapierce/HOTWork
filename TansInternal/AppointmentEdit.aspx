﻿<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="AppointmentEdit.aspx.cs" Inherits="HOTTropicalTans.AppointmentEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Allow the appointmentDate to use the datepicker
            $("#<%=appointmentDate.ClientID%>").datepicker();

            // When the appointmentEdit button is pressed
            $("#<%= this.editAppointment.ClientID %>").click(function () {
                // Is the page valid?
                if (!Page_IsValid) {
                    // Display the error messages
                    $("#<%= this.panError.ClientID %>").dialog({
                        resizable: false,
                        width: 420,
                        modal: true
                    });
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <!-- Display errors associated with validating Appointment information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="apptValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="editAppt" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>
    <!-- Appointment Validation -->
    <asp:RequiredFieldValidator ID="dateRequired" Display="None" runat="server" ControlToValidate="appointmentDate" ErrorMessage="Please enter an appointment date." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editAppt" InitialValue="" />
    <asp:RequiredFieldValidator ID="bedRequired" Display="None" runat="server" ControlToValidate="appointmentBed" ErrorMessage="Please select a bed." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editAppt" InitialValue="" />
    <asp:RequiredFieldValidator ID="timeRequired" Display="None" runat="server" ControlToValidate="appointmentTime" ErrorMessage="Please select a time." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="editAppt" />
   
    <table class='tanning'>
        <thead>
            <tr>
                <th colspan="2">Edit Tanning Appointment</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class='rightAlignHeader'>Customer Name:</td>
                <td>
                    <asp:Label ID="customerName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Date:</td>
                <td>
                    <asp:TextBox ID="appointmentDate" Style="width: 105px;" runat="server" ValidationGroup="editAppt" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Bed:</td>
                <td>
                    <asp:DropDownList ID='appointmentBed' runat="server" ValidationGroup="editAppt" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Time:</td>
                <td>
                    <asp:DropDownList ID="appointmentTime" runat="server" ValidationGroup="editAppt" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Length:</td>
                <td>
                    <asp:TextBox ID="appointmentLength" Text="0" Style="width: 35px;" runat="server" ValidationGroup="editAppt" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Swap?</td>
                <td>
                    <asp:CheckBox ID="swapAppointments" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:HiddenField ID="customerID" runat="server" />
                    <asp:Button ID="editAppointment" Text="Edit Appointment" OnClick="editAppointment_Click" runat="server" ValidationGroup="editAppt" CausesValidation="true" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>