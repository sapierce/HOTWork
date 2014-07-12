<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="SchoolLanding.aspx.cs" Inherits="SDAFederation.SchoolLanding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="centerRoundedDiv">
        <h1><asp:Label ID="schoolName" runat="server" /></h1>
        <br />
        <asp:HyperLink ID="viewStudents" runat="server" Text="View All Active Students" /><br />
        <br />
        <asp:HyperLink ID="addStudent" runat="server" Text="Add Student" /><br />
    </div>
</asp:Content>