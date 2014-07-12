<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartRedirect.aspx.cs"
    Inherits="PublicWebsite.ShoppingCartRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>HOT Tropical Tans - PayPal Redirect</title>
</head>
<body onload="document.payment_provider_form.submit()">
    <h2>
        Please wait while redirecting to Paypal for payment
    </h2>
    <form name="payment_provider_form" method="post" action="https://www.paypal.com/cgi-bin/webscr">
    <br />
    <br />
    Click here if nothing happens...
    <input type="image" src="http://www.paypal.com/en_US/i/btn/x-click-but01.gif" name="submit"
        alt="Make payments with PayPal - it's fast, free and secure!" />
    <input name="business" type="hidden" value="mgrimes2@hotmail.com">
    <input name="rm" type="hidden" value="2">
    <input name="no_shipping" type="hidden" value="1">
    <input name="no_note" type="hidden" value="1">
    <input name="cs" type="hidden" value="0">
    <input name="currency_code" type="hidden" value="USD">
    <input type="hidden" name="cmd" value="_xclick">
    <input type="hidden" name="item_name" value="HOT Tropical Tans Online Purchase">
    <input type="hidden" name="currency_code" value="USD">
    

    <%

        Response.Write("<input type=\"hidden\" name=\"amount\" value=\"" + HttpContext.Current.Session["cartTotal"].ToString() + "\">");
        //HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        //double totalTaxed = 0, cartTax = 0, discountedPrice = 0;
        //int count = 0;

        //try
        //{
        //    List<HOTBAL.CartItem> cart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
        //    if (cart.CartItem.Length > 0)
        //    {
        //        foreach (HOTBAL.CartItem item in cart)
        //        {
        //            Response.Write("<input name=\"item_name_" + count.ToString() + "\" type=\"hidden\" value=\"" + item.ItemName + "\"> ");
        //            Response.Write("<input name=\"item_number_" + count.ToString() + "\" type=\"hidden\" value=\"" + item.ItemID.ToString() + "\"> ");
        //            //if (item.ItemType == "LTN")
        //            //{
        //            //    discountedPrice = item.ItemPrice * .15;
        //            //}
        //            //else
        //            //{
        //                discountedPrice = item.ItemPrice;
        //            //}
        //            Response.Write("<input name=\"amount_" + count.ToString() + "\" type=\"hidden\" value=\"" + discountedPrice + "\"> ");
        //            Response.Write("<input name=\"quantity_" + count.ToString() + "\" type=\"hidden\" value=\"" + item.ItemQuantity.ToString() + "\"> ");

        //            if (item.ItemTaxed)
        //            {
        //                totalTaxed = totalTaxed + (discountedPrice * item.ItemQuantity);
        //            }
        //            count++;
        //        }
        //    }

        //    Response.Write("<input name=\"num_cart_items\" type=\"hidden\" value=\"" + count.ToString() + "\"> ");
        //    cartTax = totalTaxed * 0.0825;
        //    Response.Write("<input type='hidden' name='tax_cart' value='" + cartTax + "'> ");
        //}
        //catch (Exception ex)
        //{
        //    functionsClass.SendErrorMail("Site: PayPalRedirect", ex.StackTrace, ex.Message, "");
        //}
    %>
    </form>
</body>
</html>