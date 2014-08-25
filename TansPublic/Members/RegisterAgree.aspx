<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAgree.aspx.cs"
    Inherits="PublicWebsite.MembersArea.RegisterAgree" MasterPageFile="..\PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <div id="agreement">
        <table>
            <tr>
                <td colspan='2'>
                    <asp:Label ID="errorMessage" runat="server" CssClass="errorLabel" />
                    <asp:ValidationSummary ID="agreementErrorSummary" runat="server" CssClass="errorLabel" ShowSummary="true" ValidationGroup="agree" />
                    <asp:RequiredFieldValidator ID="requiredCustomerSignature" runat="server" ErrorMessage="Signature is required." ControlToValidate="customerSignature" ValidationGroup="agree" Display="None" />
                    <asp:RegularExpressionValidator ValidationExpression="^[A-Za-z -']+$" ID="validCustomerSignature" runat="server" ErrorMessage="Signature can only contain A-Z." ControlToValidate="customerSignature" ValidationGroup="agree" Display="None" />
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <h4>
                        Please Read the Following Customer Notice</h4>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (A)
                </td>
                <td style="vertical-align:top;">
                    Failure to use the eyeprotection provided to the customer by the tanning facility
                    may result in permanent damage to the eyes;
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (B)
                </td>
                <td style="vertical-align:top;">
                    Overexposure to ultraviolet light causes burns;
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (C)
                </td>
                <td style="vertical-align:top;">
                    Repeated exposure may result in premature aging of the skin and skin cancer;
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (D)
                </td>
                <td style="vertical-align:top;">
                    Abnormal skin sensitivity or burning may be caused by reactions of ultraviolet light
                    to certain:<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;(i)Foods;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;(ii)Cosmetics; or<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;(iii)medications, including:<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(I)tranquilizers;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(II)diuretics;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(III)antibiotics;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(IV)high blood pressure medicines;<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(V)birth control pills
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (E)
                </td>
                <td style="vertical-align:top;">
                    Any person taking a prescription or over-the-counter drug should consult a physician
                    before using a tanning device;
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (F)
                </td>
                <td style="vertical-align:top;">
                    Pregnant women should consult their physicians(s) before using a tanning device;
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (G)
                </td>
                <td style="vertical-align:top;">
                    A person with skin that always burns easily and never tans should avoid a tanning
                    device;
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    (H)
                </td>
                <td style="vertical-align:top;">
                    A person with a family or past medical history of skin cancer should avoid a tanning
                    device;
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:CheckBox ID='readWarnings' runat="server" ValidationGroup="agree" />
                    <b>By checking this box and typing your name below, you are acknowledging that you have
                        read and understood the above customer notice and the warning signs posted in the
                        entry area and tanning room(s). In addition, you agree to wear protective eyewear.</b>
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:Label ID="customerNotification" runat="server" CssClass="errorLabel" />
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <b>Your Name:</b>
                    <asp:TextBox ID='customerSignature' runat="server" ValidationGroup="agree" />
                </td>
            </tr>
            <tr>
                <td colspan='2'>
                    <asp:Button Text='Submit' ID="submit" runat="server" OnClick="submit_OnClick" ValidationGroup="agree" CausesValidation="true" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
