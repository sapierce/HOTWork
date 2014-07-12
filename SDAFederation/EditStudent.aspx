<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="EditStudent.aspx.cs" Inherits="SDAFederation.EditStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <table class="standardTable">
        <tr>
            <td colspan='2' class="standardHeader">
                Edit Student Information
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Federation ID:
            </td>
            <td class="standardField">
                <asp:Label ID='federationID' runat='server' />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                First Name:
            </td>
            <td class="standardField">
                <asp:TextBox ID='firstName' runat='server' MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Last Name:
            </td>
            <td class="standardField">
                <asp:TextBox ID='lastName' runat='server' MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Address:
            </td>
            <td class="standardField">
                <asp:TextBox ID='address' runat='server' MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                City:
            </td>
            <td class="standardField">
                <asp:TextBox ID='city' runat='server' MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                State:
            </td>
            <td class="standardField">
                <asp:TextBox ID='state' runat='server' MaxLength="2" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Zip:
            </td>
            <td class="standardField">
                <asp:TextBox ID='zipCode' runat='server' MaxLength="11" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Birthday:
            </td>
            <td class="standardField">
                <asp:TextBox ID='birthDate' runat='server' MaxLength="10" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Emergency Contact:
            </td>
            <td class="standardField">
                <asp:TextBox ID='emergencyContact' runat='server' MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td class="rightAlignHeader">
                Notes:
            </td>
            <td class="standardField">
                <asp:TextBox ID='notes' runat='server' MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td colspan='2' class="standardField">
                <asp:Label ID='studentID' runat='server' />
                <asp:Button ID='updateStudent' OnClick="updateStudent_Click" Text='Update Information' runat='server' />
            </td>
        </tr>
    </table>
</asp:Content>