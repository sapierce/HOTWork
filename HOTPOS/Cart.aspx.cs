using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace HOTPOS
{
    public partial class Cart : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass TansFunctionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods TansSqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = HOTBAL.POSConstants.INTERNAL_NAME;
                checkDate();

                if (Request.QueryString["Action"] != null)
                {
                    if ((!Page.IsPostBack) && (Request.QueryString["Action"].ToString() != "Remove"))
                    {
                        HttpContext.Current.Session["Cart"] = null;
                        outputCartList();
                        clearFields();
                    }
                }

                if (Request.QueryString["ID"] == null)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL;

                    outputCartList();
                    clearFields();
                }
                else
                {
                    if (String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL;

                        outputCartList();
                        clearFields();
                    }
                    else
                    {
                        long customerNumber = Convert.ToInt64(Request.QueryString["ID"].ToString());

                        if (customerNumber > 0)
                        {
                            HOTBAL.Customer customerInfo = TansSqlClass.GetLimitedCustomerInformationByID(customerNumber);

                            if (String.IsNullOrEmpty(customerInfo.Error))
                            {
                                customerName.Text = customerInfo.FirstName + " " + customerInfo.LastName;
                                isMember.Checked = true;
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += customerInfo.Error;

                                outputCartList();
                                clearFields();
                            }
                        }
                        else
                        {
                            if (Request.QueryString["FN"] != null)
                            {
                                customerName.Text = Request.QueryString["FN"].ToString() + " " + Request.QueryString["LN"].ToString();
                                isMember.Checked = false;
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;

                                outputCartList();
                                clearFields();
                            }
                        }

                        if (Request.QueryString["Action"] != null)
                        {
                            if (Request.QueryString["Action"].ToString() == "Remove")
                            {
                                if (!Page.IsPostBack)
                                    RemoveItem(Request.QueryString["ItemID"].ToString(), Request.QueryString["ItemName"].ToString());

                                outputCartList();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TansSqlClass.LogErrorMessage(ex, "", "POS: Cart: PageLoad");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }

        protected void addOtherItem_OnClick(object sender, EventArgs e)
        {
            AddItem("69", itemName.Text, itemPrice.Text, itemQuantity.Text, "OTH", itemTax.Checked);

            outputCartList();
            clearFields();
        }

        protected void doTransaction_OnClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    string buildOtherString = String.Empty;

                    if (!String.IsNullOrEmpty(tradeNumber.Text))
                    {
                        buildOtherString = tradeNumber.Text;
                    }

                    if (!String.IsNullOrEmpty(otherNote.Text))
                    {
                        if (String.IsNullOrEmpty(buildOtherString))
                        {
                            buildOtherString = otherNote.Text;
                        }
                        else
                        {
                            buildOtherString += "-" + otherNote.Text;
                        }
                    }

                    if (Request.QueryString["FN"] != null)
                    {
                        if (String.IsNullOrEmpty(buildOtherString))
                        {
                            buildOtherString = Request.QueryString["FN"] + " " + Request.QueryString["LN"];
                        }
                        else
                        {
                            buildOtherString += "-" + Request.QueryString["FN"] + " " + Request.QueryString["LN"];
                        }
                    }

                    Int64 response = TansSqlClass.InsertTransaction(((List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"]), 
                        Convert.ToInt32(employeeID.Text), 
                        HttpContext.Current.Session["cartTotal"].ToString(), 
                        Convert.ToInt64(Request.QueryString["ID"].ToString()), 
                        "W", 
                        paymentMethod.SelectedValue, 
                        TansFunctionsClass.FormatDash(Convert.ToDateTime(transactionDate.Text)), 
                        HttpContext.Current.Session["cartTax"].ToString(), 
                        "1", 
                        buildOtherString);

                    if (response > 0)
                    {
                        pnlCart.Style.Add("display", "none");
                        pnlComplete.Style.Remove("display");
                        clearFields();
                        customerName.Text = String.Empty;
                        shoppingCartOutput.Text = String.Empty;
                        HttpContext.Current.Session["Cart"] = null;
                        HttpContext.Current.Session["cartTotal"] = null;
                        HttpContext.Current.Session["cartTax"] = null;
                        receiptView.NavigateUrl = HOTBAL.POSConstants.RECEIPT_URL + "?ID=" + response;
                        backToPOS.NavigateUrl = HOTBAL.POSConstants.DEFAULT_URL;
                        backToSchedule.NavigateUrl = HOTBAL.TansConstants.MAIN_INTERNAL_URL;
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                catch (Exception ex)
                {
                    TansFunctionsClass.SendErrorMail("doTransaction", ex, "");
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                }
            }
        }

        protected void addBarcodedItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(barCodeText.Text.ToString().Trim()))
                {
                    HOTBAL.Product barcodeItem = TansSqlClass.GetProductByBarCode(barCodeText.Text.ToString().Trim());

                    if (String.IsNullOrEmpty(barcodeItem.ErrorMessage))
                    {
                        double itemPrice = 0.00;
                        if (isMember.Checked)
                            itemPrice = (barcodeItem.IsOnSaleInStore ? barcodeItem.ProductSalePrice : barcodeItem.ProductPrice);
                        else
                            itemPrice = barcodeItem.ProductPrice;

                        AddItem(barcodeItem.ProductId.ToString(), barcodeItem.ProductType + "-" + barcodeItem.ProductSubType + "-" + barcodeItem.ProductName,
                            itemPrice.ToString(), barCodeQuantity.Text, barcodeItem.ProductType, barcodeItem.IsTaxable);
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += barcodeItem.ErrorMessage;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += HOTBAL.POSMessages.ERROR_NO_BARCODE;
                }
            }
            catch (Exception ex)
            {
                TansFunctionsClass.SendErrorMail("addBarcodedItem", ex, barCodeText.Text.ToString().Trim());
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
            finally
            {
                outputCartList();
                clearFields();
            }
        }

        private void AddItem(string itemID, string itemName, string itemPrice, string itemQuantity, string itemType, bool itemTax)
        {
            try
            {
                bool flag = false;

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
                            if (item.ItemId.ToString() == itemID)
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
                        if (!flag)
                        {
                            //Item not already in the cart, add it
                            HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                            cartItem.ItemId = Convert.ToInt32(itemID);
                            cartItem.ItemName = itemName;
                            cartItem.ItemPrice = Convert.ToDouble(itemPrice);
                            cartItem.ItemType = itemType;
                            cartItem.ItemQuantity = Convert.ToInt32(itemQuantity);
                            cartItem.ItemIsTaxed = itemTax;
                            shoppingCartRefresh.Add(cartItem);
                        }

                        HttpContext.Current.Session["Cart"] = shoppingCartRefresh;
                    }
                    catch (Exception ex)
                    {
                        TansSqlClass.LogErrorMessage(ex, "", "TansPOS: AddItem2");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += "Error adding item information<br>";
                    }
                }
                else
                {
                    //Cart was empty
                    List<HOTBAL.CartItem> shoppingCart = new List<HOTBAL.CartItem>();
                    HOTBAL.CartItem cartItem = new HOTBAL.CartItem();
                    cartItem.ItemId = Convert.ToInt32(itemID);
                    cartItem.ItemName = itemName;
                    cartItem.ItemPrice = Convert.ToDouble(itemPrice);
                    cartItem.ItemType = itemType;
                    cartItem.ItemQuantity = Convert.ToInt32(itemQuantity);
                    cartItem.ItemIsTaxed = itemTax;
                    shoppingCart.Add(cartItem);

                    HttpContext.Current.Session["Cart"] = shoppingCart;
                }
            }
            catch (Exception ex)
            {
                TansSqlClass.LogErrorMessage(ex, itemName, "TansPOS: AddItem");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error adding item information<br>";
            }
        }

        private void RemoveItem(string itemID, string itemName)
        {
            try
            {
                HOTBAL.Product productInfo = new HOTBAL.Product();
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    //Divide up the individual items
                    List<HOTBAL.CartItem> shoppingCart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
                    List<HOTBAL.CartItem> shoppingCartRefresh = new List<HOTBAL.CartItem>();

                    try
                    {
                        foreach (HOTBAL.CartItem item in shoppingCart)
                        {
                            if (item.ItemId.ToString() == itemID)
                            {
                                if (item.ItemName.ToString() == itemName)
                                {
                                    //Item we're looking for, remove it from the cart
                                    //  or decrease the item quantity if more than one
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
                        TansSqlClass.LogErrorMessage(ex, itemID, "TansPOS: RemoveItem2");
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text += "Error removing item information<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                TansSqlClass.LogErrorMessage(ex, itemID, "TansPOS: RemoveItem");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error removing item information<br>";
            }
        }

        private void outputCartList()
        {
            try
            { 
                Double taxTotal = 0, nonTaxTotal = 0, cartTotal = 0;
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    List<HOTBAL.CartItem> shoppingCart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
                    shoppingCartOutput.Text = "";
                    foreach (HOTBAL.CartItem item in shoppingCart)
                    {
                        shoppingCartOutput.Text += "<tr><td>" + item.ItemId
                            + "</td><td>" + item.ItemName
                            + "</td><td>" + item.ItemQuantity
                            + "</td><td>" + String.Format("{0:C2}", item.ItemPrice)
                            + "</td><td>" + String.Format("{0:C2}", (item.ItemPrice * item.ItemQuantity))
                            + "</td><td><a href='" + HOTBAL.POSConstants.CART_URL + "?Action=Remove&ID=" +
                                Request.QueryString["ID"].ToString() +
                                (Request.QueryString["FN"] != null ? "&FN=" + Request.QueryString["FN"] + "&LN=" + Request.QueryString["LN"] : "") +
                                "&ItemID=" + item.ItemId +
                                "&ItemName=" + item.ItemName +
                                "'>Remove</a>"
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
                }
                else
                {
                    shoppingCartOutput.Text = "<tr><td colspan='7'>No items present</td></tr>";
                }

                shoppingCartOutput.Text += "<tr><td COLSPAN=4 class='rightAlignHeader'>Tax:</td><td>";
                Double cartTax = 0.00;
                cartTax = taxTotal * 0.0825;
                HttpContext.Current.Session["cartTax"] = cartTax;
                shoppingCartOutput.Text += String.Format("{0:C2}", Math.Round(cartTax, 2)) + "</td><td><br></td></tr>";
                shoppingCartOutput.Text += "<tr><td COLSPAN=4 class='rightAlignHeader'>Total:</td><td>";
                cartTotal = nonTaxTotal + taxTotal + cartTax;
                HttpContext.Current.Session["cartTotal"] = cartTotal;
                shoppingCartOutput.Text += String.Format("{0:C2}", Math.Round(cartTotal, 2)) + "</td><td><br></td></tr>";
            }
            catch (Exception ex)
            {
                TansSqlClass.LogErrorMessage(ex, Request.QueryString["ID"].ToString(), "TansPOS: outputCartList");
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += "Error creating cart<br>";
            }
        }

        private void clearFields()
        {
            barCodeText.Text = "";
            barCodeQuantity.Text = "1";
            itemName.Text = "";
            itemPrice.Text = "";
            itemQuantity.Text = "1";
            itemTax.Checked = false;
        }

        private void checkDate()
        {
            if (String.IsNullOrEmpty(transactionDate.Text))
                transactionDate.Text = DateTime.Now.ToShortDateString();
        }
    }
}