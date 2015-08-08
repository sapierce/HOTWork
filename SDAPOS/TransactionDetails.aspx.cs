using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HOTBAL;
using System.Web.UI.WebControls;
using System.Globalization;

namespace SDAPOS
{
    public partial class TransactionDetails : System.Web.UI.Page
    {
        SDAFunctionsClass FunctionsClass = new SDAFunctionsClass();
        SDAMethods sqlClass = new SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Transaction Details";

            if (!Page.IsPostBack)
            {
                HOTBAL.Transaction transactionDetails = sqlClass.GetStudentTransaction(Convert.ToInt32(Request.QueryString["ID"].ToString()));
                sltTrnsPymt.Items.Add(new ListItem("None", ""));
                sltTrnsPymt.Items.Add(new ListItem("Cash", "Cash"));
                sltTrnsPymt.Items.Add(new ListItem("CreditCard", "CC"));
                sltTrnsPymt.Items.Add(new ListItem("Check", "Check"));
                sltTrnsPymt.Items.Add(new ListItem("Trade", "Trade"));
                sltTrnsPymt.Items.Add(new ListItem("GiftCard", "GC"));
                sltTrnsPymt.Items.Add(new ListItem("Online", "ONLINE"));
                sltTrnsPymt.Items.Add(new ListItem("PayPal", "PAYPAL"));
                sltTrnsPymt.Items.Add(new ListItem("Other", "Other"));

                if (String.IsNullOrEmpty(transactionDetails.ErrorMessage))
                {
                    List<HOTBAL.TransactionItem> transactionItems = sqlClass.GetTransactionItems(transactionDetails.TransactionId);
                    HOTBAL.Student studentDetails = sqlClass.GetStudentInformation(transactionDetails.CustomerId);

                    lblTrnsID.Text = transactionDetails.TransactionId.ToString();
                    if (String.IsNullOrEmpty(studentDetails.ErrorMessage))
                    {
                        lblTrnsBuyer.Text = studentDetails.FirstName + " " + studentDetails.LastName;
                    }
                    else
                    {
                        lblTrnsBuyer.Text = transactionDetails.CustomerId.ToString();
                    }
                    lblStudentID.Text = transactionDetails.CustomerId.ToString();
                    txtTrnsSeller.Text = transactionDetails.SellerId;
                    txtTrnsTtl.Text = transactionDetails.TransactionTotal.ToString();
                    txtTrnsDate.Text = FunctionsClass.FormatSlash(transactionDetails.TransactionDate);
                    chkTrnsPaid.Checked = transactionDetails.IsTransactionPaid;
                    chkTrnsVoid.Checked = transactionDetails.IsTransactionVoid;
                    sltTrnsPymt.Items.FindByValue(transactionDetails.PaymentMethod).Selected = true;

                    if (transactionItems != null)
                    {
                        if (String.IsNullOrEmpty(transactionItems[0].ErrorMessage))
                        {
                            lblItems.Text = "<table>";
                            foreach (TransactionItem i in transactionItems)
                            {
                                lblItems.Text += "<tr><td>" + i.ProductName + " @ " + i.ProductPrice + " each"
                                    + "</td><td>" + i.ItemQuantity + "</td><td><a href='TransactionItemDelete.aspx?ID="
                                    + i.TransactionItemId + "&TID=" + i.TransactionId + "&Date=" + DateTime.Now.ToShortDateString()
                                    + "'>Delete</a></td></tr>";
                            }
                            lblItems.Text += "</table>";
                        }
                        else
                        {
                            lblError.Text = transactionItems[0].ErrorMessage;
                        }
                    }
                    else
                    {
                        lblError.Text = transactionDetails.ErrorMessage;
                    }
                }
            }
        }

        public void onclick_btnTrnsEdit(Object sender, EventArgs e)
        {
            bool response = false;
            response = sqlClass.UpdateTransaction(Convert.ToInt32(Request.QueryString["ID"].ToString()), txtTrnsSeller.Text, txtTrnsTtl.Text, 
                Convert.ToDateTime(txtTrnsDate.Text), sltTrnsPymt.SelectedValue, 
                (chkTrnsVoid.Checked), (chkTrnsPaid.Checked));

            if (response)
            {
                lblError.Text = SDAMessages.ERROR_GENERIC;
            }
            else
            {
                Response.Redirect("../Schedule/StudentTransactions.aspx?ID=" + lblStudentID.Text + "&Date=" + FunctionsClass.FormatDash(DateTime.Now));
            }
        }

        public void onclick_btnTrnsReceipt(Object sender, EventArgs e)
        {
            Response.Redirect("TransactionReceipt.aspx?ID=" + Request.QueryString["ID"].ToString() + "&Date=" + Request.QueryString["Date"].ToString());
        }
    }
}