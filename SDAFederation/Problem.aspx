<%@ Page Title="" Language="C#" MasterPageFile="Federation.Master" AutoEventWireup="true" CodeBehind="Problem.aspx.cs" Inherits="SDAFederation.Problem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <p align='center'><asp:label id="lblError" class="error" runat="server"/></p>
		<p align='center'><asp:label id="lblResponse" runat="server"/></p>
		<table>
			<tr>
				<td colspan='2'><h3>Please report any problems or comments here</h3></td>
			</tr>
			<tr>
				<td>To:</td>
				<td>Stephanie (Problems@hottropicaltans.com)</td>
			</tr>
			<tr>
				<td>Your Name:</td>
				<td><asp:textbox id='txtName' runat="server"/></td>
			</tr>
			<tr>
				<td valign='top'>Comments/Problem:</td>
				<td><asp:textbox id='txtComment' textmode="MultiLine" rows='4' columns='25' runat="server"/>
			</tr>
			<tr>
				<td colspan='2'><input type='submit' value='Submit'></td>
			</tr>
		</table>
</asp:Content>
