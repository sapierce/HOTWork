using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace HOTTropicalTans.admin
{
    public partial class _default : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Administration";
            try
            {
                if (functionsClass.isAdmin())
                {
                    loginPanel.Visible = false;
                    adminPanel.Visible = true;
                }
                else
                {
                    loginPanel.Visible = true;
                    adminPanel.Visible = false;
                }

                if (!Page.IsPostBack)
                {
                    transactionStartDate.Text = transactionEndDate.Text = bedDate.Text = productCountDate.Text = functionsClass.FormatDash(DateTime.Now);

                    ClearDropDowns();
                    ProductLoad();
                    PackagesLoad();
                    SpecialsLoad();
                    EmployeeLoad();
                    BedLoad();
                }
            }
            catch (Exception ex)
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC;
                sqlClass.LogErrorMessage(ex, "", "Admin: PageLoad");
            }
        }
        public void ClearDropDowns()
        {
            editBedList.Items.Clear();
            editItemList.Items.Clear();
            editPackageList.Items.Clear();
            editSpecialList.Items.Clear();
            editEmployeeList.Items.Clear();
            employeeWorkedList.Items.Clear();
            employeeScheduleList.Items.Clear();
            employeeWorkedList.Items.Add(new ListItem("All", "All"));
            employeeScheduleList.Items.Add(new ListItem("All", "All"));
        }

        public void BedLoad()
        {
            List<HOTBAL.Bed> bedList = sqlClass.GetAllActiveBeds();

            if (bedList.Count > 0)
            {
                foreach (HOTBAL.Bed b in bedList)
                {
                    editBedList.Items.Add(new ListItem(b.BedType + " - " + b.BedLong, b.BedID.ToString()));
                }
            }
            else
                editBedList.Items.Add(new ListItem("-No Beds Available-", "0"));
        }

        public void ProductLoad()
        {
            List<HOTBAL.Product> productList = sqlClass.GetAllProducts();

            if (productList.Count > 0)
            {
                foreach (HOTBAL.Product p in productList)
                {
                    string prodCategory = "Unknown";
                    string prodType = "Unknown";

                    switch (p.ProductType)
                    {
                        case "ACC":
                            prodCategory = "Accessories";
                            break;

                        case "LTN":
                            prodCategory = "Lotions";
                            break;

                        case "DIS":
                            prodCategory = "Discounts";
                            break;

                        case "PKG":
                            prodCategory = "Packages";
                            break;

                        case "SPC":
                            prodCategory = "Specials";
                            break;

                        case "OTH":
                            prodCategory = "Other";
                            break;
                    }

                    switch (p.ProductSubType)
                    {
                        case "BB":
                            prodType = "BigBed";
                            break;

                        case "DS":
                            prodType = "Discount";
                            break;

                        case "GB":
                            prodType = "Gift Bag";
                            break;

                        case "LB":
                            prodType = "Lip Balm";
                            break;

                        case "LO":
                            prodType = "Moisturizer";
                            break;

                        case "LM":
                            prodType = "Mystic";
                            break;

                        case "LN":
                            prodType = "Non-Tingle";
                            break;

                        case "LS":
                            prodType = "Sample";
                            break;

                        case "LT":
                            prodType = "Tingle";
                            break;

                        case "MY":
                            prodType = "Mystic";
                            break;

                        case "OT":
                            prodType = "Other";
                            break;

                        case "PH":
                            prodType = "PowerHouse";
                            break;

                        case "SB":
                            prodType = "SmallBed";
                            break;
                    }

                    if ((prodCategory != "Packages") && (prodCategory != "Specials"))
                    {
                        editItemList.Items.Add(new ListItem(prodCategory + " - " + prodType + " - " + p.ProductName, p.ProductID.ToString()));
                    }
                }
            }
            else
                editItemList.Items.Add(new ListItem("-No Products Available-", "0"));
        }

        public void EmployeeLoad()
        {
            List<HOTBAL.Employee> employeeList = sqlClass.GetAllEmployees();

            if (employeeList.Count > 0)
            {
                foreach (HOTBAL.Employee e in employeeList)
                {
                    employeeWorkedList.Items.Add(new ListItem(e.LastName + ", " + e.FirstName, e.EmployeeID.ToString()));
                    employeeScheduleList.Items.Add(new ListItem(e.LastName + ", " + e.FirstName, e.EmployeeID.ToString()));
                    editEmployeeList.Items.Add(new ListItem(e.LastName + ", " + e.FirstName, e.EmployeeID.ToString()));
                }
            }
            else
            {
                employeeWorkedList.Items.Add(new ListItem("-No Employees Found-", "0"));
                employeeScheduleList.Items.Add(new ListItem("-No Employees Found-", "0"));
                editEmployeeList.Items.Add(new ListItem("-No Employees Found-", "0"));
            }
        }

        public void PackagesLoad()
        {
            List<HOTBAL.Package> packageList = sqlClass.GetAllPackages();

            foreach (HOTBAL.Package p in packageList)
            {
                editPackageList.Items.Add(new ListItem(p.PackageName, p.PackageID.ToString()));
            }
        }

        public void SpecialsLoad()
        {
            List<HOTBAL.Special> specialList = sqlClass.GetAllSpecials();

            foreach (HOTBAL.Special p in specialList)
            {
                editSpecialList.Items.Add(new ListItem(p.SpecialName, p.SpecialID.ToString()));
            }
        }

        protected void adminLogin_Click(object sender, EventArgs e)
        {
            if ((sqlClass.AdministrationCheck(adminLoginPassword.Text, "Login")) || (sqlClass.AdministrationCheck(adminLoginPassword.Text, "Override"))) //(adminLoginPassword.Text == "miriam")
            {
                loginPanel.Visible = false;
                adminPanel.Visible = true;
                HttpContext.Current.Session["admin"] = "Yes";
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = "Invalid Password.  Please try again.";
            }
        }

        protected void editItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductEdit.aspx?ID=" + editItemList.SelectedValue, false);
        }

        protected void editPackage_Click(object sender, EventArgs e)
        {
            Response.Redirect("PackageEdit.aspx?ID=" + editPackageList.SelectedValue, false);
        }

        protected void editSpecial_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpecialsEdit.aspx?ID=" + editItemList.SelectedValue, false);
        }

        protected void addProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductAdd.aspx", false);
        }

        protected void addPackage_Click(object sender, EventArgs e)
        {
            Response.Redirect("PackageAdd.aspx", false);
        }

        protected void addSpecial_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpecialsAdd.aspx", false);
        }

        protected void employeeWorked_Click(object sender, EventArgs e)
        {
            if (employeeWorkedList.SelectedValue == "All")
            {
                Response.Redirect("Reports/EmployeeClockedHours.aspx?Admin=Yes", false);
            }
            else
            {
                Response.Redirect("/Schedule/Employees/EmployeeInformation.aspx?ID=" + employeeWorkedList.SelectedValue + "&Admin=Yes", false);
            }
        }

        protected void employeeSchedule_Click(object sender, EventArgs e)
        {
            if (employeeScheduleList.SelectedValue == "All")
            {
                Response.Redirect("Reports/EmployeeScheduledHours.aspx?Admin=Yes", false);
            }
            else
            {
                Response.Redirect("/Schedule/Employees/EmployeeInformation.aspx?ID=" + employeeScheduleList.SelectedValue + "&Admin=Yes", false);
            }
        }

        protected void addEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Schedule/Admin/EmployeeAdd.aspx", false);
        }

        protected void editEmployee_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Schedule/Admin/EmployeeEdit.aspx?ID=" + editEmployeeList.SelectedValue, false);
        }

        protected void notesFromEmployees_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeNotes.aspx?From=F", false);
        }

        protected void notesToEmployees_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeNotes.aspx?From=E", false);
        }

        protected void siteNotice_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiteNotice.aspx", false);
        }

        protected void combineAccounts_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerCombine.aspx", false);
        }

        protected void changePasswords_Click(object sender, EventArgs e)
        {
            Response.Redirect("SitePasswords.aspx", false);
        }

        protected void editHours_Click(object sender, EventArgs e)
        {
            Response.Redirect("HoursEdit.aspx", false);
        }

        protected void bedInformation_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports/BedInformation.aspx?Date=" + bedDate.Text, false);
        }

        protected void addBed_Click(object sender, EventArgs e)
        {
            Response.Redirect("BedsAdd.aspx", false);
        }

        protected void editBed_Click(object sender, EventArgs e)
        {
            Response.Redirect("BedsEdit.aspx?ID=" + editBedList.SelectedValue, false);
        }

        protected void viewFullTransaction_Click(object sender, EventArgs e)
        {
            Response.Redirect("/HOTPOS/TransactionLog.aspx?StartDate=" + transactionStartDate.Text + "&EndDate=" + transactionEndDate.Text + "&Totals=" + totalsOnly.Checked.ToString(), false);
        }

        protected void productCount_Click(object sender, EventArgs e)
        {
            Response.Redirect("/employees/ProductEmployeeCounts.aspx?Date=" + productCountDate.Text, false);
        }

        protected void fullInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports/ProductInventory.aspx", false);
        }

        protected void deletedProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductsDeleted.aspx", false);
        }
    }
}