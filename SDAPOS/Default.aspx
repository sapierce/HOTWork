<%@ Page Title="Home Page" Language="C#" MasterPageFile="SDAPOS.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SDAPOS._Default" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <p align='center'>
        <asp:Label ID="lblError" class="error" runat="server" /></p>
    <table style="padding: 0px 30px">
        <tr>
            <td>
                <table class="defense">
                    <thead>
                        <tr>
                            <th colspan='2'>
                                Point-of-Sale
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td colspan='2'>
                            Enter Customer Name:
                        </td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">
                            Last Name:
                        </td>
                        <td>
                            <asp:TextBox ID='txtLName' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">
                            First Name:
                        </td>
                        <td>
                            <asp:TextBox ID='txtFName' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Not A Signed Up Customer:
                        </td>
                        <td>
                            <asp:CheckBox ID="notACustomer" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2' align='right'>
                            <asp:Button ID="submitPOS" runat="server" Text="Submit" />
                        </td>
                    </tr>
                    <tr>
                        <td align='right'>
                            <asp:Label ID="lblResults" runat="server" /><br />
                            <asp:Label ID="lblCustomers" runat='server' />
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <table class="defense">
                    <thead>
                        <tr>
                            <th colspan='2'>
                                Transaction Logs
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td class="rightAlignHeader">
                            Employee number:
                        </td>
                        <td>
                            <asp:TextBox ID='txtEmpNum' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td class="rightAlignHeader">
                            Date:
                        </td>
                        <td>
                            <asp:TextBox ID='txtDate' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td align='right'>
                            <input type='submit' value='End of Shift'><br>
                            <span class="detailInformation">Will print for both tanning and martial arts</span>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>