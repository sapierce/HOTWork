﻿using System;
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
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Student Transactions";

            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            if (Request.QueryString["ID"] != null)
                if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    int studentId = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    buildStudentInformation(studentId);
                    buildStudentTransactions(studentId);
                }
                else
                    errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
            else
                errorLabel.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
        }

        private void buildStudentInformation(int studentId)
        {
            // Set up the error label
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");

            HOTBAL.Student studentInfo = sqlClass.GetStudentInformation(studentId);

            if (String.IsNullOrEmpty(studentInfo.Error))
            {
                studentName.Text = studentInfo.FirstName + " " + studentInfo.LastName + (String.IsNullOrEmpty(studentInfo.Suffix) ? "" : ", " + studentInfo.Suffix);
                addTransaction.NavigateUrl = HOTBAL.SDAPOSConstants.CART_URL + "?ID=" + studentId.ToString() + "&Action=";
                studentInformation.NavigateUrl = HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + studentId.ToString();
            }
            else
            {
                errorLabel.Text = studentInfo.Error;
            }
        }

        private void buildStudentTransactions(int studentId)
        {
            List<HOTBAL.Transaction> transactionList = sqlClass.GetAllStudentTransactions(studentId);

            if (transactionList.Count > 0)
            {
                foreach (HOTBAL.Transaction transaction in transactionList)
                {
                    transactionOutput.Text += "<tr><td valign='top'>"
                        + (transaction.Void ? "<b>**VOID**</b><br />" : "")
                        + "<a href='" + HOTBAL.SDAPOSConstants.MA_TRANSACTION_DETAILS_URL + "?ID=" + transaction.ID + "'>" + transaction.ID + "</a><br />"
                        + "<a href='" + HOTBAL.SDAPOSConstants.RECEIPT_URL + "?ID=" + transaction.ID + "'>Receipt</a>"
                        + "</td><td valign='top'>" + FunctionsClass.FormatSlash(transaction.Date) + "</td><td valign='top'><table>";

                    // Get transaction items
                    List<HOTBAL.TransactionItem> transactionItems = sqlClass.GetTransactionItems(transaction.ID);

                    foreach (HOTBAL.TransactionItem item in transactionItems)
                    {
                        transactionOutput.Text += "<tr><td>" + item.ProductName
                                        + "&nbsp;&nbsp;(" + item.Price + ")"
                                        + "</td><td>" + item.Quantity
                                        + "</td></tr>";
                    }

                    transactionOutput.Text += "</table></td><td valign='top'>" + transaction.Payment
                        + "</td><td valign='top'>" + transaction.Total
                        + "</td><td valign='top'>" + (transaction.Paid ? "Yes" : "No")
                        + "</td></tr>";
                }
            }
            else
                transactionOutput.Text += "<tr><td colspan='6'>" + HOTBAL.SDAMessages.NO_TRANSACTIONS + "</td></tr>";
        }
    }
}