using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTPOS
{
    public partial class GiftCards : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.POSConstants.INTERNAL_NAME + " - Gift Cards";
            if (Request.QueryString["Confirm"] != null)
            {
                fromName.Text = functionsClass.LightCleanUp(Request.QueryString["F"].ToString());
                giftAmount.Text = functionsClass.CleanUp(Request.QueryString["A"].ToString());
                giftDescription.Text = functionsClass.LightCleanUp(Request.QueryString["D"].ToString());
                employeeNumber.Text = functionsClass.CleanUp(Request.QueryString["E"].ToString());
                paymentMethod.Items.FindByValue(Request.QueryString["P"].ToString()).Selected = true;
                AddGiftCard(Convert.ToInt32(Request.QueryString["ID"]));
            }
        }

        public void submitGift_Click(Object sender, EventArgs e)
        {
            List<HOTBAL.Customer> customer = new List<HOTBAL.Customer>();
            Int64 customerID = 0;

            if (newCustomer.Checked)
            {
                customerID = sqlClass.InsertNewCustomer(toFirstName.Text,
                    toLastName.Text,
                    DateTime.Now, 0, "Other",
                    DateTime.Now, "Start First Tan", 
                    false, false, false, 0, Convert.ToDateTime("2001-01-01"));

                AddGiftCard(customerID);
            }
            else
            {
                customer = sqlClass.GetCustomerByName(toFirstName.Text, toLastName.Text, true);

                if (customer.Count == 0)
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += "No users named " + functionsClass.LightCleanUp(toFirstName.Text.Trim()) + " " + functionsClass.LightCleanUp(toLastName.Text.Trim()) + ".  Try again or <a href='" + HOTBAL.TansConstants.CUSTOMER_ADD_INTERNAL_URL + "'>add a new user</a>.";
                }
                else if (customer.Count > 1)
                {
                    customerList.Text = "<table class='tanning'>";
                    customerList.Text += "<thead><tr><th>Customer Results</th></tr></thead><tbody>";
                    foreach (HOTBAL.Customer c in customer)
                    {
                        customerList.Text += "<tr><td><a href='" + HOTBAL.POSConstants.GIFT_CARDS_URL + "?Confirm=Y&ID="
                            + c.ID
                            + "&F=" + functionsClass.LightCleanUp(fromName.Text)
                            + "&A=" + functionsClass.CleanUp(giftAmount.Text)
                            + "&D=" + functionsClass.LightCleanUp(giftDescription.Text)
                            + "&E=" + functionsClass.CleanUp(employeeNumber.Text)
                            + "&P=" + functionsClass.LightCleanUp(paymentMethod.SelectedValue)
                            + "'>" + c.LastName + ", " + c.FirstName + "</a></td></tr>";
                    }
                    customerList.Text = "</tbody></table>";
                }
                else
                {
                    AddGiftCard(customer[0].ID);
                }
            }
        }

        public void AddGiftCard(Int64 userID)
        {
            try
            {
                bool response = sqlClass.AddGiftCard(userID, functionsClass.LightCleanUp(fromName.Text), Convert.ToInt32(employeeNumber.Text), Convert.ToDouble(giftAmount.Text), DateTime.Now, functionsClass.LightCleanUp(giftDescription.Text));
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                sqlClass.LogErrorMessage(ex, "", "AddGiftCard: Add Card");
            }
            
            try
            {
                List<HOTBAL.CartItem> giftCardCart = new List<HOTBAL.CartItem>();
                HOTBAL.CartItem giftCardItem = new HOTBAL.CartItem();
                giftCardItem = new HOTBAL.CartItem();
                giftCardItem.ItemID = 69;
                giftCardItem.ItemName = "Gift Card";
                giftCardItem.ItemPrice = Convert.ToDouble(giftAmount.Text);
                giftCardItem.ItemQuantity = 1;
                giftCardItem.ItemTaxed = false;
                giftCardItem.ItemType = "";
                giftCardCart.Add(giftCardItem);

                Int64 response = sqlClass.InsertTransaction(giftCardCart, 
                    Convert.ToInt32(employeeNumber.Text), 
                    giftAmount.Text, 
                    userID, 
                    "W", 
                    paymentMethod.SelectedValue, 
                    functionsClass.FormatDash(DateTime.Now), 
                    "0", "1", "");

                if (response > 0)
                {
                    Response.Redirect(HOTBAL.POSConstants.DEFAULT_URL);
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    sqlClass.LogErrorMessage(new Exception("ErrorAddingTransaction"), "", "AddGiftCard: Add Transaction");
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text += HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                sqlClass.LogErrorMessage(ex, "", "AddGiftCard: Add Transaction");
            }
        }
    }
}