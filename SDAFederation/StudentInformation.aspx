<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="StudentInformation.aspx.cs" Inherits="SDAFederation.StudentInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="centerRoundedDiv">
        <asp:HyperLink ID="editStudent" runat="server" Text="Edit Student" />
        <table class="standardTable">
            <tr>
                <td colspan='2' class="standardHeader">Student Information</td>
            </tr>
            <tr>
                <td class="rightAlignHeader">ID:</td>
                <td class="standardField">
                    <asp:Label ID="studentID" runat='server' /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Name:</td>
                <td class="standardField">
                    <asp:Label ID='studentName' runat='server' /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Address:</td>
                <td class="standardField">
                    <asp:Label ID='studentAddress' runat='server' /><br />
                    <asp:Label ID='studentCity' runat='server' /><br />
                    <asp:Label ID='studentState' runat='server' /><br />
                    <asp:Label ID="studentZip" runat='server' />
                </td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Birthday:</td>
                <td class="standardField">
                    <asp:Label ID="studentBirthDate" runat='server' /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Emergency Contact:</td>
                <td class="standardField">
                    <asp:Label ID="studentEmergencyContact" runat='server' /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Notes:</td>
                <td class="standardField">
                    <asp:Label ID="studentNotes" runat='server' /></td>
            </tr>
            <tr>
                <td class="rightAlignHeader">Arts:</td>
                <td class="standardField">
                    <asp:Label ID="studentArtInformation" runat='server' /></td>
            </tr>
        </table>
    </div>
</asp:Content>