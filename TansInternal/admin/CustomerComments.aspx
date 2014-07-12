<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerComments.aspx.cs" Inherits="HOTTropicalTans.admin.CustomerComments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <table>
        <tr>
            <td><br /></td>
            <td>Date</td>
            <td>From</td>
            <td>E-mail</td>
            <td>About</td>
            <td>Comment</td>
        </tr>
        <asp:Literal ID="customerComments" runat="server" />
    </table>
</asp:Content>
