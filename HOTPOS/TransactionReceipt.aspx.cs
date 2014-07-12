using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace HOTPOS
{
    public partial class TransactionReceipt : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass TansFunctionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods TansSqlClass = new HOTBAL.TansMethods();
        CultureInfo ci = new CultureInfo("en-us");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                string strBody = "<table><tr><td colspan='4'>" + address.Text + "<br /></td></tr><tr><td valign='top' colspan='4'>"
                + voidIndicator.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Sold to: </td><td valign='top' colspan='2'>"
                + customerName.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Date: </td><td valign='top' colspan='2'>"
                + transactionDate.Text + "</td></tr><tr><td><b>Item</b></td><td><b>Quantity</b></td><td><b>Unit Price</b></td><td><b>Total Price</b></td></tr>"
                + itemsList.Text + "<tr><td valign='top' align='right' colspan='2'>SubTotal:</td><td valign='top'>"
                + subTotal.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Tax:</td><td valign='top' colspan='2'>"
                + tax.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Total:</td><td valign='top' colspan='2'>"
                + total.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Payment by:</td><td valign='top' colspan='2'>" 
                + paymentMethod.Text + "</td></tr></table>";

                TansFunctionsClass.SendMail(emailAddress.Text, "receipts@hottropicaltans.net", "Your Receipt From HOT Tropical Tans", strBody);
                errorMessage.Text = "Email has been sent to " + emailAddress.Text;
            }
            else
            {
                if (Request.QueryString["ID"] != null)
                {
                    int transactionNumber = Convert.ToInt32(Request.QueryString["ID"].ToString());

                    address.Text = "<h3>HOT Tropical Tans</h3><br>710 N. Valley Mills Dr.<br>Waco, Texas 76710<br>254-399-9944<br>www.hottropicaltans.com<br>";
                        HOTBAL.Transaction customerTransaction = TansSqlClass.GetCustomerTransaction(transactionNumber);
                        List<HOTBAL.TransactionItem> transactionItems = TansSqlClass.GetCustomerTransactionItems(transactionNumber);

                        if (String.IsNullOrEmpty(customerTransaction.Error) && String.IsNullOrEmpty(transactionItems[0].Error))
                        {
                            HOTBAL.Customer customerInfo = new HOTBAL.Customer();
                            if (customerTransaction.CustomerID != 0)
                            {
                                customerInfo = TansSqlClass.GetCustomerInformationByID(customerTransaction.CustomerID);
                            }
                            else
                            {
                                customerInfo = TansSqlClass.GetCustomerNameByTransactionID(transactionNumber);
                            }

                            if (String.IsNullOrEmpty(customerInfo.Error))
                            {
                                customerName.Text = customerInfo.FirstName + " " + customerInfo.LastName;
                            }

                            if (customerTransaction.Void)
                            {
                                voidIndicator.Text = "<b>**VOID**</b>";
                            }

                            Double taxTotal = 0, nonTaxTotal = 0;
                            foreach (HOTBAL.TransactionItem item in transactionItems)
                            {
                                itemsList.Text += "<tr><td>" + item.ProductName
                                    + "</td><td style='text-align: center;'>" + item.Quantity
                                    + "</td><td style='text-align: center;'>" + String.Format("{0:c}", item.Price)
                                    + "</td><td style='text-align: center;'>" + String.Format("{0:c}", (item.Price * item.Quantity))
                                    + "</td></tr>";
                                if (item.Tax)
                                {
                                    taxTotal = (taxTotal + (item.Price * item.Quantity));
                                }
                                else
                                {
                                    nonTaxTotal = (nonTaxTotal + (item.Price * item.Quantity));
                                }
                            }
                            subTotal.Text = String.Format("{0:c}", (nonTaxTotal + taxTotal));
                            tax.Text = String.Format("{0:c}", customerTransaction.Tax);
                            total.Text = String.Format("{0:c}", customerTransaction.Total);
                            paymentMethod.Text = customerTransaction.Payment;
                            transactionDate.Text = TansFunctionsClass.FormatSlash(customerTransaction.Date);
                        }
                }
                else
                {
                    Response.Redirect("/HOTPOS/");
                }
            }
        }
    }
}