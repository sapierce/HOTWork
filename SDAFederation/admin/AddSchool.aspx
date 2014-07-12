<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="AddSchool.aspx.cs" Inherits="SDAFederation.admin.AddSchool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:ValidationSummary ID="validation" DisplayMode="BulletList" EnableClientScript="true" ShowSummary="true" ValidationGroup="addSchool" runat="server" HeaderText="Please correct the following issues:" />
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" ControlToValidate="schoolName" ErrorMessage="Please enter a school name" ValidationGroup="addSchool" runat="server" />
    <%--<asp:RequiredFieldValidator ID="passwordRequired" Display="None" ControlToValidate="schoolPassword" ErrorMessage="Please enter a school password" ValidationGroup="addSchool" runat="server" />--%>
    <h1>Add a School</h1>
    School Name: <asp:TextBox ID="schoolName" runat="server" ValidationGroup="addSchool" /><br />
    <%--School Password: <asp:TextBox ID="schoolPassword" runat="server" TextMode="Password" ValidationGroup="addSchool" /><br />--%>
    <asp:Button ID="addSchool" runat="server" Text="Add School" CausesValidation="true" OnClick="addSchool_Click" />
</asp:Content>
