using System;
using System.Collections.Generic;
using System.Web.UI;

namespace HOTSelfDefense.Admin.Reports
{
    public partial class ReportActiveStudents : System.Web.UI.Page
    {
        private HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        private HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - All Active Students";

            List<HOTBAL.Student> allStudents = sqlClass.GetAllStudents();

            dgrActiveStudents.DataSource = allStudents;
            dgrActiveStudents.DataBind();
        }
    }
}