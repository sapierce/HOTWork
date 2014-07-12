<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="PublicWebsite.MembersArea.LogOn"
	MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">

	<asp:ValidationSummary ID="loginSummary" runat="server" DisplayMode="BulletList" EnableClientScript="true" CssClass="errorLabel" HeaderText="The following problems were found:" ShowSummary="true" ValidationGroup="login" />
	<asp:RequiredFieldValidator ID="nameRequired" runat="server" ControlToValidate="userName" EnableClientScript="true" ErrorMessage="Please enter a user name." ValidationGroup="login" />
	<asp:RequiredFieldValidator ID="passwordRequired" runat="server" ControlToValidate="passWord" EnableClientScript="true" ErrorMessage="Please enter a password." ValidationGroup="login" />
	<div id='login'>
		<table>
			<tr>
				<td colspan="2">
					<h3>
						Please Login</h3>
				    <asp:Label ID="errorMessage" CssClass="errorLabel" runat="server" />
	            </td>
			</tr>
			<tr>
				<td align='right'>
					<font style="font-weight: bold; font-size: 12px">User:</font>
				</td>
				<td align='left'>
					<asp:TextBox ID="userName" size='15' runat="server" AutoCompleteType="None" ValidationGroup="login" />
				</td>
			</tr>
			<tr>
				<td align='right'>
					<font style="font-weight: bold; font-size: 12px">Password:</font>
				</td>
				<td align='left'>
					<asp:TextBox ID="passWord" size='15' TextMode="password" runat="server" AutoCompleteType="None" ValidationGroup="login" />
				</td>
			</tr>
			<tr>
				<td align='right' colspan="2">
					<asp:Button ID="siteLogOn" Text="Login" runat="server" OnClick="siteLogOn_OnClick" />
				</td>
			</tr>
			<tr>
				<td align='left' colspan="2">
					<font style="font-size: 11px"><a href="ForgotPassword.aspx" class="center">Forgot Password?</a></font>
				</td>
			</tr>
			<tr>
				<td align='left' colspan="2">
					<font style="font-size: 11px">Don't have an online account? <a href="/Members.aspx"
						class="center">Get one here!</a></font>
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
