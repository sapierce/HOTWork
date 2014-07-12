<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="PublicWebsite.About"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="aboutContentHeader" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <style type="text/css">
        #map-canvas {
            width: 300px;
            height: 300px;
            float: left;
        }
    </style>
    <script type="text/javascript"
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC9uAGYgcGJFrpLqtHk6dVHw4o-Y6aDpf8&sensor=false">
    </script>
    <script type="text/javascript">
        function initialize() {
            var latLong = new google.maps.LatLng(31.529, -97.177);
            var mapOptions = {
                center: latLong,
                zoom: 15,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"),
                mapOptions);
            var marker = new google.maps.Marker({
                position: latLong,
                title: 'HOT TROPICAL TANS'
            });
            marker.setMap(map);
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
</asp:Content>
<asp:Content ID="aboutContent" runat="server" ContentPlaceHolderID="contentPlaceHolder">
    <table>
        <tr>
            <td style="vertical-align: top; padding: 10px 20px 10px 20px; border-spacing: 20px;">
                <table class="tanning" style="width: 100%;">
                    <thead>
                        <tr>
                            <th>Waco Location</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td style="vertical-align: top;">710 N Valley Mills Drive<br />
                                Waco, TX&nbsp;&nbsp;&nbsp;76710<br />
                                <br />
                                Phone: 254-399-9944<br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
            <td rowspan="2" style="vertical-align: top; padding: 10px 20px 10px 20px; border-spacing: 20px;">
                <asp:Literal ID="hoursOfOperation" runat="server" /><br />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; padding: 10px 20px 10px 20px; border-spacing: 20px;">
                <div id="map-canvas"></div>
                <%--<img src="http://maps.googleapis.com/maps/api/staticmap?center=31.529280,-97.177805&zoom=15&size=250x250&markers=color:blue%7Clabel:HOT TROPICAL TANS%7C31.529280,-97.177805" style="border: 1px solid black;" />--%>
            </td>
        </tr>
    </table>
</asp:Content>