<%@ Page Title="" Language="C#" MasterPageFile="~/FederationPOS.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SDAFederationPOS.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="placeholderMain" runat="server">
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td colspan='2'>Point-of-Sale
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>Enter Name:
                        </td>
                    </tr>
                    <tr>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox ID='lastName' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>First Name:</b>
                        </td>
                        <td>
                            <asp:TextBox ID='firstName' runat='server' />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>School:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="federationSchools" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan='2'>
                            <asp:Button ID="submit" Text="Submit" OnClick="submit_Click" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="resultsList" runat="server" /><br />
                            <asp:Label ID="customersList" runat='server' />
                        </td>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>