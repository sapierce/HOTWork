<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="SDAFederation.StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="centerRoundedDiv">
        <h1>Student List</h1>
        <asp:Literal ID="studentList" runat="server" />
    </div>
</asp:Content>