using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;
using System.Globalization;

namespace SDAPOS
{
    public partial class Cart : System.Web.UI.Page
    {
        SDAFunctionsClass FunctionsClass = new SDAFunctionsClass();
        SDAMethods sqlClass = new SDAMethods();
        CultureInfo ci = new CultureInfo("en-us");

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Header.Title = "HOT Self Defense - Point of Sale Cart";
                if ((!Page.IsPostBack) && (Request.QueryString["Action"].ToString() != "Remove"))
                {
                    HttpContext.Current.Session["Cart"] = null;
                    transactionDate.Text = DateTime.Now.ToShortDateString();

                    outputCartList();
                    clearFields();
                }

                if (Request.QueryString["ID"] == null)
                {
                    errorMessage.Text = "Unable to locate student.";

                    outputCartList();
                    clearFields();
                }
                else
                {
                    if (String.IsNullOrEmpty(Request.QueryString["ID"].ToString()))
                    {
                        errorMessage.Text = "Unable to locate student.";

                        outputCartList();
                        clearFields();
                    }
                    else
                    {
                        int customerNumber = Convert.ToInt32(Request.QueryString["ID"].ToString());

                        if (customerNumber > 0)
                        {
                            Student studentInfo = sqlClass.GetStudentInformation(customerNumber);

                            if (String.IsNullOrEmpty(studentInfo.Error))
                            {
                                customerName.Text = studentInfo.FirstName + " " + studentInfo.LastName;
                            }
                            else
                            {
                                errorMessage.Text = studentInfo.Error;

                                outputCartList();
                                clearFields();
                            }
                        }
                        else
                        {
                            customerName.Text = Request.QueryString["FN"].ToString() + " " + Request.QueryString["LN"].ToString();
                            isMember.Checked = false;
                        }

                        if (Request.QueryString["Action"].ToString() == "Remove")
                        {
                            RemoveItem(Request.QueryString["ItemID"].ToString(), Request.QueryString["ItemName"].ToString());

                            outputCartList();
                            clearFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sqlClass.LogErrorMessage(ex, "", "POS: Cart: PageLoad");
                errorMessage.Text = SDAMessages.ERROR_GENERIC; ;
            }
        }

        protected void addBarCode_Click(object sender, EventArgs e)
        {
            HOTBAL.Product barcodeItem = sqlClass.GetItemByBarCode(barCodeText.Text.ToString().Trim());

            if (barcodeItem.ProductID > 0)
            {
                double itemPrice = 0.00;
                if (isMember.Checked)
                    itemPrice = (barcodeItem.ProductSaleInStore ? barcodeItem.ProductSalePrice : barcodeItem.ProductPrice);
                else
                    itemPrice = barcodeItem.ProductPrice;

                AddItem(barcodeItem.ProductID.ToString(), barcodeItem.ProductType + "-" + barcodeItem.ProductSubType + "-" + barcodeItem.ProductName,
                    itemPrice.ToString(), barCodeQuantity.Text, barcodeItem.ProductType, barcodeItem.ProductTaxable);
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }

            outputCartList();
            clearFields();
        }

        protected void addNonBarcode_Click(object sender, EventArgs e)
        {
            AddItem("69", itemName.Text, itemPrice.Text, itemQuantity.Text, "OTH", itemTaxed.Checked);

            outputCartList();
            clearFields();
        }

        protected void checkOut_Click(object sender, EventArgs e)
        {
            bool response = sqlClass.AddTransaction((List<CartItem>)HttpContext.Current.Session["cart"], Convert.ToInt32(employeeId.Text), HttpContext.Current.Session["cartTotal"].ToString(), Convert.ToInt32(Request.QueryString["ID"]),
                "W", paymentMethod.SelectedValue, FunctionsClass.FormatDash(Convert.ToDateTime(transactionDate.Text)), HttpContext.Current.Session["cartTax"].ToString(), tradeNumber.Text);

            if (response)
            {
                pnlCart.Style.Add("display", "none");
                pnlComplete.Style.Remove("display");
                litComplete.Text = "<h3>Transaction recorded.</h3><br><a href='TransactionReceipt.aspx?Date=" + transactionDate.Text + "&ID=" + response + "'>Click here for a receipt</a>"
                    + "<br><br><a href='default.aspx'>Back to Point of Sale</a><br><br><a href='http://www.hottropicaltans.net/HOTSDA/schedule/'>Back to Schedule</a>";
            }
            else
            {
                errorMessage.Text = SDAMessages.ERROR_GENERIC;
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
                    List<CartItem> shoppingCart = (List<CartItem>)HttpContext.Current.Session["Cart"];
                    //HttpContext.Current.Session.Clear();
                    List<CartItem> shoppingCartRefresh = new List<CartItem>();

                    try
                    {
                        foreach (CartItem item in shoppingCart)
                        {
                            if (item.ItemID.ToString() == itemID)
                            {
                                if (item.ItemName.ToString() == itemName)
                                {
                                    //Item we're looking for, add it to the cart + 1
                                    CartItem cartItem = new CartItem();
                                    cartItem.ItemID = item.ItemID;
                                    cartItem.ItemName = item.ItemName;
                                    cartItem.ItemPrice = item.ItemPrice;
                                    cartItem.ItemQuantity = item.ItemQuantity + 1;
                                    cartItem.ItemTaxed = item.ItemTaxed;
                                    cartItem.ItemType = item.ItemType;
                                    shoppingCartRefresh.Add(cartItem);
                                    flag = true;
                                }
                                else
                                {
                                    //Not the item, add it back
                                    CartItem cartItem = new CartItem();
                                    cartItem.ItemID = item.ItemID;
                                    cartItem.ItemName = item.ItemName;
                                    cartItem.ItemPrice = item.ItemPrice;
                                    cartItem.ItemQuantity = item.ItemQuantity;
                                    cartItem.ItemTaxed = item.ItemTaxed;
                                    cartItem.ItemType = item.ItemType;
                                    shoppingCartRefresh.Add(cartItem);
                                }
                            }
                            else
                            {
                                //Not the item, add it back
                                CartItem cartItem = new CartItem();
                                cartItem.ItemID = item.ItemID;
                                cartItem.ItemName = item.ItemName;
                                cartItem.ItemPrice = item.ItemPrice;
                                cartItem.ItemQuantity = item.ItemQuantity;
                                cartItem.ItemTaxed = item.ItemTaxed;
                                cartItem.ItemType = item.ItemType;
                                shoppingCartRefresh.Add(cartItem);
                            }
                        }

                        if (!flag)
                        {
                            //Item not already in the cart, add it
                            CartItem cartItem = new CartItem();
                            cartItem.ItemID = Convert.ToInt32(itemID);
                            cartItem.ItemName = itemName;
                            cartItem.ItemPrice = Convert.ToDouble(itemPrice);
                            cartItem.ItemType = itemType;
                            cartItem.ItemQuantity = Convert.ToInt32(itemQuantity);
                            cartItem.ItemTaxed = itemTax;
                            shoppingCartRefresh.Add(cartItem);
                        }

                        HttpContext.Current.Session["Cart"] = shoppingCartRefresh;
                    }
                    catch (Exception ex)
                    {
                        FunctionsClass.SendErrorMail("SDAPOS: AddItem2", ex, "");
                        errorMessage.Text += "Error adding item information<br>";
                    }
                }
                else
                {
                    //Cart was empty
                    List<CartItem> shoppingCart = new List<CartItem>();
                    CartItem cartItem = new CartItem();
                    cartItem.ItemID = Convert.ToInt32(itemID);
                    cartItem.ItemName = itemName;
                    cartItem.ItemPrice = Convert.ToDouble(itemPrice);
                    cartItem.ItemType = itemType;
                    cartItem.ItemQuantity = Convert.ToInt32(itemQuantity);
                    cartItem.ItemTaxed = itemTax;
                    shoppingCart.Add(cartItem);

                    HttpContext.Current.Session["Cart"] = shoppingCart;
                }
            }
            catch (Exception ex)
            {
                FunctionsClass.SendErrorMail("SDAPOS: AddItem", ex, "");
                errorMessage.Text += "Error getting item information<br>";
            }
        }

        private void RemoveItem(string itemID, string itemName)
        {
            try
            {
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    //Divide up the individual items
                    List<CartItem> shoppingCart = (List<CartItem>)HttpContext.Current.Session["Cart"];
                    //HttpContext.Current.Session.Clear();
                    List<CartItem> shoppingCartRefresh = new List<CartItem>();

                    try
                    {
                        foreach (CartItem item in shoppingCart)
                        {
                            if (item.ItemID.ToString() == itemID)
                            {
                                if (item.ItemName.ToString() == itemName)
                                {
                                    //Item we're looking for, remove it from the cart
                                    if (item.ItemQuantity > 1)
                                    {
                                        CartItem cartItem = new CartItem();
                                        cartItem.ItemID = item.ItemID;
                                        cartItem.ItemName = item.ItemName;
                                        cartItem.ItemPrice = item.ItemPrice;
                                        cartItem.ItemQuantity = item.ItemQuantity - 1;
                                        cartItem.ItemTaxed = item.ItemTaxed;
                                        cartItem.ItemType = item.ItemType;
                                        shoppingCartRefresh.Add(cartItem);
                                    }
                                }
                                else
                                {
                                    //Not the item, add it back
                                    CartItem cartItem = new CartItem();
                                    cartItem.ItemID = item.ItemID;
                                    cartItem.ItemName = item.ItemName;
                                    cartItem.ItemPrice = item.ItemPrice;
                                    cartItem.ItemQuantity = item.ItemQuantity;
                                    cartItem.ItemTaxed = item.ItemTaxed;
                                    cartItem.ItemType = item.ItemType;
                                    shoppingCartRefresh.Add(cartItem);
                                }
                            }
                            else
                            {
                                //Not the item, add it back
                                CartItem cartItem = new CartItem();
                                cartItem.ItemID = item.ItemID;
                                cartItem.ItemName = item.ItemName;
                                cartItem.ItemPrice = item.ItemPrice;
                                cartItem.ItemQuantity = item.ItemQuantity;
                                cartItem.ItemTaxed = item.ItemTaxed;
                                cartItem.ItemType = item.ItemType;
                                shoppingCartRefresh.Add(cartItem);
                            }

                        }

                        HttpContext.Current.Session["Cart"] = shoppingCartRefresh;
                    }
                    catch (Exception ex)
                    {
                        FunctionsClass.SendErrorMail("SDAPOS: RemoveItem2", ex, "");
                        errorMessage.Text += "Error adding item information<br>";
                    }
                }
            }
            catch (Exception ex)
            {
                FunctionsClass.SendErrorMail("SDAPOS: RemoveItem", ex, "");
                errorMessage.Text += "Error getting item information<br>";
            }
        }

        private void outputCartList()
        {
            Double taxTotal = 0, nonTaxTotal = 0, cartTotal = 0;
            if (HttpContext.Current.Session["Cart"] != null)
            {
                List<HOTBAL.CartItem> shoppingCart = (List<HOTBAL.CartItem>)HttpContext.Current.Session["Cart"];
                shoppingCartOutput.Text = "";
                foreach (HOTBAL.CartItem item in shoppingCart)
                {
                    shoppingCartOutput.Text += "<tr><td class='standardField'>" + item.ItemID
                        + "</td><td class='standardField'>" + item.ItemName
                        + "</td><td class='standardField'>" + item.ItemQuantity
                        + "</td><td class='standardField'>" + String.Format("{0:C2}", item.ItemPrice)
                        + "</td><td class='standardField'>" + String.Format("{0:C2}", (item.ItemPrice * item.ItemQuantity))
                        + "</td><td class='standardField'><a href='Cart.aspx?Action=Remove&ID=" + Request.QueryString["ID"].ToString() + "&ItemID=" + item.ItemID + "&ItemName=" + item.ItemName + "'>Remove</a>"
                        + "</td></tr>";
                    if (item.ItemTaxed)
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
                shoppingCartOutput.Text = "<tr><td colspan='7' class='standardField'>No items present</td></tr>";
            }

            shoppingCartOutput.Text += "<tr><td COLSPAN=4 class='rightAlignHeader'>Tax:</td><td class='standardField'>";
            Double cartTax = 0.00;
            cartTax = taxTotal * 0.0825;
            HttpContext.Current.Session["cartTax"] = cartTax;
            shoppingCartOutput.Text += String.Format("{0:C2}", Math.Round(cartTax, 2)) + "</td><td class='standardField'><br></td></tr>";
            shoppingCartOutput.Text += "<tr><td COLSPAN=4 class='rightAlignHeader'>Total:</td><td class='standardField'>";
            cartTotal = nonTaxTotal + taxTotal + cartTax;
            HttpContext.Current.Session["cartTotal"] = cartTotal;
            shoppingCartOutput.Text += String.Format("{0:C2}", Math.Round(cartTotal, 2)) + "</td><td class='standardField'><br></td></tr>";
        }

        private void clearFields()
        {
            barCodeText.Text = "";
            barCodeQuantity.Text = "1";
            itemName.Text = "";
            itemPrice.Text = "";
            itemQuantity.Text = "1";
            itemTaxed.Checked = false;
            isMember.Checked = true;
        }
    }
}