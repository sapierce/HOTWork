<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ClassStudentAdd.aspx.cs" Inherits="HOTSelfDefense.ClassStudentAdd" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // When the addStudentLesson button is pressed
            $("#<%= this.addStudentLesson.ClientID %>").click(function () {
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
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <!-- Display errors associated with validating Add information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p><span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
		<strong>The following errors were found:</strong> <asp:ValidationSummary ID="classValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="addLesson" ShowMessageBox="false" EnableClientScript="true" style="text-align:left" ForeColor="" /></p><span>
        </span>
    </asp:Panel>

    <!-- Add Validation -->
    <asp:RequiredFieldValidator ID="studentListRequired" Display="None" runat="server" ControlToValidate="studentList" ErrorMessage="Please select a student." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="addLesson" InitialValue="0" />
    
    <table class="defense">
        <tr>
            <th colspan="2">Add Student to Class</th>
        </tr>
        <tr>
            <td class="rightAlignHeader">Student:</td>
            <td>
                <!-- What student is being added? -->
                <asp:DropDownList runat="server" ID="studentList" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <!-- Add student to lesson after validation -->
                <asp:Button ID="addStudentLesson" Text="Add Student" runat="server" CssClass="ui-button" OnClick="addStudentLesson_Click" CausesValidation="true" ValidationGroup="addLesson" /></td>
        </tr>
    </table>
</asp:Content>