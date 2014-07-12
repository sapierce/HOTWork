<%@ Page Title="Home Page" Language="C#" MasterPageFile="SDAPOS.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="SDAPOS._Default" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<p align='center'><asp:label id="lblError" class="error" runat="server" /></p>
	<table cellpadding='30'>
			<tr>
				<td valign='top'>
					<table>
						<tr>
							<td colspan='2'>
								<font style='font-size:14px;'>
									<b>Point-of-Sale</b>
								</font>
							</td>
						</tr>
						<tr>
							<td colspan='2'>
								<b>Enter Customer Name:</b>
							</td>
						</tr>
						<tr>
							<td align='right'>
								<b>Last Name:</b>
							</td>
							<td>
								<asp:textbox id='txtLName' runat='server'/>
							</td>
						</tr>
						<tr>
							<td align='right'>
								<b>First Name:</b>
							</td>
							<td>
								<asp:textbox id='txtFName' runat='server'/>
							</td>
						</tr>
                    <tr>
                        <td>
                            <b>Not A Signed Up Customer:</b>
                        </td>
                        <td>
                            <asp:CheckBox ID="notACustomer" runat="server" />
                        </td>
                    </tr>
						<tr>
							<td colspan='2' align='right'>
								<asp:Button ID="submitPOS" runat="server" Text="Submit" />
							</td>
						</tr>
						<tr>
							<td align='right'>
								<asp:Label ID="lblResults" runat="server" /><br />
								<asp:Label ID="lblCustomers" runat='server'/>
							</td>
							<td>
                            <br />
							</td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td colspan='2'>
								<font style='font-size:14px;'>
									<b>Transaction Logs</b>
								</font>
							</td>
						</tr>
						<tr>
							<td align='right'>
								<b>Employee number:</b>
							</td>
							<td>
								<asp:textbox id='txtEmpNum' runat='server'/>
							</td>
						</tr>
						<tr>
							<td align='right'>
								<b>Date:</b>
							</td>
							<td>
								<asp:textbox id='txtDate' runat='server'/>
							</td>
						</tr>
						<tr>
							<td align='right'>
								<input type='submit' value='End of Shift'><br>
								<font style='font-size:11px;'>Will print for both tanning and martial arts</font>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
</asp:Content>