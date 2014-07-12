using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAFederation
{
    public partial class Federation : System.Web.UI.MasterPage
    {
        SDAFunctionsClass functionsClass = new SDAFunctionsClass();
        FederationMethods methodsClass = new FederationMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            mainPage.NavigateUrl = HOTBAL.FederationConstants.MAIN_DEFAULT_URL;
            pointOfSale.NavigateUrl = HOTBAL.FederationConstants.POS_URL;
            adminSection.NavigateUrl = HOTBAL.FederationConstants.ADMIN_DEFAULT_URL;
            reportProblem.NavigateUrl = HOTBAL.FederationConstants.PROBLEM_URL;
            if (functionsClass.SchoolSelected())
            {
                viewStudents.NavigateUrl = HOTBAL.FederationConstants.STUDENT_LIST_URL;
                addStudent.NavigateUrl = HOTBAL.FederationConstants.ADD_STUDENT_INFORMATION_URL;
                search.NavigateUrl = HOTBAL.FederationConstants.SEARCH_URL;
            }
        }
    }
}