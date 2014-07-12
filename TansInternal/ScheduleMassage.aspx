<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="ScheduleMassage.aspx.cs" Inherits="HOTTropicalTans.ScheduleMassage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=enterDate.ClientID%>").datepicker();
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $(document).tooltip();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div style="text-align: center; margin: auto;">
        <asp:TextBox ID="enterDate" runat="server" />&nbsp;
		<asp:Button ID="changeDate" Text="Go to Date" runat="server" OnClick="changeDate_Click" />
    </div>
    <br />
    <table class="tanning" width="90%" style="margin: auto;">
        <asp:Literal ID="massageScheduleOutput" runat="server" />
    </table>
</asp:Content>
