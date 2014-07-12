<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRegister.aspx.cs" Inherits="PublicWebsite.MembersArea.NewRegister"
	MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
	<div id="newMember">
		<table>
			<tr>
				<td colspan='2'>
					<h4>
						New Member Sign-Up</h4>
				</td>
			</tr>
			<tr>
				<td colspan='2'>
					<asp:Label ID="errorMessage" runat="server" CssClass="errorLabel" />
					<asp:ValidationSummary ID="newRegistrationSummary" runat="server" ShowSummary="true" ValidationGroup="new" CssClass="errorLabel" />
					<asp:RequiredFieldValidator ID="firstNameRequired" runat="server" ErrorMessage="Please enter a first name." EnableClientScript="true" ValidationGroup="new" ControlToValidate="firstName" Display="None" />
					<asp:RequiredFieldValidator ID="lastNameRequired" runat="server" ErrorMessage="Please enter a last name." EnableClientScript="true" ValidationGroup="new" ControlToValidate="lastName" Display="None" />
					<asp:RequiredFieldValidator ID="addressRequired" runat="server" ErrorMessage="Please enter an address." EnableClientScript="true" ValidationGroup="new" ControlToValidate="address" Display="None" />
					<asp:RequiredFieldValidator ID="cityRequired" runat="server" ErrorMessage="Please enter a city." EnableClientScript="true" ValidationGroup="new" ControlToValidate="city" Display="None" />
					<asp:RequiredFieldValidator ID="stateRequired" runat="server" ErrorMessage="Please enter a state." EnableClientScript="true" ValidationGroup="new" ControlToValidate="state" Display="None" />
					<asp:RequiredFieldValidator ID="zipCodeRequired" runat="server" ErrorMessage="Please enter a zip code." EnableClientScript="true" ValidationGroup="new" ControlToValidate="zipCode" Display="None" />
					<asp:RequiredFieldValidator ID="phoneRequired" runat="server" ErrorMessage="Please enter a phone number." EnableClientScript="true" ValidationGroup="new" ControlToValidate="phoneNumber" Display="None" />
					<asp:RequiredFieldValidator ID="dateOfBirthRequired" runat="server" ErrorMessage="Please enter a date of birth." EnableClientScript="true" ValidationGroup="new" ControlToValidate="dateOfBirth" Display="None" />
					<asp:RequiredFieldValidator ID="userNameRequired" runat="server" ErrorMessage="Please enter a username." EnableClientScript="true" ValidationGroup="new" ControlToValidate="userName" Display="None" />
					<asp:RequiredFieldValidator ID="passwordRequired" runat="server" ErrorMessage="Please enter a password." EnableClientScript="true" ValidationGroup="new" ControlToValidate="password" Display="None" />
					<asp:RequiredFieldValidator ID="passwordConfirmRequired" runat="server" ErrorMessage="Please confirm your password." EnableClientScript="true" ValidationGroup="new" ControlToValidate="passwordConfirm" Display="None" />
					<asp:RequiredFieldValidator ID="emailAddressRequired" runat="server" ErrorMessage="Please enter an e-mail address." EnableClientScript="true" ValidationGroup="new" ControlToValidate="emailAddress" Display="None" />
					<asp:RequiredFieldValidator ID="familyHistoryRequired" runat="server" ErrorMessage="Please select if you have a family history of skin cancer." EnableClientScript="true" ValidationGroup="new" ControlToValidate="familyHistory" Display="None" />
					<asp:RequiredFieldValidator ID="personalHistoryRequired" runat="server" ErrorMessage="Please select if you have a personal history of skin cancer." EnableClientScript="true" ValidationGroup="new" ControlToValidate="personalHistory" Display="None" />
					<asp:RegularExpressionValidator ID="firstNameRegEx" runat="server" ErrorMessage="First name may only contain alpha characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="firstName" Display="None" ValidationExpression="^[A-Za-z -']+$" />
					<asp:RegularExpressionValidator ID="lastNameRegEx" runat="server" ErrorMessage="Last name may only contain alpha characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="lastName" Display="None" ValidationExpression="^[A-Za-z -']+$" />
					<asp:RegularExpressionValidator ID="addressRegEx" runat="server" ErrorMessage="Address may only contain alphanumeric characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="address" Display="None" ValidationExpression="^[0-9A-Za-z -#\.']+$" />
					<asp:RegularExpressionValidator ID="cityRegEx" runat="server" ErrorMessage="City may only contain alphanumeric characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="city" Display="None" ValidationExpression="^[A-Za-z -']+$" />
					<asp:RegularExpressionValidator ID="stateRegEx" runat="server" ErrorMessage="State may only contain two uppercase alpha characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="state" Display="None" ValidationExpression="[A-Z]{2}" />
					<asp:RegularExpressionValidator ID="zipCodeRegEx" runat="server" ErrorMessage="Zip code may only contain numeric characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="zipCode" Display="None" ValidationExpression="^\d{5}" />
					<asp:RegularExpressionValidator ID="phoneRegEx" runat="server" ErrorMessage="Phone number is not in a valid format." EnableClientScript="true" ValidationGroup="new" ControlToValidate="phoneNumber" Display="None" ValidationExpression="\(?(\d{3})\)?-?(\d{3})-(\d{4})" />
					<asp:RegularExpressionValidator ID="dateOfBirthRegEx" runat="server" ErrorMessage="Date of birth is not in a valid format (MM/DD/YYYY)." EnableClientScript="true" ValidationGroup="new" ControlToValidate="dateOfBirth" Display="None" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$" />
					<asp:RegularExpressionValidator ID="userNameRegEx" runat="server" ErrorMessage="Username may only contain alphanumeric characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="userName" Display="None" ValidationExpression="^[A-Za-z0-9]+$" />
					<asp:RegularExpressionValidator ID="passwordRegEx" runat="server" ErrorMessage="Password may only contain alphanumeric characters." EnableClientScript="true" ValidationGroup="new" ControlToValidate="password" Display="None" ValidationExpression="^[A-Za-z0-9]+$" />
					<asp:RegularExpressionValidator ID="emailExpresson" runat="server" ErrorMessage="E-mail address is not in a valid format." EnableClientScript="true" ValidationGroup="new" ControlToValidate="emailAddress" Display="None" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b" />
                    <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="ValidateChkList" runat="server">Required.</asp:CustomValidator>
					<asp:CompareValidator ID="passwordCompare" runat="server" ErrorMessage="The entered passwords do not match." EnableClientScript="true" ControlToCompare="password" ControlToValidate="passwordConfirm" ValidationGroup="new" ValueToCompare="passwordConfirm" Display="None" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>First Name:</b>
				</td>
				<td>
					<asp:TextBox ID='firstName' runat="server" MaxLength="50" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Last Name:</b>
				</td>
				<td>
					<asp:TextBox ID='lastName' runat="server" MaxLength="50" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Address:</b>
				</td>
				<td>
					<asp:TextBox ID='address' runat="server" MaxLength="50" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>City:</b>
				</td>
				<td>
					<asp:TextBox ID='city' runat="server" MaxLength="50" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>State:</b>
				</td>
				<td>
					<asp:TextBox ID='state' size='2' runat="server" MaxLength="2" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Zip Code:</b>
				</td>
				<td>
					<asp:TextBox ID='zipCode' size='6' runat="server" MaxLength="5" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Telephone Number:</b>
				</td>
				<td>
					<asp:TextBox ID='phoneNumber' size='13' runat="server" MaxLength="12" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Date of Birth:</b>
				</td>
				<td>
					<asp:TextBox ID='dateOfBirth' size='11' runat="server" MaxLength="10" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b><a href='#fitz'>Fitzpatrick number:</a></b>
				</td>
				<td>
					<asp:DropDownList ID='fitzpatrickNumber' runat="server" ValidationGroup="new">
						<asp:ListItem Value='1' Text='1' />
						<asp:ListItem Value='2' Text='2' />
						<asp:ListItem Value='3' Text='3' />
						<asp:ListItem Value='4' Text='4' />
						<asp:ListItem Value='5' Text='5' />
						<asp:ListItem Value='6' Text='6' />
					</asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Do you have a family history of skin cancer?</b>
				</td>
				<td>
					<asp:RadioButtonList ID="familyHistory" runat="server" ValidationGroup="new" RepeatDirection="Horizontal">
						<asp:ListItem Text="Yes" Value="1" />
						<asp:ListItem Text="No" Value="0" />
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Do you have a medical history of skin cancer?</b>
				</td>
				<td>
					<asp:RadioButtonList ID="personalHistory" runat="server" ValidationGroup="new" RepeatDirection="Horizontal">
						<asp:ListItem Text="Yes" Value="1" />
						<asp:ListItem Text="No" Value="0" />
					</asp:RadioButtonList>
				</td>
			</tr>
			<tr><td><br /></td></tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>User Name:</b>
				</td>
				<td>
					<asp:TextBox ID='userName' runat="server" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Password:</b>
				</td>
				<td>
					<asp:TextBox ID='password' TextMode='password' runat="server" AutoCompleteType="None" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Confirm Password:</b>
				</td>
				<td>
					<asp:TextBox ID='passwordConfirm' TextMode='password' runat="server" AutoCompleteType="None" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>E-mail Address:</b>
				</td>
				<td>
					<asp:TextBox ID='emailAddress' runat="server" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>Would you like to receive e-mail specials?</b>
				</td>
				<td>
					<asp:CheckBox ID="receiveSpecials" runat="server" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td style="text-align: right; width: 50%;">
					<b>I have read and understand the site <a href="/TermsOfService.aspx">Terms of Service</a> and <a href="/PrivacyPolicy.aspx">Privacy Policy</a>:</b>
				</td>
				<td>
					<asp:CheckBox ID="readTermsOfService" runat="server" ValidationGroup="new" />
				</td>
			</tr>
			<tr>
				<td colspan='2' style='text-align:right;'>
					<asp:Button Text='Submit' ID="registerNew" runat="server" OnClick="registerNew_OnClick" ValidationGroup="new" />
				</td>
			</tr>
		</table>
	</div>
	<br />
	<div id='fitz'>
		<a name='fitz'></a>
		<table style="border: 1px solid #000000">
			<tr>
				<td colspan='2' style="border: 1px solid #000000">
					The fitzpatrick scale is the following scale for classifying a skin type, based
					on the skin's reaction to the first 10 to 45 minutes of sun exposure after the winter
					season.
				</td>
			</tr>
			<tr>
				<td style="border: 1px solid #000000">
					<b>Skin Type</b>
				</td>
				<td style="border: 1px solid #000000">
					<b>Sun burning and Tanning History</b>
				</td>
			</tr>
			<tr>
				<td align='center' style="border: 1px solid #000000">
					1
				</td>
				<td style="border: 1px solid #000000">
					Always burns easily; never tans
				</td>
			</tr>
			<tr>
				<td align='center' style="border: 1px solid #000000">
					2
				</td>
				<td style="border: 1px solid #000000">
					Always burns easily; tans minimally
				</td>
			</tr>
			<tr>
				<td align='center' style="border: 1px solid #000000">
					3
				</td>
				<td style="border: 1px solid #000000">
					Burns moderately; tans gradually
				</td>
			</tr>
			<tr>
				<td align='center' style="border: 1px solid #000000">
					4
				</td>
				<td style="border: 1px solid #000000">
					Burns minimally; always tans well
				</td>
			</tr>
			<tr>
				<td align='center' style="border: 1px solid #000000">
					5
				</td>
				<td style="border: 1px solid #000000">
					Rarely burns; tans profusely
				</td>
			</tr>
			<tr>
				<td align='center' style="border: 1px solid #000000">
					6
				</td>
				<td style="border: 1px solid #000000">
					Never burns; deeply pigmented
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
