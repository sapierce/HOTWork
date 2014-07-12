<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="SDAFederation.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:Panel ID="customerSearch" runat="server">
        <div class="centerRoundedDiv">
            <table>
                <tr>
                    <td colspan='2' class='header'>Search for student
                    </td>
                </tr>
                <tr>
                    <td>Last Name:
                    </td>
                    <td>
                        <asp:TextBox ID='lastName' runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>First Name:
                    </td>
                    <td>
                        <asp:TextBox ID="firstName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>
                        <asp:Button Text='Search' runat="server" ID="searchStudents" OnClick="searchStudents_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="customerSearchResults" runat="server">
        <table>
            <tr>
                <td colspan='2' class='header'>Student Results</td>
            </tr>
            <asp:Label ID="studentResults" runat="server" />
        </table>
    </asp:Panel>
</asp:Content>