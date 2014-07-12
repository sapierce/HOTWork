<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HOTTropicalTans._Default"
    MasterPageFile="HOTTropicalTans.master" Title="Content Page" %>

<asp:Content ContentPlaceHolderID="headerPlaceHolder" runat="server">
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
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Main">
    <table class='tanning'>
        <thead>
            <tr>
                <th colspan='7'>Color Code
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td align="center">
                    <img src="/images/1.gif" alt="Has not tanned" />
                    - Has not tanned
                </td>
                <td align="center">
                    <img src="/images/2.gif" alt="Has tanned" />
                    - Has tanned
                </td>
                <td align="center">
                    <img src="/images/3.gif" alt="Package expired" />
                    - Package expired
                </td>
                <td align="center">
                    <img src="/images/4.gif" alt="Owed Product" />
                    - Owed product
                </td>
                <td align="center">
                    <img src="/images/5.gif" alt="Owes money" />
                    - Owes money
                </td>
                <td align="center">
                    <img src="/images/6.gif" alt="Check transactions" />
                    - Check transactions
                </td>
                <td align="center">
                    <img src="/images/7.gif" alt="Lotion check" />
                    - Lotion check
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <div style="text-align: center; margin: auto;">
        <asp:TextBox ID="enterDate" runat="server" />&nbsp;
		<asp:Button ID="changeDate" Text="Go to Date" runat="server" OnClick="changeDate_Click" />
    </div>
    <br />
    <table class="tanning" width="90%" style="margin: auto;">
        <asp:Literal ID="scheduleOutput" runat="server" />
    </table>
</asp:Content>