﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SDAPOS
{
    public partial class TransactionLog : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass sdaFunctionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods sdaSqlClass = new HOTBAL.SDAMethods();
        private HOTBAL.TansFunctionsClass tansFunctionsClass = new HOTBAL.TansFunctionsClass();
        private HOTBAL.TansMethods tansSqlClass = new HOTBAL.TansMethods();
        private double tax = 0.00, total = 0.00, tanningTaxed = 0.00, tanningNonTaxed = 0.00, martialTaxed = 0.00, martialNonTaxed = 0.00, taxed = 0.00, nonTaxed = 0.00;
        private double cash = 0.00, credit = 0.00, check = 0.00, trade = 0.00, giftCard = 0.00, online = 0.00, payPal = 0.00, other = 0.00;
        private double tanningCash = 0.00, tanningCredit = 0.00, tanningCheck = 0.00, tanningTrade = 0.00, tanningGiftCard = 0.00;
        private double tanningOnline = 0.00, tanningPayPal = 0.00, tanningOther = 0.00, tanningTax = 0.00, tanningTotal = 0.00;
        private double martialCash = 0.00, martialCredit = 0.00, martialCheck = 0.00, martialTrade = 0.00, martialGiftCard = 0.00;
        private double martialOnline = 0.00, martialPayPal = 0.00, martialOther = 0.00, martialTax = 0.00, martialTotal = 0.00;
        private bool showBreakdown = false;
        private bool totalsOnly = false;
        private int employeeId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(Request.QueryString["StartDate"].ToString());
            DateTime endDate = Convert.ToDateTime(Request.QueryString["EndDate"].ToString());
            totalsOnly = Convert.ToBoolean(Request.QueryString["Totals"]);

            if (Request.QueryString["ID"] != null)
                employeeId = Convert.ToInt32(Request.QueryString["ID"].ToString());

            if (employeeId > 0)
            {
                // Employee transactions
                logDate.Text = Request.QueryString["StartDate"];
                headerTitle.Text = "Employee Transaction Log for " + employeeId.ToString() + " on " + startDate.ToShortDateString();
            }
            else
            {
                if (startDate == endDate)
                {
                    // One day transactions
                    logDate.Text = Request.QueryString["StartDate"];
                    headerTitle.Text = "Full Transaction Log for " + startDate.ToShortDateString();
                }
                else
                {
                    // Multi day transactions
                    logDate.Text = startDate + "-" + endDate;
                    headerTitle.Text = "Full Transaction Log for " + startDate.ToShortDateString() + " through " + endDate.ToShortDateString();
                }
            }

            if (Request.QueryString["B"] != null)
                showBreakdown = true;

            for (DateTime newDate = startDate; newDate <= endDate; newDate = newDate.AddDays(1))
            {
                TanningSales(tansFunctionsClass.FormatDash(newDate), "W", employeeId, showBreakdown);
                MartialArtsSales(sdaFunctionsClass.FormatDash(newDate), employeeId, showBreakdown);

                TanningTotals(newDate.ToShortDateString());
                MartialTotals(newDate.ToShortDateString());
                Totals(newDate.ToShortDateString());
                ResetTotals();
            }
        }

        protected void TanningSales(string transactionDate, string trnsStore, int employeeID, bool displayBreakdown)
        {
            try
            {
                List<HOTBAL.Transaction> tanTransactions = new List<HOTBAL.Transaction>();

                if (employeeID > 0)
                    tanTransactions = tansSqlClass.EmployeeTanningTransactions(employeeID, transactionDate, transactionDate);
                else
                    tanTransactions = tansSqlClass.GetAllTanningTransactions(transactionDate, trnsStore);

                if (tanTransactions != null)
                {
                    if (tanTransactions.Count > 0)
                    {
                        foreach (HOTBAL.Transaction transaction in tanTransactions)
                        {
                            if (transaction.ID != 0)
                            {
                                if (!totalsOnly)
                                    tanningSales.Text += buildTransactionLine("T", transaction);

                                calculateTotals("T", transaction);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tansSqlClass.LogErrorMessage(ex, transactionDate, "Reports: TanningsSales");
                errorMessage.Text = "Unable to load Tanning log";
            }
        }

        protected void MartialArtsSales(string transactionDate, int employeeID, bool displayBreakdown)
        {
            try
            {
                List<HOTBAL.Transaction> maTransactions = new List<HOTBAL.Transaction>();

                if (employeeID > 0)
                    maTransactions = sdaSqlClass.GetEmployeeMartialArtTransactions(transactionDate, employeeID);
                else
                    maTransactions = sdaSqlClass.GetAllMartialArtTransactions(transactionDate);

                foreach (HOTBAL.Transaction transaction in maTransactions)
                {
                    if (transaction.ID != 0)
                    {
                        if (!totalsOnly)
                            martialArtSales.Text += buildTransactionLine("M", transaction);

                        calculateTotals("M", transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                sdaFunctionsClass.SendErrorMail("Reports: MartialArtsSales", ex, transactionDate);
                errorMessage.Text = "Unable to load martial arts log";
            }
        }

        protected void TanningTotals(string transactionDate)
        {
            tanningTotal = tanningCash + tanningCredit + tanningCheck + tanningOnline + tanningPayPal + tanningOther;
            tanningTotals.Text += "<tr><td colspan='4'><h4>Tanning Totals for " + transactionDate + "</h4></td></tr>";
            tanningTotals.Text += "<tr><td align='right'><b>Cash Total: </b> </td><td>" + String.Format("{0:C}", tanningCash) + "</td>";
            tanningTotals.Text += "<td align='right'><b>Credit Total: </b> </td><td>" + String.Format("{0:C}", tanningCredit) + "</td></tr>";
            tanningTotals.Text += "<tr><td align='right'><b>Check Total: </b> </td><td>" + String.Format("{0:C}", tanningCheck) + "</td>";
            tanningTotals.Text += "<td align='right'><b>Trade Total: </b> </td><td>" + String.Format("{0:C}", tanningTrade) + "</td></tr>";
            tanningTotals.Text += "<tr><td align='right'><b>Online Total: </b> </td><td>" + String.Format("{0:C}", tanningOnline) + "</td>";
            tanningTotals.Text += "<td align='right'><b>PayPal Total: </b> </td><td>" + String.Format("{0:C}", tanningPayPal) + "</td></tr>";
            tanningTotals.Text += "<tr><td align='right'><b>Gift Card Total: </b> </td><td>" + String.Format("{0:C}", tanningGiftCard) + "</td>";
            tanningTotals.Text += "<td align='right'><b>Other Total: </b> </td><td>" + String.Format("{0:C}", tanningOther) + "</td></tr>";
            tanningTotals.Text += "<tr><td align='right'><b>Total Taxed: </b> </td><td>" + String.Format("{0:C}", tanningTaxed) + "</td>";
            tanningTotals.Text += "<td align='right'><b>Total NonTaxed: </b> </td><td>" + String.Format("{0:C}", tanningNonTaxed) + "</td></tr>";
            tanningTotals.Text += "<tr><td align='right'><b>Complete Total: </b></td><td>" + String.Format("{0:C}", tanningTotal) + "</td>";
            tanningTotals.Text += "<td align='right'><b>Total Tax: </b></td><td>" + String.Format("{0:C}", tanningTax) + "</td></tr>";
        }

        protected void MartialTotals(string transactionDate)
        {
            martialTotal = martialCash + martialCredit + martialCheck + martialOnline + martialPayPal + martialOther;
            martialArtTotals.Text += "<tr><td colspan='4'><h4>Martial Arts Totals for " + transactionDate + "</h4></td></tr>";
            martialArtTotals.Text += "<tr><td align='right'><b>Cash Total: </b> </td><td>" + String.Format("{0:C}", martialCash) + "</td>";
            martialArtTotals.Text += "<td align='right'><b>Credit Total: </b> </td><td>" + String.Format("{0:C}", martialCredit) + "</td></tr>";
            martialArtTotals.Text += "<tr><td align='right'><b>Check Total: </b> </td><td>" + String.Format("{0:C}", martialCheck) + "</td>";
            martialArtTotals.Text += "<td align='right'><b>Trade Total: </b> </td><td>" + String.Format("{0:C}", martialTrade) + "</td></tr>";
            martialArtTotals.Text += "<tr><td align='right'><b>Online Total: </b> </td><td>" + String.Format("{0:C}", martialOnline) + "</td>";
            martialArtTotals.Text += "<td align='right'><b>PayPal Total: </b> </td><td>" + String.Format("{0:C}", martialPayPal) + "</td></tr>";
            martialArtTotals.Text += "<tr><td align='right'><b>Gift Card Total: </b> </td><td>" + String.Format("{0:C}", martialGiftCard) + "</td>";
            martialArtTotals.Text += "<td align='right'><b>Other Total: </b> </td><td>" + String.Format("{0:C}", martialOther) + "</td></tr>";
            martialArtTotals.Text += "<tr><td align='right'><b>Total Taxed: </b> </td><td>" + String.Format("{0:C}", martialTaxed) + "</td>";
            martialArtTotals.Text += "<td align='right'><b>Total NonTaxed: </b> </td><td>" + String.Format("{0:C}", martialNonTaxed) + "</td></tr>";
            martialArtTotals.Text += "<tr><td align='right'><b>Complete Total: </b></td><td>" + String.Format("{0:C}", martialTotal) + "</td>";
            martialArtTotals.Text += "<td align='right'><b>Total Tax: </b></td><td>" + String.Format("{0:C}", martialTax) + "</td></tr>";
        }

        protected void Totals(string transactionDate)
        {
            total = cash + credit + check + online + payPal + other;
            completeTotals.Text += "<tr><td colspan='4'><h4>Complete Totals for " + transactionDate + "</h4></td></tr>";
            completeTotals.Text += "<tr><td align='right'><b>Cash Total: </b> </td><td>" + String.Format("{0:C}", cash) + "</td>";
            completeTotals.Text += "<td align='right'><b>Credit Total: </b> </td><td>" + String.Format("{0:C}", credit) + "</td></tr>";
            completeTotals.Text += "<tr><td align='right'><b>Check Total: </b> </td><td>" + String.Format("{0:C}", check) + "</td>";
            completeTotals.Text += "<td align='right'><b>Trade Total: </b> </td><td>" + String.Format("{0:C}", trade) + "</td></tr>";
            completeTotals.Text += "<tr><td align='right'><b>Online Total: </b> </td><td>" + String.Format("{0:C}", online) + "</td>";
            completeTotals.Text += "<td align='right'><b>PayPal Total: </b> </td><td>" + String.Format("{0:C}", payPal) + "</td></tr>";
            completeTotals.Text += "<tr><td align='right'><b>Gift Card Total: </b> </td><td>" + String.Format("{0:C}", giftCard) + "</td>";
            completeTotals.Text += "<td align='right'><b>Other Total: </b> </td><td>" + String.Format("{0:C}", other) + "</td></tr>";
            completeTotals.Text += "<tr><td align='right'><b>Total Taxed: </b> </td><td>" + String.Format("{0:C}", taxed) + "</td>";
            completeTotals.Text += "<td align='right'><b>Total NonTaxed: </b> </td><td>" + String.Format("{0:C}", nonTaxed) + "</td></tr>";
            completeTotals.Text += "<tr><td align='right'><b>Complete Total: </b></td><td>" + String.Format("{0:C}", total) + "</td>";
            completeTotals.Text += "<td align='right'><b>Total Tax: </b></td><td>" + String.Format("{0:C}", tax) + "</td></tr>";
        }

        private string buildTransactionLine(string transactionType, HOTBAL.Transaction transaction)
        {
            string customerName = "Unknown";
            string voidLine = (transaction.Void ? "<b>**VOID**</b><br />" : "");
            string detailUrls = buildTransactionDetailUrls(transactionType, transaction);
            string transactionLine = "<tr><td valign='top'>" + voidLine + detailUrls;
            double transactionTotal = 0.00, transactionTax = 0.00;

            if (transactionType == "M")
            {
                HOTBAL.Student student = sdaSqlClass.GetStudentInformation(transaction.CustomerID);
                if (String.IsNullOrEmpty(student.Error))
                {
                    customerName = student.FirstName + " " + student.LastName;
                }
            }
            else
            {
                HOTBAL.Customer customer = tansSqlClass.GetCustomerInformationByID(transaction.CustomerID);
                if (String.IsNullOrEmpty(customer.Error))
                {
                    customerName = customer.FirstName + " " + customer.LastName;

                    if (!customer.IsActive)
                        customerName += " - INACTIVE";
                }
            }

            transactionLine += "</td><td valign='top'>" + tansFunctionsClass.FormatSlash(transaction.Date) + "</td>";

            transactionLine += "<td valign='top'>" + customerName + "</td><td valign='top'>";

            //Get transaction items
            List<HOTBAL.TransactionItem> transactionItems;
            if (transactionType == "M")
            {
                transactionItems = sdaSqlClass.GetTransactionItems(transaction.ID);
            }
            else
            {
                transactionItems = tansSqlClass.GetTanningTransactionItems(transaction.ID);
            }
            transactionLine += "<table width='100%' style='border: 0px'><tr style='border: 0px'>"
                + "<td style='border: 0px; width: 50%'><strong>Item</strong></td><td style='border: 0px; width: 25%'><strong>Quantity</strong><td style='border: 0px; width: 25%'><strong>Tax</strong></td></tr>";

            foreach (HOTBAL.TransactionItem item in transactionItems)
            {
                transactionLine += "<tr style='border: 0px'><td style='border: 0px'>" + item.ProductName
                                + "&nbsp;&nbsp;(" + String.Format("{0:C}", item.Price) + ")"
                                + "</td><td style='border: 0px'>" + item.Quantity
                                + "</td><td style='border: 0px'>" + (item.Tax ? "Y" : "N")
                                + "</td></tr>";
                transactionTotal = transactionTotal + (item.Price * item.Quantity);
                if (item.Tax)
                    transactionTax = transactionTax + ((item.Price * item.Quantity) * .0825);
            }
            transactionLine += "</table></td><td valign='top'>" + transaction.Seller;
            transactionLine += "</td><td valign='top'>" + String.Format("{0:C}", transaction.Payment);
            transactionLine += "</td><td valign='top'>" + String.Format("{0:C}", transactionTotal);
            transactionLine += "</td><td valign='top'>" + String.Format("{0:C}", transactionTax);
            transactionLine += "</td><td valign='top'>" + String.Format("{0:C}", transaction.Total);
            transactionLine += "</td></tr>";

            return transactionLine;
        }

        private string buildTransactionDetailUrls(string transactionType, HOTBAL.Transaction transaction)
        {
            string detailUrl = "";

            if (transactionType == "M")
            {
                detailUrl = "<a href='" + HOTBAL.SDAPOSConstants.TRANSACTION_DETAILS_URL + "?ID=" + transaction.ID + "'>" + transaction.ID + "</a><br />";
                detailUrl += "<a href='" + HOTBAL.SDAPOSConstants.TRANSACTION_VOID_URL + "?ID=" + transaction.ID + (transaction.Void ? "&Task=Unvoid'>Unvoid</a><br>" : "&Task=Void'>Void</a><br />");
            }
            else
            {
                detailUrl = "<a href='" + HOTBAL.POSConstants.TRANSACTION_DETAILS_URL + "?ID=" + transaction.ID + "'>" + transaction.ID + "</a><br />";
                detailUrl += "<a href='" + HOTBAL.POSConstants.TRANSACTION_VOID_URL + "?ID=" + transaction.ID + (transaction.Void ? "&Task=Unvoid'>Unvoid</a><br>" : "&Task=Void'>Void</a><br />");
            }

            if (transactionType == "M")
            {
                detailUrl += "<a href='" + HOTBAL.POSConstants.RECEIPT_URL + "?ID=" + transaction.ID + "'>Receipt</a><br />";
            }
            else
            {
                detailUrl += "<a href='" + HOTBAL.SDAPOSConstants.RECEIPT_URL + "?ID=" + transaction.ID + "'>Receipt</a><br />";
            }

            return detailUrl;
        }

        private void calculateTotals(string transactionType, HOTBAL.Transaction transaction)
        {
            bool isMartialArts = (transactionType == "M" ? true : false);

            tax = tax + transaction.Tax;

            if (isMartialArts)
                martialTax = martialTax + transaction.Tax;
            else
                tanningTax = tanningTax + transaction.Tax;

            if (!transaction.Void)
            {
                if (transaction.Tax > 0)
                {
                    taxed = taxed + (transaction.Total - transaction.Tax);
                    if (isMartialArts)
                        martialTaxed = martialTaxed + (transaction.Total - transaction.Tax);
                    else
                        tanningTaxed = tanningTaxed + (transaction.Total - transaction.Tax);
                }
                else
                {
                    nonTaxed = nonTaxed + transaction.Total;
                    if (isMartialArts)
                        martialNonTaxed = martialNonTaxed + transaction.Total;
                    else
                        tanningNonTaxed = tanningNonTaxed + transaction.Total;
                }

                switch (transaction.Payment)
                {
                    case "Cash":
                        cash = cash + transaction.Total;
                        if (isMartialArts)
                            martialCash = martialCash + transaction.Total;
                        else
                            tanningCash = tanningCash + transaction.Total;
                        break;

                    case "CC":
                        credit = credit + transaction.Total;
                        if (isMartialArts)
                            martialCredit = martialCredit + transaction.Total;
                        else
                            tanningCredit = tanningCredit + transaction.Total;
                        break;

                    case "Check":
                        check = check + transaction.Total;
                        if (isMartialArts)
                            martialCheck = martialCheck + transaction.Total;
                        else
                            tanningCheck = tanningCheck + transaction.Total;
                        break;

                    case "Trade":
                        trade = trade + transaction.Total;
                        if (isMartialArts)
                            martialTrade = martialTrade + transaction.Total;
                        else
                            tanningTrade = tanningTrade + transaction.Total;
                        break;

                    case "GC":
                        giftCard = giftCard + transaction.Total;
                        if (isMartialArts)
                            martialGiftCard = martialGiftCard + transaction.Total;
                        else
                            tanningGiftCard = tanningGiftCard + transaction.Total;
                        break;

                    case "ONLINE":
                        online = online + transaction.Total;
                        if (isMartialArts)
                            martialOnline = martialOnline + transaction.Total;
                        else
                            tanningOnline = tanningOnline + transaction.Total;
                        break;

                    case "PAYPAL":
                        payPal = payPal + transaction.Total;
                        if (isMartialArts)
                            martialPayPal = martialPayPal + transaction.Total;
                        else
                            tanningPayPal = tanningPayPal + transaction.Total;
                        break;

                    default:
                        other = other + transaction.Total;
                        if (isMartialArts)
                            martialOther = martialOther + transaction.Total;
                        else
                            tanningOther = tanningOther + transaction.Total;
                        break;
                }
            }
        }

        private void ResetTotals()
        {
            tax = 0.00;
            total = 0.00;
            tanningTaxed = 0.00;
            tanningNonTaxed = 0.00;
            martialTaxed = 0.00;
            martialNonTaxed = 0.00;
            taxed = 0.00;
            nonTaxed = 0.00;
            cash = 0.00;
            credit = 0.00;
            check = 0.00;
            trade = 0.00;
            giftCard = 0.00;
            online = 0.00;
            payPal = 0.00;
            other = 0.00;
            tanningCash = 0.00;
            tanningCredit = 0.00;
            tanningCheck = 0.00;
            tanningTrade = 0.00;
            tanningGiftCard = 0.00;
            tanningOnline = 0.00;
            tanningPayPal = 0.00;
            tanningOther = 0.00;
            tanningTax = 0.00;
            tanningTotal = 0.00;
            martialCash = 0.00;
            martialCredit = 0.00;
            martialCheck = 0.00;
            martialTrade = 0.00;
            martialGiftCard = 0.00;
            martialOnline = 0.00;
            martialPayPal = 0.00;
            martialOther = 0.00;
            martialTax = 0.00;
            martialTotal = 0.00;
        }
    }
}