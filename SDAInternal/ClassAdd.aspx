<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ClassAdd.aspx.cs" Inherits="HOTSelfDefense.ClassAdd" MasterPageFile="~/HOTSelfDefense.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Allow the beginDate to use the datepicker
            $("#<%=beginDate.ClientID%>").datepicker();

            // When the addClass button is pressed
            $("#<%= this.addClass.ClientID %>").click(function () {
                // Is the page valid?
                if (!Page_IsValid) {
                    // Display the error messages
                    $("#<%= this.panClassError.ClientID %>").dialog({
                        resizable: false,
                        width: 420,
                        modal: true
                    });
                }
            });

            // When the addLesson button is pressed
            $("#<%= this.addLesson.ClientID %>").click(function () {
                // Is the page valid?
                if (!Page_IsValid) {
                    // Display the error messages
                    $("#<%= this.panLessonError.ClientID %>").dialog({
                        resizable: false,
                        width: 420,
                        modal: true
                    });
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <!-- Display errors associated with validating Class information -->
    <asp:Panel ID="panClassError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="classValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="addClass" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Display errors associated with validating Lesson information -->
    <asp:Panel ID="panLessonError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="lessonValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="addLesson" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Class Validation -->
    <asp:RequiredFieldValidator ID="artClassRequired" Display="None" runat="server" ControlToValidate="artFirst" ErrorMessage="Please enter at least one art." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addClass" InitialValue="0" />
    <asp:RequiredFieldValidator ID="beginDateClassRequired" Display="None" runat="server" ControlToValidate="beginDate" ErrorMessage="Please enter a beginning date." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addClass" />
    <asp:RequiredFieldValidator ID="startTimeClassRequired" Display="None" runat="server" ControlToValidate="startTime" ErrorMessage="Please enter a start time." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addClass" InitialValue="0" />
    <asp:RequiredFieldValidator ID="instructorClassRequired" Display="None" runat="server" ControlToValidate="instructor" ErrorMessage="Please select an instructor." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addClass" InitialValue="0" />
    <asp:RequiredFieldValidator ID="classTitleRequired" Display="None" runat="server" ControlToValidate="classTitle" ErrorMessage="Please enter a class title." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addClass" />
    <asp:RegularExpressionValidator ID="beginDateClassExpression" Display="None" runat="server" ControlToValidate="beginDate" ErrorMessage="Begin Date is not formatted correctly (MM-DD-YYYY)" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$" ValidationGroup="addClass" />

    <!-- Lesson Validation -->
    <asp:RequiredFieldValidator ID="artLessonRequired" Display="None" runat="server" ControlToValidate="artFirst" ErrorMessage="Please enter at least one art." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addLesson" InitialValue="0" />
    <asp:RequiredFieldValidator ID="beginDateLessonRequired" Display="None" runat="server" ControlToValidate="beginDate" ErrorMessage="Please enter a beginning date." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addLesson" />
    <asp:RequiredFieldValidator ID="startTimeLessonRequired" Display="None" runat="server" ControlToValidate="startTime" ErrorMessage="Please enter a start time." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addLesson" InitialValue="0" />
    <asp:RequiredFieldValidator ID="instructorLessonRequired" Display="None" runat="server" ControlToValidate="instructor" ErrorMessage="Please select an instructor." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addLesson" InitialValue="0" />
    <asp:RequiredFieldValidator ID="studentListRequired" Display="None" runat="server" ControlToValidate="studentList" ErrorMessage="Please select a student." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addLesson" InitialValue="0" />
    <asp:RegularExpressionValidator ID="beginDateLessonExpression" Display="None" runat="server" ControlToValidate="beginDate" ErrorMessage="Begin Date is not formatted correctly (MM-DD-YYYY)" EnableClientScript="true" SetFocusOnError="true" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$" ValidationGroup="addLesson" />

    <table class="defense">
        <tr>
            <th colspan="2">Add Class</th>
        </tr>
        <tr>
            <td class="rightAlignHeader">Class Art:</td>
            <td>
                <!-- First or only art for this class or lesson -->
                <asp:DropDownList ID="artFirst" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Secondary Class Art:</td>
            <td>
                <!-- Optional second art for this class or lesson -->
                <asp:DropDownList ID="artSecond" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Begin Date:</td>
            <td>
                <!-- On what date does this class or lesson begin? -->
                <asp:TextBox ID="beginDate" runat="server" Width="105" OnTextChanged="beginDate_TextChanged" AutoPostBack="true" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Recurring?</td>
            <td>
                <!-- Is this class or lesson recurring? If so, on what day? -->
                <asp:DropDownList ID="recurringClass" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Time:</td>
            <td>
                <!-- What time does this class or lesson begin? -->
                <asp:DropDownList ID="startTime" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Instructor:</td>
            <td>
                <!-- Who is the instructor for this class or lesson? -->
                <asp:DropDownList ID="instructor" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="accordion">
                    <!-- Class -->
                    <h3>Is this a class?</h3>
                    <div>
                        <table class="defense">
                            <tr>
                                <td class="rightAlignHeader">Title:</td>
                                <td>
                                    <!-- What is the class called? -->
                                    <asp:TextBox ID="classTitle" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- Add the class after validation -->
                                    <asp:Button ID="addClass" Text="Add Class" runat="server" OnClick="addClass_Click" CssClass="ui-button" ValidationGroup="addClass" CausesValidation="true" /></td>
                            </tr>
                        </table>
                    </div>
                    <!-- Lesson -->
                    <h3>Is this a lesson?</h3>
                    <div>
                        <table class="defense">
                            <tr>
                                <td class="rightAlignHeader">Student:</td>
                                <td>
                                    <!-- What student is taking this lesson? -->
                                    <asp:DropDownList runat="server" ID="studentList" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <!-- Add the lesson after validation -->
                                    <asp:Button ID="addLesson" Text="Add Lesson" runat="server" OnClick="addLesson_Click" CssClass="ui-button" ValidationGroup="addLesson" CausesValidation="true" /></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <script>
                    // Allow showing/hiding of the class or lesson specific options
                    $("#accordion").accordion({ collapsible: true, active: false });
                </script>
            </td>
        </tr>
    </table>
</asp:Content>