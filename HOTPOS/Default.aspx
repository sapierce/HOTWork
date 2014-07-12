<%@ Page Title="Home Page" Language="C#" MasterPageFile="TansPOS.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HOTPOS._Default" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table style="padding: 0px 30px 0px; border-spacing: 0px 30px 0px;">
        <tr>
            <td style="vertical-align: top;">
                <table class="tanning">
                    <thead>
                        <tr>
                            <th colspan="2">
                                Point-of-Sale
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="centerAlignHeader" colspan="2">
                                Enter Customer Name:
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Last Name:
                            </td>
                            <td>
                                <asp:TextBox ID="lastName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                First Name:
                            </td>
                            <td>
                                <asp:TextBox ID="firstName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Not A Signed Up Customer:
                            </td>
                            <td>
                                <asp:CheckBox ID="notACustomer" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="submitPOS" runat="server" Text="Submit" OnClick="submitPOS_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Literal ID="customerResults" runat="server" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td style="vertical-align: top;">
                <table class="tanning">
                    <thead>
                        <tr>
                            <th colspan="2">
                                Transaction Logs
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td style="text-align: right;">
                                Employee number:
                            </td>
                            <td>
                                <asp:TextBox ID="employeeNumber" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                Date:
                            </td>
                            <td>
                                <asp:TextBox ID="transactionDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="endOfShift" runat="server" Text="End of Shift" OnClick="endOfShift_Click" /><br />
                                <span class="detailInformation">Will print for both tanning and martial arts</span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>