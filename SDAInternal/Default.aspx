<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" Inherits="HOTSelfDefense._Default" MasterPageFile="HOTSelfDefense.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="placeholderHeader" runat="server">
    
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-12 text-center center-block">
                <!-- Select what date we are looking at on the schedule-->
                <div class="form-group">
                    <div class="input-group">
                    <label for="scheduleDate">Go to Date: </label>
                    <asp:TextBox id="scheduleDate" runat="server" type="date" CssClass="scheduleDate form-control input-sm" />
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">
                            <span class="glyphicon glyphicon-calendar">
                            </span>
                        </button>
                    </span>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12">
                <table class="defense" style="margin: auto; width: 50%;">
                    <thead>
                        <tr>
                            <th style="width: 30%;">Time</th>
                            <th>Class Name</th>
                        </tr>
                    </thead>
                    <!-- Output a schedule of classes and lessons for the given date -->
                    <asp:Literal id="outputSchedule" runat="server" />
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="placeholderScripts" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            // Add the datepicker function to the schedule date
            $(".scheduleDate").datepicker();
        });
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