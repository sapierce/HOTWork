<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="EditArt.aspx.cs" Inherits="SDAFederation.admin.EditArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:ValidationSummary ID="validation" DisplayMode="BulletList" EnableClientScript="true" ShowSummary="true" ValidationGroup="addArt" runat="server" HeaderText="Please correct the following issues:" />
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" ControlToValidate="artTitle" ErrorMessage="Please enter an art name" ValidationGroup="addArt" runat="server" />
    <h1>Edit an Art</h1>
    School: <asp:Label runat="server" ID="artSchool" /><br />
    Art Title: <asp:TextBox ID="artTitle" runat="server" ValidationGroup="addArt" /><br />
    <asp:Button ID="editArt" runat="server" Text="Edit Art" CausesValidation="true" OnClick="editArt_Click" />
</asp:Content>
