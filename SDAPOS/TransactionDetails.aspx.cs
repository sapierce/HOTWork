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

                if (String.IsNullOrEmpty(transactionDetails.Error))
                {
                    List<HOTBAL.TransactionItem> transactionItems = sqlClass.GetTransactionItems(transactionDetails.ID);
                    HOTBAL.Student studentDetails = sqlClass.GetStudentInformation(transactionDetails.CustomerID);

                    lblTrnsID.Text = transactionDetails.ID.ToString();
                    if (String.IsNullOrEmpty(studentDetails.Error))
                    {
                        lblTrnsBuyer.Text = studentDetails.FirstName + " " + studentDetails.LastName;
                    }
                    else
                    {
                        lblTrnsBuyer.Text = transactionDetails.CustomerID.ToString();
                    }
                    lblStudentID.Text = transactionDetails.CustomerID.ToString();
                    txtTrnsSeller.Text = transactionDetails.Seller;
                    txtTrnsTtl.Text = transactionDetails.Total.ToString();
                    txtTrnsDate.Text = FunctionsClass.FormatSlash(transactionDetails.Date);
                    chkTrnsPaid.Checked = transactionDetails.Paid;
                    chkTrnsVoid.Checked = transactionDetails.Void;
                    sltTrnsPymt.Items.FindByValue(transactionDetails.Payment).Selected = true;

                    if (transactionItems != null)
                    {
                        if (String.IsNullOrEmpty(transactionItems[0].Error))
                        {
                            lblItems.Text = "<table>";
                            foreach (TransactionItem i in transactionItems)
                            {
                                lblItems.Text += "<tr><td>" + i.ProductName + " @ " + i.Price + " each"
                                    + "</td><td>" + i.Quantity + "</td><td><a href='TransactionItemDelete.aspx?ID="
                                    + i.ID + "&TID=" + i.TransactionID + "&Date=" + DateTime.Now.ToShortDateString()
                                    + "'>Delete</a></td></tr>";
                            }
                            lblItems.Text += "</table>";
                        }
                        else
                        {
                            lblError.Text = transactionItems[0].Error;
                        }
                    }
                    else
                    {
                        lblError.Text = transactionDetails.Error;
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