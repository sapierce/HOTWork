<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PublicWebsite._Default"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <asp:Label ID="welcomeNotice" runat="server" CssClass="mainHeaderText" /><br />
    <br />
    <div id="mainText" style="text-align: left;">
        <asp:Literal ID="mainSiteText" runat="server" /><br />
        <br />
        <asp:Literal ID="siteNotice" runat="server" /><br />
        <!-- AddThis Button BEGIN -->
        <div class="addthis_toolbox addthis_default_style addthis_32x32_style">
            <a class="addthis_button_preferred_1"></a>
            <a class="addthis_button_preferred_2"></a>
            <a class="addthis_button_preferred_3"></a>
            <a class="addthis_button_preferred_4"></a>
            <a class="addthis_button_compact"></a>
            <a class="addthis_counter addthis_bubble_style"></a>
        </div>
        <script type="text/javascript">var addthis_config = { "data_track_addressbar": true };</script>
        <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-4d795dcb325f15ba"></script>
        <!-- AddThis Button END -->
        <br />
        <div class="fb-like" data-href="http://www.facebook.com/hottropicaltans" data-send="true" data-width="450" data-show-faces="true" data-font="verdana"></div>
        <br />
    </div>
</asp:Content>