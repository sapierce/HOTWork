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
    public partial class TransactionReceipt : System.Web.UI.Page
    {
        SDAFunctionsClass FunctionsClass = new SDAFunctionsClass();
        SDAMethods sqlClass = new SDAMethods();
        CultureInfo ci = new CultureInfo("en-us");

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.Header.Title = "HOT Self Defense - Transaction Receipt";

            if (Page.IsPostBack)
            {
                string strBody = "<table><tr><td colspan='3'>" + lblAddress.Text + "<br /></td></tr><tr><td valign='top' colspan='3'>"
                + lblVoid.Text + "</td></tr><tr><td valign='top' align='right'>Sold to: </td><td valign='top' colspan='2'>"  
                + lblName.Text + "</td></tr><tr><td><b>Item</b></td><td><b>Quantity</b></td><td><b>Unit Price</b></td> </tr>"
                + litItems.Text + "<tr><td valign='top' align='right' colspan='2'>SubTotal:</td><td valign='top'>"
                + lblSubTotal.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Tax:</td><td valign='top'>"
                + lblTax.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Total:</td><td valign='top'>"
                + lblTotal.Text + "</td></tr><tr><td valign='top' align='right' colspan='2'>Payment by:</td><td valign='top'>" 
                + lblPayment.Text + "</td></tr></table>";
                FunctionsClass.SendMail(txtEmail.Text, "receipts@hottropicaltans.net", "Your Receipt From HOT Self Defense", strBody);
                lblError.Text = "Email has been sent to " + txtEmail.Text;
            }
            else
            {
                lblAddress.Text = "<h3>H.O.T. Self Defense Academy</h3><br>710 N. Valley Mills Dr.<br>Waco, Texas 76710<br>254-399-9944<br>";

                Transaction studentTransaction = sqlClass.GetStudentTransaction(Convert.ToInt32(Request.QueryString["ID"].ToString()));
                List<TransactionItem> transactionItems = sqlClass.GetTransactionItems(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                if (String.IsNullOrEmpty(studentTransaction.Error) && String.IsNullOrEmpty(transactionItems[0].Error))
                {
                    Student studentInfo = sqlClass.GetStudentInformation(studentTransaction.CustomerID);

                    if (String.IsNullOrEmpty(studentInfo.Error))
                    {
                        lblName.Text = studentInfo.FirstName + " " + studentInfo.LastName;
                    }

                    if (studentTransaction.Void)
                    {
                        lblVoid.Text = "<b>**VOID**</b>";
                    }

                    Double taxTotal = 0, nonTaxTotal = 0;
                    foreach (TransactionItem item in transactionItems)
                    {
                        litItems.Text += "<tr><td>" + item.ProductName
                            + "</td><td>" + item.Quantity
                            + "</td><td>" + item.Price
                            + "</td><td>" + (item.Price * item.Quantity)
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
                    lblSubTotal.Text = (nonTaxTotal + taxTotal).ToString();
                    lblTax.Text = studentTransaction.Tax.ToString();
                    lblTotal.Text = studentTransaction.Total.ToString();
                    lblPayment.Text = studentTransaction.Payment;
                }
            }
        }
    }
}