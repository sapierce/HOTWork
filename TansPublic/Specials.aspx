<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Specials.aspx.cs" Inherits="PublicWebsite.Specials"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <div id='about'>
        <div id='special'>
            <h2>Current Specials</h2>
            * 15% off all online lotion purchases<br />
            * Student Specials - Special Semester rates with valid student ID<br />
            <br />
        </div>
        <table align='center' border='0'>
            <tr>
                <td align='center' valign="middle">
                    <img src="images/buddypackage.gif" alt="Buddy Packages!" />
                </td>
                <td valign='top'>
                    <b>Buddy Packages!</b><br />
                    * $5 off any month package<br />
                    * For 2-5 people<br />
                    * All must sign up on the same day<br />
                    &nbsp;&nbsp;(do not have to tan at the same time)
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
