<%@ Page Title="" Language="C#" MasterPageFile="../HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="CustomerCombine.aspx.cs" Inherits="HOTTropicalTans.CustomerCombine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan="2">Combine Customers</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Id to Keep:</td>
            <td>
                <asp:TextBox ID="keepId" runat="server" MaxLength="50" OnTextChanged="keepId_TextChanged" AutoPostBack="true" /><br />
                <asp:Label ID="keepNames" runat="server" CssClass="detailInformation" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Id(s) to Remove:</td>
            <td>
                <asp:TextBox ID="removeId" runat="server" MaxLength="50" OnTextChanged="removeId_TextChanged" AutoPostBack="true" /><br />
                <span class="detailInformation">Comma separated</span><br />
                <asp:Label ID="removeNames" runat="server" CssClass="detailInformation" />
            </td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="combineCustomers" runat="server" Text="Combine Customers" OnClick="combineCustomers_Click" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:HyperLink ID="returnToAdmin" runat="server" Text="Return to Administration" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Literal ID="combinationResults" runat="server" /></td>
        </tr>
    </table>
</asp:Content>
