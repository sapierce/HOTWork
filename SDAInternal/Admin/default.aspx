<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="HOTSelfDefense._default" MasterPageFile="../HOTSelfDefense.master" %>

<asp:Content ID="defaultMain" runat="server" ContentPlaceHolderID="placeholderMain">
<table align="center">
			<tr>
				<td class="header">Class Information</td>
				<td class="header">Sales/Inventory</td>
			</tr>
			<tr>
				<td valign="top">
					<!-- Edit/Delete Classes -->
					Edit/Delete Repeating Class: <asp:dropdownlist id="ddlCourse" runat="server" /> <asp:button id="btnEditCourse" onClick="btnEditCourse_onClick" runat="server" text="Go" /><br />
					<br />
                    <!-- Add/Edit/Delete Instructors -->
					<asp:button id="btnAddInst" onClick="btnAddInst_onClick" runat="server" text="Add Instructor" /><br />
					Edit/Delete Instructor: <asp:dropdownlist id="ddlInst" runat="server" /> <asp:button id="btnEditInst" onClick="btnEditInst_onClick" runat="server" text="Go" />
				</td>
				<td valign="top">
					<!-- Full Transaction Log -->
					Full Transaction Log for: <asp:textbox id="txtFullTrns" runat="server" /> <asp:button id="btnFullTrns" onClick="btnFullTrns_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Add/Edit/Delete Inventory -->
					<asp:button id="btnAddItem" onClick="btnAddItem_onClick" runat="server" text="Add Item" /><br />
					Edit/Delete Item: <asp:dropdownlist id="ddlItem" runat="server" /> <asp:button id="btnEditItem" onClick="btnEditItem_onClick" runat="server" text="Go" /><br />
					
					<!-- Current Inventory -->
					<asp:button id="btnInven" onClick="btnInven_onClick" runat="server" text="Current Inventory" />
				</td>
			</tr>
			<tr>
				<td class="header">Art Information</td>
				<td class="header">Reports</td>
			</tr>
			<tr>
				<td valign="top">
					<!-- Add/Edit/Delete Art -->
					<asp:button id="btnAddArt" onClick="btnAddArt_onClick" runat="server" text="Add Art" /><br />
					Edit/Delete Art: <asp:dropdownlist id="ddlArt" runat="server" /> <asp:button id="btnEditArt" onClick="btnEditArt_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Add/Edit/Delete Belt -->
					<asp:button id="btnAddBelt" onClick="btnAddBelt_onClick" runat="server" text="Add Belt" /><br />
					Edit/Delete Belt: <asp:dropdownlist id="ddlBelt" runat="server" /> <asp:button id="btnEditBelt" onClick="btnEditBelt_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Add/Edit/Delete Tip -->
					<asp:button id="btnAddTip" onClick="btnAddTip_onClick" runat="server" text="Add Tip" /><br />
					Edit/Delete Tip: <asp:dropdownlist id="ddlTip" runat="server" /> <asp:button id="btnEditTip" onClick="btnEditTip_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Add/Edit/Delete Term -->
					<asp:button id="btnTerm" onClick="btnAddTerm_onClick" runat="server" text="Add Term" /><br />
					Edit/Delete Term: <asp:dropdownlist id="ddlTerm" runat="server" /> <asp:button id="btnEditTerm" onClick="btnEditTerm_onClick" runat="server" text="Go" /><br />
					<br />
				</td>
				<td valign="top">
					<!-- Birthdays -->
					Birthdays in the next <asp:textbox id="txtBirthday" runat="server" /> day(s) <asp:button id="btnBirthday" onClick="btnBirthday_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Belts Passed -->
					Belts Passed Between:<br />
					<asp:textbox id="txtPassBeg" runat="server" /> - <asp:textbox id="txtPassEnd" runat="server" /> <asp:button id="btnPass" onClick="btnPass_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Last Tip -->
					Students on last tip as of: <asp:textbox id="txtLastTip" runat="server" /> <asp:button id="btnLastTip" onClick="btnLastTip_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- Class Attendance -->
					Class attendance for: <asp:textbox id="txtAttend" runat="server" /> <asp:button id="btnAttend" onClick="btnAttend_onClick" runat="server" text="Go" /><br />
					<br />
					<!-- All Active Students -->
					<asp:button id="btnActive" onClick="btnActive_onClick" runat="server" text="All Active Students" />
				</td>
			</tr>
		</table>
</asp:Content>