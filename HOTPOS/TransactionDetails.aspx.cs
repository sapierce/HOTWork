using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTPOS
{
    public partial class TransactionDetails : System.Web.UI.Page
    {
        private HOTBAL.TansFunctionsClass TansFunctionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods TansSqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.POSConstants.INTERNAL_NAME + " - Transaction Details";

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    int transactionNumber = Convert.ToInt32(Request.QueryString["ID"].ToString());

                    transactionPayment.Items.Add(new ListItem("None", ""));
                    transactionPayment.Items.Add(new ListItem("Cash", "Cash"));
                    transactionPayment.Items.Add(new ListItem("CreditCard", "CC"));
                    transactionPayment.Items.Add(new ListItem("Check", "Check"));
                    transactionPayment.Items.Add(new ListItem("Trade", "Trade"));
                    transactionPayment.Items.Add(new ListItem("GiftCard", "GC"));
                    transactionPayment.Items.Add(new ListItem("Online", "ONLINE"));
                    transactionPayment.Items.Add(new ListItem("PayPal", "PAYPAL"));
                    transactionPayment.Items.Add(new ListItem("Other", "Other"));

                    HOTBAL.Transaction transactionDetails = TansSqlClass.GetCustomerTransaction(transactionNumber);

                    if (String.IsNullOrEmpty(transactionDetails.ErrorMessage))
                    {
                        List<HOTBAL.TransactionItem> transactionItems = TansSqlClass.GetCustomerTransactionItems(transactionDetails.TransactionId);
                        HOTBAL.Customer customerDetails = TansSqlClass.GetCustomerInformationByID(transactionDetails.CustomerId);

                        transactionID.Text = transactionDetails.TransactionId.ToString();
                        if (String.IsNullOrEmpty(customerDetails.Error))
                        {
                            transactionBuyer.Text = customerDetails.FirstName + " " + customerDetails.LastName;
                        }
                        else
                        {
                            transactionBuyer.Text = transactionDetails.CustomerId.ToString();
                        }
                        buyerID.Text = transactionDetails.CustomerId.ToString();
                        transactionSeller.Text = transactionDetails.SellerId;
                        transactionTotal.Text = String.Format("{0:C}", transactionDetails.TransactionTotal).Replace("$", "");
                        transactionDate.Text = TansFunctionsClass.FormatSlash(transactionDetails.TransactionDate);
                        isPaid.Checked = transactionDetails.IsTransactionPaid;
                        isVoid.Checked = transactionDetails.IsTransactionVoid;
                        transactionPayment.Items.FindByValue(transactionDetails.PaymentMethod).Selected = true;

                        if (transactionItems != null)
                        {
                            if (String.IsNullOrEmpty(transactionItems[0].ErrorMessage))
                            {
                                transactionItemsList.Text = "<table width='100%' style='border: 0px'><tr style='border: 0px'><td style='border: 0px; width: 67%'>" +
                                    "<strong>Item</strong></td><td style='border: 0px; width: 33%'><strong>Quantity</strong></td>" +
                                    "<td style='border: 0px; width: 33%'><br /></td></tr>";
                                foreach (HOTBAL.TransactionItem i in transactionItems)
                                {
                                    transactionItemsList.Text += "<tr style='border: 0px'><td style='border: 0px'>" + 
                                        i.ProductName + " @ " + String.Format("{0:C}", i.ProductPrice) + " each"
                                        + "</td><td style='border: 0px'>" + i.ItemQuantity +
                                        "</td><td style='border: 0px'><a href='" + HOTBAL.POSConstants.TRANSACTION_ITEM_DELETE_URL + "?ID="
                                        + i.TransactionItemId + "&TID=" + i.TransactionId + "'>Delete</a></td></tr>";
                                }
                                transactionItemsList.Text += "</table>";
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = "&#149; " + transactionItems[0].ErrorMessage;
                            }
                        }
                        else
                        {
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = "&#149; " + transactionDetails.ErrorMessage;
                        }
                    }
                }
            }
        }

        public void transactionEdit_Click(Object sender, EventArgs e)
        {
            bool response = false;
            int transactionNumber = Convert.ToInt32(Request.QueryString["ID"].ToString());

            response = TansSqlClass.UpdateTransaction(transactionNumber, transactionSeller.Text, transactionTotal.Text,
                    TansFunctionsClass.FormatDash(Convert.ToDateTime(transactionDate.Text)), transactionPayment.SelectedValue,
                    isVoid.Checked, isPaid.Checked);

            if (!response)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "&#149; " + HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
            else
            {
                Response.Redirect(HOTBAL.TansConstants.CUSTOMER_TRANS_INTERNAL_URL + "?ID=" + buyerID.Text);
            }
        }

        public void transactionReceipt_Click(Object sender, EventArgs e)
        {
            Response.Redirect(HOTBAL.POSConstants.RECEIPT_URL + "?ID=" + Request.QueryString["ID"].ToString());
        }
    }
}