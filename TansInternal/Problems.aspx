<%@ Page Title="" Language="C#" MasterPageFile="HOTTropicalTans.Master" AutoEventWireup="true" CodeBehind="Problems.aspx.cs" Inherits="HOTTropicalTans.Problems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <table class="tanning">
        <thead>
            <tr>
                <th colspan='2'>
                    Problems/Comments
                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>To:</td>
                <td>Stephanie (HOTTans@hottropicaltans.com)</td>
            </tr>
            <tr>
                <td>Your Name:</td>
                <td>
                    <asp:TextBox ID='reportName' runat="server" /></td>
            </tr>
            <tr>
                <td valign='top'>Comments/Problem:</td>
                <td>
                    <asp:TextBox ID='commentProblem' TextMode="MultiLine" Rows='4' Columns='25' runat="server" /></td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:Button Text="Submit" OnClick="submitProblem_Click" ID="submitProblem" runat="server" /></td>
            </tr>
        </tbody>
    </table>
</asp:Content>