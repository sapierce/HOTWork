using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Manager;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PublicWebsite
{
    public partial class ShoppingCart : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        private CultureInfo ci = new CultureInfo("en-us");

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.PUBLIC_NAME + " - Shopping Cart";
            HttpContext.Current.Session.Timeout = 30;
            try
            {
                if (!functionsClass.isLoggedIn())
                {
                    //User is logged out
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = "Please log in to view your cart.";
                }
                else
                {
                    if (!Page.IsPostBack)
                    {
                        HOTBAL.Customer user = new HOTBAL.Customer();
                        user = sqlClass.GetCustomerInformationByID(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()));

                        Double taxTotal = 0, nonTaxTotal = 0, cartTotal = 0;

                        //errorMessage.Text += "Action:" + Request.QueryString["action"];
                        if (Request.QueryString["action"] != null)
                        {
                            if (String.IsNullOrEmpty(user.Error))
                            {
                                if (Request.QueryString["action"].ToString() == "remove")
                                {
                                    RemoveItem(Convert.ToInt32(Request.QueryString["ItemID"]));
                                }
                                else if (Request.QueryString["action"].ToString() == "add")
                                {
                                    //errorMessage.Text += "Getting product:" + Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ItemID"])).ToString();
                                    HOTBAL.Product getProduct = sqlClass.GetProductByID(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ItemID"])));

                                    if (getProduct.ProductId > 0)
                                    {
                                        //errorMessage.Text += "Adding product";
                                        AddItem(getProduct.ProductId, getProduct.ProductType + "-" + getProduct.ProductSubType + "-" + getProduct.ProductName,
                                                    (getProduct.IsOnSaleOnline ? getProduct.ProductSalePrice : getProduct.ProductPrice).ToString(),
                                                    "1", getProduct.ProductType, getProduct.IsTaxable);
                                    }
                                    else
                                    {
                                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                        errorLabel.Text += "Unable to find product:" + getProduct.ProductId.ToString();
                                    }
                                }
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += user.Error;
                            }
                        }

                        if (HttpContext.Current.Session["Cart"] != null)
                        {
                            List<HOTBAL.CartItem> shoppingCart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
                            shoppingCartOutput.Text = "";
                            if (shoppingCart.Count > 0)
                            {
                                foreach (HOTBAL.CartItem item in shoppingCart)
                                {
                                    shoppingCartOutput.Text += "<tr><td>" + item.ItemId + "</td><td class='reg'>" + item.ItemName.Replace("-", " ").ToUpper() 
                                        + "</td><td class='reg'>" + item.ItemQuantity
                                        + "</td><td class='reg'>" + String.Format("{0:C}", item.ItemPrice)
                                        + "</td><td class='reg'>" + String.Format("{0:C}", (item.ItemPrice * item.ItemQuantity))
                                        + "</td><td class='reg'><a href='" + HOTBAL.TansConstants.SHOPPING_PUBLIC_URL + "?Action=remove&ItemID=" + 
                                        item.ItemId + "&count=1'>Remove</a>"
                                        + "</td></tr>";

                                    if (item.ItemIsTaxed)
                                    {
                                        taxTotal = (taxTotal + (item.ItemPrice * item.ItemQuantity));
                                    }
                                    else
                                    {
                                        nonTaxTotal = (nonTaxTotal + (item.ItemPrice * item.ItemQuantity));
                                    }
                                }
                                shoppingCartOutput.Text += "<tr><td COLSPAN=4 class='rheader'>Tax:</td><td class='reg'>";
                                Double cartTax = 0.00;
                                cartTax = taxTotal * 0.0825;
                                HttpContext.Current.Session["cartTax"] = cartTax;
                                shoppingCartOutput.Text += String.Format("{0:C}", cartTax) + "</td><td class='reg'><br></td></tr>";
                                shoppingCartOutput.Text += "<tr><td COLSPAN=4 class='rheader'>Total:</td><td class='reg'>";
                                cartTotal = nonTaxTotal + taxTotal + cartTax;
                                HttpContext.Current.Session["cartTotal"] = cartTotal;
                                shoppingCartOutput.Text += String.Format("{0:C}", cartTotal) + "</td><td class='reg'><br></td></tr>";
                            }
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text += "Nothing in the cart!";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "SitePOS: PageLoad");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error loading cart<br>";
            }
        }

        private void AddItem(int itemID, string itemName, string itemPrice, string itemQuantity, string itemType, bool itemTax)
        {
            try
            {
                bool flag = false;
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    HttpContext.Current.Session["owedProduct"] = "false";
                    //Divide up the individual items
                    List<HOTBAL.CartItem> shoppingCart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
                    //HttpContext.Current.Session.Clear();
                    List<HOTBAL.CartItem> shoppingCartRefresh = new List<HOTBAL.CartItem>();

                    try
                    {
                        foreach (HOTBAL.CartItem item in shoppingCart)
                        {
                            if (item.ItemId == itemID)
                            {
                                if (item.ItemName.ToString() == itemName)
                                {
                                    //Item we're looking for, add it to the cart + 1
                                    HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                                    cartItem.ItemId = item.ItemId;
                                    cartItem.ItemName = item.ItemName;
                                    cartItem.ItemPrice = item.ItemPrice;
                                    cartItem.ItemQuantity = item.ItemQuantity + 1;
                                    cartItem.ItemIsTaxed = item.ItemIsTaxed;
                                    cartItem.ItemType = item.ItemType;
                                    shoppingCartRefresh.Add(cartItem);
                                    flag = true;
                                    if (item.ItemType != "PKG")
                                        HttpContext.Current.Session["owedProduct"] = "true";
                                }
                                else
                                {
                                    //Not the item, add it back
                                    HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                                    cartItem.ItemId = item.ItemId;
                                    cartItem.ItemName = item.ItemName;
                                    cartItem.ItemPrice = item.ItemPrice;
                                    cartItem.ItemQuantity = item.ItemQuantity;
                                    cartItem.ItemIsTaxed = item.ItemIsTaxed;
                                    cartItem.ItemType = item.ItemType;
                                    shoppingCartRefresh.Add(cartItem);
                                    if (item.ItemType != "PKG")
                                        HttpContext.Current.Session["owedProduct"] = "true";
                                }
                            }
                            else
                            {
                                //Not the item, add it back
                                HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                                cartItem.ItemId = item.ItemId;
                                cartItem.ItemName = item.ItemName;
                                cartItem.ItemPrice = item.ItemPrice;
                                cartItem.ItemQuantity = item.ItemQuantity;
                                cartItem.ItemIsTaxed = item.ItemIsTaxed;
                                cartItem.ItemType = item.ItemType;
                                shoppingCartRefresh.Add(cartItem);
                                if (item.ItemType != "PKG")
                                    HttpContext.Current.Session["owedProduct"] = "true";
                            }
                        }
                        if (!flag)
                        {
                            //Item not already in the cart, add it
                            HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                            cartItem.ItemId = Convert.ToInt32(itemID);
                            cartItem.ItemName = itemName;
                            if (itemType == "LTN")
                            {
                                cartItem.ItemPrice = (Convert.ToDouble(itemPrice) - (Convert.ToDouble(itemPrice) * .15));
                            }
                            else
                            {
                                cartItem.ItemPrice = Convert.ToDouble(itemPrice);
                            }
                            cartItem.ItemType = itemType;
                            cartItem.ItemQuantity = Convert.ToInt32(itemQuantity);
                            cartItem.ItemIsTaxed = itemTax;
                            shoppingCartRefresh.Add(cartItem);

                            if (itemType != "PKG")
                                HttpContext.Current.Session["owedProduct"] = "true";
                        }

                        HttpContext.Current.Session["Cart"] = shoppingCartRefresh;
                    }
                    catch (Exception ex)
                    {
                        sqlClass.LogErrorMessage(ex, "", "SitePOS: AddItem2");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += "Error adding item information<br>";
                    }
                }
                else
                {
                    //Cart was empty
                    List<HOTBAL.CartItem> shoppingCartRefresh = new List<HOTBAL.CartItem>();
                    HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                    cartItem.ItemId = Convert.ToInt32(itemID);
                    cartItem.ItemName = itemName;
                    if (itemType == "LTN")
                    {
                        cartItem.ItemPrice = (Convert.ToDouble(itemPrice) - (Convert.ToDouble(itemPrice) * .15));
                    }
                    else
                    {
                        cartItem.ItemPrice = Convert.ToDouble(itemPrice);
                    }
                    cartItem.ItemType = itemType;
                    cartItem.ItemQuantity = Convert.ToInt32(itemQuantity);
                    cartItem.ItemIsTaxed = itemTax;
                    shoppingCartRefresh.Add(cartItem);

                    if (itemType != "PKG")
                        HttpContext.Current.Session["owedProduct"] = "true";
                    else
                        HttpContext.Current.Session["owedProduct"] = "false";
                    HttpContext.Current.Session["Cart"] = shoppingCartRefresh;
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "SitePOS: AddItem");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error getting item information<br>";
            }
        }

        private void RemoveItem(int itemID)
        {
            try
            {
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    //Divide up the individual items
                    List<HOTBAL.CartItem> shoppingCart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
                    //HttpContext.Current.Session.Clear();
                    List<HOTBAL.CartItem> shoppingCartRefresh = new List<HOTBAL.CartItem>();

                    try
                    {
                        foreach (HOTBAL.CartItem item in shoppingCart)
                        {
                            if (item.ItemId == itemID)
                            {
                                //Item we're looking for, remove it from the cart
                                if (item.ItemQuantity > 1)
                                {
                                    HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                                    cartItem.ItemId = item.ItemId;
                                    cartItem.ItemName = item.ItemName;
                                    cartItem.ItemPrice = item.ItemPrice;
                                    cartItem.ItemQuantity = item.ItemQuantity - 1;
                                    cartItem.ItemIsTaxed = item.ItemIsTaxed;
                                    cartItem.ItemType = item.ItemType;
                                    shoppingCartRefresh.Add(cartItem);
                                }
                            }
                            else
                            {
                                //Not the item, add it back
                                HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                                cartItem.ItemId = item.ItemId;
                                cartItem.ItemName = item.ItemName;
                                cartItem.ItemPrice = item.ItemPrice;
                                cartItem.ItemQuantity = item.ItemQuantity;
                                cartItem.ItemIsTaxed = item.ItemIsTaxed;
                                cartItem.ItemType = item.ItemType;
                                shoppingCartRefresh.Add(cartItem);
                            }
                        }

                        HttpContext.Current.Session["Cart"] = shoppingCartRefresh;
                    }
                    catch (Exception ex)
                    {
                        sqlClass.LogErrorMessage(ex, "", "SitePOS: RemoveItem2");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += "Error adding item information<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "SitePOS: RemoveItem");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error getting item information<br>";
            }
        }

        public void onClick_inStore(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Cart"] == null)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Nothing in the cart!";
            }
            else if (HttpContext.Current.Session["userID"] == null)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "Please log in to view your cart.";
            }
            else
            {
                CheckoutInStore();
            }
        }

        public void onClick_online(Object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["Cart"] == null)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Nothing in the cart!";
            }
            else if (HttpContext.Current.Session["userID"] == null)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "Please log in to view your cart.";
            }
            else
            {
                CheckoutOnline();
            }
        }

        private void CheckoutInStore()
        {
            try
            {
                Int64 insertTransaction = sqlClass.InsertTransaction((List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"], 
                    0, Convert.ToDouble(HttpContext.Current.Session["cartTotal"].ToString()).ToString(), 
                    Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()), "W", "ONLINE", functionsClass.FormatDash(DateTime.Now).ToString(), 
                    HttpContext.Current.Session["cartTax"].ToString(), "0", "");

                if (insertTransaction > 0)
                {
                    bool response = sqlClass.AddCustomerNote(Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()),
                            HOTBAL.TansMessages.NOTE_OWES.Replace("@TransactionID", insertTransaction.ToString()), true, 
                            (HttpContext.Current.Session["owedProduct"].ToString() == "true" ? true : false), false);
                    HttpContext.Current.Session["cart"] = "";
                    Response.Redirect(HOTBAL.TansConstants.SHOPPING_DONE_PUBLIC_URL + "?F=InStore");
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "CheckOutInStore");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }

        private void CheckoutOnline()
        {
            try
            {
                double cartTotal = Convert.ToDouble(HttpContext.Current.Session["cartTotal"].ToString());
                double cartTax = Convert.ToDouble(HttpContext.Current.Session["cartTax"].ToString());

                if (HttpContext.Current.Session["userID"] != null)
                {
                    Int64 insertTransaction = sqlClass.InsertTransaction((List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"],
                        99, cartTotal.ToString(), Convert.ToInt64(HttpContext.Current.Session["userID"].ToString()), "W", "PAYPAL", 
                        functionsClass.FormatDash(DateTime.Now).ToString(), cartTax.ToString(), "0", "");

                    if (insertTransaction > 0)
                    {
                        bool owedProduct = false;
                        if (HttpContext.Current.Session["owedProduct"] != null)
                        {
                            owedProduct = true;
                        }

                        bool response = sqlClass.AddCustomerNote(Convert.ToInt32(HttpContext.Current.Session["userID"].ToString()), 
                            HOTBAL.TansMessages.NOTE_CHECK_TRANS.Replace("@TransactionID", insertTransaction.ToString()), false, owedProduct, true);

                        //HOTBAL.Customer customerInformation = sqlClass.GetCustomerInformationByID(userID);

                        //try
                        //{
                            Response.Redirect(HOTBAL.TansConstants.SHOPPING_REDIRECT_PUBLIC_URL, false);
                        //    // Create request object
                        //    SetExpressCheckoutRequestType request = new SetExpressCheckoutRequestType();
                        //    populateRequestObject(request, customerInformation, (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"]);

                        //    // Invoke the API
                        //    SetExpressCheckoutReq wrapper = new SetExpressCheckoutReq();
                        //    wrapper.SetExpressCheckoutRequest = request;
                        //    PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();
                        //    SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);

                        //    // Check for API return status
                        //    HttpContext CurrContext = HttpContext.Current;
                        //    CurrContext.Items.Add("paymentDetails", request.SetExpressCheckoutRequestDetails.PaymentDetails);
                        //    setKeyResponseObjects(service, setECResponse);
                        //}
                        //catch (Exception ex)
                        //{
                        //    sqlClass.LogErrorMessage(ex, "", "PayPalCall");
                        //    errorMessage.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                        //}
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += "Nothing in the cart!";
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "CheckOutOnline");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }

        private void populateRequestObject(SetExpressCheckoutRequestType request, HOTBAL.Customer customerInformation, List<HOTBAL.CartItem> cartInformation)
        {
            try
            {
                SetExpressCheckoutRequestDetailsType ecDetails = new SetExpressCheckoutRequestDetailsType();
                ecDetails.ReturnURL = HOTBAL.TansConstants.PAYPAL_RETURN_URL;
                ecDetails.CancelURL = HOTBAL.TansConstants.PAYPAL_CANCEL_URL;
                ecDetails.BuyerEmail = customerInformation.Email;
                ecDetails.ReqConfirmShipping = "0";
                ecDetails.AddressOverride = "0";
                ecDetails.NoShipping = "1";
                ecDetails.SolutionType = (SolutionTypeType)Enum.Parse(typeof(SolutionTypeType), "SOLE");

                /* Populate payment requestDetails.
                 * SetExpressCheckout allows parallel payments of upto 10 payments.
                 * This samples shows just one payment.
                 */
                PaymentDetailsType paymentDetails = new PaymentDetailsType();
                ecDetails.PaymentDetails.Add(paymentDetails);
                double orderTotal = 0.0;
                double itemTotal = 0.0;
                CurrencyCodeType currency = (CurrencyCodeType)
                    Enum.Parse(typeof(CurrencyCodeType), "USD");

                paymentDetails.TaxTotal = new BasicAmountType(currency, HttpContext.Current.Session["cartTotal"].ToString());
                orderTotal += Double.Parse(HttpContext.Current.Session["cartTotal"].ToString());

                //if (orderDescription.Value != "")
                //{
                //    paymentDetails.OrderDescription = orderDescription.Value;
                //}
                paymentDetails.PaymentAction = (PaymentActionCodeType)
                    Enum.Parse(typeof(PaymentActionCodeType), "SALE");

                // Each payment can include requestDetails about multiple items
                // This example shows just one payment item
                foreach (HOTBAL.CartItem c in cartInformation)
                {
                    PaymentDetailsItemType itemDetails = new PaymentDetailsItemType();
                    itemDetails.Name = c.ItemName;
                    itemDetails.Amount = new BasicAmountType(currency, c.ItemPrice.ToString());
                    itemDetails.Quantity = Int32.Parse(c.ItemQuantity.ToString());
                    itemDetails.ItemCategory = (ItemCategoryType)
                        Enum.Parse(typeof(ItemCategoryType), "PHYSICAL");
                    itemTotal += Double.Parse(itemDetails.Amount.value) * itemDetails.Quantity.Value;
                    if (c.ItemIsTaxed)
                    {
                        itemDetails.Tax = new BasicAmountType(currency, (c.ItemPrice * .0825).ToString());
                        orderTotal += Double.Parse((c.ItemPrice * .0825).ToString());
                    }
                    itemDetails.Description = c.ItemName;
                    paymentDetails.PaymentDetailsItem.Add(itemDetails);
                }

                orderTotal += itemTotal;
                paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
                paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());

                //paymentDetails.NotifyURL = ipnNotificationUrl.Value.Trim();

                //// Set Billing agreement (for Reference transactions & Recurring payments)
                //if (billingAgreementText.Value != "")
                //{
                //    BillingCodeType billingCodeType = (BillingCodeType)
                //        Enum.Parse(typeof(BillingCodeType), "MERCHANTINITIATEDBILLINGSINGLEAGREEMENT");
                //    BillingAgreementDetailsType baType = new BillingAgreementDetailsType(billingCodeType);
                //    baType.BillingAgreementDescription = billingAgreementText.Value;
                //    ecDetails.BillingAgreementDetails.Add(baType);
                //}

                ecDetails.LocaleCode = "US";

                // Set styling attributes for PayPal page
                //if (pageStyle.Value != "")
                //{
                //    ecDetails.PageStyle = pageStyle.Value;
                //}
                ecDetails.cppHeaderImage = "http://www.hottropicaltans.com/images/HOTTansExternalHeader.jpg";
                ecDetails.cppHeaderBorderColor = "000000";
                ecDetails.cppHeaderBackColor = "C3A865";
                ecDetails.cppPayflowColor = "3399CC";
                ecDetails.BrandName = HOTBAL.TansConstants.PUBLIC_NAME;

                request.SetExpressCheckoutRequestDetails = ecDetails;
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "populateRequestObject");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
            }
        }

        // A helper method used by APIResponse.aspx that returns select response parameters
        // of interest.
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, SetExpressCheckoutResponseType setECResponse)
        {
            Dictionary<string, string> keyResponseParameters = new Dictionary<string, string>();
            keyResponseParameters.Add("API Status", setECResponse.Ack.ToString());
            HttpContext CurrContext = HttpContext.Current;
            if (setECResponse.Ack.Equals(AckCodeType.FAILURE) ||
                (setECResponse.Errors != null && setECResponse.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", setECResponse.Errors);
                CurrContext.Items.Add("Response_redirectURL", null);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                keyResponseParameters.Add("EC token", setECResponse.Token);
                //CurrContext.Items.Add("Response_redirectURL", ConfigManager.Instance.GetProperty("paypalUrl")
                //    + "_express-checkout&token=" + setECResponse.Token);
                CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
                CurrContext.Items.Add("Response_apiName", "SetExpressCheckout");
                CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
                CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
                Server.Transfer(CurrContext.Items["Response_redirectURL"].ToString());
            }
        }
    }
}