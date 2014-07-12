<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="AppointmentMassageEdit.aspx.cs" Inherits="HOTTropicalTans.AppointmentMassageEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <table class='tanning'>
        <thead>
            <tr>
                <th colspan="2">Edit Massage Appointment</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td class='rightAlignHeader'><b>Customer Name:</b></td>
                <td>
                    <asp:Label ID="customerName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'><b>Date:</b></td>
                <td>
                    <asp:TextBox ID="appointmentDate" Style="width: 105px;" runat="server" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'><b>Time:</b></td>
                <td>
                    <asp:TextBox ID="appointmentTime" Style="width: 105px;" runat="server" />
                </td>
            </tr>
            <tr>
                <td class='rightAlignHeader'><b>Length:</b></td>
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
                    <asp:Button ID="editAppointment" Text="Edit Appointment" OnClick="editAppointment_Click" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>