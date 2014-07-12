<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="AppointmentMassageAdd.aspx.cs" Inherits="HOTTropicalTans.AppointmentMassageAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=appointmentDate.ClientID%>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th>Add Massage Appointment</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Last Name:</td>
            <td>
                <asp:TextBox ID="lastName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">First Name:</td>
            <td>
                <asp:TextBox ID="firstName" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Date:</td>
            <td>
                <asp:TextBox ID="appointmentDate" Style="width: 105px;" runat="server" OnTextChanged="appointmentDate_TextChanged" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Time:</td>
            <td>
                <asp:DropDownList ID="appointmentTime" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Length:</td>
            <td>
                <asp:DropDownList runat="server" ID="appointmentLength">
                    <asp:ListItem Value="30">30 minutes</asp:ListItem>
                    <asp:ListItem Value="60">1 hour</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:HiddenField ID="customerID" runat="server" />
                <asp:Button ID="addAppointment" Text="Add Appointment" OnClick="addAppointment_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>