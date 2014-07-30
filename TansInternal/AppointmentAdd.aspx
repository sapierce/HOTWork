<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="AppointmentAdd.aspx.cs" Inherits="HOTTropicalTans.AppointmentAdd" %>

<asp:Content ID="addAppointmentHeader" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Allow the appointmentDate to use the datepicker
            $("#<%=appointmentDate.ClientID%>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="addAppointmentContent" ContentPlaceHolderID="Main" runat="server">
    <table class='tanning'>
        <thead>
            <tr>
                <th colspan="2">Add Tanning Appointment</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class='rightAlignHeader'>Last Name:</td>
                <td>
                    <asp:TextBox ID="lastName" runat="server" TabIndex="1" AutoCompleteType="LastName" MaxLength="50" class="lastName" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>First Name:</td>
                <td>
                    <asp:TextBox ID="firstName" runat="server" TabIndex="2" AutoCompleteType="FirstName" MaxLength="50" class="lastName" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Date:</td>
                <td>
                    <asp:TextBox ID="appointmentDate" Style="width: 105px;" runat="server" OnTextChanged="appointmentDate_TextChanged" AutoPostBack="true" MaxLength="10" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Bed:</td>
                <td>
                    <asp:DropDownList ID='appointmentBed' runat="server" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Time:</td>
                <td>
                    <asp:DropDownList ID="appointmentTime" runat="server" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'>Length:</td>
                <td>
                    <asp:TextBox ID="appointmentLength" Text="0" Style="width: 35px;" runat="server" MaxLength="2" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="align-content:center;">
                    <asp:HiddenField ID="customerID" runat="server" />
                    <asp:Button ID="addAppointment" Text="Add Appointment" OnClick="addAppointment_Click" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <script>
        // initialize tooltipster on text input elements
        $('#aspnetForm input[type="text"]').tooltipster({
            trigger: 'custom',
            onlyOne: false,
            position: 'right',
            theme: 'tooltipster-light'
        });

        // initialize tooltipster on select input elements
        $('#aspnetForm select').tooltipster({
            trigger: 'custom',
            onlyOne: false,
            position: 'right',
            theme: 'tooltipster-light'
        });

        $("#aspnetForm").validate({
            errorPlacement: function (error, element) {
                $(element).tooltipster('update', $(error).text());
                $(element).tooltipster('show');
            },
            success: function (label, element) {
                $(element).tooltipster('hide');
            }
        });

        $(".lastName").rules("add", {
            required: true,
            messages: {
                required: "Please enter in a last name.",
                maxlength: 50
            }
        });
        $(".firstName").rules("add", {
            required: true,
            messages: {
                required: "Please enter in a first name.",
                maxlength: 50
            }
        });
        $(".fitzNumber").rules("add", {
            required: true,
            messages: {
                required: "Please select a Fitzpatrick number.",
                range: [0, 6],
                maxlength: 1,
                digits: true
            }
        });
        $(".joinDate").rules("add", {
            required: true,
            date: true,
            messages: {
                required: "Please enter in a join date.",
                maxlength: 10,
                date: "Entered information must be in date format."
            }
        });
        $(".package").rules("add", {
            required: true,
            messages: {
                required: "Please select a tanning package."
            }
        });
        $(".renewalDate").rules("add", {
            required: true,
            date: true,
            messages: {
                required: "Please enter in a renewal date.",
                maxlength: 10,
                date: "Entered information must be in date format."
            }
        });
    </script>
</asp:Content>