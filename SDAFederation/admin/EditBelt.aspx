<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="EditBelt.aspx.cs" Inherits="SDAFederation.admin.EditBelt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:ValidationSummary ID="validation" DisplayMode="BulletList" EnableClientScript="true" ShowSummary="true" ValidationGroup="addArt" runat="server" HeaderText="Please correct the following issues:" />
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" ControlToValidate="artTitle" ErrorMessage="Please enter an art name" ValidationGroup="addArt" runat="server" />
    <h1>Edit an Art</h1>
    School: <asp:Label runat="server" ID="beltSchool" /><br />
    Art Title: <asp:Label ID="artTitle" runat="server" /><br />
    Belt Title: <asp:TextBox ID="beltTitle" runat="server" /><br />
    Belt Level: <asp:TextBox ID="beltLevel" runat="server" Text="0" /><br />
    <asp:Button ID="editBelt" runat="server" Text="Edit Belt" CausesValidation="true" OnClick="editBelt_Click"/>
    <asp:HiddenField ID="artID" runat="server" />
</asp:Content>
