<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="EmployeeInformation.aspx.cs" Inherits="HOTTropicalTans.EmployeeInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">

    <script type="text/javascript">
        $(function () {
            //Hide/Show the feedback messages if they contain data
            if ($("#<%=errorMessage.ClientID%>").text() != "") {
                $("#<%=errorMessagePanel.ClientID%>").css("display", "block");

            }
            else {
                $("#<%=errorMessagePanel.ClientID%>").css("display", "none");
            }
            if ($("#<%=successMessage.ClientID%>").text() != "") {
                $("#<%=successMessagePanel.ClientID%>").css("display", "block");
            }
            else {
                $("#<%=successMessagePanel.ClientID%>").css("display", "none");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <p style="margin: auto;">
        <asp:Panel ID="errorMessagePanel" CssClass="message_error" runat="server">
            <asp:Label ID="errorMessage" runat="server" />
        </asp:Panel>
        <asp:Panel ID="successMessagePanel" CssClass="message_success" runat="server">
            <asp:Label ID="successMessage" runat="server" />
        </asp:Panel>
    </p>
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Employee Information</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td valign="top" class="rightAlignHeader">Employee Name
                </td>
                <td valign="top">
                    <asp:Label ID="employeeName" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top" colspan="2">
                    <asp:Button ID="employeeClockIn" Visible="false" runat="server" OnClick="employeeClockIn_Click" Text="Clock In" />
                    <asp:Button ID="employeeClockOut" Visible="false" runat="server" OnClick="employeeClockOut_Click" Text="Clock Out" ShiftId="0" />
                    <asp:Button ID="productCounts" Visible="true" runat="server" OnClick="productCounts_Click" Text="Product Counts" />
                    <asp:Button ID="changePassword" Visible="true" runat="server" OnClick="changePassword_Click" Text="Change Password" /><br />
                    <asp:Button ID="addHours" Visible="false" runat="server" OnClick="addHours_Click" Text="Add Employee Hours" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <table class="tanning">
        <thead>
            <tr>
                <th style="width: 25%">Worked Hours for
                    <asp:Literal ID="workedRangeText" runat="server" /></th>
                <th style="width: 25%">Scheduled Hours for
                    <asp:Literal ID="scheduledRangeText" runat="server" /></th>
                <th style="width: 25%">Total Sales for
                    <asp:Literal ID="salesRangeText" runat="server" /></th>
                <th style="width: 25%">Notes -
                <asp:Button ID="addNote" runat="server" OnClick="addNote_Click" Text="Send Note" /></th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="width: 25%">
                    <asp:DropDownList ID="workedDateRange" runat="server" OnSelectedIndexChanged="workedDateRange_SelectedIndexChanged" AutoPostBack="true" /></td>
                <td style="width: 25%">
                    <asp:DropDownList ID="scheduledDateRange" runat="server" OnSelectedIndexChanged="scheduledDateRange_SelectedIndexChanged" AutoPostBack="true" /></td>
                <td style="width: 25%">
                    <asp:DropDownList ID="salesDateRange" runat="server" OnSelectedIndexChanged="salesDateRange_SelectedIndexChanged" AutoPostBack="true" /></td>
                <td style="width: 25%">
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 25%; vertical-align:top;">
                    <table class="tanning" style="text-align: center; width: 100%;">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Start Time</th>
                                <th>End Time</th>
                                <th>Hours</th>
                                <th>
                                    <br />
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="workedHours" runat="server" />
                        </tbody>
                    </table>
                </td>
                <td style="width: 25%; vertical-align:top;">
                    <table class="tanning" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Start Time</th>
                                <th>End Time</th>
                                <th>Location</th>
                                <th>
                                    <br />
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="scheduledHours" runat="server" />
                        </tbody>
                    </table>
                </td>
                <td style="width: 25%; vertical-align:top;">
                    <table class="tanning" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Total Sales</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="employeeSales" runat="server" />
                        </tbody>
                    </table>
                </td>
                <td style="width: 25%; vertical-align:top;">
                    <table class="tanning" style="width: 100%;">
                        <thead>
                            <tr>
                                <th>To</th>
                                <th>From</th>
                                <th>Note</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="employeeNotes" runat="server" />
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>