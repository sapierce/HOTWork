<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="HOTSelfDefense._Default" MasterPageFile="~/HOTSelfDefense.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Add the datepicker function to the schedule date
            $("#<%=scheduleDate.ClientID%>").datepicker();
        });

        // When the changeDate button is pressed
        $("#<%= this.changeDate.ClientID %>").click(function () {
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
    </script>
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <!-- Display errors associated with validating Class information -->
    <asp:Panel ID="panError" runat="server" CssClass="ui-state-error" Style="display: none">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>The following errors were found:</strong>
            <asp:ValidationSummary ID="scheduleValidation" runat="server" CssClass="ui-state-error-text"
                ShowSummary="true" ValidationGroup="changeSchedule" ShowMessageBox="false" EnableClientScript="true" Style="text-align: left" ForeColor="" />
        </p>
        <span></span>
    </asp:Panel>

    <!-- Date Validation -->
    <asp:RequiredFieldValidator ID="dateRequired" Display="None" runat="server" ControlToValidate="changeDate" ErrorMessage="Please select a schedule date." EnableClientScript="true" SetFocusOnError="true" ValidationGroup="changeSchedule" />
    
    <div style="text-align: center; margin: auto;">
        <!-- Select what date we are looking at on the schedule-->
        <asp:TextBox ID="scheduleDate" runat="server" />&nbsp;
        <!-- Submit to change the date on the schedule -->
        <asp:Button ID="changeDate" runat="server" Text="Go to Date" OnClick="changeDate_Click" CausesValidation="true" ValidationGroup="changeSchedule" CssClass="ui-button" />
    </div>
    <br />
    <table class="defense" style="margin: auto; width: 50%;">
        <thead>
            <tr>
                <th style="width: 30%;">Time</th>
                <th>Class Name</th>
            </tr>
        </thead>
        <!-- Output a schedule of classes and lessons for the given date -->
        <asp:Literal ID="outputSchedule" runat="server" />
    </table>
</asp:Content>