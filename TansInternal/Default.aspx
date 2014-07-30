<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HOTTropicalTans._Default"
    MasterPageFile="HOTTropicalTans.master" Title="HOT Tropical Tans Scheduling" %>

<asp:Content ID="scheduleHeaderContent" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=enterDate.ClientID%>").datepicker();
            $(".taken").tooltipster({
                contentAsHTML: true,
                position: 'bottom',
                theme: 'tooltipster-light',
                trigger: 'hover'
            });
            $(".note").tooltipster({
                contentAsHTML: true,
                position: 'bottom',
                theme: 'tooltipster-light',
                trigger: 'hover',
                interactive: true
            });
        });
    </script>
</asp:Content>
<asp:Content ID="scheduleContent" runat="server" ContentPlaceHolderID="Main">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="7">Color Code
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td style="align-items: center;">
                    <img src="/images/1.gif" alt="Has not tanned" />
                    - Has not tanned
                </td>
                <td style="align-items: center;">
                    <img src="/images/2.gif" alt="Has tanned" />
                    - Has tanned
                </td>
                <td style="align-items: center;">
                    <img src="/images/3.gif" alt="Package expired" />
                    - Package expired
                </td>
                <td style="align-items: center;">
                    <img src="/images/4.gif" alt="Owed Product" />
                    - Owed product
                </td>
                <td style="align-items: center;">
                    <img src="/images/5.gif" alt="Owes money" />
                    - Owes money
                </td>
                <td style="align-items: center;">
                    <img src="/images/6.gif" alt="Check transactions" />
                    - Check transactions
                </td>
                <td style="align-items: center;">
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
    <table class="tanning" style="margin: auto; width: 90%;">
        <asp:Literal ID="scheduleOutput" runat="server" />
    </table>
</asp:Content>