using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentTransactionsPage : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Student Transactions";
            List<HOTBAL.Transaction> transactions = new List<HOTBAL.Transaction>();
            transactions = sqlClass.GetAllStudentTransactions(Convert.ToInt32(Request.QueryString["ID"].ToString()));
            HOTBAL.Student studentInfo = sqlClass.GetStudentInformation(Convert.ToInt32(Request.QueryString["ID"].ToString()));

            if (String.IsNullOrEmpty(studentInfo.Error))
            {
                lblCustName.Text = studentInfo.FirstName + " " + studentInfo.LastName;
            }
            else
            {
                lblError.Text = studentInfo.Error;
            }
            foreach (HOTBAL.Transaction trans in transactions)
            {
                litTransactions.Text += "<tr><td valign='top' class='reg'>"
                    + (trans.Void ? "<b>**VOID**</b><br />" : "")
                    + "<art href='/SDAPOS/TransactionDetails.aspx?ID=" + trans.ID + "&Date=" + Request.QueryString["Date"].ToString() + "'>" + trans.ID + "</art><br />"
                    + "<art href='/SDAPOS/TransactionReceipt.aspx?ID=" + trans.ID + "&Date=" + Request.QueryString["Date"].ToString() + "'>Receipt</art>"
                    + "</td><td valign='top' class='reg'>" + FunctionsClass.FormatSlash(trans.Date) + "</td><td valign='top' class='reg'><table>";

                //Get transaction items
                List<HOTBAL.TransactionItem> transactionItems = sqlClass.GetTransactionItems(trans.ID);

                foreach (HOTBAL.TransactionItem item in transactionItems)
                {
                    litTransactions.Text += "<tr><td>" + item.ProductName
                                    + "&nbsp;&nbsp;(" + item.Price + ")"
                                    + "</td><td>" + item.Quantity
                                    + "</td></tr>";
                }

                litTransactions.Text += "</table></td><td valign='top' class='reg'>" + trans.Payment
                    + "</td><td valign='top' class='reg'>" + trans.Total
                    + "</td><td valign='top' class='reg'>" + (trans.Paid ? "Yes" : "No")
                    + "</td></tr>";
            }
        }
    }
}