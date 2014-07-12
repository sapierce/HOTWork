using System;
using System.Collections.Generic;

namespace HOTSelfDefense.Admin.Reports
{
    public partial class FullTransactionLog : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();
        private HOTBAL.TansMethods hottansSqlClass = new HOTBAL.TansMethods();
        private HOTBAL.Customer hottansCustomer = new HOTBAL.Customer();
        private double ttlTax, ttlTotal, ttlTanningTaxed, ttlTanningNonTaxed, ttlMartialTaxed, ttlMartialNonTaxed, ttlTaxed, ttlNonTaxed;
        private double ttlCash, ttlCredit, ttlCheck, ttlTrade, ttlGiftCard, ttlOnline, ttlPayPal, ttlOther;
        private double ttlTanningCash, ttlTanningCredit, ttlTanningCheck, ttlTanningTrade, ttlTanningGiftCard, ttlTanningOnline, ttlTanningPayPal, ttlTanningOther, ttlTanningTax, ttlTanningTotal;
        private double ttlMartialCash, ttlMartialCredit, ttlMartialCheck, ttlMartialTrade, ttlMartialGiftCard, ttlMartialOnline, ttlMartialPayPal, ttlMartialOther, ttlMartialTax, ttlMartialTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Date"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["Date"]))
                {
                    string transactionDate = Request.QueryString["Date"].ToString();
                    titleDate.Text = transactionDate;
                    headerText.Text = "Full Transaction Log for " + transactionDate;

                    TanningSales(transactionDate, "W");
                    MartialArtsSales(transactionDate);
                    TanningTotals();
                    MartialTotals();
                    Totals();
                }
            }
        }

        protected void TanningSales(string transactionDate, string transactionStore)
        {
            try
            {
                bool includeTransaction = true;
                List<HOTBAL.Transaction> tanTransactions = hottansSqlClass.GetAllTanningTransactions(transactionDate, transactionStore);

                foreach (HOTBAL.Transaction t in tanTransactions)
                {
                    litTanningSales.Text += "<tr><td valign='top'>";

                    if (t.Void)
                    {
                        litTanningSales.Text += "<b>**VOID**</b><br>";
                        includeTransaction = false;
                    }

                    litTanningSales.Text += "<a href='http://www.hottropicaltans.com/HOTPOS/trans_detail.aspx?ID=" + t.ID + "&Date=" + transactionDate + "'>" + t.ID + "</a><br>";
                    if (t.Void)
                    {
                        litTanningSales.Text += "<a href='http://www.hottropicaltans.com/HOTPOS/trans_unvoid.aspx?ID=" + t.ID + "&Date=" + transactionDate + "&type=M'>Unvoid</a><br>";
                    }
                    else
                    {
                        litTanningSales.Text += "<a href='http://www.hottropicaltans.com/HOTPOS/trans_void.aspx?ID=" + t.ID + "&Date=" + transactionDate + "&type=M'>Void</a><br>";
                    }

                    litTanningSales.Text += "<a href='http://www.hottropicaltans.com/HOTPOS/receipt.aspx?ID=" + t.ID + "&Date=" + transactionDate + "'>Receipt</a>";

                    hottansCustomer = hottansSqlClass.GetCustomerInformationByID(t.CustomerID);
                    litTanningSales.Text += "</td><td valign='top'>" + hottansCustomer.FirstName + " " + hottansCustomer.LastName + "</td><td><table>";
                    //Get transaction items
                    List<HOTBAL.TransactionItem> transactionItems = hottansSqlClass.GetTanningTransactionItems(t.ID);

                    foreach (HOTBAL.TransactionItem item in transactionItems)
                    {
                        litTanningSales.Text += "<tr><td>" + item.ProductName
                                        + "&nbsp;&nbsp;(" + String.Format("{0:$0.00}", item.Price) + ")"
                                        + "</td><td>" + item.Quantity
                                        + "</td></tr>";
                    }
                    litTanningSales.Text += "</table></td><td valign='top'>" + t.Location;
                    litTanningSales.Text += "</td><td valign='top'>" + t.Seller;
                    litTanningSales.Text += "</td><td valign='top'>" + String.Format("{0:$0.00}", t.Payment);
                    litTanningSales.Text += "</td><td valign='top'>" + String.Format("{0:$0.00}", t.Total);
                    litTanningSales.Text += "</td></tr>";

                    ttlTax = ttlTax + t.Tax;
                    ttlTanningTax = ttlTanningTax + t.Tax;
                    if (includeTransaction)
                    {
                        if (t.Tax > 0)
                        {
                            ttlTanningTaxed = ttlTanningTaxed + t.Total;
                            ttlTaxed = ttlTaxed + t.Total;
                        }
                        else
                        {
                            ttlTanningNonTaxed = ttlTanningNonTaxed + t.Total;
                            ttlNonTaxed = ttlNonTaxed + t.Total;
                        }

                        switch (t.Payment)
                        {
                            case "Cash":
                                ttlCash = ttlCash + t.Total;
                                ttlTanningCash = ttlTanningCash + t.Total;
                                break;

                            case "CC":
                                ttlCredit = ttlCredit + t.Total;
                                ttlTanningCredit = ttlTanningCredit + t.Total;
                                break;

                            case "Check":
                                ttlCheck = ttlCheck + t.Total;
                                ttlTanningCheck = ttlTanningCheck + t.Total;
                                break;

                            case "Trade":
                                ttlTrade = ttlTrade + t.Total;
                                ttlTanningTrade = ttlTanningTrade + t.Total;
                                break;

                            case "GC":
                                ttlGiftCard = ttlGiftCard + t.Total;
                                ttlTanningGiftCard = ttlTanningGiftCard + t.Total;
                                break;

                            case "ONLINE":
                                ttlOnline = ttlOnline + t.Total;
                                ttlTanningOnline = ttlTanningOnline + t.Total;
                                break;

                            case "PAYPAL":
                                ttlPayPal = ttlPayPal + t.Total;
                                ttlTanningPayPal = ttlTanningPayPal + t.Total;
                                break;

                            default:
                                ttlOther = ttlOther + t.Total;
                                ttlTanningOther = ttlTanningOther + t.Total;
                                break;
                        }
                    }
                    includeTransaction = true;
                }
            }
            catch (Exception ex)
            {
                functionsClass.SendErrorMail("Reports: TanningsSales", ex, transactionDate);
                errorMessage.Text = "Unable to load Tanning log";
            }
        }

        protected void MartialArtsSales(string trnsDate)
        {
            try
            {
                bool inc = true;
                List<HOTBAL.Transaction> maTransactions = sqlClass.GetAllMartialArtTransactions(trnsDate);

                foreach (HOTBAL.Transaction t in maTransactions)
                {
                    litMartialArtSales.Text += "<tr><td valign='top'>";

                    if (t.Void)
                    {
                        litMartialArtSales.Text += "<b>**VOID**</b><br>";
                        inc = false;
                    }

                    litMartialArtSales.Text += "<a href='/HOTSDA/SDAPOS/TransactionDetails.aspx?ID=" + t.ID + "&Date=" + trnsDate + "'>" + t.ID + "</a><br>";
                    if (t.Void)
                    {
                        litMartialArtSales.Text += "<a href='/HOTSDA/SDAPOS/trans_unvoid.aspx?ID=" + t.ID + "&Date=" + trnsDate + "&type=M'>Unvoid</a><br>";
                    }
                    else
                    {
                        litMartialArtSales.Text += "<a href='/HOTSDA/SDAPOS/trans_void.aspx?ID=" + t.ID + "&Date=" + trnsDate + "&type=M'>Void</a><br>";
                    }

                    litMartialArtSales.Text += "<a href='/HOTSDA/SDAPOS/TransactionReceipt.aspx?ID=" + t.ID + "&Date=" + trnsDate + "'>Receipt</a>";

                    HOTBAL.Student buyerInfo = sqlClass.GetStudentInformation(t.CustomerID);
                    litMartialArtSales.Text += "</td><td valign='top'>" + buyerInfo.FirstName + " " + buyerInfo.LastName + "</td><td><table>";
                    //Get transaction items
                    List<HOTBAL.TransactionItem> transactionItems = sqlClass.GetTransactionItems(t.ID);

                    foreach (HOTBAL.TransactionItem item in transactionItems)
                    {
                        litMartialArtSales.Text += "<tr><td>" + item.ProductName
                                        + "&nbsp;&nbsp;(" + String.Format("{0:$0.00}", item.Price) + ")"
                                        + "</td><td>" + item.Quantity
                                        + "</td></tr>";
                    }
                    litMartialArtSales.Text += "</table></td><td valign='top'>" + t.Location;
                    litMartialArtSales.Text += "</td><td valign='top'>" + t.Seller;
                    litMartialArtSales.Text += "</td><td valign='top'>" + String.Format("{0:$0.00}", t.Payment);
                    litMartialArtSales.Text += "</td><td valign='top'>" + String.Format("{0:$0.00}", t.Total);
                    litMartialArtSales.Text += "</td></tr>";

                    ttlTax = ttlTax + t.Tax;
                    ttlMartialTax = ttlMartialTax + t.Tax;
                    if (inc)
                    {
                        if (t.Tax > 0)
                        {
                            ttlMartialTaxed = ttlMartialTaxed + t.Total;
                            ttlTaxed = ttlTaxed + t.Total;
                        }
                        else
                        {
                            ttlMartialNonTaxed = ttlMartialNonTaxed + t.Total;
                            ttlNonTaxed = ttlNonTaxed + t.Total;
                        }

                        switch (t.Payment)
                        {
                            case "Cash":
                                ttlCash = ttlCash + t.Total;
                                ttlMartialCash = ttlMartialCash + t.Total;
                                break;

                            case "CC":
                                ttlCredit = ttlCredit + t.Total;
                                ttlMartialCredit = ttlMartialCredit + t.Total;
                                break;

                            case "Check":
                                ttlCheck = ttlCheck + t.Total;
                                ttlMartialCheck = ttlMartialCheck + t.Total;
                                break;

                            case "Trade":
                                ttlTrade = ttlTrade + t.Total;
                                ttlMartialTrade = ttlMartialTrade + t.Total;
                                break;

                            case "GC":
                                ttlGiftCard = ttlGiftCard + t.Total;
                                ttlMartialGiftCard = ttlMartialGiftCard + t.Total;
                                break;

                            case "ONLINE":
                                ttlOnline = ttlOnline + t.Total;
                                ttlMartialOnline = ttlMartialOnline + t.Total;
                                break;

                            case "PAYPAL":
                                ttlPayPal = ttlPayPal + t.Total;
                                ttlMartialPayPal = ttlMartialPayPal + t.Total;
                                break;

                            default:
                                ttlOther = ttlOther + t.Total;
                                ttlMartialOther = ttlMartialOther + t.Total;
                                break;
                        }
                    }
                    inc = true;
                }
            }
            catch (Exception ex)
            {
                functionsClass.SendErrorMail("Reports: MartialArtsSales", ex, trnsDate);
                errorMessage.Text = "Unable to load martial arts log";
            }
        }

        protected void Totals()
        {
            ttlTotal = ttlCash + ttlCredit + ttlCheck + ttlOnline + ttlPayPal;
            litCompleteTotals.Text = "<tr><td align='right'><b>Cash Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlCash) + "</td>";
            litCompleteTotals.Text += "<td align='right'><b>Credit Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlCredit) + "</td></tr>";
            litCompleteTotals.Text += "<tr><td align='right'><b>Check Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlCheck) + "</td>";
            litCompleteTotals.Text += "<td align='right'><b>Trade Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTrade) + "</td></tr>";
            litCompleteTotals.Text += "<tr><td align='right'><b>Online Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlOnline) + "</td>";
            litCompleteTotals.Text += "<td align='right'><b>PayPal Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlPayPal) + "</td></tr>";
            litCompleteTotals.Text += "<tr><td align='right'><b>Gift Card Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlGiftCard) + "</td>";
            litCompleteTotals.Text += "<td align='right'><b>Other Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlOther) + "</td></tr>";
            litCompleteTotals.Text += "<tr><td align='right'><b>Total Taxed: </b> </td><td>" + String.Format("{0:$0.00}", ttlTaxed) + "</td>";
            litCompleteTotals.Text += "<td align='right'><b>Total NonTaxed: </b> </td><td>" + String.Format("{0:$0.00}", ttlNonTaxed) + "</td></tr>";
            litCompleteTotals.Text += "<tr><td align='right'><b>Complete Total: </b></td><td>" + String.Format("{0:$0.00}", ttlTotal) + "</td>";
            litCompleteTotals.Text += "<td align='right'><b>Total Tax: </b></td><td>" + String.Format("{0:$0.00}", ttlTax) + "</td></tr></table>";
        }

        protected void TanningTotals()
        {
            ttlTanningTotal = ttlTanningCash + ttlTanningCredit + ttlTanningCheck + ttlTanningOnline + ttlTanningPayPal;
            litTanningTotals.Text = "<tr><td align='right'><b>Cash Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningCash) + "</td>";
            litTanningTotals.Text += "<td align='right'><b>Credit Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningCredit) + "</td></tr>";
            litTanningTotals.Text += "<tr><td align='right'><b>Check Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningCheck) + "</td>";
            litTanningTotals.Text += "<td align='right'><b>Trade Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningTrade) + "</td></tr>";
            litTanningTotals.Text += "<tr><td align='right'><b>Online Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningOnline) + "</td>";
            litTanningTotals.Text += "<td align='right'><b>PayPal Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningPayPal) + "</td></tr>";
            litTanningTotals.Text += "<tr><td align='right'><b>Gift Card Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningGiftCard) + "</td>";
            litTanningTotals.Text += "<td align='right'><b>Other Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningOther) + "</td></tr>";
            litTanningTotals.Text += "<tr><td align='right'><b>Total Taxed: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningTaxed) + "</td>";
            litTanningTotals.Text += "<td align='right'><b>Total NonTaxed: </b> </td><td>" + String.Format("{0:$0.00}", ttlTanningNonTaxed) + "</td></tr>";
            litTanningTotals.Text += "<tr><td align='right'><b>Complete Total: </b></td><td>" + String.Format("{0:$0.00}", ttlTanningTotal) + "</td>";
            litTanningTotals.Text += "<td align='right'><b>Total Tax: </b></td><td>" + String.Format("{0:$0.00}", ttlTanningTax) + "</td></tr></table>";
        }

        protected void MartialTotals()
        {
            ttlMartialTotal = ttlMartialCash + ttlMartialCredit + ttlMartialCheck + ttlMartialOnline + ttlMartialPayPal;
            litMartialArtTotals.Text = "<tr><td align='right'><b>Cash Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialCash) + "</td>";
            litMartialArtTotals.Text += "<td align='right'><b>Credit Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialCredit) + "</td></tr>";
            litMartialArtTotals.Text += "<tr><td align='right'><b>Check Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialCheck) + "</td>";
            litMartialArtTotals.Text += "<td align='right'><b>Trade Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialTrade) + "</td></tr>";
            litMartialArtTotals.Text += "<tr><td align='right'><b>Online Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialOnline) + "</td>";
            litMartialArtTotals.Text += "<td align='right'><b>PayPal Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialPayPal) + "</td></tr>";
            litMartialArtTotals.Text += "<tr><td align='right'><b>Gift Card Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialGiftCard) + "</td>";
            litMartialArtTotals.Text += "<td align='right'><b>Other Total: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialOther) + "</td></tr>";
            litMartialArtTotals.Text += "<tr><td align='right'><b>Total Taxed: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialTaxed) + "</td>";
            litMartialArtTotals.Text += "<td align='right'><b>Total NonTaxed: </b> </td><td>" + String.Format("{0:$0.00}", ttlMartialNonTaxed) + "</td></tr>";
            litMartialArtTotals.Text += "<tr><td align='right'><b>Complete Total: </b></td><td>" + String.Format("{0:$0.00}", ttlMartialTotal) + "</td>";
            litMartialArtTotals.Text += "<td align='right'><b>Total Tax: </b></td><td>" + String.Format("{0:$0.00}", ttlMartialTax) + "</td></tr></table>";
        }
    }
}