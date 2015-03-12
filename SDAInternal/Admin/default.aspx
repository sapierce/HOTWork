<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HOTSelfDefense._default" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="defense">
        <thead>
            <tr>
                <th style="width: 50%;">Class Information</th>
                <th style="width: 50%;">Sales/Inventory</th>
            </tr>
        </thead>
        <tr>
            <td>
                <!-- Edit/Delete Classes -->
                <b>Edit/Delete Repeating Class:</b><br />
                <asp:DropDownList ID="courseSelection" runat="server" />
                <asp:Button ID="editCourse" OnClick="editCourse_Click" runat="server" Text="Go" />
            </td>
            <td>
                <!-- Full Transaction Log -->
                <b>Full Transaction Log for:<br />
                    <span class="detailInformation">(includes taxable and nontaxable)</span></b><br />
                <asp:TextBox ID="transactionStartDate" runat="server" Width="75" MaxLength="10" />
                To
                <asp:TextBox ID="transactionEndDate" runat="server" Width="75" MaxLength="10" />
                <br />
                <b>Totals only: </b>
                <asp:CheckBox ID="totalsOnly" runat="server" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="viewFullTransaction" Text="Go" OnClick="viewFullTransaction_Click" runat="server" /><br />
            </td>
        </tr>
        <tr>
            <td>
                
                <!-- Add/Edit/Delete Instructors -->
                <asp:Button ID="addInstructor" OnClick="addInstructor_Click" runat="server" Text="Add Instructor" /><br />
                <br />
                <b>Edit/Delete Instructor:</b><br />
                <asp:DropDownList ID="instructorSelection" runat="server" />
                <asp:Button ID="editInstructor" OnClick="editInstructor_Click" runat="server" Text="Go" />
            </td>
            <td>
                <!-- Add/Edit/Delete Inventory -->
                <asp:Button ID="addItem" OnClick="addItem_Click" runat="server" Text="Add Item" /><br />
                <br />
                <b>Edit/Delete Item:</b><br />
                <asp:DropDownList ID="itemSelection" runat="server" />
                <asp:Button ID="editItem" OnClick="editItem_Click" runat="server" Text="Go" /><br />
                <br />
                <!-- Current Inventory -->
                <asp:Button ID="viewInventory" OnClick="viewInventory_Click" runat="server" Text="Current Inventory" />
            </td>
        </tr>
        <tr>
            <td class="centerAlignHeader" style="width: 50%;">Art Information</td>
            <td class="centerAlignHeader" style="width: 50%;">Reports</td>
        </tr>
        <tr>
            <td>
                <!-- Add/Edit/Delete Art -->
                <asp:Button ID="addArt" OnClick="addArt_Click" runat="server" Text="Add Art" /><br />
                <br />
                <b>Edit/Delete Art:</b><br />
                <asp:DropDownList ID="artSelection" runat="server" />
                <asp:Button ID="editArt" OnClick="editArt_Click" runat="server" Text="Go" />
            </td>
            <td>
                <!-- Birthdays -->
                <b>Birthdays in the next</b>
                <asp:TextBox ID="birthdayDays" runat="server" Width="20" MaxLength="2" />
                day(s)
                <asp:Button ID="birthdayReport" OnClick="birthdayReport_Click" runat="server" Text="Go" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- Add/Edit/Delete Belt -->
                <asp:Button ID="addBelt" OnClick="addBelt_Click" runat="server" Text="Add Belt" /><br />
                <br />
                <b>Edit/Delete Belt:</b><br />
                <asp:DropDownList ID="beltSelection" runat="server" />
                <asp:Button ID="editBelt" OnClick="editBelt_Click" runat="server" Text="Go" />
            </td>
            <td>
                <!-- Belts Passed -->
                <b>Belts Passed Between:</b>&nbsp;&nbsp;
                <asp:TextBox ID="passBeginDate" runat="server" Width="75" MaxLength="10" />
                -
                <asp:TextBox ID="passEndDate" runat="server" Width="75" MaxLength="10" />
                <asp:Button ID="beltsPassed" OnClick="beltsPassed_Click" runat="server" Text="Go" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- Add/Edit/Delete Tip -->
                <asp:Button ID="addTip" OnClick="addTip_Click" runat="server" Text="Add Tip" /><br />
                <br />
                <b>Edit/Delete Tip:</b><br />
                <asp:DropDownList ID="tipSelection" runat="server" />
                <asp:Button ID="editTip" OnClick="editTip_Click" runat="server" Text="Go" />
            </td>
            <td>
                <!-- Last Tip -->
                <b>Students on last tip as of:</b>
                <asp:TextBox ID="lastTipDate" runat="server" Width="75" MaxLength="10" />
                <asp:Button ID="lastTip" OnClick="lastTip_Click" runat="server" Text="Go" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- Add/Edit/Delete Term -->
                <asp:Button ID="addTerm" OnClick="addTerm_Click" runat="server" Text="Add Term" /><br />
                <br />
                <b>Edit/Delete Term:</b><br />
                <asp:DropDownList ID="termSelection" runat="server" />
                <asp:Button ID="editTerm" OnClick="editTerm_Click" runat="server" Text="Go" /><br />
                <br />
            </td>
            <td>
                <!-- Class Attendance -->
                <b>Class attendance for:</b>
                <asp:TextBox ID="attendanceDate" runat="server" Width="75" MaxLength="10" />
                <asp:Button ID="classAttendance" OnClick="classAttendance_Click" runat="server" Text="Go" /><br />
                <br />
                <!-- All Active Students -->
                <asp:Button ID="activeStudents" OnClick="activeStudents_Click" runat="server" Text="All Active Students" />
            </td>
        </tr>
    </table>
</asp:Content>