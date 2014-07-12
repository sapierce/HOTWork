using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class _default : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass FunctionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "HOT Self Defense - Administration";

            if (!Page.IsPostBack)
            {
                // Load defaults
                string currentDate = FunctionsClass.FormatDash(DateTime.Now);
                if (Request.QueryString["Date"] != null)
                    currentDate = Request.QueryString["Date"].ToString();

                txtFullTrns.Text = currentDate;
                txtBirthday.Text = "14";
                txtPassBeg.Text = currentDate;
                txtPassEnd.Text = currentDate;
                txtLastTip.Text = currentDate;
                txtAttend.Text = currentDate;

                // Load drop downs
                PopulateArts();
                PopulateBelts();
                PopulateTips();
                PopulateCourses();
                PopulateTerms();
                PopulateItems();
                PopulateInstructors();
            }
        }

        private void PopulateArts()
        {
            ddlArt.Items.Clear();
            ddlArt.Items.Add(new ListItem("-Choose An Art-", "0"));

            List<HOTBAL.Art> artList = sqlClass.GetAllSDAArts();

            if (artList != null)
            {
                if (String.IsNullOrEmpty(artList[0].Error))
                {
                    foreach (HOTBAL.Art a in artList)
                    {
                        ddlArt.Items.Add(new ListItem(a.Title, a.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateBelts()
        {
            ddlBelt.Items.Clear();
            ddlBelt.Items.Add(new ListItem("-Choose A Belt-", "0"));

            List<HOTBAL.Belt> beltList = sqlClass.GetAllSDABelts();

            if (beltList != null)
            {
                if (string.IsNullOrEmpty(beltList[0].Error))
                {
                    foreach (HOTBAL.Belt b in beltList)
                    {
                        ddlBelt.Items.Add(new ListItem(sqlClass.GetArtTitle(b.ArtID) + "-" + b.Title, b.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateTips()
        {
            ddlTip.Items.Clear();
            ddlTip.Items.Add(new ListItem("-Choose art Tip-", "0"));

            List<HOTBAL.Tip> tipList = sqlClass.GetAllTips();

            if (tipList != null)
            {
                if (string.IsNullOrEmpty(tipList[0].Error))
                {
                    foreach (HOTBAL.Tip t in tipList)
                    {
                        ddlTip.Items.Add(new ListItem(t.Title, t.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateCourses()
        {
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("-Choose A Class-", "0"));

            List<HOTBAL.Course> courseList = sqlClass.GetAllActiveRepeatingClasses();

            if (courseList != null)
            {
                if (String.IsNullOrEmpty(courseList[0].Error))
                {
                    foreach (HOTBAL.Course c in courseList)
                    {
                        ddlCourse.Items.Add(new ListItem((c.Title + " (" + sqlClass.GetArtTitle(c.FirstArtID) + (c.SecondArtID == 0 ? "" : "/" + sqlClass.GetArtTitle(c.SecondArtID)) + ") - " + c.Day + " - " + c.Time), c.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateTerms()
        {
            ddlTerm.Items.Clear();
            ddlTerm.Items.Add(new ListItem("-Choose A Term-", "0"));

            List<HOTBAL.Term> termsList = sqlClass.GetAllTerms();

            if (termsList != null)
            {
                if (String.IsNullOrEmpty(termsList[0].Error))
                {
                    foreach (HOTBAL.Term t in termsList)
                    {
                        ddlTerm.Items.Add(new ListItem(t.English, t.ID.ToString()));
                    }
                }
            }
        }

        private void PopulateItems()
        {
            ddlItem.Items.Clear();
            ddlItem.Items.Add(new ListItem("-Choose An Item-", "0"));

            List<HOTBAL.Product> itemsList = new List<HOTBAL.Product>();
            itemsList = sqlClass.GetAllItems();

            if (itemsList != null)
            {
                if (itemsList != null)
                {
                    foreach (HOTBAL.Product i in itemsList)
                    {
                        ddlItem.Items.Add(new ListItem(i.ProductName, i.ProductID.ToString()));
                    }
                }
            }
        }

        private void PopulateInstructors()
        {
            ddlInst.Items.Clear();
            ddlInst.Items.Add(new ListItem("-Choose An Instructor-", "0"));

            List<HOTBAL.Instructor> instList = sqlClass.GetAllInstructors();

            if (instList != null)
            {
                if (String.IsNullOrEmpty(instList[0].Error))
                {
                    foreach (HOTBAL.Instructor i in instList)
                    {
                        ddlInst.Items.Add(new ListItem(i.FirstName + " " + i.LastName, i.ID.ToString()));
                    }
                }
            }
        }

        #region onClicks
        //Adds
        public void btnAddInst_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Instructors.aspx?Action=add");
        }

        public void btnAddArt_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Arts.aspx?Action=add");
        }

        public void btnAddBelt_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Belts.aspx?Action=add");
        }

        public void btnAddTip_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Tips.aspx?Action=add");
        }

        public void btnAddTerm_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Terms.aspx?Action=add");
        }

        public void btnAddItem_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Items.aspx?Action=add");
        }

        //Edits
        public void btnEditInst_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Instructors.aspx?Action=edit&ID=" + ddlInst.SelectedValue.ToString());
        }

        public void btnEditArt_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Arts.aspx?Action=edit&ID=" + ddlArt.SelectedValue);
        }

        public void btnEditBelt_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Belts.aspx?Action=edit&ID=" + ddlBelt.SelectedValue);
        }

        public void btnEditTip_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Tips.aspx?Action=edit&ID=" + ddlTip.SelectedValue);
        }

        public void btnEditTerm_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Terms.aspx?Action=edit&ID=" + ddlTerm.SelectedValue);
        }

        public void btnEditItem_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Items.aspx?Action=edit&ID=" + ddlItem.SelectedValue);
        }

        public void btnEditCourse_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Classes.aspx?Action=edit&ID=" + ddlCourse.SelectedValue);
        }

        //Others
        public void btnFullTrns_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("http://www.hottropicaltans.net/schedule/admin/reports/translogfull.aspx?Date=" + txtFullTrns.Text + "&Store=B", false);
        }

        public void btnInven_onClick(Object sender, EventArgs e)
        {
        }

        public void btnBirthday_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Reports/ReportBirthdays.aspx?d=" + txtBirthday.Text);
        }

        public void btnPass_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Reports/ReportBelts.aspx?endDate=" + txtPassEnd.Text + "&beginDate=" + txtPassBeg.Text);
        }

        public void btnLastTip_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Reports/ReportLastTip.aspx?d=" + txtLastTip.Text);
        }

        public void btnAttend_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Reports/ReportStudentAttendance.aspx?d=" + txtAttend.Text);
        }

        public void btnActive_onClick(Object sender, EventArgs e)
        {
            Response.Redirect("Reports/ReportActiveStudents.aspx");
        }
        #endregion
    }
}