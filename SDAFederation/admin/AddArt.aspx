<%@ Page Title="" Language="C#" MasterPageFile="../Federation.Master" AutoEventWireup="true" CodeBehind="AddArt.aspx.cs" Inherits="SDAFederation.admin.AddArt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:ValidationSummary ID="validation" DisplayMode="BulletList" EnableClientScript="true" ShowSummary="true" ValidationGroup="addArt" runat="server" HeaderText="Please correct the following issues:" />
    <asp:RequiredFieldValidator ID="nameRequired" Display="None" ControlToValidate="artTitle" ErrorMessage="Please enter an art name" ValidationGroup="addArt" runat="server" />
    <h1>Add an Art</h1>
    School: <asp:DropDownList ID="federationSchools" runat="server" /><br />
    Art Title: <asp:TextBox ID="artTitle" runat="server" ValidationGroup="addArt" /><br />
    <asp:Button ID="addArt" runat="server" Text="Add Art" CausesValidation="true" OnClick="addArt_Click" />
</asp:Content>
