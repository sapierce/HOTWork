<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="HOTSelfDefense._Default" MasterPageFile="HOTSelfDefense.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Add the datepicker function to the schedule date
            $("#<%=scheduleDate.ClientID%>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <div style="text-align: center; margin: auto;">
        <!-- Select what date we are looking at on the schedule-->
        <asp:TextBox ID="scheduleDate" runat="server" class="scheduleDate" />&nbsp;
        <!-- Submit to change the date on the schedule -->
        <asp:Button ID="changeDate" runat="server" Text="Go to Date" OnClick="changeDate_Click" CssClass="ui-button" />
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

        $(".scheduleDate").rules("add", {
            required: true,
            date: true,
            messages: {
                required: "Please enter in a schedule date.",
                maxlength: 10,
                date: "Entered information must be in date format."
            }
        });
    </script>
</asp:Content>