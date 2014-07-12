<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HOTTropicalTans.admin._default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=transactionStartDate.ClientID%>").datepicker();
            $("#<%=transactionEndDate.ClientID%>").datepicker();
            $("#<%=productCountDate.ClientID%>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <asp:Panel ID="loginPanel" runat="server">
            <table class="tanning">
                <thead>
                    <tr>
                        <th>Please Login To Access:
                        </th>
                    </tr>
                </thead>
                <tr>
                    <td>
                        <asp:TextBox ID="adminLoginPassword" runat='server' TextMode='Password' />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="adminLogin" runat="server" Text="Login" OnClick="adminLogin_Click" />
                    </td>
                </tr>
            </table>
    </asp:Panel>
    <asp:Panel ID="adminPanel" runat="server">
        <table class="tanning">
            <thead>
                <tr>
                    <th colspan='2'>Administration</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Button ID="changePasswords" Text="Change Renewal Date/Admin Password" OnClick="changePasswords_Click" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Button ID="siteNotice" Text="Set Website Notice" OnClick="siteNotice_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <br />
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Button ID="combineAccounts" Text="Combine Customer Accounts" OnClick="combineAccounts_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="centerAlignHeader" style="width: 50%;">Products</td>
                    <td class="centerAlignHeader" style="width: 50%;">Transaction Information</td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <b>Edit/Delete Product:</b><br />
                        <asp:DropDownList ID="editItemList" runat="server" />
                        <asp:Button ID="editItem" Text="Edit" OnClick="editItem_Click" runat="server" /><br />
                        <b>Edit/Delete Packages:</b><br />
                        <asp:DropDownList ID="editPackageList" runat="server" />
                        <asp:Button ID="editPackage" Text="Edit" OnClick="editPackage_Click" runat="server" /><br />
                        <b>Edit/Delete Specials:</b><br />
                        <asp:DropDownList ID="editSpecialList" runat="server" />
                        <asp:Button ID="editSpecial" Text="Edit" OnClick="editSpecial_Click" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <b>Full Transaction Log for:<br />
                        <span class="detailInformation">(includes taxable and nontaxable)</span></b><br />
                        <asp:TextBox ID='transactionStartDate' runat='server' /> To <asp:TextBox ID='transactionEndDate' runat='server' />
                        <br />
                        <b>Totals only: </b><asp:CheckBox ID="totalsOnly" runat="server" />
                        <br />
                        <asp:Button ID="viewFullTransaction" Text="Go" OnClick="viewFullTransaction_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Button ID="addProduct" Text="Add Product" OnClick="addProduct_Click" runat="server" />
                        <asp:Button ID="addPackage" Text="Add Package" OnClick="addPackage_Click" runat="server" />
                        <asp:Button ID="addSpecial" Text="Add Special" OnClick="addSpecial_Click" runat="server" />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Button ID="fullInventory" Text="Full Current Inventory" OnClick="fullInventory_Click" runat="server" />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Product Counts for:</b><br />
                        <asp:TextBox ID="productCountDate" runat='server' />
                        <asp:Button ID="productCount" Text="Go" OnClick="productCount_Click" runat="server" />
                    </td>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="centerAlignHeader" colspan='2'>Salon Information</td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <b>Bed Information</b><br />
                        <asp:TextBox ID='bedDate' runat='server' />
                        <asp:Button ID="bedInformation" Text="Go" OnClick="bedInformation_Click" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <b>Edit/Delete Bed:</b><br />
                        <asp:DropDownList ID="editBedList" runat="server" />
                        <asp:Button ID="editBed" Text="Edit Bed" OnClick="editBed_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Button ID="editHours" Text="Edit Hours" OnClick="editHours_Click" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Button ID="addBed" Text="Add Bed" OnClick="addBed_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="centerAlignHeader" colspan='2'>Employee Information</td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <b>Employee Scheduled Hours</b><br />
                        <asp:DropDownList ID="employeeScheduleList" runat='server' />
                        <asp:Button ID="employeeSchedule" Text="Go" OnClick="employeeSchedule_Click" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <b>Edit/Delete Employee</b><br />
                        <asp:DropDownList ID="editEmployeeList" runat='server' />
                        <asp:Button ID="editEmployee" Text="Go" OnClick="editEmployee_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <b>Employee Worked Hours</b><br />
                        <asp:DropDownList ID="employeeWorkedList" runat='server' />
                        <asp:Button ID="employeeWorked" Text="Go" OnClick="employeeWorked_Click" runat="server" />
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Button ID="addEmployee" Text="Add Employee" OnClick="addEmployee_Click" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <br />
                    </td>
                    <td>
                        <asp:Button ID="notesFromEmployees" Text="Notes From Employees" OnClick="notesFromEmployees_Click" runat="server" /></td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <br />
                    </td>
                    <td>
                        <asp:Button ID="notesToEmployees" Text="Notes To Employees" OnClick="notesToEmployees_Click" runat="server" /></td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
</asp:Content>