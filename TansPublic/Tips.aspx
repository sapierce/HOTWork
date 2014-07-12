<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tips.aspx.cs" Inherits="PublicWebsite.Tips"
    MasterPageFile="PublicWebsite.master" %>

<asp:Content ID="defaultContent" runat="server" ContentPlaceHolderID="SiteContent">
    <div id='tips'>
        <table cellpadding='5'>
            <tr>
                <td valign="top">
                    <b>Eye Protection</b><br />
                    Ultraviolet tanning rays (UVA and UVB) are harmful to your eyes. Eyelids are too
                    thin to provide adequate protection against injury. Eye goggles are furnished and
                    must be worn. You may also <a href="<%= HOTBAL.TansConstants.MISC_PUBLIC_URL %>" class='center'>purchase your own goggles</a>.<br />
                    <br />
                    <a name="24hours"><b>Frequency</b></a><br />
                    Allow a minimum of 24 hours between tanning sessions. Tanning professionals recommend
                    waiting 48 to 72 hours, the span of time necessary for your skin to recover from
                    UV exposure, and to create melanin and a tan. The <a href="http://www.dshs.state.tx.us/dmd/dmdrules.shtm#Tanning"
                        class="center">Texas Department of Health</a> does not allow licensed tanning
                    salons to allow customers to tan more than once in 24 hours.
                    <br />
                    <br />
                    <b>Moisturizing</b><br />
                    <a href="<%= HOTBAL.TansConstants.LOTION_PUBLIC_URL %>" class='center'>Moisturizing</a> is key to a long-lasting
                    tan. Dry skin tends to reflect UV-light, thus decreasing your tanning potential.
                    Remember: Healthy and moist skin tans faster and darker.<br />
                    <br />
                    <b>Lip Protection</b><br />
                    Lips cannot produce melanin, and are not capable of tanning. Protect your lips with
                    a <a href="<%= HOTBAL.TansConstants.LIP_BALM_PUBLIC_URL %>" class='center'>lip balm</a> that contains sunblock.<br />
                    <br />
                    <b>Tattoos</b><br />
                    UV-light may cause color to fade. Cover tattoos with an SPF sunscreen, sunblock
                    or even <a href="<%= HOTBAL.TansConstants.LIP_BALM_PUBLIC_URL %>" class='center'>lip balm</a>.<br />
                    <br />
                    <b>Rash or Itching</b><br />
                    Some people may get a slight reaction or an itching sensation. Unless you are on
                    medication that is photosensitive or you are sensitive to sunlight, this is usually
                    a minor heat rash which will go away. Wait a day of two before you tan again. Certain
                    cosmetics, foods and medications can cause sensitivity to sunlight. If your concerned,
                    check with your doctor or pharmacist.<br />
                    <br />
                    <b>Outside Oil and Lotions</b><br />
                    Outdoor products are intended to be used outdoors. They can damage the tanning bed
                    acrylics and cause a film, actually inhibits your body from tanning. Tanning oils
                    are not permitted for use on HOT Tropical Tans' beds.<br />
                    <br />
                    <b>Tanning Accelerators/Maximizers</b><br />
                    Your body makes use of many ingredients in addition to UV-light to produce a tan.
                    <a href="<%= HOTBAL.TansConstants.LOTION_PUBLIC_URL %>" class='center'>Indoor tanning accelerators</a> are designed
                    to provide you with a balanced amount of these essential ingredients to achieve
                    a better tan. Many advanced products also contain skin care components that help
                    reverse the damaging effects of UV light and improve the condition of your skin.<br />
                    <br />
                    <b>Added Comfort</b><br />
                    For added comfort and circulation, change positions slightly by moving around a
                    bit while lying down. Areas with impaired circulation will cause little or no tanning
                    to occur.<br />
                    <br />
                    <b>What to Wear</b><br />
                    You may wear your swimsuit, undergarments, or nothing at all while tanning. If you
                    choose to wear nothing, use extra caution on areas that have never been exposed
                    to sunlight. For the first few visits, cover these areas for half the duration of
                    the session, to gradually ease into a tan.<br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
