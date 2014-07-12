<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ClassDetails.aspx.cs" Inherits="HOTSelfDefense.ClassDetails" MasterPageFile="HOTSelfDefense.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
    <table class="defense" style="width: 30%;">
        <thead>
            <tr>
                <th colspan="2">Class Information</th>
            </tr>
        </thead>
        <tr>
            <td class="rightAlignHeader">Class Title:</td>
            <td>
                <!-- Output the class/lesson title -->
                <asp:Label ID="classTitle" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Art:</td>
            <td>
                <!-- Output the class/lesson art(s) -->
                <asp:Label ID="classArt" runat="server" /></td>
        </tr>
        <tr>
            <td class="rightAlignHeader">Instructor:</td>
            <td>
                <!-- Output the class/lesson instructor -->
                <asp:Label ID="classInstructor" runat="server" /></td>
        </tr>
    </table>
    <br />
    <br />
    <!-- Build a table list of students in this class/lesson -->
    <table class="defense" style="width: 50%;">
        <tr>
            <td colspan="3" style="text-align: right;">
                <!-- Give a link to a printable version of this list -->
                <asp:Label ID="printRoster" runat="server" /></td>
        </tr>
        <thead>
            <tr>
                <th>Check In</th>
                <th>Student Name</th>
                <th>Notes</th>
            </tr>
        </thead>
        <!-- Output a table list of students in this class/lesson -->
        <asp:Literal ID="classRoster" runat="server" />
        <tr>
            <td colspan="3">
                <!-- Give a link to add another student to this class/lesson -->
                <asp:HyperLink ID="addStudent" runat="server" Text="Add a student to this class" /></td>
        </tr>
    </table>
</asp:Content>