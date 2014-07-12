<%@ Page Language="vb" AutoEventWireup="false" Codebehind="schedule.aspx.vb" Inherits="HOTSDA.schedule"%>
<%@ Register TagPrefix="HOTSDA" TagName="head" src="includes/header.ascx"%>
<%@ Register TagPrefix="HOTSDA" TagName="foot" src="includes/footer.ascx"%>
<HOTSDA:head Id="ucHeader" Runat="Server"/>
	<!-- #include file="includes/calendar.inc" -->
	<table>
		<tr>
			<td>
				<h2>Classes for <% =Request.QueryString("Date") %></h2>
			</td>
		</tr>
		<tr>
			<td align="center">
				<div id="overDiv" style="position:absolute; visibility:hidden; z-index:1000; align: center;"></div><form name="datePick" action="schedule.aspx"><input type="text" name="Date"><a href="javascript:show_calendar('datePick.Date');" onMouseOver="window.status='Date Picker'; return true;" onMouseOut="window.status=''; nd(); return true;"><img src="/HOTSDA/images/cal.gif" width=24 height=22 border=0 hspace="10"></a><input type="submit" value="Go to Date"></form>
			</td>
		</tr>
	</table>

    <form id="Form1" method="post" runat="server">
		<p align='center'><asp:label id="lblError" class="error" runat="server"/></p>
		<table align='center' width='80%' class='bcc'>
			<tr>
				<td class='rheader' width='30%'>Time</td><td class='lheader'>Class Name</td>
			</tr>
			<% Call OutputClass() %>
		</table>
    </form>
  </body>
</html>
