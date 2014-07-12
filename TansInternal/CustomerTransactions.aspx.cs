using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerTransactions : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            HOTBAL.Customer customerInfo = sqlClass.GetCustomerInformationByID(Convert.ToInt32(functionsClass.CleanUp(Request.QueryString["ID"])));
            int transactionCount = 0;

            if (customerInfo != null)
            { 
                if (String.IsNullOrEmpty(customerInfo.Error))
                {
                    customerName.Text = customerInfo.FirstName + " " + customerInfo.LastName;

                    List<HOTBAL.Transaction> allCustomerTransactions = new List<HOTBAL.Transaction>();
                    allCustomerTransactions = sqlClass.GetAllCustomerTransactions(customerInfo.ID);

                    if (allCustomerTransactions != null)
                    {
                        if (allCustomerTransactions.Count > 0)
                        {
                            if (String.IsNullOrEmpty(allCustomerTransactions[0].Error))
                            {
                                foreach (HOTBAL.Transaction t in allCustomerTransactions)
                                {
                                    if (transactionCount < 10)
                                    {
                                        transactionLog.Text += "<tr><td>";

                                        if (t.Void)
                                        {
                                            transactionLog.Text += "<b>**VOID**</b><br />";
                                        }

                                        transactionLog.Text += "<a href='" + HOTBAL.POSConstants.TRANSACTION_DETAILS_URL + "?ID=" + t.ID.ToString() + "'>" + t.ID.ToString() + "</a><br />";
                                        transactionLog.Text += "<a href='" + HOTBAL.POSConstants.RECEIPT_URL + "?ID=" + t.ID.ToString() + "'>Receipt</a><br />";
                                        transactionLog.Text += "</td><td>" + functionsClass.FormatSlash(t.Date);
                                        transactionLog.Text += "</td><td>";

                                        List<HOTBAL.TransactionItem> itemList = new List<HOTBAL.TransactionItem>();
                                        itemList = sqlClass.GetTanningTransactionItems(t.ID);
                                        if (itemList != null)
                                        {
                                            if (String.IsNullOrEmpty(itemList[0].Error))
                                            {
                                                string transactionItems = "<table style='width: 100%;'>";
                                                foreach (HOTBAL.TransactionItem i in itemList)
                                                {
                                                    transactionItems += "<tr><td>" + i.ProductName + " (" + String.Format("{0:C}", i.Price) + ")";
                                                    transactionItems += "</td><td>" + i.Quantity + "</td></tr>";
                                                }
                                                transactionItems += "</table>";
                                                transactionLog.Text += transactionItems;
                                            }
                                        }

                                        transactionLog.Text += "</td><td>" + t.Location;
                                        transactionLog.Text += "</td><td>" + t.Payment;
                                        transactionLog.Text += "</td><td>" + String.Format("{0:C}", t.Total);
                                        transactionLog.Text += "</td><td>" + t.Paid.ToString();
                                        transactionLog.Text += "</td></tr>";

                                        transactionCount++;
                                    }
                                }
                            }
                            else
                            {
                                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                                errorLabel.Text = allCustomerTransactions[0].Error;
                            }
                        }
                        else
                        {
                            transactionLog.Text += "<tr><td colspan='7'>" + HOTBAL.TansMessages.ERROR_NO_CUSTOMER_TRNS + "</td>";
                        }
                    }
                    else
                    {
                        Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                        errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                    }
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = customerInfo.Error;
                }
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }
    }
}